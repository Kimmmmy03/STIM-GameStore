using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace WebProject
{
    public partial class GameDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string gameId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(gameId))
                {
                    LoadGameDetails(gameId);
                }
                else
                {
                    lblMessage.Text = "Game not found.";
                }
            }
        }

        private void LoadGameDetails(string gameId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT GameName, GameDesc, GameImage, GamePrice FROM Games WHERE GameID = @GameID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GameID", gameId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblGameName.Text = reader["GameName"].ToString();
                    lblGameDesc.Text = reader["GameDesc"].ToString();
                    lblGamePrice.Text = Convert.ToDecimal(reader["GamePrice"]).ToString("0.00");
                    imgGame.ImageUrl = "~/images/" + reader["GameImage"].ToString();
                }
                reader.Close();
            }

            ViewState["GameID"] = gameId;
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            int gameId = Convert.ToInt32(ViewState["GameID"]);

            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string insertQuery = @"
                    INSERT INTO Cart (UserID, GameID)
                    SELECT @UserID, @GameID
                    WHERE NOT EXISTS (
                        SELECT 1 FROM Cart WHERE UserID = @UserID AND GameID = @GameID
                    )";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@GameID", gameId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Game added to cart!";
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = ddlCategories.SelectedValue;
            if (selectedCategory == "all")
                Response.Redirect("~/Store.aspx");
            else
                Response.Redirect("~/Store.aspx?category=" + Server.UrlEncode(selectedCategory));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
                Response.Redirect("~/Store.aspx?search=" + Server.UrlEncode(searchTerm));
        }
        protected void btnCheckout_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}
