<%@ Page Title="Buscar" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Theme="Tema" Inherits="Buscar" Codebehind="Buscar.aspx.cs" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<%--<%@ Register src="controles/UCNavegacion.ascx" tagname="UCNavegacion" tagprefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="js/JScript.js"/></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
        window.onload = function () { txtProducto.focus(); }
        //        document.writeln(screen.height)
        var obj = document.getElementById('Contenedor');
        var obj1 = document.getElementById('ctl00_ContentPlaceHolder1_Panel1');
        obj.style.height = (screen.height) + "px";

        if (obj1 != null) {
        obj1.style.height = (screen.height) + "px";
        } 

        document.onkeypress = function (e) {         //Notar que BB solo captura los keypress si el foco está en una caja de texto
            if (e != null && e.keyCode == 13) {
                btBuscar.click();
            }
        }

        function concatenar(cadena) {
            txtProducto.value = txtProducto.value + cadena;
            txtProducto.focus();
        }

        function quitarUno() {
            var valor = txtProducto.value.toString();
            if (valor.length > 0) {
                valor = valor.substring(0, valor.length - 1);
            }
            txtProducto.value = valor;
            txtProducto.focus();
        }

        function limpiar() {
            txtProducto.value = '';
            txtProducto.focus();
        }

        function validar() {
            if (txtProducto.value == '') {
                txtProducto.focus();
                return false;
            }
            else
                return true;
        }
                 
    </script>
   

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btBuscar" style="height:100%" >
        <%--<uc1:UCNavegacion ID="UCNavegacion1" runat="server" />--%>
        
        <asp:TextBox ID="txtProducto" runat="server" CssClass="boton" style="width:90%;height:30px;"></asp:TextBox>
        <asp:Button ID="btBuscar" runat="server"  CssClass="boton" style="width:30%;height:12%;" 
            Text="<%$ Resources:Resource, Buscar %> " OnClick="btBuscar_Click" OnClientClick="return validar();" 
            TabIndex="1"  />
        <br />
        
        <Button ID="b1" onclick="javascript:concatenar('1');" tabindex="4" class="boton" style="width:30%;height:12%;" >&nbsp;1&nbsp;</button>
        <Button ID="b2" onclick="javascript:concatenar('2');" tabindex="5" class="boton" style="width:30%;height:12%;" >&nbsp;2&nbsp;</button>
        <Button ID="b3" onclick="javascript:concatenar('3');" tabindex="6" class="boton" style="width:30%;height:12%;" >&nbsp;3&nbsp;</button>
        <br />
        <Button ID="b4" onclick="javascript:concatenar('4');" tabindex="7" class="boton" style="width:30%;height:12%;" >&nbsp;4&nbsp;</button>
        <Button ID="b5" onclick="javascript:concatenar('5');" tabindex="8" class="boton" style="width:30%;height:12%;" >&nbsp;5&nbsp;</button>
        <Button ID="b6" onclick="javascript:concatenar('6');" tabindex="9" class="boton" style="width:30%;height:12%;" >&nbsp;6&nbsp;</button>
        <br />
        <Button ID="b7" onclick="javascript:concatenar('7');" tabindex="10" class="boton" style="width:30%;height:12%;" >&nbsp;7&nbsp;</button>
        <Button ID="b8" onclick="javascript:concatenar('8');" tabindex="11" class="boton" style="width:30%;height:12%;" >&nbsp;8&nbsp;</button>
        <Button ID="b9" onclick="javascript:concatenar('9');" tabindex="12" class="boton" style="width:30%;height:12%;" >&nbsp;9&nbsp;</button>
        <br />
        <Button ID="b0" onclick="javascript:concatenar('0');" tabindex="13" class="boton" style="width:30%;height:12%;" >&nbsp;0&nbsp;</button>
        <Button ID="ba" onclick="javascript:concatenar('*');" tabindex="14" class="boton" style="width:30%;height:12%;" >&nbsp;*&nbsp;</button>
        <Button ID="bc" onclick="javascript:limpiar();" tabindex="15" class="boton" style="width:30%;height:12%;" >&nbsp;C&nbsp;</button>

    </asp:Panel>
</asp:Content>
