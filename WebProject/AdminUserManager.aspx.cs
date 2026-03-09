using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class AdminUserManager : System.Web.UI.Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, Username, Email FROM Users WHERE Username != 'admin'";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                gvUsers.DataSource = reader;
                gvUsers.DataBind();
            }
        }
        protected void gvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                string username = e.Row.Cells[1].Text;

                if (username == "admin")
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.NewEditIndex].Value);

            Response.Redirect("EditUserGames.aspx?UserId=" + userId);
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string deleteOrderDetailsQuery = "DELETE FROM OrderDetails WHERE OrderId IN (SELECT OrderId FROM Orders WHERE UserId = @UserId)";
                SqlCommand cmdDeleteOrderDetails = new SqlCommand(deleteOrderDetailsQuery, con);
                cmdDeleteOrderDetails.Parameters.AddWithValue("@UserId", userId);
                cmdDeleteOrderDetails.ExecuteNonQuery();

                string deleteOrdersQuery = "DELETE FROM Orders WHERE UserId = @UserId";
                SqlCommand cmdDeleteOrders = new SqlCommand(deleteOrdersQuery, con);
                cmdDeleteOrders.Parameters.AddWithValue("@UserId", userId);
                cmdDeleteOrders.ExecuteNonQuery();

                string deleteUserGamesQuery = "DELETE FROM UserGames WHERE UserId = @UserId";
                SqlCommand cmdDeleteUserGames = new SqlCommand(deleteUserGamesQuery, con);
                cmdDeleteUserGames.Parameters.AddWithValue("@UserId", userId);
                cmdDeleteUserGames.ExecuteNonQuery();

                string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";
                SqlCommand cmdDeleteUser = new SqlCommand(deleteUserQuery, con);
                cmdDeleteUser.Parameters.AddWithValue("@UserId", userId);
                cmdDeleteUser.ExecuteNonQuery();

                con.Close();
            }

            LoadUsers();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx");
        }
    }
}