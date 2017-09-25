<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AVE.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>AVE</title>
        <link href="css/estilos.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            
            .frmLogin 
            {
                width: 400px;
                height:auto;
                margin-top: 5%;
                text-align:center;
            }
            
            .tablaLogin 
            {
                width: 350px;
                height: auto;
                margin-left:auto; 
                margin-right: auto;
                background-color: #000000;
                color: White;
                font-weight: bold;
            }
            
        </style>

    <script src="js/json2.js" type="text/javascript"></script>
    <script src="js/jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="js/jstorage.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>

</head>
<body onLoad="document.form1.txtLogin.focus()">
    <form id="form1" runat="server" defaultbutton="btnLogin">

        <asp:HiddenField runat="server" ID="hidNombreMaquina" ClientIDMode="Static"/>
        <asp:HiddenField runat="server" ID="hidSesionActivaTpv" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidStringUrl" />

        <div class="frmLogin centered" runat="server" id="frmLogin">
            <img src="img/ninewest_s.png"  alt="NineWest"/>
<%--            <div id="divBanderas" style="margin-top: 5px; padding-top: 5px;">
                <asp:ImageButton ID="imgES" runat="server" onclick="imgES_Click" ImageUrl="~/img/es.png" style="height:20px;"/>
                &nbsp;
                <asp:ImageButton ID="imgEN" runat="server" onclick="imgEN_Click" ImageUrl="~/img/en.png" style="height:20px;"/>
                &nbsp;
                <asp:ImageButton ID="imgCH" runat="server" onclick="imgCH_Click" ImageUrl="~/img/cn.png" style="height:20px;"/>
                <br />
            </div>
--%>            <br />
            <div id="divLogin">
                <table class="tablaLogin" cellpadding="4" cellspacing="0">
                    <tr>
                        <td style="width: 200px;">
                            USUARIO:<br />
                            <asp:TextBox ID="txtLogin" Width="100%"  runat="server" TabIndex="1" type="number" onfocus="establecerControlFoco(this);" ></asp:TextBox>
                        </td>
                        <td rowspan="2" style="text-align:center;">
                            <asp:Button ID="btnLogin" runat="server" Width="90px" Font-Bold="true"  TabIndex="3" Text="<%$ Resources:Resource, Login%>"  onclick="btnLogin_Click"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            PASSWORD: <br />
                            <asp:TextBox ID="txtPassword" Width="100%" runat="server" TabIndex="2" TextMode="Password" onfocus="establecerControlFoco(this);" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
<%--                            <div style="text-align:center; width:100%;">
                                <br />
                                <img src='img/info.png' style="height: 32px; width:32px;" alt="info"/>
                                <br />
                                <asp:Label runat="server" ID="lblIP"></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="lblMaquina"></asp:Label>
                            </div>
--%>                        </td>
                    </tr>
                </table>
            </div>
            <div id="divTerminal" class="tablaLogin" style="padding: 15px 2px 15px 2px; display:none;">
                <p>Debe introducir un identificador de terminal</p>
                <asp:TextBox runat="server" ID="txtTerminal" ClientIDMode="Static" Width="150px"></asp:TextBox>
                &nbsp;<input type="button" id="cmdTerminal" value="Guardar" style="font-weight:bold;"  />
                <br />
                <br />
            </div>
            <div id="divSesionCerrada" style="display:none;">
                <p><strong>Sesion de TPV inactiva, favor de iniciar sesion desde el Punto de Venta.</strong></p>
                <asp:button runat="server" id="btnActualizar" Text="Actualizar página" />
            </div>
            <a href="Login.aspx" id="lnkEliminarTerminal" >Eliminar valor del terminal</a>
            <div style="text-align: center; padding-top: 5px;">
                <img src="img/wedoshoe.png"  alt="We do shoe"/>
            </div>
        </div>
    </form>
</body>
</html>
