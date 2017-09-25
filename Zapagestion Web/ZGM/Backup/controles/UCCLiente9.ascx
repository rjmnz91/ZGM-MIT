<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCLiente9.ascx.cs" Inherits="AVE.controles.UCCLiente9" %>

<div style="width: 99%;text-align:left">
<asp:label ID="error"  runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="#990000"></asp:label > 
        
</div>
<div id="cliente9">
	
    <div class="tipodato">
    	
        	Tarjeta:<br />
        	  <asp:Panel  runat="server" id="p_lbl_cliente" >
            Nombre:<br />
            Correo:<br />
            Nivel:<br />
            Teléfono: <br />
            Celular: <br />
            Aniversario: <br />
            Cumpleaños:
        </asp:Panel>
        
    </div>
    <div class="datos" >
    		<asp:TextBox runat="server" id="txt_id_tarjeta_c9" type="number" value="" 
                ontextchanged="Button1_Click"  AutoPostBack="true" /><br />
        <asp:Panel  runat="server" id="d_cliente" >
            <asp:Label runat="server" id="lbl_Nombre"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Mail"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Shoelover"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Telefono"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Celular"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Aniversario"></asp:Label ><br />
            <asp:Label  runat="server" id="lbl_Cumpleaños"></asp:Label ><br />
        </asp:Panel>
    </div>
	<div class="beneficios" " >
    <table width="250" class="tablabeneficios"  cellspacing="0" cellpadding="0" runat="server" id="tab_beneficios">
  <tr>
    <td class="tab_linea" height="30" colspan="2" align="left" bgcolor="#CCCCCC"><strong>Beneficios</strong></td>
    </tr>
  <tr>
    <td class="tab_linea" height="30" align="right"><strong>Puntos 9:</strong></td>
    <td class="tab_linea" height="30" align="right"><asp:Label id="lbl_Puntos" runat="server" /></td>
  </tr>
  <tr>
    <td height="30" align="right"><strong>Pares Acumulados:</strong></td>
    <td height="30" align="right"><asp:Label id="lbl_ParesAcumulados" runat="server" /></td>
  </tr>
  <tr>
    <td class="tab_linea" height="30" align="right"><strong>Promedio Par 9:</strong></td>
    <td class="tab_linea" height="30" align="right"><asp:Label id="lbl_PromedioPar" runat="server" /></td>
  </tr>
  <tr>
    <td height="30" align="right"><strong>Bolsas Acumuladas</strong></td>
    <td height="30" align="right"><asp:Label id="lbl_BolsasAcumuladas" runat="server" /></td>
  </tr>
  <tr>
    <td height="30" align="right"><strong>Promedio Bolsa 5:</strong></td>
    <td height="30" align="right"><asp:Label id="lbl_PromedioBolsa" runat="server" /></td>
  </tr>
</table>

    </div>
</div>