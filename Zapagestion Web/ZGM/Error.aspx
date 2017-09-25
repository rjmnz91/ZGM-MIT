<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AVE.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h3 style="text-align:center;">¡Atención!</h3>
        <div class="container">
            <br />
            <div class="alert alert-warning" role="alert" style="text-align:center;">
                <img src="img/warning.png" width="175px" height="175px" />
                <br />
                <br />
                <h4>
                    <asp:Label id="errorMsg" runat="server"></asp:Label>
                </h4>
            </div>
            <div style="text-align:center;" runat="server">
                <asp:button runat="server" ID="cmdInicio" Text="Volver a Intentar" CssClass="btn btn-info" Width="50%"/>
            </div>
        </div>
</asp:Content>