<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  Theme="Tema" Inherits="StockEnTienda" Codebehind="StockEnTienda.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
      
        function AgregarTalla(Label) {

            if (Label.innerHTML != '')
            {

                var strRegistro = Label.innerHTML;
                if (Label.className == 'Negrita') {
                    Talla_Selecionada.value += strRegistro + '|';
                    Label.className = 'TallaSelecionada';
                }
                else {

                    Talla_SelecionadaNo.value += strRegistro + '|';
                    Label.className = 'Negrita';
                    

                } 
            }
            
            return true;

        }


     </script>

    <div class="barraNavegacion">
        <asp:Button ID="btnComplementos" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, Complementos%>" Enabled="false" 
            onclick="btnComplementos_Click" Visible="False" />        
        <asp:Button ID="btnSustitutivos" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, Sustitutivos%>" Enabled="false" 
            onclick="btnSustitutivos_Click" Visible="False" />        
        <asp:Button ID="btnMasTiendas" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, MasTiendas%>"  Enabled="false" 
            onclick="btnMasTiendas_Click" Visible="False" />        
        <asp:Button ID="btnFoto" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, Foto%>"  Enabled="false" onclick="btnFoto_Click" 
            Visible="False" />        
        <asp:Button ID="btnDetalles" runat="server" CssClass="botonNavegacion" 
        Text="<%$ Resources:Resource, Detalles%>"  Enabled="false" 
            onclick="btnDetalles_Click" Visible="False" />
         <center><asp:Button ID="btnVolver" runat="server" TabIndex="1" CssClass="botonNavegacion"  Text="<%$ Resources:Resource, Volver%>" onclick="btnVolver_Click"/>
         <asp:TextBox ID="txtBusquedaProducto" runat="server" TabIndex="2" CssClass="txtBuscar" MaxLength="50" style=" width:100px;"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" TabIndex="3" CssClass="botonNavegacion" Text="<%$ Resources:Resource, Buscar%>" onclick="btnBuscar_Click"/></center> 
                         
    </div>
     <a href="javascript:history.back();"></a>
     <div id="PnlFoto" style="float:left;display:table-cell;vertical-align:top;">
     <table>
     
     <tr>
     <td colspan=2><asp:Label ID="lblDescripcion" runat="server" BorderStyle="None" Font-Bold="True"></asp:Label><br/></td>
     </tr>
     <tr>
     <td><asp:Image ID="FotoArticulo" runat="server" style=" width:240px;" /></td>
     <td valign=top><asp:Label ID="lblPrecioValor" runat="server"></asp:Label></td>
     </tr>
     </table>
     
         
     

     <asp:SqlDataSource ID="AVE_ArticuloFotoObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloFotoObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>
       <asp:Button ID="btnPrecio" runat="server" Text="✚" onclick="btnPrecio_Click" CssClass="boton2" Visible="false"  />
    </div>
    <div id="pnlModelo">
        <asp:Panel ID="PModelo" runat="server">
        
        </asp:Panel>
    </div>
    <br/>
    <br/>
    <div style="float:left;" >
    <asp:GridView runat="server" ID="GridView2" AutoGenerateColumns="false" DataKeyNames="Articulo"
        SkinId="gridviewSkin" 
        onselectedindexchanged="GridView2_SelectedIndexChanged" Visible="False" >
        <RowStyle BackColor="White"/>
                <AlternatingRowStyle BackColor="PaleTurquoise" />
        <Columns>
        	<asp:TemplateField Visible="true" ItemStyle-CssClass="GridItem" >
                <ItemTemplate><asp:Button runat="server" CommandName="select" ID="lnkSelect" Text="✚" /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Articulo" HeaderText="<%$ Resources:Resource, Articulo%>" ItemStyle-CssClass="GridItem" />
            <asp:BoundField DataField="Color" HeaderText="<%$ Resources:Resource, Color%>" ItemStyle-CssClass="GridItem" />
        </Columns>
    </asp:GridView>
    <asp:GridView runat="server" ID="GridStock" AutoGenerateColumns="false" 
        SkinId="gridviewSkin" onrowdatabound="GridStock_RowDataBound" >
        <RowStyle BackColor="White" />
                <AlternatingRowStyle BackColor="PaleTurquoise" />
        <Columns>
            <asp:BoundField DataField="IdTienda" HeaderText="<%$ Resources:Resource, ID%>" HeaderStyle-Width="70" ItemStyle-CssClass="GridItem" />
            <asp:BoundField DataField="Tienda" HeaderText="<%$ Resources:Resource, Tienda%>" ItemStyle-CssClass="GridItem" />
            <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Resource, Total%>" HeaderStyle-Width="50" ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
    <asp:HiddenField ID="hiddenTallas" runat="server" />
    <asp:HiddenField ID="hiddenTallasNo" runat="server" 
    onvaluechanged="hiddenTallasNo_ValueChanged" />
    <asp:Panel ID="PnlTallaje" runat="server">
    </asp:Panel>
     <asp:Panel ID="PnlSolicitar" runat="server">
     &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%--<asp:Button ID="Button1" 
             runat="server" Text="Solicitar" Font-Size="X-Large" 
             Height="48px" Width="269px" onclick="Button1_Click" Visible="False"/>--%></asp:Panel>
    
    <asp:SqlDataSource ID="AVE_StockEnTiendaObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_StockEnTiendaObtener" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="String" />
            <asp:Parameter Name="IdTienda" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
     
    <asp:SqlDataSource ID="AVE_StockEnTiendaCSObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_StockEnTiendaCSObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="int32" />
            <asp:Parameter Name="IdTienda" Type="String" />
            <asp:QueryStringParameter Name="Tipo" QueryStringField="Tipo" Type="Char" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSTieneCS" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="AVE_ProductoTieneCS" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataTallajes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_TallajeArticulo" SelectCommandType="StoredProcedure">
       <SelectParameters>
            <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="int32" />
       </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSPedido" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
            InsertCommand="AVE_PedidosCrear" InsertCommandType="StoredProcedure" 
            OnInserted="SDSPedido_Inserted">
            <InsertParameters>
                <asp:Parameter Name="IdArticulo" Type="Int32" />
                <asp:Parameter Name="Talla" Type="String" />
                <asp:Parameter Name="Unidades" Type="Int16" />
                <asp:Parameter Name="Precio" Type="Decimal" />
                <asp:Parameter Name="IdEmpleado" Type="Int32" />
                <asp:Parameter Name="Usuario" Type="String" />
                <asp:Parameter Name="IdTienda" Type="String" />
                <asp:Parameter Name="Stock" Type="Int16" />
                <asp:Parameter Name="IdPedido" Type="Int32" Direction="ReturnValue" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataModColores" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
            SelectCommand="AVE_ColorModFabricante" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="IdArticulo" QueryStringField="IdArticulo" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
</div>
</asp:Content>

