<%@ Page Title="<%$ Resources:Resource, CargosDetalle%>" Language="C#" Theme="Tema" AutoEventWireup="true" CodeBehind="CargosDetalle.aspx.cs" Inherits="AVE.CargosDetalle" %>
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
                    var txtBusquedaCargo = document.getElementById("<%= txtBusquedaCargo.ClientID %>");
                    txtBusquedaCargo.focus();
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
                    if(confirm('<asp:Literal runat="server" Text="<%$ Resources:Resource,ConfirmarCancelar%>"/>'))
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
                        for(var q=0;q<tabla.childNodes[1].childNodes.length;q++)
                        {
                               hddCantidades.value = hddCantidades.value + tabla.childNodes[1].childNodes[q].childNodes[0].value + ";";
                        }
                    }
                    else {

                        if (tabla.rows.length > 0)
                          {
                            for (var q = 0; q < tabla.rows[1].cells.length; q++) {
                                hddCantidades.value = hddCantidades.value + tabla.rows[1].cells[q].childNodes[0].value + ";";
                             }
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
                    <td colspan="2" width="123px;">
                        <asp:TextBox ID="txtBusquedaCargo" runat="server" TabIndex="1" CssClass="txtBuscar" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" TabIndex="2" CssClass="boton3" OnClick="btnBuscar_Click" OnClientClick="return RecorrerTabla();" 
                            Text="<%$ Resources:Resource, Buscar%>"/>
                        <asp:Button ID="btnSalir" runat="server" TabIndex="4" CssClass="boton3" 
                            Text="<%$ Resources:Resource, CancelarAbrev%>" onclick="btnCancelar_Click" OnClientClick="return Salir();"/>
                    </td>
                </tr>
                <tr>
                     <td colspan="2">
                        <asp:Label ID="lblTitTiendaOrigen" runat="server" Text="<%$ Resources:Resource, TiendaOrigenAbrev%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdTiendaOrigen" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitNCargo" runat="server" Text="<%$ Resources:Resource, NumeroCargo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdNCargo" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdReferencia" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTitTiendaDestino" runat="server" Text="<%$ Resources:Resource, TiendaDestinoAbrev%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdTiendaDestino" runat="server"></asp:Label>
                        <br />
                        &nbsp;<asp:Label ID="lblTitArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblIdArticulo" runat="server"></asp:Label>
                        <br />
                        &nbsp;<asp:Label ID="lblTitModelo" runat="server" Text="<%$ Resources:Resource, Modelo%>" CssClass="negrita"></asp:Label>
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
                            <asp:Button ID="BtnFoto" runat="server" TabIndex="5" CssClass="boton3" 
                            Text="<%$ Resources:Resource, Foto%>" onclick="btnFoto_Click" OnClientClick="RecorrerTabla();"/>   
                            <asp:Button ID="btnBorrar" runat="server" TabIndex="5" CssClass="boton3"  
                            Text="<%$ Resources:Resource, Borrar%>" onclick="btnBorrar_Click" OnClientClick="return Borrar();"/>   
                    </td>    
                    <td>
                        <asp:Button ID="btnFinalizar" runat="server" TabIndex="8" CssClass="boton3"  
                        Text="<%$ Resources:Resource, Finalizar%>" onclick="btnFinalizar_Click" OnClientClick="RecorrerTabla(); return Finalizar();"/>
                    </td>
                </tr>
                <tr>
                     <td colspan="3" style=" text-align:center;">
                        <asp:Label ID="lblTitTotCargo" runat="server" Text="<%$ Resources:Resource, TotalCargo%>" CssClass="negrita"></asp:Label>
                        <asp:Label ID="lblTotCargo" runat="server"></asp:Label>
                    </td>    
                </tr>
                 <tr>
                    <td>
                        <asp:Button ID="btnPrimero" runat="server" TabIndex="9" CssClass="boton3" 
                            Text="<<" onclick="btnPrimero_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnAnterior" runat="server" TabIndex="10" CssClass="boton3" 
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
                        <asp:Button ID="btnSiguiente" runat="server" TabIndex="11" CssClass="boton3" 
                            Text=">" onclick="btnSiguiente_Click" OnClientClick="return RecorrerTabla();"/>
                        <asp:Button ID="btnUltimo" runat="server" TabIndex="12" CssClass="boton3" 
                            Text=">>" onclick="btnUltimo_Click" OnClientClick="return RecorrerTabla();"/>
                    </td>
                </tr>           
            </table>
            
            
            <%--OBTENER CARGO--%>
            <asp:SqlDataSource ID="sdsObtenerCargo" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="dbo.AVE_CargosObtener" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdCargo" Type ="Int32"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--OBTENER CABECEROS--%>
            <asp:SqlDataSource ID="sdsObtenerCabeceros" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="dbo.AVE_CabecerosDetallesObtenerPorArticulo" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdArticulo" Type ="Int32"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--OBTENER DATOS DEL ARTICULO--%>
            <asp:SqlDataSource ID="sdsObtenerArticuloDetalle" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="dbo.AVE_ArticuloDetalleObtener" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                    <asp:Parameter Name="IdTienda" Type="String"/>
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--PROCESO DE GUARDADO--%>
            <asp:SqlDataSource ID="sdsGuardarCargo" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                InsertCommand="dbo.AVE_CargosDetalleGuardar" 
                InsertCommandType="StoredProcedure" onselecting="sdsGuardarCargo_Selecting">
                <InsertParameters>
                    <asp:Parameter Name="IdCargo" Type="Int32"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                    <asp:Parameter Name="Id_Cabecero_Detalle" Type="Int32"/>
                    <asp:Parameter Name="Cantidad" Type="Int32"/>
                    <asp:Parameter Name="Usuario" Type="String"/>
                </InsertParameters>
            </asp:SqlDataSource>
            
            <%--BORRADO DE UN ARTICULO DEL CARGO--%>
            <asp:SqlDataSource ID="sdsBorrarArticuloCargo" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                DeleteCommand="dbo.AVE_CargosDetalleBorrar" DeleteCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="IdCargo" Type="Int32"/>
                    <asp:Parameter Name="IdArticulo" Type="Int32"/>
                </DeleteParameters>
            </asp:SqlDataSource>
            
            <%--BORRADO DE UN CARGO--%>
            <asp:SqlDataSource ID="sdsBorrarCargo" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                DeleteCommand="dbo.AVE_CargosBorrar" DeleteCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="IdCargo" Type="Int32"/>
                </DeleteParameters>
            </asp:SqlDataSource>
            
            <%--FINALIZAR CARGO--%>
            <asp:SqlDataSource ID="sdsFinalizarCargo" runat="server"
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                UpdateCommand="dbo.AVE_CargosFinalizar" UpdateCommandType="StoredProcedure">
                <UpdateParameters>
                    <asp:Parameter Name="IdCargo" Type ="Int32"/>
                    <asp:Parameter Name="Usuario" Type ="String"/>
                </UpdateParameters>
            </asp:SqlDataSource>
            
        </form>     
    </body>
</html>
