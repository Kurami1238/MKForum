using MKForum.Helpers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MKForum.Managers
{
    public class MemberFollowManager
    {
        public List<MemberFollow> GetMemberFollows(string MemberID)
        {
            string connectionStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM MemberFollows
                    WHERE MemberID = @MemberID;
                ";

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using(SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        List<MemberFollow> Follows = new List<MemberFollow>();
                        connection.Open();

                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        SqlDataReader reader = command.ExecuteReader();

                        while(reader.Read())
                        {
                            MemberFollow Follow = new MemberFollow()
                            {
                                MemberID = (Guid)reader["MemberID"],
                                PostID = (Guid)reader["PostID"],
                                FollowStatus = (bool)reader["FollowStatus"],
                                ReadedDate = (DateTime)reader["ReadedDate"],
                                Replied = (bool)reader["Replied"],
                            };
                            Follows.Add(Follow);
                        }
                        return Follows;
                    }    
                }

            }
            catch(Exception ex)
            {
                Logger.WriteLog("MemberFollowManager.GetMemberFollows", ex);
                throw;
            }
        }

        public MemberFollow GetMemberFollowThisPost(string MemberID, string PostID)
        {
            string connectionStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM MemberFollows
                    WHERE MemberID = @MemberID AND PostID = @PostID;
                ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@PostID", PostID);
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();

                        MemberFollow Follow = new MemberFollow()
                        {
                            MemberID = (Guid)reader["MemberID"],
                            PostID = (Guid)reader["PostID"],
                            FollowStatus = (bool)reader["FollowStatus"],
                            ReadedDate = (DateTime)reader["ReadedDate"],
                            Replied = (bool)reader["Replied"],
                        };
                        return Follow;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberFollowManager.GetMemberFollows", ex);
                throw;
            }
        }

        public void Updatetrack(string MemberID, string PostID, int FollowStatus)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    UPDATE MemberFollows
                    SET FollowStatus = @FollowStatus
                    WHERE MemberID = @MemberID AND PostID = @PostID;
                ";
            try
            {
                using(SqlConnection connection = new SqlConnection(connStr))
                {
                    using(SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@FollowStatus", FollowStatus);
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@PostID", PostID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Updatetrack", ex);
                throw;
            }
        }
    }
}