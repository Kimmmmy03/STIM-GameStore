<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebProject.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">User Profile</h2>

    <div class="admin-flex-container">

        <div class="form-section">
            <h3>Your Details</h3>
            <asp:Label runat="server" Text="Username:" AssociatedControlID="lblUsername" CssClass="label-text" /><br />
            <asp:Label ID="lblUsername" runat="server" CssClass="profile-username" /><br /><br /><br />
            <asp:Button ID="btnAdminDashboard" runat="server" Text="Admin Dashboard" OnClick="btnAdminDashboard_Click" 
                Style="width: 100%; background-color: green; color: white; cursor: pointer; margin-top: 10px;" Visible="false" /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="Logout" OnClick="btnLogout_Click" 
                Style="width: 100%; background-color: red; color:white; cursor: pointer;" />
        </div>

        <div class="existing-section" style="max-width: 600px;">
            <h3>Order History</h3>
            <asp:GridView ID="gvOrderHistory" runat="server"
                AutoGenerateColumns="False"
                CssClass="gv-orderhistory"
                GridLines="None"
                CellPadding="10"
                CellSpacing="5">
                <Columns>
                    <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
