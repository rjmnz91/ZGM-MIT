using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE.controles
{
    public partial class UCNavegacionHERMES : System.Web.UI.UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string UrlHermesQueryString
        {
            get
            {
                if (Request.QueryString["UrlHermes"] != null && !string.IsNullOrEmpty(Request.QueryString["IdCarrito"]))
                    return Request.QueryString["UrlHermes"];
                else
                    return string.Empty;
            }
        }

        private string UrlHermesSession
        {

            get
            {
                if (Session["UrlHermes"] != null && !string.IsNullOrEmpty(Session["UrlHermes"].ToString()))
                    return Session["UrlHermes"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))

                    Session["UrlHermes"] = value;
                else
                    Session["UrlHermes"] = null;
            }
        }

        private int? IdCarritoSession
        {
            get
            {
                int idCarrito;

                if (Session["IdCarrito"] != null && int.TryParse(Session["IdCarrito"].ToString(), out idCarrito))
                    return idCarrito;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    Session["IdCarrito"] = value.Value;
                else
                    Session["IdCarrito"] = null;
            }
        }

        private int? IdCarritoQueryString
        {
            get
            {
                int idCarrito;

                if (Request.QueryString["IdCarrito"] != null && int.TryParse(Request.QueryString["IdCarrito"], out idCarrito))
                    return idCarrito;
                else
                    return null;
            }
        }

        private string UrlHermesAppSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString() != null)
                    return System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString();
                else
                    return string.Empty;
            }
        }

        private int CheckArticulosCarritoHermes(int? idCarrito)
        {
            DLLGestionVenta.ProcesarVenta objVenta;
            int numArticulos = 0;
            try
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objVenta.GetArticulosCarrito(Convert.ToString(idCarrito));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return numArticulos;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.anclaCarrito.ServerClick += new EventHandler(anclaCarrito_Click);
            if (Session["IdCarrito"] != null)
            {
                ImageButtonCarro.Visible = true;

            }
            else
            {
                ImageButtonCarro.Visible = false;

            }
            txtUser.Text = (string)Session[Constantes.Session.Usuario];
            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
            if (Session["TiendaCamper"].ToString() == "1") this.ImageButtonC9.Visible = false;
            else ImageButtonC9.Visible = true;

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();

        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            if (UrlHermesQueryString != null)
            {
                Response.Redirect(UrlHermesQueryString);
            }
            else
            {
                Response.Redirect(UrlHermesSession);
            }

        }
        protected void anclaCarrito_Click(object sender, EventArgs e)
        {
            if (IdCarritoQueryString == null || IdCarritoQueryString == 0)
            {
                String script = String.Empty;
                script = "alert('No hay artículos en el carrito.');";
                Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                return;
            }
            int? idArtCarrito = IdCarritoQueryString.Value;
            int ArtiCarrito = CheckArticulosCarritoHermes(idArtCarrito);
            if (ArtiCarrito == 0)
            {
                String script = String.Empty;
                script = "alert('No hay artículos en el carrito.');";
                Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                return;
            }
            if (IdCarritoQueryString != null && UrlHermesQueryString != null)
            {
                Response.Redirect("~/carritodetalleHERMES.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
            }
            else if (IdCarritoQueryString != null && UrlHermesQueryString == null)
            {
                Response.Redirect("~/carritodetalleHERMES.aspx?idCarrito=" + IdCarritoQueryString);
            }
            else
            {
                Response.Redirect(UrlHermesQueryString);
            }
        }
        protected void ImageButtonCarro_Click(object sender, ImageClickEventArgs e)
        {
            if (IdCarritoQueryString == 0)
            {
                String script = String.Empty;
                script = "alert('No hay artículos en el carrito.');";
                Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                return;
            }
            if (IdCarritoQueryString != null && UrlHermesQueryString != null)
            {
                Response.Redirect("~/carritodetalleHERMES.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
            }
            else if (IdCarritoQueryString != null && UrlHermesQueryString == null)
            {
                Response.Redirect("~/carritodetalleHERMES.aspx?idCarrito=" + IdCarritoQueryString);
            }
            else
            {
                Response.Redirect(UrlHermesQueryString);
            }
        }
        protected void ImageButtonFavoritos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(UrlHermesQueryString + "/favoritos?timestap=1408956335565");
        }
        protected void ImageButtonOutlet_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect(UrlHermesQueryString + "/productos/outlet");
            Response.Redirect(UrlHermesQueryString + "/outlet-1591.html");
        }

        protected void ImageButtonC9_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/ConsultaCliente9Hermes.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
            //Response.Redirect("~/AdmClienteNine.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(UrlHermesQueryString);
        }



    }
}