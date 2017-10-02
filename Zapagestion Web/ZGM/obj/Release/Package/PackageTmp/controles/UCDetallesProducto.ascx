<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDetallesProducto.ascx.cs" Inherits="AVE.controles.UCDetallesProducto" %>
<%@ Register Src="~/controles/UCCLiente9.ascx" TagPrefix="uc1" TagName="UCCLiente9" %>

<%--<uc1:UCCLiente9 runat="server" ID="UCCLiente9" />--%>

<table width="100%">
        <tr>
            <td>
                <asp:Label Style="text-transform:uppercase" ID="LtrProeveedor" runat="server" Text="<%$ Resources:Resource, Proveedor%>" CssClass="negrita"></asp:Label>
            </td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblProveedor" runat="server"></asp:Label></td>
        </tr>
      <%--  <tr>
            <td><asp:Label Style="text-transform:uppercase" ID="LtrIdArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblIdArticulo" runat="server"></asp:Label></td> 
        </tr>--%>
        <tr style="background-color:paleturquoise;">
            <td ><asp:Label Style="text-transform:uppercase" ID="LtrReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblReferencia" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label Style="text-transform:uppercase" ID="LtrDescripcion" runat="server" Text="<%$ Resources:Resource, Descripcion%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblDescripcion" runat="server"></asp:Label></td>
        </tr>
        <tr style="background-color:paleturquoise;">
            <td><asp:Label Style="text-transform:uppercase" ID="LtrColor" runat="server" Text="<%$ Resources:Resource, Color%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblColor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label Style="text-transform:uppercase" ID="LtrObservaciones" runat="server" Text="<%$ Resources:Resource, Observaciones%>" CssClass="negrita"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblObservaciones" runat="server"></asp:Label></td>
        </tr>
        <tr style="background-color:paleturquoise;">
            <td><asp:Label Style="text-transform:uppercase" ID="LtrCSObservaciones" runat="server" Text="<%$ Resources:Resource, ObservacionesCS%>" CssClass="negrita" Visible="false"></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblCSObservaciones" runat="server" Visible="false"></asp:Label></td>
        </tr>
          <tr style="visibility:collapse;" >
            <td><asp:Label Style="text-transform:uppercase" ID="LtrModelo" runat="server" 
                    Text="<%$ Resources:Resource, Modelo %>" CssClass="negrita" Visible="False" ></asp:Label></td>
            <td><asp:Label Style="text-transform:uppercase" ID="lblModelo" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>

    <asp:SqlDataSource ID="AVE_ArticuloDetalleObtener" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
        SelectCommand="dbo.AVE_ArticuloDetalleObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:Parameter Name="IdArticulo" Type="int32" />
            <asp:Parameter Name="IdTienda" Type="string" />
        </SelectParameters>
    </asp:SqlDataSource>
        
