using MKForum.Managers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MKForum
{
    public partial class MemberFollowList : System.Web.UI.Page
    {
        private MemberFollowManager _mfmgr = new MemberFollowManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<MemberFollow> MemberFollows = _mfmgr.GetMemberFollows("c8142d85-68c2-4483-ab51-e7d3fc366b89");

            this.GVMemberFollows.DataSource = MemberFollows;
            this.GVMemberFollows.DataBind();
        }
    }
}