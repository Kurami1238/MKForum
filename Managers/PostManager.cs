using MKForum.Helpers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MKForum.Managers
{
    public class PostManager
    {
        private static List<string> _msgList = new List<string>();
        public static void CreatePost(Guid member, int cboard, string title, string postcotent)
        {

            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    INSERT INTO Posts
                    (MemberID, CboardID, PostView, Title, PostCotent)
                    VALUES
                    (@memberID, @cboardID, @postView, @title, @postCotent)
                    ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue(@"memberID", member);
                        command.Parameters.AddWithValue(@"cboardID", cboard);
                        command.Parameters.AddWithValue(@"postView", 0);
                        command.Parameters.AddWithValue(@"title", title);
                        command.Parameters.AddWithValue(@"postCotent", postcotent);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,CreatePost", ex);
                throw;
            }
        }
        public static void CreatePost(Guid member, Guid postid, int cboard, string title, string postcotent)
        {

            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    INSERT INTO Posts
                    (MemberID, PointID, CboardID, PostView, Title, PostCotent)
                    VALUES
                    (@memberID, @pointID, @cboardID, @postView, @title, @postCotent)
                    ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue(@"memberID", member);
                        command.Parameters.AddWithValue(@"pointID", postid);
                        command.Parameters.AddWithValue(@"cboardID", cboard);
                        command.Parameters.AddWithValue(@"postView", 0);
                        command.Parameters.AddWithValue(@"title", title);
                        command.Parameters.AddWithValue(@"postCotent", postcotent);
                    }
                }
                CreateInMemberFollows(member, postid);
                List<MemberFollow> followlist = GetMemberFollowsMemberID(postid);
                RepliedtoNO(followlist,postid);
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,CreatePost", ex);
                throw;
            }
        }
        public static void RepliedtoNO(List<MemberFollow> member, Guid postid)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"UPDATE MemberFollows
                  SET Replied = 0
                  WHERE ";
            for (int i = 0; i < member.Count; i++)
            {
                if (i != member.Count - 1)
                    commandText += $"(MemberID = {member[i].MemberID} AND PostID = {postid}) OR ";
                else
                    commandText += $"(MemberID = {member[i].MemberID} AND PostID = {postid})";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,RepliedtoNO", ex);
                throw;
            }
        }
        public static void CreateInMemberFollows(Guid member, Guid postid)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    INSERT INTO MemberFollows
                    (MemberID, PostID, FollowStatus, Replied)
                    VALUES
                    (@memberID, @postID, @followStatus, @replied";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue(@"memberID", member);
                        command.Parameters.AddWithValue(@"postID", postid);
                        command.Parameters.AddWithValue(@"followStatus", 1);
                        command.Parameters.AddWithValue(@"replied", 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,CreateInMemberFollows", ex);
                throw;
            }
        }
        public static List<MemberFollow> GetMemberFollowsMemberID(Guid postid)
        {
            string connectionStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM MemberFollows
                    WHERE PostID = @postID;
                ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        List<MemberFollow> Follows = new List<MemberFollow>();
                        connection.Open();

                        command.Parameters.AddWithValue("@postID", postid);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
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
            catch (Exception ex)
            {
                Logger.WriteLog("MemberFollowManager.GetMemberFollows", ex);
                throw;
            }
        }
        public static Post GetPost(Guid postid)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Posts
                    WHERE PostID = @postID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText))
                    {
                        command.Parameters.AddWithValue("@postID", postid);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Post post = new Post()
                            {
                                PostID = (Guid)reader["PostID"],
                                MemberID = (Guid)reader["MemberID"],
                                CboardID = (int)reader["CboardID"],
                                PointID = (Guid?)reader["PointID"],
                                PostDate = (DateTime)reader["PostDate"],
                                PostView = (int)reader["PostView"],
                                Title = (string)reader["Title"],
                                PostCotent = (string)reader["PostCotent"],
                                LastEditTime = (DateTime?)reader["LastEditTime"]
                            };
                            return post;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager.GetPost", ex);
                throw;
            }
        }
        public static void UpdatePost(Guid postid, string title, string postcotent)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Posts
                    SET 
                        Title = @title,
                        PostCotent = @postcotent,
                        LastEditTime = @lastedittime,
                    WHERE PostID = @postid ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"postID", postid);
                        command.Parameters.AddWithValue(@"title", title);
                        command.Parameters.AddWithValue(@"postcotent", postcotent);
                        command.Parameters.AddWithValue(@"lastedittime", DateTime.Now.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,UpdatePost", ex);
                throw;
            }
        }
        public static void DeletePost(Guid postid)
        {
            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM Posts
                    WHERE PostID = @postid ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"postID", postid);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PostManager,DeletePost", ex);
                throw;
            }
        }
        public static bool CheckInput(string titletext, string postcotenttext)
        {
            _msgList = new List<string>();
            List<string> msgList = new List<string>();

            if (titletext.Length > 100)
                msgList.Add("標題字數請小於一百中文字。");
            if (postcotenttext.Length > 4096)
                msgList.Add("內文字數請小於兩千中文字。");
            if (KinkiNoKotoba(titletext, postcotenttext) == true)
                _msgList = msgList;
            else
                // 失敗就額外加禁字提示 還沒完成
                _msgList = msgList;
            if (msgList.Count > 0)
            {
                return false;
            }
            return true;
        }
        public static List<string> GetmsgList()
        {
            return _msgList;
        }
        public static string GetmsgText()
        {
            List<string> errlist = PostManager.GetmsgList();
            string allError = string.Join("<br/>", errlist);
            return allError;
        }
        static bool KinkiNoKotoba(string titletext, string postcotenttext)
        {
            // 比對標題及內文與禁字表是否有重疊
            return true;
        }

    }
}