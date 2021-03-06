﻿<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true" CodeBehind="CarritoDetalle_back.aspx.cs" Inherits="AVE.CarritoDetalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div style="display: none">
        <form id="scannCarr" action="sendScanReader" name="sendScanReader" method="post">
            <!-- Este action para hacer la llamada al lector -->
            <input type="hidden" name="dataType" id="dataType" value="numeric" /><!-- Tipo de dato que quieres leer, puede ser numeric o alfanumeric -->
            <input type="hidden" name="dataLength" id="dataLength" value="100" /><!-- Tamaño del dato que quieres leer-->
            <input type="hidden" name="inputTextNameToReturnData" id="inputTextNameToReturnData" value="txtArticulo" /><!-- input donde quieres que se escriba la respuesta del lector-->
            <input name="sbtEscanear" type="button" value="Escanear" onclick="submitDetailsForm()" id="sbtEscanear" />
        </form>
    </div>
    <style type="text/css">
        .fallback {
            background-image: url("img/noImagen.jpg");
        }

        .borderBottom {
            border-bottom: solid 1px black;
        }

        .style2 {
            width: 262px;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 350px;
            height: 400px;
        }
    </style>

    <%--<script src="js/jquery-1.2.6.min.js" type="text/javascript"></script>--%>
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>

    <script src="js/CarritoDetalle.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {

            habilitaDivPagos();
        }
        function mostrar_procesar() {
            document.getElementById('procesando_div').style.display = "";
            setTimeout('document.images["procesando_gif"].src="img/ajax-loader.gif"', 200);
        }
        function rmPagAll() {

            $("#ctl00_ContentPlaceHolder1_tipPagos option[value='N']").remove();
            $("#ctl00_ContentPlaceHolder1_tipPagos option[value='9']").remove();

        }
        function rmPagEfectivo() {

            $("#ctl00_ContentPlaceHolder1_tipPagos option[value='E']").remove();

        }
        function rmInsPagEmpleado(option) {

            if (option == 0) {
                //    $("#ctl00_ContentPlaceHolder1_tipPagos option[value='N']").remove();
                //$("#ctl00_ContentPlaceHolder1_tipPagos").append('<option value="9">Cliente 9</option>');       
            }
            else {
                //   $("#ctl00_ContentPlaceHolder1_tipPagos").append('<option value="N">Nota Empleado</option>');


                habilitaDivPagos();
            }
        }
        function rmInsPagNine(option) {


            if (option == 0) {
                //   $("#ctl00_ContentPlaceHolder1_tipPagos option[value='9']").remove();

            }
            else {
                // $("#ctl00_ContentPlaceHolder1_tipPagos").append('<option value="9">Cliente 9</option>');

                habilitaDivPagos();
            }
        }
        /* function OcultaItem(item) {
             debugger;
            
             if (item == 'item2')  $('#item2').hide();
                
             else if (item == 'item3')  $('#item3').toggle();
             else {
              $('#item2').toggle();
               $('#item3').toggle();
             }
 
         }*/


        var nav4 = window.Event ? true : false;
        function IsNumber(evt, obj) {
            // Backspace = 8, Enter = 13, ’0′ = 48, ’9′ = 57, ‘.’ = 46,","=44
            var key = nav4 ? evt.which : evt.keyCode;
            var text = obj.value;


            if (text.indexOf(",") > 0 || text.indexOf(".") > 0) {
                if (key == 46 || key == 44) {
                    key = 45;
                }
            }

            return (key <= 13 || (key >= 48 && key <= 57) || key == 46 || key == 44);
        }
        function inhabilitaBoton() {
            $('#ctl00_ContentPlaceHolder1_ButFinalizarVenta').prop('disabled', true);
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
        //acl, cuando se pulsa en la imagen de cliente nine, rellena el cliente con el prefijo del cliente nine
        $('#ctl00_ContentPlaceHolder1_tipPagos').on("change", function () { alert("cambio el select"); });
        function rellenaNine() {
            $('#ctl00_ContentPlaceHolder1_nomcliente').val('22114005');
        }
        function SelNine() {

            $('#ctl00_ContentPlaceHolder1_hiddenOptPago').val('9');
            $('#ctl00_ContentPlaceHolder1_Pendiente').css('height', '20px');
            $('#ctl00_ContentPlaceHolder1_Label17').hide();
            $('#ctl00_ContentPlaceHolder1_TDevolucion').hide();
            var elp = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            elp.style.display = 'none';
            var el1 = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            var el = document.getElementById('DivPagar');
            el1.style.display = 'inline-block';
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");

            //importe.value = "0.00";
            importe.value = "0";
            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
            //Si no ha habido un pago, se habilita, en caso contrario se inhabilita

            if ($('#ctl00_ContentPlaceHolder1_RepeaterPago_ctl01_LabImporte').html() != '$0.00') {
                $('#BtnCancelar').prop('disabled', false);
                $('#Button1').prop('disabled', false);
            }
            else {
                $('#BtnCancelar').prop('disabled', true);
            }


        }

        function SelEmpleado() {
            $('#ctl00_ContentPlaceHolder1_hiddenOptPago').val('N');
            $('#ctl00_ContentPlaceHolder1_Pendiente').css('height', '20px');
            $('#ctl00_ContentPlaceHolder1_Label17').hide();
            $('#ctl00_ContentPlaceHolder1_TDevolucion').hide();
            var el = document.getElementById('DivPagar');
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

            //importe.value = pagar.innerHTML.replace("$", "");

            MostrarImporteAPagar();

            document.getElementById("ctl00_ContentPlaceHolder1_txtPago").focus();


            var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            elp.style.display = 'none';
            elp = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            elp.style.display = 'none';

            $('#BtnCancelar').prop('disabled', true);
            $('#Button1').prop('disabled', true);
        }
        function SelImporte() {
            $('#ctl00_ContentPlaceHolder1_hiddenOptPago').val('E');
            $('#ctl00_ContentPlaceHolder1_Pendiente').css('height', '30px');
            $('#ctl00_ContentPlaceHolder1_Label17').show();
            $('#ctl00_ContentPlaceHolder1_TDevolucion').show();
            var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            elp.style.display = 'none';
            var el1 = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            el1.style.display = 'none';
            var el = document.getElementById('DivPagar');
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

            //importe.value = pagar.innerHTML.replace("$", "");
            MostrarImporteAPagar();
        }
        function SelTarjeta() {
            $('#ctl00_ContentPlaceHolder1_hiddenOptPago').val('T');
            //div de tarjeta
            bAlgunoPulsado = true;
            $('#ctl00_ContentPlaceHolder1_Pendiente').css('height', '20px');
            $('#ctl00_ContentPlaceHolder1_Label17').hide();
            $('#ctl00_ContentPlaceHolder1_TDevolucion').hide();
            var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            elp.style.display = 'none';
            var el1 = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            var el = document.getElementById('DivPagar');
            el1.style.display = 'inline-block';
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

            //importe.value = pagar.innerHTML.replace("$", "");
            MostrarImporteAPagar();

            //ACL. Lo comento, ya que he quitado de momento el combo de bancos o tarjetas.
            // document.getElementById("ctl00_ContentPlaceHolder1_lstTarjetas").focus();

            $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);
            $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
        }
        function doHourglass() {
            document.body.style.cursor = 'wait';
        }
        function habilitaDivPagos() {
            var items = 3;

            var selected = 0;
            var rbl = document.getElementById('ctl00_ContentPlaceHolder1_tipPagos');
            items = rbl.options.length;

            for (i = 0; i < items; i++) {
                if (rbl.options[i].selected == true) {
                    selected = rbl.options[i].value;
                }
            }
            if (selected == 'T') SelTarjeta();
            else if (selected == 'E') { SelImporte() }
            else if (selected == 'N') SelEmpleado();
            else if (selected == '9') SelNine();

        }

        function submitDetailsForm() {

            $("#scannCarr").submit();
        }
        function LanzaScanner() {

            $('input#sbtEscanear').trigger('click');

        }


        // 

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajax:ToolkitScriptManager ID="script1" runat="server"></ajax:ToolkitScriptManager>
    <%--<asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>--%>


    <div class="row" id="divDetalle" runat="server">
        <asp:HiddenField ID="HiddenPromo" runat="server" />
        <div class="container">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label ID="lblHead" Font-Size="Larger" Font-Bold="true" runat="server" Text='<%$ Resources:Resource, ResumenCompra %>' />
                    <asp:HiddenField runat="server" ID="hidIdCliente" />
                    <asp:HiddenField runat="server" ID="hiddenOptPago" />
                    <asp:HiddenField runat="server" ID="hidNumeroTarjetaCliente9" />
                </div>

                        <div id="gvCarritoDiv">
                    <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="false" Style="width: 99%" GridLines="None" OnRowDataBound="gvCarrito_RowDataBound" PageSize="2" OnRowCreated="gvCarrito_RowCreated" OnRowCommand="gvCarrito_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="borderBottom" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" Style="width: 25px; height: 25px" ID="imgBorrar" ImageUrl="~/img/Remove.png" CommandName="Borrar" CommandArgument='<%# Eval("id_carrito_detalle") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="borderBottom" />
                                <ItemTemplate>
                                    <asp:Image ID="FotoArticulo" CssClass="fallback" onerror="this.src='~/img/noImagen.jpg'" ImageUrl='<%# ObtenerURL(Eval("idTemporada").ToString(), Eval("idProveedor").ToString(), Eval("ModeloProveedor").ToString()) %>' runat="server" Style="max-width: 150px; max-height: 100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle CssClass="borderBottom" Width="35%" />
                                <ItemTemplate>
                                    <div style="width: 100%">
                                        <div style="float: left; width: 30%; font-weight: bold; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label78" Text='<%$ Resources:Resource, Modelo%>'></asp:Label>:
                                        </div>
                                        <div style="float: left; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblModelo" Text='<%#Bind("ModeloProveedor") %>'></asp:Label>
                                        </div>
                                        <br />
                                        <div style="width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label3" Text='<%$ Resources:Resource, Color%>'></asp:Label>:
                                        </div>
                                        <div style="float: left; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblColor" Text='<%#Bind("Color") %>'></asp:Label>
                                        </div>
                                        <br />
                                        <div style="width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label15" Text="Codigo SAP: "></asp:Label>
                                        </div>
                                        <div style="float: left; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label16" Text='<%# CodigoAlfaDesdeIdArticulo(Eval("IdArticulo")) %>'></asp:Label>
                                        </div>
                                        <br />
                                        <div style="clear: left; width: 30%; float: left; font-weight: bold; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label4" Text='<%$ Resources:Resource, Talla%>'></asp:Label>
                                        </div>
                                        <div style="float: left; font-size: 12px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblNombreTalla" Text='<%#Bind("Nombre_Talla") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <asp:Panel Style="width: 90%; text-align: center; font-size: 14px" runat="server" ID="divforanea">
                                        <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label1" Text='<%$ Resources:Resource, SolicitudForanea%>'></asp:Label>:
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <%--                                <HeaderStyle Font-Bold="true" />
                                <HeaderTemplate>
                                    <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>
                                </HeaderTemplate>--%>
                                <ItemStyle CssClass="borderBottom" Width="40%" />
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center">
                                        <div style="float: left; width: 110px; text-align: right; margin-right: 15px; margin-left: 3%;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>:
                                        </div>
                                        <div style="float: left; text-align: right; width: 70px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblPrecioOriginal" Text='<%# FormateaNumero(Eval("PVPORI").ToString())%>'></asp:Label>
                                        </div>
                                        <br />
                                        <div runat="server" id="laDescuento" style="width: 110px; float: left; text-align: right; margin-right: 15px; margin-left: 3%;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label5" Text="DESC:"></asp:Label>
                                        </div>
                                        <div runat="server" id="laImporteDescuento" style="float: left; text-align: right; width: 70px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblImporteDTO" Text='<%# "-" + FormateaNumero(Eval("DTOArticulo").ToString()) %>'></asp:Label>
                                        </div>
                                        <div runat="server" id="PorcDescuento" style="float: left; text-align: right; width: 70px;">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblPorDescuento" Text=''></asp:Label>
                                        </div>
                                        <asp:Label Style="text-transform:uppercase"  ID="lblPromobr" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Panel ID="divPromocionBotas" Visible="false" runat="server" Style="clear: left">
                                            <div style="width: 110px; text-align: right; margin-right: 15px; margin-left: 3%; float: left">
                                                <asp:Label Style="text-transform:uppercase"  runat="server" ID="DescriPromo" Text=""></asp:Label>
                                            </div>
                                            <div runat="server" id="divPromoVentas" style="float: left; text-align: right; width: 70px;">
                                                <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblPromocionBotas" Text='<%# "-" + FormateaNumero(Eval("DtoPromo").ToString()) %>'></asp:Label>
                                                <asp:DropDownList ID="ddlSelecionarPromocion" runat="server" Visible="false" Width="180px" Style="padding-left: 10px;" OnSelectedIndexChanged="ddlSelecionarPromocion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <div runat="server" id="divDescuentoPromocion" visible="false" style="float: left; text-align: right; width: 70px;">
                                                <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblPorcentajeDescuento"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <div style="clear: left; float: left; width: 110px; text-align: right; margin-right: 15px; margin-left: 3%; color:red">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label6" Text='<%$ Resources:Resource, A_pagar%>'></asp:Label>:
                                        </div>
                                        <div style="float: left; text-align: right; width: 70px; color:red">
                                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblPagar" Text=''></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="borderBottom" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" Style="width: 25px; height: 25px" ID="imgEnvio" ImageUrl="~/img/solicitudes.png" OnClick="imgEnvio_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div runat="server" id="Total" style="margin-left: 48%;">
                        <div style="float: left; width: 150px; text-align: right;">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label8" Text='<%$ Resources:Resource, SubTotal%>'></asp:Label>:
                        </div>
                        <div style="float: left; width: 90px; text-align: right; padding-left: 8PX">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblSubTotal" Text=''></asp:Label>
                        </div>
                        <asp:Label Style="text-transform:uppercase"  runat="server" ID="Descuentobr" Text="<br />"></asp:Label>
                        <div runat="server" id="divEtiqDescuento" style="float: left; width: 150px; text-align: right;">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label5" Text='<%$ Resources:Resource, Descuentos%>'></asp:Label>:
                        </div>
                        <div runat="server" id="divImporteDescuento" style="float: left; width: 90px; text-align: right; padding-left: 8PX">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblImporteDTOs" Text=''></asp:Label>
                        </div>
                        <br />
                        <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold;">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label7" Text='<%$ Resources:Resource, Total%>'></asp:Label>:
                        </div>
                        <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px; padding-left: 8PX">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="lblTotalPagar" Text=''></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div runat="server" id="resumenPago" style="margin-left: 48%;">
                        <asp:Repeater ID="RepeaterPago" runat="server">
                            <HeaderTemplate>
                                <table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 150px; text-align: right;">
                                        <asp:Label runat="server" ID="LabPAgo" Style='<%#DataBinder.Eval(Container.DataItem, "Tipo").ToString()=="Total Pagado:" ? "width: 150px; text-transform:uppercase; text-align: right; font-weight: bold; font-size: 12px;": "width: 150px; text-align: right;"%>' Text='<%# Eval("Tipo") %>' />
                                    </td>
                                    <td style="width: 90px; text-align: right">
                                        <asp:Label runat="server" ID="LabImporte" Style=" text-transform:uppercase; width: 90px; text-align: right; font-weight: bold; font-size: 12px;" Text='<%#FormateaNumero(Eval("Importe").ToString()) %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div runat="server" id="Pendiente" style="margin-left: 48%;">
                        <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold; padding-left: 8PX">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label19" Text='Total por Pagar'></asp:Label>:
                        </div>
                        <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px;">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="TotPendiente" Text=''></asp:Label>
                        </div>
                        <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold; padding-left: 8PX">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="Label17" Text='A Devolver:'><br /><br /><br /></asp:Label>
                        </div>
                        <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px;">
                            <asp:Label Style="text-transform:uppercase"  runat="server" ID="TDevolucion" Text='   '><br /><br /><br /></asp:Label>
                            <br />
                            <br />
                            <br />
                        </div>
                        <div>&nbsp;</div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>

    <center>
        <div class="row" runat="server">
        <div class="container">
            <div runat="server" id="divenviaPos">
                        <table>
                            <tr>
                                <td style="width:20%">
                                    <asp:Button ID="btnRegresar" runat="server" Text="Seguir Comprando" Width="155px" OnClick="btnRegresar_Click" CssClass="btn btn-block" />
                                </td>
                                <td style="width: 5%;">&nbsp;</td>
                                <td style="width: 20%;">
                                    <asp:Button ID="btnEnviarPOS" runat="server" Text='<%$ Resources:Resource, EnviarPOS%>' Width="125px" OnClientClick="SetSource(this.id)" OnClick="btnEnviarPOS_Click" CssClass="btn btn-info" />&nbsp;
                                </td>
                                <td style="width: 5%;">&nbsp;</td>
                                <td style="width: 20%;">
                                    <asp:Button ID="showPago" runat="server" Text="Pagar" Width="125px" OnClick="bntShowPago_Click" CssClass="btn btn-primary" />&nbsp;
                                </td>
                                <td style="width: 5%;">&nbsp;</td>
                                <td style="width: 20%;">
                                    <asp:Button runat="server" ID="btnBorrarCarrito" Text="Cancelar Venta" Width="125px" OnClientClick="return confirm('¿ Desea eliminar todos los productos del carrito ?\n\n RECUERDE QUE SI TIENE ALGUN PAGO BANCARIO EN ESTE CARRITO DEBE REALIZAR LA DEVOLUCION.');" OnClick="btnBorrarCarrito_Click" CssClass="btn btn-danger" />&nbsp;
                                </td>
                                <td style="width: 5%;">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
            <div style="margin-top: 10px" id="divResumen" runat="server" visible="false">
                        <br />
                        <br />
                        <div class="ui-widget-overlay">
                        </div>
                        <div class="ui-widget-shadow" style="width: 300px; height: 200px; position: absolute; left: 51%; top: 51%; margin: -100px 0 0 -150px;">
                        </div>
                        <div style="width: 300px; height: 200px; position: absolute; left: 50%; top: 50%; margin: -100px 0 0 -150px;">
                            <%-- MJM 24/02/2014 FIN --%>
                            <div class="ui-widget-content" style="text-align: center; height: 100%; font-size: 14px">
                                <%-- %> <asp:RadioButton ID="RadioButton1" runat="server"  Text="Nota Empleado"  CssClass="Ocultarcontrol" Enabled="False" Onclick="handleClickNotaCredito(this);" />--%>
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
                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="btn btn-info" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
            <asp:Panel ID="ModalPanel" runat="server" class="modalPopup" Visible="False">
                        Se esta ejecutando un proceso de consulta/actualización. Espere a que termine, para continuar con su tarea.<br />
                    </asp:Panel>
        </div>
    </div>    
     </center> 

    <asp:Button runat="server" ID="bntpopup" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="bntpopup" PopupControlID="PanelEntregasArt" BackgroundCssClass="modalBackground"></ajax:ModalPopupExtender>
    <br />
    <asp:Button runat="server" ID="btnProcess" Style="display: none" CssClass="btn btn-info" />
    <ajax:ModalPopupExtender ID="ModalPopupExtenderProcess" runat="server" TargetControlID="btnProcess" PopupControlID="ModalPanel" BackgroundCssClass="modalBackground"></ajax:ModalPopupExtender>
    <br />
    <div class="row" style="text-align: center">
        <div>
            <span id="procesando_div" style="display: none; position: absolute; text-align: center">
                <img src='' id='procesando_gif' align='center' alt='' style="width: 50px; height: 50px;" />
            </span>
        </div>
    </div>
    <div class="row">
        <div id="divPnlPagoCte" runat="server" style="display: none;" class="container">
            <div runat="server" id="divPagos" visible="true">
                <div class="col-sm-12 col-md-12">
                    <div class="panel-primary" style="margin-top: 2px; border-style: solid; border-width: 1px; margin-left: 2px;" id="divcliente" visible="true">
                        <div class="panel-heading">
                            <label class="header-label" style="font-weight: bold; font-size: medium;">NOMBRE O NO. DE CTE.</label>
                        </div>
                        <br />
                        <asp:TextBox ID="nomcliente" runat="server" Width="80%"></asp:TextBox>
                        <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="80%" runat="server"
                            OnSelectedIndexChanged="LstClientes_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>


                        <asp:Button ID="BtnCliente" runat="server" Text="..." OnClick="BCliente_Click" OnClientClick="mostrar_procesar();" CssClass="btn btn-toolbar" />
                        <asp:ImageButton runat="server" ID="imgCliente9" Width="5%" ImageUrl="~/img/c9.png" OnClientClick="rellenaNine();return false;" /><br />
                        <br />
                        <asp:Label runat="server" ID="labelDirectivo" Text='' Style="font-weight: bold; font-size: small"></asp:Label><br />
                        <div class="panel-body">
                            <table>
                                <tr>
                                    <td style="text-align: right;">
                                        <label style="font-weight: bold; font-size: small;">Forma de Pago:</label>&nbsp;
                                        <asp:DropDownList ID="tipPagos" runat="server">
                                            <asp:ListItem Value="T">Tarjeta Bancaria</asp:ListItem>
                                            <asp:ListItem Value="E">En Efectivo</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <div runat="server" visible="false" id="CLiente" style="display: inline-block; border-style: solid; float: right; padding: 2px; border-width: 1px">
                                <label style="font-weight: bold">Nombre</label>
                                <asp:Label ID="Nombre" runat="server" value=""></asp:Label><br />
                                <asp:Label ID="Email" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label><br />
                                <asp:Label ID="Shoelover" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label>
                            </div>
                            <div runat="server" visible="false" id="Divfinalizar" style="border-style: solid; padding: 2px; border-width: 1px">

                                <button type="button" class="btn btn-success" style="width: 75%"
                                    onclick="this.disabled='disabled';document.getElementById('<%= btn.ClientID %>').click();">
                                    Finalizar Venta</button>
                                <asp:Button ID="btn" runat="server" CssClass="btn btn-info" OnClick="ButFinalizarVenta_Click" Style="display: none;" />
                                <!--input id="Button1" type="button" style="width:100%" disabled  value="Cancelar" onclick="return ValidarCancelPago()"   /-->
                                <asp:Button ID="Button2" runat="server" Enabled="false" CssClass="btn btn-danger" Style="width: 75%" Text="Cancelar" OnClick="ButCancelarPago_Click" />
                            </div>
                            <table>
                                <tr>
                                    <td style="width:45%;">
                                        <div runat="server" id="DivTajeta" style="display: none; border-style: solid; border-width: 1px; width:100%" class="panel panel-primary">
                                            <div runat="server" class="panel-heading" style="font-size: larger; font-weight: bold;">Pago con TC </div>
                                            <div class="panel-body" style="width:100%">
                                                <table>
                                                <tr>
                                                    <td style="width:45%; text-align:center;">
                                                        <label style="font-weight: bold">Seleciona MIT</label>
                                                    </td>
                                                    <td style="width:3%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align:center; width:45%;">
                                                        <label style="font-weight: bold">Plazo</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:45%">
                                                        <asp:DropDownList ID="lstTipoMIT" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="lstTipoMIT_SelectedIndexChanged">
                                                            <asp:ListItem Value="VMC" Text="V/MC"></asp:ListItem>
                                                            <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width:3%;">&nbsp;</td>
                                                    <td style="width:45%;">
                                                        <asp:DropDownList runat="server" ID="cboPlazoNormal" AutoPostBack="false" DataSourceID="dsPlazoNormal"
                                                DataTextField="Meses" DataValueField="Mercha" Width="100%" CssClass="form-control cboPlazoNormal" OnSelectedIndexChanged="cboPlazoNormal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                                        <asp:DropDownList runat="server" ID="cboPlazoAmex" AutoPostBack="false" DataSourceID="dsPlazoAmex"
                                                DataTextField="Meses" DataValueField="Mercha" Width="100%" CssClass="form-control cboPlazoAmex" OnSelectedIndexChanged="cboPlazoAmex_SelectedIndexChanged">
                                            </asp:DropDownList>
                                                        <asp:SqlDataSource runat="server" ID="dsPlazoNormal"
                                                SelectCommand="select * from tblpromos where meses NOT like '%AMEX_%' order by idpromocion"
                                                ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"></asp:SqlDataSource>
                                                        <asp:SqlDataSource runat="server" ID="dsPlazoAmex"
                                                SelectCommand="select idPromocion, RIGHT(Meses, LEN(meses)-5) as Meses, Mercha from tblpromos where meses like '%AMEX_%' order by idpromocion"
                                                ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="panel panel-primary" runat="server" id="Divpuntos" style=" width:100%;display: none; padding: 2px; display: none; border-width: 1px; border-style: solid">
                                            <div runat="server" class="panel-heading" style="font-size: larger; font-weight: bold;"> Beneficios </div>
                                            <div class="panel-body">
                                                <table border="1">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="RadioButton2" runat="server" Text="Puntos 9" Onclick="ClickCliente9(this);" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <table >
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
                                                    <td >
                                                        <asp:Label ID="Label11" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table >
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
                                                        <asp:Label ID="Label12" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width:1%;">
                                        &nbsp;
                                    </td>
                                    <td style="width:45%;">
                                        <div class="panel panel-primary" id="DivPagar" style="display: none; border-style: solid;border-width: 1px;">
    <div runat="server" class="panel-heading" style="font-size: larger; font-weight: bold;">Importe</div>
    <div class="panel-body" style="width:100%">
        <table >
            <tr>
                <td colspan="2">
                    <label style="font-weight:bold; color:darkgreen; text-transform:uppercase">Total ($): </label>
                </td>
            </tr>
		<tr>
            <td colspan="2">
                <asp:TextBox CssClass="form-control" ID="txtPago" Style="text-align: right;" Width="100%" runat="server" onkeypress="return IsNumber(event,this);"></asp:TextBox>
            </td>
            <td >
                <input id="ButPagar" type="button" class="btn btn-info" value="Pagar" onclick="return ValidarImporte()" />
                <asp:Button ID="BtnPagar" CssClass="btn btn-info" Style="display: none" runat="server" Text="Pagar" OnClick="ButPagar_Click2" OnClientClick="mostrar_procesar();" />
            </td>
            <td style="text-align:right">
                <input id="BtnCancelar" class="btn btn-danger" type="button" disabled value="Cancelar" onclick="return ValidarCancelPago()" />
                <asp:Button ID="ButCancelarPago" Style="display: none" runat="server" Text="Cancelar" OnClick="ButCancelarPago_Click" />
            </td>
        </tr>
    </table>
    </div>
