<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCLiente9.ascx.cs" Inherits="AVE.controles.UCCLiente9" %>

<div style="width: 99%;text-align:left">
<asp:label ID="error"  runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="#990000"></asp:label > 
        
</div>
<div id="cliente9">
	<div class="tipodato">
	<table>
		<tr>
			<td>
				Tarjeta:
			</td>
			<td>
				<asp:TextBox runat="server" id="txt_id_tarjeta_c9" value="" 
                ontextchanged="Button1_Click"  AutoPostBack="true" CssClass="form-control" />
			</td>
		</tr>
	</table>
        <table>
            <tr>
                <td>
                    <asp:Panel runat="server" id="p_lbl_cliente">
		<table>
            <tr>
                <td>Nombre:<br /></td>
				<td>
                    <asp:Label runat="server" id="lbl_Nombre"></asp:Label ><br />
                </td>
            </tr>
            <tr style="background-color:paleturquoise;">
                <td>Correo:<br /></td>
				<td>
                    <asp:Label  runat="server" id="lbl_Mail"></asp:Label ><br />
                </td>
            </tr>
            <tr>
                <td>Nivel:<br /></td>
				<td>
                    <asp:Label  runat="server" id="lbl_Shoelover"></asp:Label ><br />
				</td>
            </tr>
            <tr style="background-color:paleturquoise;">
                <td>Teléfono:<br /></td>
				<td>
                    <asp:Label  runat="server" id="lbl_Telefono"></asp:Label ><br />
                </td>
            </tr>
            <tr>
                <td>Celular:<br /></td>
				<td>
					<asp:Label  runat="server" id="lbl_Celular"></asp:Label ><br />
				</td>
            </tr>
            <tr style="background-color:paleturquoise;">
                <td>Aniversario:<br /></td>
				<td>
					<asp:Label  runat="server" id="lbl_Aniversario"></asp:Label ><br />
				</td>
            </tr>
            <tr>
                <td>Cumpleaños:</td>
				<td>
					<asp:Label  runat="server" id="lbl_Cumpleaños"></asp:Label ><br />
				</td>
            </tr>
        </table>
	</asp:Panel>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <div class="beneficios" style="width=35%">
    <table cellspacing="0" cellpadding="0" runat="server" id="tab_beneficios">
  <tr>
    <td colspan="2" align="center" ><strong>Beneficios</strong></td>
    </tr>
  <tr style="background-color:paleturquoise;">
    <td ><strong>Puntos 9:</strong></td>
    <td ><asp:Label id="lbl_Puntos" runat="server" /></td>
  </tr>
  <tr>
    <td ><strong>Pares Acumulados:</strong></td>
    <td ><asp:Label id="lbl_ParesAcumulados" runat="server" /></td>
  </tr>
  <tr style="background-color:paleturquoise;">
    <td ><strong>Promedio Par 9:</strong></td>
    <td ><asp:Label id="lbl_PromedioPar" runat="server" /></td>
  </tr>
  <tr>
    <td ><strong>Bolsas Acumuladas</strong></td>
    <td ><asp:Label id="lbl_BolsasAcumuladas" runat="server" /></td>
  </tr>
  <tr style="background-color:paleturquoise;">
    <td ><strong>Promedio Bolsa 5:</strong></td>
    <td ><asp:Label id="lbl_PromedioBolsa" runat="server" /></td>
  </tr>
</table>

    </div>
                </td>
            </tr>
        </table>
	
        
	</div>
</div>