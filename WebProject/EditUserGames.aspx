<%@ Page Title="Edit User Games" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EditUserGames.aspx.cs" Inherits="WebProject.EditUserGames" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">Edit User's Owned Games</h2>

    <div>
        <asp:Button ID="btnBackToUserManager" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="back-button" />
    </div>

    <div class="admin-flex-container">

        <div class="form-section" style="max-width: 300px; min-width: 200px;">
            <asp:GridView ID="gvUserGames" runat="server"
                AutoGenerateColumns="False"
                GridLines="None"
                CellPadding="10"
                CellSpacing="5"
                OnRowDeleting="gvUserGames_RowDeleting"
                DataKeyNames="UserGameId">

                <Columns>
                    <asp:BoundField DataField="GameName" HeaderText="Game Name" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
