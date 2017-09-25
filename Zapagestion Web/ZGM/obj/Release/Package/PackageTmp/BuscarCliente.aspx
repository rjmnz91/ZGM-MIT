<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BuscarCliente.aspx.cs" Inherits="AVE.BuscarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnBuscar">
        <asp:TextBox ID="txtFiltro" runat="server" MaxLength="50"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnBuscar" runat="server" Text="<%$ Resources:Resource, Buscar%>" onclick="btnBuscar_Click" />
        <br /><br />
        
        <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id_Cliente" AllowSorting="true" 
            onselectedindexchanged="grdClientes_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ButtonType="Button" SelectText="&gt;" ShowSelectButton="True" />
                <asp:BoundField DataField="id_Cliente" HeaderText="<%$ Resources:Resource, IdCliente%>" Visible="false"/>
                <asp:BoundField DataField="Cif" HeaderText="<%$ Resources:Resource, CIF%>" SortExpression="Cif" />
                <asp:BoundField DataField="Nombre" HeaderText="<%$ Resources:Resource, Nombre%>" SortExpression="Nombre" />
                <asp:BoundField DataField="Apellidos" HeaderText="<%$ Resources:Resource, Apellidos%>" SortExpression="Apellidos" />
                <asp:BoundField DataField="Direccion" HeaderText="<%$ Resources:Resource, Direccion%>" SortExpression="Direccion" />
                <asp:BoundField DataField="CodPostal" HeaderText="<%$ Resources:Resource, CodigoPostal%>" SortExpression="CodPostal" />
                <asp:BoundField DataField="Poblacion" HeaderText="<%$ Resources:Resource, Poblacion%>" SortExpression="Poblacion" />
                <asp:BoundField DataField="Provincia" HeaderText="<%$ Resources:Resource, Provincia%>" SortExpression="Provincia" />
                <asp:BoundField DataField="Pais" HeaderText="<%$ Resources:Resource, Pais%>" SortExpression="Pais" />
            </Columns>
        </asp:GridView>    
    </asp:Panel>
    
    <asp:SqlDataSource ID="SDSClientes" runat="server" CancelSelectOnNullParameter="false"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_ClienteBuscar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFiltro" Name="Filtro" PropertyName="Text" 
                Type="String" ConvertEmptyStringToNull="true" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
