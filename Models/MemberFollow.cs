using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    public class MemberFollow
    {
        public Guid MemberID { get; set; }
<<<<<<< HEAD
        public int PostID { get; set; }
=======
        public Guid PostID { get; set; }
>>>>>>> 9a7cb22621f16af67bc30b1fb512ae7beda3113a
        public bool FollowStatus { get; set; }
        public DateTime ReadedDate { get; set; }
        public bool Replied { get; set; }
    }
}