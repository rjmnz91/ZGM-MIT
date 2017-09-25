<%@ Page Language="C#" MasterPageFile="~/MasterPageNine.Master" AutoEventWireup="true" CodeBehind="ActualizarCliente9.aspx.cs" Inherits="AVE.ActualizarCliente9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/Cliente9.js" type="text/javascript"></script>
     <script type="text/javascript">
         function ConfirmarTJT(texto) {
             if (confirm(texto)) {
                 document.getElementById('<%= Button2.ClientID %>').click();
             }
         }
       
    
  </script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <asp:HiddenField runat="server" ID ="hidIdCliente" />
 <asp:HiddenField runat="server" ID ="hidNumeroTarjetaCliente9" />
 <asp:HiddenField runat="server" ID ="hidMsjError" />
 <asp:HiddenField runat="server" ID ="hidEsEmpleado" />
<asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" UseSubmitBehavior="false" CausesValidation="False" OnClick="Error309_Click" />

 <div id="DatCliente" style=" width:100%;">
    <div id="DatGeneral" style=" width:45%; float:left;">
        <div  id="BusquedaCliente" class="datTJT" style="MARGIN-TOP:20px;margin-bottom:15px">
            <asp:Label ID="Label1" runat="server" Text="BuscarCliente:" style="margin-left:49px; "></asp:Label>
            <asp:TextBox ID="nomcliente" runat="server" Width="60%"></asp:TextBox>
             <asp:DropDownList ID="LstClientes" CssClass="ocul1" Width="60%"  runat="server" 
                 onselectedindexchanged="LstClientes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:ImageButton runat="server" ID="imgCliente9" height="15px" Width="3%" ImageUrl="~/img/buscar.png"  UseSubmitBehavior="false" CausesValidation="False" OnClick="btnBuscaCliente_Click"  />            
        </div>
        <div id="datosC9" class="EnDatos">
        
            <div style="MARGIN-TOP:15px;margin-bottom:8px">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" style="margin-left:87px; " ></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" Width="40%"></asp:TextBox>
            
                <asp:RegularExpressionValidator runat="server" ID="reqNombre" ControlToValidate="txtNombre"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca un nombre válido" 
                                        ValidationExpression=" [^a-zA-Z \-]|( )|(\-\-)|(^\s*$)">
               </asp:RegularExpressionValidator>
            
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblApel" runat="server" Text="Apellidos:" style="margin-left:77px; " ></asp:Label>
                <asp:TextBox ID="txtApellidos" runat="server" Width="40%"></asp:TextBox>
         
                <asp:RegularExpressionValidator runat="server" ID="reqApellidos" ControlToValidate="txtApellidos"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca unos apellidos válidos" 
                                        ValidationExpression=" [^a-zA-Z \-]|( )|(\-\-)|(^\s*$)">
               </asp:RegularExpressionValidator>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblEMail" runat="server" Text="E-Mail:" style="margin-left:97px; " ></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="40%"></asp:TextBox>
            
                <asp:RegularExpressionValidator runat="server" ID="reqEmail" ControlToValidate="txtEmail"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca una dirección de email válida, por ejemplo: persona@dominio.com" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
               </asp:RegularExpressionValidator>
            
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblFecNac" runat="server" Text="Fecha Nacimiento:" style="margin-left:20px; " ></asp:Label>
                <asp:TextBox ID="txtFecNac" runat="server" Width="40%"></asp:TextBox>
           
                <asp:RegularExpressionValidator runat="server" ID="regtxtFnac" ControlToValidate="txtFecNac"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca una fecha correcta, por ejemplo: 12/2/2015" 
                                        ValidationExpression="^(([0-9])|([0-2][0-9])|(3[0-1]))\/(([1-9])|(0[1-9])|(1[0-2]))\/(([0-9][0-9])|([1-2][0,9][0-9][0-9]))$">
               </asp:RegularExpressionValidator>
            
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblTfnoCasa" runat="server" Text="Teléfono Casa:" style="margin-left:45px; " ></asp:Label>
                <asp:TextBox ID="txtTfnoCasa" runat="server" Width="40%"></asp:TextBox>
           
                <asp:RegularExpressionValidator runat="server" ID="reqTelefono" ControlToValidate="txtTfnoCasa"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca un telefono válida con 10 dígitos, sin caracteres especiales, como -" 
                                        ValidationExpression="^(\d{10})$">
               </asp:RegularExpressionValidator>
            
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblMovil" runat="server" Text="Teléfono Móvil:" style="margin-left:41px; " ></asp:Label>
                <asp:TextBox ID="txtMovil" runat="server" Width="40%"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ID="reqMovil" ControlToValidate="txtMovil"
                                    ErrorMessage="Error" 
                                        ToolTip="Introduzca un telefono válida con 10 dígitos, sin caracteres especiales, como -" 
                                        ValidationExpression="^(\d{10})$">
               </asp:RegularExpressionValidator>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblCargo" runat="server" Text="Emp/Cargo:" style="margin-left:63px; " ></asp:Label>
                <asp:TextBox ID="txtCargo" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblPais" runat="server" Text="País:" style="margin-left:111px; " ></asp:Label>
                <asp:TextBox ID="txtPais" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblEstado" runat="server" Text="Estado:" style="margin-left:94px; " ></asp:Label>
                <asp:DropDownList runat="server" ID="cboEstado" Width="40%" DataSourceID="AVE_EstadosDataSource"
                                DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                                <asp:ListItem Text=" " Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblPoblacion" runat="server" Text="Población:" style="margin-left:73px; " ></asp:Label>
                <asp:TextBox ID="txtPoblacion" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblbCP" runat="server" Text="CP:" style="margin-left:121px; " ></asp:Label>
                <asp:TextBox ID="txtCP" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" style="margin-left:76px;  vertical-align:top; " ></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server" TextMode="Multiline" Columns="58"
                Rows="4" Width="60%"></asp:TextBox>
            </div>
             <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblTalla" runat="server" Text="Talla:" style="margin-left:106px; " ></asp:Label>
                <asp:TextBox ID="txtTalla" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblTfnoTjo" runat="server" Text="Teléfono Trabajo:" style="margin-left:26px; " ></asp:Label>
                <asp:TextBox ID="txtTfnoTjo" runat="server" Width="40%"></asp:TextBox>
            </div>
             <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblFax" runat="server" Text="Fax:" style="margin-left:115px; " ></asp:Label>
                <asp:TextBox ID="txtFax" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div style="MARGIN-TOP:8px;margin-bottom:8px">
                <asp:Label ID="lblContacto" runat="server" Text="Contacto:" style="margin-left:80px; " ></asp:Label>
                <asp:TextBox ID="txtContacto" runat="server" Width="40%"></asp:TextBox>
            </div>
             <div style="MARGIN-TOP:8px;margin-bottom:20px">
                <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" style="margin-left:55px;  vertical-align:top; " ></asp:Label>
                <asp:TextBox ID="txtComentarios" runat="server" TextMode="Multiline" Columns="58"
                Rows="4" Width="60%"></asp:TextBox>
            </div>
        </div>
    </div>
    <div id="Fact9"style=" width:40%; display:inline-block; margin-top:8%; ">
    <div id="PanelFact"  style="margin-top: 5%;">
          <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    
         <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Conditional">

            <ContentTemplate>
         <asp:Button runat="server" ID="btnActivarFactura" Text="Datos de Facturación"  style="margin-left:40%; "
                                     onclick="btnActivarFactura_Click" 
             UseSubmitBehavior="false" CausesValidation="False" />
         
               <asp:Panel ID="Panel1" class ="Panel" runat="server" Visible="False" style="border-style:double; margin-top:10px;">
               <div style="MARGIN-TOP:20px;margin-bottom:5px">
                    <br />
                    <asp:DropDownList ID="ListaRFC" runat="server" OnSelectedIndexChanged="ListaRFC_SelectedIndexChanged" AutoPostBack="true"   style="margin-left:40%; ">
                    <asp:ListItem Value="0">Nuevo</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="MARGIN-TOP:20px;margin-bottom:5px">
                    
                    <asp:Label ID="lblRFC" runat="server" Text="RFC:" style="margin-left:99px; " ></asp:Label>
                    <asp:TextBox ID="txtRFC" runat="server" Width="60%"></asp:TextBox>
                </div>
                
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblNomFac" runat="server" Text="Nombre:" style="margin-left:72px; " ></asp:Label>
                        <asp:TextBox ID="txtNomFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                 <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblDomFac" runat="server" Text="Dirección:" style="margin-left:62px; " ></asp:Label>
                        <asp:TextBox ID="txtDomFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblNumExt" runat="server" Text="No. Exterior:" style="margin-left:45px; " ></asp:Label>
                        <asp:TextBox ID="txtNumExt" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblNumInt" runat="server" Text="No. Interior:" style="margin-left:45px; " ></asp:Label>
                        <asp:TextBox ID="txtNumInt" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia:" style="margin-left:74px; " ></asp:Label>
                        <asp:TextBox ID="txtColonia" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblPaisFac" runat="server" Text="País:" style="margin-left:97px; " ></asp:Label>
                        <asp:TextBox ID="txtPaisFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblEstadoFac" runat="server" Text="Estado:" style="margin-left:80px; " ></asp:Label>
                    <asp:DropDownList runat="server" ID="cbEstadoFac" Width="60%" DataSourceID="AVE_EstadosDataSource"
                                DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                    <asp:ListItem Text=" " Value="0"></asp:ListItem>
                </asp:DropDownList>

                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblPobFac" runat="server" Text="Población:" style="margin-left:60px; " ></asp:Label>
                        <asp:TextBox ID="txtPobFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblCPFac" runat="server" Text="CP:" style="margin-left:108px; " ></asp:Label>
                        <asp:TextBox ID="txtCPFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                 <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblTfnoFac" runat="server" Text="Teléfono:" style="margin-left:68px; " ></asp:Label>
                        <asp:TextBox ID="txtTfnoFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblMovilFac" runat="server" Text="Móvil:" style="margin-left:90px; " ></asp:Label>
                        <asp:TextBox ID="txtMovilFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblFaxFac" runat="server" Text="Fax:" style="margin-left:103px; " ></asp:Label>
                        <asp:TextBox ID="txtFaxFac" runat="server" Width="60%"></asp:TextBox>
                </div>
                <div style="MARGIN-TOP:5px;margin-bottom:5px">
                    <asp:Label ID="lblMailFac" runat="server" Text="eMail:" style="margin-left:90px; " ></asp:Label>
                        <asp:TextBox ID="txtMailFac" runat="server" Width="60%"></asp:TextBox>
                </div>
              
                 <div style="MARGIN-TOP:5px;margin-bottom:20px">
                   <asp:CheckBox ID="chkEnvMail" runat="server" Text="Envia por Mail:" 
                        TextAlign="Left" style="margin-left:32px; " />
                </div>
                 <div style="MARGIN-TOP:5px;margin-bottom:20px">
                  <asp:Button runat="server" ID="btnGuardarFact" Text="Guardar RFC"  style="margin-left:70%; "
                                     onclick="btnGuardarFactc_Click"  UseSubmitBehavior="false" CausesValidation="False" />
                   </div>
               </asp:Panel>
               </ContentTemplate>

               </asp:UpdatePanel>
              
         </div>
         <div style="MARGIN-TOP:5%;">
            <asp:Button runat="server" ID="btnActualizar" Text="Actualizar Datos"  style="margin-left:40%; "
                                     onclick="btnActualizar_Click" />
        </div>
        
    </div>
     
</div>
<asp:SqlDataSource ID="AVE_EstadosDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="SELECT * FROM [N_PROVINCIAS]"></asp:SqlDataSource>
</asp:Content>
   