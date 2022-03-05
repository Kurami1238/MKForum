﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    public class Post
    {
        public Guid PostID { get; set; }
        public int CboardID { get; set; }
        public Guid MemberID { get; set; }
        public Guid? PointID { get; set; }
        public DateTime PostDate { get; set; }
        public int PostView { get; set; }
        public string Title { get; set; }
        public string PostCotent { get; set; }

    }
}