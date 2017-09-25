<%@ Page Language="C#" MasterPageFile="~/MasterPageHermes.Master" AutoEventWireup="true" CodeBehind="FinalizaCompraHERMES.aspx.cs" Inherits="AVE.FinalizaCompraHERMES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fallback
        {
            background-image: url("img/noImagen.jpg");
        }
        .borderBottom
        {
            border-bottom: solid 5px silver;
        }
        .style3
        {
            width: 63px;
        }
    </style>
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/CarritoDetalle.js" type="text/javascript"></script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
     <div id="divCompraFinalizada" runat="server" style="text-align:center; width:100%; font-size: 13pt; background-color: #EA2E8C; color:White">
        <asp:Label ID="lblCompraFinalizada" runat="server" Text="COMPRA FINALIZADA CON ÉXITO" style="padding-left: 20px;"></asp:Label>
     </div>
     <br />
     <br />
     <div id="divDatosPedido" runat="server" style="width: 80%; height: 100px; text-align : center">
        <table cellpadding = "2" cellspacing ="2" style="height: 86px; width: 507px" align="center" > 
            <tr>
                <td>
                    <asp:label ID="lblNumeroPedidoRecurso" runat="server" Font-Bold="true" Text ='<%$ Resources:Resource, NumeroPedido%>'></asp:label>:
                    <asp:label ID="lblNumeroPedido" runat="server"></asp:label>
                </td>
                <td class="style3">
                
                </td>
                <td align="left" >
                    <asp:Label ID="lblAsesorVentasRecurso" runat="server" Font-Bold="true" Text ='<%$ Resources:Resource, AsesorVentas%>'></asp:Label>:
                    <asp:Label ID="lblAsesorVentas" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                 <td class="style3">
                
                </td>
                <td align="left" >
                    <asp:Label ID="lblClienteRecurso" runat="server" Font-Bold="true" Text='<%$ Resources:Resource, Cliente%>' ></asp:Label>:
                    <asp:Label ID="lblCliente" runat="server" ></asp:Label>
                </td>
            </tr>
        </table>
     </div>

     <div id="divDatosEntregaDomicilio" runat="server" visible="false"  
            
            style="border-style: solid; border-width: 1px; width: 60%; height: 90px; text-align : left; margin-left:200px;">
        <div id="divDatosEntregaDomicilioTienda" runat="server" style="width: 100%; text-align: left;">
            <asp:Label ID="lblDatosEntregaDomicilio" runat="server" Font-Bold="true" Font-Size="11pt" Text="DATOS DE ENTREGA (DOMICILIO/TIENDA)"></asp:Label> 
        </div>
        
        <table cellpadding = "2" cellspacing ="2" align="center" width="100%" >
           <tr>
                <td>
                    <asp:label ID="lblDireccionEntrega" runat="server" ></asp:label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblTelefonoFijoEntrega" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td>
                    <asp:Label ID="lblColoniaEntrega" runat="server" ></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblTelefonoMovilEntrega" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td>
                    <asp:Label ID="lblCodigoPostalEntrega" runat="server" ></asp:Label>
                    <asp:Label ID="lblCiudadEntrega" runat="server"></asp:Label>
                    <asp:Label ID="lblEstadoEntrega" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblEmailEntrega" runat="server"></asp:Label>
                </td>
           </tr>
        </table>
     </div>
     <div id="divDatosEntregaTienda" runat="server" visible="false"
            
            style="border-style: solid; border-width: 1px; width: 60%; height: 90px; text-align : left; margin-left:200px;">
        <div id="div1" runat="server" style="width: 100%; text-align: left;">
            <asp:Label ID="lblDatosEntregaTienda" runat="server" Font-Bold="true" Font-Size="11pt" Text="DATOS DE ENTREGA (DOMICILIO/TIENDA)"></asp:Label> 
        </div>
        
        <table cellpadding = "2" cellspacing ="2" align="center" width="100%" >
           <tr>
                <td>
                    <asp:label ID="lblDireccionEntregaTienda" runat="server" ></asp:label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblTelefonoFijoEntregaTienda" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td>
                    <asp:Label ID="lblColoniaEntregaTienda" runat="server" ></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblTelefonoMovilEntregaTienda" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td>
                    <asp:Label ID="lblCodigoPostalEntregaTienda" runat="server" ></asp:Label>
                    <asp:Label ID="lblCiudadEntregaTienda" runat="server"></asp:Label>
                    <asp:Label ID="lblEstadoEntregaTienda" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblEmailEntregaTienda" runat="server"></asp:Label>
                </td>
           </tr>
        </table>
     </div>
     <br />
     <div id="divResumenCompra" runat="server" style="text-align:left; width:100%; font-size: 13pt; background-color: #EA2E8C; color:White">
        <asp:Label ID="lblResumenCompra" runat="server" Text="RESUMEN DE COMPRA" style="padding-left: 20px;"></asp:Label>
     </div>
     <br />
     <div id="divArticulosCarrito" runat="server" style="width:100%" visible="true">
            <%--<br />
            <br />--%>
            <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="false"
                Style="width: 100%" GridLines="None" PageSize="2" EnableViewState = "True" OnRowDataBound="gvCarrito_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>Artículo</HeaderTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="borderBottom" Font-Bold="true"  />
                        <ItemTemplate>
                            <asp:Image ID="FotoArticulo" CssClass="fallback" onerror="this.src='~/img/noImagen.jpg'"
                                ImageUrl='<%#ObtenerURL(Eval("IdArticulo").ToString()) %>'
                                runat="server" Style="max-width: 150px; max-height: 100px" />
                             <br />
                             <%--<asp:ImageButton runat="server" Style="width: 15px; height: 15px" ID="imgBorrar"
                                ImageUrl="~/img/Remove.png" CommandName="Borrar" CommandArgument='<%# Eval("id_carrito_detalle") %>' />
                             <asp:Label ID="lblEliminarArticulo" runat="server" Text ="Eliminar articulo" Font-Size="X-Small"  ></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>Descripción</HeaderTemplate>
                        <HeaderStyle HorizontalAlign ="Left" CssClass ="headerPaddingRight" />
                        <ItemStyle CssClass="borderBottom" Width="21%" />
                        <ItemTemplate>
                            <div style="width: 100%">
                                <div style="width: 30%; float: left; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label15" Text="Referencia"></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label16" Text='<%# CodigoAlfaDesdeIdArticulo(Eval("IdArticulo")) %>'></asp:Label>
                                </div>
                                <br />
                                <div style="float: left; width: 30%; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label78" Text='<%$ Resources:Resource, Modelo%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblModelo" Text='<%#Bind("ModeloProveedor") %>'></asp:Label>
                                </div>
                                <br />
                                <div style="width: 30%; float: left; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label3" Text='<%$ Resources:Resource, Color%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblColor" Text='<%#Bind("Color") %>'></asp:Label>
                                </div>
                                <br />
                                <div style="clear: left; width: 30%; float: left; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label4" Text='<%$ Resources:Resource, Talla%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblNombreTalla" Text='<%#Bind("Nombre_Talla") %>'></asp:Label>
                                </div>
                                <div style="clear: left; width: 30%; float: left; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblMaterialRecurso" Text='<%$ Resources:Resource, Material%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblMaterial" Text='<%#Bind("Material") %>'></asp:Label>
                                </div>
                            </div>
                            <%--<br />
                            <br />--%>
                            <asp:Panel Style="width: 90%; text-align: center; font-size: 14px" runat="server"
                                ID="divforanea">
                                <asp:Label runat="server" ID="Label1" Visible="false" Text='<%$ Resources:Resource, SolicitudForanea%>'></asp:Label>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle CssClass="borderBottom" Width="40%" Font-Bold="true"  />
                        <HeaderTemplate>Precio</HeaderTemplate>
                        <HeaderStyle HorizontalAlign ="Left" CssClass ="headerPadding" />
                        <ItemTemplate>
                            <div style="width: 100%; text-align: center">
                                <div style="float: left; width: 110px; text-align: right; color: white; margin-right: 15px; margin-left: 3%;">
                                    <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>
                                </div>
                                <div style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPrecioOriginal" Text='<%# FormateaNumero(Eval("PVPORI").ToString())%>'></asp:Label>
                                </div>
                                <br />
                                <div runat="server" id="laDescuento" style="width: 110px; float: left; text-align: right;
                                    margin-right: 15px; margin-left: 3%;">
                                    <%-- <asp:Label runat="server" ID="Label5" Text="Descuento :">--%>
                                    <asp:Label runat="server" ID="Label5" Text="Descuento :"></asp:Label>
                                </div>
                                <div runat="server" id="laImporteDescuento" style="float: left; text-align: right;
                                    width: 70px; color: Red ">
                                    <asp:Label runat="server" ID="lblImporteDTO" Text='<%# "-" + FormateaNumero(Eval("DTOArticulo").ToString()) %>'></asp:Label>
                                </div>
                                <div runat="server" id="PorcDescuento" style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPorDescuento" Text=''></asp:Label>
                                </div>
                                <asp:Label ID="lblPromobr" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Panel ID="divPromocionBotas" Visible="false" runat="server" Style="clear: left">
                                    <div style="width: 110px; text-align: right; margin-right: 15px; margin-left: 3%;
                                        float: left">
                                        <asp:Label runat="server" ID="DescriPromo" Text=""></asp:Label>
                                    </div>
                                    <div runat="server" id="divPromoVentas" style="float: left; text-align: right; width: 70px;">
                                        <asp:Label runat="server" ID="lblPromocionBotas" Text='<%# "-" + FormateaNumero(Eval("DtoPromo").ToString()) %>'></asp:Label>
                                        <asp:DropDownList ID="ddlSelecionarPromocion" runat="server" Visible="false" Width="180px"
                                            Style="padding-left: 10px;" OnSelectedIndexChanged="ddlSelecionarPromocion_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" id="divDescuentoPromocion" visible="false" style="float: left;
                                        text-align: right; width: 70px;">
                                        <asp:Label runat="server" ID="lblPorcentajeDescuento"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div style="clear: left; float: left; width: 110px; text-align: right; margin-right: 15px;
                                    margin-left: 3%;">
                                    <asp:Label runat="server" ID="Label6" Text='<%$ Resources:Resource, A_pagar%>'></asp:Label>:
                                </div>
                                <div style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPagar" Text=''></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="Silver" Font-Bold="True" Font-Size="11pt" 
                    HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
            </asp:GridView>
        </div>
        <br />
        <div runat="server" visible="true" id="divTotal" style="font-size:11pt; font-weight: bold; border-style: solid; border-width: medium medium medium 15px; border-color: #EA2E8C; margin-left: 45%; width: 260px;
            float: left">
            <div style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="lblSubtotalRecurso" Text='<%$ Resources:Resource, SubTotal_hermes%>'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right; padding-left: 8PX">
                <asp:Label runat="server" ID="lblSubTotal" Text=''></asp:Label>
            </div>
            <div  id = "divGastosEnvioRecurso" runat="server" style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="lblGastosEnvioRecurso" Text='Gastos de envío:'></asp:Label>
            </div>
            <div id = "divGastosEnvio" runat="server" style="float: left; width: 90px; text-align: right; padding-left: 8PX">
                <asp:Label runat="server" ID="lblGastosEnvio" Text=''></asp:Label>
            </div>
            <hr id="hrSubtotalGastos" style="background-color : #EA2E8C; height: 1px; width: 100%"/> 
            <br id="brDscuento" runat="Server" visible="false" />
            <div id="divEtiqDescuento" runat="server" style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="lblImporteDTOsRecurso" Text='<%$ Resources:Resource, Descuentos%>'></asp:Label>:
            </div>
            <div id="divImporteDescuento" runat="server" style="float: left; width: 90px; text-align: right;
                padding-left: 8PX">
                <asp:Label runat="server" ID="lblImporteDTOs" Text=''></asp:Label>
            </div>
            <div style="float: left; width: 150px; text-align: right; font-size: 11pt; font-weight: bold;">
                <asp:Label runat="server" ID="lblTotalPagarRecurso" Text='<%$ Resources:Resource, Total_A_Pagar%>'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 11pt;
                padding-left: 8PX">
                <asp:Label runat="server" ID="lblTotalPagar" Text=''></asp:Label>
            </div>
            <div style="float: left; width: 150px; text-align: right; font-size: 11pt; font-weight: bold;">
                <asp:Label runat="server" Visible="false"  ID="lblPagoPuntosNineRecurso" Text='<%$ Resources:Resource, PagoPuntosNine%>'></asp:Label>
            </div>
            <div id="divPagoPunosNine" style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 11pt;
                padding-left: 8PX; color: Red;">
                <asp:Label runat="server" Visible="false" ID="lblPagoPuntosNine" Text=''></asp:Label>
            </div>
             <hr id="hrSaldo" style="background-color : #EA2E8C; height: 1px; width: 100% ; visibility : hidden"/>
            <div id="divSaldoNineRecurso" style="float: left; width: 150px; text-align: right; font-size: 11pt; font-weight: bold;">
                <asp:Label runat="server" Visible="false"  ID="lblSaldoNineRecurso" Text='<%$ Resources:Resource, SaldoNine%>'></asp:Label>
            </div>
            <div id="divSaldoNine" style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 11pt;
                padding-left: 8PX">
                <asp:Label runat="server" Visible="false" ID="lblSaldoNine" Text=''></asp:Label>
            </div>
        </div>

     <%--<div runat="server" id="Resumen" style="margin-left: 1%; width: 500px; height: 55px; border: solid 1px black;
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
            onclick="cmdImprimirTicket_Click" />--%>
        <%--<asp:button runat="server" ID="cmdInicio" CssClass="botonNavegacion" 
            style="margin-left:150px; height: 60px; width:100px;" 
            onclick="cmdInicio_Click" />--%>

        <asp:button runat="server" ID="cmdInicio" CssClass="botonNavegacion" 
            style="margin-left:150px; height: 60px; width:100px;" 
            onclick="cmdInicio_Click" />

        <asp:SqlDataSource ID="AVE_CarritoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_dameDetalleCarrito" SelectCommandType="StoredProcedure"
        DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdCarrito" Type="int32" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="AVE_CarritoObtenerDirec" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_dameDetalleCarritoDirectivo" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IdCarrito" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsCodigoAlfaArticulo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="select top 1 codigoalfa from articulos where idarticulo = @idArticulo">
        <SelectParameters>
            <asp:Parameter Name="idArticulo" />
        </SelectParameters>
    </asp:SqlDataSource>

        </a></asp:Content>