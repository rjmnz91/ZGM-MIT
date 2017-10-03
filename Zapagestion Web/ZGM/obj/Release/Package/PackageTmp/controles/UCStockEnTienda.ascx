<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCStockEnTienda.ascx.cs"
    Inherits="AVE.controles.UCStockEnTienda" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controles/UCDetallesProducto.ascx" TagName="UCDetallesProducto"
    TagPrefix="UCD" %>

<style type="text/css">
    .auto-style2 {
        width: 188px;
    }
</style>

<script type="text/javascript">

    // MJM 26/03/2014 INICIO
    // http://www.dotnetbull.com/2011/11/scrollable-gridview-with-fixed-headers.html
    //
    function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
        var tbl = document.getElementById(gridId);
        if (tbl) {
            var DivHR = document.getElementById('DivHeaderRow');
            var DivMC = document.getElementById('DivMainContent');
            var DivFR = document.getElementById('DivFooterRow');

            //*** Set divMainContent Properties ****
            //DivMC.style.width = width + 'px';
            DivMC.style.height = height + 'px';
            DivMC.style.position = 'relative';
            DivMC.style.top = -headerHeight + 'px';
            DivMC.style.zIndex = '1';

            //*** Set divheaderRow Properties ****
            DivHR.style.height = headerHeight + 'px';
            DivHR.style.width = (parseInt(width) - 16) + 'px';
            //DivHR.style.width = DivMC.style.width;
            DivHR.style.position = 'relative';
            DivHR.style.top = '0px';
            DivHR.style.zIndex = '10';
            DivHR.style.verticalAlign = 'top';

            //*** Set divFooterRow Properties ****
            DivFR.style.width = (parseInt(width) - 16) + 'px';
            //DivFR.style.width = DivMC.style.width;
            DivFR.style.position = 'relative';
            DivFR.style.top = -headerHeight + 'px';
            DivFR.style.verticalAlign = 'top';
            DivFR.style.paddingtop = '2px';

            if (isFooter) {
                var tblfr = tbl.cloneNode(true);
                tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                var tblBody = document.createElement('tbody');
                tblfr.style.width = '100%';
                tblfr.cellSpacing = "0";
                tblfr.border = "0px";
                tblfr.rules = "none";
                //*****In the case of Footer Row *******
                tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                tblfr.appendChild(tblBody);
                DivFR.appendChild(tblfr);
            }
            //****Copy Header in divHeaderRow****
            DivHR.appendChild(tbl.cloneNode(true));
        }
    }

    function OnScrollDiv(Scrollablediv) {
        document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
    }

    // MJM 26/03/2014 FIN

    function AgregarTalla(Label) {

        if (Label.innerHTML != '') {

            var strRegistro = Label.innerHTML;
            if (Label.className == 'Negrita') {
                Talla_Selecionada.value += strRegistro + '|';
                Label.className = 'TallaSelecionada';
            }
            else {

                Talla_SelecionadaNo.value += strRegistro + '|';
                Label.className = 'Negrita';


            }
        }
        return true;

    }

    function ConfirmarSolicitud(texto) {
        if (confirm(texto)) {
            document.getElementById('<%= Button2.ClientID %>').click();
        }
    }

    function ConfirmarCarrito(texto) {
        if (confirm(texto)) {
            document.getElementById('<%= Button3.ClientID %>').click();
        }

    }
    function ConfirmarCarritoSinSol(texto) {
        if (confirm(texto)) {
            document.getElementById('<%= ConfirmPedido.ClientID %>').click();
        }
        else {
            document.getElementById('<%= CancelPedido.ClientID %>').click();
        }
    }

</script>


<div class="container">
    <div style="width:80%">
        <div class="barraNavegacion" style="font-weight: 700">
    <asp:Button ID="btnMasTiendas" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Resource, MasTiendas%>"
        Enabled="false" OnClick="btnMasTiendas_Click" Visible="False" />
    <asp:Button ID="btnFoto" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Resource, Foto%>"
        Enabled="false" OnClick="btnFoto_Click" Visible="False" />
    <asp:Button ID="btnDetalles" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Resource, Detalles%>"
        Enabled="false" OnClick="btnDetalles_Click" Visible="False" />
</div>
   
        <div>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" OnClick="Button2_Click" CssClass="btn btn-default" />
    <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" OnClick="Button3_Click" CssClass="btn btn-default" />
    <asp:Button ID="ConfirmPedido" runat="server" Text="Button" Style="display: none" OnClick="ConfirmPedido_Click" CssClass="btn btn-default" />
    <asp:Button ID="CancelPedido" runat="server" Text="Button" Style="display: none" OnClick="CancelPedido_Click" CssClass="btn btn-default" />
    <a href="javascript:history.back();"></a>
    <asp:Button ID="btnComplementosMini" runat="server" Text="Complementos" Visible="false" OnClick="btnComplementosMini_Click" CssClass="btn btn-default" />
    <asp:Button ID="btnSustitutivoMini" runat="server" Text="Sustitutos" Visible="false" OnClick="btnSustitutivoMini_Click" CssClass="btn btn-default" />
    <asp:Button ID="btnComplementos" runat="server" TabIndex="3" CssClass="btn btn-default" Text="Stocks de C/S" OnClick="btnComplementos_Click" Visible="false" CommandArgument="" />
