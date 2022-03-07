using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKForum.Managers;

namespace MKForum.BackAdmin
{
    public partial class Registersuccess : System.Web.UI.Page
    {
        private AccountManager _mga = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this._mga.Logout();
            Response.Redirect("~/Login.aspx");
        }
    }
}