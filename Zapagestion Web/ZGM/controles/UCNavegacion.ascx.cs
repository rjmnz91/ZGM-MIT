using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLGestionVenta.CapaDatos;

namespace AVE.controles
{
    public partial class UCNavegacion : System.Web.UI.UserControl
    {
        private static readonly log4net.ILog logC = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void inhabilitaBoton() {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "txtArticulo", "<script> $(document).ready( function() {  inhabilitaTextoEan(); }); </script>", false);
        
        }
        public int compruebaSlash(string ruta)
        {
            int valor = 0;

            int ultimo = ruta.Length;
            string slash = ruta.Substring(ultimo - 1, 1);
            if (slash == "/") valor = 1;

            return valor;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string appPath = "";
            this.txtArticulo.Focus();
            appPath = HttpContext.Current.Request.ApplicationPath;
            if (Session["IdCarrito"] != null)
            {
                ImageButton2.Visible = true;
                lblNumArt.Visible = true;
                if (appPath == "") appPath = "/";
                if (compruebaSlash(appPath) == 0) appPath = appPath + "/";
                lblNumArt.NavigateUrl = appPath + "CarritoDetalle.aspx";

            }
            else
            {
                ImageButton2.Visible = false;
                lblNumArt.Visible = false;

            }
            txtUser.Text = (string) Session[Constantes.Session.Usuario];
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "Enviafoco", "Enviafoco();", true);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect(Constantes.Paginas.Login);
        }

        protected void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            btnAniadirClick();
        }
        
        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio);
        }
        protected void btnAniadir_Click(object sender, EventArgs e)
        {
            btnAniadirClick();
        }
        protected void btnAniadirClick()
        {
            int idCarrito=0;
            string idUsuario = Contexto.IdEmpleado;
            string idTienda = Contexto.IdTienda;
            string strError = "";
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "txtArticulo", "<script> $(document).ready( function() {  inhabilitaTextoEan(); }); </script>", false);
            logC.Error("el valor del EAN es " + txtArticulo.Text);
                if (Session["idCarrito"] != null && txtArticulo.Text!="") { 
                    idCarrito = Convert.ToInt32(Session["IdCarrito"].ToString());
                    logC.Error("Vamos a añadir el articulo al carrito");
                    DLLGestionVenta.ProcesarVenta ArtiV = new DLLGestionVenta.ProcesarVenta();
                    ArtiV.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                    if (ArtiV.AniadeArticuloCarrito(idCarrito, this.txtArticulo.Text, idTienda, idUsuario,ref strError) == 2)
                    {
                        this.txtArticulo.Text = "";
                        logC.Error("Articulo añadido al carrito");
                        //ACL.Comentado al haceer el redirect, no saca el alert. Y si no hago el redirect
                        //no se actualiza la pagina
                        //String script = String.Empty;
                        ////Se aprovecha la variable, si es OK, para obtener la descripción del artículo
                        //script = "alert('Se añadió el artículo "+ strError +", al carrito.');";
                        //Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                        this.txtArticulo.Focus();
                        Response.Redirect("~/CarritoDetalle.aspx");
                    }
                    else {
                        this.txtArticulo.Text = "";
                        string error = strError.Substring(4, strError.Length - 4);
                        String script = String.Empty;
                        logC.Error("No se pudo añadir el artículo, al carrito. " + error);
                        script = "alert('No se pudo añadir el artículo, al carrito. "+ error + "');";
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                    }
                    
                }
                
            
           
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CarritoDetalle.aspx");
        }

     
    }
}