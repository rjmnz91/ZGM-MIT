<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AVE.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AVE</title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .frmLogin {
            width: 400px;
            height: auto;
            margin-top: 5%;
            text-align: center;
        }

        .tablaLogin {
            width: 350px;
            height: auto;
            margin-left: auto;
            margin-right: auto;
            background-color: #000000;
            color: White;
            font-weight: bold;
        }
    </style>

    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="js/json2.js" type="text/javascript"></script>
    <script src="js/jquery-3.2.1.js" type="text/javascript"></script>
    <script src="js/jstorage.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>

</head>
<body onload="document.form1.txtLogin.focus()">
    <form id="form1" runat="server" defaultbutton="btnLogin">

        <asp:HiddenField runat="server" ID="hidNombreMaquina" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidSesionActivaTpv" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidStringUrl" />
        <asp:HiddenField runat="server" ID="hidSettingsUrl" />

        

            <div class="row">
                    <nav class="navbar navbar-inverse">
                            <div class="col-sm-10 col-lg-10 col-md-10" style="text-align:center; padding-left:10em;">
                                <img src="img/ninewest_s.png" alt="ninewest" style="background-color:white; border:none;" />
                                <asp:ImageButton runat="server" Width="55px" Height="55px" ID="lnkEliminarTerminal" ImageAlign="Right"  OnClick="lnkEliminarTerminal_Click" ImageUrl="~/img/terminal.png" />
                            </div>
                    </nav>
            </div>
        
            <div class="container" style="text-align:center;">
            
                <div class="row">
                <div class="col-sm-12 col-md-12" >
                    <div id="divLogin">
                        <asp:Label CssClass="header-label" Text="USUARIO:" runat="server"></asp:Label>
                        <center><asp:TextBox ID="txtLogin" CssClass="form-control" runat="server" Width="45%"></asp:TextBox></center><br />
                        <asp:Label CssClass="header-label" Text="PASSWORD:" runat="server"></asp:Label>
                        <center><asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" Width="45%" TextMode="Password"></asp:TextBox></center>
                        <br />
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn fondoNegro" Width="45%" ForeColor="White" Font-Bold="true" Text="<%$ Resources:Resource, Login%>" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
      
                <div class="row">
                <div class="col-sm-12 col-md-12" style="text-align:center">
                    <div id="divTerminal" style="display:none;" class="alert alert-info">
                        <p><strong>Debe introducir un identificador de terminal</strong></p>
                        <center><asp:TextBox runat="server" ID="txtTerminal" ClientIDMode="Static" CssClass="form-control" Width="45%"></asp:TextBox></center><br />
                        <input type="button" id="cmdTerminal" value="Guardar" class="btn fondoNegro" style="color: white; width: 45%; font-weight: bold;" />
                    </div>
                </div>

            </div>
                    
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <div style="display:none;" class="alert alert-warning">
                            <br />
                            <p><strong>Sesion de TPV inactiva. Favor de iniciar sesión desde el Punto de Venta</strong></p>
                            <asp:Button runat="server" ID="btnActualizar" CssClass="btn fondoNegro" Width="45%" ForeColor="White" Font-Bold="true" Text="Actualizar Página" />
                        </div>
                    </div>
                </div>
            </div>


    <footer style="text-align:end" >
        <br /><br /><br /><br /><center><img  src="img/wedoshoe.png" alt="We do shoe" /></center>
    </footer>
        </form>
</body>
</html>
