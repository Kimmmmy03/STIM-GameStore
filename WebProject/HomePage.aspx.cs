using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int userId = GetCurrentUserId();
                LoadOwnedGames(userId);
                LoadRecentlyPlayedGames(userId);
            }

        }

        private int GetCurrentUserId()
        {
            object userId = Session["UserId"];
            return userId != null ? Convert.ToInt32(userId) : 0;
        }

        private void LoadOwnedGames(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

            string query = @"
                SELECT G.GameName, G.GameImage
                FROM UserGames UG
                INNER JOIN Games G ON UG.GameId = G.GameId
                WHERE UG.UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptGames.DataSource = reader;
                    rptGames.DataBind();
                }
            }
        }


        private void LoadRecentlyPlayedGames(int userId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString))
            {
                string query = @"
                    SELECT G.GameName, G.GameImage, UG.LastPlayed
                    FROM UserGames UG
                    INNER JOIN Games G ON UG.GameId = G.GameId
                    WHERE UG.UserId = @UserId
                    ORDER BY UG.LastPlayed DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptRecentGames.DataSource = reader;
                    rptRecentGames.DataBind();
                    reader.Close();
                }
            }
        }
    }
}