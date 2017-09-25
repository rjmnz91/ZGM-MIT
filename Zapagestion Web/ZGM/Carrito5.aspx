<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito5.aspx.cs" Inherits="AVE.Carrito5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="js/Carrito.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>

    <asp:HiddenField runat="server" ID="hidTotal" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidPendiente" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidPuntos" ClientIDMode="Static" />

            <asp:GridView ID="gvCarrito" runat="server" DataKeyNames="id_carrito_detalle" AutoGenerateColumns="false" ShowHeader="false"
                Style="width: 99%" GridLines="Both" OnRowDataBound="gvCarrito_RowDataBound" PageSize="2" ViewStateMode="Disabled" 
                DataSourceID="AVE_CarritoObtener" onrowcommand="gvCarrito_RowCommand" OnRowCreated="gvCarrito_RowCreated" BorderStyle="Solid" BorderWidth="1px" >
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
                            <%--<asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>--%>
                        </HeaderTemplate>
                        <ItemStyle CssClass="borderBottom" Width="40%" />
                        <ItemTemplate>
                            <div style="width: 100%; text-align: center">
                                <div style="float: left; width: 110px; text-align: right; margin-right: 15px; margin-left: 3%; font-weight: bold; font-size: 10px;">
                                    <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, Precio%>'></asp:Label>:
                                </div>
                                <div style="float: left; text-align: right; width: 70px;">
                                    <asp:Label runat="server" ID="lblPrecioOriginal" Text='<%# FormateaNumero(Eval("PVPORI").ToString())%>'></asp:Label>
                                </div>
                                <br />
                                <div runat="server" id="laDescuento" style="width: 110px; float: left; text-align: right; margin-right: 15px; margin-left: 3%; font-weight: bold; font-size: 10px;">
                                   <%-- <asp:Label runat="server" ID="Label5" Text="Descuento :">--%>
                                   <asp:Label runat="server" ID="Label5" Text="Descuento :" ></asp:Label>
                                </div>
                                <div runat="server" id="laImporteDescuento" style="float: left; text-align: right; width: 70px; ">
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
                                         AutoPostBack="true" ></asp:DropDownList>
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

    <br />
    <asp:Panel runat="server" ID="pnlBusqueda"  DefaultButton="btnBuscar" ClientIDMode="Static">
        <asp:TextBox runat="server" id="txtBuscar" Width="99%" ></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
            onclick="btnBuscar_Click" />
            <asp:DropDownList runat="server" ID="cboClientes" 
            AppendDataBoundItems="true" AutoPostBack="true" Visible="false" Width="99%" 
            onselectedindexchanged="cboClientes_SelectedIndexChanged">
                <asp:ListItem Selected="True" Text="[Seleccione un cliente de la lista]" Value=0></asp:ListItem>
            </asp:DropDownList>
    </asp:Panel>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 98%;">
        <tr>
            <td style="width:50%; vertical-align:top;">
                <asp:UpdatePanel runat="server" ID="pnlPagos">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnPagar" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:RadioButton runat="server" ID="optTarjeta" Text="Tarjeta MIT"  Visible="true" AutoPostBack="true" CssClass="optTarjeta"
                            GroupName="Pagos" oncheckedchanged="optTarjeta_CheckedChanged" ClientIDMode="Static"  Checked="true"/>
                        <asp:RadioButton runat="server" ID="optNotaEmpleado" Text="Nota Empleado" Visible="false"  AutoPostBack="true"
                            GroupName="Pagos" oncheckedchanged="optTarjeta_CheckedChanged" ClientIDMode="Static"/>
                        <asp:RadioButton runat="server" ID="optCliente9" Text="Cliente 9" Visible="false"  AutoPostBack="true"
                            GroupName="Pagos" oncheckedchanged="optTarjeta_CheckedChanged" ClientIDMode="Static"/>
                        <br />
                        <br />
                        <asp:Panel runat="server" ID="pnlPagoTarjeta" Visible="false">
                            <label style="font-weight: bold">Seleciona Tarjeta</label>
                            <br />
                            <asp:DropDownList ID="lstTarjetas" runat="server" width="250px" ClientIDMode="Static" CssClass="lstTarjetas"
                                DataSourceID="AVE_BANCOS" DataTextField="descrip" DataValueField="IdBanco">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="AVE_BANCOS" runat="server" ConnectionString="<%$ ConnectionStrings:Pagos_MIT %>"
                                SelectCommand="Select * from  (SELECT [IdBanco], [descrip] FROM [TblBancos] union
                                              Select 0 as IdBanco,'' as descrip) BANCOS ORDER BY [IdBanco]">
                            </asp:SqlDataSource>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlPagoNota" Visible="false">
                            <br />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlPagoCliente9" Visible="false">
                            <table cellpadding="0" cellspacing="0" border="1" style="width:300px;">
                                <tbody><tr>
                                  <td colspan="2">
	                                <label style="font-weight: bold">Beneficios</label>
                                  </td>
                                </tr>    
                                  <tr>
	                                <td>
                                        <asp:RadioButton runat="server" ID="optPuntos9" Text="Puntos 9" GroupName="Cliente9" ClientIDMode="Static"/>
                                    </td>
	                                <td style="text-align:right;">
                                        <asp:Label runat="server" ID="Label10"></asp:Label>
                                    </td>
                                  </tr>
                                 <tr>
	                                <td style="width:80%">
	                                    <table style="width:100%">
	                                        <tbody>
                                                <tr>
	                                                <td>
                                                        <asp:RadioButton runat="server" ID="optParesAcumulados" Text="Pares Acumulados" GroupName="Cliente9" ClientIDMode="Static"/>
                                                   </td>
	                                                <td style="text-align:right;">
                                                        <asp:Label runat="server" ID="Label13"></asp:Label>
                                                    </td>
	                                            </tr>
                                           </tbody>
                                       </table>
	                                </td>
                                    <td style="width:20%">
                                        <asp:Label runat="server" ID="Label11"></asp:Label>
                                    </td>
                                  </tr>
                                  <tr>
	                                <td>
	                                 <table style="width:100%">
		                                <tbody>
                                            <tr>
			                                    <td>
