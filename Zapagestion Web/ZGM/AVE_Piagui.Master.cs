using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class AVE_Piagui : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //TODO: asegurarse que la sesion es suficientemente larga en el servidor
            // actualizamos el tiempo de actualizacion
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //// //Comprobamos la visualizacion del Mensaje
                PanelAviso.Visible = (AVE.Configuracion.ComprobarSolicitudesPendientes());
            }

            lnkCarrito.PostBackUrl = Constantes.Paginas.Carrito;

        }

        public void CambiarEstadoImagenCarrito(Boolean Visible)
        {
            lnkCarrito.Visible = (Session["IdCarrito"] != null);
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect(Constantes.Paginas.Login, true);
            return;
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {

            string cad1 = "";
            string cad2 = "";

            int i = txtBuscar.Text.IndexOf('*');
            cad1 = txtBuscar.Text.Split('*')[0].ToString();
            if (i > -1)
                cad2 = txtBuscar.Text.Split('*')[1].ToString();

            //Insertar estadística
            Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);

            //Response.Redirect("StockEnTienda.aspx?Producto=" + cad1 + "&Talla=" + cad2);
            //Dirección a la que tiene que reenviar EleccionProducto
            string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);

            //Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
            //EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
            string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
                                         "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

            string sz = string.Format("{0}?{1}={2}&{3}={4}", Constantes.Paginas.EleccionProducto, Constantes.QueryString.FiltroArticulo,
                cad1, Constantes.QueryString.ReturnUrl, returnUrl);

            Response.Redirect(sz, true);
            return;

        }

    }
}