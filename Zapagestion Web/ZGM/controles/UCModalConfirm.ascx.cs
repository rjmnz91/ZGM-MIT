using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE.controles
{
    public partial class UCModalConfirm : System.Web.UI.UserControl
    {
        public  Boolean valor;
        private string literal;

        public string Literal
        {
            get { return literal; }
            set { literal = value;
            Lbliteral.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cancelar_Click(object sender, EventArgs e)
        {
            valor = false;

        }

        protected void aceptar_Click(object sender, EventArgs e)
        {
            valor = true;
        }

      
    }
}