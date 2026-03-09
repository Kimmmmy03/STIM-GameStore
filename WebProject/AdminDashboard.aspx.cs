using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
	public partial class AdminDashboard : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnManageGames_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminGameManager.aspx");
        }

        protected void btnManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminUserManager.aspx");
        }
    }
}