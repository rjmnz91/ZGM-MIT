<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSolicitudAlmacen.ascx.cs" Inherits="AVE.controles.UCSolicitudAlmacen" %>


     
    <asp:Localize runat="server" ID="Cabecera" Text="<%$ Resources:Resource, SolicitudesCabecera%>"/>
    &nbsp;
    <asp:DropDownList ID="ddlEstados" runat="server" DataSourceID="SDSEstados" 
        DataTextField="Resource" DataValueField="IdEstado" AutoPostBack="true"
        onselectedindexchanged="ddlEstados_SelectedIndexChanged" CssClass="boton" 
        ondatabound="ddlEstados_DataBound"/>
         <asp:Localize runat="server" ID="Soldia" Text="<%$ Resources:Resource, SolicitudDia%>"/>
         &nbsp;
         <asp:TextBox name="date" id="fecha" runat="server"  
         onmouseover="return overlib('Selecciona Fecha.');" onmouseout="return nd();" 
         style="width: 108px; text-align:right;" class="estiloControles" 
         ontextchanged="fecha_TextChanged" AutoPostBack="True" />
         <img src="Img/calendar.png" id="selector"/><br/>
   <br /><br />
     <asp:GridView ID="grdSolicitudes" runat="server" AutoGenerateColumns="False" SkinId="gridviewSkin"
        DataKeyNames="IdPedido" DataSourceID="SDSSolicitudes" 
        onrowdatabound="grdSolicitudes_RowDataBound" EnableModelValidation="True" 
         onselectedindexchanged="grdSolicitudes_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="&gt;" ShowSelectButton="True" ItemStyle-CssClass="boton"  ControlStyle-Height="60px" ControlStyle-Width="40px"  />
            <asp:BoundField DataField="IdPedido" HeaderText="<%$ Resources:Resource, IdPedido%>" SortExpression="IdPedido" />
            <asp:BoundField DataField="IdTienda" HeaderText=" <%$ Resources:Resource, Tienda%> " SortExpression="idTienda" />
            <asp:BoundField DataField="Proveedor" HeaderText="<%$ Resources:Resource, Proveedor%>" SortExpression="Proveedor" />
            <asp:BoundField DataField="IdArticulo" HeaderText="<%$ Resources:Resource, Articulo%>" SortExpression="IdArticulo" />
            <asp:BoundField DataField="Referencia" HeaderText="<%$ Resources:Resource, Referencia%>" SortExpression="Referencia" />
            <asp:BoundField DataField="Modelo" HeaderText="<%$ Resources:Resource, Modelo%>" SortExpression="Modelo" />
            <asp:BoundField DataField="Descripcion" HeaderText="<%$ Resources:Resource, Descripcion%>" SortExpression="Descripcion" />
            <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color%>" SortExpression="Color" />
            <asp:BoundField DataField="Talla" HeaderText="<%$ Resources:Resource, Talla%>" SortExpression="Talla" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Unidades" HeaderText="<%$ Resources:Resource, Unidades%>" SortExpression="Unidades"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Vendedor" HeaderText="<%$ Resources:Resource, Vendedor%>" SortExpression="Vendedor" />
            <asp:TemplateField HeaderText="<%$ Resources:Resource, Estado%>" SortExpression="EstadoSolicitudResource">
                    <ItemTemplate>
                         <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" Width="100px" OnSelectedIndexChanged="ddlEstadoSolicitud_SelectedIndexChanged" AutoPostBack="true"  >
                         </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="EstadoSolicitudResource" HeaderText="<%$ Resources:Resource, Estado%>" SortExpression="EstadoSolicitudResource" />--%>
            <asp:BoundField DataField="FechaPedido" HeaderText="<%$ Resources:Resource, Fecha%>" SortExpression="FechaPedido" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Localize runat="server" ID="Cabecera" Text="<%$ Resources:Resource, NoResultados%>"/>
        </EmptyDataTemplate>
    </asp:GridView>
        
    <asp:SqlDataSource ID="SDSSolicitudes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesAlmacenObtener" 
         SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlEstados" DefaultValue="1" Name="IdEstado" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:SessionParameter Name="IdTienda" SessionField="IdTienda" Type="String"/>    
       </SelectParameters>
       
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSEstados" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_EstadosSolicitudesObtener" 
        SelectCommandType="StoredProcedure" >
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSolicitudesPorDia" runat="server"  ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesAlmacenObtener" 
         SelectCommandType="StoredProcedure">
          <SelectParameters>
            <asp:SessionParameter Name="IdTienda" SessionField="IdTienda" Type="String"/>
            <asp:ControlParameter ControlID="fecha" Name="Fecha" PropertyName="Text"/>
          </SelectParameters>
     </asp:SqlDataSource>


