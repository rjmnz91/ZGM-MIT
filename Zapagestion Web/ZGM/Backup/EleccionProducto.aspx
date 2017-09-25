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
    <title>ELECCIÓN PRODUCTO</title>
    <!--Hay que hacerlo del siguiente modo ya que estos hacks no los reconoce 
        IE Mobile por lo que no es válido poner ni "if IE" ni "if !IE". En ambos 
        casos no recoge la hoja de estilo.  Además tampoco se puede reconocer si
        nos encontramos en Firefox para aplicar las CSS por lo cual tendremos que
        aplicar la hoja de estilos para navegadores WEB en 2 casos: para IE y otra 
        para !IE, es decir, Firefox, Chrome, etc... auqnue se trate de la misma hoja--%>
    Estilos generales (IE Mobile, IE, Firefox)-->
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <!--Estilos IE-->
    <!--[if IE]>
            <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />
        <![endif]-->
    <!--Estilos Firefox-->
    <style type="text/css">
        .style2
        {
            width: 244px;
        }
        
        
        .ajax__tab_default .ajax__tab_header
        {
            white-space: nowrap;
        }
        .ajax__tab_default .ajax__tab_outer
        {
            display: -moz-inline-box;
            display: inline-block;
        }
        .ajax__tab_default .ajax__tab_inner
        {
            display: -moz-inline-box;
            display: inline-block;
        }
        .ajax__tab_default .ajax__tab_tab
        {
            margin-right: 4px;
            overflow: hidden;
            text-align: center;
            cursor: pointer;
            display: -moz-inline-box;
            display: inline-block;
        }
        
        
        /* tab style */
        .myCustomstyle .ajax__tab_header
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            background: url('img/Tabs/tab-line.gif') repeat-x bottom;
        }
        .myCustomstyle .ajax__tab_outer
        {
            padding-right: 4px;
            background: url('img/Tabs/tab-right.gif') no-repeat right;
            height: 21px;
        }
        .myCustomstyle .ajax__tab_inner
        {
            padding-left: 3px;
            background: url('img/Tabs/tab-left.gif') no-repeat;
        }
        .myCustomstyle .ajax__tab_tab
        {
            height: 13px;
            padding: 4px;
            margin: 0;
            background: url('img/Tabs/tab.gif') repeat-x;
        }
        .myCustomstyle .ajax__tab_hover .ajax__tab_outer
        {
            background: url('img/Tabs/tab-hover-right.gif') no-repeat right;
        }
        .myCustomstyle .ajax__tab_hover .ajax__tab_inner
        {
            background: url('img/Tabs/tab-hover-left.gif') no-repeat;
        }
        .myCustomstyle .ajax__tab_hover .ajax__tab_tab
        {
            background: url('img/Tabs/tab-hover.gif') repeat-x;
        }
        .myCustomstyle .ajax__tab_active .ajax__tab_outer
        {
            background: url('img/Tabs/tab-active-right.gif') no-repeat right;
        }
        .myCustomstyle .ajax__tab_active .ajax__tab_inner
        {
            background: url('img/Tabs/tab-active-left.gif') no-repeat;
        }
        .myCustomstyle .ajax__tab_active .ajax__tab_tab
        {
            background: url('img/Tabs/tab-active.gif') repeat-x;
        }
        .myCustomstyle .ajax__tab_body
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 1px solid #999999;
            border-top: 0;
            padding: 8px;
            background-color: #ffffff;
            float: left;
            width: 99%;
        }
        
        .myCustomstyle2 .ajax__tab_header
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
        }
        .myCustomstyle2 .ajax__tab_outer
        {
            padding-right: 4px;
            background: url('img/Tabs/tab-right.gif') no-repeat right;
            height: 21px;
        }
        .myCustomstyle2 .ajax__tab_inner
        {
            padding-left: 3px;
            background: url('img/Tabs/tab-left.gif') no-repeat;
        }
        .myCustomstyle2 .ajax__tab_tab
        {
            height: 13px;
            padding: 4px;
            margin: 0;
            background: url('img/Tabs/tab.gif') repeat-x;
        }
        .myCustomstyle2 .ajax__tab_hover .ajax__tab_outer
        {
            background: url('img/Tabs/tab-hover-right.gif') no-repeat right;
        }
        .myCustomstyle2 .ajax__tab_hover .ajax__tab_inner
        {
            background: url('img/Tabs/tab-hover-left.gif') no-repeat;
        }
        .myCustomstyle2 .ajax__tab_hover .ajax__tab_tab
        {
            background: url('img/Tabs/tab-hover.gif') repeat-x;
        }
        .myCustomstyle2 .ajax__tab_active .ajax__tab_outer
        {
        }
        .myCustomstyle2 .ajax__tab_active .ajax__tab_inner
        {
        }
        .myCustomstyle2 .ajax__tab_active .ajax__tab_tab
        {
        }
        .myCustomstyle2 .ajax__tab_body
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 0px solid #999999;
            border-top: 0;
            padding: 0px;
            background-color: #ffffff;
            float: left;
        }
        .texto_blanco
        {
            color:White;
        }
        /* scrolling */
        .ajax__scroll_horiz
        {
            overflow-x: scroll;
        }
        .ajax__scroll_vert
        {
            overflow-y: scroll;
        }
        .ajax__scroll_both
        {
            overflow: scroll;
        }
        .ajax__scroll_auto
        {
            overflow: auto;
        }
    </style>
    <!--[if !IE]><!-->
    <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />
    <!--<![endif]-->

    <script src="js/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script type="text/javascript">
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
<div style="display:none">
    <form id="scannPro" action="sendScanReader" name="sendScanReader" method="post"><!-- Este action para hacer la llamada al lector -->
        <input type="hidden" name="dataType" id="dataType" value="numeric" /><!-- Tipo de dato que quieres leer, puede ser numeric o alfanumeric -->
        <input type="hidden" name="dataLength" id="dataLength" value="100"/><!-- Tamaño del dato que quieres leer-->
        <input type="hidden" name="inputTextNameToReturnData" id="inputTextNameToReturnData" value="txtBusquedaProducto"/><!-- input donde quieres que se escriba la respuesta del lector-->
        <input name="sbtEscanear" type="button" value="Escanear" onclick="submitDetailsForm()" id="sbtEscanear" />
    </form>
