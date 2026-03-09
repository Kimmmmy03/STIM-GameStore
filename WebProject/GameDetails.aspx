<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GameDetails.aspx.cs" Inherits="WebProject.GameDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <link href="Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
        <div class="store-navbar">
        <div class="nav-left">
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="category-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                <asp:ListItem Text="All Categories" Value="all" />
                <asp:ListItem Text="Action" Value="Action" />
                <asp:ListItem Text="Adventure" Value="Adventure" />
                <asp:ListItem Text="Puzzle" Value="Puzzle" />
            </asp:DropDownList>

            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search games..." />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="add-to-cart-btn" OnClick="btnSearch_Click" />
        </div>

        <div class="nav-right">
            <asp:ImageButton ID="btnCheckout" runat="server" ImageUrl="~/images/carts.png" PostBackUrl="~/Checkout.aspx" ToolTip="Go to cart" Width="50"/>
        </div>
    </div>

    <div class="game-details-wrapper">
        <div class="game-details-box">
            <asp:Image ID="imgGame" runat="server" CssClass="game-img-large" />
            <div class="game-info">
                <h2><asp:Label ID="lblGameName" runat="server" /></h2>
                <p><asp:Label ID="lblGameDesc" runat="server" /></p>
                <p><strong>Price: RM </strong><asp:Label ID="lblGamePrice" runat="server" /></p>

                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn add-to-cart-btn" OnClick="btnAddToCart_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
            </div>
        </div>
    </div>


</asp:Content>
