using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AVE;

public partial class Buscar :  AVE.CLS.Cls_Session 
{
    protected void Page_Load(object sender, EventArgs e)
    {
               
        //Registramos en javascript el control de la caja de texto para usarlo desde otras funciones jscript;
        string script = "var txtProducto = document.getElementById('" + txtProducto.ClientID + "');" +
                        "var btBuscar = document.getElementById('" + btBuscar.ClientID + "');";
        Page.ClientScript.RegisterStartupScript(typeof(string), "txtProducto", script, true);



        //string getUserHostName = HttpContext.Current.Request.UserHostName;

        //Response.Write(getUserHostName);

    }
   
    protected void btBuscar_Click(object sender, EventArgs e)
    {

        string cad1 = "";
        string cad2 = "";


        int i = txtProducto.Text.IndexOf('*');
        cad1 = txtProducto.Text.Split('*')[0].ToString();
        if (i > -1)
            cad2 = txtProducto.Text.Split('*')[1].ToString();
        
        //Insertar estadística
        Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);

        //Response.Redirect("StockEnTienda.aspx?Producto=" + cad1 + "&Talla=" + cad2);
        //Dirección a la que tiene qeu reenviar EleccionProducto
        string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);
        
        //Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
        //EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
        string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
                                     "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

        Response.Redirect(urlEleccionProducto);
    }
}
