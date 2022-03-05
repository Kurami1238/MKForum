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
        public static void CreatePost(Members member, Cboard cboard, string titletext, string postcotenttext)
        {

            string connectionString = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    INSERT INTO Posts
                    (MemberID, CboardID, PostView, Title, PostCotent)
                    VALUES
                    (@MemberID, @CboardID, @PostView, @Title, @PostCotent)
                    ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // (@MemberID, @CboardID, @PostView, @Title, @PostCotent)

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue(@"MemberID", member.MemberID);
                        command.Parameters.AddWithValue(@"CboardID", cboard.CboardID);
                        command.Parameters.AddWithValue(@"PostView", 0);
                        command.Parameters.AddWithValue(@"Title", titletext);
                        command.Parameters.AddWithValue(@"PostCotent", postcotenttext);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MemberManager.GetMembers", ex);
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
        static bool KinkiNoKotoba(string titletext, string postcotenttext)
        {
            // 比對標題及內文與禁字表是否有重疊
            
            return true;
        }

    }
}