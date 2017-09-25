<%@ Page Title="<%$ Resources:Resource, PedidosEntrada%>" Language="C#" Theme="Tema" AutoEventWireup="true" CodeBehind="PedidosEntrada.aspx.cs" Inherits="AVE.PedidosEntrada" EnableEventValidation="false" EnableViewState="true"%>
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
                    var txtBusquedaPedido = document.getElementById("<%= txtBusquedaPedido.ClientID %>");
                    txtBusquedaPedido.focus();
                }  
                 
                function forzarInteger(control) 
                {
                if (isNaN(control.value))
                    control.value = '';
                }
                
                function Borrar()
                {
                    if(confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarCancelar%>"/>'))
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
                
                function Finalizar()
                {
                    if(confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarFinalizarEntrada%>"/>'))
                        return true;
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
                if(navigator.appName == "Microsoft IE Mobile")
                    {
                        //Modo normal
                        if(tabla.childNodes.length == 4)
                        {
                            for(var q=0;q<tabla.childNodes[2].childNodes.length;q++)
                            {
                                   hddCantidades.value = hddCantidades.value + tabla.childNodes[2].childNodes[q].childNodes[0].value + ";";
                            }
                        }
                        //Modo ciego
                        else
                        {
                            for(var q=0;q<tabla.childNodes[1].childNodes.length;q++)
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
                   
                function CopiarDatos()
                {
                var tabla =document.getElementById("<%= tblTallas.ClientID %>");
                
                //Realización del proceso de guardado de las cantidades introducidas en función
                //del navegador
                if(navigator.appName == "Microsoft IE Mobile")
                {
                    for(var q=0;q<tabla.childNodes[2].childNodes.length-1;q++)
                    {
                           tabla.childNodes[1].childNodes[q].childNodes[0].value = tabla.childNodes[2].childNodes[q].innerHTML;
                    }
                }
                else
                {
                    for(var q=0;q<tabla.rows[2].cells.length;q++)
                    {
                        tabla.rows[1].cells[q].childNodes[0].value = tabla.rows[2].cells[q].innerHTML;
                    }
                }
                CalcularTotales();
                return false;
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

                        indice = 1;
 
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
                        indice = 1;

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
            <table width="auto" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtBusquedaPedido" runat="server" TabIndex="1" CssClass="txtBuscar2" MaxLength="50"></asp:TextBox>
                        <asp:RangeValidator ID="rnvPedido" runat="server" ControlToValidate="txtBusquedaPedido" Type="Integer" ErrorMessage="*" 
                        ValidationGroup="Buscar" CssClass="mensajeRequerido2"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="rfvPedido" runat="server" ControlToValidate="txtBusquedaPedido" ErrorMessage="*"
                        ValidationGroup="Buscar" CssClass="mensajeRequerido2"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" TabIndex="2" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Buscar%>" onclick="btnBuscar_Click"  ValidationGroup="Buscar"/>
                        <asp:Button ID="btnCancelar" runat="server" TabIndex="4" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Cancelar%>" onclick="btnCancelar_Click" OnClientClick="return Borrar();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblTitNumPedido" runat="server" Text="<%$ Resources:Resource, Pedido%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdPedido" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdReferencia" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTitIdArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdArticulo" runat="server"></asp:Label>
                        <br />
                        &nbsp;<asp:Label ID="lblTitIdModelo" runat="server" Text="<%$ Resources:Resource, Modelo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdModelo" runat="server"></asp:Label>
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
                            <asp:Button ID="BtnFoto" runat="server" TabIndex="5" CssClass="boton3" Enabled="false" 
                            Text="<%$ Resources:Resource, Foto%>" onclick="btnFoto_Click" OnClientClick="RecorrerTabla();"/>   
                            <asp:Button ID="BtnCopiar" runat="server" TabIndex="5" CssClass="boton3" Enabled="false"  
                            Text="<%$ Resources:Resource, Copiar%>" OnClientClick="return CopiarDatos();" UseSubmitBehavior="false"/>   
                            
                    </td>    
                    <td>
                        <asp:Button ID="btnFinalizar" runat="server" TabIndex="8" CssClass="boton3" Enabled="false"  
                        Text="<%$ Resources:Resource, Finalizar%>" onclick="btnFinalizar_Click" OnClientClick="RecorrerTabla(); return Finalizar();"/>
                    </td>
                </tr>
                <tr>
                     <td colspan="2">
                        &nbsp;<asp:Label ID="lblTitTotPedido" runat="server" Text="<%$ Resources:Resource, TotalPedido%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblTotPedido" runat="server"></asp:Label>
                     </td>    
                    <td>
                       &nbsp;<asp:Label ID="lblTitTotEntrada" runat="server" Text="<%$ Resources:Resource, TotalEntrada%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblTotEntrada" runat="server"></asp:Label>
                    </td>    
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnPrimero" runat="server" TabIndex="9" CssClass="boton3" Enabled="false" 
                            Text="<<" onclick="btnPrimero_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnAnterior" runat="server" TabIndex="10" CssClass="boton3" Enabled="false"  
                            Text="<" onclick="btnAnterior_Click" OnClientClick="return RecorrerTabla();"/>
                        
                   </td>
                   <td width="10">
                        <asp:Label ID="lblActual" runat="server" Text="0"></asp:Label>
                        <br />
                        <asp:Label ID="lblSeparador" runat="server" Text="/"></asp:Label>
                        <br />
                        <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                        
                   </td>
                   <td>
                        <asp:Button ID="btnSiguiente" runat="server" TabIndex="11" CssClass="boton3" Enabled="false" 
                            Text=">" onclick="btnSiguiente_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnUltimo" runat="server" TabIndex="12" CssClass="boton3" Enabled="false"  
                            Text=">>" onclick="btnUltimo_Click" OnClientClick="return RecorrerTabla();"/>
                    </td>
                </tr>       
            </table>
            
            
            
            <%--OBTENER PEDIDO EN EL PRIMER ACCESO--%>
            <asp:SqlDataSource ID="sdsObtenerPedidoPrimerAcceso" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="dbo.AVE_PedidosEntradaBuscar" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdTienda" Type ="String" />
                    <asp:Parameter Name="IdPedido" Type ="int32" />
                </SelectParameters>
            </asp:SqlDataSource>
    
             <%--CREACIÓN DEL PEDIDO DE ENTRADA--%>
            <asp:SqlDataSource ID="sdsCrearPedidoEntrada" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                InsertCommand="dbo.AVE_PedidosEntradaCrear" 
                InsertCommandType="StoredProcedure" 
                oninserted="sdsCrearPedidoEntrada_Inserted">
                <InsertParameters>
                    <asp:Parameter Name="IdPedido" Type="Int32" />
                    <asp:Parameter Name="FechaEntrada" Type="DateTime" />
                    <asp:Parameter Name="IdTienda" Type ="String" />
                    <asp:Parameter Name="IdEmpleado" Type ="Int32" />
                    <asp:Parameter Name="IdTerminal" Type="String"/>
                    <asp:Parameter Name="Usuario" Type="String"/>
                    <asp:Parameter Name="IdEntrada" Type="Int32" Direction="Output"/>
                </InsertParameters>
            </asp:SqlDataSource>
    
    
            <%--OBTENER PEDIDO DE ENTRADA--%>
            <asp:SqlDataSource ID="sdsObtenerPedidoEntrada" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="dbo.AVE_PedidosEntradaObtener" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdEntrada" Type ="Int32"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--GUARDAR DETALLE PEDIDO ENTRADA--%>
            <asp:SqlDataSource ID="sdsGuardarDetallePedidoEntrada" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                InsertCommand="dbo.AVE_PedidosEntradaDetalleGuardar" InsertCommandType="StoredProcedure">
                <InsertParameters>
                    <asp:Parameter Name="IdEntrada" Type ="Int32"/>
                    <asp:Parameter Name="IdArticulo" Type ="Int32"/>
                    <asp:Parameter Name="ID_Cabecero_Detalle" Type ="Int32"/>
                    <asp:Parameter Name="Cantidad" Type ="Int32"/>
                    <asp:Parameter Name="Usuario" Type ="String"/>
                </InsertParameters>
            </asp:SqlDataSource>
    
    
            <%--ELIMINAR PEDIDO ENTRADA--%>
            <asp:SqlDataSource ID="sdsBorrarPedidoEntrada" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                DeleteCommand="dbo.AVE_PedidosEntradaBorrar" DeleteCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="IdEntrada" Type ="Int32"/>
                </DeleteParameters>
            </asp:SqlDataSource>
            
            <%--FINALIZAR PEDIDO ENTRADA--%>
            <asp:SqlDataSource ID="sdsFinalizarEntrada" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                UpdateCommand="dbo.AVE_PedidosEntradaFinalizar" UpdateCommandType="StoredProcedure">
                <UpdateParameters>
                    <asp:Parameter Name="IdEntrada" Type ="Int32"/>
                    <asp:Parameter Name="Usuario" Type ="String"/>
                </UpdateParameters>
            </asp:SqlDataSource>
 
        </form>     
    </body>
</html>
