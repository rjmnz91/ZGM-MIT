<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Configuraciones.aspx.cs" Inherits="AVE.Configuraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SDSConfiguraciones" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_ConfiguracionesObtener" SelectCommandType="StoredProcedure" 
        UpdateCommand="AVE_ConfiguracionesGuardar" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IdTienda" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IdTienda" Type="String" />
            <asp:Parameter Name="IdTipoVista" Type="Int32" />
            <asp:Parameter Name="Usuario" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
 
    <asp:SqlDataSource ID="SDSModoVisualizacion" runat="server"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_Tipos_Vista_InventarioObtener" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>   
 
    <br />
    <asp:DetailsView ID="DetailsView1" runat="server" 
        AutoGenerateRows="False" DataKeyNames="IdTienda" 
        DataSourceID="SDSConfiguraciones">
        <Fields>
            <asp:BoundField HeaderText="<%$ Resources:Resource, Tienda %>" DataField="IdTienda" SortExpression="IdTienda" ReadOnly="true" />
            <asp:TemplateField HeaderText="<%$ Resources:Resource, ModalidadVisualizacion %>" 
                SortExpression="IdTipoVista">
                <EditItemTemplate>
                    <asp:DropDownList ID="cmbModoVisualizacion" runat="server" CssClass="boton" SelectedValue='<%# Bind("IdTipoVista") %>' 
                        DataTextField="Nombre" DataValueField="IdTipoVista" AutoPostBack="false"
                        DataSourceID="SDSModoVisualizacion"></asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="cmbModoVisualizacion" runat="server" CssClass="boton" SelectedValue='<%# Bind("IdTipoVista") %>' 
                        DataTextField="Nombre" DataValueField="IdTipoVista" AutoPostBack="false"
                        DataSourceID="SDSModoVisualizacion" Enabled="false"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
