using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKForum.Managers;
using MKForum.Models;

namespace MKForum.BackAdmin
{
    public partial class Index : System.Web.UI.Page
    {
        private AccountManager _mga = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            Member account = this._mga.GetCurrentUser();
            this.ltlAccount.Text = account.Account;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this._mga.Logout();
            Response.Redirect("~/Login.aspx");
        }
    }
}