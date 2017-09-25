<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EleccionProducto.aspx.cs"
    Inherits="AVE.EleccionProducto" EnableEventValidation="false" Theme="Tema" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/controles/UCEleccionProducto.ascx" TagName="UCEleccionProducto"
    TagPrefix="uc1" %>
<%@ Register Src="~/controles/UCStockEnTienda.ascx" TagName="UCStockEnTienda" TagPrefix="uc1" %>
<%@ Register Src="~/controles/UCCLiente9.ascx" TagName="UCCliente9" TagPrefix="c9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="js/json2.js" type="text/javascript"></script>
    <script src="js/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="js/jstorage.js" type="text/javascript"></script>

    <%--<link href="css/sidebar-style.css" rel="stylesheet" type="text/css" />--%>


    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <!-- Bootstrap Js CDN -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <title>ELECCIÓN PRODUCTO</title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
        }

        .ajax__tab_default .ajax__tab_header {
            white-space: nowrap;
        }

        .ajax__tab_default .ajax__tab_outer {
        }

        .ajax__tab_default .ajax__tab_inner {
        }

        .ajax__tab_default .ajax__tab_tab {
            overflow: hidden;
            text-align: center;
            cursor: pointer;
        }

        /* tab style */
        .myCustomstyle .ajax__tab_header {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            background: url('img/Tabs/tab-line.gif') repeat-x bottom;
        }

        .myCustomstyle .ajax__tab_outer {
            background: url('img/Tabs/tab-right.gif') no-repeat right;
        }

        .myCustomstyle .ajax__tab_inner {
            background: url('img/Tabs/tab-left.gif') no-repeat;
        }

        .myCustomstyle .ajax__tab_tab {
            background: url('img/Tabs/tab.gif') repeat-x;
        }

        .myCustomstyle .ajax__tab_hover .ajax__tab_outer {
            background: url('img/Tabs/tab-hover-right.gif') no-repeat right;
        }

        .myCustomstyle .ajax__tab_hover .ajax__tab_inner {
            background: url('img/Tabs/tab-hover-left.gif') no-repeat;
        }

        .myCustomstyle .ajax__tab_hover .ajax__tab_tab {
            background: url('img/Tabs/tab-hover.gif') repeat-x;
        }

        .myCustomstyle .ajax__tab_active .ajax__tab_outer {
            background: url('img/Tabs/tab-active-right.gif') no-repeat right;
        }

        .myCustomstyle .ajax__tab_active .ajax__tab_inner {
            background: url('img/Tabs/tab-active-left.gif') no-repeat;
        }

        .myCustomstyle .ajax__tab_active .ajax__tab_tab {
            background: url('img/Tabs/tab-active.gif') repeat-x;
        }

        .myCustomstyle .ajax__tab_body {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 1px solid #999999;
            border-top: 0;
            background-color: #ffffff;
            float: left;
        }

        .myCustomstyle2 .ajax__tab_header {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
        }

        .myCustomstyle2 .ajax__tab_outer {
            background: url('img/Tabs/tab-right.gif') no-repeat right;
        }

        .myCustomstyle2 .ajax__tab_inner {
            background: url('img/Tabs/tab-left.gif') no-repeat;
        }

        .myCustomstyle2 .ajax__tab_tab {
            background: url('img/Tabs/tab.gif') repeat-x;
        }

        .myCustomstyle2 .ajax__tab_hover .ajax__tab_outer {
            background: url('img/Tabs/tab-hover-right.gif') no-repeat right;
        }

        .myCustomstyle2 .ajax__tab_hover .ajax__tab_inner {
            background: url('img/Tabs/tab-hover-left.gif') no-repeat;
        }

        .myCustomstyle2 .ajax__tab_hover .ajax__tab_tab {
            background: url('img/Tabs/tab-hover.gif') repeat-x;
        }

        .myCustomstyle2 .ajax__tab_active .ajax__tab_outer {
        }

        .myCustomstyle2 .ajax__tab_active .ajax__tab_inner {
        }

        .myCustomstyle2 .ajax__tab_active .ajax__tab_tab {
        }

        .myCustomstyle2 .ajax__tab_body {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 0px solid #999999;
            border-top: 0;
            background-color: #ffffff;
            float: left;
        }

        .texto_blanco {
        }
        /* scrolling */
        .ajax__scroll_horiz {
            overflow-x: scroll;
        }

        .ajax__scroll_vert {
            overflow-y: scroll;
        }

        .ajax__scroll_both {
            overflow: scroll;
        }

        .ajax__scroll_auto {
            overflow: auto;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').toggleClass('active');
            });
        });

        function CargarTab() {
            document.getElementById("btnCabecera").click();
        }
        function SustitutoVisible() {
            document.getElementById("btnSustituto").click();
        }
        function ComplementoVisible() {
            document.getElementById("btnComplemento").click();
        }
        function submitDetailsForm() {
            $("#scannPro").submit();
        }
        function LanzaScanner() {
            $('input#sbtEscanear').trigger('click');

        }
    </script>

