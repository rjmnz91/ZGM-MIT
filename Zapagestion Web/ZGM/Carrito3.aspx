<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito3.aspx.cs" Inherits="AVE.Carrito3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/Carrito.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Carrito.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:ScriptManager runat="server" ID="ScriptManager" ></asp:ScriptManager>

        <asp:HiddenField runat="server" ID="hidIdCliente" />

        <asp:GridView ID="gridCarrito" runat="server" Style="width: 99%"  GridLines="None"  PageSize="100" AutoGenerateColumns="False" 
            DataKeyNames="id_carrito_detalle" DataSourceID="dsCarrito" ShowHeader="false" AllowPaging="false" >
            <Columns>
                <asp:TemplateField>
                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdBorrar" CommandName="Delete" ImageUrl="~/img/Remove.png" CssClass="btnBorrar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="borderBottom" />
                    <ItemTemplate>
                    <asp:Image ID="FotoArticulo" CssClass="fallback" onerror="this.src='~/img/noImagen.jpg'"
                            ImageUrl='<%# ObtenerURL(Eval("idTemporada").ToString(), Eval("idProveedor").ToString(), Eval("ModeloProveedor").ToString()) %>'
                            runat="server" Style="max-width:150px; max-height:100px"   />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle CssClass="borderBottom" Width="21%" />
                    <ItemTemplate>
                        <div style="width: 100%">
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
                        </div>
                        <br />
                        <br />
                        <asp:Panel Style="width: 90%; text-align: center; font-size: 14px" runat="server"
                            ID="divforanea">
                            <asp:Label runat="server" ID="Label1" Text='<%$ Resources:Resource, SolicitudForanea%>'></asp:Label>:
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Font-Bold="true" />
                    <HeaderTemplate>
                        <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle CssClass="borderBottom" Width="40%" />
                    <ItemTemplate>
                        <div style="width: 100%; text-align: center">
                            <div style="float: left; width: 110px; text-align: right; margin-right: 15px; margin-left: 3%;">
                                <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>:
                            </div>
                            <div style="float: left; text-align: right; width: 70px;">
                                <asp:Label runat="server" ID="lblPrecioOriginal" Text='<%# FormateaNumero(Eval("PVPORI").ToString())%>'></asp:Label>
                            </div>
                            <br />
                            <div runat="server" id="laDescuento" style="width: 110px; float: left; text-align: right; margin-right: 15px; margin-left: 3%;">
                                <%-- <asp:Label runat="server" ID="Label5" Text="Descuento :">--%>
                                <asp:Label runat="server" ID="Label5" Text="Descuento :" ></asp:Label>
                            </div>
                            <div runat="server" id="laImporteDescuento" style="float: left; text-align: right; width: 70px;">
                                <asp:Label runat="server" ID="lblImporteDTO" Text='<%# "-" + FormateaNumero(Eval("DTOArticulo").ToString()) %>'></asp:Label>
                            </div>
                            <div runat="server" id="PorcDescuento" style="float: left; text-align: right; width: 70px;">
                                <asp:Label runat="server" ID="lblPorDescuento" Text=''></asp:Label>
                            </div>
                            <asp:Label ID="lblPromobr" runat="server" Text="" Visible="false" ></asp:Label>
                            <asp:Panel ID="divPromocionBotas" Visible="false"  runat="server" Style="clear: left">
                                <div style="width: 110px; text-align: right; margin-right: 15px; margin-left: 3%; float:left">
                                    <asp:Label runat="server" ID="DescriPromo" Text="" ></asp:Label> 
                                </div>
                                <div style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPromocionBotas" Text='<%# "-" + FormateaNumero(Eval("DtoPromo").ToString()) %>'></asp:Label>
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
            <%--<RowStyle CssClass="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active" />--%>
        </asp:GridView>

        <asp:SqlDataSource ID="dsCarrito" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
            SelectCommand="AVE_dameDetalleCarrito" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="IdCarrito" SessionField="IdCarrito" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:panel runat="server" id="divCliente" DefaultButton="cmdBuscarCliente" >
            <strong>Cliente: </strong>
            <br />
            <asp:UpdatePanel runat="server" ID="updPnlBuscar" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" class="tablaBusqueda">
                        <tr>
                            <td class="tdBusqueda">
                                <asp:TextBox ID="txtBuscarCliente" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td class="tdBusqueda" style="margin-left: 15px;">
                                <asp:Button ID="cmdBuscarCliente" runat="server" CssClass="btnBuscar" 
                                    onclick="cmdBuscarCliente_Click" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdBuscarCliente" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:panel>
        <asp:Panel runat="server" ID="pnlMensaje" CssClass="ui-state-highlight" Visible="false">
            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
        </asp:Panel>
        <br />
        <table id="tblBottom" class="tablaNormal" cellpadding="0" cellspacing="0">
            <tr>
                <td class="td50">
                    <asp:UpdatePanel runat="server" ID="updPnlTiposPago" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButton runat="server" ID="optTarjetaMIT" Text="Tarjeta MIT" />
                            <asp:RadioButton runat="server" ID="optNotaEmpleado" Text="Nota Empleado" Visible="false" />
                            <asp:RadioButton runat="server" ID="optTarjetaCliente9" Text="Cliente 9" Visible="false"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="td50">
                    <div id="divTotales">
                    </div>
                </td>
            </tr>
            <tr>
            </tr>
        </table>

        <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="false"
                Style="width: 99%" GridLines="None"  PageSize="2">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="borderBottom" />
                        <ItemTemplate>
                            <asp:ImageButton Style="width: 25px; height: 25px" ID="imgBorrar" ImageUrl="~/img/Remove.png"
                                CommandName="Delete" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="borderBottom" />
                        <ItemTemplate>
                        <asp:Image ID="FotoArticulo" CssClass="fallback" onerror="this.src='~/img/noImagen.jpg'"
                                ImageUrl='<%# ObtenerURL(Eval("idTemporada").ToString(), Eval("idProveedor").ToString(), Eval("ModeloProveedor").ToString()) %>'
                                runat="server" Style="max-width:150px; max-height:100px"   />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle CssClass="borderBottom" Width="21%" />
                        <ItemTemplate>
                            <div style="width: 100%">
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
                            </div>
                            <br />
                            <br />
                            <asp:Panel Style="width: 90%; text-align: center; font-size: 14px" runat="server"
                                ID="divforanea">
                                <asp:Label runat="server" ID="Label1" Text='<%$ Resources:Resource, SolicitudForanea%>'></asp:Label>:
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Font-Bold="true" />
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle CssClass="borderBottom" Width="40%" />
                        <ItemTemplate>
                            <div style="width: 100%; text-align: center">
                                <div style="float: left; width: 110px; text-align: right; margin-right: 15px; margin-left: 3%;">
                                    <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>:
                                </div>
                                <div style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPrecioOriginal" Text='<%# FormateaNumero(Eval("PVPORI").ToString())%>'></asp:Label>
                                </div>
                                <br />
                                <div runat="server" id="laDescuento" style="width: 110px; float: left; text-align: right; margin-right: 15px; margin-left: 3%;">
                                   <%-- <asp:Label runat="server" ID="Label5" Text="Descuento :">--%>
                                   <asp:Label runat="server" ID="Label5" Text="Descuento :" ></asp:Label>
                                </div>
                                <div runat="server" id="laImporteDescuento" style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblImporteDTO" Text='<%# "-" + FormateaNumero(Eval("DTOArticulo").ToString()) %>'></asp:Label>
                                </div>
                                <div runat="server" id="PorcDescuento" style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPorDescuento" Text=''></asp:Label>
                                </div>
                                <asp:Label ID="lblPromobr" runat="server" Text="" Visible="false" ></asp:Label>
                                <asp:Panel ID="divPromocionBotas" Visible="false"  runat="server" Style="clear: left">
                                    <div style="width: 110px; text-align: right; margin-right: 15px; margin-left: 3%; float:left">
                                       <asp:Label runat="server" ID="DescriPromo" Text="" ></asp:Label> 
                                    </div>
                                    <div style="float: left; text-align: right; width: 70px;">
                                        <asp:Label runat="server" ID="lblPromocionBotas" Text='<%# "-" + FormateaNumero(Eval("DtoPromo").ToString()) %>'></asp:Label>
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
            </asp:GridView>
    
    </div>
    </form>
</body>
</html>
