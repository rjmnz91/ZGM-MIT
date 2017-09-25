<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNine.Master" AutoEventWireup="true" CodeBehind="ConsultaCliente9Hermes.aspx.cs" Inherits="AVE.ConsultaCliente9Hermes" %>
<%@ Register Src="~/controles/UCCLiente9.ascx" TagName="UCCliente9H" TagPrefix="c9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<c9:UCCliente9H runat="server" ID="ctlCliente9H"  />
        
</asp:Content>
