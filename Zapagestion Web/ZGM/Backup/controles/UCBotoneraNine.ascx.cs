using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace AVE.controles
{
    public partial class UCBotoneraNine : System.Web.UI.UserControl
    {
        private string strEntorno;
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
           // this.Page.Validate = false;
            HabilitaSel();
        }
        private void  HabilitaSel(){
            this.btnConsulta.CssClass = "botonC9";
            this.btnActivacion.CssClass = "botonC9";
            this.btnActualizacion.CssClass = "botonC9";
            this.btnCambio.CssClass = "botonC9";
            if (Session["AccionNine"] != null) {
                switch (Session["AccionNine"].ToString())
                { 
                    case "C":
                        this.btnConsulta.CssClass = "botonC9Sel";
                        break;
                    case "A":
                        this.btnActivacion.CssClass = "botonC9Sel";
                        break;
                    case "R":
                        this.btnCambio.CssClass = "botonC9Sel";
                        break;
                    case "Z":
                        this.btnActualizacion.CssClass = "botonC9Sel";
                        break;
                }
            }
        }
        
        
      

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            ValidaEntorno();
            Session["AccionNine"] = "C";
            HabilitaSel();
        
            if (strEntorno == "AVE")
            {
               
                Response.Redirect(Constantes.Paginas.ConsultaCliente9);
            }
            else Response.Redirect("~/ConsultaCliente9Hermes.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
            
        }
        protected void btnActivacion_Click(object sender, EventArgs e)
        {
            ValidaEntorno();
            Session["AccionNine"] = "A";
            HabilitaSel();

            if (strEntorno == "AVE")
            {

                Response.Redirect(Constantes.Paginas.ActivacTjt9);
            }
            else Response.Redirect("~/ActivacionTarjeta9.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));

        }
        protected void btnCambio_Click(object sender, EventArgs e)
        {
            ValidaEntorno();
            Session["AccionNine"] = "R";
            HabilitaSel();

            if (strEntorno == "AVE")
            {

                Response.Redirect(Constantes.Paginas.CambioPlastico);
            }
            else Response.Redirect("~/CambioPlastico.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));

        }
        protected void btnActualizacion_Click(object sender, EventArgs e)
        {
            ValidaEntorno();
            Session["AccionNine"] = "Z";
            HabilitaSel();

            if (strEntorno == "AVE")
            {

                Response.Redirect(Constantes.Paginas.ActualizaCliente9);
            }
            else Response.Redirect("~/ActualizarCliente9.aspx?idCarrito=" + IdCarritoQueryString + "&urlhermes=" + System.Web.HttpUtility.UrlEncode(UrlHermesQueryString));
        }
        protected void ValidaEntorno() {
            if (ConfigurationManager.AppSettings["EntornoTR"] != null) strEntorno = "TR";
            else strEntorno = "AVE";
            
        
        }

    }
}