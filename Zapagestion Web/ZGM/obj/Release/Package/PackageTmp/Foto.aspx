<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  Theme="Tema" Inherits="Foto" Codebehind="Foto.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <a href="javascript:history.back();">
            <asp:Image ID="FotoArticulo" runat="server" style=" width:240px;" />
        </a>
        <asp:SqlDataSource ID="AVE_ArticuloFotoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
