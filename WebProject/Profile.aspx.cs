using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebProject
{
    public partial class Profile : Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();
                    lblUsername.Text = username;

                    LoadOrderHistory(username);

                    if (username == "admin")
                    {
                        btnAdminDashboard.Visible = true;
                    }
                    else
                    {
                        btnAdminDashboard.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void LoadOrderHistory(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Step 1: Get UserId for this username
                string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";
                SqlCommand cmdUser = new SqlCommand(getUserIdQuery, conn);
                cmdUser.Parameters.AddWithValue("@Username", username);

                object userIdObj = cmdUser.ExecuteScalar();

                if (userIdObj == null)
                {
                    // No user found - clear GridView and exit
                    gvOrderHistory.DataSource = null;
                    gvOrderHistory.DataBind();
                    return;
                }

                int userId = Convert.ToInt32(userIdObj);

                // Step 2: Get orders for that user
                string orderQuery = @"
                    SELECT OrderId, OrderDate, TotalAmount
                    FROM Orders
                    WHERE UserId = @UserId
                    ORDER BY OrderDate DESC";

                SqlCommand cmdOrders = new SqlCommand(orderQuery, conn);
                cmdOrders.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmdOrders);
                DataTable dtOrders = new DataTable();
                da.Fill(dtOrders);

                gvOrderHistory.DataSource = dtOrders;
                gvOrderHistory.DataBind();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx");
        }

        protected void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminDashboard.aspx");
        }
    }
}
