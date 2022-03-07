using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKForum.Managers;
using MKForum.Models;

namespace MKForum
{
    public partial class Register : System.Web.UI.Page
    {
        private AccountManager _mga = new AccountManager();
        private RegisterManager _mgr = new RegisterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this._mga.IsRegister())
            {
                this.plcInfo.Visible = true;
                this.plcReg.Visible = false;
                Member account = this._mga.GetCurrentUser();
                this.ltlAccount.Text = account.Account + "成功註冊";
            }
            else
            {
                this.plcInfo.Visible = false;
                this.plcReg.Visible = true;
            }
        }

        protected void btnAcc_Click(object sender, EventArgs e)
        {
            string account = this.txtAcc.Text.Trim();

            if (string.IsNullOrWhiteSpace(account))
            {
                this.ltlAcc.Text = "帳號為空值，請輸入值";
            }
            else if (this._mga.CheckAccount(account))
            {
                //密碼強度-使用regex进行格式设置 至少有数字、小写字母，最少3个字符、最长8个字符
                Regex regex = new Regex(@"(?=.*[0-9])(?=.*[a-z]).{3,8}");
                if (regex.IsMatch(account))
                {
                    this.ltlAcc.Text = "帳號不重複，通過";
                }
                else
                {
                    this.ltlAcc.Text = "帳號格式，不通過";
                }

            }
            else if (!this._mga.CheckAccount(account))
            {
                this.ltlAcc.Text = "帳號重複，請再取一個";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.CheckInput())//是否通過檢查
                return;
            string account = this.txtAcc.Text.Trim();
            string password = this.txtPwd.Text.Trim();
            string mail = this.txtMail.Text.Trim();
            string cap = this.txtCapt.Text.Trim();

            Member member = new Member();
            member.Account = account;
            member.Password = password;
            member.Email = mail;
            MemberRegister memberRegister = new MemberRegister();
            memberRegister.Captcha = cap;

            if (this._mga.TryRegister(account, password, mail, cap))
            {
                _mgr.CreateRegister(member, memberRegister);
                Response.Redirect(Request.RawUrl);
            }
            if (!this._mga.TryRegister(account, password, mail, cap))
            {
                this.ltlReg.Text += "失敗<br/>";
            }


        }

        public bool CheckInput()
        {
            Regex regex = new Regex(@"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{3,8}");
            List<string> msgList = new List<string>();
            if (string.IsNullOrWhiteSpace(this.txtAcc.Text))
            {
                msgList.Add("帳號為必填，至少有數字、小寫字母 3-8位數");
            }

            else if (this._mga.CheckAccount(this.txtAcc.Text))
            {
                //密碼強度-使用regex进行格式设置 至少有数字、小写字母，最少3个字符、最长8个字符
                Regex regex2 = new Regex(@"(?=.*[0-9])(?=.*[a-z]).{3,8}");
                if (regex2.IsMatch(this.txtAcc.Text))
                {
                    this.ltlAcc.Text = "帳號不重複，通過";
                }
                else
                {
                    this.ltlAcc.Text = "帳號格式，不通過";
                }

            }


            if (string.IsNullOrWhiteSpace(this.txtPwd.Text))
            {
                msgList.Add("密碼為必填，至少有數字、大小寫字母，最少3個字符、最長8個字符");
            }
            if (regex.IsMatch(this.txtPwd.Text))
            {

            }
            else if (!regex.IsMatch(this.txtPwd.Text))
            {
                msgList.Add("密碼不符合，至少有數字、大小寫字母，最少3個字符、最長8個字符");
            }
            if (string.IsNullOrWhiteSpace(this.txtMail.Text))
            {
                msgList.Add("Email為必填，要符合Email格式.");
            }
            if (new EmailAddressAttribute().IsValid(this.txtMail.Text))
            {

            }
            else if (!new EmailAddressAttribute().IsValid(this.txtMail.Text))
            {

                msgList.Add("不符合Email格式.");
            }
            if (string.IsNullOrWhiteSpace(this.txtCapt.Text))
            {
                msgList.Add("驗證碼為必填，尚未寫出來");
            }

            //如果有錯誤發生，就傳值false,並提示錯誤訊息
            if (msgList.Count > 0)
            {
                string allError = string.Join("<br/>", msgList);
                this.ltlReg.Text = allError;
                return false;
            }

            return true;

        }
    }
}