</div>
    
        <asp:SqlDataSource ID="AVE_StockEnTiendaComplementario" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
    SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure"
    DataSourceMode="DataSet">
    <SelectParameters>
        <asp:Parameter Name="IdArticulo" Type="String" />
        <asp:Parameter Name="IdTienda" Type="String" />
        <asp:Parameter Name="Tipo" Type="Char" />
    </SelectParameters>
</asp:SqlDataSource>
    
        <div runat="server" id="divComplementosMini" style="overflow: horizontal; border: solid 1px black;" visible="false">
    <asp:Repeater runat="server" ID="repComplementos" DataSourceID="AVE_StockEnTiendaComplementario">
        <HeaderTemplate>
            <table style="width: auto;" cellpadding="10" cellspacing="0">
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
                <asp:HyperLink runat="server" ID="lnkComplementoMini" NavigateUrl=' <%# String.Format("~/EleccionProducto.aspx?Filtro={0}", Eval("IdArticulo")) %>' Font-Underline="false">
                    <table>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Image runat="server" ID="imgMiniatura" Width="64px" Height="64px" ImageUrl='<%# ObtenerURLFoto(Eval("IdArticulo").ToString()) %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <p>
                                    <asp:Label runat="server" ID="lblDescComplementario" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                    <br />
                                    Stock en Tienda:&nbsp;<asp:Label runat="server" ID="lblStockEnTienda" Text='<%# StockDeArticulo(Eval("idArticulo").ToString(), true) %>'></asp:Label>
                                    <br />
                                    Stock Otras Tiendas:&nbsp;<asp:Label runat="server" ID="lblStockEnOtrasTiendas" Text='<%# StockDeArticulo(Eval("idArticulo").ToString(), false) %>'></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </asp:HyperLink>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
        <br />
        <asp:SqlDataSource ID="AVE_StockEnTiendaSustitutivo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
    <SelectParameters>
        <asp:Parameter Name="IdArticulo" Type="String" />
        <asp:Parameter Name="IdTienda" Type="String" />
        <asp:Parameter Name="Tipo" Type="Char" />
    </SelectParameters>
