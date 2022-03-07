using MKForum.Managers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MKForum.BackAdmin
{
    public partial class MemberEditor : System.Web.UI.Page
    {
        private MemberManager _mmgr = new MemberManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            Member memberInfo = _mmgr.GetMember("c8142d85-68c2-4483-ab51-e7d3fc366b89");

            if (memberInfo == null)
            {
                this.txtMember_name.Visible = false;
                this.txtMember_Sex.Visible = false;
                this.txtMember_Birthday.Visible = false;
                this.lblMember_Status.Visible = false;
                this.txtMember_Account.Visible = false;
                this.txtMember_Mail.Visible = false;
                this.phEmpty.Visible = true;
            }
            else
            {
                this.txtMember_name.Text = memberInfo.NickName;

                if (memberInfo.Sex == 1)
                    this.txtMember_Sex.Text = "男";
                else if (memberInfo.Sex == 2)
                    this.txtMember_Sex.Text = "男";

                if (memberInfo.MemberStatus == 1)
                    this.lblMember_Status.Text = "一般會員";
                else if (memberInfo.MemberStatus == 2)
                    this.lblMember_Status.Text = "版主";
                else if (memberInfo.MemberStatus == 3)
                    this.lblMember_Status.Text = "管理員";


                this.txtMember_Birthday.Text = memberInfo.Birthday.ToString();

                this.txtMember_Account.Text = memberInfo.Account;

                this.txtMember_Mail.Text = memberInfo.Email;

                this.phEmpty.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}