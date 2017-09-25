
<%@ Page Title="<%$ Resources:Resource, Inventario%>" Language="C#" MasterPageFile="~/MasterPage.Master" Theme="Tema" AutoEventWireup="true" CodeBehind="Inventarios.aspx.cs" Inherits="AVE.Inventarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentInventarios" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Panel que se carga con las Ordenes de Inventario--%>
    <asp:Panel ID="pnlOI" runat="server" Visible="false">
        <asp:SqlDataSource ID="SDSOrdenesInventario" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
            SelectCommand="AVE_OrdenesInventarioBuscar" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="IdTienda" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:GridView runat="server" ID="grdOI" AutoGenerateColumns="false" DataKeyNames="IdOrdenInventario"
            SkinId="gridviewSkin" EmptyDataText="<%$ Resources:Resource, NoOrdenesInventario%>" 
            DataSourceID="SDSOrdenesInventario" 
            onselectedindexchanged="grdOI_SelectedIndexChanged"  >
            <Columns>
                <asp:TemplateField Visible="true" ItemStyle-CssClass="GridItem" >
                    <ItemTemplate><asp:Button runat="server" CommandName="select" ID="lnkSelect" Text=" > "/></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Nombre" HeaderText="<%$ Resources:Resource, OrdenInventario%>" ItemStyle-CssClass="GridItem" />
                <asp:BoundField DataField="FechaInventarioIni" HeaderText="<%$ Resources:Resource, Inicio%>" HtmlEncode="false" DataFormatString="{0:d}" ItemStyle-CssClass="GridItem"/>
                <asp:BoundField DataField="FechaInventarioFin" HeaderText="<%$ Resources:Resource, Fin%>" HtmlEncode="false" DataFormatString="{0:d}" ItemStyle-CssClass="GridItem" />
                <asp:CheckBoxField DataField="OIRealizada" HeaderText="<%$ Resources:Resource, Realizado%>" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
        <br />
         <asp:Button ID="btnNuevo" runat="server" Text="<%$ Resources:Resource, Nuevo%>" 
            CssClass="boton4" onclick="btnNuevo_Click"  />
     </asp:Panel>
   
    <%--Panel que se carga al no haber inventario pendiente, para crear uno nuevo--%>
    <asp:Panel ID="pnlNuevo" runat="server" DefaultButton="btnCrear" Visible="false">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblTipoNuevo" runat="server" Text="<%$ Resources:Resource, TipoInventario%>" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoInventario" runat="server" CssClass="boton"
                        DataTextField="Nombre" DataValueField="IdTipoInventario" AutoPostBack="false"
                        DataSourceID="SDSTipoInventario"></asp:DropDownList>
                    <asp:SqlDataSource ID="SDSTipoInventario" runat="server"
                        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                        SelectCommand="AVE_Tipos_InventarioObtener" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblEmpresaNuevo" runat="server" Text="<%$ Resources:Resource, Empresa%>" CssClass="negrita"></asp:Label><br>
        <asp:TextBox ID="txtEmpresa" runat="server" MaxLength="50"></asp:TextBox>&nbsp;
        <asp:RequiredFieldValidator ID="rfvEmpresa" Runat="server"
            ControlToValidate="txtEmpresa" EnableClientScript="True" ValidationGroup="CrearInventario"
            ErrorMessage="*" CssClass="mensajeRequerido"></asp:RequiredFieldValidator>
        <br>
        
        
        <asp:Button ID="btnCrear" runat="server" Text="<%$ Resources:Resource, Crear%>" 
            CssClass="boton4" onclick="btnCrear_Click" ValidationGroup="CrearInventario"/>
    </asp:Panel>
    
    <%--Panel que se carga al haber inventario pendiente.--%>
    <asp:Panel ID="pnlPendiente" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTipoEti" runat="server" Text="<%$ Resources:Resource, TipoInventario%>" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTipo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModalidadEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, ModalidadVisualizacion%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblModalidad" runat="server" Text=""></asp:Label>
                </td>
            <tr>
                <td>
                    <asp:Label ID="lblEmpresaEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, Empresa%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmpresa" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTiendaEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, Tienda%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTienda" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmpleadoEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, Empleado%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTerminalEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, Terminal%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTerminal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrdenInventarioEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, OrdenInventario%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblOrdenInventario" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaCreacionEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, FechaCreacion%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFechaCreacion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaUltimaModificacionEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, FechaUltimaModificacion%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFechaUltimaModificacion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCantidadTotalUnidadesEti" runat="server" CssClass="negrita" 
                        Text="<%$ Resources:Resource, CantidadTotalUnidades%>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblArticulosTotales" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAbrir" runat="server" Text="<%$ Resources:Resource, Abrir%>" 
            CssClass="boton4" onclick="btnAbrir_Click"/>
        &nbsp;
        
        <asp:Button ID="btnEliminar" runat="server" 
            Text="<%$ Resources:Resource, Eliminar%>" CssClass="boton4" 
            onclick="btnEliminar_Click"/>
        &nbsp;
        
        <asp:Button ID="btnFinalizar" runat="server" 
            Text="<%$ Resources:Resource, Finalizar%>" CssClass="boton4" 
            onclick="btnFinalizar_Click"/>
        &nbsp;
    </asp:Panel>
    
    <asp:SqlDataSource ID="SDSInventario" runat="server"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        DeleteCommand="dbo.AVE_InventariosPendientesBorrar" DeleteCommandType="StoredProcedure"
        UpdateCommand="dbo.AVE_InventariosPendientesFinalizar" UpdateCommandType="StoredProcedure"
        InsertCommand="dbo.AVE_InventariosPendientesCrear" InsertCommandType="StoredProcedure"
        SelectCommand="dbo.AVE_InventariosPendientesBuscar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IdTienda" Type ="String" />
            <asp:Parameter Name="IdEmpleado" Type ="String" />
            <asp:Parameter Name="IdTerminal" Type="String"/>
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="IdInventario" Type ="int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="IdInventario" Type ="int32" />
            <asp:Parameter Name="Usuario" Type ="String" />
            <asp:Parameter Name="IdOrdenInventario" Type="String"/>
            <asp:Parameter Name="IdTienda" Type="String"/>
            <asp:Parameter Name="ObservacionesTienda" Type="String"/>
        </UpdateParameters>
        <InsertParameters>
            <asp:ControlParameter Name="IdTipoInventario" ControlID="cmbTipoInventario" PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter Name="IdTipoVista" Type="Int32" />
            <asp:ControlParameter Name="Empresa" ControlID="txtEmpresa" PropertyName="Text" Type="String" />
            <asp:Parameter Name="IdTienda" Type ="String" />
            <asp:Parameter Name="IdEmpleado" Type ="int32" />
            <asp:Parameter Name="IdTerminal" Type="String"/>
            <asp:Parameter Name="IdOrdenInventario" Type="Int32" ConvertEmptyStringToNull="true"/>
            <asp:Parameter Name="Usuario" Type ="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>