</div>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <label style="font-weight: bold; font-size: small;">Email:</label>
                            <asp:TextBox CssClass="form-control" ID="txtemail" runat="server"  Width="80%"></asp:TextBox>
                            <br />

                            <div style="display: none; border-style: solid; width: 45%; border-width: 1px">
                                <label style="font-weight: bold; font-size: medium;" visible="false">Numero de Tarjeta Cliente</label><br />
                                <asp:TextBox CssClass="form-control" ID="TarjetaCliente" runat="server" Width="85%" Visible="false"></asp:TextBox>
                                <asp:Button ID="ButClient" runat="server" Text="..." OnClick="BC9_Click" Width="8%" Visible="false" /><br />
                            </div>

                            <label style="font-weight: bold; font-size: small;">Comentarios en Ticket: </label>
                            <asp:TextBox CssClass="form-control" ID="TxtComentarios" runat="server" Width="80%"></asp:TextBox><br />

                            <%-- MJM 24/02/2014 FIN --%>
                            <asp:UpdatePanel runat="server" ID="updPnlEntregaOtraUbicacion" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:CheckBox ID="optEntregaOtraUbicacion" runat="server"
                                        Text="Entrega en Otra Ubicación" CssClass="RellenoCarrito"
                                        OnCheckedChanged="optEntregaOtraUbicacion_CheckedChanged" AutoPostBack="true" />
                                    &nbsp;
                    
                    <asp:Panel runat="server" ID="pnlOtraObicacion" Visible="false">

                        <table style="width: 99%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <strong>Nombre</strong>
                                </td>
                                <td>* En atencion a quien se entrega
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtOUNombre" Width="90%" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqtxtOUNombre" ErrorMessage="*"
                                        Font-Bold="true" ControlToValidate="txtOUNombre">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Apellidos</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUApellidos" Width="90%" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*"
                                        Font-Bold="true" ControlToValidate="txtOUApellidos">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Email</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUEmail" Width="90%" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*"
                                        Font-Bold="true" ControlToValidate="txtOUEmail">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="reqEmail" ControlToValidate="txtOUEmail"
                                        ErrorMessage="Error"
                                        ToolTip="Introduzca una dirección de email válida, por ejemplo: persona@dominio.com"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Dirección</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUDireccion" Width="90%" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*"
                                        Font-Bold="true" ControlToValidate="txtOUDireccion">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Exterior</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUNoExterior" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Interior</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUNoInterior" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Estado</strong>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cboOUEstado" Width="90%"
                                        DataSourceID="AVE_EstadosDataSource" DataTextField="Nombre"
                                        DataValueField="Id" AppendDataBoundItems="True" CssClass="form-control">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Ciudad</strong>
                                </td>
                                <td>
                                    <br />
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUCiudad" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Colonia</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUColonia" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Codigo Postal</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUCodigoPostal" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Telefono Celular</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUTelfCelular" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Telefono Fijo</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUTelfFijo" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Referencia de llegada</strong>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtOUReferenciaLlegada" Width="90%" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="cmdConfirmarEntrega" Text="Confirmar Entrega" Width="90%" CssClass="btn btn-info" OnClick="cmdConfirmarEntrega_Click" /><br />
                                    <br />
                                    <asp:Label runat="server" ID="lblEntregaConfirmada" Text="Entrega Confirmada" Visible="False"> </asp:Label><br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                     <asp:Panel ID="PanelEntregasArt" runat="server" >
                                        <table style="width: 99%" cellpadding="0" cellspacing="0">
