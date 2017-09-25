<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHermes.Master" AutoEventWireup="true"
    CodeBehind="CarritoDetalleHERMES.aspx.cs" Inherits="AVE.CarritoDetalleHERMES"
    EnableEventValidation="false" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fallback
        {
            background-image: url("img/noImagen.jpg");
        }
        .borderBottom
        {
            border-bottom: solid 5px silver;
        }
        .style2
        {
            width: 262px;
        }
    </style>
    <%--<script src="js/jquery-1.2.6.min.js" type="text/javascript"></script>--%>
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/CarritoDetalleHermes.js" type="text/javascript"></script>
    <script type="text/javascript">

        var nav4 = window.Event ? true : false;
        function IsNumber(evt, obj) {
            // Backspace = 8, Enter = 13, ’0′ = 48, ’9′ = 57, ‘.’ = 46,","radio
            var key = nav4 ? evt.which : evt.keyCode;
            var text = obj.value;


            if (text.indexOf(",") > 0 || text.indexOf(".") > 0) {
                if (key == 46 || key == 44) {
                    key = 45;
                }
            }

            return (key <= 13 || (key >= 48 && key <= 57) || key == 46 || key == 44);
        }

        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event)
                keycode = window.event.keyCode;
            else if (e)
                keycode = e.which;



            if ($.browser.chrome) {

                if (keycode == 116 || (e.ctrlKey && keycode == 82)) {
                    e.returnValue = false;
                    e.keyCode = 0;
                    return false;

                }
            }
            // Mozilla firefox
            if ($.browser.mozilla) {
                if (keycode == 116 || (e.ctrlKey && keycode == 82)) {
                    if (e.preventDefault) {
                        e.preventDefault();
                        e.stopPropagation();
                    }
                }
            }
            // IE
            else if ($.browser.msie) {
                if (keycode == 116 || (window.event.ctrlKey && keycode == 82)) {
                    window.event.returnValue = false;
                    window.event.keyCode = 0;
                    window.status = "Refresh is disabled";
                }
            }
        }



        //

       
            function copiarDatosFacturacion() {
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUNombreFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUNombreF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUDireccionFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUDireccionF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUNoExteriorFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUNoExteriorF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUNoInteriorFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUNoInteriorF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_cboOUEstadoFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_cboOUEstadoF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUCiudadFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUCiudadF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUColoniaFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUColoniaF").value;
                document.getElementById("ctl00_ContentPlaceHolder1_txtOUCodigoPostalFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUCodigoPostalF").value;
               // document.getElementById("ctl00_ContentPlaceHolder1_txtOUTelfCelularFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUTelfCelularF").value;
                //document.getElementById("ctl00_ContentPlaceHolder1_txtOUTelfFijoFacturacion").value = document.getElementById("ctl00_ContentPlaceHolder1_txtOUTelfFijoF").value;
            }
        
            function GetData( textControl ) {
            
            
                var rfc = textControl.value;
                if($('input#radioDatosFacturacion').is(':checked')) {
                }else{
                    $.ajax({
                        type: "POST",
                        url: "CarritoDetalleHermes.aspx/getdatosRFC",
                        data: "{'idRFC':'" + rfc + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (msg) {
                            if (msg != null) {
                                $('#ctl00_ContentPlaceHolder1_txtOUNombreFacturacion').val(msg.d.nombre);
                                $('#ctl00_ContentPlaceHolder1_txtOUDireccionFacturacion').val(msg.d.direccion);
                                $('#ctl00_ContentPlaceHolder1_txtOUNoExteriorFacturacion').val(msg.d.exterior);
                                $('#ctl00_ContentPlaceHolder1_txtOUNoInteriorFacturacion').val(msg.d.interior);
                                $('#ctl00_ContentPlaceHolder1_cboOUEstadoFacturacion option').each(function () {
                                    var opcion = this.text.toUpperCase();
                                    this.selected = (opcion == msg.d.estado);
                                });  //  [text=" + msg.d.estado + "]").attr("selected", true)
                                // $('#ctl00_ContentPlaceHolder1_cboOUEstadoFacturacion option[text=" + msg.d.estado + "]').attr("selected", true)
                                //   $('#ctl00_ContentPlaceHolder1_cboOUEstadoFacturacion').val(msg.d.estado);
                                $('#ctl00_ContentPlaceHolder1_txtOUCiudadFacturacion').val(msg.d.ciudad);
                                $('#ctl00_ContentPlaceHolder1_txtOUColoniaFacturacion').val(msg.d.colonia);
                                $('#ctl00_ContentPlaceHolder1_txtOUCodigoPostalFacturacion').val(msg.d.cp);
                                // $('#ctl00_ContentPlaceHolder1_txtOUTelfCelularFacturacion').val(msg.movil);
                                //('#ctl00_ContentPlaceHolder1_txtOUTelfFijoFacturacion').val(msg.telefono);
                                // $('#id_company').val(msg.d.id_company);

                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.responseText); }
                    });
                }
            }
    
    </script>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManagerUpatePanel" />
    <asp:HiddenField ID="HiddenPromo" runat="server" />
    <asp:HiddenField ID="hidIdCliente" runat="server" />
    <asp:HiddenField ID="hidPuntos" runat="server" />
    <asp:HiddenField ID="hidNumeroTarjetaCliente9" runat="server" />
    <asp:HiddenField ID="hidSourceID" runat="server" />
    <asp:HiddenField ID="HiddenTipoCliente" runat="server" />
    <asp:HiddenField ID="hidTotalArtTR" runat="server" />
    <asp:HiddenField ID="hidPagosNineTR" runat="server" />
    <br />
    <div id="divCarritoCompra" visible="true" runat="server" style="width: 100%; height: 30px;
        background-color: #EA2E8C; color: White; font-size: large;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="img/HERMES/btCarroRosa.png" height="30px" alt="Carrito" />
                </td>
                <td>
                    <asp:Label ID="lblCarritoCompra" runat="server" Text="CARRITO DE COMPRA" Style="padding-left: 20px;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="divPasosProcesoCompra">
        <table width="100%">
            <tr>
                <td align="left" style="width: 20%">
                    <div id="divProductosCarrito" runat="server" visible="true" class="pasoActivo" width="50%">
                        <asp:Label runat="server" ID="lblProductosCarrito" Text='Productos del Carrito'></asp:Label>
                    </div>
                </td>
                <td style="width: 20%" align="center">
                    <asp:ImageButton ImageUrl="~/img/HERMES/FlechaRosa3.jpg" ID="ImageButtonProductosPago"
                        runat="server" Width="30" Height="30" />
                </td>
                <td align="left" style="width: 20%">
                    <div id="divPago" runat="server" visible="true" class="pasoInactivo" width="50%">
                        <asp:Label runat="server" ID="lblPago" Text='Pago'></asp:Label>
                    </div>
                </td>
                <td style="width: 20%" align="center">
                    <asp:ImageButton ImageUrl="~/img/HERMES/FlechaRosa3.jpg" ID="ImagenButtonPagoEntrega"
                        runat="server" Width="30" Height="30" />
                </td>
                <td align="left" style="width: 20%">
                    <div id="divEntrega" runat="server" visible="true" class="pasoInactivo" width="50%">
                        <asp:Label runat="server" ID="lblEntrega" Text='Entrega'></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 10px" id="divDetalle" runat="server">
        <%-- <asp:Label runat="server" ID="Label5" Text="Descuento :">--%>
        <div id="divArticulosCarrito">
            <%-- BT Quitar envio a POS
    <div style="margin-top: 10px" id="divResumen" runat="server" visible="false">
        <br />
        <br />
        <div class="ui-widget-overlay">
        </div>
        <div class="ui-widget-shadow" style="width: 300px; height: 200px; position: absolute;
            left: 51%; top: 51%; margin: -100px 0 0 -150px;">
        </div>
        <div style="width: 300px; height: 200px; position: absolute; left: 50%; top: 50%;
            margin: -100px 0 0 -150px;">--%>
            <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="false"
                Style="width: 100%" GridLines="None" OnRowDataBound="gvCarrito_RowDataBound"
                PageSize="2" OnRowCommand="gvCarrito_RowCommand" EnableViewState="True">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Artículo</HeaderTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="borderBottom" Font-Bold="true" />
                        <ItemTemplate>                        
                            <asp:Image ID="FotoArticulo" CssClass="fallback2" 
                                ImageUrl='<%# ObtenerURL(Eval("IdArticulo").ToString()) %>'
                                runat="server" Style="max-width: 150px; max-height: 100px" />
                            <br />
                            <asp:ImageButton runat="server" Style="width: 15px; height: 15px" ID="imgBorrar"
                                ImageUrl="~/img/Remove.png" CommandName="Borrar" CommandArgument='<%# Eval("id_carrito_detalle") %>' />
                            <asp:Label ID="lblEliminarArticulo" runat="server" Text="Eliminar articulo" Font-Size="X-Small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Descripción</HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="headerPaddingRight" />
                        <ItemStyle CssClass="borderBottom" Width="21%" />
                        <ItemTemplate>
                            <div style="width: 100%">
                                <div style="width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                    <asp:Label runat="server" ID="Label15" Text="Referencia"></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label16" Text='<%# CodigoAlfaDesdeIdArticulo(Eval("IdArticulo")) %>'></asp:Label>
                                </div>
                                <br />
                                <div style="float: left; width: 30%; font-weight: bold; font-size: 12px;">
                                    <asp:Label runat="server" ID="Label78" Text='<%$ Resources:Resource, Modelo%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblModelo" Text='<%#Bind("ModeloProveedor") %>'></asp:Label>
                                </div>
                                <br />
                                <div style="width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                    <asp:Label runat="server" ID="Label3" Text='<%$ Resources:Resource, Color%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblColor" Text='<%#Bind("Color") %>'></asp:Label>
                                </div>
                                <br />
                                <div style="clear: left; width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                    <asp:Label runat="server" ID="lblMaterialRecurso" Text='<%$ Resources:Resource, Material%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblMaterial" Text='<%#Bind("Material") %>'></asp:Label>
                                </div>
                                <br />
                                <div style="clear: left; width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                    <asp:Label runat="server" ID="Label4" Text='<%$ Resources:Resource, Talla%>'></asp:Label>:
                                </div>
                                <div style="float: left; font-size: 10px;">
                                    <asp:Label runat="server" ID="lblNombreTalla" Text='<%#Bind("Nombre_Talla") %>'></asp:Label>
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
                        <ItemStyle CssClass="borderBottom" Width="40%" Font-Bold="true" />
                        <HeaderTemplate>
                            Precio</HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="headerPadding" />
                        <ItemTemplate>
                            <div style="width: 100%; text-align: center">
                                <div style="float: left; width: 110px; text-align: right; color: white; margin-right: 15px;
                                    margin-left: 3%;">
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
                                    width: 80px; color: Red">
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
                <HeaderStyle BackColor="Silver" Font-Bold="True" Font-Size="11pt" HorizontalAlign="Center"
                    VerticalAlign="Middle" Width="20px" />
            </asp:GridView>
        </div>
        <br />
        <div id="divSeleccionEnvio" runat="server" visible="false" 
            style="float: left; padding-left:50px; font-weight: bold; height: 127px;">
        <asp:Label ID="lblSeleccioneEnvio" runat="server" Text="Seleccione un tipo de envío: "></asp:Label>
            <br />
            <asp:RadioButton ID="rbEnvioTienda" runat="server" GroupName="GrupoSeleccionEnvio"
                Text="Tienda" OnCheckedChanged="rbEnvioTienda_CheckedChanged" AutoPostBack="true" style="margin-left:30px; font-weight: bold;" />
            <br />
            <asp:RadioButton ID="rbEnvioDomicilio" runat="server" GroupName="GrupoSeleccionEnvio"
                Text="Domicilio" AutoPostBack="true" OnCheckedChanged="rbEnvioDomicilio_CheckedChanged" style="margin-left:30px;font-weight: bold;" />
          <!--  <asp:CheckBox ID="chkEnvioExpress" runat="server" AutoPostBack="true" Text="Envío Express" OnCheckedChanged="chkEnvioExpress_CheckedChanged" Font-Size="10px" Visible="false"  />-->
            <br />
            <asp:CheckBox ID="chkEnvio" runat="server" AutoPostBack="True" OnCheckedChanged="chkEnvio_CheckedChanged" 
                Text="Cobrar envío" />
            <br />
            <br />
            <asp:Label ID="lblEnvioSeleccionadoRecurso" runat="server" Visible="false" style="font-weight: bold;" ></asp:Label>
            <asp:Label ID="lblEnvioSeleccionado" runat="server" Visible="false" style="font-weight: bold;"></asp:Label>
        </div>
        <%-- <asp:RadioButton ID="Cliente9" runat="server" Text="Cliente 9" CssClass="RellenoCarrito" />
    <asp:RadioButton ID="Tarjeta" runat="server"  Text="Tarjeta MIT" CssClass="RellenoCarrito1" />--%>
        <div runat="server" visible="false" id="divTotal" style="font-size: 11pt; font-weight: bold;
            border-style: solid; border-width: medium medium medium 6px; border-color: #EA2E8C;
            margin-left: 0px; width: 260px; float: right">
            <div style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="lblSubtotalRecurso" Text='<%$ Resources:Resource, SubTotal_hermes%>'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right; padding-left: 8PX">
                <asp:Label runat="server" ID="lblSubTotal" Text=''></asp:Label>
            </div>
            <div id="divGastosEnvioRecurso" runat="server" style="float: left; width: 150px;
                text-align: right;">
                <asp:Label runat="server" ID="lblGastosEnvioRecurso" Text='Gastos de envío:'></asp:Label>
            </div>
            <div id="divGastosEnvio" runat="server" style="float: left; width: 90px; text-align: right;
                padding-left: 8PX">
                <asp:Label runat="server" ID="lblGastosEnvio" Text=''></asp:Label>
            </div>
            <hr id="hrSubtotalGastos" style="background-color: #EA2E8C; height: 1px; width: 100%;
                -webkit-margin-before: 0.3em; -webkit-margin-after: 0.1em" />
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
            <div style="float: left; width: 150px; text-align: right; font-size: 14px; font-weight: bold;">
                <asp:Label runat="server" Visible="false" ID="lblPagoPuntosNineRecurso" Text='<%$ Resources:Resource, PagoPuntosNine%>'></asp:Label>
            </div>
            <div id="divPagoPunosNine" style="float: left; width: 90px; text-align: right; font-weight: bold;
                font-size: 11pt; padding-left: 8PX; color: Red;">
                <asp:Label runat="server" Visible="false" ID="lblPagoPuntosNine" Text=''></asp:Label>
            </div>
            <hr id="hrSaldo" runat="server" visible="false" style="background-color: #EA2E8C;
                height: 1px; width: 100%; -webkit-margin-before: 0.3em; -webkit-margin-after: 0.1em" />
            <div id="divSaldoNineRecurso" style="float: left; width: 150px; text-align: right;
                font-size: 11pt; font-weight: bold;">
                <asp:Label runat="server" Visible="false" ID="lblSaldoNineRecurso" Text='<%$ Resources:Resource, SaldoNine%>'></asp:Label>
            </div>
            <div id="divSaldoNine" style="float: left; width: 90px; text-align: right; font-weight: bold;
                font-size: 11pt; padding-left: 8PX">
                <asp:Label runat="server" Visible="false" ID="lblSaldoNine" Text=''></asp:Label>
            </div>
        </div>
        <%-- BT Quitar envio a POS
           
           <div class="ui-widget-content" style="text-align: center; height: 100%; font-size: 14px">
               
                <div class="ui-widget-header">
                    <br />
                    ENVIO A POS
                    <br />
                </div>
                <br />
                <div style="width: 25%; float: left;">
                    <asp:Image runat="server" ID="imgEnvioPos" ImageUrl="~/img/Ok.png" Width="64px" Height="64px" />
                </div>
                <div style="width: 75%; float: right;">
                    <p>
                        <asp:Label Style="font-weight: bold" ID="lblPOS" runat="server" Text=""></asp:Label>
                    </p>
                    <div style="display: inline-block; text-align: center;">
                        <br />
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>--%>
        <div runat="server" id="divResumenPago" visible="false" style="margin-left: 45%;
            width: 260px; height: 100%; border: solid 1px black; float: left">
            <asp:Repeater ID="RepeaterPago" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="width: 150px; text-align: right;">
                            <asp:Label runat="server" ID="LabPAgo" Style='<%#DataBinder.Eval(Container.DataItem, "Tipo").ToString()=="Total Pagado:" ? "width: 150px; text-align: right; font-weight: bold; font-size: 12px;": "width: 150px; text-align: right;"%>'
                                Text='<%# Eval("Tipo") %>' />
                        </td>
                        <td style="width: 90px; text-align: right">
                            <asp:Label runat="server" ID="LabImporte" Style="width: 90px; text-align: right;
                                font-weight: bold; font-size: 12px;" Text='<%#FormateaNumero(Eval("Importe").ToString()) %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div runat="server" id="divPendientePago" style="border: 3px solid #EA2E8C; margin-left: 400px;
            width: 262px; height: 20px; float: right; font-weight: bold;">
            <div style="float: left; width: 150px; text-align: right; font-size: 11pt; font-weight: bold;
                padding-left: 8PX">
                <asp:Label runat="server" ID="Label19" Text='Total a Pagar'></asp:Label>:
            </div>
            <div style="float: right; width: 90px; text-align: right; font-weight: bold; font-size: 11pt;">
                <asp:Label runat="server" ID="TotPendiente" Text=''></asp:Label>
            </div>
            <br />
        </div>
    </div>
    <br clear="all" />
    <br />
    <asp:Button ID="btnCancelaVenta" runat="server" class="botonCancelarVenta" 
        Text="Cancela Venta" onclick="btnCancelaVenta_Click" OnClientClick ="return confirm('¿ Desea eliminar todos los productos del carrito ?\n\n RECUERDE QUE SI TIENE ALGUN PAGO BANCARIO EN ESTE CARRITO DEBE REALIZAR LA DEVOLUCION.');" />
    <br />
    <br />
    <hr id="hrPendientePago" runat="server" visible="true" style="height: 5px; width: 100%;
        background-color: Silver; border-width: 0px" />
        
    <%--<br />--%><%--<asp:RequiredFieldValidator runat="server" Enabled="false" ID="rqfEmail" ErrorMessage="*"
                Font-Bold="true" ControlToValidate="txtemail"> </asp:RequiredFieldValidator>--%><%--<asp:RequiredFieldValidator runat="server" ID="rqfComentarios" Enabled="false"  ErrorMessage="*"
                Font-Bold="true" ControlToValidate="TxtComentarios"> </asp:RequiredFieldValidator>--%>
    <br />
    <div id="divDireccionEntrega" visible="false" runat="server" style="width: 100%;
        height: 30px; background-color: Silver; color: Black; font-size: large;">
        <asp:Label ID="lblDireccionEntrega" runat="server" Text="DIRECCION DE ENTREGA" Style="padding-left: 20px;"></asp:Label>
    </div>
    <div id="divDatosDelpago" visible="false" runat="server" style="width: 100%; height: 20px;
        background-color: Silver; color: Black; font-size: large;">
        <asp:Label ID="lblDatosDelPago" runat="server" Text="DATOS DEL PAGO" Style="padding-left: 20px;"></asp:Label>
    </div>
    <%-- <asp:RadioButton ID="Cliente9" runat="server" Text="Cliente 9" CssClass="RellenoCarrito" />
             <asp:RadioButton ID="Tarjeta" runat="server"  Text="Tarjeta MIT" CssClass="RellenoCarrito1" />--%>
    <div id="divFormaPagoCliente" runat="server" style="margin-top: 10px; border-style: none;
        display: block; clear: left; overflow: hidden; width: 98%; border-width: 1px;
        border-width: 1px" runat="server" visible="false">
        <div id="divcliente" runat="server" style="margin-top: 2px; border-style: none; width: 50%;
            float: left; border-width: 1px; margin-left: 2px;" visible="true">
           <div id="divDatosCliente" runat="server" visible="true">
            <label id="lblNombreNumeroCliente" runat="server" style="font-weight: bold; font-size: medium;">
                Nombre o numero de cliente</label><br id="brNombreNumeroCliente" runat="server" />
            <asp:TextBox ID="nomcliente" runat="server" Width="80%"></asp:TextBox>
            <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="80%" runat="server" OnSelectedIndexChanged="LstClientes_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:Button ID="BtnCliente" runat="server" Text="..." OnClick="BCliente_Click" Width="8%" />
            <br id="brBtnCliente" runat="server" />
            
          <!--  <asp:Label runat="server" ID="labelDirectivo" Text='' style="font-weight: bold; font-size:small"></asp:Label>
            <br id="brlabelDirectivo" runat="server"/>-->
            <table style="width: 300px;">
                <tr>
                    <td style="text-align: right;">
                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Nota Empleado" CssClass="Ocultarcontrol"
                            Enabled="False" Onclick="handleClickNotaCredito(this);" />
                    </td>
                    <td style="text-align: left; font-weight: bold">
                        <asp:RadioButtonList ID="RadioButtonlTipoPago" RepeatLayout="Flow" RepeatDirection="Horizontal" 
                            runat="server" CssClass="RellenoCarrito1" >
                            <asp:ListItem Value="9">Cliente 9&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="T">Tarjeta Bancaria</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
             <label id="lblAlerta" runat="server" 
                   style="font-weight: bold; font-size: small; color: #FF0000; "></label>
             <br />
            <label id="lblEmail" runat="server" style="font-weight: bold; font-size: small;">
                Email</label>
            <asp:TextBox ID="txtemail" runat="server" Width="60%"></asp:TextBox><br id="brEmail"
                runat="server" />
               <%--<asp:UpdatePanel runat="server" id="updPnlEntregaOtraUbicacion" UpdateMode="Always">
            <Triggers>
            <asp:PostBackTrigger ControlID="optEntregaOtraUbicacion" />
            </Triggers>
                <ContentTemplate>--%>
            <asp:label id="LabelNom" runat="server" style="font-weight: bold; font-size: small;" Text="Enviar a Nombre de"></asp:label>
            <asp:TextBox ID="txtNomCli" runat="server" Width="58%"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" Enabled="false" ID="rqfNombreCliente" ErrorMessage="*"
                Font-Bold="true" ControlToValidate="txtNomCli"> </asp:RequiredFieldValidator>
            <br id="br1"
                runat="server" />
            
          <!--  <label id="lblComentario" runat="server" style="font-weight: bold; font-size: small;">
                Comentario
            </label>-->
            <asp:TextBox ID="TxtComentarios" runat="server" Width="58%" ></asp:TextBox><br id="brComentarios"
                runat="server" />
               <%--<tr>
						<td>
							<asp:Label id="Label7" runat="server" Text="Datos de Facturacion" font-size="Medium" />
						</td>	
					</tr>--%>
            <br />
            <div id="divRequiereFactura" runat="server" visible="false">
                <asp:Label ID="lblFacturacion" runat="server" Text="Requiere Factura: "></asp:Label>
                <asp:RadioButton ID="rbFacturaSi" runat="server" GroupName="GrupoRequiereFactura"
                    Text="Si" OnCheckedChanged="rbFacturaSi_CheckedChanged" AutoPostBack="false" style="margin-left:30px; font-weight: bold;" />
                <asp:RadioButton ID="rbFacturaNo" runat="server" GroupName="GrupoRequiereFactura"
                    Text="No" OnCheckedChanged="rbFacturaNo_CheckedChanged" AutoPostBack="false" style="margin-left:30px; font-weight: bold;" />    
            </div>
           </div>
            <%--   <tr>
                        <td>
                            País *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUPaisFacturacion" Width="50%" MaxLength="10" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Celular*
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfCelularFacturacion" Width="90%" MaxLength="20"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUTelfCelularFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Fijo
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfFijoFacturacion" Width="90%" MaxLength="20"/>
                        </td>
                    </tr>--%><%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger  ControlID="optEntregaOtraUbicacion" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>--%>
             <div runat="server" id="divOtraObicacion" visible="false">
                <table style="width: 99%; font-weight: bold; font-size: 12px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 25%;">
                            <strong>Nombre *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNombre" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="reqtxtOUNombre" ErrorMessage="*" Font-Bold="true"
                                ControlToValidate="txtOUNombre"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Apellidos *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUApellidos" Width="90%"  MaxLength="100"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUApellidos"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Email *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUemail" Width="90%" MaxLength="255" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUemail"> </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="reqEmail" ControlToValidate="txtOUemail"
                                ErrorMessage="Error" ToolTip="Introduzca una dirección de email válida, por ejemplo: persona@dominio.com"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <strong>Dirección *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUDireccion" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUDireccion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Exterior *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoExterior" Width="50%" MaxLength="20" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUNoExterior"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Interior
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoInterior" Width="50%" MaxLength="20"/>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estado *
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cboOUEstado" Width="40%" DataSourceID="AVE_EstadosDataSource"
                                DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                                <asp:ListItem Text=" " Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ErrorMessage="Seleccione provincia"
                                Font-Bold="true" ControlToValidate="cboOUEstado" InitialValue ="0"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ciudad *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUCiudad" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCiudad"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Colonia *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUColonia" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUColonia"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Codigo Postal *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUCodigoPostal" Width="50%" MaxLength="10" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCodigoPostal"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Celular*
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfCelular" Width="90%" MaxLength="20"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUTelfCelular"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Fijo
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfFijo" Width="90%" MaxLength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Referencia de llegada
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUReferenciaLlegada" Width="90%" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:Label runat="server" ID="lblDatosRequeridos" Font-Size="Smaller" Text="(*)Datos Requeridos"></asp:Label><br /><br /></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button runat="server" CssClass="botonNavegacion" ID="cmdConfirmarEntrega" Text="Confirmar Entrega"
                                Width="40%" OnClick="cmdConfirmarEntrega_Click" />
                            <asp:Label runat="server" ID="lblEntregaConfirmada" Font-Bold="true" Text="Entrega Confirmada"
                                Visible="False"> </asp:Label>
                            &nbsp;
                            <asp:Button runat="server" CssClass="botonNavegacion" ID="btnReseteaEntrega" Text="Borrar Datos"
                                Width="40%" onclick="btnReseteaEntrega_Click" />
                        </td>
                    </tr>
                </table>
            </div>

           <div runat="server" id="divOtraObicacionFacturacion" visible="false">
                <div runat="server" id="divUbicacionFacturacion" visible="false">
                <table style="width: 99%; font-weight: bold; font-size: 12px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 25%;">
                            <strong>Nombre *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNombreF" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="reqtxtOUNombreFa" ErrorMessage="*" Font-Bold="true"
                                ControlToValidate="txtOUNombreF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Apellidos *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUApellidosF" Width="90%"  MaxLength="100"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUApellidosF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Email *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUemailF" Width="90%" MaxLength="255" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUemailF"> </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="reqEmailF" ControlToValidate="txtOUemail"
                                ErrorMessage="Error" ToolTip="Introduzca una dirección de email válida, por ejemplo: persona@dominio.com"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <strong>Dirección *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUDireccionF" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUDireccionF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Exterior *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoExteriorF" Width="50%" MaxLength="20" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUNoExteriorF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Interior
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoInteriorF" Width="50%" MaxLength="20"/>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estado *
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cboOUEstadoF" Width="40%" DataSourceID="AVE_EstadosDataSource"
                                DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                                <asp:ListItem Text=" " Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7F" ErrorMessage="Seleccione provincia"
                                Font-Bold="true" ControlToValidate="cboOUEstadoF" InitialValue ="0"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ciudad *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUCiudadF" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCiudadF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Colonia *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUColoniaF" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUColoniaF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Codigo Postal *
                        </td>
                        <td>
                            <asp:TextBox runat="server"  ID="txtOUCodigoPostalF" Width="50%" MaxLength="10" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCodigoPostalF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Celular*
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfCelularF" Width="90%" MaxLength="20" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10F" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUTelfCelularF"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telefono Fijo
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUTelfFijoF" Width="90%" MaxLength="20"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Referencia de llegada
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUReferenciaLlegadaF" Width="90%" MaxLength="100" />
                        </td>
                    </tr>
                    </table>
                    </div>
                <%-- MJM 24/02/2014 FIN --%>
                    <div id="divTextoFacturacion" style="width: 300px;text-decoration: underline; color: #EA2E8C; font-weight:bold">
                        <br/>
                        <br/>
                        <span style="font-size:Medium;width: 300px;">Datos de Facturacion</span>
                        <br />
                        <br />
                    </div>
                    <table style="width: 99%; font-weight: bold; font-size: 12px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 100%;" colspan="5">
                              <input value="si" id="radioDatosFacturacion" type="checkbox" onclick="javascript:copiarDatosFacturacion();">
                            <strong>Copiar datos de envio</strong>
                        </td>
                      
                    </tr>

					<tr>
                        <td style="width: 25%;">
                            <strong>Nombre/Razon Social *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNombreFacturacion" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="reqtxtOUNombreF" ErrorMessage="*" Font-Bold="true"
                                ControlToValidate="txtOUNombreFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>RFC *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOURfcFacturacion" Width="90%"  MaxLength="100" onchange="javascript:GetData( this );"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF1" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOURfcFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <strong>Dirección *</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUDireccionFacturacion" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF3" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUDireccionFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Exterior *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoExteriorFacturacion" Width="50%" MaxLength="20" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF5" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUNoExteriorFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Interior
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUNoInteriorFacturacion" Width="50%" MaxLength="20"/>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estado *
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cboOUEstadoFacturacion" Width="40%" DataSourceID="AVE_EstadosDataSource"
                                DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                                <asp:ListItem Text=" " Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF7" ErrorMessage="Seleccione provincia"
                                Font-Bold="true" ControlToValidate="cboOUEstadoFacturacion" InitialValue ="0"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ciudad *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUCiudadFacturacion" Width="90%" MaxLength="100" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF8" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCiudadFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Colonia *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUColoniaFacturacion" Width="90%" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF12" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUColoniaFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Codigo Postal *
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOUCodigoPostalFacturacion" Width="50%" MaxLength="10"  />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorF9" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUCodigoPostalFacturacion"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        <%-- <asp:RadioButton ID="Cliente9" runat="server" Text="Cliente 9" CssClass="RellenoCarrito" />
             <asp:RadioButton ID="Tarjeta" runat="server"  Text="Tarjeta MIT" CssClass="RellenoCarrito1" />--%>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:Label runat="server" ID="lblDatosRequeridosFacturacion" Font-Size="Smaller" Text="(*)Datos Requeridos"></asp:Label><br /><br /></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button runat="server" CssClass="botonNavegacion" ID="cmdConfirmarEntregaFacturacion" Text="Confirmar Entrega"
                                Width="40%" OnClick="cmdConfirmarEntregaFacturacion_Click" />
                            <asp:Label runat="server" ID="lblEntregaConfirmadaFacturacion" Font-Bold="true" Text="Entrega Confirmada"
                                Visible="False"> </asp:Label>
                            &nbsp;
                            <asp:Button runat="server" CssClass="botonNavegacion" ID="btnReseteaEntregaFacturacion" Text="Borrar Datos"
                                Width="40%" onclick="btnReseteaEntregaFacturacion_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <%-- BT no se usa
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] 
union
Select 0 as IdBanco,'' as descrip) BANCOS 
ORDER BY [IdBanco]"></asp:SqlDataSource>--%><%-- BT no se usa
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] 
union
Select 0 as IdBanco,'' as descrip) BANCOS 
ORDER BY [IdBanco]"></asp:SqlDataSource>--%>
        </div>
        <div style="display: none; border-style: solid; float: right; width: 45%; border-width: 1px">
            <label style="font-weight: bold; font-size: medium;" visible="false">
                Numero de Tarjeta Cliente</label><br />
            <asp:TextBox ID="TarjetaCliente" runat="server" Width="85%" Visible="false"></asp:TextBox>
            <asp:Button ID="ButClient" runat="server" Text="..." OnClick="BC9_Click" Width="8%"
                Visible="false" /><br />
            <%-- BT no se usa
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] 
union
Select 0 as IdBanco,'' as descrip) BANCOS 
ORDER BY [IdBanco]"></asp:SqlDataSource>--%>
        </div>
        <div runat="server" visible="false" id="CLiente" style="display: inline-block; border-style: solid;
            float: right; width: 45%; padding: 2px; border-width: 1px">
            <label style="font-weight: bold">
                Nombre</label>
            <asp:Label ID="Nombre" runat="server" value=""></asp:Label><br />
            <asp:Label ID="Email" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label><br />
            <asp:Label ID="Shoelover" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label>
        </div>
        <div runat="server" id="DivTajeta" style="display: none; border-style: solid; float: right;
            width: 45%; padding: 2px; border-width: 1px">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%">
                        <label style="font-weight: bold">
                            Seleciona MIT</label>
                    </td>
                    <!--td style="width:60%"><label style="font-weight: bold">Seleciona Tarjeta</label></td-->
                    <td style="width: 20%">
                        <label style="font-weight: bold">
                            Plazo</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="lstTipoMIT" runat="server" Width="100%">
                            <asp:ListItem Value="VMC" Text="V/MC"></asp:ListItem>
                            <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <!--td>
                    <asp:DropDownList ID="lstTarjetas" runat="server" Width="100%"
                    DataSourceID="AVE_BANCOS" DataTextField="descrip" DataValueField="IdBanco">
                    </asp:DropDownList>
                </td-->
                    <td>
                        <asp:DropDownList runat="server" ID="cboPlazoNormal" AutoPostBack="false" DataSourceID="dsPlazoNormal"
                            DataTextField="Meses" DataValueField="Mercha" CssClass="cboPlazoNormal">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="cboPlazoAmex" AutoPostBack="false" DataSourceID="dsPlazoAmex"
                            DataTextField="Meses" DataValueField="Mercha" CssClass="cboPlazoAmex">
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="dsPlazoNormal" SelectCommand="select * from tblpromos where meses NOT like '%AMEX_%' order by idpromocion"
                            ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"></asp:SqlDataSource>
                        <asp:SqlDataSource runat="server" ID="dsPlazoAmex" SelectCommand="select idPromocion, RIGHT(Meses, LEN(meses)-5) as Meses, Mercha from tblpromos where meses like '%AMEX_%' order by idpromocion"
                            ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="Divpuntos" style="display: none; width: 45%; padding: 2px;
            display: none; clear: right; float: right; border-width: 1px; border-style: solid">
            <table border="2" cellpadding="1" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <label style="font-weight: bold">
                            Beneficios</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="RadioButton2" runat="server" Text="Puntos 9" Onclick="ClickCliente9(this);this.Checked=true;" />
                    </td>
                    <td>
                        <asp:Label ID="Label10" Style="float: right" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton3" runat="server" Text="Pares Acumulados" Onclick="ClickPar9(this);" />
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="0(0)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20%">
                        <asp:Label Style="float: right" ID="Label11" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton4" runat="server" Text="Bolsas Acumuladas" Onclick="ClickBolsa9(this);" />
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="0(0)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style2">
                        <asp:Label ID="Label12" Style="float: right" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="DivPagar" style="display: none; width: 45%; padding: 2px; clear: right;
            float: right; border-width: 1px; border-style: solid">
            <table style="width: 100%">
                <tr>
                    <td style="width: 25%">
                        <label>
                            Importe:</label>
                    </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtPago" Style="text-align: right;" runat="server" Width="100px" onkeypress="return IsNumber(event,this);"></asp:TextBox>
                    </td>
                    <td style="width: 25%">
                        <input id="ButPagar" type="button" style="width: 100%" value="Pagar" onclick="return ValidarImporte()" />
                        <asp:Button ID="BtnPagar" Style="display: none" runat="server" Text="Pagar" OnClick="ButPagar_Click" />
                    </td>
                    <td style="width: 20%">
                        <input id="BtnCancelar" type="button" style="width: 100%" value="Cancelar" onclick="return ValidarCancelPago()" />
                        <asp:Button ID="ButCancelarPago" Style="display: none" runat="server" Text="Cancelar"
                            OnClick="ButCancelarPago_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br clear="all" />
    <div id="divTiendasEnvio" class= "divTiendasEnvio" visible= "false" runat="server" style="width: 80%;">
        <br />
            <div class="divTiendasEnvio-content">
                <div style="text-align: center">
                    <h2>
                        Seleccione una tienda</h2>
                </div>
                <div>
                    <asp:CustomValidator ID="cvTiendaSeleccionada" runat="server" ErrorMessage="obligatorio seleccionar una tienda"
                        SetFocusOnError="True" ValidationGroup="validationiGroupSeleccionarTienda" Display="Dynamic"
                        Text="Debe seleccionar una tienda si desea envío a tienda" OnServerValidate="cvTiendaSeleccionada_ServerValidate"> </asp:CustomValidator>
                    <asp:GridView ID="gvTiendasEnvio" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="4" Font-Names="Verdana" BorderColor="White" 
                        BorderStyle="None" Width="100%"
                        DataKeyNames="IdTienda, observaciones" 
                        onselectedindexchanged="gvTiendasEnvio_SelectedIndexChanged" >
                        <EditRowStyle BorderStyle="None" />
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tienda">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbTienda" runat="server" value='<%# Eval("IdTienda") %>' Text='<%# Eval("Observaciones") %>'
                                        GroupName="grupoTiendasEnvio" AutoPostBack="true" OnCheckedChanged="rbTienda_CheckedChanged" />
                                        <asp:Label ID="nomTienda" runat="server" Text='<%# Eval("Observaciones") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle Width="25%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Localidad" HeaderText="Ciudad" HtmlEncode="false" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Direccion" HeaderText="Direccion" ItemStyle-Width="50%">
                                <ItemStyle Width="50%"></ItemStyle>
                            </asp:BoundField>
                             
                        </Columns>
                        <RowStyle BackColor="White" ForeColor="Black" BorderStyle="None" />
                        <SelectedRowStyle BackColor="#EA2E8C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
                <br />
                <div id="divCerrarPopUp">
                    <asp:Button ID="btnCerrarPopUp" runat="server" CssClass="botonNavegacion" 
                        Width="100px" onclick="btnCerrarPopUp_Click" Text="Cerrar" style="float:right"/>
                </div>
            </div>
            <div class="divTiendasEnvio-overlay">
            </div>        
    </div>
    <div id="divBotonesNavegacion" style="width: 100%;">
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Button runat="server" ID="BtnSeguirComprando" Width="200px" Text="SEGUIR COMPRANDO"
                        OnClick="BtnSeguirComprando_Click" CssClass="botonNavegacion" />
                </td>
                <td align="center">
                </td>
                <td align="right">
                    <asp:Button runat="server" ID="BtnContinuaPago" CssClass="botonNavegacion" Width="150px"
                        Text="CONTINUAR" OnClick="BtnContinuaPago_Click" />
                    <asp:Button runat="server" ID="BtnFinalizaVenta" CssClass="botonNavegacion" Visible="false"
                        Width="150px" Text="CONTINUAR" OnClick="ButFinalizarVenta_Click" ValidationGroup="validationiGroupSeleccionarTienda"
                        CausesValidation="true" />
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" visible="false" id="Divfinalizar" style="border-style: solid;
        float: right; width: 45%; padding: 2px; border-width: 1px">
        <asp:Button ID="ButFinalizarVenta" runat="server" Text="Finalizar Venta" CssClass="RellenoCarrito2"
            OnClick="ButFinalizarVenta_Click" />
        <input id="Button1" type="button" style="width: 100%" value="Cancelar" onclick="return ValidarCancelPago()" />
        <asp:Button ID="Button2" Style="display: none" runat="server" Text="Cancelar" OnClick="ButCancelarPago_Click" />
    </div>
    <asp:SqlDataSource ID="AVE_ArticuloFotoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure"
        DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_BANCOS" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] union
                      Select 0 as IdBanco,'' as descrip) BANCOS ORDER BY [IdBanco]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_CarritoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_dameDetalleCarrito" SelectCommandType="StoredProcedure"
        DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdCarrito" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_CarritoEliminar" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        DeleteCommand="dbo.AVE_eliminaLineaCarrito" DeleteCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="IdLineaCarrito" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="EnviaPOS" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="AVE_EnviaPOS" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IdTerminal" Type="String" />
            <asp:Parameter Name="IdTienda" Type="String" />
            <asp:Parameter Name="IdCarrito" Type="Int32" />
            <asp:Parameter Name="IdUsuario" Type="String" />
            <asp:Parameter Name="dPrecio" Type="Double" />
            <asp:Parameter Name="strComentario" Type="String" />
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
    <%-- BT no se usa
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] 
union
Select 0 as IdBanco,'' as descrip) BANCOS 
ORDER BY [IdBanco]"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="AVE_EstadosDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="SELECT * FROM [N_PROVINCIAS]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCodigoAlfaArticulo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="select top 1 codigoalfa from articulos where idarticulo = @idArticulo">
        <SelectParameters>
            <asp:Parameter Name="idArticulo" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
