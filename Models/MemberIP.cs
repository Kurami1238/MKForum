using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    public class MemberIP
    {
        public Guid MemberID { get; set; }
        public string IPLocation { get; set; }
        public int MissTotal { get; set; }
        public DateTime LastTime { get; set; }
    }
}