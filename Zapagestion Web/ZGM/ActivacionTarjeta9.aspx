<%@ Page Language="C#" MasterPageFile="~/MasterPageNine.Master" AutoEventWireup="true" CodeBehind="ActivacionTarjeta9.aspx.cs" Inherits="AVE.ActivacionTarjeta9" %>
 
    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/Cliente9.js" type="text/javascript"></script>
    <link type="text/css"rel="stylesheet" 
     href="css/jquery-ui.css" />   

  <script type="text/javascript">
      function Enviafoco() {
      }
      function ConfirmarTJT(texto) {
          if (confirm(texto)) {
              document.getElementById('<%= Button2.ClientID %>').click();
          }
          else {
              document.getElementById('<%= Button3.ClientID %>').click();
          }



      }
      $(document).ready(function () {
     
          $('#txtFechaNacimiento').datepicker({ dateFormat: 'dd/mm/yy',
              changeMonth: true,
              changeYear: true,
               showButtonPanel: true,
              closeText: "Cerrar",
              numberOfMonths: 1,
              maxDate: "+16Y",
              minDate: "-100Y",
              yearRange: "-100:+16",
              dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
              monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
              monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr',
                    'May', 'Jun', 'Jul', 'Ago',
                    'Sep', 'Oct', 'Nov', 'Dic'],
              onClose: function () {
                  $('#ctl00_ContentPlaceHolder2_hidFechaNac').val($('#txtFechaNacimiento').val());
                  $('#ctl00_ContentPlaceHolder2_txtFecNac').val($('#txtFechaNacimiento').val());
              }
          });
         
      });
    
  </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
        <asp:HiddenField runat="server" ID ="hidIdCliente" />
        <asp:HiddenField runat="server" ID ="hidNumeroTarjetaCliente9" />
        <asp:HiddenField runat="server" ID ="hidTipoCliente" />
        <asp:HiddenField runat="server" ID ="hidFechaNac" />
        <asp:HiddenField runat="server" ID ="hidEsEmpleado" />
        <asp:HiddenField runat="server" ID ="hidMsjError" />
        <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none"  UseSubmitBehavior="false" CausesValidation="False" OnClick="EliminaNine_Click" />
        <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" UseSubmitBehavior="false" CausesValidation="False" OnClick="Error309_Click" />
    <div id = "divContent" style="width:70%; margin:0 auto 0 auto;">
     <asp:RadioButtonList ID="RadioButtonlTipoCli" RepeatLayout="Flow" 
                            RepeatDirection="Horizontal"  runat="server" CssClass="OptionsC9">
            <asp:ListItem Value="9" Selected>Cliente Registrado</asp:ListItem>
            <asp:ListItem Value="N9" >Nuevo Cliente</asp:ListItem>
                           
    </asp:RadioButtonList>
    </div>
    <div id="DatC9" class="" style=" width:65%; margin:0 auto 0 auto;">
        <div  id="BusquedaCliente" class="datTJT" style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="Label1" runat="server" Text="BuscarCliente:" style="margin-left:49px; "></asp:Label>
            <asp:TextBox ID="nomcliente" runat="server" Width="60%"></asp:TextBox>
             <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="60%"  runat="server" 
                 onselectedindexchanged="LstClientes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:ImageButton runat="server" ID="imgCliente9" heigth="2%" Width="2%" ImageUrl="~/img/buscar.png"  OnClick="btnBuscaCliente_Click" UseSubmitBehavior="false" CausesValidation="False"/>            
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
                                ErrorMessage="Introduzca una dirección de email válida" 
                                    ToolTip="Introduzca una dirección de email válida" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
           </asp:RegularExpressionValidator>
            
        </div>
        <div style="MARGIN-TOP:20px;margin-bottom:20px">
            <asp:Label ID="lblFecNac" runat="server" Text="Fecha Nacimiento:" style="margin-left:20px; " ></asp:Label>
            <asp:TextBox ID="txtFecNac" runat="server" Width="40%" ></asp:TextBox>
            <input id="txtFechaNacimiento"  type="text" style="width:40%;" readonly="readonly" />
             <asp:RequiredFieldValidator runat="server" ID="RFVFecha" ErrorMessage="*"
                                Font-Bold="true" ControlToValidate="txtFecNac" ValidationGroup="Obligatorio" >
           </asp:RequiredFieldValidator>
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
     <div style="MARGIN-TOP:40px;margin-bottom:60px" class="datTJT">
            <asp:Label ID="lblTarjeta" runat="server" Text="Número de Tarjeta:" style="margin-left:105px; " ></asp:Label>
            <asp:TextBox ID="txtNumTarjeta" runat="server" Width="40%"></asp:TextBox>
             <asp:RegularExpressionValidator runat="server" ID="reqTJT" ControlToValidate="txtNumTarjeta"
                                ErrorMessage="Introduzca un codigo tarjeta válido" 
                                    ToolTip="Introduzca un codigo tarjeta válido" 
                                    ValidationExpression="[0-9]*">
           </asp:RegularExpressionValidator>
            
     </div>
     <div style="MARGIN-TOP:40px;margin-bottom:60px">
             <asp:Button runat="server" ID="btnActivarTjt" Text="Activar Tarjeta"  style="margin-left:40%; "
                                     onclick="btnActivarTjt_Click" ValidationGroup="Obligatorio" />
            
     </div>
  </div>
    
</asp:Content>
   