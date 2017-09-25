using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace AVE
{
    public partial class MasterPageNine : System.Web.UI.MasterPage
    {
        string strEntorno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
             ValidaEntorno();
             if (strEntorno == "AVE")
             {
                 this.Literal1.Text = "<link href='css/estilos.css' rel='stylesheet' type='text/css' />";
                 this.ucNavegacionHERMES.Visible = false;
             }
             else 
             {
                 this.Literal1.Text = " <link href='css/estilosHERMES.css' rel='stylesheet' type='text/css' />";
                 this.navegacion2.Visible = false;
             }
        }

        public void CambiarEstadoImagenCarrito(Boolean Visible)
        {
            ImageButton Img;
            Img = (ImageButton)navegacion2.FindControl("ImageButton2");
            Img.Visible = Visible;
        }
        public void AniadeRefImagenCarrito(string Url){

            HyperLink lblArt;
            lblArt = (HyperLink)navegacion2.FindControl("lblNumArt");
            lblArt.NavigateUrl=Url;
         
        }
        public void MuestraArticulosCarrito(string articulos)
        {
            HyperLink lblArt;
            int cantidad = Convert.ToInt32(articulos);
            lblArt = (HyperLink)navegacion2.FindControl("lblNumArt");
            if (articulos == "0")
                lblArt.Visible = false;

            else
                lblArt.Visible = true;
            lblArt.Text = articulos;
        }
        public void MuestraArticulosCarritoHermes(string articulos)
        {
            HtmlGenericControl lblArti;
            int cantidad = Convert.ToInt32(articulos);
            lblArti = (HtmlGenericControl)ucNavegacionHERMES.FindControl("divContCarrito");
            if (articulos == "0")
                lblArti.Visible = false;

            else
                lblArti.Visible = true;
            lblArti.InnerHtml = articulos;
        }

        public void AniadeLinkCarrito(string urlPath)
        { 
        
        
        
        }
        protected void ValidaEntorno()
        {
            if (ConfigurationManager.AppSettings["EntornoTR"] != null) strEntorno = "TR";
            else strEntorno = "AVE";


        }
         
    }
}