using MKForum.Helpers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MKForum.Managers
{
    public class MemberManager
    {
        public List<Members> GetMembers()
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM Members
                    ORDER BY MemberID;
                ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<Members> Members = new List<Members>();

                        while (reader.Read())
                        {
                            Members member = new Members()
                            {
                                MemberID = (Guid)reader["MemberID"],
                                MemberStatus = (int)reader["MemberStatus"],
                                Account = reader["Account"] as string,
                                Password = reader["Password"] as string,
                                Email = reader["Email"] as string,
                                NickName = reader["NickName"] as string,
                                Birthday = (DateTime)reader["Birthday"],
                                Sex = (int)reader["Sex"],
                            };
                            Members.Add(member);
                        }
                        return Members;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberManager.GetMembers", ex);
                throw;
            }
        }

        public Members GetMember(string name)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM Member
                    WHERE NickName = @name
                ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@name", name);

                        SqlDataReader reader = command.ExecuteReader();

                        Members member = new Members()
                        {
                            MemberID = (Guid)reader["MemberID"],
                            MemberStatus = (int)reader["MemberStatus"],
                            Account = reader["Account"] as string,
                            Password = reader["Password"] as string,
                            Email = reader["Email"] as string,
                            NickName = reader["NickName"] as string,
                            Birthday = (DateTime)reader["Birthday"],
                            Sex = (int)reader["Sex"],
                        };
                        return member;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberManager.GetMembers", ex);
                throw;
            }
        }

        //未完成
        public void CreateMember(Members member)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    INSERT INTO Member
                        (MemberStatus, Account, Password, Email, NickName, Birthday, Sex)
                    VALUES
                        (@MemberStatus, @Account, @Password, @Email, @NickName, @Birthday, @Sex)
                    ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(connectionString, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Account", member.Account);
                        command.Parameters.AddWithValue("@Password", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@NickName", member.NickName);
                        command.Parameters.AddWithValue("@Birthday", member.Birthday);
                        command.Parameters.AddWithValue("@Sex", member.Sex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberManager.GetMembers", ex);
                throw;
            }
        }

        public void UpdateMember(Members member)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    UPDATA Member
                    SET Account = @Account, Password = @Password, Email = @Email
                    Where
                ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(connectionString, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Account", member.Account);
                        command.Parameters.AddWithValue("@Password", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@NickName", member.NickName);
                        command.Parameters.AddWithValue("@Birthday", member.Birthday);
                        command.Parameters.AddWithValue("@Sex", member.Sex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberManager.GetMembers", ex);
                throw;
            }
        }
    }
}