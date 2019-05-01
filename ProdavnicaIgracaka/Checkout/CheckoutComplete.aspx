<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutComplete.aspx.cs" Inherits="ProdavnicaIgracaka.Checkout.CheckoutComplete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Kreirana porudzbina</h1>
    <p></p>
    <h3>Šifra transakcije:</h3> <asp:Label ID="TransactionId" runat="server"></asp:Label>
    <p></p>
    <h3>Hvala!</h3>
    <p></p>
    <hr />
    <asp:Button ID="Continue" runat="server" Text="Nastavite kupovinu" OnClick="Continue_Click" />
</asp:Content>
