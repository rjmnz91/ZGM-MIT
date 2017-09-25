<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCModalConfirm.ascx.cs" Inherits="AVE.controles.UCModalConfirm" %>
<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2">
    <asp:Label ID="Lbliteral" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
    <asp:Button ID="aceptar" runat="server" Text="Si"  onclick="aceptar_Click" />
        </td>
        <td>
    <asp:Button ID="cancelar" runat="server" Text="No" onclick="cancelar_Click" />
        </td>
    </tr>
</table>



