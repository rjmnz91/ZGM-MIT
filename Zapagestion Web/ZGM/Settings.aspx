<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="AVE.Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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

    <script type="text/javascript">

        function saveIdTerminal () {
            var terminal = $('#txtTerminal').val().toString().trim();

            if (terminal != null && terminal != "") {
                if (confirm("[ " + terminal + " ] \n\n¿ Establecer este valor como identificador de Terminal ?")) {
                    $.jStorage.set(key, terminal);
                    location.reload(true);
                }
            }
            else {
                alert("El identificador introducido no es válido.");
                $('#txtTerminal').focus();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hidNombreMaquina" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidSesionActivaTpv" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidStringUrl" />
        <asp:HiddenField runat="server" ID="hidSettingsUrl" />

        <div class="row">
            <nav class="navbar navbar-inverse">
                <div class="col-sm-10 col-lg-10 col-md-10" style="text-align:center; padding-left:10em;">
                    <img src="img/ninewest_s.png" alt="ninewest" style="background-color:white; border:none;" />
                    <%--<a href="Login.aspx" id="lnkEliminarTerminal"style="padding-left:10em;">
                        <img  src="img/terminal.png" title="Eliminar Terminal" alt="EliminarTerminal" style="height:75px; width:75px;" >
                        </img>
                        </a>--%>
                    <asp:ImageButton runat="server" ID="lnkEliminarTerminal" Height="75px" Width="75px" OnClick="lnkEliminarTerminal_Click" ImageUrl="~/img/terminal.png" />
                </div>
            </nav>
        </div>

        <div class="row">
            <div class="col-sm-12 col-md-12" style="text-align:center">
                <div id="divTerminal" class="alert alert-info">
                    <p><strong>Debe introducir un identificador de terminal</strong></p>
                    <center><asp:TextBox runat="server" ID="txtTerminal" ClientIDMode="Static" CssClass="form-control" Width="45%"></asp:TextBox></center><br />
                    <input type="button" id="cmdTerminal" runat="server" value="Guardar" class="btn fondoNegro" style="color: white; width: 45%; font-weight: bold;" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>