<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito7.aspx.cs" Inherits="AVE.Carrito7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .fallback
        {
            background-image: url("img/noImagen.jpg");
        }
        .borderBottom
        {
            border-bottom: solid 1px black;
        }
        .style2
        {
            width: 262px;
        }
    </style>
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/CarritoDetalle.js" type="text/javascript"></script>
    <script type="text/javascript" >

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <div style="margin-top: 10px" id="divDetalle" runat="server">
        <br />
<%--        <br />
        <div style="text-align: left;">
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Text='<%$ Resources:Resource, ResumenCompra%>'></asp:Label>
            <br />
        </div>
--%>            

    <asp:HiddenField runat="server" ID="hidIdCliente" />
        
            <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="False"
                Style="width: 99%" GridLines="None" 
                OnRowDataBound="gvCarrito_RowDataBound" PageSize="2" OnRowCommand="gvCarrito_RowCommand"
                OnRowCreated="gvCarrito_RowCreated" DataSourceID="AVE_CarritoObtener">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="borderBottom" />
                        <ItemTemplate>
                            <asp:ImageButton Style="width: 25px; height: 25px" ID="imgBorrar" ImageUrl="~/img/Remove.png"
                                CommandName="Borrar" runat="server" CommandArgument='<%# Eval("id_carrito_detalle") %>' />
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
                                    <div runat="server" id="divPromoVentas"  style="float: left; text-align: right; width: 70px;">
                                        <asp:Label runat="server" ID="lblPromocionBotas" Text='<%# "-" + FormateaNumero(Eval("DtoPromo").ToString()) %>'></asp:Label>
                                        <asp:DropDownList ID="ddlSelecionarPromocion" runat="server" Visible="false" Width="180px" style="padding-left:10px;" 
                                         OnSelectedIndexChanged="ddlSelecionarPromocion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
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
        <br />
        <div runat="server" id="Total" style="margin-left: 45%; width: 260px; border: solid 1px black;
            float: left">
            <div style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="Label8" Text='<%$ Resources:Resource, SubTotal%>'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right;padding-left:8PX ">
                <asp:Label runat="server" ID="lblSubTotal" Text=''></asp:Label>
            </div>
            <asp:Label runat="server"  ID="Descuentobr" Text="<br />"></asp:Label>  
            <div runat="server" id="divEtiqDescuento"  style="float: left; width: 150px; text-align: right;">
                <asp:Label runat="server" ID="Label5" Text='<%$ Resources:Resource, Descuentos%>'></asp:Label>:
            </div>
            <div runat="server" id="divImporteDescuento" style="float: left; width: 90px; text-align: right;padding-left:8PX">
                <asp:Label runat="server" ID="lblImporteDTOs" Text=''></asp:Label>
            </div>
            <br />
            <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold;">
                <asp:Label runat="server" ID="Label7" Text='<%$ Resources:Resource, Total%>'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px;padding-left:8PX">
                <asp:Label runat="server" ID="lblTotalPagar" Text=''></asp:Label>
            </div>
        </div>
        <div  runat="server" id="divenviaPos" style="margin-left: 10px; float: left; height: 55px; width: 100px;">
            <br />
            <br />
            <asp:Button ID="btnEnviarPOS" runat="server" Text='<%$ Resources:Resource, EnviarPOS%>'
                OnClientClick="SetSource(this.id)" OnClick="btnEnviarPOS_Click" />
        </div>
        <div runat="server" id="resumenPago" style="margin-left: 45%; width: 260px; height:100%; border: solid 1px black;float: left">
            <asp:Repeater ID="RepeaterPago" runat="server" EnableViewState="true">
            <HeaderTemplate>
              <table>
              </HeaderTemplate>
                  <ItemTemplate>
                     <tr>
                       <td style="width: 150px; text-align: right;">
                         <asp:Label runat="server" ID="LabPAgo" 
                         style='<%#DataBinder.Eval(Container.DataItem, "Tipo").ToString()=="Total Pagado:" ? "width: 150px; text-align: right; font-weight: bold; font-size: 12px;" :"width: 150px; text-align: right;"%>'
                         text='<%# Eval("Tipo") %>' /></td>
                      <td style="width: 90px; text-align: right">
                     <asp:Label runat="server" ID="LabImporte" 
                     style="width: 90px; text-align: right; font-weight: bold; font-size: 12px;"
                     text='<%#FormateaNumero(Eval("Importe").ToString()) %>' /></td>
                    </tr>
                  </ItemTemplate>
            <FooterTemplate>
               </table>
           </FooterTemplate> 
            </asp:Repeater>
        </div>
        <div runat="server" id="Pendiente" style="margin-left: 45%; width: 260px; height: 20px; border: solid 1px black;
            float: left">
             <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold;padding-left:8PX">
                <asp:Label runat="server" ID="Label19" Text='Total por Pagar'></asp:Label>:
            </div>
            <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px;">
                <asp:Label runat="server" ID="TotPendiente" Text=''></asp:Label>
            </div>
            <br />
        </div>
     
     <%--</div>--%>
      
    <div style="margin-top: 10px" id="divResumen" runat="server" visible="false">
        <br />
        <br />
        <div style="width: 300px; height: 200px; position: absolute; left: 50%; top: 50%;
            margin: -100px 0 0 -150px;">
            <div class="barraNavegacion" style="width: 100%; height: 24px; font-weight: bold;
                font-size: 16px">
                AVE</div>
            <div style="text-align: center; background-color: #FEF8B6; height: 100%; font-size: 14px">
                <div style="display: inline-block;">
                    <br />
                    Pedido enviado a POS con éxito
                    <br />
                </div>
                <br />
                <div style="display: inline-block">
                    <br />
                    <asp:Label Style="font-weight: bold" ID="lblPOS" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                </div>
                <br />
                <div style="display: inline-block">
                    <br />
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                </div>
            </div>
        </div>
    </div>
        <br />

        <div class="ui-widget">
        <asp:Panel runat="server" ID="pnlMensajes" cssClass="ui-corner-all" style="margin-top: 20px; padding: 0 .7em;">
            <p>
                <asp:Label runat="server" ID="lblMensaje"></asp:Label>
            </p>
        <br />
        </asp:panel>
        </div>
         <div runat="server" id="divPagos" visible="true" style="margin-top: 30px; border-style: solid;  display:block;clear:left; overflow:hidden;width:98%;border-width:1px;border-width:1px" >
         <div style="margin-top: 2px; border-style: solid; width: 50%;float:left;border-width:1px;margin-left:2px;" 
            id="divcliente"  visible="true">
            <label style="font-weight: bold;font-size:medium;">Nombre o numero de cliente</label><br />
            <asp:TextBox ID="nomcliente" runat="server" Width="80%"></asp:TextBox>
            <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="80%"  runat="server" 
                 onselectedindexchanged="LstClientes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:Button ID="BtnCliente" runat="server"  Text="..." onclick="BCliente_Click" Width="8%"/><br />
            <br />
            <label style="font-weight: bold;font-size:small;">Email</label>
            <asp:TextBox ID="txtemail" runat="server" Width="60%"></asp:TextBox><br />
            
            <%-- <asp:RadioButton ID="Cliente9" runat="server" Text="Cliente 9" CssClass="RellenoCarrito" />
             <asp:RadioButton ID="Tarjeta" runat="server"  Text="Tarjeta MIT" CssClass="RellenoCarrito1" />--%>
            <asp:UpdatePanel runat="server" id="updPnlEntregaOtraUbicacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:CheckBox ID="optEntregaOtraUbicacion" runat="server"  
                         Text="Entrega en Otra Ubicación" CssClass="RellenoCarrito" 
                         oncheckedchanged="optEntregaOtraUbicacion_CheckedChanged" AutoPostBack="true" />
                    &nbsp;
                    
                    <asp:Panel runat="server" ID="pnlOtraObicacion" Visible="false">
                        
                        <table style="width: 99%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width:25%" >
                                <strong>Nombre</strong>
                            </td>
                            <td>
                                * En atencion a quien se entrega
                                <asp:TextBox runat="server" ID="txtOUNombre" Width="90%" />
                                <asp:RequiredFieldValidator runat="server" ID="reqtxtOUNombre" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUNombre" >
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Apellidos</strong>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUApellidos" Width="90%"/>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUApellidos" >
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Email</strong>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUEmail" Width="90%"/>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUEmail" >
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
                                <asp:TextBox runat="server" ID="txtOUDireccion" Width="90%"/>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtOUDireccion" >
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No. Exterior
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUNoExterior" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No. Interior
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUNoInterior" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Estado
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="cboOUEstado" Width="90%" 
                                    DataSourceID="AVE_EstadosDataSource" DataTextField="Nombre" 
                                    DataValueField="Id" AppendDataBoundItems="True" >
                                    <asp:ListItem Selected="True" Text=" " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ciudad
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUCiudad" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Colonia
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUColonia" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Codigo Postal
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUCodigoPostal" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefono Celular
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUTelfCelular" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefono Fijo
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUTelfFijo" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Referencia de llegada
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOUReferenciaLlegada" Width="90%"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;
                            </td>
                            <td>
                                <asp:Button runat="server" ID="cmdConfirmarEntrega" Text="Confirmar Entrega" 
                                    Width="90%" onclick="cmdConfirmarEntrega_Click" />
                                <asp:Label runat="server" ID="lblEntregaConfirmada" Font-Bold="true" Text="Entrega Confirmada" Visible="False"> </asp:Label>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger  ControlID="optEntregaOtraUbicacion" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
            
            <%-- MJM 24/02/2014 FIN --%>
         </div>
         <div style="display:inline-block;border-style: solid;float:right;width: 45%;border-width:1px">
             <label style="font-weight: bold;font-size:medium;">&nbsp;Formas de Pago</label><br />
             <asp:TextBox ID="TarjetaCliente" runat="server" Width="85%" Visible="false"></asp:TextBox>
             <asp:Button ID="ButClient" runat="server"  Text="..." Width="8%" Visible="false"/><br />
            <%-- <asp:RadioButton ID="Cliente9" runat="server" Text="Cliente 9" CssClass="RellenoCarrito" />
             <asp:RadioButton ID="Tarjeta" runat="server"  Text="Tarjeta MIT" CssClass="RellenoCarrito1" />--%>
             <table style="width:100%" >
                <tr>
                    <td >
                          <asp:RadioButton ID="RadioButton1" runat="server"  Text="Nota Empleado"  CssClass="Ocultarcontrol" Enabled="False" Onclick="handleClickNotaCredito(this);" />
                    </td>
                    <td style="margin-left: 5px; text-align:left;">
                          <asp:RadioButtonList ID="RadioButtonlTipoPago" RepeatLayout="Flow" 
                             RepeatDirection="Horizontal"  runat="server" CssClass="RellenoCarrito1">
                           <asp:ListItem Value="9" >Cliente 9&nbsp;&nbsp;</asp:ListItem>
                           <asp:ListItem Value="T">Tarjeta MIT</asp:ListItem>
                         </asp:RadioButtonList>
                    </td>
                </tr>
             </table>
          </div>
            
          <div runat="server" visible="false" id="CLiente" style="display:inline-block;border-style:solid;float:right ;width: 45%;padding:2px;border-width:1px"  >    
           <label style="font-weight: bold">Nombre</label>
           <asp:Label ID="Nombre" runat="server" value=""></asp:Label><br />
           <asp:Label ID="Email" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label><br />
           <asp:Label ID="Shoelover" runat="server" value="" CssClass="RellenoCarrito1"></asp:Label>
         </div>
            <div runat="server" visible="false" id="Divfinalizar" style="border-style: solid;float:right;width: 45%;padding:2px;border-width:1px" >
              <asp:Button ID="ButFinalizarVenta" runat="server" Text="Finalizar Venta" 
               CssClass="RellenoCarrito2" onclick="ButFinalizarVenta_Click"   />
           </div>  
          <div runat="server"  id="DivTajeta" style="display:none;border-style: solid;float:right;width: 45%;padding:2px;border-width:1px" >
           <table style="width: 100%">
             <tr>
             <td style="width:30%"><label style="font-weight: bold">Seleciona MIT</label></td>
             <td style="width:70%"><label style="font-weight: bold">Seleciona Tarjeta</label></td> 
             </tr>
             <tr>
             <td><asp:DropDownList ID="lstTipoMIT" runat="server" Width="100%"> 
                 <asp:ListItem Value="VMC" Text="V/MC" >V/MC</asp:ListItem> 
                 <asp:ListItem Value="AMEX" Text="AMEX">AMEX</asp:ListItem>
              </asp:DropDownList></td>
              <td><asp:DropDownList ID="lstTarjetas" runat="server" Width="100%"
                  DataSourceID="AVE_BANCOS" DataTextField="descrip" DataValueField="IdBanco">
              </asp:DropDownList></td>
              </tr>
           </table>
        </div>
          <div runat="server" id="Divpuntos" style="display:none;width:45%;padding:2px; display:none;clear:right;float:right;border-width:1px;border-style:solid" >
            <table border="2" cellpadding="1" cellspacing="0" >
            <tr>
              <td colspan="2" >
                <label style="font-weight: bold">Beneficios</label>
              </td>
            </tr>    
              <tr>
                <td><asp:RadioButton ID="RadioButton2" runat="server" Text="Puntos 9" Onclick="ClickCliente9(this);" /></td>
                <td><asp:Label ID="Label10"  style="float:right" runat="server" Text="0"></asp:Label></td>
              </tr>
             <tr>
                <td style="width:80%">
                <table style="width:100%">
                <tr>
                   <td><asp:RadioButton ID="RadioButton3" runat="server" Text="Pares Acumulados" Onclick="ClickPar9(this);" /></td>
                   <td><asp:Label ID="Label13" runat="server" Text="0(0)"></asp:Label></td>
                </tr>
               </table>
                </td><td style="width:20%"><asp:Label style="float:right" ID="Label11" runat="server" Text="0"></asp:Label></td>
              </tr>
              <tr>
                <td>
                 <table style="width:100%">
                    <tr>
                        <td><asp:RadioButton ID="RadioButton4" runat="server" Text="Bolsas Acumuladas"  Onclick="ClickBolsa9(this);"/></td>
                        <td><asp:Label ID="Label14" runat="server" Text="0(0)"></asp:Label></td>
                   </tr>
                 </table>
                 </td>
                <td class="style2"><asp:Label ID="Label12"  style="float:right" runat="server" Text="0"></asp:Label></td>
              </tr>                 
            </table>
          </div> 
          <div id="DivPagar" style="display:none;width:45%;padding:2px;clear:right;float:right;border-width:1px;border-style:solid" >
          <table style="width:100%">
                <tr>
                   <td style="width:40%"><label>Importe a Pagar</label></td>
                   <td style="width:30%"><asp:TextBox ID="txtPago" style="text-align:right;"  runat="server" onkeypress="return IsNumber(event,this);"></asp:TextBox></td>
                   <td style="width:30%">
                        <input id="ButPagar" type="button" style="width:100%" value="Pagar" onclick="return ValidarImporte()"   />
                        <asp:Button ID="BtnPagar" style="display:none" runat="server" Text="Pagar"  onclick="ButPagar_Click" />
                  </td>
                
                </tr>
               </table>
            
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
        SelectCommand="AVE_dameDetalleCarrito" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="IdCarrito" SessionField="IdCarrito" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AVE_CarritoEliminar" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        DeleteCommand="dbo.AVE_eliminaLineaCarrito" DeleteCommandType="StoredProcedure"
        OnDeleted="AVE_CarritoEliminar_Deleted">
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

</asp:Content>
