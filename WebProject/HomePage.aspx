<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebProject.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />

    <div class="homepage-container">

        <div class="section">
            <h2 class="section-title">Your Owned Games</h2>
            <div class="game-shelf">
                <asp:Repeater ID="rptGames" runat="server">
                    <ItemTemplate>
                        <div class="game-tile">
                            <asp:Image ID="imgGame" runat="server"
                                ImageUrl='<%# ResolveUrl("~/images/" + Eval("GameImage")) %>'
                                AlternateText='<%# Eval("GameName") %>'
                                CssClass="game-img-large" />
                            <div class="game-caption">
                                <%# Eval("GameName") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="section">
            <h2 class="section-title">Recently Played Games</h2>
            <div class="game-shelf">
                <asp:Repeater ID="rptRecentGames" runat="server">
                    <ItemTemplate>
                        <div class="game-tile">
                            <asp:Image ID="imgRecentGame" runat="server"
                                ImageUrl='<%# ResolveUrl("~/images/" + Eval("GameImage")) %>'
                                AlternateText='<%# Eval("GameName") %>'
                                CssClass="game-img-large" />
                            <div class="game-caption">
                                <%# Eval("GameName") %><br />
                                <span class="game-subtext">Last Played: <%# Eval("LastPlayed", "{0:dd MMM yyyy}") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


    </div>

</asp:Content>