</asp:SqlDataSource>
    
        <div runat="server" id="divSustitutosMini" style="overflow: horizontal; border: solid 1px black;" visible="false">
    <asp:Repeater runat="server" ID="repSustitutosMini" DataSourceID="AVE_StockEnTiendaSustitutivo">
        <HeaderTemplate>
            <table style="width: auto;" cellpadding="10" cellspacing="0">
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl=' <%# String.Format("~/EleccionProducto.aspx?Filtro={0}", Eval("IdArticulo")) %>' Font-Underline="false">
                    <table>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Image runat="server" ID="Image1" Width="64px" Height="64px" ImageUrl='<%# ObtenerURLFoto(Eval("IdArticulo").ToString()) %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <p>
                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                    <br />
                                    Stock en Tienda:&nbsp;<asp:Label runat="server" ID="Label2" Text='<%# StockDeArticulo(Eval("idArticulo").ToString(), true) %>'></asp:Label>
                                    <br />
                                    Stock Otras Tiendas:&nbsp;<asp:Label runat="server" ID="Label3" Text='<%# StockDeArticulo(Eval("idArticulo").ToString(), false) %>'></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </asp:HyperLink>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
        <br />

        <div class="col-lg-2 col-sm-2 col-md-2"></div>
        <div class="col-lg-8 col-sm-8 col-md-8">
            <div class="panel panel-primary">
        <div class="panel-heading">
            <asp:Label ID="lblDescripcion" runat="server" BorderStyle="None" Font-Bold="True"></asp:Label>
        </div>
        <div class="panel-body" id="PnlFoto">
        
        
        <table>
            <tr>
                <td>
                    <asp:Image ID="FotoArticulo" runat="server" Style="width: 250px;" />
                </td>
                <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrecio" runat="server" Font-Bold="true" Text="Precio: "></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblPrecioDescuento" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label style="text-transform:uppercase;" runat="server" ID="lblPorcentajeDescuento" Visible="false" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblPrecioValor" runat="server" Font-Bold="true" Style="text-align:right;"></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td colspan=2>
                                        <UCD:UCDetallesProducto ID="Detalle" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                <td colspan="2" class="auto-style2" style="padding-left:3em;">
                    <div id="pnlModelo" style="text-align:center;">
                        <asp:Panel ID="PModelo" runat="server" Visible="false">
                        </asp:Panel>
                        <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-control" Width="115%" AutoPostBack="true" DataSourceID="SqlDataModColores" DataTextField="Color" DataValueField="idarticulo" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>

        <asp:Button ID="btnPrecio" runat="server" Text="+" OnClick="btnPrecio_Click" CssClass="btn btn-default" Visible="false" />
        
        <asp:SqlDataSource ID="AVE_ArticuloFotoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure"
                        DataSourceMode="DataSet">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="AVE_ArticuloFotoByIdArticulo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure"
                        DataSourceMode="DataSet">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

        <div class="row" style="width:65%">
            <table>
                <tr>
                    <td style="width:10%"></td>
                    <td style="width:80%">
                        <asp:GridView runat="server" ID="GridView2" AutoGenerateColumns="false" DataKeyNames="Articulo" EnableViewState="true" 
                OnSelectedIndexChanged="GridView2_SelectedIndexChanged" CssClass="table table-striped  hover table-hover table-condensed" Visible="False">
                <RowStyle BackColor="White" ForeColor="Black" />
                <AlternatingRowStyle BackColor="PaleTurquoise" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField Visible="true" ItemStyle-CssClass="GridItem">
                        <ItemTemplate>
                            <asp:Button ID="lnkSelect" runat="server" CommandName="select" CssClass="btn btn-primary" Font-Size="X-Small" Style="width: 50px;" Text="✚" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Articulo" HeaderText="<%$ Resources:Resource, Articulo%>" ItemStyle-CssClass="GridItem" />
                    <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color%>" ItemStyle-CssClass="GridItem" />
                </Columns>
            </asp:GridView>
                    </td>
                    <td style="width:10%"></td>
                </tr>
            </table>
        
            <br />
            <div id="DivRoot">
                <%--<div style="overflow: hidden; background-color:antiquewhite" id="DivHeaderRow" >
                </div>--%>
                <br /><br />
                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                    <asp:GridView runat="server" ID="GridStock" AutoGenerateColumns="false" OnRowDataBound="GridStock_RowDataBound" 
                        OnRowCommand="GridStock_RowCommand" CssClass="table table-striped  hover table-hover table-condensed">
                        <HeaderStyle BackColor="#3333FF" ForeColor="Black" Font-Bold="true"/>
                        <RowStyle BackColor="White" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="PaleTurquoise" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="IdTienda" HeaderText="<%$ Resources:Resource, ID%>" HeaderStyle-Width="70" ItemStyle-CssClass="GridItem" />
                                <asp:BoundField DataField="Tienda" HeaderText="<%$ Resources:Resource, Tienda%>" HeaderStyle-Width="220" ItemStyle-CssClass="GridItem" />
                                <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Resource, Total%>" HeaderStyle-Width="50" ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                </div>
                <%--<div id="DivFooterRow" style="overflow: hidden">
                </div>--%>
            </div>
        </div>

        <div class="row">
                <asp:Panel ID="PnlTallaje" runat="server">
                </asp:Panel>
                <asp:Panel ID="PnlSolicitar" runat="server">
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                </asp:Panel>
            </div>

        <div style="float: left; width: 100%;">

                    <asp:HiddenField ID="hiddenTallas" runat="server" />
                    <asp:HiddenField ID="hiddenTallasNo" runat="server" OnValueChanged="hiddenTallasNo_ValueChanged" />

                    <asp:SqlDataSource ID="AVE_StockEnTiendaObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="dbo.AVE_StockEnTiendaObtener" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="String" />
                            <asp:Parameter Name="IdTienda" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataTallajes" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="AVE_TallajeArticulo" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataModColores" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="AVE_ColorModFabricante" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SDSPedido" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        InsertCommand="AVE_PedidosCrear" InsertCommandType="StoredProcedure" OnInserted="SDSPedido_Inserted">
                        <InsertParameters>
                            <asp:Parameter Name="IdArticulo" Type="Int32" />
                            <asp:Parameter Name="Talla" Type="String" />
                            <asp:Parameter Name="Unidades" Type="Int16" />
                            <asp:Parameter Name="Precio" Type="Decimal" />
                            <asp:Parameter Name="IdEmpleado" Type="Int32" />
                            <asp:Parameter Name="Usuario" Type="String" />
                            <asp:Parameter Name="IdTienda" Type="String" />
                            <asp:Parameter Name="Stock" Type="Int16" />
                            <asp:Parameter Name="IdTerminal" Type="Int16" />
                            <asp:Parameter Name="IdPedido" Type="Int32" Direction="ReturnValue" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="AnadirCarrito" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        InsertCommand="AVE_InsertaCarrito" InsertCommandType="StoredProcedure"
                        OnInserted="AnadirCarrito_Inserted">
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
                    <asp:SqlDataSource ID="SDSTieneCS" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                        SelectCommand="AVE_ProductoTieneCS" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
                        <SelectParameters>
                            <asp:Parameter Name="IdArticulo" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>

        </div>
    </div>
        </div>
        <div class="col-lg-2 col-sm-2 col-md-2"></div>
    </div>
</div>

