<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCEleccionProducto.ascx.cs" Inherits="AVE.controles.UCEleccionProducto" %>
<%@ Register Src="~/controles/UCStockEnTienda.ascx" TagName="UCStockEnTienda" TagPrefix="uc2" %>

<asp:Panel ID="Resultado" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading"><asp:Label ID="lblResult" runat="server"></asp:Label></div>
    <div class="panel-body">
        <asp:GridView runat="server"  ID="grdListaProductos" AutoGenerateColumns="False" DataKeyNames="IdArticulo" SkinID="gridviewSkin" EmptyDataText="<%$ Resources:Resource, NoProductosFiltro %>" DataSourceID="AVE_ArticuloBuscarLike" OnSelectedIndexChanged="grdListaProductos_SelectedIndexChanged" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="GridItem" Visible="true">
                    <ItemTemplate>
                        <asp:Button ID="lnkSelect" runat="server" CommandName="select" CssClass="btn btn-primary" Font-Size="X-Small" Style="width: 50px;" Text="✚" />
                    </ItemTemplate>
                    <ItemStyle CssClass="GridItem" />
                </asp:TemplateField>
                <asp:BoundField DataField="StockTienda" HeaderStyle-Width="75px" HeaderText="<%$ Resources:Resource, Stock%>" ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle Width="75px" />
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="StockOtras" HeaderStyle-Width="65px" HeaderText="<%$ Resources:Resource, StockOtras%>" ItemStyle-CssClass="GridItem" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="65px" />
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="IdArticulo" HeaderText="<%$ Resources:Resource, Id%>" ItemStyle-CssClass="GridItem" Visible="false">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="CodigoAlfa" HeaderText="<%$ Resources:Resource, CodigoAlfa%>" ItemStyle-CssClass="GridItem" Visible="false">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="EAN" HeaderText="<%$ Resources:Resource, Ean%>" ItemStyle-CssClass="GridItem" Visible="false">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="Modelo" HeaderText="<%$ Resources:Resource, Modelo%>" ItemStyle-CssClass="GridItem">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="<%$ Resources:Resource, Descripcion%>" ItemStyle-CssClass="GridItem">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
                <asp:BoundField DataField="Seccion" HeaderText="Sección" ItemStyle-CssClass="GridItem">
                <ItemStyle CssClass="GridItem" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE"  ForeColor="Black"/>
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    </div>
    <%--ORIGEN DE DATOS PARA LAS BÚSQUEDA DE PRODUCTOS--%>
    <asp:SqlDataSource ID="AVE_ArticuloBuscarLike" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloBuscarLike"
        SelectCommandType="StoredProcedure" DataSourceMode="DataSet"
        OnSelected="AVE_ArticuloBuscarLike_Selected">
        <SelectParameters>
            <asp:Parameter Name="Filtro" Type="String" />
            <asp:Parameter Name="StrTalla" Type="String" />
            <asp:Parameter Name="IdCabeceroDetalle" Type="int32" Direction="Output" />
            <asp:Parameter Name="IdArticulo" Type="int32" Direction="Output" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Panel ID="PnlRegistrarBusqueda" runat="server" CssClass="panel panel-info">
        <div class="panel-heading"><label>Registrar Busqueda</label></div>
        <div>
            <table style="">
            <tr>
                <td class="style2">
                    <asp:Label ID="LtrTipoArticulo" runat="server" Text="<%$ Resources:Resource, TipoArticulo%>" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSeccion" runat="server" DataSourceID="SqldataSeccion" DataTextField="Seccion" DataValueField="idSeccion" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LstMarca" runat="server" Text="Marca" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMarca" runat="server" DataSourceID="SqlDataMarcas" DataTextField="NombreComercial" DataValueField="idproveedor" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LblCorte" runat="server" Text="Corte" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCorte" runat="server" DataSourceID="SqlDataCorte" DataTextField="Corte" DataValueField="idCorte" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Text="Material" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMaterial" runat="server" DataSourceID="SqlDataMaterial" DataTextField="Material" DataValueField="IdMaterial" CssClass="form-control"  />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LtrColor" runat="server" Text="Color" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlColor" runat="server" DataSourceID="SqlDataColor" DataTextField="Color" DataValueField="idColor" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LtrTalla" runat="server" Text="Talla" CssClass="negrita"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTalla" runat="server" DataSourceID="SqlDataTallas" DataTextField="Nombre_Talla" DataValueField="Nombre_Talla" CssClass="form-control"/>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LtrObservaciones" runat="server" Text="Comentario" CssClass="negrita"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtComentario" runat="server" Height="58px" TextMode="MultiLine" CssClass="form-control" MaxLength="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style2"><asp:Button ID="Button1" runat="server" Text="Registrar Petición" CssClass="btn btn-info" OnClick="Button1_Click" Height="44px" /></td>
                <td class="style2"></td>
                <td class="style2"><asp:Button ID="Button2" runat="server" Text="Volver a Inicio" CssClass="btn btn-default" OnClick="Button2_Click" Height="44px" /></td>
                    
            </tr>
        </table>
            <asp:SqlDataSource ID="SqldataSeccion" runat="server"
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
            SelectCommand="AVE_Secciones" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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
            SelectCommand="AVE_TallasZG" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataMarcas" runat="server"
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
            SelectCommand="AVE_MARCASPROVEEDOR" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </div>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="Stock" runat="server">    
    <uc2:UCStockEnTienda runat="server" ID="ST0" />
    <center><asp:Button ID="btnVolver" runat="server" TabIndex="1" CssClass="bnt btn-info"  Text="<%$ Resources:Resource, Volver%>" onclick="btnVolver_Click" Visible="false" /></center>
</asp:Panel>