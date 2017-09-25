using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class RegistroTerminal :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Simplemente registramos una cookie que incluye el texto proporcionado como idTerminal
            if (Page.IsValid)
            {
                HttpCookie c = new HttpCookie(Constantes.CteCookie.IdTerminal, txtIdTerminal.Text);
                c.Expires = DateTime.MaxValue;

                Response.Cookies.Add(c);

                Response.Redirect(Constantes.Paginas.Inicio);
            }
        }
    }
}
