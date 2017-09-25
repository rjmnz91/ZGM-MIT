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
      <h3>Compra Finalizada!</h3>
     <div runat="server" id="Resumen" style="margin-left: 1%; width: 500px; height: 55px; border: solid 1px black;
            float: left">
           
            <div style="float: left; width: 150px; text-align: right; font-weight: bold; font-size: 12px">
                <asp:Label runat="server" ID="Label8" Text="Ticket No"></asp:Label>:
            </div>
            <div style="float: left; width: 300px; text-align: left">
                <asp:Label runat="server" ID="NumTicket" Text=''></asp:Label>
            </div>
            <br />
             <div style="float: left; width: 150px; text-align: right; font-weight: bold; font-size: 12px">
                <asp:Label runat="server" ID="Label2" Text="Cliente"></asp:Label>:
            </div>
            <div style="float: left; width: 300px; text-align: left">
                <asp:Label runat="server" ID="NomCliente" Text=''></asp:Label>
            </div>
            <br />
            <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold;">
                <asp:Label runat="server" ID="Label7" Text="Entrega"></asp:Label>:
            </div>
            <div style="float: left; width: 300px; text-align: left; font-weight: bold; font-size: 12px;">
                <asp:Label runat="server" ID="Entrega" Text=''></asp:Label>
            </div>
        </div>
        <br />
        <div style="clear:left; width: 500px; text-align: center; font-weight: bold; font-size: 12px;">
                <h3><asp:Label runat="server" ID="Label1" Text='No olvide entregar los documentos al cliente'></asp:Label></h3>
        </div>
        <br />
        <br />
        <asp:Button runat="server" ID="cmdImprimirTicket" Text="Imprimir Ticket" Visible="false" 
            onclick="cmdImprimirTicket_Click" />
        <asp:button runat="server" ID="cmdInicio" Text="Volver" PostBackUrl="~/Inicio.aspx" />
    </asp:Content>