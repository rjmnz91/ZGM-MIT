<%@ Page Language="C#" MasterPageFile="~/MasterPageNine.Master"   AutoEventWireup="true" CodeBehind="CambioPlastico.aspx.cs" Inherits="AVE.CambioPlastico" %>

   <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/Cliente9.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
        <asp:HiddenField runat="server" ID ="hidIdCliente" />
        <asp:HiddenField runat="server" ID ="hidNumeroTarjetaCliente9" />
        <asp:HiddenField runat="server" ID ="hidTipoCambio" />
        <asp:HiddenField runat="server" ID ="hidNuevoCambio" />
        <asp:HiddenField runat="server" ID ="hidNuevoNivel" />
         <asp:HiddenField runat="server" ID ="hidEsEmpleado" />
        <asp:HiddenField runat="server" ID ="hidNivel" />
    <div id = "divContent" style="width:70%; margin:0 auto 0 auto;">
     <asp:RadioButtonList ID="RadioButtonlTipoCambio" RepeatLayout="Flow" 
                            RepeatDirection="Horizontal"  runat="server" CssClass="OptionsC9">
            <asp:ListItem Value="C" >Cambio Nivel</asp:ListItem>
            <asp:ListItem Value="R" Selected>Reemplazo</asp:ListItem>
                           
    </asp:RadioButtonList>
    </div>
    <div id="DatC9" class="" style=" width:65%; margin:0 auto 0 auto;">
        <div  id="BusquedaCliente" class="datTJT" style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="Label1" runat="server" Text="BuscarCliente:" style="margin-left:49px; "></asp:Label>
            <asp:TextBox ID="nomcliente" runat="server" Width="60%"></asp:TextBox>
             <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="60%"  runat="server" 
                 onselectedindexchanged="LstClientes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:ImageButton runat="server" ID="imgCliente9" heigth="2%" Width="2%" ImageUrl="~/img/buscar.png" OnClick="btnBuscaCliente_Click"  />            
        </div>
        <div id="datosC9" class="EnDatos">
        
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" style="margin-left:87px; " ></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" Width="40%"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ID="RFVNombre" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtNombre" ValidationGroup="Obligatorio">
           </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="reqNombre" ControlToValidate="txtNombre"
                                ErrorMessage="Introduzca un nombre válido" 
                                    ToolTip="Introduzca un nombre válido" 
                                    ValidationExpression="[a-zA-Z \-]*">
           </asp:RegularExpressionValidator>
            
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblApel" runat="server" Text="Apellidos:" style="margin-left:77px; " ></asp:Label>
            <asp:TextBox ID="txtApellidos" runat="server" Width="40%"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ID="RFVApellidos" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtApellidos" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="reqApellidos" ControlToValidate="txtApellidos"
                                ErrorMessage="Introduzca unos apellidos válidos" 
                                    ToolTip="Introduzca unos apellidos válidos" 
                                    ValidationExpression="[a-zA-Z \-]*">
           </asp:RegularExpressionValidator>
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblEMail" runat="server" Text="E-Mail:" style="margin-left:97px; " ></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" Width="40%"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="RFVMail" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtEmail" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="reqEmail" ControlToValidate="txtEmail"
                                ErrorMessage="Error" 
                                    ToolTip="Introduzca una dirección de email válida, por ejemplo: persona@dominio.com" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
           </asp:RegularExpressionValidator>
            
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblFecNac" runat="server" Text="Fecha Nacimiento:" style="margin-left:20px; " ></asp:Label>
            <asp:TextBox ID="txtFecNac" runat="server" Width="40%"></asp:TextBox>
           
            <asp:RequiredFieldValidator runat="server" ID="RFVFecha" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtFecNac" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>

            <%--<asp:RegularExpressionValidator runat="server" ID="regtxtFnac" ControlToValidate="txtFecNac"
                                ErrorMessage="Error" 
                                    ToolTip="Introduzca una fecha correcta, por ejemplo: 12\2\2015" 
                                    ValidationExpression="/^([0][1-9]|[12][0-9]|3[01])(\/|-)([0][1-9]|[1][0-2])\2(\d{4})$/">
           </asp:RegularExpressionValidator>--%>
            
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblTfnoCasa" runat="server" Text="Teléfono Casa:" style="margin-left:45px; " ></asp:Label>
            <asp:TextBox ID="txtTfnoCasa" runat="server" Width="40%"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ID="RFVTelefono" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtTfnoCasa" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>

               <asp:RegularExpressionValidator runat="server" ID="reqTelefono" ControlToValidate="txtTfnoCasa"
                                ErrorMessage="Introduzca un teléfono válido, sin -" 
                                    ToolTip="Introduzca un teléfono válido, sin -" 
                                    ValidationExpression="[0-9]*">
           </asp:RegularExpressionValidator>
            
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblMovil" runat="server" Text="Teléfono Móvil:" style="margin-left:41px; " ></asp:Label>
            <asp:TextBox ID="txtMovil" runat="server" Width="40%"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="RFVMovil" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtMovil" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="reqMovil" ControlToValidate="txtMovil"
                                ErrorMessage="Introduzca un teléfono válido, sin -" 
                                    ToolTip="Introduzca un teléfono válido, sin -" 
                                    ValidationExpression="[0-9]*">
           </asp:RegularExpressionValidator>
        </div>
        
     </div>
     
      <div style="MARGIN-TOP:40px;margin-bottom:60px" class="datTJT" id="datTJT">
            <asp:Label ID="lblNivel" runat="server" Text="Nivel Actual:" style="margin-left:148px; " ></asp:Label>
            <asp:TextBox ID="txtNivel" runat="server" Width="40%"></asp:TextBox>
             <asp:RegularExpressionValidator runat="server" ID="regNivel" ControlToValidate="txtNivel"
                                ErrorMessage="Error" 
                                    ToolTip="Introduzca un nivel de Cliente Nine" 
                                    ValidationExpression="[a-zA-Z0-9 \-]*">
           </asp:RegularExpressionValidator>
            
     </div>
     
     <div style="MARGIN-TOP:40px;margin-bottom:60px" class="datTJT">
                <asp:Label ID="lblCambio" runat="server" Text="Referencia:" style="margin-left:148px; " ></asp:Label>
                <asp:DropDownList id ="tipCambio" runat="server" OnSelectedIndexChanged ="tipCambio_SelectedIndexChanged">
                            <asp:ListItem   Value="750000000291">Primer Cambio</asp:ListItem>
                            <asp:ListItem  Value="108303000332">Cambio Tarjeta</asp:ListItem>                          
                </asp:DropDownList>
                <asp:DropDownList id ="tipNivel" runat="server" OnSelectedIndexChanged ="tipNivel_SelectedIndexChanged">
                            <asp:ListItem   Value="750000000161">ShoeLover</asp:ListItem>
                            <asp:ListItem  Value="750000000437">First ShoeLover</asp:ListItem> 
                            <asp:ListItem  Value="750000000436">Basica</asp:ListItem>                          
                </asp:DropDownList>

     </div>
      <div style="MARGIN-TOP:40px;margin-bottom:60px" class="datTJT">
            <asp:Label ID="lblTarjetaActual" runat="server" Text="Tarjeta Actual:" style="margin-left:135px; " ></asp:Label>
            <asp:TextBox ID="txtTarjetaActual" runat="server" Width="40%"></asp:TextBox>
           <asp:RegularExpressionValidator runat="server" ID="reqTJTAct" ControlToValidate="txtNumTarjeta"
                                ErrorMessage="Introduzca un codigo tarjeta válido" 
                                    ToolTip="Introduzca un codigo tarjeta válido" 
                                    ValidationExpression="[0-9]*">
           </asp:RegularExpressionValidator>
            
     </div>
     <div style="MARGIN-TOP:40px;margin-bottom:60px" class="datTJT">
            <asp:Label ID="lblTarjeta" runat="server" Text="Número de Tarjeta:" style="margin-left:105px; " ></asp:Label>
            <asp:TextBox ID="txtNumTarjeta" runat="server" Width="40%"></asp:TextBox>
             <asp:RegularExpressionValidator runat="server" ID="reqTJT" ControlToValidate="txtNumTarjeta"
                                ErrorMessage="Error" 
                                    ToolTip="Introduzca un codigo tarjetaValida" 
                                    ValidationExpression="[0-9]*">
           </asp:RegularExpressionValidator>
            
     </div>

     <div style="MARGIN-TOP:40px;margin-bottom:60px">
             <asp:Button runat="server" ID="btnReemplazarTjt" Text="Reemplazar Tarjeta"  style="margin-left:40%; "
                                     onclick="btnReemplazarTjt_Click" />
             <asp:Button runat="server" ID="btnCambiarNivel" Text="Activar Tarjeta"  style="margin-left:40%; "
                                     onclick="btnCambiarNivel_Click" />
            
     </div>
  </div>
    
</asp:Content>
   