</div>

    <form id="form1" runat="server" enableviewstate="true" style="width: 99%;" defaultbutton="btnBuscar">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false">
    </asp:ToolkitScriptManager>

    <asp:Button ID="btnCabecera" runat="server" Text="Button" OnClick="btnCabecera_Click"
        Style="visibility: collapse;" />
    <asp:Button ID="btnSustituto" runat="server" Text="Button" OnClick="btnSustituto_Click"
        Style="visibility: collapse;" />
    <asp:Button ID="btnComplemento" runat="server" Text="Button" OnClick="btnComplemento_Click"
        Style="visibility: collapse;" />
   
    <table width="99%">
        <tr>
            <td>
                <div class="cabFondoAzul" style="width: 66%; float:left">
                    <asp:Button ID="btnVolver" runat="server" TabIndex="1" CssClass="botonNavegacion"
                        Text="<%$ Resources:Resource, Volver%>" OnClick="btnVolver_Click" />
                 <asp:ImageButton runat="server" ID="BtnScanner" Width="24px" 
                                ImageUrl="~/img/lector.png" Height="24px" style="margin-top: 0px" 
                                OnClientClick="LanzaScanner(); return false;" />
                    <asp:TextBox ID="txtBusquedaProducto" runat="server" TabIndex="2" CssClass="txtBuscar"
                        MaxLength="50" Style="width: 100px;"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" TabIndex="3" CssClass="botonNavegacion"
                        Text="<%$ Resources:Resource, Buscar%>" OnClick="btnBuscar_Click" />
                    <asp:Button ID="C9" runat="server" Text="C9" OnClick="btnCliente9_Click" CssClass="botonNavegacion" />
                    <asp:Button ID="SOLICITUDES" runat="server" Text="Solicitudes" 
                        CssClass="botonNavegacion" onclick="SOLICITUDES_Click" />
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </div>
                <div class="cabFondoAzul" style="float:left;width: 33%; height:28px">     
                <asp:Label ID="lblUser" runat="server" cText="Usuario:" CssClass="texto_blanco" 
                        Text="Usuario:"></asp:Label>
                    <asp:Label ID="txtUser" runat="server" CssClass="texto_blanco"></asp:Label>               
                    <asp:ImageButton ImageUrl="~/img/carro.png" ID="ImageButton1" runat="server" style="margin-top:5px" 
                        onclick="ImageButton1_Click" Width="21px"  CssClass="div_carrito" />
                    <asp:HyperLink ID="lblNumArt" runat ="server" CssClass="div_itemcarrito" ForeColor="White" BackColor="Red" ></asp:HyperLink>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="PanelAviso" runat="server" Visible="false">
                    <marquee bgcolor="#FFFFFF" scrolldelay="0 " id="TextoMovil" style="width: 99%"> 
             <h3 id="Mensaje"  style="color: black; font-family:Arial, Verdana;">Hay Nuevas Solicitudes Pendientes de Tramitar</h3>
             </marquee>
                    <script type="text/javascript">
                        var filename = "../Sonidos/Alarma.wav";
                        if (navigator.appName == "Microsoft IE Mobile")
                            document.writeln('<BGSOUND SRC="' + filename + '">');
                        else
                            document.writeln('<EMBED SRC="' + filename + '" AUTOSTART=TRUE WIDTH=0 HEIGHT=0>');
                    </script>
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tabs" width="99%" runat="server">
                    <tr>
                        <td>
                            <asp:TabContainer runat="server" ActiveTabIndex="0" ID="TabArticulosCS" AutoPostBack="true"
                                Width="160px" Height="100%" Visible="false" BorderColor="Transparent" CssClass="myCustomstyle2"
                                OnActiveTabChanged="TabArticulosCS_ActiveTabChanged">
                                <asp:TabPanel ID="TabArt1" runat="server" HeaderText="articulo 1" OnClientClick="CargarTab">
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TabContainer runat="server" ActiveTabIndex="0" ID="TabSustitutoCS" AutoPostBack="true"
                                OnActiveTabChanged="TabSustitutoCS_ActiveTabChanged" Width="100%" Height="100%"
                                Visible="false" BorderColor="Transparent" ClientIDMode="Static" CssClass="myCustomstyle2">
                                <asp:TabPanel ID="TabSustituto1" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 1
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabSustituto2" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 2
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabSustituto3" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 3
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabSustituto4" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 4
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabSustituto5" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 5
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabSustituto6" runat="server" HeaderText="Sustituto 1" OnClientClick="SustitutoVisible">
                                    <headertemplate>
                            Sustituto 6
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            <asp:TabContainer runat="server" ActiveTabIndex="2" ID="tabComplementosCS" AutoPostBack="true"
                                OnActiveTabChanged="tabComplementosCS_ActiveTabChanged" Width="100%" Height="100%"
                                Visible="false" BorderColor="Transparent" ClientIDMode="Static" 
                                CssClass="myCustomstyle2">
                                <asp:TabPanel ID="TabComplemento1" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 1
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabComplemento2" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 2
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabComplemento3" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 3
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabComplemento4" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 4
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabComplemento5" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 5
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabComplemento6" runat="server" HeaderText="Complemento 1" OnClientClick="ComplementoVisible">
                                    <headertemplate>
                            Complemento 6
                        </headertemplate>
                                    <contenttemplate>
                        </contenttemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TabContainer runat="server" ActiveTabIndex="0" ID="TabArticulos" Width="99%"
                                CssClass="myCustomstyle">
                                <asp:TabPanel ID="tabArticulo0" runat="server" HeaderText="articulo 1">
                                    <headertemplate>
                                        Articulo 1
                                    
