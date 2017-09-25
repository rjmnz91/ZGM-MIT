<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AVE.Inicio" MaintainScrollPositionOnPostback="true" %>

<%--  --%>
<html>
<head id="Head1" runat="server">
    <%--Estilos generales (IE Mobile, IE, Firefox)--%>
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/sticky-footer.css" rel="stylesheet" type="text/css" />

    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="js/json2.js" type="text/javascript"></script>
    <script src="js/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="js/jstorage.js" type="text/javascript"></script>
    <script src="js/Inicio.js" type="text/javascript"></script>
    <script src="js/jquery.barcodelistener-1.1.js"></script>
    <script src="js/jquery-ui-personalized-1.5.2.min.js"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="js/jquery-3.2.1.js"></script>
    <script src="js/jquery-1.2.6.min.js"></script>
    <script src="js/jquery-1.11.0.min.js"></script>

    <%--Mover a su fichero de estilos--%>
    <style type="text/css">
                /************************************************************************/
        /* PSEUDO-TOGGLE BUTTON MADE OF ASP.NET CHECKBOX AND CSS3*/
        div.divToggleButton input[type=checkbox]
        {
            display: none;
            white-space: nowrap;
        }
        div.divToggleButton label
        {
            display: block;
            float: left;
            cursor: pointer;
        }

        /* set the size of the pseudo-toggle button control */
        div.divToggleButton input[type=checkbox]:checked + label::before,
        div.divToggleButton input[type=checkbox]:not(:checked) + label::before,
        div.divToggleButton input[type=checkbox] + label
        {
            width: 100px;
            line-height: 24px;
        }

        /* additional styling: rounded border, gradient */
        div.divToggleButton input[type=checkbox] + label
        {
            vertical-align: middle;
            text-align:center;
            font-size: medium;
            font-family:Arial, Calibri;
            border: 1px solid #bdbdbd;
            border-radius: 5px;
            background: #f0f0f0;
            /* gradient style (optional)*/
            background-image: -moz-linear-gradient(top, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            background-image: -webkit-gradient(linear, center top, center bottom,
            from(#fdfdfd), color-stop(0.5, #f9f9f9), color-stop(0.5, #e5e5e5 ), to(#fdfdfd));
            background-image: linear-gradient(to bottom, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
        }

        /* content to display and style pertinent to unchecked state*/
        div.divToggleButton input[type=checkbox]:not(:checked) + label::before
        {
            content: "\2610\0020 Buscar";
            color: #303030;
            opacity: 0.6;
        }

        /* content to display and style pertinent to checked state*/
        div.divToggleButton input[type=checkbox]:checked + label::before
        {
            content         : "\2611\0020Vender";
            color           : #4ca828;
            font-weight     : bold;
        }
        /************************************************************************/
             .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }

        .onoffswitch-label {
            display: block;
            overflow: hidden;
            cursor: pointer;
            border: 2px solid #999999;
            border-radius: 20px;
        }

        .onoffswitch-inner {
            display: block;
            width: 200%;
            margin-left: -100%;
            transition: margin 0.3s ease-in 0s;
        }

            .onoffswitch-inner:before, .onoffswitch-inner:after {
                display: block;
                float: left;
                width: 50%;
                height: 30px;
                padding: 0;
                line-height: 30px;
                font-size: 12px;
                color: white;
                font-family: Trebuchet, Arial, sans-serif;
                font-weight: bold;
                box-sizing: border-box;
            }

            .onoffswitch-inner:before {
                content: "Comprar";
                padding-left: 10px;
                background-color: #4AC234;
                color: #FFFFFF;
            }

            .onoffswitch-inner:after {
                content: "Consultar";
                padding-right: 10px;
                background-color: #34A7C1;
                color: #FFFFFF;
                text-align: right;
            }

        .onoffswitch-switch {
            display: block;
            width: 30px;
            margin: 0px;
            background: #FFFFFF;
            position: absolute;
            top: 0;
            bottom: 0;
            right: 81px;
            border: 2px solid #999999;
            border-radius: 20px;
            transition: all 0.3s ease-in 0s;
        }

        .onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-inner {
            margin-left: 0;
        }

        .onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-switch {
            right: 0px;
        }

        .highlight {
            background-color: #353C45;
            color: White;
        }

        .tdOpciones {
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }
    </style>

    <script src="js/inicio.js" type="text/javascript"></script>
    <script type="text/javascript">
        function submitDetailsForm() {
            $("#scann").submit();
        }
        function LanzaScanner() {
            $('input#sbtEscanear').trigger('click');
        }

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        function mostrar_procesar() {
            document.getElementById('procesando_div').style.display = "";
            setTimeout('document.images["procesando_gif"].src="img/ajax-loader.gif"', 200);
        }
        function doHourglass() {
            document.body.style.cursor = 'wait';
        }

        function proxyFunction() {
            var x = document.getElementById("myCheck").checked;
            if (x == true) {
                alert(x + '');
            }
        }

    </script>

</head>
<body>
    <div style="display: none">
        <form id="scann" action="sendScanReader" name="sendScanReader" method="post">
            <!-- Este action para hacer la llamada al lector -->
            <input type="hidden" name="dataType" id="dataType" value="numeric" /><!-- Tipo de dato que quieres leer, puede ser numeric o alfanumeric -->
            <input type="hidden" name="dataLength" id="dataLength" value="100" /><!-- Tamaño del dato que quieres leer-->
            <input type="hidden" name="inputTextNameToReturnData" id="inputTextNameToReturnData" value="txtBuscar" /><!-- input donde quieres que se escriba la respuesta del lector-->
            <input name="sbtEscanear" type="button" value="Escanear" onclick="submitDetailsForm()" id="sbtEscanear" />
        </form>
    </div>

    <form id="form1" runat="server">


        <div class="row">
            <div class="col-lg-12 col-sm-12 col-md-12">
                <nav class="navbar navbar-inverse">
                    <table class="container">
                        <tr>
                            <td style="width: 10%">
                                <table>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:Image runat="server" ImageUrl="img/contact.png" ImageAlign="AbsMiddle" Style="height: 35px; width: 30px;" AlternateText="Usuario" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblUser" ForeColor="White" runat="server" Text="Usuario:" Font-Size="XX-Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align:center">
                                <asp:Image runat="server" ID="imgLogo" AlternateText="ninewest" ImageUrl="img/ninewest_s.png" />
                            </td>
                            <td style="width: 10%;">
                                <asp:HyperLink ID="lblNumArt" runat="server" CssClass="div_itemcarrito" ForeColor="White" BackColor="Red" Style="position: absolute"></asp:HyperLink>
                                <asp:ImageButton ImageUrl="~/img/menu-cart.png" ID="lnkCarrito" runat="server" CssClass="div_carrito" OnClick="lnkCarrito_Click" Width="35px" Height="30px" />
                            </td>
                            <td style="width: 10%;">
                                <table>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:ImageButton ImageUrl="~/img/logoff.png" ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" Width="45px" Height="40px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <label style="font-size: x-small; color: white;" onclick="lnkLogout_Click">Logout</label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </nav>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 col-md-12" style="text-align: center">
                <div class="divToggleButton container">
                    <asp:CheckBox ID="chkToggleButton" runat="server" AutoPostBack="true" OnCheckedChanged="myonoffswitch_CheckedChanged"/>
                    <asp:Label ID="lblToggleButton" ForeColor="Black"  Font-Size="Smaller" AssociatedControlID="chkToggleButton" runat="server"/>
                    <asp:ImageButton runat="server" ID="BtnScanner" Width="24px" ImageUrl="~/img/lector.png" Height="24px" Style="margin-top: 0px"
                        OnClick="BtnScanner_Click" />
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="btn btn-default" Style="width: 45%;" OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <br />
            </div>
        </div>
        <br />
        <asp:Panel runat="server" Visible="false" ID="pnlDatos" CssClass="container">
            <div class="panel panel-primary">
                <div class="panel-heading"><strong>Detalle del producto</strong></div>
                <div class="panel-body">
                    <table style="width:100%">
                        <tr>
                            <td>
                                <asp:Label Style="text-transform:uppercase"  ID="LtrProeveedor" runat="server" Text="Marca" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-left:3em;">
                                <asp:Label Style="text-transform:uppercase" ID="lblProveedor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color:paleturquoise;">
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="LtrReferencia" runat="server" Text="Codigo Alfa" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-left:3em;">
                                <asp:Label Style="text-transform:uppercase" ID="lblReferencia" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="LtrDescripcion" runat="server" Text="<%$ Resources:Resource, Descripcion%>"  Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-left:3em;">
                                <asp:Label Style="text-transform:uppercase" ID="lblDescripcion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color:paleturquoise;">
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="LtrColor" runat="server" Text="<%$ Resources:Resource, Color%>" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-left:3em;">
                                <asp:Label Style="text-transform:uppercase" ID="lblColor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="visibility: collapse;">
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="LtrModelo" runat="server" Text="<%$ Resources:Resource, Modelo %>"  Visible="False" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="lblModelo" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td>
                                <asp:Label Style="text-transform:uppercase" ID="LtrTalla" runat="server" Text="Talla" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-left:3em;">
                                <asp:Label Style="text-transform:uppercase" ID="lblTalla" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:10em;">
                                <asp:ImageButton ImageAlign="Right" ImageUrl="~/img/buscar.png" Width="45px" Height="40px" runat="server" ID="lnkBuscar" Text=' <%$ Resources:Resource, Buscar%> ' OnClick="lnkBuscar_Click" />
                                </td>
                            <td style="padding-right:10em;">
                                <asp:ImageButton ImageAlign="Right" ImageUrl="~/img/carro.png" runat="server" ID="lnkAniadir" Width="45px" Height="40px" OnClick="lnkAniadir_Click" Text="Añadir" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlEmpty" CssClass="container">
            <div class="panel panel-info">
                <div class="panel-heading"><strong>Lo sentimos</strong></div>
                <div class="panel-body">
                    <table style="width:100%">
                        <tr>
                            <td></td><td></td><td></td>
                        </tr>
                        <tr><td></td>
                            <td style="text-align:center">
                                No se ha encontrado información acerca del producto que ha escaneado. <br />Por favor intentelo nuevamente
                            </td>
                            <td></td></tr>
                        <tr><td></td><td></td><td></td></tr>
                        <tr><td></td>
                            <td style="text-align:center">
                                <asp:Button runat="server" CssClass ="btn btn-info" Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_Click"/>
                            </td>
                            <td></td></tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <br />
        <asp:SqlDataSource ID="AVE_ArticuloDetalleObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
            SelectCommand="SELECT 
   dbo.PROVEEDORES.Nombre AS Proveedor,
   dbo.ARTICULOS.CodigoAlfa as Referencia,
   dbo.ARTICULOS.ModeloProveedor + ' (' + dbo.ARTICULOS.DescripcionFabricante + ')' AS Modelo,
   dbo.ARTICULOS.ModeloProveedor,
   dbo.ARTICULOS.Descripcion AS Descripcion,
   dbo.COLORES.Color AS Color,
   cb.Nombre_Talla as Talla,
   dbo.ARTICULOS.Observaciones,
    ( SELECT TOP 1 PrecioEuro FROM
   dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
   ORDER BY Fecha DESC) AS Precio,
CASE 
                  WHEN ArticulosCS.Observaciones &lt;&gt;'Obs:'
                     THEN ArticulosCS.Observaciones
                  ELSE '' 
             ENd AS CS_OBS     
 FROM
   dbo.ARTICULOS LEFT JOIN 
   dbo.PROVEEDORES ON dbo.ARTICULOS.IdProveedor = dbo.PROVEEDORES.IdProveedor LEFT JOIN 
   dbo.COLORES on dbo.COLORES.IdColor = dbo.ARTICULOS.IdColor
 left JOIN
             dbo.ArticulosCS ON 
     CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + 'ý' + 
     CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + 'ý' + 
     CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50)) = 
     dbo.ArticulosCS.Articulo2
inner join CABECEROS c on c.Id_Cabecero = ARTICULOS.Id_Cabecero
inner join CABECEROS_DETALLES cb on cb.Id_Cabecero = c.Id_Cabecero
inner JOIN ean e on ARTICULOS.IdArticulo = e.IdArticulo and e.IdCabeceroDetalle = cb.Id_Cabecero_Detalle
where e.EAN = @IdArticulo">
            <SelectParameters>
                <asp:Parameter Name="IdTienda" Type="string" />
                <asp:Parameter Name="IdArticulo" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
