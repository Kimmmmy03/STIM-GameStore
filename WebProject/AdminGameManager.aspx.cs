using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class AdminGameManager : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGames();
                LoadCategories();
            }
        }

        private void LoadGames()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT GameId, GameName, GameImage, CatId, ReleaseDate FROM Games";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvGames.DataSource = dt;
                gvGames.DataBind();
            }
        }
        
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Categories";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        protected void btnAddGame_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            INSERT INTO Games (GameName, GameImage, CatId, ReleaseDate, GameDesc, GamePrice)
            VALUES (@GameName, @GameImage, @CatId, @ReleaseDate, @GameDesc, @GamePrice)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GameName", txtGameName.Text);
                cmd.Parameters.AddWithValue("@GameImage", txtGameImage.Text);
                cmd.Parameters.AddWithValue("@CatId", txtCategoryId.Text);
                cmd.Parameters.AddWithValue("@ReleaseDate", txtReleaseDate.Text);
                cmd.Parameters.AddWithValue("@GameDesc", txtGameDesc.Text);
                cmd.Parameters.AddWithValue("@GamePrice", txtGamePrice.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "Game added successfully.";
                LoadGames();
            }
        }


        protected void gvGames_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int gameId = Convert.ToInt32(gvGames.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Games WHERE GameId = @GameId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GameId", gameId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadGames();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Page.Validate("None");

            Response.Redirect("AdminDashboard.aspx");
        }
    }
}