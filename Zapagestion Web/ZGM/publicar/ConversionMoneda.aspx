<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConversionMoneda.aspx.cs" Inherits="AVE.ConversionMoneda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panel1" runat="server" onclick="javascript:history.back();">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataSourceID="SDSConversion" ShowHeader="false" GridLines="None" CssClass="GridConversion" CellPadding="5">
            <Columns>
                <asp:BoundField DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Moneda" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SDSConversion" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
            SelectCommand="AVE_ConvertirImporte" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="Importe" Type="Decimal" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
</asp:Content>
