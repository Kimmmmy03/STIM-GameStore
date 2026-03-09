<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="WebProject.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="text-align:center;">Admin Dashboard</h2>

    <div style="display:flex; justify-content:center; align-items:center; height:70vh;">
        <div class="admin-dashboard-buttons" style="text-align:center;">

            <asp:Button ID="btnManageGames" runat="server" Text="Add/Remove Games" 
                OnClick="btnManageGames_Click" 
                Style="width: 250px; height: 70px; font-size: 20px; background-color: #66c0f4; color: #fff; border: none; border-radius: 6px; cursor: pointer;" /><br /><br />

            <asp:Button ID="btnManageUsers" runat="server" Text="Edit/Remove Users" 
                OnClick="btnManageUsers_Click" 
                Style="width: 250px; height: 70px; font-size: 20px; background-color: #66c0f4; color: #fff; border: none; border-radius: 6px; cursor: pointer;" />
        </div>
    </div>
</asp:Content>
