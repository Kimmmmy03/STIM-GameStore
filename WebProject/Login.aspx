<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebProject.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="text-align:center;">Login</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />

    <div style="display:flex; justify-content:center; align-items:center; height:70vh;">
        <div class="form-section">
            <asp:Label Text="Username:" AssociatedControlID="txtUsername" runat="server" />
            <asp:TextBox ID="txtUsername" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtUsername" ErrorMessage="Username is required." ForeColor="Red" runat="server" /><br /><br />

            <asp:Label Text="Password:" AssociatedControlID="txtPassword" runat="server" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="Password is required." ForeColor="Red" runat="server" /><br /><br />

            <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /><br /><br />
            <asp:HyperLink NavigateUrl="Register.aspx" runat="server">I don't have an account</asp:HyperLink>
        </div>
    </div>
</asp:Content>