</headertemplate>
                                    


<contenttemplate>
<uc1:UCEleccionProducto runat="server" ID="EP0" />



                                    
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="tabArticulo1" runat="server" HeaderText="articulo 2">
                                    <headertemplate>
                                        Articulo 2
                                    
</headertemplate>
                                        


<contenttemplate>
                                        <uc1:UCEleccionProducto runat="server" ID="EP1" />
                                    
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="tabArticulo2" runat="server" HeaderText="articulo 3">
                                    <headertemplate>
                                        Articulo 3
                                    
</headertemplate>
                                                


<contenttemplate>
                                        <uc1:UCEleccionProducto runat="server" ID="EP2" />
                                    
</contenttemplate>
                                            


</asp:TabPanel>
                                            <asp:TabPanel ID="tabArticulo3" runat="server" HeaderText="articulo 4">
                                                <headertemplate>
                                        Articulo 4
                                    
</headertemplate>
                                    


<contenttemplate>
                                        <uc1:UCEleccionProducto runat="server" ID="EP3" />
                                    
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="tabArticulo4" runat="server" HeaderText="articulo 5">
                                    <headertemplate>
                                        Articulo 5
                                    
</headertemplate>
                                    


<contenttemplate>
                                        <uc1:UCEleccionProducto runat="server" ID="EP4" />
                                    
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="tabArticulo5" runat="server" HeaderText="articulo 6">
                                    <headertemplate>
                                        Articulo 6
                                    
