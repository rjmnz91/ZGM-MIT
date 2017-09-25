using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class Configuraciones : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            SDSConfiguraciones.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            SDSConfiguraciones.UpdateParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            SDSConfiguraciones.UpdateParameters["Usuario"].DefaultValue = Contexto.Usuario;

        }
    }
}
