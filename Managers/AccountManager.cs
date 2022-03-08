using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using MKForum.Helpers;
using MKForum.Models;

namespace MKForum.Managers
{
    public class AccountManager
    {
        public bool TryLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;

            Member memberAcc = this.GetAccount(account);

            if (memberAcc == null)//找不到就登入失敗
                return false;

            if (string.Compare(memberAcc.Account, account, true) == 0)
                isAccountRight = true;
            if (memberAcc.Password == password)
                isPasswordRight = true;
            //檢查帳號密碼是否正確
            bool result = (isAccountRight && isPasswordRight);

            //帳密正確，把值寫入Session
            if (result)
            {
                HttpContext.Current.Session["Member"] = new Member()
                {
                    Account = account,
                    Password = password
                };
            }


            return result;
        }

        public Member GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText = @"
                SELECT*
                FROM Members
                WHERE Account=@account
                     
                   ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@account", account);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Member member = new Member()
                            {
                                Account = reader["Account"] as string,
                                Password = reader["PWD"] as string,
                                Email = reader["Email"] as string
                            };
                            return member;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccount", ex);
                throw;
            }
        }

        public bool IsLogin()
        {
            Member account = GetCurrentUser();

            return (account != null);
        }
        public bool IsRegister()
        {
            Member account = GetCurrentUser();

            return (account != null);
        }
        public Member GetCurrentUser()
        {
            Member account = HttpContext.Current.Session["Member"] as Member;

            return account;

        }
        public void Logout()
        {
            HttpContext.Current.Session.Remove("Member");
        }



        public bool CheckAccount(string account)
        {
            bool isAccountRight = false;

            Member memberAcc = this.GetAccount(account);

            if (memberAcc == null)//找不到就能註冊
                isAccountRight = true;

            if (string.Empty == account)
                isAccountRight = false;

            bool result = isAccountRight;

            return result;
        }



        public bool TryRegister(string account, string password, string email, string capt)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isEmailRight = false;
            bool isCaptRight = false;

            Member memberAcc = this.GetAccount(account);//抓SQL帳號

            //帳號
            if (memberAcc == null)//找不到就能註冊
                isAccountRight = true;
            if (string.Empty == account)
                isAccountRight = false;
            //密碼強度-使用regex进行格式设置 至少有数字、大小写字母，最少3个字符、最长8个字符
            Regex regex = new Regex(@"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{3,8}");
            if (regex.IsMatch(password))
            {
                isPasswordRight = true;
            }
            else
            {
                isPasswordRight = false;
            }
            //Email格式
            if (email == null)
            {
                isEmailRight = false;
            }
            if (new EmailAddressAttribute().IsValid(email))
            {
                isEmailRight = true;
            }
            else
            {

                isEmailRight = false;
            }
            //驗證碼
            if (capt != null)
            {
                isCaptRight = true;
            }

            //檢查帳號、密碼、Email格式、驗證碼是否正確
            //bool result = (isAccountRight && isPasswordRight && isEmailRight&&isCaptRight);
            bool result = (isAccountRight && isPasswordRight && isEmailRight);

            //帳密正確，把值寫入Session
            if (result)
            {
                HttpContext.Current.Session["Member"] = new Member()
                {
                    Account = account,
                    Password = password,
                    Email = email,

                };
                HttpContext.Current.Session["MemberRegister"] = new MemberRegister()
                {

                    Captcha = capt
                };
            }


            return result;

        }



        public bool TryFindpassword(string account, string mail)
        {
            bool isAccountRight = false;
            bool isMailRight = false;

            Member memberAcc = GetAccount(account);//抓SQL帳號

            if (memberAcc == null)//找不到帳號
                return false;
            if (mail == null)//找不到Email
                return false;
            if (memberAcc.Account == account)
                isAccountRight = true;
            if (memberAcc.Email == mail)
                isMailRight = true;
            bool result = (isAccountRight && isMailRight);

            //帳密正確，把值寫入Session
            if (result)
            {
                HttpContext.Current.Session["Member"] = new Member()
                {
                    Account = account,
                    Email = mail
                };
            }
            return result;
        }





        //驗證碼
        public void ProcessRequest(HttpContext context)
        {
            string checkCode = GenCode(5);  // 產生5位隨機字元 
            context.Session["Code"] = checkCode; //將字串儲存到Session中，以便需要時進行驗證 
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(70, 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成隨機生成器 
                Random random = new Random();

                //清空圖片背景色 
                g.Clear(Color.White);

                // 畫圖片的背景噪音線 
                int i;
                for (i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //畫圖片的前景噪音點 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Gif";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }


        private string GenCode(int num)
        {
            string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] chastr = str.ToCharArray();
            // string[] source ={ "0", "1", "2","3", "4", "5", "6", "7","8", "9", "A", "B", "C","D", "E", "F", "G", "H","I", "J", "K", "L", "M","N", "O", "P", "Q", "R","S", "T", "U", "V", "W","X", "Y", "Z", "#", "$", "%","&", "@" }; 
            string code = "";
            Random rd = new Random();
            int i;
            for (i = 0; i < num; i++)
            {
                //code += source[rd.Next(0, source.Length)]; 
                code += str.Substring(rd.Next(0, str.Length), 1);
            }
            return code;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //驗證碼
    }
}