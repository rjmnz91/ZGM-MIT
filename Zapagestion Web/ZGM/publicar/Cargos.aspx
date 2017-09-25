<%@ Page Title="<%$ Resources:Resource, NuevoCargo%>" Language="C#" MasterPageFile="~/MasterPage.Master" Theme="Tema" AutoEventWireup="true" CodeBehind="Cargos.aspx.cs" Inherits="AVE.Cargos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentCargos" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlCargo" runat="server" DefaultButton="btnCrear">
    <br>
    <asp:Label ID="lblTiendaOrigenEti" runat="server" Text="<%$ Resources:Resource, TiendaOrigen%>" CssClass="negrita"></asp:Label>
    &nbsp;
    <asp:Label ID="lblTiendaOrigen" runat="server" Text=""></asp:Label>
    <br>
    <br>
    <asp:Label ID="lblTiendaDestinoEti" runat="server" Text="<%$ Resources:Resource, TiendaDestino%>" CssClass="negrita"></asp:Label>
    <br>
    <asp:DropDownList ID="cmbTiendaDestino" runat="server" AutoPostBack="false" 
        CssClass="combo1" DataSourceID="SDSTiendaDestino" DataTextField="Tienda" 
        DataValueField="IdTienda" Width="226px">
    </asp:DropDownList>
    <br>
    <br>
    <asp:Label ID="lblNumeroCargoEti" runat="server" Text="<%$ Resources:Resource, NumeroTraspaso%>" CssClass="negrita"></asp:Label>
    <br>
    <asp:TextBox ID="txtNumeroCargo" runat="server" MaxLength="15" CssClass="boton" 
        Width="119px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNuevoCargo" Runat="server"
            ControlToValidate="txtNumeroCargo" EnableClientScript="True" ValidationGroup="NuevoCargo"
            ErrorMessage="*" CssClass="mensajeRequerido"></asp:RequiredFieldValidator>
    <asp:Button ID="btnGenerarNumero" runat="server" CssClass="boton4"
        OnClick="btnGenerarNumero_Click" Text="<%$ Resources:Resource, GenerarNumero%>" />
    <br>
    <asp:Button ID="btnCrear" runat="server" CssClass="boton4" ValidationGroup="NuevoCargo"
        OnClick="btnCrear_Click" Text="<%$ Resources:Resource, Crear%>" />
    </asp:Panel>
    
    
    <asp:SqlDataSource ID="SDSTiendaDestino" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="dbo.AVE_TiendasObtener" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblTiendaOrigen" Name="idTienda" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSGenerar" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="dbo.AVE_CargosIdCargoGenerar" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IdTienda" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSCrear" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        InsertCommand="dbo.AVE_CargosCrear" InsertCommandType="StoredProcedure">
        <InsertParameters>
            <asp:Parameter Name="IdCargo" Type="Int32" />
            <asp:Parameter Name="IdTiendaOrigen" Type="String" />
            <asp:Parameter Name="IdTiendaDestino" Type="String" />
            <asp:Parameter Name="IdEmpleado" Type="int32" />
            <asp:Parameter Name="IdTerminal" Type="String" />
            <asp:Parameter Name="Usuario" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>