<tr>
    <td style="width: 25%">
        <strong>Nombre</strong>
    </td>
    <td>* Datos de Entrega Por Articulo
		<asp:TextBox CssClass="form-control" runat="server" ID="txtNombreArt" Width="90%" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNombreArt" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
            <td>
                <strong>Apellidos</strong>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtApellidosArt" Width="90%" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtApellidosArt" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>
                <strong>Email</strong>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmailArt" Width="90%" />

            </td>
        </tr>
        <tr>
            <td>
                <br><strong>Dirección</strong>
            </td>
            <td>
			<br/>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtDireccionArt" Width="90%" />
                <asp:RequiredFieldValidator ControlToValidate="txtDireccionArt" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><strong>No. Exterior</strong
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExteriorArt" Width="90%" />
                <asp:RequiredFieldValidator ControlToValidate="txtExteriorArt" ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><strong>No. Interior</strong>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtInteriorArt" Width="90%" /><br />
            </td>
        </tr>
        <tr>
            <td><strong>Estado</strong><br/>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ListEstadoArt" Width="90%" CssClass="form-control" 
                    DataSourceID="AVE_EstadosDataSource" DataTextField="Nombre"
                    DataValueField="Id" AppendDataBoundItems="True">
                    <asp:ListItem Text=" " Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="ListEstadoArt" ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator><br/>
            </td>
        </tr>
        <tr>
            <td>Ciudad
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCiudadArt" Width="90%" />
                <asp:RequiredFieldValidator ControlToValidate="txtCiudadArt" ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><strong>Colonia</strong>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtColoniaArt" Width="90%" />
                <asp:RequiredFieldValidator ControlToValidate="txtColoniaArt" ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="EnvioArt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><strong>Codigo Postal</strong><br/>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCpArt" Width="90%" /><br/>
            </td>
        </tr>
        <tr>
            <td><strong>Telefono Celular</strong><br/>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCelArt" Width="90%" /><br/>
            </td>
        </tr>
        <tr>
            <td><strong>Telefono Fijo</strong><br/>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtTelArt" Width="90%" /><br/>
            </td>
        </tr>
        <tr>
            <td><strong>Observaciones</strong>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtObservacionesArt" Width="90%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>

        </tr>
