using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO; 

public partial class MasterPage : System.Web.UI.MasterPage
{
   
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TODO: asegurarse que la sesion es suficientemente larga en el servidor

        // actualizamos el tiempo de actualizacion

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
     
            //// //Comprobamos la visualizacion del Mensaje
            if (AVE.Configuracion.ComprobarSolicitudesPendientes())
            {
                PanelAviso.Visible = true;
            }
                
            //MembershipUserCollection users;
            //users = Membership.GetAllUsers();
            //MembershipUser u = users[HttpContext.Current.User.Identity.Name.ToString()];
           
            //u.LastActivityDate = DateTime.Now;
            //Membership.UpdateUser(u);

           
       }
         //       
       //Session["IdCarrito"] = 7;  

    }

    public void CambiarEstadoImagenCarrito(Boolean Visible)
    {
        ImageButton Img;
        Img = (ImageButton)navegacion2.FindControl("lnkCarrito");
        Img.Visible = Visible;
    }
    public void MuestraArticulosCarrito(string articulos)
    {
        HyperLink lblArt;
        int cantidad = Convert.ToInt32(articulos);
        lblArt = (HyperLink)navegacion2.FindControl("lblNumArt");
        if (articulos == "0")
        {
            lblArt.Visible = false;
            lnkCarrito.Visible = false;
        }
        else
        {
            lblArt.Visible = true;
            lnkCarrito.Visible = true;
        }
            lblArt.Text= articulos;
    }
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit", StringComparison.CurrentCultureIgnoreCase) > -1)
        {
            Page.ClientTarget = "uplevel";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
        objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
        //lblUser.Text ="Usuario: " + (string)HttpContext.Current.Session[Constantes.Session.Usuario];
        //lblUser.Text = lblUser.Text + AVE.Contexto.Usuario;
        
        if (!objDatos.HaySesionActivaTPV())
        {
            Response.Redirect(Constantes.Paginas.Login);
        }

        if (Page.IsPostBack)
            this.lblUser.Text = lblUser.Text;
        else
        {
            string usu = (string)HttpContext.Current.Session[Constantes.Session.Usuario];
            if (!string.IsNullOrEmpty(usu))
            {
                string[] ar = usu.Split(' ');
                this.lblUser.Text = ar[1].ToUpper() + ", " + ar[0];
            }
            else
            {
                this.lblUser.Text = "";
            }
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session[Constantes.Session.Usuario] = null;
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect(Constantes.Paginas.Login, true);

        return;
    }

    protected void lnkCarrito_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CarritoDetalle.aspx");
    }
}
