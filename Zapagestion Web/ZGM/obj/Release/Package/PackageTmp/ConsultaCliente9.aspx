<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNine.Master" AutoEventWireup="true" CodeBehind="ConsultaCliente9.aspx.cs" Inherits="AVE.ConsultaCliente9" %>
<%@ Register Src="~/controles/UCCLiente9.ascx" TagName="UCCliente9" TagPrefix="c9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<c9:UCCliente9 runat="server" ID="ctlCliente9"  />
        
</asp:Content>
