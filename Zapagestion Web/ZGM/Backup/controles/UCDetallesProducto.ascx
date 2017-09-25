<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDetallesProducto.ascx.cs" Inherits="AVE.controles.UCDetallesProducto" %>
 
    <table width="100%">
        <tr>
            <td><asp:Label ID="LtrProeveedor" runat="server" Text="<%$ Resources:Resource, Proveedor%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblProveedor" runat="server"></asp:Label></td>
        </tr>
      <%--  <tr>
            <td><asp:Label ID="LtrIdArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblIdArticulo" runat="server"></asp:Label></td> 
        </tr>--%>
        <tr>
            <td ><asp:Label ID="LtrReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblReferencia" runat="server"></asp:Label></td>
        </tr>
      
        <tr>
            <td><asp:Label ID="LtrDescripcion" runat="server" Text="<%$ Resources:Resource, Descripcion%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblDescripcion" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="LtrColor" runat="server" Text="<%$ Resources:Resource, Color%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblColor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="LtrObservaciones" runat="server" Text="<%$ Resources:Resource, Observaciones%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label ID="lblObservaciones" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="LtrCSObservaciones" runat="server" Text="<%$ Resources:Resource, ObservacionesCS%>" CssClass="negrita" Visible="false"></asp:Label></td>
            <td><asp:Label ID="lblCSObservaciones" runat="server" Visible="false"></asp:Label></td>
        </tr>
          <tr style="visibility:collapse;" >
            <td><asp:Label ID="LtrModelo" runat="server" 
                    Text="<%$ Resources:Resource, Modelo %>" CssClass="negrita" Visible="False" ></asp:Label></td>
            <td><asp:Label ID="lblModelo" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>

    <asp:SqlDataSource ID="AVE_ArticuloDetalleObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloDetalleObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
            <asp:Parameter Name="IdTienda" Type="string" />
        </SelectParameters>
    </asp:SqlDataSource>
        
