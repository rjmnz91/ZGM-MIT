<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SolicitudesAlmacenDetalle.aspx.cs" Inherits="AVE.SolicitudesAlmacenDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="barraNavegacion">
        <asp:Button ID="btnFoto" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, Foto%>" onclick="btnFoto_Click" />        
    </div>
    <table width="100%">
        <tr>
            <td><asp:Label ID="Label11" runat="server" Text="<%$ Resources:Resource, Pedido%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblIdPedido" runat="server"></asp:Label>
            &nbsp;(<asp:Label ID="lblFechaPedido" runat="server"></asp:Label>)</td>
        </tr>
        <tr>
            <td><asp:Label ID="Label12" runat="server" Text="<%$ Resources:Resource, Tienda%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblTienda" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Proveedor%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblIdProveedor" runat="server"/>
                &nbsp;<asp:Label ID="lblProveedor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label13" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblIdArticulo" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td><asp:Label ID="Label14" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblReferencia" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td ><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, Talla%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblTalla" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label15" runat="server" Text="<%$ Resources:Resource, Modelo%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblModelo" runat="server"></asp:Label></td>
        </tr>       
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Descripcion%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblDescripcion" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Color%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblColor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td ><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, Unidades%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblUnidades" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td ><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, Vendedor%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblVendedor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td ><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, EstadoActual%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblEstadoActual" runat="server"></asp:Label>
            &nbsp;(
                <asp:Label ID="lblFechaCambio" runat="server"></asp:Label>)</td>        
        </tr>
        <tr>
            <td ><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource, CambiarEstado%>" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlEstados" runat="server" DataSourceID="SDSEstados" 
                DataTextField="Resource" DataValueField="IdEstado" AutoPostBack="true"
                    CssClass="boton" onselectedindexchanged="ddlEstados_SelectedIndexChanged" 
                    ondatabound="ddlEstados_DataBound"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="button" id="btnImprimir" onclick="window.print();" value="<% = Resources.Resource.Imprimir %>" class="boton" /></td>
        </tr>
    </table>
    
    
    <asp:SqlDataSource ID="SDSSolicitud" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesAlmacenObtener" SelectCommandType="StoredProcedure"
         UpdateCommand="AVE_PedidosCambiarEstadoSolicitud" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="IdPedido" QueryStringField="IdPedido" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="idTienda" QueryStringField="Tienda" Type="String" />
            <asp:Parameter Name="IdTerminal" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="IdPedido" QueryStringField="IdPedido" Type="Int32" />
            <asp:ControlParameter ControlID="ddlEstados" PropertyName="SelectedValue" Name="IdEstado" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
          <asp:SqlDataSource ID="AnadirCarrito" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                InsertCommand="AVE_InsertaCarrito" InsertCommandType="StoredProcedure" 
                oninserted="AnadirCarrito_Inserted">
                <InsertParameters>
                    <asp:Parameter Name="Usuario" Type="String" />
                    <asp:Parameter Name="IdCliente" Type="Int32" />
                    <asp:Parameter Name="Maquina" Type="String" />
                    <asp:Parameter Name="EstadoCarrito" Type="Int32" />
                    <asp:Parameter Name="IdCarrit" Type="Int32" Direction="ReturnValue" />
                </InsertParameters>
            </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SDSEstados" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_EstadosSolicitudesObtener" 
        SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
            <asp:SqlDataSource ID="AnadirDetalleCarrito" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                InsertCommand="AVE_GuardaDetalleCarrito" InsertCommandType="StoredProcedure">
                <InsertParameters>
                    <asp:Parameter Name="IdArticulo" Type="Int32" />
                    <asp:Parameter Name="IdCarrito" Type="Int32" />
                    <asp:Parameter Name="IdPedido" Type="Int32" />
                    <asp:Parameter Name="Talla" Type="String" />
                    <asp:Parameter Name="Cantidad" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>

                 </asp:Content>
