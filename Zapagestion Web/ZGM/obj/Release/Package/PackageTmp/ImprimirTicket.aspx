<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ImprimirTicket.aspx.cs" Inherits="AVE.ImprimirTicket" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" id="scrManager"> </asp:ScriptManager>
    <asp:Button runat="server" ID="cmdImprimir1" Text="Enviar a Impresora" onclick="Button1_Click" />
    <br />
    <table style="width:99%;">
    <tr>
        <td style="width:50%; vertical-align:top;">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" ShowBackButton="False" 
                ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False" 
                ShowPageNavigationControls="False" ShowParameterPrompts="False" 
                ShowPromptAreaButton="False" ShowRefreshButton="False" ShowWaitControlCancelLink="False" 
                SizeToReportContent="True" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" ShowExportControls="False" 
                ShowPrintButton="False" ShowToolBar="False" ShowZoomControl="False">
                <LocalReport ReportPath="Ticket.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="dsCabecera" Name="DsReport" />
                        <rsweb:ReportDataSource DataSourceId="dsLineas" Name="DsLineas" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </td>
        <td style="width:50%; vertical-align:top;">
            <rsweb:ReportViewer ID="rwNota" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" ShowBackButton="False" 
                ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False" 
                ShowPageNavigationControls="False" ShowParameterPrompts="False" 
                ShowPromptAreaButton="False" ShowRefreshButton="False" ShowWaitControlCancelLink="False" 
                SizeToReportContent="True" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" ShowExportControls="False" 
                ShowPrintButton="False" ShowToolBar="False" ShowZoomControl="False">
                <LocalReport ReportPath="NotaEmpleado.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="dsCabecera" Name="DsReport" />
                        <rsweb:ReportDataSource DataSourceId="dsLineas" Name="DsLineas" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </td>
    </tr>
    </table>
    <br />
    <asp:Button runat="server" ID="Button1" Text="Enviar a Impresora" onclick="Button1_Click" />
    <asp:SqlDataSource ID="dsLineas" runat="server"> </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCabecera" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="select * from VW_AVE_TICKET_CABECERA"></asp:SqlDataSource>
</asp:Content>
