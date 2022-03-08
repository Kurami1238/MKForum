using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKForum.Managers;
using MKForum.Models;

namespace MKForum
{
    public partial class Login : System.Web.UI.Page
    {
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this._mgr.IsLogin())
            {
                this.plcInfo.Visible = true;
                this.plcLogin.Visible = false;
                Member account = this._mgr.GetCurrentUser();
                this.ltlAccount.Text = account.Account;
            }
            else
            {
                this.plcInfo.Visible = false;
                this.plcLogin.Visible = true;
            }
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            if (this._mgr.TryLogin(account, password))
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                this.ltlMessage.Text = "登入失敗，請檢查帳號密碼。";
            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

    }
}