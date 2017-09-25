<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNavegacion.ascx.cs"
    Inherits="AVE.controles.UCNavegacion" %>

<script type="text/javascript">
   
    function Enviafoco() {
   //     $('#txtArticuloC').focus();
    }

  
   // function inhabilitaTextoEan() {
   //     $('#ctl00_navegacion2_txtArticulo').val($('#txtArticuloC').val());
   // }
</script>

<div class="barraNavegacion" style="width: 66%; float: left">
    <asp:Button ID="btnInicio" runat="server" CssClass="botonNavegacion1" Text="<%$ Resources:Resource, Inicio%>"
        OnClick="btnInicio_Click" />
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/logoff2.gif" AlternateText="<%$ Resources:Resource, Desconectar%>"
        OnClick="ImageButton1_Click" />
    <asp:Label ID="lblUser" runat="server" Text="Usuario:"></asp:Label>
    <asp:Label ID="txtUser" runat="server" ForeColor="White"></asp:Label>
    <asp:ImageButton runat="server" ID="BtnScanner" Width="24px" 
                                ImageUrl="~/img/lector.png" Height="24px" style="margin-top: 0px" 
                                OnClientClick="LanzaScanner(); return false;" />
    <!--<input type="text" id="txtArticuloC"  class="txtAniadir" onchange="this.disabled=true;return inhabilitaTextoEan();" />-->
    <asp:TextBox ID ="txtArticulo" runat="server" CssClass="txtAniadir" 
        ontextchanged="txtArticulo_TextChanged"/>
    <asp:Button ID="btnAniadir" runat="server" CssClass="botonNavegacion1" Text="<%$ Resources:Resource, Añadir%>"
        OnClick="btnAniadir_Click" />
  
</div>
<div class="cabFondoAzul" style="float: left; width: 33%; height: 29px">
    <asp:ImageButton  ImageUrl="~/img/carro.png" ID="ImageButton2" runat="server" CssClass="div_carrito"
        OnClick="ImageButton2_Click" />
    <asp:HyperLink ID="lblNumArt" runat ="server" CssClass="div_itemcarrito" ForeColor="White" BackColor="Red" ></asp:HyperLink>
    
</div>
