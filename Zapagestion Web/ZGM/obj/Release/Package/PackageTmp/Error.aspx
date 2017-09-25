<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AVE.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h3 style="text-align:center;">ERROR!</h3>
        <div class="container">
            <br />
            <div class="alert alert-danger" role="alert" style="text-align:center;">
                <asp:TextBox id="errorMsg" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div style="text-align:center;" runat="server">
                <asp:button runat="server" ID="cmdInicio" Text="Volver a Intentar" CssClass="btn btn-info" Width="15%"/>
            </div>
        </div>
</asp:Content>