</head>
<body>
    <div style="display: none">
        <form id="scannPro" action="sendScanReader" name="sendScanReader" method="post">
            <!-- Este action para hacer la llamada al lector -->
            <input type="hidden" name="dataType" id="dataType" value="numeric" /><!-- Tipo de dato que quieres leer, puede ser numeric o alfanumeric -->
            <input type="hidden" name="dataLength" id="dataLength" value="100" /><!-- Tamaño del dato que quieres leer-->
            <input type="hidden" name="inputTextNameToReturnData" id="inputTextNameToReturnData" value="txtBusquedaProducto" /><!-- input donde quieres que se escriba la respuesta del lector-->
            <input name="sbtEscanear" type="button" value="Escanear" onclick="submitDetailsForm()" id="sbtEscanear" />
        </form>
    </div>

    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false"></asp:ToolkitScriptManager>

        <div class="row">
                <div class="col-sm-12 col-md-12" style="text-align: center">
                    <nav class="navbar navbar-inverse">
                        <div class="container-fluid">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" style="width: auto" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                                <img src="img/ninewest_s.png" alt="ninewest" style="background-color: white; border: none; width: 299px; height: 53px;" />
                                <asp:ImageButton ImageAlign="Right" ImageUrl="~/img/menu-cart.png" ID="ImageButton1" runat="server" Style="margin-top: 5px" OnClick="ImageButton1_Click" Width="21px" CssClass="div_carrito" />
                                <asp:HyperLink ID="lblNumArt" runat="server" CssClass="div_itemcarrito" ForeColor="White" BackColor="Red"></asp:HyperLink>
                            </div>
                            <div class="collapse navbar-collapse" id="myNavbar">
                                <ul class="nav navbar-nav" style="float: left;">
                                    <li style="text-align: center">
                                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="btnBuscar" runat="server" Width="100px" CssClass="btn btn-default" Text="<%$ Resources:Resource, Buscar%>" OnClick="btnBuscar_Click" />&nbsp;<br />

                                    </li>
                                    <li style="text-align: center">
                                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="C9" runat="server" Text="C9" Width="100px" OnClick="btnCliente9_Click" CssClass="btn btn-default" />&nbsp;<br />
                                    </li>
                                    <li style="text-align: center">
                                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="SOLICITUDES" runat="server" Width="100px" Text="Solicitudes" CssClass="btn btn-default" OnClick="SOLICITUDES_Click" />&nbsp;<br />
                                    </li>
                                    <li style="text-align: center">
                                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="btnRegresar" runat="server" Width="100px" CssClass="btn btn-default" Text="Ir a Inicio" OnClick="btnRegresar_Click" />&nbsp;<br />

                                    </li>
                                </ul>
                            </div>
                        </div>
                    </nav>
                    <div>

                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="btnComplemento" runat="server" Text="Button" OnClick="btnComplemento_Click" Style="visibility: collapse;" CssClass="btn btn-info" />
                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="btnCabecera" runat="server" Text="Button" OnClick="btnCabecera_Click" Style="visibility: collapse;" CssClass="btn btn-info" />
                        <asp:Button Font-Bold="true" Font-Size="Smaller" ID="btnSustituto" runat="server" Text="Button" OnClick="btnSustituto_Click" Style="visibility: collapse;" CssClass="btn btn-info" />
                        <asp:Panel ID="Panel1" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="row">
                <center>
               <div>
                <div >
                    <div id="tabs" runat="server">
                        <table id="Table1" runat="server">
                            <tr>
                                <td>
                                    <panel runat="server" ActiveTabIndex="0" ID="TabArticulosCS" AutoPostBack="true" Height="100%" Visible="false" BorderColor="Transparent" CssClass="" OnActiveTabChanged="TabArticulosCS_ActiveTabChanged">
                                        <div ID="TabArt1" runat="server" HeaderText="articulo 1" >
                                        </div>
                                    </panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <panel runat="server" ActiveTabIndex="0" ID="TabSustitutoCS" AutoPostBack="true" OnActiveTabChanged="TabSustitutoCS_ActiveTabChanged" Height="100%" Visible="false" BorderColor="Transparent" ClientIDMode="Static" CssClass="">
                                        <div ID="TabSustituto1" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                        <div ID="TabSustituto2" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                        <div ID="TabSustituto3" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                        <div ID="TabSustituto4" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                        <div ID="TabSustituto5" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                        <div ID="TabSustituto6" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                        </div>
                                    </panel>
                                    <panel runat="server" ActiveTabIndex="2" ID="tabComplementosCS" AutoPostBack="true" OnActiveTabChanged="tabComplementosCS_ActiveTabChanged" Height="100%" Visible="false" BorderColor="Transparent" ClientIDMode="Static" CssClass="">
                                        <div ID="TabComplemento1" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                        <div ID="TabComplemento2" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                        <div ID="TabComplemento3" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                        <div ID="TabComplemento4" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                        <div ID="TabComplemento5" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                        <div ID="TabComplemento6" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                        </div>
                                    </panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <panel runat="server" ActiveTabIndex="0" ID="TabArticulos" CssClass="">
                                        <div ID="tabArticulo0" runat="server" HeaderText="articulo 1">
                                            <div>
                                                <uc1:UCEleccionProducto runat="server" ID="EP0" />
                                            </div>
                                        </div>
                                    </panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <panel runat="server" ActiveTabIndex="0" ID="tabComplementos"  Visible="false" CssClass="">
                                        <div ID="Complemento0" runat="server" HeaderText="Complemento 1">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC1" />
                                            </div>
                                        </div>
                                        <div ID="Complemento1" runat="server" HeaderText="Complemento 2">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC2" />
                                            </div>
                                        </div>
                                        <div ID="Complemento2" runat="server" HeaderText="Complemento 3">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC3" />
                                            </div>
                                        </div>
                                        <div ID="Complemento3" runat="server" HeaderText="Complemento 4">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC4" />
                                            </div>
                                        </div>
                                        <div ID="Complemento4" runat="server" HeaderText="Complemento 5">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC5" />
                                            </div>
                                        </div>
                                        <div ID="Complemento5" runat="server" HeaderText="Complemento 6">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC6" />
                                            </div>
                                        </div>
                                    </panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <panel runat="server" ActiveTabIndex="0" ID="TabSustituto" ClientIDMode="Static" Visible="false" CssClass="">
                                        <div ID="Sustituto0" runat="server" HeaderText="Sustituto 1">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS1" />
                                            </div>
                                        </div>
                                        <div ID="Sustituto1" runat="server" HeaderText="Sustituto 2">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS2" />
                                            </div>
                                        </div>
                                        <div ID="Sustituto2" runat="server" HeaderText="Sustituto 3">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS3" />
                                            </div>
                                        </div>
                                        <div ID="Sustituto3" runat="server" HeaderText="Sustituto 4">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS4" />
                                            </div>
                                        </div>
                                        <div ID="Sustituto4" runat="server" HeaderText="Sustituto 5">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS5" />
                                            </div>
                                        </div>
                                        <div ID="Sustituto5" runat="server" HeaderText="Sustituto 6">
                                            <div>
                                                <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS6" />
                                            </div>
                                        </div>
                                    </panel>
                                </td>
                            </tr>
                        </table>
                        <c9:UCCliente9 runat="server" Visible="false" Id="ucC9">
                        </c9:UCCliente9>
                    </div>
                </div>
                <br />
                <br />
                <asp:SqlDataSource ID="AVE_StockEnTiendaCSObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                    SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure"
                    DataSourceMode="DataSet">
                    <SelectParameters>
                        <asp:Parameter Name="IdArticulo" Type="String" />
                        <asp:Parameter Name="IdTienda" Type="String" />
                        <asp:Parameter Name="Tipo" Type="Char" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
           </center>
            </div>
            <br />
    </form>
</body>
</html>
