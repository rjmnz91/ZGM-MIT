<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCStockEnTienda.ascx.cs"
    Inherits="AVE.controles.UCStockEnTienda" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controles/UCDetallesProducto.ascx" TagName="UCDetallesProducto"
    TagPrefix="UCD" %>

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
<div class="barraNavegacion">
    <asp:Button ID="btnMasTiendas" runat="server" CssClass="botonNavegacion" Text="<%$ Resources:Resource, MasTiendas%>"
        Enabled="false" OnClick="btnMasTiendas_Click" Visible="False" />
    <asp:Button ID="btnFoto" runat="server" CssClass="botonNavegacion" Text="<%$ Resources:Resource, Foto%>"
        Enabled="false" OnClick="btnFoto_Click" Visible="False" />
    <asp:Button ID="btnDetalles" runat="server" CssClass="botonNavegacion" Text="<%$ Resources:Resource, Detalles%>"
        Enabled="false" OnClick="btnDetalles_Click" Visible="False" />
</div>
<asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" OnClick="Button2_Click" />
<asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" OnClick="Button3_Click" />
<asp:Button ID="ConfirmPedido" runat="server" Text="Button" Style="display: none" OnClick="ConfirmPedido_Click" />
<asp:Button ID="CancelPedido" runat="server" Text="Button" Style="display: none" OnClick="CancelPedido_Click" />
<a href="javascript:history.back();"></a>
<asp:Button id="btnComplementosMini" runat="server" Text="Complementos" Visible="false" OnClick="btnComplementosMini_Click" />
<asp:Button id="btnSustitutivoMini" runat="server" Text="Sustitutos" Visible="false" OnClick="btnSustitutivoMini_Click" />
<asp:Button ID="btnComplementos" runat="server" TabIndex="3" CssClass="botonNavegacion" Text="Stocks de C/S" OnClick="btnComplementos_Click" Visible="false" CommandArgument="" />

<asp:SqlDataSource ID="AVE_StockEnTiendaComplementario" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
    SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure"
    DataSourceMode="DataSet">
    <SelectParameters>
        <asp:Parameter Name="IdArticulo" Type="String" />
        <asp:Parameter Name="IdTienda" Type="String" />
        <asp:Parameter Name="Tipo" Type="Char" />
    </SelectParameters>
</asp:SqlDataSource>

<div runat="server" id="divComplementosMini" style="overflow: horizontal; border: solid 1px black;" visible="false" >
    <asp:Repeater runat="server" ID="repComplementos" DataSourceID="AVE_StockEnTiendaComplementario">
        <HeaderTemplate>
            <table style="width:auto;" cellpadding="10" cellspacing="0">
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
                    <td>
                        <asp:HyperLink runat="server" ID="lnkComplementoMini" NavigateUrl=' <%# String.Format("~/EleccionProducto.aspx?Filtro={0}", Eval("IdArticulo")) %>' Font-Underline="false">
                            <table>
                                <tr>
                                    <td style="text-align:center;">
                                        <asp:Image runat="server" ID="imgMiniatura" Width="64px" Height="64px" ImageUrl='<%# ObtenerURLFoto(Eval("IdArticulo").ToString()) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center;">
                                        <p><asp:Label runat="server" ID="lblDescComplementario" Text='<%# Eval("Descripcion") %>'></asp:Label>
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
<asp:SqlDataSource ID="AVE_StockEnTiendaSustitutivo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
    SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure"
    DataSourceMode="DataSet">
    <SelectParameters>
        <asp:Parameter Name="IdArticulo" Type="String" />
        <asp:Parameter Name="IdTienda" Type="String" />
        <asp:Parameter Name="Tipo" Type="Char" />
    </SelectParameters>
