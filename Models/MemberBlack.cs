using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    public class MemberBlack
    {
        public Guid MemberID { get; set; }
<<<<<<< HEAD
        public int CboardID { get; set; }
=======
        public Guid CboardID { get; set; }
>>>>>>> 9a7cb22621f16af67bc30b1fb512ae7beda3113a
        public DateTime CreateDate { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}