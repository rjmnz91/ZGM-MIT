<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PedidosDetalle.aspx.cs" Inherits="AVE.PedidosDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function alternarEntrega(check) {
            if (check.checked == true) {
                document.getElementById("divDatosEntrega").style.display = 'none';
            }
            else {
                document.getElementById("divDatosEntrega").style.display = 'block';
            }
        }
    </script>
   <br/>    
    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Resource, IdPedido%>" CssClass="negrita"/>
    &nbsp;
    <asp:Label ID="lblIdPedido" runat="server" Text=""/>
    &nbsp;&nbsp;
    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Resource, Descripcion%>" CssClass="negrita"/>
    &nbsp;
    <asp:Label ID="lblDescripcion" runat="server" Text=""/>
    &nbsp;&nbsp;
    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Resource, Talla%>" CssClass="negrita"/>
    &nbsp;
    <asp:Label ID="lblTalla" runat="server" Text=""/>
    &nbsp;&nbsp;
    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Resource, Unidades%>" CssClass="negrita"/>
    &nbsp;
    <asp:Label ID="lblUnidades" runat="server" Text=""/>
    &nbsp;&nbsp;
    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Resource, Precio%>" CssClass="negrita"/>
    &nbsp;
    <asp:Label ID="lblPrecio" runat="server" Text=""/>
    $
    <asp:Button ID="btnPrecio" runat="server" Text="+" onclick="btnPrecio_Click" CssClass="boton2" />
    <br /><br />
    <asp:Button ID="btnBuscarCliente" runat="server" Text="<%$ Resources:Resource, BuscarCliente%>" onclick="btnBuscarCliente_Click" />
    &nbsp;
    <asp:Button ID="btnGuardarPedido" runat="server" Text="<%$ Resources:Resource, Guardar%>" 
        onclick="btnGuardarPedido_Click" />

    <br /><br />
    
    <fieldset title="<%= Resources.Resource.DatosCliente%>" style="width:1%;" >
        <legend><asp:Label ID="LtrDtCliente" runat="server" Text="<%$ Resources:Resource, DatosCliente%>" CssClass="negrita"></asp:Label></legend>
        <asp:HiddenField ID="hddIdCliente" runat="server" />
        <table width="600px">
            <tr>
                <td><asp:Label ID="Label27" runat="server" Text="<%$ Resources:Resource, Cif%>"></asp:Label></td>
                <td><asp:TextBox ID="txtCif" runat="server" MaxLength="50"></asp:TextBox></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Nombre%>"></asp:Label></td>
                <td><asp:TextBox ID="txtNombreC" runat="server" MaxLength="60"></asp:TextBox></td>
                <td><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Apellidos%>"></asp:Label></td>
                <td><asp:TextBox ID="txtApellidosC" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Direccion%>"></asp:Label></td>
                <td><asp:TextBox ID="txtDireccionC" runat="server" MaxLength="150"></asp:TextBox></td>
                <td><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, CodigoPostal%>"></asp:Label></td>
                <td><asp:TextBox ID="txtCodPostalC" runat="server" MaxLength="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Poblacion%>"></asp:Label></td>
                <td><asp:TextBox ID="txtPoblacionC" runat="server" MaxLength="100"></asp:TextBox></td>
                <td><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, Provincia%>"></asp:Label></td>
                <td><asp:TextBox ID="txtProvinciaC" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, Pais%>"></asp:Label></td>
                <td><asp:TextBox ID="txtPaisC" runat="server" MaxLength="100"></asp:TextBox></td>
                <td><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource, Email%>"></asp:Label></td>
                <td><asp:TextBox ID="txtEmailC" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, Telefono%>"></asp:Label></td>
                <td><asp:TextBox ID="txtTelefonoC" runat="server" MaxLength="50"></asp:TextBox></td>
                <td><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Movil%>"></asp:Label></td>
                <td><asp:TextBox ID="txtMovilC" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label12" runat="server" Text="<%$ Resources:Resource, Observaciones%>"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtObservacionesC" Columns="52" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox></td>
            </tr>
        </table>	
            <asp:CheckBox ID="chkActualizarCliente" runat="server" Text="<%$ Resources:Resource, DatosClienteActualizar%>" Checked="false" Visible="false"/>

    </fieldset>
    <br />
    <asp:CheckBox ID="chkDireccionEntrega" runat="server" Text="<%$ Resources:Resource, DireccionEntregaCoincide%>" Checked="true" onclick="alternarEntrega(this);"/>
    <br /><br />
    <div id="divDatosEntrega" style=" display:none;">
        <fieldset title="<%= Resources.Resource.DatosEntrega%>" style="width:1%;" >
            <legend><asp:Label ID="Label0" runat="server" Text="<%$ Resources:Resource, DatosEntrega%>" CssClass="negrita"></asp:Label></legend>
            <table width="600px">
                <tr>
                    <td><asp:Label ID="Label16" runat="server" Text="<%$ Resources:Resource, Nombre%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtNombreD" runat="server" MaxLength="60"></asp:TextBox></td>
                    <td><asp:Label ID="Label18" runat="server" Text="<%$ Resources:Resource, Apellidos%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtApellidosD" runat="server" MaxLength="100"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label19" runat="server" Text="<%$ Resources:Resource, Direccion%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtDireccionD" runat="server" MaxLength="150"></asp:TextBox></td>
                    <td><asp:Label ID="Label20" runat="server" Text="<%$ Resources:Resource, CodigoPostal%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtCodPostalD" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label21" runat="server" Text="<%$ Resources:Resource, Poblacion%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtPoblacionD" runat="server" MaxLength="100"></asp:TextBox></td>
                    <td><asp:Label ID="Label22" runat="server" Text="<%$ Resources:Resource, Provincia%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtProvinciaD" runat="server" MaxLength="100"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label23" runat="server" Text="<%$ Resources:Resource, Pais%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtPaisD" runat="server" MaxLength="100"></asp:TextBox></td>
                    <td><asp:Label ID="Label24" runat="server" Text="<%$ Resources:Resource, Email%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtEmailD" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label25" runat="server" Text="<%$ Resources:Resource, Telefono%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtTelefonoD" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td><asp:Label ID="Label26" runat="server" Text="<%$ Resources:Resource, Movil%>"></asp:Label></td>
                    <td><asp:TextBox ID="txtMovilD" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label28" runat="server" Text="<%$ Resources:Resource, Observaciones%>"></asp:Label></td>
                    <td colspan="3"><asp:TextBox ID="txtObservacionesD" Columns="52" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                </tr>
            </table>	
        </fieldset>
    </div>
    
    <br />
    <asp:Button ID="btnBuscarCliente2" runat="server" Text="<%$ Resources:Resource, BuscarCliente%>" onclick="btnBuscarCliente_Click" />
    &nbsp;
    <asp:Button ID="btnGuardarPedido2" runat="server" Text="<%$ Resources:Resource, Guardar%>" onclick="btnGuardarPedido_Click" />

  
    <asp:SqlDataSource ID="SDSPedido" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_PedidosObtener" SelectCommandType="StoredProcedure" 
        UpdateCommand="AVE_PedidoActualizarCliente" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="IdPedido" QueryStringField="IdPedido" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IdPedido" Type="Int32" />
            <asp:Parameter Name="id_Cliente" Type="Int32" />
            <asp:Parameter Name="Cif_Cliente" Type="String" />
            <asp:Parameter Name="Nombre_Cliente" Type="String" />
            <asp:Parameter Name="Apellidos_Cliente" Type="String" />
            <asp:Parameter Name="Direccion_Cliente" Type="String" />
            <asp:Parameter Name="CodPostal_Cliente" Type="String" />
            <asp:Parameter Name="Poblacion_Cliente" Type="String" />
            <asp:Parameter Name="Provincia_Cliente" Type="String" />
            <asp:Parameter Name="Pais_Cliente" Type="String" />
            <asp:Parameter Name="Telefono_Cliente" Type="String" />
            <asp:Parameter Name="Movil_Cliente" Type="String" />
            <asp:Parameter Name="email_Cliente" Type="String" />
            <asp:Parameter Name="Observaciones_Cliente" Type="String" />
            <asp:Parameter Name="Nombre_Destinatario" Type="String" />
            <asp:Parameter Name="Apellidos_Destinatario" Type="String" />
            <asp:Parameter Name="Direccion_Destinatario" Type="String" />
            <asp:Parameter Name="CodPostal_Destinatario" Type="String" />
            <asp:Parameter Name="Poblacion_Destinatario" Type="String" />
            <asp:Parameter Name="Provincia_Destinatario" Type="String" />
            <asp:Parameter Name="Pais_Destinatario" Type="String" />
            <asp:Parameter Name="Telefono_Destinatario" Type="String" />
            <asp:Parameter Name="Movil_Destinatario" Type="String" />
            <asp:Parameter Name="email_Destinatario" Type="String" />
            <asp:Parameter Name="Observaciones_Destinatario" Type="String" />
            <asp:Parameter Name="Usuario" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SDSClientes" runat="server" CancelSelectOnNullParameter="false"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_ClienteObtener" SelectCommandType="StoredProcedure" 
        InsertCommand="AVE_ClienteGuardar" InsertCommandType="StoredProcedure" 
        UpdateCommand="AVE_ClienteGuardar" UpdateCommandType="StoredProcedure" 
        oninserted="SDSClientes_Inserted">
        <SelectParameters>
            <asp:Parameter Name="id_Cliente" Type="Int32"/>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="id_Cliente" Type="Int32" />
            <asp:Parameter Name="Cif" Type="String" />
            <asp:Parameter Name="Nombre" Type="String" />
            <asp:Parameter Name="Apellidos" Type="String" />
            <asp:Parameter Name="Direccion" Type="String" />
            <asp:Parameter Name="CodPostal" Type="String" />
            <asp:Parameter Name="Poblacion" Type="String" />
            <asp:Parameter Name="Provincia" Type="String" />
            <asp:Parameter Name="Telefono" Type="String" />
            <asp:Parameter Name="Movil" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="Pais" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="id_Cliente" Type="Int32" Direction="InputOutput" />
            <asp:Parameter Name="Cif" Type="String" />
            <asp:Parameter Name="Nombre" Type="String" />
            <asp:Parameter Name="Apellidos" Type="String" />
            <asp:Parameter Name="Direccion" Type="String" />
            <asp:Parameter Name="CodPostal" Type="String" />
            <asp:Parameter Name="Poblacion" Type="String" />
            <asp:Parameter Name="Provincia" Type="String" />
            <asp:Parameter Name="Telefono" Type="String" />
            <asp:Parameter Name="Movil" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="Pais" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>
