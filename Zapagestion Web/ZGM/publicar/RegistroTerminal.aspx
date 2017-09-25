<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistroTerminal.aspx.cs" Inherits="AVE.RegistroTerminal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
        <br />
        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, RegistroTerminalMensaje%>"></asp:Label>
        <br />
        <asp:TextBox ID="txtIdTerminal" runat="server" MaxLength="50" CssClass="boton"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdTerminal" ErrorMessage="*" CssClass="mensajeRequerido"></asp:RequiredFieldValidator>
        <asp:Button ID="Button1" runat="server" Text=">" CssClass="boton" 
            onclick="Button1_Click" />
    </asp:Panel>
</asp:Content>
