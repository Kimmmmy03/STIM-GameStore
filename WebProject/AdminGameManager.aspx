<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AdminGameManager.aspx.cs" Inherits="WebProject.AdminGameManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">Admin - Manage Games</h2>

    <div>
        <asp:Button ID="btnBackToDashboard" runat="server" Text="Back" OnClick="btnBack_Click" ValidationGroup="None" CssClass="back-button" />
    </div>

    <div class="admin-flex-container">

        <div class="form-section">
            <h3>Add New Game</h3>

            <asp:Label Text="Game Name:" AssociatedControlID="txtGameName" runat="server" />
            <asp:TextBox ID="txtGameName" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtGameName" ErrorMessage="Game Name is required." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Label Text="Game Image Filename:" AssociatedControlID="txtGameImage" runat="server" />
            <asp:TextBox ID="txtGameImage" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtGameImage" ErrorMessage="Game Image Filename is required." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Label Text="Category ID:" AssociatedControlID="txtCategoryId" runat="server" />
            <asp:TextBox ID="txtCategoryId" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtCategoryId" ErrorMessage="Category ID is required." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Label Text="Release Date:" AssociatedControlID="txtReleaseDate" runat="server" />
            <asp:TextBox ID="txtReleaseDate" runat="server" Placeholder="yyyy-MM-dd" />
            <asp:RequiredFieldValidator ControlToValidate="txtReleaseDate" ErrorMessage="Release Date is required." ForeColor="Red" runat="server" />
            <asp:RegularExpressionValidator ControlToValidate="txtReleaseDate" ValidationExpression="\d{4}-\d{2}-\d{2}" ErrorMessage="Invalid date format. Use yyyy-MM-dd." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Label Text="Game Description:" AssociatedControlID="txtGameDesc" runat="server" />
            <asp:TextBox ID="txtGameDesc" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtGameDesc" ErrorMessage="Game Description is required." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Label Text="Game Price:" AssociatedControlID="txtGamePrice" runat="server" />
            <asp:TextBox ID="txtGamePrice" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtGamePrice" ErrorMessage="Game Price is required." ForeColor="Red" runat="server" />
            <br /><br />

            <asp:Button ID="btnAddGame" runat="server" Text="Add Game" OnClick="btnAddGame_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
        </div>

        <div class="category-reference">
            <h3>Game Categories</h3>
            <asp:GridView ID="gvCategories" runat="server"
                AutoGenerateColumns="False"
                GridLines="None"
                CellPadding="10"
                CellSpacing="5">
                <Columns>
                    <asp:BoundField DataField="CatId" HeaderText="Cat ID" />
                    <asp:BoundField DataField="CatTitle" HeaderText="Title" />
                    <asp:BoundField DataField="CatDesc" HeaderText="Description" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

    <hr />

    <div class="existing-section">
        <h3>Existing Games</h3>
        <asp:GridView ID="gvGames" runat="server"
            AutoGenerateColumns="False"
            GridLines="None"
            CellPadding="10"
            CellSpacing="5"
            OnRowDeleting="gvGames_RowDeleting"
            DataKeyNames="GameId">
            <Columns>
                <asp:BoundField DataField="GameId" HeaderText="Game ID" />
                <asp:BoundField DataField="GameName" HeaderText="Game Name" />
                <asp:BoundField DataField="GameImage" HeaderText="Image File" />
                <asp:BoundField DataField="CatId" HeaderText="Cat ID" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>


</asp:Content>
