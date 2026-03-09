<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WebProject.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <h2>Your Cart</h2>
    <div class="checkout-container">
        <div class="cart-items">
            <asp:Repeater ID="rptCart" runat="server">
                <ItemTemplate>
                    <div class="cart-item">
                        <asp:Image ID="imgGame" runat="server" CssClass="game-img" 
                            ImageUrl='<%# ResolveUrl("~/images/" + Eval("GameImage")) %>' />

                        <div class="info">
                            <div><strong><%# Eval("GameName") %></strong></div>
                            <div>RM <%# String.Format("{0:0.00}", Eval("GamePrice")) %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="cart-summary">
            <div class="total-price">
                Total: RM <asp:Label ID="lblTotal" runat="server" />
            </div>
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn-checkout" OnClick="btnCheckout_Click" />
        </div>

    <asp:Panel ID="pnlConfirmation" runat="server" CssClass="popup-panel" Style="display:none;">
    <div class="popup-content">
        <h3>Order Confirmed!</h3>
        <p>Thank you for your purchase.</p>
        <asp:Button ID="btnClosePopup" runat="server" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn-close" />
    </div>
</asp:Panel>
    </div>
</asp:Content>
