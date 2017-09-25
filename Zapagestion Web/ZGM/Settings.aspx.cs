using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uri = HttpContext.Current.Request.Url.AbsoluteUri;
            if (uri.Contains('?'))
            {
                String[] value = uri.Split('?');
                txtTerminal.Text = value[1];
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>saveIdTerminal();</script>", false);
            }
            else
                ;
        }
        protected void lnkEliminarTerminal_Click(object sender, ImageClickEventArgs e)
        {
            string terminal = hidNombreMaquina.Value;
            string redirect = Constantes.Paginas.Configuracion + "?" + terminal;
            Response.Redirect(redirect);
        }
    }
}