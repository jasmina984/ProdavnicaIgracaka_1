<%@ Page Title="Proizvodi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="ProdavnicaIgracaka.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                <h2><%: Page.Title  %></h2>
            </hgroup>
           <%-- Za prikaz liste proizvod-a koristimo ASP:ListView kontrolu. Ova kontrola ce prikazati podatke na osnovu svojstva SelectMethod 
            - GetProducts metode.--%>
            <asp:ListView ID="productList" runat="server" DataKeyNames="ProductID" GroupItemCount="4" ItemType="ProdavnicaIgracaka.Models.Product" SelectMethod="GetProducts">
                <EmptyDataTemplate>
                    <%--U okviru ove liste imamo definisan EmptyDataTemplate - template koji ce biti iskoriscen ukoliko SelectMethod 
                    definisana na view-u ne vrati nikakve podatke iz baze, kao I EmptyItemTemplate.--%>
                    <table>
                        <tr>
                            <td>Podaci nisu vraceni.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <%--Svi proizvodi ce imati sliku, naziv, cenu i link(dugme) kojim se poziva akcija dodavanja prozivoda u korpu.--%>
                        <table>
                            <tr>
                                <td>
                                    <%--Ovde je primenjen UrlRouting – koriscenjem metode GetRouteUrl iskoristili smo prethodno definisanu rutu 
                                    u Global.asax file-u (gde smo deklarisali rute i njihove parametre).Tako ovde osim navedenog naziva rute,
                                    moramo navesti parametre koje ova ruta ocekuje, u ovom slucaju to je prdId.
                                    Koriscenjem Routing-a postize se koriscenje friendly url-a umesto urla sa query stringom. 
                                    Nema vise ? = & nije klasican query string -  sad je friendly--%>
                                <a href="<%#: GetRouteUrl("ProductByNameRoute", new {prdId = Item.ProductID}) %>">
                                    <image src='/Catalog/Images/Thumbs/<%#:Item.ImagePath%>'
                                     width="100" height="75" border="1" /></a> 
                                </td>
                            </tr>
                            <tr>
                                <td>                                
                                    <a href="<%#: GetRouteUrl("ProductByNameRoute", new {prdId = Item.ProductID}) %>">
                                      <%#:Item.ProductName%></a>
                                    <br />
                                    <span>
                                        <b>Cena: </b><%#:String.Format("{0:c}",Item.UnitPrice) %>
                                    </span>
                                    <br />
                                    <!--Kada se klikne na link, aplikacija prelazi na stranicu za obradu pod nazivom AddToCart.aspx. 
                                        Stranica AddToCart.aspx će pozvati metod AddToCart u klasi ShoppingCart koju sam ranije dodala.
                                        Sada  dodajem vezu "Dodaj u korpu" i na stranicu ProductList.aspx i na stranicu 
                                        ProductDetails.aspx. Ovaj link će uključiti ID proizvoda koji se preuzima iz baze podataka. -->
                                    <a href="/AddToCart.aspx?ProductID=<%#:Item.ProductID %>">
                                        <span class="ProductListItem">
                                            <b>Dodaj u korpu</b>
                                        </span>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table style="width:100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </section>
</asp:Content>

