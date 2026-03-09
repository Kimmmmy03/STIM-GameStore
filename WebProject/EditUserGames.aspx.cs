using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class EditUserGames : System.Web.UI.Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the UserId from the query string (passed from AdminUserManager.aspx)
                int userId = Convert.ToInt32(Request.QueryString["UserId"]);

                // Load the games the user owns
                LoadUserGames(userId);
            }
        }

        private void LoadUserGames(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ug.UserGameId, g.GameName, ug.LastPlayed FROM UserGames ug " +
                               "JOIN Games g ON ug.GameId = g.GameId WHERE ug.UserId = @UserId";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@UserId", userId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUserGames.DataSource = dt;
                gvUserGames.DataBind();
            }
        }

        protected void gvUserGames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userGameId = Convert.ToInt32(gvUserGames.DataKeys[e.RowIndex].Value);

            // Delete the game from the UserGames table (removing it from the user's library)
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM UserGames WHERE UserGameId = @UserGameId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserGameId", userGameId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            int userId = Convert.ToInt32(Request.QueryString["UserId"]);
            LoadUserGames(userId);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminUserManager.aspx");
        }
    }
}