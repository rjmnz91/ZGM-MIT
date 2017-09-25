using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AVE
{
    public partial class AdmClienteNine : System.Web.UI.Page
    {
                private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DLLGestionVenta.ProcesarVenta objVenta;

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
        private int CheckArticulosCarritoHermes(int? idCarrito)
        {
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
            Session["AccionNine"] = null;
            if (IdCarritoQueryString != null)
            {
                int? idCarritoH = IdCarritoQueryString.Value;
                if (idCarritoH != null)
                {
                    int ArtiCarrito = CheckArticulosCarritoHermes(idCarritoH);
                    if (ArtiCarrito > 0)
                    {

                        var miMaster = (MasterPageHermes)this.Master;
                        miMaster.MuestraArticulosCarritoHermes(Convert.ToString(ArtiCarrito));
                    }
                    
                }
            }
        }
    }
        
}
