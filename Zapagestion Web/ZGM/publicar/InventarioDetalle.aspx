<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventarioDetalle.aspx.cs" Inherits="AVE.InventarioDetalle" EnableEventValidation="false" EnableViewState="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <%--Hay que hacerlo del siguiente modo ya que estos hacks no los reconoce 
        IE Mobile por lo que no es válido poner ni "if IE" ni "if !IE". En ambos 
        casos no recoge la hoja de estilo.  Además tampoco se puede reconocer si
        nos encontramos en Firefox para aplicar las CSS por lo cual tendremos que
        aplicar la hoja de estilos para navegadores WEB en 2 casos: para IE y otra 
        para !IE, es decir, Firefox, Chrome, etc... auqnue se trate de la misma hoja--%>
         
        <%--Estilos generales (IE Mobile, IE, Firefox)--%>
        <link href="css/estilos.css" rel="stylesheet" type="text/css" />
        
        <%--Estilos IE--%>
        <!--[if IE]>
            <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />
        <![endif]-->
            
        <%--Estilos Firefox--%>
        <!--[if !IE]><!-->
            <link rel="stylesheet" type="text/css" href="css/estilosWeb.css" />
        <!--<![endif]-->
    </head>
    <body>
        <form id="form1" runat="server">
            <script type="text/javascript">
                window.onload = function() {
                    var txtBusquedaProducto = document.getElementById("<%= txtBusquedaProducto.ClientID %>");
                    var txtBloque = document.getElementById("<%= txtBloque.ClientID %>");

                    if (txtBloque)
                        txtBloque.focus();
                    else
                        txtBusquedaProducto.focus();
                }  
                 
                function forzarInteger(control) 
                {
                if (isNaN(control.value))
                    control.value = '';
                }
                
                function Borrar()
                {
                    if(confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarBorrar%>"/>'))
                        return true;
                    else
                        return false;
                }
                
                function Salir()
                {
                    if(confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarSalir%>"/>'))
                        return true;
                    else
                        return false;
                }
                
                function Finalizar() {
                    RecorrerTabla();
                    if (confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarFinalizar%>"/>')) {
                        if ("<%=idOrdenInventario != null%>" == "True") {           //Si hay OI pedimos las observaciones
                            var hddcomentarios = document.getElementById("<%= hddComentariosOI.ClientID %>");
                            hddcomentarios.value = prompt('<%= Resources.Resource.ObservacionesTienda %>', '');
                        }
                        return true;
                    }
                    else
                        return false;
                }
                
                function RecorrerTabla()
                {
                var tabla =document.getElementById("<%= tblTallas.ClientID %>");
                var hddCantidades = document.getElementById("<%= hddCantidades.ClientID %>");
                hddCantidades.value = "";
                
                //Realización del proceso de guardado de las cantidades introducidas en función
                //del navegador
                if(navigator.appName == "Microsoft IE Mobile") {
                        //Modo normal
                        if(tabla.childNodes.length == 4) {
                            for (var q = 0; q < tabla.childNodes[2].childNodes.length - 1; q++) 
                            {                                
                                   hddCantidades.value = hddCantidades.value + tabla.childNodes[2].childNodes[q].childNodes[0].value + ";";
                               }
                        }
                        //Modo ciego
                        else {
                            for(var q=0;q<tabla.childNodes[1].childNodes.length-1;q++)
                            {
                                   hddCantidades.value = hddCantidades.value + tabla.childNodes[1].childNodes[q].childNodes[0].value + ";";
                            }
                        }
                    }
                    else
                    {
                        //Modo normal
                        if(tabla.rows.length == 4)
                        {
                            for(var q=0;q<tabla.rows[2].cells.length;q++)
                            {
                                hddCantidades.value = hddCantidades.value +  tabla.rows[2].cells[q].childNodes[0].value + ";";
                            }
                        }
                        //Modo ciego
                        else
                        {
                            for(var q=0;q<tabla.rows[1].cells.length;q++)
                            {
                                hddCantidades.value = hddCantidades.value +  tabla.rows[1].cells[q].childNodes[0].value + ";";
                            }
                        }
                    }
                } 
                
                function CalcularDiferenciaInventario(indice)
                {
                var tabla =document.getElementById("<%= tblTallas.ClientID %>");
                var introducida;
                var inventariada;
                var diferencia;
                
                //Realización del proceso de guardado de las cantidades introducidas en función
                //del navegador
                if(navigator.appName == "Microsoft IE Mobile")
                    {
                        //Modo normal
                        if(tabla.childNodes.length == 4)
                        {
                            if(tabla.childNodes[2].childNodes[indice].childNodes[0].value == "")
                                introducida = 0;
                            else
                                introducida =  tabla.childNodes[2].childNodes[indice].childNodes[0].value;  
                            
                            inventariada = tabla.childNodes[1].childNodes[indice].innerHTML;
                            diferencia = introducida - inventariada;
                            tabla.childNodes[3].childNodes[indice].innerHTML = diferencia;          
                        }
                    }
                    else
                    {
                        //Modo normal
                        if(tabla.rows.length == 4)
                        {
                            if(tabla.rows[2].cells[indice].childNodes[0].value == "")
                                introducida = 0;
                            else    
                                introducida = tabla.rows[2].cells[indice].childNodes[0].value;
                                
                            inventariada = tabla.rows[1].cells[indice].innerHTML;
                            diferencia = introducida - inventariada;
                            tabla.rows[3].cells[indice].innerHTML = diferencia; 
                        }
                    }
                }

                //Debemos recorrer la fila de inventariado para sumar el total de unidades. Si el modo no es ciego, también tenemos que calcular
                //el total de la diferencia
                function CalcularTotales() {
                    var tabla = document.getElementById("<%= tblTallas.ClientID %>");
                    var total = 0;
                    var indice;
                    var j;

                    //Realización del proceso de guardado de las cantidades introducidas en función
                    //del navegador
                    if (navigator.appName == "Microsoft IE Mobile") {

                        //Modo normal
                        if (tabla.childNodes.length == 4) {
                            indice = 2;
                        }
                        else    //Modo ciego
                        {
                            indice = 1;
                        }

                        //Suma de unidades inventariadas
                        for (j = 0; j < tabla.childNodes[indice].childNodes.length - 1; j = j + 1)      //Para cada columna menos la última
                        {
                            if (tabla.childNodes[indice].childNodes[j].childNodes[0].value != "")        //Si está vacío es 0 y no hay que sumar
                                total = total + parseInt(tabla.childNodes[indice].childNodes[j].childNodes[0].value); //Sumamos el valor
                        }
                        tabla.childNodes[indice].childNodes[j].innerHTML = total;            //Escribimos el total en la última celda

                        //Si no es ciego tenemos que calcular también la diferencia de unidades
                        if (tabla.childNodes.length == 4)
                            tabla.childNodes[indice + 1].childNodes[j].innerHTML = total - parseInt(tabla.childNodes[indice - 1].childNodes[j].innerHTML);    //Calculamos la diferencia total
                    }
                    else
                    {
                        //Modo normal
                        if (tabla.rows.length == 4) {
                            indice = 2;
                        }
                        else    //Modo ciego
                        {
                            indice = 1;
                        }

                        //Suma de unidades inventariadas
                        for (j = 0; j < tabla.rows[indice].cells.length - 1; j = j + 1)      //Para cada columna menos la última
                        {
                            if (tabla.rows[indice].cells[j].childNodes[0].value != "")        //Si está vacío es 0 y no hay que sumar
                                total = total + parseInt(tabla.rows[indice].cells[j].childNodes[0].value); //Sumamos el valor
                        }
                        tabla.rows[indice].cells[j].innerHTML = total;            //Escribimos el total en la última celda

                        //Si no es ciego tenemos que calcular también la diferencia de unidades
                        if (tabla.rows.length == 4)
                            tabla.rows[indice + 1].cells[j].innerHTML = total - parseInt(tabla.rows[indice - 1].cells[j].innerHTML);    //Calculamos la diferencia total
                    }
                }

                  
                </script>
            <asp:HiddenField id="hddCantidades" runat="server"/>
            <asp:HiddenField id="hddComentariosOI" runat="server"/>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtBusquedaProducto" runat="server" TabIndex="1" CssClass="txtBuscar" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" TabIndex="2" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Buscar%>" onclick="btnBuscar_Click" OnClientClick="RecorrerTabla();"/>
                        <asp:Button ID="btnSalir" runat="server" TabIndex="4" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Salir%>" onclick="btnSalir_Click" OnClientClick="RecorrerTabla(); return Salir();"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="123px;">
                        <asp:Label ID="lblTitIdArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdArticulo" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdReferencia" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitUP" runat="server" Text="<%$ Resources:Resource, UP%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblUP" runat="server" Text="UP:"></asp:Label>

                        <asp:Label ID="lblTitUT" runat="server" Text="<%$ Resources:Resource, UT%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblUT" runat="server" Text="UT:"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnBorrar" runat="server" TabIndex="3" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Borrar%>" onclick="btnBorrar_Click" OnClientClick="return Borrar();" />
                        <asp:Button ID="btnFoto" runat="server" TabIndex="5" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Foto%>" onclick="btnFoto_Click" OnClientClick="RecorrerTabla();"/>        
                    </td>
                </tr>     
                <tr>
                    <td colspan="3">

                        <div style="padding:1px; width:auto;">
                            <asp:Table ID="tblTallas" runat="server" CssClass="tablaTallas" CellPadding="0" CellSpacing="0" EnableViewState="true">
                            </asp:Table>
                        </div>
                       
                    </td>    
                </tr>  
                <tr>
                    <td colspan="2">
                            <asp:DropDownList ID="ddlBloques" TabIndex="6" runat="server" AutoPostBack="true"
                                CssClass="ddlEstandar" onselectedindexchanged="ddlBloques_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="<%$ Resources:Resource, Total%>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$ Resources:Resource, NuevoBloque%>"></asp:ListItem>
                            </asp:DropDownList>
                            
                            <asp:TextBox ID="txtBloque" runat="server" CssClass="txtBuscar" Visible="false" MaxLength="50"></asp:TextBox>
                    </td>        
                    <td>
                            <asp:Button ID="btnGuardar" runat="server" TabIndex="7" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Guardar%>" onclick="btnGuardar_Click" OnClientClick="return RecorrerTabla();"/>
                            <asp:Button ID="btnFinalizar" runat="server" TabIndex="8" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Finalizar%>" onclick="btnFinalizar_Click" OnClientClick="return Finalizar(); "/>
    
                    </td>
                </tr>
                <tr>
                    <td width="1">
                        <asp:Button ID="btnPrimero" runat="server" TabIndex="9" CssClass="boton3" 
                            Text="<<" onclick="btnPrimero_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnAnterior" runat="server" TabIndex="10" CssClass="boton3" 
                            Text="<" onclick="btnAnterior_Click" OnClientClick="return RecorrerTabla();"/>
                        
                        </td><td width="7">
                        <asp:Label ID="lblActual" runat="server" Text="0"></asp:Label>
                        <br />
                        <asp:Label ID="lblSeparador" runat="server" Text="/"></asp:Label>
                        <br />
                        <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>

                   </td>
                   <td>
                        <asp:Button ID="btnSiguiente" runat="server" TabIndex="11" CssClass="boton3" 
                            Text=">" onclick="btnSiguiente_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnUltimo" runat="server" TabIndex="12" CssClass="boton3" 
                            Text=">>" onclick="btnUltimo_Click" OnClientClick="return RecorrerTabla();"/>
                    </td>
                </tr>       
            </table>
            
            <%--OBTENCIÓN DEL INVENTARIO--%>
            <asp:SqlDataSource ID="sdsObtenerInventario" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand=".AVE_InventariosPendientesBuscar" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdTienda" Type ="String" />
                    <asp:Parameter Name="IdEmpleado" Type ="String" />
                    <asp:Parameter Name="IdTerminal" Type="String"  />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--OBTENCIÓN DEL DETALLE DEL INVENTARIO--%>
            <asp:SqlDataSource ID="sdsObtenerInventarioDetalle" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_InventariosPendientesObtener" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdInventario" Type ="String" />
                </SelectParameters>
            </asp:SqlDataSource>
    
            <%--ORIGEN DE DATOS PARA LA BÚSQUEDA DE LAS TALLAS--%>
            <asp:SqlDataSource ID="sdsStockInventarioObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                SelectCommand="AVE_StockInventarioObtener" 
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                    <asp:Parameter Name="IdTienda" Type="String"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--ORIGEN DE DATOS PARA LA INSERCIÓN DE LOS PRODUCTOS EN EL INVENTARIO--%>
            <asp:SqlDataSource ID="sdsInventariosPendientesGuardarDetalle" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                InsertCommand="AVE_InventariosPendientesDetalleGuardar" InsertCommandType="StoredProcedure">
                <InsertParameters>
                    <asp:Parameter Name="IdInventario" Type="Int32"/>
                    <asp:Parameter Name="Bloque" Type="String"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                    <asp:Parameter Name="Id_Cabecero_Detalle" Type="Int32"/>
                    <asp:Parameter Name="Cantidad" Type="Int32" ConvertEmptyStringToNull="true"/>
                    <asp:Parameter Name="Usuario" Type="String"/>
                </InsertParameters>
            </asp:SqlDataSource>
            
            <%--ORIGEN DE DATOS PARA LA INSERCIÓN DEL FORMULARIO PENDIENTE A LA TABLA HISTÓRICA--%>
            <asp:SqlDataSource ID="sdsInventariosPendientesFinalizar" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                InsertCommand="AVE_InventariosPendientesFinalizar" InsertCommandType="StoredProcedure">
                <InsertParameters>
                    <asp:Parameter Name="IdInventario" Type="Int32"/>
                    <asp:Parameter Name="Usuario" Type="String"/>
                    <asp:Parameter Name="IdOrdenInventario" Type="String"/>
                    <asp:Parameter Name="IdTienda" Type="String"/>
                    <asp:Parameter Name="ObservacionesTienda" Type="String"/>
                </InsertParameters>
            </asp:SqlDataSource>

            
            <%--ORIGEN DE DATOS PARA LA ELIMINACIÓN DE UN ARTÍCULO--%>
            <asp:SqlDataSource ID="sdsArticuloEliminar" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                 DeleteCommand="AVE_InventariosPendientesDetalleBorrar" DeleteCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="IdInventario" Type="Int32"/>
                    <asp:Parameter Name="Bloque" Type="String"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                </DeleteParameters>
            </asp:SqlDataSource>
            
            <%--ORIGEN DE DATOS PARA LA COMPROBAR SI EL ARTÍCULO YA ESTÁ EN EL INVENTARIO--%>
            <asp:SqlDataSource ID="sdsInventariosPendientesExisteArticulo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                 SelectCommand="AVE_InventariosPendientesExisteArticulo" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdInventario" Type="Int32"/>
                    <asp:Parameter Name="Bloque" Type="String"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--ORIGEN DE DATOS PARA LA COMPROBAR SI EL ARTÍCULO PERTENECE AL PERÍMETRO DE LA OI--%>
            <asp:SqlDataSource ID="sdsOrdenInventarioEsArticuloEnPerimetro" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                 SelectCommand="AVE_OrdenInventarioEsArticuloEnPerimetro" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdOrdenInventario" Type="Int32"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                </SelectParameters>
            </asp:SqlDataSource>

        </form>     
    </body>
</html>
