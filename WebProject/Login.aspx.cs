using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebProject
{
    public partial class Login : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtUsername.Text == "admin" && txtPassword.Text == "adminadmin")
                {
                    Session["Username"] = txtUsername.Text;
                    Response.Redirect("~/AdminDashboard.aspx");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "SELECT UserId FROM Users WHERE Username = @Username AND PasswordHash = @Password";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            Session["Username"] = txtUsername.Text;
                            Session["UserId"] = result.ToString();
                            Response.Redirect("HomePage.aspx");
                        }
                        else
                        {
                            lblLoginError.Text = "Invalid username or password.";
                        }

                    }
                }
            }
        }
    }
}