<%--                                        <input id="ctl00_ContentPlaceHolder1_RadioButton4" name="ctl00$ContentPlaceHolder1$RadioButton4" value="RadioButton4" disabled="disabled" onclick="ClickBolsa9(this);" type="radio">
                                            <label for="ctl00_ContentPlaceHolder1_RadioButton4">Bolsas Acumuladas</label></span> --%>
                                                    <asp:RadioButton runat="server" ID="optBolsasAcumuladas" Text="Bolsas Acumuladas" GroupName="Cliente9" ClientIDMode="Static"/>
                                                </td>
			                                    <td style="text-align:right;">
                                                    <asp:Label runat="server" ID="Label14"></asp:Label>
                                                </td>
	                                       </tr>
	                                    </tbody>
                                     </table>
	                                 </td>
	                                <td class="style2">
                                        <asp:Label runat="server" ID="Label12"></asp:Label>
                                    </td>
                                  </tr>                 
                                </tbody>
                            </table>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlPagar" Visible="false">
                            <br />
                            Importe a pagar:&nbsp;<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPagar" CssClass="txtPago" style="text-align: right;"   ></asp:TextBox>
                            &nbsp;
                            <asp:Button runat="server" ID="btnPagar" Text="Pagar" OnClick="btnPagar_Click" OnClientClick="return ValidarImporte();"/>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width:50%; vertical-align:top;">
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
                <div runat="server" id="resumenPago" style="margin-left: 45%; width: 260px; height:100%; border: solid 1px black;float: left">
                    <asp:Repeater ID="RepeaterPagos" runat="server">
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

                <div runat="server" id="Pendiente" style="margin-left: 45%; width: 260px; height: 20px; border: solid 1px black;
                    float: left">
                     <div style="float: left; width: 150px; text-align: right; font-size: 12px; font-weight: bold;padding-left:8PX">
                        <asp:Label runat="server" ID="lbl14" Text='Total por Pagar'></asp:Label>:
                    </div>
                    <div style="float: left; width: 90px; text-align: right; font-weight: bold; font-size: 12px;">
                        <asp:Label runat="server" ID="TotPendiente" Text=''></asp:Label>
                    </div>
                    <br />
                </div>
                <div style="margin-left: 45%; width: 260px; height:100%; border: solid 1px black;float: left">
                    <asp:Button ID="btnEnviarPOS" runat="server" Text='<%$ Resources:Resource, EnviarPOS%>'
                        OnClientClick="SetSource(this.id)" OnClick="btnEnviarPOS_Click" Width="100%" />
                </div>

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
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <%--OnDeleted="AVE_CarritoEliminar_Deleted"--%>

    <asp:SqlDataSource ID="AVE_CarritoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_dameDetalleCarrito" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:SessionParameter Name="IdCarrito" Type="int32" SessionField="IdCarrito" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
