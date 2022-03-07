using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    public class MemberFollow
    {
        public Guid MemberID { get; set; }
        public int PostID { get; set; }
        public bool FollowStatus { get; set; }
        public DateTime FollowDate { get; set; }
        public bool Replied { get; set; }
    }
}