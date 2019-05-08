<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="ProdavnicaIgracaka.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ShoppingCartTitle" runat="server" class="ContentHead"><h1>Korpa</h1></div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="false" ShowFooter="True" GridLines="Vertical" CellPadding="4" ItemType="ProdavnicaIgracaka.Models.CartItem" SelectMethod="GetShoppingCartItems" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Šifra artikla" SortExpression="ProductID" />
                <asp:BoundField DataField="Product.ProductName" HeaderText="Ime" />
                <asp:BoundField DataField="Product.UnitPrice" HeaderText="Pojedinačna cena" DataFormatString="{0:c}" />
                <asp:TemplateField HeaderText="Količina">
                    <ItemTemplate>
                        <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ukupno">
                    <ItemTemplate>
                        <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) * Convert.ToDouble(Item.Product.UnitPrice))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ukloni artikal">
                    <ItemTemplate>
                        <asp:CheckBox ID="Remove" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelTotalText" runat="server" Text="Ukupan iznos:"></asp:Label>
            <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
        </strong>
        </div>
        <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="UpdateBtn" runat="server" Text="Ažuriraj" OnClick="UpdateBtn_Click" />
            </td>
            <td>
                <!--Checkout Placeholder -->
                <asp:ImageButton ID="CheckoutImageBtn" runat="server" 
                      ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" 
                      Width="145" AlternateText="Check out with PayPal" 
                      OnClick="CheckoutBtn_Click" 
                      BackColor="Transparent" BorderWidth="0" />
            </td>
        </tr>
    </table>
</asp:Content>
