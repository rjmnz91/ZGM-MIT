<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  Theme="Tema" Inherits="StockOtras" Codebehind="StockOtras.aspx.cs"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="barraNavegacion">
     <asp:Button ID="btnFoto" runat="server" CssClass="botonNavegacion" 
     Text="<%$ Resources:Resource, Foto%>"  Enabled="false" onclick="btnFoto_Click" />        
     </div>
    <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
    |
    <asp:Label ID="lblPrecio" runat="server" Text="<%$ Resources:Resource, Precio%>"></asp:Label>        
    <asp:Label ID="lblPrecioValor" runat="server"></asp:Label>
    $     
    <asp:Button ID="btnPrecio" runat="server" Text="+" onclick="btnPrecio_Click" CssClass="boton2" />
    <br/><br/>
    <asp:GridView runat="server" ID="GridStock" AutoGenerateColumns="false" 
        SkinId="gridviewSkin" onrowdatabound="GridStock_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="IdTienda" HeaderText="<%$ Resources:Resource, ID%>" HeaderStyle-Width="70" ItemStyle-CssClass="GridItem" />
            <asp:BoundField DataField="Tienda" HeaderText="<%$ Resources:Resource, Tienda%>" ItemStyle-CssClass="GridItem" />
            <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Resource, Total%>" HeaderStyle-Width="50" ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="SDSStock"
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_StockOtrasTiendasObtener" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="Int32"/>
            <asp:Parameter Name="IdTienda" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