</table>
                                        <asp:Button ID="btnClose" runat="server" Text="Cerrar" Width="30%" OnClick="btnClose_Click" CssClass="btn btn-danger" />
                                        <asp:Button runat="server" ID="btnGuardarDir" Text="Guardar" Width="30%" OnClick="btnGuardarDir_Click" CssClass="btn btn-info" ValidationGroup="EnvioArt" />
                                        <asp:Button ID="btnBorrarEntrega" runat="server" Width="30%" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnBorrarEntrega_Click" />

                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="optEntregaOtraUbicacion" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1 col-md-1"></div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidSourceID" runat="server" />
    <asp:SqlDataSource ID="AVE_ArticuloFotoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure"
        DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_BANCOS" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
        SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] union
                      Select 0 as IdBanco,'' as descrip) BANCOS ORDER BY [IdBanco]"></asp:SqlDataSource>
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
    <asp:HiddenField ID="HiddenTipoCliente" runat="server" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>" SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] 
union
Select 0 as IdBanco,'' as descrip) BANCOS 
ORDER BY [IdBanco]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_EstadosDataSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="SELECT * FROM [N_PROVINCIAS]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCodigoAlfaArticulo" runat="server"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="select top 1 codigoalfa from articulos where idarticulo = @idArticulo">
        <SelectParameters>
            <asp:Parameter Name="idArticulo" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

