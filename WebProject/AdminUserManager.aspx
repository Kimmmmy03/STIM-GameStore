<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AdminUserManager.aspx.cs" Inherits="WebProject.AdminUserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">Admin - Manage Users</h2>

    <div>
        <asp:Button ID="btnBackToDashboard" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="back-button" />
    </div>

    <div class="admin-flex-container">

    <div class="form-section" style="max-width: 500px; min-width: 300px;">
        <h3>Registered Users</h3>
        <asp:GridView ID="gvUsers" runat="server"
            AutoGenerateColumns="False"
            GridLines="None"
            CellPadding="10"
            CellSpacing="5"
            OnRowEditing="gvUsers_RowEditing"
            OnRowDeleting="gvUsers_RowDeleting"
            OnRowDataBound="gvUsers_RowDataBound"
            DataKeyNames="UserId">

            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
