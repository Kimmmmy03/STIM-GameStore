using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace WebProject
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                LoadCart();
            }
        }

        private void LoadCart()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            decimal total = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT g.GameName, g.GamePrice, g.GameImage
                    FROM Cart c
                    JOIN Games g ON c.GameID = g.GameID
                    WHERE c.UserId = @UserId";

                SqlCommand selectCartItems = new SqlCommand(query, conn);
                selectCartItems.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter adapter = new SqlDataAdapter(selectCartItems);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                rptCart.DataSource = dt;
                rptCart.DataBind();

                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["GamePrice"]);
                }

                lblTotal.Text = total.ToString("0.00");
                ViewState["TotalAmount"] = total;
            }
        }

        private decimal CalculateTotalAmount()
        {
            if (ViewState["TotalAmount"] != null)
                return Convert.ToDecimal(ViewState["TotalAmount"]);
            else
                return 0;
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int UserId = Convert.ToInt32(Session["UserId"]);
            decimal totalAmount = CalculateTotalAmount();

            string connStr = ConfigurationManager.ConnectionStrings["GameStoreDbConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insert into Orders
                    string insertOrder = "INSERT INTO Orders (UserId, OrderDate, TotalAmount) OUTPUT INSERTED.OrderId VALUES (@UserId, @OrderDate, @TotalAmount)";
                    SqlCommand insertOrderCmd = new SqlCommand(insertOrder, conn, transaction);
                    insertOrderCmd.Parameters.AddWithValue("@UserId", UserId);
                    insertOrderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    insertOrderCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                    int orderId = (int)insertOrderCmd.ExecuteScalar();

                    // 2. Get Cart Items into a list
                    string selectCart = "SELECT GameID FROM Cart WHERE UserId = @UserId";
                    SqlCommand selectCartCmd = new SqlCommand(selectCart, conn, transaction);
                    selectCartCmd.Parameters.AddWithValue("@UserId", UserId);

                    List<int> gameIds = new List<int>();
                    using (SqlDataReader reader = selectCartCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameIds.Add(Convert.ToInt32(reader["GameID"]));
                        }
                    }

                    // 3. Insert into OrderDetails for each gameId
                    foreach (int gameId in gameIds)
                    {
                        // Insert into OrderDetails
                        string insertDetail = "INSERT INTO OrderDetails (OrderId, GameId, Quantity) VALUES (@OrderId, @GameId, 1)";
                        SqlCommand insertDetailCmd = new SqlCommand(insertDetail, conn, transaction);
                        insertDetailCmd.Parameters.AddWithValue("@OrderId", orderId);
                        insertDetailCmd.Parameters.AddWithValue("@GameId", gameId);
                        insertDetailCmd.ExecuteNonQuery();

                        // Insert into UserGames if not exists, with LastPlayed = NULL
                        string insertUserGame = @"
                        IF NOT EXISTS (SELECT 1 FROM UserGames WHERE UserId = @UserId AND GameId = @GameId)
                        BEGIN
                            INSERT INTO UserGames (UserId, GameId, LastPlayed)
                            VALUES (@UserId, @GameId, @LastPlayed)
                        END";

                        SqlCommand insertUserGameCmd = new SqlCommand(insertUserGame, conn, transaction);
                        insertUserGameCmd.Parameters.AddWithValue("@UserId", UserId);
                        insertUserGameCmd.Parameters.AddWithValue("@GameId", gameId);
                        insertUserGameCmd.Parameters.AddWithValue("@LastPlayed", DBNull.Value);
                        insertUserGameCmd.ExecuteNonQuery();
                    }


                    // 4. Clear Cart
                    string deleteCart = "DELETE FROM Cart WHERE UserId = @UserId";
                    SqlCommand deleteCartCmd = new SqlCommand(deleteCart, conn, transaction);
                    deleteCartCmd.Parameters.AddWithValue("@UserId", UserId);
                    deleteCartCmd.ExecuteNonQuery();

                    transaction.Commit();

                    // Show popup confirmation
                    pnlConfirmation.Style["display"] = "flex";

                    // Disable checkout UI
                    rptCart.DataSource = null;
                    rptCart.DataBind();
                    lblTotal.Text = "0.00";
                    btnCheckout.Enabled = false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblTotal.Text = "Error processing order: " + ex.Message;
                }
            }
        }

        protected void btnClosePopup_Click(object sender, EventArgs e)
        {
            pnlConfirmation.Style["display"] = "none";
        }
    }
}