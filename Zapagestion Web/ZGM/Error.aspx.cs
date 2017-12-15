using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class Error : System.Web.UI.Page
    {

        DLLGestionVenta.ProcesarVenta objVenta;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());
                var miMaster = (MasterPage)this.Master;
                miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                ImageButton carrito = (ImageButton)this.Master.FindControl("lnkCarrito");
                HyperLink numCarrito = (HyperLink)this.Master.FindControl("lblNumArt");
                carrito.Visible = false;
                numCarrito.Visible = false;
                string uri = HttpContext.Current.Request.Url.AbsoluteUri;
                if (uri.Contains("denied"))
                {
                    //Error.aspx?CarritoDetalle.aspx?La%20tarjeta%20debe%20ser%20procesada%20como%20chip
                    String[] value = uri.Split('?');
                    cmdInicio.PostBackUrl = value[1];
                    value[1] = value[1].Replace("%20", " ");
                    value[1] = value[1].Replace("%C3%B3", "ó");
                    value[1] = value[1].Replace("%C2%A1", "í");
                    value[3] = value[3].Replace("%20", " ");
                    value[3] = value[3].Replace("%C3%B3", "ó");
                    value[3] = value[3].Replace("%C2%A1", "í");
                    errorMsg.Text = "Lo sentimos, la transaccion que ha intentado fue denegada, por favor intentelo nuevamente." + "<br/>" + "Si el problema persiste por favor consulte a su banco" + "<br/>" + value[3];
                }
                else if (uri.Contains("errorTransaccion"))
                {
                    String[] value = uri.Split('?');
                    cmdInicio.PostBackUrl = value[1];
                    value[1] = value[1].Replace("%20", " ");
                    value[1] = value[1].Replace("%C3%B3", "ó");
                    value[1] = value[1].Replace("%C2%A1", "í");
                    value[3] = value[3].Replace("%20", " ");
                    value[3] = value[3].Replace("%C3%B3", "ó");
                    value[3] = value[3].Replace("%C2%A1", "í");
                    errorMsg.Text = "Lo sentimos, hubo un error durante la transacción. Por favor intentelo nuevamente." + "<br/>" + value[3];
                }
                else if (Session["Error"] != null)
                {
                    errorMsg.Text = Session["Error"].ToString();
                    cmdInicio.PostBackUrl = Session["lastURL"].ToString();
                }
                else
                {
                    String[] value = uri.Split('?');
                    cmdInicio.PostBackUrl = value[1];
                    value[2] = value[2].Replace("%20", " ");
                    value[2] = value[2].Replace("%C3%B3", "ó");
                    value[2] = value[2].Replace("%C2%A1", "í");
                    errorMsg.Text = value[2];
                }
            }
            catch (Exception error)
            {
                errorMsg.Text = Session["Error"].ToString();
                cmdInicio.PostBackUrl = Session["lastURL"].ToString();
            }
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
                //log.Error(ex);
            }
            return numArticulos;
        }
    }
}