using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class Inicio :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();

            if (Session["TiendaCamper"].ToString() == "1")
            {
                this.imgCliente9.Visible = false;
                lblInventario.Visible= false;
            }
            else
            {
                lblInventario.Visible = true;
                imgCliente9.Visible = true;
            }
            //Comprobamos la visualizacion del Mensaje
            PanelAviso.Visible = AVE.Configuracion.ComprobarSolicitudesPendientes();

            //ACL.09-07-2014-Se comprueba si existe un carrito pte por tramitar y se obtiene el último
            // para esa máquina y ese usuario
            //que no haya sido cancelado, por cerrar sesion o cualquier otra incidencia.
            
            //ACL. LO COMENTO, ES UNA LOCURA CON TODOS LOS CARRITOS SIN CERRAR EXISTENTES
            bool CarrPte = AVE.Configuracion.ComprobarCarritoPendiente();

            imgSolicitudes.PostBackUrl = Constantes.Paginas.SolicitudesAlmacen;
            //imgPedidos.PostBackUrl = Constantes.Paginas.Pedidos;
            //imgCargos.PostBackUrl = Constantes.Paginas.Cargos;
            //imgTraspasoEntrada.PostBackUrl = Constantes.Paginas.PedidosEntrada;
            
            //acl comentado para hacer pruebas
            //imgCliente9.PostBackUrl = Constantes.Paginas.ConsultaCliente9;
            imgCliente9.PostBackUrl = Constantes.Paginas.AdmCliente9;

            lnkVerSolicitudes.PostBackUrl = Constantes.Paginas.SolicitudesAlmacen;

            lnkCarrito.Visible = (Session["IdCarrito"] != null);
            lnkCarrito.PostBackUrl = Constantes.Paginas.Carrito;
            //Se muestra el id del usuario, que se ha logueado.
            this.txtUser.Text = (string)HttpContext.Current.Session[Constantes.Session.Usuario];

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
        //Dirección a la que tiene qeu reenviar EleccionProducto
        string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);
        
        //Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
        //EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
        string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
                                     "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

        string sz = string.Format("{0}?{1}={2}&{3}={4}",Constantes.Paginas.EleccionProducto ,Constantes.QueryString.FiltroArticulo,
            cad1,Constantes.QueryString.ReturnUrl , returnUrl);

        Response.Redirect(sz,true);
        return;

        }

        protected void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.SolicitudesAlmacen,true);
            return;
        }

        protected void btnPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Pedidos,true);
            return;
        }

        protected void btnInventario_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inventario,true);
            return;
        }

        protected void btnCargos_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Cargos,true);
            return;
        }

        protected void btnPedidosEntrada_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.PedidosEntrada,true);
            return;
        }

        protected void btnCargosEntrada_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.CargosEntrada,true);
            return;
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Constantes.Session.Usuario] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect(Constantes.Paginas.Login,true);
            
            return;
        }

    }
}