</headertemplate>
                                    


<contenttemplate>
                                        <uc1:UCEleccionProducto runat="server" ID="EP5" />
                                    
</contenttemplate>
                                


</asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TabContainer runat="server" ActiveTabIndex="0" ID="tabComplementos" Width="80%"
                                Visible="false" CssClass="myCustomstyle">
                                <asp:TabPanel ID="Complemento0" runat="server" HeaderText="Complemento 1">
                                    <headertemplate>
                            Complemento 1
                        
</headertemplate>
                                    


<contenttemplate>
<uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC1" />



                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Complemento1" runat="server" HeaderText="Complemento 2">
                                    <headertemplate>
                            Complemento 2
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC2" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Complemento2" runat="server" HeaderText="Complemento 3">
                                    <headertemplate>
                            Complemento 3
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC3" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Complemento3" runat="server" HeaderText="Complemento 4">
                                    <headertemplate>
                            Complemento 4
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC4" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Complemento4" runat="server" HeaderText="Complemento 5">
                                    <headertemplate>
                            Complemento 5
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC5" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Complemento5" runat="server" HeaderText="Complemento 6">
                                    <headertemplate>
                            Complemento 6
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaC6" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TabContainer runat="server" ActiveTabIndex="0" ID="TabSustituto" Width="99%"
                                ClientIDMode="Static" Visible="false" CssClass="myCustomstyle">
                                <asp:TabPanel ID="Sustituto0" runat="server" HeaderText="Sustituto 1">
                                    <headertemplate>
                            Sustituto 1
                        
</headertemplate>
                                    


<contenttemplate>
<uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS1" />



                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Sustituto1" runat="server" HeaderText="Sustituto 2">
                                    <headertemplate>
                            Sustituto 2
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS2" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Sustituto2" runat="server" HeaderText="Sustituto 3">
                                    <headertemplate>
                            Sustituto 3
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS3" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Sustituto3" runat="server" HeaderText="Sustituto 4">
                                    <headertemplate>
                            Sustituto 4
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS4" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Sustituto4" runat="server" HeaderText="Sustituto 5">
                                    <headertemplate>
                            Sustituto 5
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS5" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                                <asp:TabPanel ID="Sustituto5" runat="server" HeaderText="Sustituto 6">
                                    <headertemplate>
                            Sustituto 6
                        
</headertemplate>
                                    


<contenttemplate>
                            <uc1:UCStockEnTienda runat="server" ID="UCStockEnTiendaS6" />
                        
</contenttemplate>
                                


</asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <c9:UCCliente9 runat="server" Visible="false" Id="ucC9">
                </c9:UCCliente9>
            </td>
        </tr>
    </table>
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
    <%--   <asp:SqlDataSource ID="SDSTieneCS" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="AVE_ProductoTieneCS" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
    </form>
</body>
</html>
