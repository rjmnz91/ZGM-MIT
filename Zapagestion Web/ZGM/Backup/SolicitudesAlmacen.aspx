<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" Theme="Tema" CodeBehind="SolicitudesAlmacen.aspx.cs" Inherits="AVE.SolicitudesAlmacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="js/SolicitudesAlmacen.js" type="text/javascript"></script>

<%--        <script src="js/calendar.js" language="javascript" type="text/javascript" ></script>
        <script src="js/calendar-es.js" language="javascript" type="text/javascript"></script>
        <script src="js/calendar-setup.js" language="javascript" type="text/javascript"></script>
        <script type="text/javascript" src="js/overlib.js"></script>
        <link rel="Stylesheet" type="text/css" href="css/calendar-green.css" /> 

--%>
    <script type="text/javascript">
        // MJM 11/03/2014 DESACTIVADO PARA MANUEL
        // opvar Tiempo = '<%=ConfigurationManager.AppSettings["TiempoRefresco"].ToString()%>';
        // setTimeout('window.location.reload()', Tiempo);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 
    <asp:Localize runat="server" ID="Cabecera" Text="<%$ Resources:Resource, SolicitudesCabecera%>"/>
    &nbsp;
    <asp:DropDownList ID="ddlEstados" runat="server" DataSourceID="SDSEstados" 
        DataTextField="Resource" DataValueField="IdEstado" AutoPostBack="true"
        CssClass="boton"  ondatabound="ddlEstados_DataBound"/>
         <asp:Localize runat="server" ID="Soldia" Text="<%$ Resources:Resource, SolicitudDia%>"/>
         &nbsp;
         <asp:TextBox name="date" id="fecha" runat="server"  
         onmouseover="return overlib('Selecciona Fecha.');" onmouseout="return nd();" 
         style="width: 108px; text-align:right;" class="estiloControles" 
         ontextchanged="fecha_TextChanged" AutoPostBack="True" />
         <%--<img src="Img/calendar.png" id="selector"/><br/>--%>

         <asp:HiddenField runat="server" ID="hidSelectedTab" Value="0" />

         <div id="tabs" style="width: 95%;">
            <ul class="ulTabs">
            <li><a href="#Almacen">Almacen</a></li>
            <li><a href="#Otras">Otras tiendas</a></li>
            </ul>
            <div id="Almacen">
                <asp:GridView ID="grdSolicitudes" runat="server" AutoGenerateColumns="False" SkinId="gridviewSkin"
                    DataKeyNames="IdPedido,IdArticulo,Talla" DataSourceID="SDSSolicitudes" 
                    onrowdatabound="grdSolicitudes_RowDataBound" 
                    onselectedindexchanged="grdSolicitudes_SelectedIndexChanged" Width="100%">
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True"  >
                    <ControlStyle Height="60px" Width="40px" />
                    <ItemStyle CssClass="boton" />
                    </asp:CommandField>
                    <asp:BoundField DataField="IdPedido" 
                        HeaderText="<%$ Resources:Resource, IdPedido %>" SortExpression="IdPedido" 
                        Visible="False"  />
                    <asp:BoundField DataField="Proveedor" 
                        HeaderText="<%$ Resources:Resource, Proveedor %>" SortExpression="Proveedor" />
                    <asp:BoundField DataField="IdArticulo" 
                        HeaderText="<%$ Resources:Resource, Articulo %>" 
                        SortExpression="IdArticulo" Visible="False" />
                    <asp:BoundField DataField="Referencia" 
                        HeaderText="<%$ Resources:Resource, Referencia %>" 
                        SortExpression="Referencia" />
                    <asp:BoundField DataField="Modelo" 
                        HeaderText="<%$ Resources:Resource, Modelo %>" SortExpression="Modelo" />
                    <asp:BoundField DataField="Descripcion" 
                        HeaderText="<%$ Resources:Resource, Descripcion %>" 
                        SortExpression="Descripcion" />
                    <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color %>" 
                        SortExpression="Color"  Visible="False" />
                    <asp:BoundField DataField="Talla" HeaderText="<%$ Resources:Resource, Talla %>" 
                        SortExpression="Talla" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Unidades" 
                        HeaderText="<%$ Resources:Resource, Unidades %>" SortExpression="Unidades" 
                        Visible="False">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vendedor" 
                        HeaderText="<%$ Resources:Resource, Vendedor %>" SortExpression="Vendedor" />
                    <asp:TemplateField HeaderText="<%$ Resources:Resource, Estado %>" 
                        SortExpression="EstadoSolicitudResource">
                            <ItemTemplate>
                                <div class="divContainerDdlSolicitud">
                                    <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" CssClass="ddlEstadoSolicitud" Width="100px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoSolicitud_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaPedido" 
                        HeaderText="<%$ Resources:Resource, Fecha %>" SortExpression="FechaPedido" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Localize runat="server" ID="Cabecera" Text="<%$ Resources:Resource, NoResultados%>"/>
                </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div id="Otras" style="display:none;">

                                        <asp:GridView ID="grdSolicitudesOtras" runat="server" 
                                            AutoGenerateColumns="False" SkinId="gridviewSkin"
                DataKeyNames="IdPedido,IdArticulo,Talla,IdTienda" DataSourceID="SDSSolicitudesOtras" 
                onrowdatabound="grdSolicitudesOtras_RowDataBound" 
                    onselectedindexchanged="grdSolicitudesOtras_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True"  >
                    <ControlStyle Height="60px" Width="40px" />
                    <ItemStyle CssClass="boton" />
                    </asp:CommandField>
                    <asp:BoundField DataField="IdPedido" 
                        HeaderText="<%$ Resources:Resource, IdPedido %>" SortExpression="IdPedido" 
                        Visible="False"  />
                    <asp:TemplateField HeaderText="<%$ Resources:Resource, Tienda %>" 
                        SortExpression="EstadoSolicitudResource">
                            <ItemTemplate>
                                    <asp:DropDownList ID="ddlIdTienda" runat="server" CssClass="ddlEstadoSolicitud" Width="100px" OnSelectedIndexChanged="ddlIdTiendaSolicitud_SelectedIndexChanged" AutoPostBack="true"  >
                                    </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proveedor" 
                        HeaderText="<%$ Resources:Resource, Proveedor %>" SortExpression="Proveedor" />
                    <asp:BoundField DataField="IdArticulo" 
                        HeaderText="<%$ Resources:Resource, Articulo %>" 
                        SortExpression="IdArticulo" Visible="False" />
                    <asp:BoundField DataField="Referencia" 
                        HeaderText="<%$ Resources:Resource, Referencia %>" 
                        SortExpression="Referencia" />
                    <asp:BoundField DataField="Modelo" 
                        HeaderText="<%$ Resources:Resource, Modelo %>" SortExpression="Modelo" />
                    <asp:BoundField DataField="Descripcion" 
                        HeaderText="<%$ Resources:Resource, Descripcion %>" 
                        SortExpression="Descripcion" />
                    <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color %>" 
                        SortExpression="Color" Visible="False"  />
                    <asp:TemplateField HeaderText="<%$ Resources:Resource, Talla %>">                
                        <ItemStyle Width="120px" HorizontalAlign="Center"/>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lblTalla" Text='<%#Bind("Talla") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:BoundField DataField="Unidades" 
                        HeaderText="<%$ Resources:Resource, Unidades %>" SortExpression="Unidades" 
                        Visible="False">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vendedor" 
                        HeaderText="<%$ Resources:Resource, Vendedor %>" SortExpression="Vendedor" />
                    <asp:TemplateField HeaderText="<%$ Resources:Resource, Estado %>" 
                        SortExpression="EstadoSolicitudResource">
                            <ItemTemplate>
                                    <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" CssClass="ddlEstadoSolicitud" Width="100px" OnSelectedIndexChanged="ddlEstadoSolicitudotros_SelectedIndexChanged" AutoPostBack="true"  >
                                    </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaPedido" 
                        HeaderText="<%$ Resources:Resource, Fecha %>" SortExpression="FechaPedido" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Localize runat="server" ID="Cabecera" Text="<%$ Resources:Resource, NoResultados%>"/>
                </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

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

                 <asp:SqlDataSource ID="SDSPedidosCambiarTienda" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
      InsertCommand="AVE_PedidosCambiarTienda" InsertCommandType="StoredProcedure">
           <InsertParameters>
                        <asp:Parameter Name="idPedido" Type="Int64" />
                        <asp:Parameter Name="IdTienda" Type="String" />
       </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSSolicitudes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesAlmacenObtener" 
         SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlEstados" DefaultValue="1" Name="IdEstado" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:SessionParameter Name="IdTienda" SessionField="IdTienda" Type="String"/>    
            <asp:Parameter Name="IdTerminal" Type="Int32" />
       </SelectParameters>
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SDSSolicitudesOtras" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesOtrasTiendasObtener" 
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
    <asp:SqlDataSource ID="SDSEstadosAlmacen" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_EstadosSolicitudesAlmacenObtener" 
        SelectCommandType="StoredProcedure" >
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SDSEstadosOtras" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_EstadosSolicitudesOtrasObtener" 
        SelectCommandType="StoredProcedure" >
    </asp:SqlDataSource>
      <asp:SqlDataSource ID="SDSTiendasProducto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_StockEnTiendaObtener" 
        SelectCommandType="StoredProcedure">
          <SelectParameters>
                        <asp:Parameter Name="IdArticulo" Type="String" />
                        <asp:SessionParameter Name="IdTienda" SessionField="IdTienda" Type="String"/>    
         </SelectParameters>
    </asp:SqlDataSource>
      
     <asp:SqlDataSource ID="SqlDataSolicitudesPorDia" runat="server"  ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_SolicitudesAlmacenObtener" 
         SelectCommandType="StoredProcedure">
          <SelectParameters>
            <asp:SessionParameter Name="IdTienda" SessionField="IdTienda" Type="String"/>
            <asp:ControlParameter ControlID="fecha" Name="Fecha" PropertyName="Text"/>
          </SelectParameters>
     </asp:SqlDataSource>
     </asp:Content>

