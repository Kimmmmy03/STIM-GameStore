using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Store1 : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlCategories;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories(); // 🆕 Load dropdown from DB
                PreselectCategory(); // 🆕 Set selected value if category in URL
                LoadGames(); // ✅ Load games based on filters
            }
        }

        private void BindCategories() // 🆕 Bind categories from DB
        {
            ddlCategories.Items.Clear();
            ddlCategories.Items.Add(new ListItem("All Categories", "all")); // default option

            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT CatId, CatTitle FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ddlCategories.Items.Add(new ListItem(reader["CatTitle"].ToString(), reader["CatId"].ToString()));
                }

                reader.Close();
            }
        }

        private void PreselectCategory() // 🆕 Maintain selected category on reload
        {
            string category = Request.QueryString["category"];
            if (!string.IsNullOrEmpty(category))
            {
                ListItem item = ddlCategories.Items.FindByValue(category);
                if (item != null)
                {
                    ddlCategories.ClearSelection();
                    item.Selected = true;
                }
            }
        }

        private void LoadGames() // ✅ Loads games filtered by category/search
        {
            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT GameID, GameName, GameImage, GameDesc, GamePrice FROM Games WHERE 1=1";

                string category = Request.QueryString["category"];
                string search = Request.QueryString["search"];

                SqlCommand cmd = new SqlCommand();
                if (!string.IsNullOrEmpty(category) && category != "all")
                {
                    query += " AND CatId = @category"; // ✅ Match DB structure
                    cmd.Parameters.AddWithValue("@category", category);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND GameName LIKE '%' + @search + '%'";
                    cmd.Parameters.AddWithValue("@search", search);
                }

                cmd.CommandText = query;
                cmd.Connection = conn;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                rptGames.DataSource = reader;
                rptGames.DataBind();
                reader.Close();
            }
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = ((DropDownList)sender).SelectedValue;

            if (selectedCategory == "all")
                Response.Redirect("~/Store.aspx");
            else
                Response.Redirect("~/Store.aspx?category=" + Server.UrlEncode(selectedCategory));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            string category = ddlCategories.SelectedValue; // 🆕 Keep category during search

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (category != "all")
                    Response.Redirect("~/Store.aspx?category=" + Server.UrlEncode(category) + "&search=" + Server.UrlEncode(searchTerm));
                else
                    Response.Redirect("~/Store.aspx?search=" + Server.UrlEncode(searchTerm));
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}
