using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MKForum.Helpers;
using MKForum.Models;

namespace MKForum.Managers
{
    public class RegisterManager
    {
        public void CreateRegister(Member member, MemberRegister memberRegister)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText = @"
             INSERT INTO Members(Account,PWD,Email)
            VALUES
            (@Account,@PWD,@Email)

            INSERT INTO MemberRecords (MemberID) 
             SELECT MemberID
             FROM Members;

             INSERT INTO MemberRegisters(MemberID)
             SELECT MemberID
             FROM Members;

             UPDATE MemberRegisters
             SET Captcha=(@Captcha)
             FROM MemberRegisters
             INNER JOIN Members
             ON MemberRegisters.MemberID=Members.MemberID;
                   ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@Account", member.Account);
                        command.Parameters.AddWithValue("@PWD", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        //command.Parameters.AddWithValue("@MemberID", memberRegister.MemberID);
                        command.Parameters.AddWithValue("@Captcha", memberRegister.Captcha);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("RegisterManager.CreateRegister", ex);
                throw;
            }
        }
    }
}