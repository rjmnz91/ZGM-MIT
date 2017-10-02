using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class Inicio :  CLS.Cls_Session 
    {
        DLLGestionVenta.ProcesarVenta objVenta;
        bool vtaSwitch;

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["tipoPago"] = null;
            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();

            if (Session["TiendaCamper"].ToString() == "1")
            {
                //this.imgCliente9.Visible = false;
                //lblInventario.Visible= false;
            }
            else
            {
                //lblInventario.Visible = true;
                //imgCliente9.Visible = true;
            }
            string uri = HttpContext.Current.Request.Url.AbsoluteUri;

            
            bool CarrPte = AVE.Configuracion.ComprobarCarritoPendiente();

            lnkCarrito.Visible = (Session["IdCarrito"] != null);
            lnkCarrito.PostBackUrl = Constantes.Paginas.Carrito;
            if (Page.IsPostBack)
            {  
                this.lblUser.Text = lblUser.Text;
            }
            else
            {
                string script = "$(document).ready(return function ShowProgress());";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                object checkBoxChecked = Session["checkBoxChecked"];

                if (checkBoxChecked == null)
                    chkToggleButton.Checked = true;
                else
                    chkToggleButton.Checked = (bool)checkBoxChecked;



                string usu = (string)HttpContext.Current.Session[Constantes.Session.Usuario];
                if (!string.IsNullOrEmpty(usu))
                {
                    string[] ar = usu.Split(' ');
                    this.lblUser.Text = ar[1].ToUpper() + ", " + ar[0];
                }
                else
                {
                    this.lblUser.Text = "";
                }
            }

            if (uri.Contains('?'))
            {
                String[] value = uri.Split('?');
                if (!value[1].Contains("send") && !value[1].Contains("Search"))
                {
                    txtBuscar.Text = value[1];
                    if (chkToggleButton.Checked)
                    {
                        if (!pnlDatos.Visible)
                        {
                            añadirCarrito();
                            chkToggleButton.Checked = true;
                        }
                    }
                    else
                        cargar();
                }
                else
                    ;
            }
            else
                ;

            try
            {
                if (!String.IsNullOrEmpty(Session["idCarrito"].ToString()))
                {
                    int ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());

                    MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                }
                else
                {
                }
            }
            catch (Exception ee)
            {

            }
        }
        
        private void añadirCarrito()
        {
            string idUsuario = Contexto.IdEmpleado;
            string idTerminal = Contexto.IdTerminal;
            string idTienda = Contexto.IdTienda;
            string idCliente = "0";
            string strError = "";
            int idCarrito = -1;
            String script = String.Empty;


            //ACL AÑADIMOS EL CARRITO
            if (Session["IdCarrito"] == null && txtBuscar.Text != "")
            {
                DLLGestionVenta.ProcesarVenta ArtiV = new DLLGestionVenta.ProcesarVenta();
                ArtiV.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();



                idCarrito = ArtiV.AniadeCarrito(idCliente, idUsuario, idTerminal);
                Session["idCarrito"] = idCarrito;
            }
            //AÑADIMOS EL ARTICULO AL CARRITO
            if (Session["idCarrito"] != null && txtBuscar.Text != "")
            {
                idCarrito = Convert.ToInt32(Session["IdCarrito"].ToString());

                //logC.Error("Vamos a añadir el articulo al carrito");
                DLLGestionVenta.ProcesarVenta ArtiV = new DLLGestionVenta.ProcesarVenta();
                ArtiV.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                if (ArtiV.AniadeArticuloCarrito(idCarrito, this.txtBuscar.Text, idTienda, idUsuario, ref strError) == 2)
                {
                    this.txtBuscar.Text = "";
                    //logC.Error("Articulo añadido al carrito");
                    lnkCarrito.Visible = (Session["IdCarrito"] != null);
                    lnkCarrito.PostBackUrl = Constantes.Paginas.Carrito;
                    script = "alert('Articulo añadido satisfactoriamente');";
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                    this.txtBuscar.Text = "";
                }
                else
                {
                    this.txtBuscar.Text = "";
                    string error = strError.Substring(4, strError.Length - 4);
                    //  logC.Error("No se pudo añadir el artículo, al carrito. " + error);
                    script = "alert('No se pudo añadir el artículo, al carrito. " + error + "');";
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                }
                this.txtBuscar.Focus();
                Response.Redirect(Constantes.Paginas.Inicio);

            }



        }
        
        protected void lnkAniadir_Click(object sender, EventArgs e)
        {
            String script = String.Empty;
            añadirCarrito();
            try
            {
                if (!String.IsNullOrEmpty(Session["idCarrito"].ToString()))
                {
                    int ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());
                    MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                    
                    script = "alert('Articulo añadido satisfactoriamente');";
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                    Response.Redirect(Constantes.Paginas.Inicio);
                }
                else
                {
                }
            }
            catch (Exception excep)
            {

            }
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

        string usu = (string)HttpContext.Current.Session[Constantes.Session.Usuario];

        //Insertar estadística
        //Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);
            Estadisticas.InsertarBusqueda(cad1,cad2,usu,Contexto.IdTerminal);

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

        private int CheckArticulosCarrito(string idCarrito)
        {
            int numArticulos = 0;
            try
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objVenta.GetArticulosCarrito(idCarrito);
            }
            catch (Exception ex)
            {
            }
            return numArticulos;
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

        protected void lnkCarrito_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CarritoDetalle.aspx");
        }

        public void MuestraArticulosCarrito(string articulos)
        {
            HyperLink lblArt;
            int cantidad = Convert.ToInt32(articulos);
            lblArt = (HyperLink)lblNumArt;
            if (articulos == "0")
                lblArt.Visible = false;
            else
                lblArt.Visible = true;
            lblArt.Text = articulos;
        }

        protected void buscarArt()
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

            string sz = string.Format("{0}?{1}={2}&{3}={4}", Constantes.Paginas.EleccionProducto, Constantes.QueryString.FiltroArticulo,
                cad1, Constantes.QueryString.ReturnUrl, returnUrl);

            Response.Redirect(sz, true);
            return;

        }

        protected void myonoffswitch_CheckedChanged(object sender, EventArgs e)
        {
            Session["checkboxChecked"] = chkToggleButton.Checked;
        }       

        //buscar
        public string IdArticulo
        {
            get { return AVE_ArticuloDetalleObtener.SelectParameters["idArticulo"].DefaultValue; }
            set { AVE_ArticuloDetalleObtener.SelectParameters["idArticulo"].DefaultValue = value; }
        }
        public string modelo
        {
            get { return lblModelo.Text; }

        }
        public void complementario(Boolean valor)
        {
            //lblCSObservaciones.Visible = valor;
            //LtrCSObservaciones.Visible = valor;
        }
        public void cargar()
        {
            System.Data.DataTable dtDetalles = new System.Data.DataTable();

            LtrColor.Text = LtrColor.Text.ToUpper();
            LtrDescripcion.Text = LtrDescripcion.Text.ToUpper();
            LtrModelo.Text = LtrModelo.Text.ToUpper();
            LtrProeveedor.Text = LtrProeveedor.Text.ToUpper();
            LtrReferencia.Text = LtrReferencia.Text.ToUpper();

            // Obtenemos todos los detalles del producto
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {

                AVE_ArticuloDetalleObtener.SelectParameters["IdTienda"].DefaultValue = HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
                AVE_ArticuloDetalleObtener.SelectParameters["IdArticulo"].DefaultValue = txtBuscar.Text;
                dtDetalles = ((System.Data.DataView)AVE_ArticuloDetalleObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

            }

            // Si se han encontrado los detalles del producto
            if (dtDetalles.Rows.Count > 0)
            {
                lblProveedor.Text = dtDetalles.Rows[0]["Proveedor"].ToString();
                // lblIdArticulo.Text =IdArticulo.ToString();
                lblReferencia.Text = dtDetalles.Rows[0]["Referencia"].ToString();
                lblModelo.Text = dtDetalles.Rows[0]["Modelo"].ToString();
                lblDescripcion.Text = dtDetalles.Rows[0]["Descripcion"].ToString();
                lblColor.Text = dtDetalles.Rows[0]["Color"].ToString();
                lblTalla.Text = dtDetalles.Rows[0]["Talla"].ToString();
                //lblObservaciones.Text = dtDetalles.Rows[0]["Observaciones"].ToString();
                //lblCSObservaciones.Text = dtDetalles.Rows[0]["CS_OBS"].ToString();
                pnlDatos.Visible = true;
            }
            else
            {
                pnlEmpty.Visible = true;
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = txtBuscar.Text;
                int codeBar = 0;
                if (value.Length > 0)
                {
                    codeBar = int.Parse(value);
                    cargar();
                }
                else
                    Response.Redirect(Constantes.Paginas.Inicio);
            }
            catch (Exception ex)
            {
                lnkBuscar_Click(this, e);
            }
        }

        protected void BtnScanner_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio + "?sendScanReader");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //pnlEmpty.Visible = false;
            //txtBuscar.Text = "";
            Response.Redirect(Constantes.Paginas.Inicio);
        }

        protected void btnBuscaTransac_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio + "?Search");
        }

        protected void btnCancelaVta_Click(object sender, ImageClickEventArgs e)
        {

        }

        //protected void BtnScanner_Click(object sender, ImageClickEventArgs e)
        //{
        //    string url = Constantes.Paginas.Inicio + "?sendScanReader";
        //    Response.Redirect(url);  
        //}

    }
}
