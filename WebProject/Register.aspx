<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebProject.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="text-align:center;">Create Your Account</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />

    <div style="display:flex; justify-content:center; align-items:center; height:70vh;">
        <div class="form-section">
            <asp:Label Text="Email Address:" AssociatedControlID="txtEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Email is required." ForeColor="Red" runat="server" />
            <asp:RegularExpressionValidator ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ForeColor="Red" ValidationExpression="\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b" runat="server" /><br /><br />

            <asp:Label Text="Username:" AssociatedControlID="txtUsername" runat="server" />
            <asp:TextBox ID="txtUsername" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtUsername" ErrorMessage="Username is required." ForeColor="Red" runat="server" />
            <asp:CustomValidator ID="cvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username already exists." OnServerValidate="cvUsername_ServerValidate" ForeColor="Red" ValidationGroup="RegisterValidation" /><br /><br />

            <asp:Label Text="Password:" AssociatedControlID="txtPassword" runat="server" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="Password is required." ForeColor="Red" runat="server" />
            <asp:RegularExpressionValidator ControlToValidate="txtPassword" ValidationExpression=".{6,}" ErrorMessage="Password must be at least 6 characters." ForeColor="Red" runat="server" /><br /><br />

            <asp:Label Text="Re-enter Password:" AssociatedControlID="txtConfirm" runat="server" />
            <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator ControlToValidate="txtConfirm" ErrorMessage="Please confirm your password." ForeColor="Red" runat="server" />
            <asp:CompareValidator ControlToCompare="txtPassword" ControlToValidate="txtConfirm" ErrorMessage="Passwords do not match." ForeColor="Red" runat="server" /><br /><br />

            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" ValidationGroup="RegisterValidation" /> <br /><br />
            <asp:HyperLink NavigateUrl="Login.aspx" runat="server">I already have an account</asp:HyperLink>
        </div>
    </div>
</asp:Content>
