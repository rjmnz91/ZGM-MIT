using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AVE
{
    public partial class MasterPageHermes : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            if (!objDatos.HaySesionActivaTPV())
            {
                Response.Redirect(Constantes.Paginas.Login);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                Page.ClientTarget = "uplevel";
            }
        }
        public void MuestraArticulosCarritoHermes(string articulos)
        {
            HtmlGenericControl lblArti;
            int cantidad = Convert.ToInt32(articulos);
            lblArti = (HtmlGenericControl)ucNavegacionHERMES.FindControl("divContCarrito");
            if (articulos == "0")
                lblArti.Visible= false;

            else
                lblArti.Visible = true;
                lblArti.InnerHtml = articulos;
        }

        public void actualizarLabel(string mensaje)
        {
            divErrorPedido.Visible = true;
            lblErrorPedido.Text = mensaje;
        }
        
    
    }
}