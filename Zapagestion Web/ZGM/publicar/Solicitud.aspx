<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Solicitud.aspx.cs" Inherits="AVE.Solicitud" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="js/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        window.onload = function () {
           txtUnidades.focus();
        }      //El control se registra desde el código de servidor

       function incrementar(cantidad) {



            if (txtStock.innerHTML == "") { txtUnidades.value = 0; }

            if (cantidad > 0) 
            {

                if ((parseInt(txtUnidades.value)) >= parseInt(txtStock.innerHTML)) 
                {

                    alert("No hay Stock disponible");
                    return false;  

                }

              }

             txtUnidades.value = parseInt(txtUnidades.value) + parseInt(cantidad);

            
            if (parseInt(txtUnidades.value)<1)
            {
                txtUnidades.value = 1;
            }
            

            return false;
        }
    </script>
    
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPedir">
        <br />        
        <table>
            <tr>
                <td><asp:Label ID="LtrTienda" runat="server" Text="<%$ Resources:Resource, Tienda%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblTienda" runat="server" Text=""></asp:Label></td>
            </tr>
            <%--<tr>
                <td><asp:Label ID="LtrProveedor" runat="server" Text="<%$ Resources:Resource, Proveedor%>" CssClass="negrita" Visible="false"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblProveedor" runat="server" Text="" Visible="false"></asp:Label></td>
            </tr>--%>
            <%--<tr>
                <td><asp:Label ID="LtrIdArticulo" runat="server" Text="<%$ Resources:Resource, IdArticulo%>" CssClass="negrita" Visible="false" ></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblIdArticulo" runat="server" Text="" Visible="false"></asp:Label></td>
            </tr>--%>
            <tr>
                <td><asp:Label ID="LtrReferencia" runat="server" Text="<%$ Resources:Resource, Referencia%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblReferencia" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="LtrModelo" runat="server" Text="<%$ Resources:Resource, Modelo%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblModelo" runat="server" Text=""></asp:Label></td>
            </tr>
             <tr>
                <td><asp:Label ID="LtrMarca" runat="server" Text="<%$ Resources:Resource, Marca%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="LblMarca" runat="server" Text=""></asp:Label></td>
            </tr>
           <%-- <tr>
                <td><asp:Label ID="LtrDescripcion" runat="server" Text="<%$ Resources:Resource, Descripcion%>" CssClass="negrita" Visible="false"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblDescripcion" runat="server" Text="" Visible="false" ></asp:Label></td>
            </tr>--%>
            <tr>
                <td><asp:Label ID="LtrColor" runat="server" Text="<%$ Resources:Resource, Color%>" CssClass="negrita"  Visible="false"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblColor" runat="server" Text="" Visible="false"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="LtrPrecio" runat="server" Text="<%$ Resources:Resource, Precio%>" CssClass="negrita"></asp:Label></td>
                <td><asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>&nbsp;$</td>
                <td><asp:Button ID="btnPrecio" runat="server" Text="+" onclick="btnPrecio_Click" CssClass="boton2" /></td>
            </tr>
             <tr>
                <td><asp:Label ID="LtrTalla" runat="server" Text="<%$ Resources:Resource, Talla%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="lblTalla" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="LtrSt" runat="server" Text="<%$ Resources:Resource, StockD%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2"><asp:Label ID="LblStock" runat="server" Text=""></asp:Label></td>
            </tr>
           <tr>
                <td><asp:Label ID="LtrUnidades" runat="server" Text="<%$ Resources:Resource, Unidades%>" CssClass="negrita"></asp:Label></td>
                <td colspan="2">
                    <asp:TextBox ID="txtUnidades" runat="server" MaxLength="3" Columns="2" style="height:30px;" CssClass="textRight boton" Text="1"></asp:TextBox>
                    <button id="bmenos"type="button" onclick="javascript:incrementar(-1);" class="boton" style="width:30px;" >&nbsp;-&nbsp;</button>
                    <button id="bmas" type="button" onclick="javascript:incrementar(1);"  class="boton" style="width:30px;">&nbsp;+&nbsp;</button>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" MaximumValue="999" MinimumValue="1" ControlToValidate="txtUnidades" ErrorMessage="<%$ Resources:Resource, CantidadNoValida%>"></asp:RangeValidator>
               </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:Button ID="btnPedir" runat="server" Text="<%$ Resources:Resource, RealizarPedido%>" CssClass="boton" onclick="btnPedir_Click" />
                </td>
            </tr>    
        </table>
        <br />
           
        <asp:SqlDataSource ID="SDSDetalleArticulo" runat="server" ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>"
            SelectCommand="dbo.AVE_ArticuloDetalleObtener" SelectCommandType="StoredProcedure" DataSourceMode="DataSet">
            <SelectParameters>
                <asp:Parameter Name="IdArticulo" Type="int32" />
                <asp:Parameter Name="IdTienda" Type="string" />
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
    </asp:Panel>    
</asp:Content>
