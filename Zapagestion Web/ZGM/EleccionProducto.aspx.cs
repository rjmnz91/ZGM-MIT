using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;

namespace AVE
{
   
    public partial class EleccionProducto :  CLS.Cls_Session 
    {
        #region VARIABLES
        private string idArticulo;

        public string IdArticulo
        {
            get { return idArticulo; }
            set { idArticulo = value; }
        }

        private struct columnasGrid
        {
            public const int ID = 3;
            public const int CodigoAlfa = 4;
            public const int EAN = 5;
            public const int Modelo = 6;
            public const int Descripcion = 7;

        }

        //Total de artículos
        private string returnUrl
        {
            get
            {
                if (ViewState["returnUrl"] != null)
                    return ViewState["returnUrl"].ToString();
                else return null;
            }
            set
            {
                ViewState["returnUrl"] = value;
            }

        }

        String StrTalla = String.Empty;

        #endregion
       
        public int compruebaSlash(string ruta){
            int valor = 0;

            int ultimo = ruta.Length;
            string slash = ruta.Substring(ultimo - 1, 1);
            if (slash == "/") valor = 1;

            return valor;
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            string appPath = "";
            appPath = HttpContext.Current.Request.ApplicationPath;
            if (!Page.IsPostBack)
            {

                //Se muestra el id del usuario, que se ha logueado.
                //this.txtUser.Text = (string)HttpContext.Current.Session[Constantes.Session.Usuario];//(string)HttpContext.Current.User.Identity.Name;
                this.Title = Resources.Resource.EleccionProducto;
                CargarPestanas();
                visibilidadComplementos(false, "C");
                visibilidadComplementos(false, "S");
               


                if (Session["IdCarrito"] != null)
                {
                    ImageButton1.Visible = true;
                    lblNumArt.Visible = true;
                    
                    if (appPath == "") appPath = "/";
                    
                    if (compruebaSlash(appPath) == 0) appPath = appPath+"/";
                    lblNumArt.NavigateUrl = appPath + "CarritoDetalle.aspx";
                    int ArtiCarrito = ValidaArticulosCarrito(Session["IdCarrito"].ToString());
                    lblNumArt.Text = ArtiCarrito.ToString();
                }
                else
                {
                    ImageButton1.Visible = false;
                    this.lblNumArt.Visible = false;
                }


            }
            else {
                if (appPath == "") appPath = "/";
                if (compruebaSlash(appPath) == 0) appPath = appPath + "/";
                lblNumArt.NavigateUrl = appPath + "CarritoDetalle.aspx";
            }
           

        }
        protected int ValidaArticulosCarrito(string idCarrito)
        {
            int numArticulos = 0;
            DLLGestionVenta.ProcesarVenta objCarrito;
            try
            {
                objCarrito = new DLLGestionVenta.ProcesarVenta();
                objCarrito.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objCarrito.GetArticulosCarrito(idCarrito);
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }
            return numArticulos;
        }
        private void CargarPestanas()
        {
            if (Request.QueryString[Constantes.QueryString.FiltroArticulo].ToString() != string.Empty && !Page.IsPostBack)
            {
                int id = 0;
                string[] where = Request.QueryString[Constantes.QueryString.FiltroArticulo].ToString().Split(',');
                if (where.Count()>=1) cargarControlEleccionProducto(EP0, where[0].Trim().ToString(), "");
                if (where.Count() >= 2)
                {
                    //cargarControlEleccionProducto(EP1, where[1].Trim().ToString(), "");
                    if (where.Count() >= 3)
                    {
                        //cargarControlEleccionProducto(EP2, where[2].Trim().ToString(), "");
                        if (where.Count() >= 4)
                        {
                            //cargarControlEleccionProducto(EP3, where[3].Trim().ToString(), "");
                            if (where.Count() >= 5)
                            {
                                //cargarControlEleccionProducto(EP4, where[4].TrimStart().ToString(), "");
                                if (where.Count() >= 6)
                                    ;//cargarControlEleccionProducto(EP5, where[5].TrimStart().ToString(), "");
                            }
                        }
                    }
                }
                mostrarPestanas();
                //vamos a controlar el mensaje
                //Comprobamos la visualizacion del Mensaje
                //¿¿quitado no se si va aqui o no ??
                //if (AVE.Configuracion.ComprobarSolicitudesPendientes())
                //{
                //    PanelAviso.Visible = true;

                //}
            }
        }
        private void CargarPestanasLocal()
        {
            //string cad1 = "";
            //string cad2 = "";


            //int i = txtBusquedaProducto.Text.IndexOf('*');
            //cad1 = txtBusquedaProducto.Text.Split('*')[0].ToString();
            //if (i > -1)
            //    cad2 = txtBusquedaProducto.Text.Split('*')[1].ToString();

            ////Insertar estadística
            //Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);

            ////Response.Redirect("StockEnTienda.aspx?Producto=" + cad1 + "&Talla=" + cad2);
            ////Dirección a la que tiene qeu reenviar EleccionProducto
            //string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);

            ////Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
            ////EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
            //string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
            //                             "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

            //Response.Redirect(urlEleccionProducto);
        }
        private void mostrarPestanas()
        {
            
            for (int i = 0; i < 6; i++)
            {
                //TabArticulos.Tabs[i].Visible = true; 
            }
            for (int i = Request.QueryString[Constantes.QueryString.FiltroArticulo].ToString().Split(',').Count(); i < 6; i++)
            {
                //TabArticulos.Tabs[i].Visible = false; 
            }
           
        }
        private void mostrarPestanasLocal()
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    Button b = this.FindControl("Articulo" + i.ToString()) as Button;
            //    b.Visible = true;
            //}
            //for (int i = txtBusquedaProducto.Text.ToString().Split(',').Count(); i < 6; i++)
            //{
            //    Button b = this.FindControl("Articulo" + i.ToString()) as Button;
            //    b.Visible = false;
            //}

        }
        protected void cargarControlEleccionProducto(controles.UCEleccionProducto EP, string where, string filtroTalla)
        {
            EP.Where = where;
            EP.Talla = "*";
            EP.returnUrl = returnUrl;
            // si viene en la url en talla
            if (!string.IsNullOrEmpty(filtroTalla))
            {
                EP.Talla = filtroTalla;
            }
            EP.cargar(this);
        }
        #region "cabezera"
        /// <summary>
        /// Volver a la página de inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVolver_Click(object sender, EventArgs e)
        {

            // MJM 26/03/2014 INICIO
            // Esto es al revés...
            //if (Request.QueryString[Constantes.QueryString.ReturnUrl] != null)
            //{
            //    Response.Redirect(Server.UrlDecode(Constantes.Paginas.Buscar));
            //}
            //else
            //{
            //    Response.Redirect(returnUrl);
            //}

            // Pero da lo mismo porque con los cambios solicitados por Piagui no se puede mantener la ultima url, 
            // en cuanto naveguen por dos o tres enlaces se llenaría el QueryString de urls enormes.
                Response.Redirect(Server.UrlDecode(Constantes.Paginas.Inicio));
        }

