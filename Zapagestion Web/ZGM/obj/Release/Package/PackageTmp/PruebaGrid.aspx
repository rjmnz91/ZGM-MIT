<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="PruebaGrid" Theme="Tema" Codebehind="PruebaGrid.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" onrowdatabound="GridView1_RowDataBound" SkinId="gridviewSkin" style="float:left;" >
        <Columns>
            <asp:TemplateField Visible="true">
                <ItemTemplate><asp:Button runat="server" CommandName="select" ID="lnkSelect" Text=">" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CodPostal") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
<asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="300"  style="float:left;">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" onrowdatabound="GridView1_RowDataBound" SkinId="gridviewSkin" >
        <Columns>
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" 
                    SortExpression="Apellidos" />
                <asp:BoundField DataField="CodPostal" HeaderText="CodPostal" 
                    SortExpression="CodPostal" />
                <asp:HyperLinkField DataTextField="Poblacion" NavigateUrl="~/PruebaGrid.aspx" />
                <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" 
                    SortExpression="Poblacion" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" 
                    SortExpression="Telefono" />
                <asp:BoundField DataField="Telefono2" HeaderText="Telefono2" 
                    SortExpression="Telefono2" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="Pais" HeaderText="Pais" SortExpression="Pais" />
                <asp:BoundField DataField="CodPostal" HeaderText="CodPostal" 
                    SortExpression="CodPostal" />
                <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" 
                    SortExpression="Poblacion" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" 
                    SortExpression="Telefono" />
                <asp:BoundField DataField="Telefono2" HeaderText="Telefono2" 
                    SortExpression="Telefono2" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="Pais" HeaderText="Pais" SortExpression="Pais" />

            </Columns>
        </asp:GridView>
        </asp:Panel>        
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MC_TDAConnectionString %>" 
        SelectCommand="AVE_ClienteObtener" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="100029" Name="id_Cliente" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>

