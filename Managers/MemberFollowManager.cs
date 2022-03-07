using MKForum.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MKForum.Managers
{
    public class MemberFollowManager
    {
        public List<MemberFollowManager> GetMemberFollows(string MemberID)
        {
            string connectionStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"
                    SELECT * FROM
                    WHERE MemberID = @MemberID;
                ";

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using(SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {


                        }
                    }    
                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}