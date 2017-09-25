<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FinalizaCompra.aspx.cs" Inherits="AVE.FinalizaCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fallback
        {
            background-image: url("img/noImagen.jpg");
        }
        .borderBottom
        {
            border-bottom: solid 1px black;
        }
        .style2
        {
            width: 262px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h3 style="text-align:center;">Compra Finalizada!</h3>
        <div class="container">
            <br />
            <div class="alert alert-success" role="alert" style="text-align:center;">
             <div style="font-weight:bold; font-size: 11px">
                <asp:Label runat="server" ID="Label8" Text="Ticket No: "></asp:Label>
                <asp:Label runat="server" ID="NumTicket" Text=''></asp:Label>
            </div>
             <br />
             <div style="font-weight:bold; font-size: 11px">
                <asp:Label runat="server" ID="Label2" Text="Cliente: "></asp:Label>
                <asp:Label runat="server" ID="NomCliente" Text=''></asp:Label>
            </div>
             <br />
             <div style="font-weight:bold; font-size: 12px">
                <asp:Label runat="server" ID="Label7" Text="Entrega: "></asp:Label>
                <asp:Label runat="server" ID="Entrega" Text=''></asp:Label>
            </div>
             <br />
             <div style="font-weight: bold; font-size: 11px;">
                <h5 style="font-weight:bold;"><asp:Label runat="server" ID="Label1" Text='No olvide entregar los documentos al cliente'></asp:Label></h5>
                 </div>
             <br />
             </div>
            <div style="text-align:center;" runat="server">
                <asp:button runat="server" ID="cmdInicio" Text="Volver" PostBackUrl="~/Inicio.aspx"  CssClass="btn btn-info" Width="15%"/>
            </div>
        </div>
</asp:Content>