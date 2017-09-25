<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNavegacionHERMES.ascx.cs"
    Inherits="AVE.controles.UCNavegacionHERMES" %>
    <script type="text/javascript">
        
        function lanzaCarrito() {
            debugger;
            document.getElementById("ctl00_ucNavegacionHERMES_ImageButtonCarro").click(); 
        }
    </script>
    <div class="ucNavegacionHERMESIzquierda"> 
    <asp:ImageButton runat="server"  ID="btnInicio" ImageUrl="../img/HERMES/nineWest.jpg" BackColor="White"  width="220" height="55" ToolTip= "<%$ Resources:Resource, Inicio%>"  OnClick="btnInicio_Click" />  
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/logoff2.gif" AlternateText="<%$ Resources:Resource, Desconectar%>"
            OnClick="ImageButton1_Click" />
        <asp:Label ID="lblUser" runat="server" Text="Usuario:"></asp:Label>
        <asp:Label ID="txtUser" runat="server" ForeColor="White"></asp:Label>
    </div>
    <div class="ucNavegacionHERMESDerecha">        
        <asp:ImageButton ImageUrl="~/img/HERMES/btBuscar.png" ID="ImageButtonBuscar" AlternateText="Buscador" runat="server" width="50" height="50"
        OnClick="ImageButtonBuscar_Click" />
        <asp:ImageButton ImageUrl="~/img/HERMES/btC9.png" ID="ImageButtonC9" AlternateText="Cliente 9" runat="server" width="50" height="50"
        OnClick="ImageButtonC9_Click" />
        <a runat="server" id="anclaCarrito"  href="#"   class ="carrito" ><div  runat="server"  id="divContCarrito" class ="contador-carro">0</div></a>
        <!--<asp:ImageButton ImageUrl="~/img/HERMES/btCarro.png" Visible = "false" ID="ImageButtonCarro" AlternateText="Carrito" runat="server" width="50" height="50"
         OnClick="ImageButtonCarro_Click"  />-->
        
        <asp:ImageButton ImageUrl="~/img/HERMES/btFavoritos.png" ID="ImageButtonFavoritos" AlternateText="Favoritos"  runat="server" width="50" height="50"
        OnClick="ImageButtonFavoritos_Click" /> 
        <asp:ImageButton ImageUrl="~/img/HERMES/btOutlet.png" ID="ImageButtonOutlet" AlternateText="Outlet"  runat="server" width="50" height="50"
        OnClick="ImageButtonOutlet_Click" />
    </div>

