<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" Theme="Tema" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="AVE.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function() { txtFiltro.focus(); }
    </script>
       <br/>
        <asp:Panel ID="pnlNormal" runat="server" DefaultButton="btnBuscar">
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="boton" MaxLength="50" style="width:90px"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" 
                Text="<%$ Resources:Resource, Buscar%>" CssClass="boton" 
                onclick="btnBuscar_Click" />
            <asp:ImageButton runat="server" id="btnCambiarBusqueda" ImageUrl="~/img/busqueda.png" onclick="btnCambiarBusqueda_Click" style="vertical-align:bottom"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAvanzada" Visible="false" DefaultButton="btnBuscarAvanzado">
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, IdPedido%>"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtIdPedido" runat="server"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" Type="Integer" ControlToValidate="txtIdPedido" MinimumValue="1" MaximumValue="999999999" runat="server" ErrorMessage="*"></asp:RangeValidator>
            &nbsp;
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Proveedor%>"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtProveedor" runat="server" MaxLength="50"></asp:TextBox>
            &nbsp;
            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Producto%>"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtProducto" runat="server" MaxLength="50"></asp:TextBox>
            &nbsp;
            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Vendedor%>"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtVendedor" runat="server" MaxLength="50"></asp:TextBox>
            &nbsp;
            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Estado%>"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlEstados" runat="server" CssClass="boton"
                DataTextField="Resource" DataValueField="IdEstado" AutoPostBack="false" 
                ondatabound="ddlEstados_DataBound"/>
            &nbsp;
            <asp:Button ID="btnBuscarAvanzado" runat="server" Text="<%$ Resources:Resource, Buscar%>" onclick="btnBuscarAvanzado_Click" CssClass="boton" />
            <asp:ImageButton runat="server" id="ImageButton1" ImageUrl="~/img/busqueda.png" onclick="btnCambiarBusqueda_Click" style="vertical-align:bottom"/>
        </asp:Panel>
        <br />
        
        <asp:GridView ID="grdPedidos" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="IdPedido" SkinID="gridviewSkin" AllowSorting="true"
            onselectedindexchanged="grdPedidos_SelectedIndexChanged" 
            onrowdatabound="grdPedidos_RowDataBound" >
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText=">" ButtonType="Button" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="IdPedido" SortExpression="IdPedido" HeaderText="<%$ Resources:Resource, Pedido%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Proveedor" SortExpression="Proveedor" HeaderText="<%$ Resources:Resource, Proveedor%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="IdArticulo" SortExpression="IdArticulo" HeaderText="<%$ Resources:Resource, Articulo%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Referencia" SortExpression="Referencia" HeaderText="<%$ Resources:Resource, Referencia%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Modelo" SortExpression="Modelo" HeaderText="<%$ Resources:Resource, Modelo%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Descripcion" SortExpression="Descripcion" HeaderText="<%$ Resources:Resource, Descripcion%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Color" SortExpression="Color" HeaderText="<%$ Resources:Resource, Color%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Talla" SortExpression="Talla" HeaderText="<%$ Resources:Resource, Talla%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Unidades" SortExpression="Unidades" HeaderText="<%$ Resources:Resource, Unidades%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Vendedor" SortExpression="Vendedor" HeaderText="<%$ Resources:Resource, Vendedor%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="EstadosPedidosResource" SortExpression="EstadosPedidosResource" HeaderText="<%$ Resources:Resource, Estado%>" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="Fecha" SortExpression="Fecha" HeaderText="<%$ Resources:Resource, Fecha%>" ItemStyle-CssClass="GridItem"/>
            </Columns>
        </asp:GridView>

    <asp:SqlDataSource ID="SDSPedidosBuscar" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_PedidosBuscar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFiltro" Name="Filtro" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSPedidosAvanzado" runat="server" CancelSelectOnNullParameter="false"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_PedidosBuscarAvanzado" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtIdPedido" Name="IdPedido" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" DefaultValue="" />
            <asp:ControlParameter ControlID="txtProveedor" Name="Proveedor" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" DefaultValue=""/>
            <asp:ControlParameter ControlID="txtProducto" Name="Producto" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" DefaultValue=""/>
            <asp:ControlParameter ControlID="txtVendedor" Name="Vendedor" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" DefaultValue="" />
            <asp:ControlParameter ControlID="ddlEstados" Name="IdEstado" PropertyName="SelectedValue" Type="Int32" ConvertEmptyStringToNull="true" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="SDSEstados" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_EstadosPedidosObtener" 
        SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
</asp:Content>
