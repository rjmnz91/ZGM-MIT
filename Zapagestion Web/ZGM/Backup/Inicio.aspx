<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AVE.Inicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <%--Estilos generales (IE Mobile, IE, Firefox)--%>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <link href="css/redmond/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>


    <%--Mover a su fichero de estilos--%>
    <style type="text/css"> 
        .highlight { background-color: #353C45; color: White;}
        .tdOpciones { padding-top: 20px; padding-bottom: 20px; text-align:center;}
    </style>

    <script src="js/inicio.js" type="text/javascript" ></script>
    <script type="text/javascript">
        function submitDetailsForm() {
            $("#scann").submit();
        }
        function LanzaScanner() {
            $('input#sbtEscanear').trigger('click');
            
        }
    </script>

</head>
<body>
<div style="display:none" >
<form id="scann" action="sendScanReader" name="sendScanReader" method="post"><!-- Este action para hacer la llamada al lector -->
    <input type="hidden" name="dataType" id="dataType" value="numeric" /><!-- Tipo de dato que quieres leer, puede ser numeric o alfanumeric -->
    <input type="hidden" name="dataLength" id="dataLength" value="100"/><!-- Tamaño del dato que quieres leer-->
    <input type="hidden" name="inputTextNameToReturnData" id="inputTextNameToReturnData" value="txtBuscar"/><!-- input donde quieres que se escriba la respuesta del lector-->
    <input name="sbtEscanear" type="button" value="Escanear" onclick="submitDetailsForm()" id="sbtEscanear" />
</form>
</div>
    <form id="form1" runat="server">

        <table style="margin-left:auto; margin-right:auto; width:60%; margin-top:10px;">
            <tr>
                <td>
                    <img src="img/ninewest_s.png"  alt="NineWest"/>
                </td>
                <td style="text-align:right;">
                    <asp:LinkButton runat="server" id="lnkLogout" Text="Cerrar Sesión" onclick="lnkLogout_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:panel runat="server" ID="pnlBusqyeda" DefaultButton="lnkBuscar" style="text-align:center;" >
            <table style="width: 60%; background-color: #404040; color:White; margin-left: auto; margin-right:auto; " cellpadding="0" cellspacing="0">
                <tr>
                    <td style="padding-left: 8px;">
                        <asp:LinkButton runat="server" ID="lnkCarrito" Text="Carrito" ></asp:LinkButton>
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Label ID="lblUser" runat="server" Text="Usuario:"></asp:Label>
                    </td>
                    <td style="text-align:left;">
                         <asp:Label ID="txtUser" runat="server" ForeColor="White"></asp:Label>
                    </td>
                    <td style=" width:40%; padding: 10px 5px 10px 5px; text-align:right; vertical-align:bottom;" >
                           <asp:ImageButton runat="server" ID="BtnScanner" Width="24px" 
                                ImageUrl="~/img/lector.png" Height="24px" style="margin-top: 0px" 
                                OnClientClick="LanzaScanner(); return false;" />
                            <asp:TextBox runat="server" ID="txtBuscar" Width="80%" CssClass="txtBuscar" ></asp:TextBox>
                    </td>
                    <td style="padding-left: 5px; padding-right: 5px;text-align: right; font-weight:bold;">
                        <asp:LinkButton runat="server" ID="lnkBuscar" Text=' <%$ Resources:Resource, Buscar%> ' OnClick="lnkBuscar_Click"/>
                    </td>
                </tr>
            </table>
        </asp:panel>
        <asp:Panel ID="PanelAviso" runat="server" class="ui-state-highlight" style="width: 60%; margin-left:auto; margin-right:auto; text-align:center;" >
            <br />
            <asp:LinkButton runat="server" ID="lnkVerSolicitudes" Text="Hay Nuevas Solicitudes Pendientes de Tramitar"></asp:LinkButton>
            <br />
            <br />
        </asp:Panel> 
        <table id="tblOpciones" style="width: 60%; margin-left:auto; margin-right: auto; font-weight:bold;" cellspacing="0" cellpadding="0" >
            <tr>
                <td rowspan="3" style="height:1px; width:1px; text-align:left;">
                    <div style="float:left; background-color: #404040; width: 1px; height: 100%;">
                    </div>
                </td>
                <td class="tdOpciones">
                    <asp:ImageButton runat="server" ID="imgSolicitudes" Width="96px" ImageUrl="~/img/solicitudes.png"  />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblSolicitudes" Text='<%$ Resources:Resource, Solicitudes%>'></asp:Label>
                </td>
                <td class="tdOpciones">
<%--                    <asp:ImageButton runat="server" ID="imgPedidos" Width="96px" ImageUrl="~/img/pedidos.png"  />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblEnvios" Text='<%$ Resources:Resource, Pedidos%>'></asp:Label>
--%>                </td>
                <td class="tdOpciones">
                    <asp:ImageButton runat="server" ID="imgCliente9" Width="96px" 
                        ImageUrl="~/img/c9.png"  />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblInventario" Text="Cliente 9"></asp:Label>
                </td>
                  <td class="tdOpciones">
                    <img src="img/encuesta2.png" Width="96px" alt="encuesta"/>
                </td>
                <td rowspan="3" style="height:1px; width:1px; text-align:right;">
                    <div style="float:right; background-color: #DA2977; width: 1px; height: 100%;">
                    </div>
                </td>
            </tr>
            <%--
            <tr>
                <td colspan="5" style="background-color: #DA2977; height:1px;">
                </td>
            </tr>
            <tr>
                <td class="tdOpciones">
                    <asp:ImageButton runat="server" ID="imgCargos" Width="96px" ImageUrl="~/img/traspasos.png"/>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label1" Text='<%$ Resources:Resource, Traspasos%>'></asp:Label>
                </td>
                <td class="tdOpciones">
                    <asp:ImageButton runat="server" ID="imgTraspasoEntrada" Width="96px" ImageUrl="~/img/traspasoentrada.png"  />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label2" Text='<%$ Resources:Resource, TraspasoEntrada%>'></asp:Label>
                </td>
                <td class="tdOpciones" >
                    <asp:ImageButton runat="server" ID="imgCatalogo" Width="96px" ImageUrl="~/img/catalogo.png"  />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label4" Text='<%$ Resources:Resource, Catalogo%>'></asp:Label>
                </td>
            </tr>--%>
        </table>
        <div style="width: 60%; height:1px; background-color: #404040; color:White; margin-left: auto; margin-right:auto; " >
            <div style="text-align:center;">
                <img alt="wedoshowe" src="img/wedoshoe.png" style="height: 24px; margin-top: 8px;" />
            </div>
        </div>

                    <%--                    <asp:Button ID="btnBuscar" runat="server" Text="<%$ Resources:Resource, Buscar%>" CssClass="boton" onclick="btnBuscar_Click" style="width:87px;"/>
                    <asp:Button ID="btnSolicitudes" runat="server" 
                            Text="<%$ Resources:Resource, Solicitudes%>" onclick="btnSolicitudes_Click"  
                            CssClass="boton" Width="91px"/>
                    <asp:Button ID="btnPedidos" runat="server" Text="<%$ Resources:Resource, Pedidos%>" onclick="btnPedidos_Click" CssClass="boton" style="width:87px;"/>
                    <asp:Button ID="btnInventario" runat="server" 
                            Text="<%$ Resources:Resource, Inventario%>" onclick="btnInventario_Click" 
                            CssClass="boton" Width="91px" />
                    <asp:Button ID="btnCargos" runat="server" Text="<%$ Resources:Resource, Traspasos%>" OnClick="btnCargos_Click" CssClass="boton" style="width:87px;"/>
                    <asp:Button ID="btnCargosEntrada" runat="server" 
                            Text="<%$ Resources:Resource, TraspasoEntrada%>" CssClass="boton" 
                            onclick="btnCargosEntrada_Click" Width="91px"/>
                    <asp:Button ID="btnPedidosEntrada" runat="server" Text="<%$ Resources:Resource, PedidosEntrada%>" CssClass="boton" onclick="btnPedidosEntrada_Click" style="width:87px;"/>
                    <asp:Button ID="btnCatalogo" runat="server" Enabled="false" 
                            Text="<%$ Resources:Resource, Catalogo%>" CssClass="boton" Width="91px"/>
--%>
    </form>
</body>
</html>
