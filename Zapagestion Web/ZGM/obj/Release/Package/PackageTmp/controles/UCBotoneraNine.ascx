<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCBotoneraNine.ascx.cs" Inherits="AVE.controles.UCBotoneraNine" %>

<div id="ButC9">
	<div class="tit_cliente9">
      Cliente 9</div>
    
<br />

<div id="divBotonera" class="BotoneraC9">
                    <asp:Button ID="btnConsulta" runat="server" CssClass="botonC9" 
                        Width="200px" Height="40" onclick="btnConsulta_Click" Text="Consulta Beneficios" />
                    <asp:Button ID="btnActivacion" runat="server" CssClass="botonC9" 
                        Width="200px" Height="40" onclick="btnActivacion_Click" Text="Activación de Tarjeta" />
                     <asp:Button ID="btnCambio" runat="server" CssClass="botonC9" 
                        Width="200px" Height="40" onclick="btnCambio_Click" Text="Cambio de Plástico"/>
                    <asp:Button ID="btnActualizacion" runat="server" CssClass="botonC9" 
                        Width="200px" Height="40" onclick="btnActualizacion_Click" Text="Actualización de cliente" />
     </div>
     </div>