        /// <summary>
        /// Busqueda de productos que coincidan con el texto introducido en la correspondiente 
        /// caja de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
           CargarPestanasLocal();
        }
        #endregion 


       
        #region "C/S"
        public void Complementos(string idarticulo)
        {
            buscarComSus("C",idarticulo);
            buscarComSus("S", idarticulo);
            //TabArticulosCS.Tabs[0].HeaderText = TabArticulos.ActiveTab.HeaderText; 
           
            TabArticulosCS.Visible = true;
            TabArticulos.Visible = false; 
            TabSustituto.Visible = false;
            TabSustitutoCS.Visible = true;
            tabComplementos.Visible = true;
            //if (!tabComplementos.Tabs[0].Visible)
            //{
                //TabSustituto.Visible = true;
                //TabSustitutoCS.Visible = false;  
            //}
        }
       
        private void buscarComSus(string tipo,string idarticulo) {
            visibilidadComplementos(false,tipo);
            //ocultarArticulos(TabArticulos.Tabs.IndexOf(TabArticulos.ActiveTab));   
            if (! String.IsNullOrEmpty(idarticulo))  AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = idarticulo;
            else 
            //switch (TabArticulos.Tabs.IndexOf(TabArticulos.ActiveTab))
            //{
                //case 0:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP0.IdArticulo;
                    //break;
                //case 1:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP1.IdArticulo;
                    //break;
                //case 2:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP2.IdArticulo;
                    //break;
                //case 3:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP3.IdArticulo;
                    //break;
                //case 4:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP4.IdArticulo;
                    //break;
                //case 5:
                    //AVE_StockEnTiendaCSObtener.SelectParameters["IdArticulo"].DefaultValue = EP5.IdArticulo;
                    //break;
            //}
            //DataView dv = null;
           
            AVE_StockEnTiendaCSObtener.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
            AVE_StockEnTiendaCSObtener.SelectParameters["Tipo"].DefaultValue =tipo;
            //dv = (DataView)AVE_StockEnTiendaCSObtener.Select(new DataSourceSelectArguments());
            
            int i =1;
            string tipoBotton;
            if (tipo == "C") tipoBotton = "Complemento";
            else tipoBotton = "Sustituto"; 
            //foreach (System.Data.DataRow row in dv.Table.Rows)
            //{
            //    if (i < 6)
            //    {
            //        //control stock
            //        if (tipo == "C")
            //        {
            //            //tabComplementos.Tabs[i - 1].Visible = true;
            //            //tabComplementosCS.Tabs[i - 1].Visible = true;
            //        }
            //        else
            //        {
            //            //TabSustituto.Tabs[i - 1].Visible = true;
            //            //TabSustitutoCS.Tabs[i - 1].Visible = true;
            //        }
            //        controles.UCStockEnTienda uc = null;
                   
            //        switch (tipo)
            //        {
            //            case "C":
            //                uc = tabComplementos.Tabs[i-1].Controls[1].Controls[1] as controles.UCStockEnTienda;
            //            break;
            //            case "S":
            //                uc = TabSustituto.Tabs[i-1].Controls[1].Controls[1] as controles.UCStockEnTienda;
            //                break;
            //        }
            //        uc.IdArticulo = row["IdArticulo"].ToString();

            //        uc.CargarStock();
            //        uc.Complementario = true;
            //        uc.EP = this;
            //    }
            //    i++;
            //}
        }
        //private void EvaluarEnlaces(controles.UCEleccionProducto ep)
        //{
        //    if (ep.cs != null)
        //    {
        //        btnComplementos.Enabled = ep.cs;
        //        btnComplementos.Visible = ep.cs;
        //    }
        //}
        private void visibilidadComplementos(Boolean visible, string tipo)
        {

            if (tipo == "C")
            {

                Complemento0.Visible = visible;
                Complemento1.Visible = visible;
                Complemento2.Visible = visible;
                Complemento3.Visible = visible;
                Complemento4.Visible = visible;
                Complemento5.Visible = visible;

                TabComplemento1.Visible = visible;
                TabComplemento2.Visible = visible;
                TabComplemento3.Visible = visible;
                TabComplemento4.Visible = visible;
                TabComplemento5.Visible = visible;
                TabComplemento6.Visible = visible;
            }
            else
            {

                Sustituto0.Visible = visible;
                Sustituto1.Visible = visible;
                Sustituto2.Visible = visible;
                Sustituto3.Visible = visible;
                Sustituto4.Visible = visible;
                Sustituto5.Visible = visible;

                TabSustituto1.Visible = visible;
                TabSustituto2.Visible = visible;
                TabSustituto3.Visible = visible;
                TabSustituto4.Visible = visible;
                TabSustituto5.Visible = visible;
                TabSustituto6.Visible = visible;

            }
        }
        private void ocultarArticulos(int idArticulo)
        {


            tabArticulo0.Visible = false;
            //tabArticulo1.Visible = false;
            //tabArticulo2.Visible = false;
            //tabArticulo3.Visible = false;
            //tabArticulo4.Visible = false;
            //tabArticulo5.Visible = false;
            //TabArticulos.Tabs[idArticulo].Visible = true;
        }
        protected void TabSustitutoCS_ActiveTabChanged(object sender, EventArgs e)
        {
            //TabSustituto.ActiveTabIndex  = TabSustitutoCS.ActiveTabIndex;
            tabComplementosCS.Visible = true;  
            tabComplementos.Visible = false;
            TabSustitutoCS.Visible = false;
            TabSustituto.Visible = true;
            
        }
        protected void tabComplementosCS_ActiveTabChanged(object sender, EventArgs e)
        {
            
            //tabComplementos.ActiveTabIndex  = tabComplementosCS.ActiveTabIndex;
            tabComplementosCS.Visible = false;
            tabComplementos.Visible = true;
            TabSustitutoCS.Visible = true;
            TabSustituto.Visible = false;
            
        }
        protected void TabSustituto_ActiveTabChanged(object sender, EventArgs e)
        {
            
        
        }
        protected void tabComplementos_ActiveTabChanged(object sender, EventArgs e)
        {

            
         

        }
        protected void TabArticulosCS_ActiveTabChanged(object sender, EventArgs e)
        {
            TabArticulosCS.Visible = false;
            TabArticulos.Visible = true;
            //TabArticulosCS.ActiveTab.HeaderText = TabArticulos.ActiveTab.HeaderText;
     
        }
        protected void btnSustituto_Click(object sender, EventArgs e)
        {
            tabComplementos.Visible = false;
            TabSustitutoCS.Visible = false;
            TabSustituto.Visible = true;
            tabComplementosCS.Visible = true;
            //TabSustituto.ActiveTabIndex = TabSustitutoCS.ActiveTabIndex;
         
        }

        protected void btnComplemento_Click(object sender, EventArgs e)
        {
            tabComplementosCS.Visible = false;
            tabComplementos.Visible = true;
            TabSustitutoCS.Visible = true;
            TabSustituto.Visible = false;
            //tabComplementos.ActiveTabIndex = tabComplementosCS.ActiveTabIndex;   
           
        }
        
        #endregion

        private void IniciarArticulo(int index)
         {

             switch (index)
             {
                 case 0:
                    EP0.Volver();
                     break;
                 case 1:
                    //EP1.Volver();
                     break;
                 case 2:
                    //EP2.Volver();
                     break;
                 case 3:
                     //EP3.Volver();
                     break;
                 case 4:
                     //EP4.Volver();
                     break;
                 case 5:
                     //EP5.Volver();
                     break;

             }
        }
    
       

     
       
      
       private void CargarArticulo() {
            mostrarPestanas();
            visibilidadComplementos(false, "C");
            visibilidadComplementos(false, "S");
         }

        protected void btnCabecera_Click(object sender, EventArgs e)
        {
            //IniciarArticulo(TabArticulos.Tabs.IndexOf(TabArticulos.ActiveTab));
            mostrarPestanas();
            TabArticulos.Visible = true;
            TabArticulosCS.Visible = false;
            TabSustituto.Visible = false;
            TabSustitutoCS.Visible = false;
            tabComplementos.Visible = false;
            tabComplementosCS.Visible = false;
          

           
        }
        protected void btnCliente9_Click(object sender, EventArgs e)
        {
            Boolean vis = true ;
            if (ucC9.Visible) vis = false;
            tabs.Visible = !vis;
           ucC9.Visible = vis;
            
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CarritoDetalle.aspx");
        }

        protected void SOLICITUDES_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.SolicitudesAlmacen);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio);
        }
        
    }
}
