using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Resources;
using System.Data;

namespace AVE
{
    public partial class Login : CLS.Cls_Session
    {
        
        // LOG
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected void Page_Load(object sender, EventArgs e)
        {

            btnActualizar.PostBackUrl = Constantes.Paginas.Login;
            if (System.Configuration.ConfigurationManager.AppSettings["EntornoTR"] != null)
                hidStringUrl.Value = Constantes.Paginas.Configuracion;
            else
                hidStringUrl.Value = Constantes.Paginas.Login;
            CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

            hidSesionActivaTpv.Value = (objDatos.HaySesionActivaTPV() ? "1" : "0");

            if (HttpContext.Current.Session[Constantes.Session.Usuario] != null)
            {
                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session[Constantes.Session.Usuario].ToString(), true);
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            AVECustomMembershipProvider AveMembership = new AVECustomMembershipProvider();

            if (Comun.IsNumeric(txtLogin.Text) && AveMembership.ValidateUser(txtLogin.Text, txtPassword.Text))
            {
                Session["IdCarrito"]=null;
                Session["ClienteNine"] = null;
                Session["objCliente"] = null;

                HttpContext.Current.Session[Constantes.Session.Usuario] = txtLogin.Text;
                if (!Comun.CargarUsuarioSesion(txtLogin.Text, hidNombreMaquina.Value))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "Terminal", "alert('El Terminal no esta configurado en el TPV correctamente.');", true);
                    return;
                }
                else
                    ;
               
                Comun.CargaConfiguracionMIT();
                
                Comun.CheckPending();
                            
                if (Contexto.IdTerminal == string.Empty)
                    Response.Redirect(Constantes.Paginas.RegistroTerminal);

                FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, true);
                Session["TiendaCamper"] = Comun.checkTiendaCamper();
            }
            else
            {
                    string script = "alert('" + Resource.LoginCredencialesIncorrectas + "');";
                    ClientScript.RegisterStartupScript(typeof(string), Resources.Resource.Error, script, true);
            }

        }

        protected void lnkEliminarTerminal_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Configuracion);
        }
    }
}