</asp:SqlDataSource>
<div runat="server" id="divSustitutosMini" style="overflow: horizontal; border: solid 1px black;" visible="false" >
    <asp:Repeater runat="server" ID="repSustitutosMini" DataSourceID="AVE_StockEnTiendaSustitutivo">
        <HeaderTemplate>
            <table style="width:auto;" cellpadding="10" cellspacing="0">
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
                    <td>
                        <asp:HyperLink runat="server" ID="lnkComplementoMini" NavigateUrl=' <%# String.Format("~/EleccionProducto.aspx?Filtro={0}", Eval("IdArticulo")) %>' Font-Underline="false">
                            <table>
                                <tr>
                                    <td style="text-align:center;">
                                        <asp:Image runat="server" ID="imgMiniatura" Width="64px" Height="64px" ImageUrl='<%# ObtenerURLFoto(Eval("IdArticulo").ToString()) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center;">
                                        <p><asp:Label runat="server" ID="lblDescComplementario" Text='<%# Eval("Descripcion") %>'></asp:Label>
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
<table width="90%" style="height:80%;" >
    <tr>
        <td>
            <div id="PnlFoto" style="float: left; display: table-cell; vertical-align: top;">
                <table width="90%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblDescripcion" runat="server" BorderStyle="None" Font-Bold="True"></asp:Label>
                            <tr>
                                <td style="width: 250px">
                                    <asp:Image ID="FotoArticulo" runat="server" Style="width: 240px;" />
                                </td>
                                <td style="width: auto">
                                    <table width="80%">
                                        <tr>
                                            <td style="width: 50%">
                                                 <strong>Precio:</strong><asp:label ID="lblPrecioDescuento" runat="server" Font-Bold="true" Visible="false" ></asp:label>
                                                 <asp:Label runat="server" id="lblPorcentajeDescuento" Visible="false" Font-Bold="true" ></asp:Label>
                                                 <asp:Label ID="br" runat="server" Text=""></asp:Label>
                                                 <strong><asp:Label ID="lblPrecioValor"  runat="server"></asp:Label>
                                                 </strong>
                                                 <UCD:UCDetallesProducto ID="Detalle" runat="server" />
                                            </td>
                                            <td>
                                                <div id="pnlModelo">
                                                    <asp:Panel ID="PModelo" runat="server">
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                </table>
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
                <asp:Button ID="btnPrecio" runat="server" Text="+" OnClick="btnPrecio_Click" CssClass="boton2"
                    Visible="false" />
            </div>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="float: left; width:100%;">
                <asp:GridView runat="server" ID="GridView2" AutoGenerateColumns="false" DataKeyNames="Articulo" EnableViewState="true"
                    SkinID="gridviewSkin" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                    Visible="False">
                    <Columns>
                        <asp:TemplateField Visible="true" ItemStyle-CssClass="GridItem">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="select" ID="lnkSelect" Text=">" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Articulo" HeaderText="<%$ Resources:Resource, Articulo%>"
                            ItemStyle-CssClass="GridItem" />
                        <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color%>" ItemStyle-CssClass="GridItem" />
                    </Columns>
                </asp:GridView>

                <%--<div style="overflow-x: auto; overflow-y: auto; width: 90%; height:150px; margin: 0 auto;">--%>

                <div id="DivRoot" align="left">
                    <div style="overflow: hidden; " id="DivHeaderRow">
                    </div>

                    <div style="overflow:scroll; width:900px !important; height: 150px !important;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                        <asp:GridView runat="server" ID="GridStock" AutoGenerateColumns="false" SkinID="gridviewSkin"
                            OnRowDataBound="GridStock_RowDataBound" 
                            OnRowCommand="GridStock_RowCommand" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="IdTienda" HeaderText="<%$ Resources:Resource, ID%>" HeaderStyle-Width="70"
                                    ItemStyle-CssClass="GridItem"  />
                                <asp:BoundField DataField="Tienda" HeaderText="<%$ Resources:Resource, Tienda%>" HeaderStyle-Width="220"
                                    ItemStyle-CssClass="GridItem" />
                                <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Resource, Total%>" HeaderStyle-Width="50"
                                    ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
       
                    </div>

                    <div id="DivFooterRow" style="overflow:hidden">
                    </div>
                </div>

                <%--</div>--%>

                <asp:HiddenField ID="hiddenTallas" runat="server" />
                <asp:HiddenField ID="hiddenTallasNo" runat="server" OnValueChanged="hiddenTallasNo_ValueChanged" />
                <asp:Panel ID="PnlTallaje" runat="server">
                </asp:Panel>
                <asp:Panel ID="PnlSolicitar" runat="server">
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%--<asp:Button ID="Button1" 
             runat="server" Text="Solicitar" Font-Size="X-Large" 
             Height="48px" Width="269px" onclick="Button1_Click" Visible="False"/>--%></asp:Panel>
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
             
                <asp:SqlDataSource ID="SDSTieneCS" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                    SelectCommand="AVE_ProductoTieneCS" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
                    <SelectParameters>
                        <asp:Parameter Name="IdArticulo" Type="int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </td>
    </tr>
</table>
