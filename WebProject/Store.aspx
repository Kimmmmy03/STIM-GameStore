<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="WebProject.Store1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="store-navbar">
        <div class="nav-left">
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="category-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                <asp:ListItem Text="All Categories" Value="all" />
                <asp:ListItem Text="Action" Value="3" />
                <asp:ListItem Text="Adventure" Value="4" />
                <asp:ListItem Text="Puzzle" Value="6" />
                <asp:ListItem Text="Shooter" Value="7" />
                <asp:ListItem Text="RPG" Value="2" />
                <asp:ListItem Text="Sports" Value="1" />
            </asp:DropDownList>

            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search games..." />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="add-to-cart-btn" OnClick="btnSearch_Click" />
        </div>

        <div class="nav-right">
            <asp:ImageButton ID="btnCheckout" runat="server" ImageUrl="~/images/carts.png" PostBackUrl="~/Checkout.aspx" ToolTip="Go to cart" Width="50"/>

        </div>
    </div>


    <div class="store-container">
        <div class="section">
            <h2 class="section-title">Games You Might Enjoy</h2>
            <div class="game-list-vertical">
                <asp:Repeater ID="rptGames" runat="server">
                    <ItemTemplate>
                        <div class="game-tile">
                            <asp:Image ID="imgGame" runat="server"
                                ImageUrl='<%# ResolveUrl("~/images/" + Eval("GameImage")) %>'
                                CssClass="game-img-large" />

                            <div class="game-caption">
                                <a href='<%# "GameDetails.aspx?id=" + Eval("GameId") %>'>
                                    <%# Eval("GameName") %>
                                </a>
                                <div class="game-price">
                                    RM <%# String.Format("{0:0.00}", Eval("GamePrice")) %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
