<%@ Page Title="<%$ Resources:Resource, OrdenInventario%>" Language="C#" MasterPageFile="~/MasterPage.Master" Theme="Tema" AutoEventWireup="true" CodeBehind="OrdenInventarioDetalle.aspx.cs" Inherits="AVE.OrdenInventarioDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentInventarios" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DetailsView ID="dvOI" Runat="server" DataSourceID="SDSOI" AutoGenerateRows="False" GridLines="None" CellPadding="1">
        <FieldHeaderStyle CssClass="negrita" />
        <Fields>
          <asp:BoundField HeaderText="<%$ Resources:Resource, Nombre%>" DataField="Nombre" />
          <asp:BoundField HeaderText="<%$ Resources:Resource, Inicio%>" DataField="FechaInventarioIni" HtmlEncode="false" DataFormatString="{0:d}" />
          <asp:BoundField HeaderText="<%$ Resources:Resource, Fin%>" DataField="FechaInventarioFin" HtmlEncode="false" DataFormatString="{0:d}" />
          <asp:BoundField HeaderText="<%$ Resources:Resource, TipoInventario%>" DataField="TipoInventario"  />
          <asp:BoundField HeaderText="<%$ Resources:Resource, TipoVista%>" DataField="TipoVista" />
          <asp:BoundField HeaderText="<%$ Resources:Resource, Observaciones%>" DataField="Observaciones" />
          <asp:CheckBoxField HeaderText="<%$ Resources:Resource, Realizado%>" DataField="Realizada" />
          <asp:CheckBoxField HeaderText="<%$ Resources:Resource, Enviado%>" DataField="Enviada"  />
          <asp:BoundField HeaderText="<%$ Resources:Resource, ObservacionesTienda%>" DataField="ObservacionesTienda"/>
        </Fields>
    </asp:DetailsView>

    <asp:Button ID="btnRealizar" runat="server" Text="<%$ Resources:Resource, Realizar%>" Enabled="false" onclick="btnRealizar_Click" CssClass="boton4"/>
    
    <asp:SqlDataSource ID="sdsOI" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" SelectCommand="AVE_OrdenInventarioObtener" SelectCommandType="StoredProcedure" EnableCaching="false">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdOrdenInventario" Type="Int32"/>
            <asp:Parameter Name="IdTienda" Type="String"/>
        </SelectParameters>
    </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="sdsInventario" runat="server"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        InsertCommand="dbo.AVE_InventariosPendientesCrear" InsertCommandType="StoredProcedure" >
        <InsertParameters>
            <asp:Parameter Name="IdTipoInventario" Type="Int32" />
            <asp:Parameter Name="IdTipoVista" Type="Int32" />
            <asp:Parameter Name="Empresa" Type="String" DefaultValue="" />
            <asp:Parameter Name="IdTienda" Type ="String" />
            <asp:Parameter Name="IdEmpleado" Type ="int32" />
            <asp:Parameter Name="IdTerminal" Type="String"/>
            <asp:Parameter Name="IdOrdenInventario" Type="Int32"/>
            <asp:Parameter Name="Usuario" Type ="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>