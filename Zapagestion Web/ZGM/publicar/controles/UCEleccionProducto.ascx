<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCEleccionProducto.ascx.cs" Inherits="AVE.controles.UCEleccionProducto" %>
   <%@ Register Src="~/controles/UCStockEnTienda.ascx" TagName="UCStockEnTienda"    TagPrefix="uc2" %>
     
        <asp:Panel id="Resultado" runat=server>
            <asp:GridView runat="server" ID="grdListaProductos" AutoGenerateColumns="false" DataKeyNames="IdArticulo"
                SkinId="gridviewSkin" EmptyDataText="<%$ Resources:Resource, NoProductosFiltro%>" 
                DataSourceID="AVE_ArticuloBuscarLike"
                onselectedindexchanged="grdListaProductos_SelectedIndexChanged"  >
                <Columns>
        	        <asp:TemplateField Visible="true" ItemStyle-CssClass="GridItem" >
                        <ItemTemplate><asp:Button runat="server" CommandName="select" ID="lnkSelect" Font-Size="X-Large" Text=" > " style="height:60px;width:50px;"/></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StockTienda"  HeaderText="<%$ Resources:Resource, Stock%>" ItemStyle-CssClass="GridItem"/>   
                    <asp:BoundField DataField="StockOtras" HeaderText="<%$ Resources:Resource, StockOtras%>" ItemStyle-CssClass="GridItem" />
                    <asp:BoundField DataField="IdArticulo" HeaderText="<%$ Resources:Resource, Id%>" ItemStyle-CssClass="GridItem" Visible="false" />
                    <asp:BoundField DataField="CodigoAlfa" HeaderText="<%$ Resources:Resource, CodigoAlfa%>" ItemStyle-CssClass="GridItem" Visible="false" />
                    <asp:BoundField DataField="EAN" HeaderText="<%$ Resources:Resource, Ean%>" ItemStyle-CssClass="GridItem" Visible="false" />
                    <asp:BoundField DataField="Modelo" HeaderText="<%$ Resources:Resource, Modelo%>" ItemStyle-CssClass="GridItem" />
                    <asp:BoundField DataField="Descripcion" HeaderText="<%$ Resources:Resource, Descripcion%>" ItemStyle-CssClass="GridItem"/>
                    <asp:BoundField DataField="Seccion" HeaderText="<%$ Resources:Resource, Seccion%>" ItemStyle-CssClass="GridItem" />
               </Columns>
            </asp:GridView>
          
            <%--ORIGEN DE DATOS PARA LAS BÚSQUEDA DE PRODUCTOS--%>
            <asp:SqlDataSource ID="AVE_ArticuloBuscarLike" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
                SelectCommand="dbo.AVE_ArticuloBuscarLike" 
                SelectCommandType="StoredProcedure" DataSourceMode="DataSet" 
                onselected="AVE_ArticuloBuscarLike_Selected">
                <SelectParameters>
                    <asp:Parameter Name="Filtro"  Type="String"/>
                    <asp:Parameter Name="StrTalla" Type="String"/>
                    <asp:Parameter Name="IdCabeceroDetalle"  Type="int32" Direction="Output"/>
                    <asp:Parameter Name="IdArticulo"  Type="int32" Direction="Output"/>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Panel ID="PnlRegistrarBusqueda" runat="server">
    <table style="width: 82%">
        <tr>
            <td class="style2"><asp:Label ID="LtrTipoArticulo" runat="server" Text="<%$ Resources:Resource, TipoArticulo%>" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlSeccion" runat="server" DataSourceID="SqldataSeccion"  
                    DataTextField="Seccion" DataValueField="idSeccion"  CssClass="boton" 
                      Width="314px"/></td>
        </tr>
        <tr>
            <td class="style2"><asp:Label ID="LstMarca" runat="server" Text="Marca" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlMarca" runat="server" DataSourceID="SqlDataMarcas"  
                    DataTextField="NombreComercial" DataValueField="idproveedor"
                      CssClass="boton" Width="314px"/></td> 
        </tr>
        <tr>
            <td class="style2"><asp:Label ID="LblCorte" runat="server" Text="Corte" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlCorte" runat="server" DataSourceID="SqlDataCorte"  
                    DataTextField="Corte" DataValueField="idCorte"
                       CssClass="boton" Width="314px"/></td> 
        </tr>
        <tr>
           <td class="style2"><asp:Label ID="Label2" runat="server" Text="Material" CssClass="negrita"></asp:Label>              
            </td>
            <td><asp:DropDownList ID="ddlMaterial" runat="server" DataSourceID="SqlDataMaterial"  
                    DataTextField="Material" DataValueField="IdMaterial"
                       CssClass="boton"  Width="314px"/></td> 
        </tr>
        <tr>
            <td class="style2"><asp:Label ID="LtrColor" runat="server" Text="Color" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlColor" runat="server" DataSourceID="SqlDataColor"  
                    DataTextField="Color" DataValueField="idColor" CssClass="boton" Width="314px"/></td>            
        </tr>
        <tr>
            <td class="style2"><asp:Label ID="LtrTalla" runat="server" Text="Talla" CssClass="negrita"></asp:Label></td>
            <td><asp:DropDownList ID="ddlTalla" runat="server" DataSourceID="SqlDataTallas"  
                    DataTextField="Nombre_Talla" DataValueField="Nombre_Talla" CssClass="boton" Width="314px"/></td>    
        </tr>
        <tr>
            <td class="style2"><asp:Label ID="LtrObservaciones" runat="server" Text="Comentario" CssClass="negrita"></asp:Label></td>
            <td> 
                <asp:TextBox ID="txtComentario" runat="server" Height="58px" TextMode="MultiLine" 
                    Width="474px" MaxLength="250" ></asp:TextBox></td>
        </tr>
         <tr>
            <td class="style2" ></td>
            <td class="style2">
                <asp:Button ID="Button1" runat="server" Text="Registrar Petición" 
                    Width="300px" onclick="Button1_Click" Height="44px" /></td>          
        </tr>
    </table>
            <asp:sqldatasource ID="SqldataSeccion" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_Secciones" SelectCommandType="StoredProcedure"></asp:sqldatasource>
            <asp:SqlDataSource ID="SqlDataCorte" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_Corte" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataMaterial" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_Material" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataColor" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_Color" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataTallas" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_TallasZG" SelectCommandType="StoredProcedure" ></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataMarcas" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
                SelectCommand="AVE_MARCASPROVEEDOR" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="Stock" runat="server" Width="90%">
      <center><asp:Button ID="btnVolver" runat="server" TabIndex="1" CssClass="botonNavegacion"  Text="<%$ Resources:Resource, Volver%>" onclick="btnVolver_Click" Visible="false" /></center>
     <uc2:UCStockEnTienda runat="server" ID="ST0" />
       </asp:Panel>


