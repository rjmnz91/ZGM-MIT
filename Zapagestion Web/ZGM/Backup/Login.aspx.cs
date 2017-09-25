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
            {
                hidStringUrl.Value = Constantes.Paginas.Login + "?" + Request.QueryString.ToString();
            }
            else
            {
                hidStringUrl.Value = Constantes.Paginas.Login;
            }

            CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

            hidSesionActivaTpv.Value = (objDatos.HaySesionActivaTPV() ? "1" : "0");
            //ACL.15/10/2014. Me comeno Mario que si ya hay abierta una sesion con un usuario identificado
            //si se abre otra pestaña en el navegador que no vuelva a pedir el usuario.
            if (HttpContext.Current.Session[Constantes.Session.Usuario] != null) {
                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session[Constantes.Session.Usuario].ToString(), true);
            }

        }

       
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            AVECustomMembershipProvider AveMembership = new AVECustomMembershipProvider();

            //if (Membership.ValidateUser(txtLogin.Text,txtPassword.Text))
            //{ 
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
                {
                    //HttpCookie terminalCookie = Request.Cookies["Terminal"]; 
                    //Session[Constantes.CteCookie.IdTerminal] = terminalCookie.Value;
                }
               
                Comun.CargaConfiguracionMIT();
                
                //DML 290414 Metodo que realizar los checkpending de todas las operaciones pendientes por terminal
                Comun.CheckPending();
                // Session["IdCarrito"] = 135; 
                            
                //Comun.CargarUsuarioSesion((Guid)Membership.GetUser(txtLogin.Text).ProviderUserKey);
                //Comun.CargaConfiguracionMIT();

              //  Comprobamos que tiene un IdTerminal válido; si no le invitamos a registrarlo
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

        protected void imgES_Click(object sender, ImageClickEventArgs e)
        {
            Comun.EstablecerCulturaUsuario("es-ES");
            Response.Redirect(this.Request.Url.ToString());
        }

        protected void imgEN_Click(object sender, ImageClickEventArgs e)
        {
            Comun.EstablecerCulturaUsuario("en-US");
            Response.Redirect(this.Request.Url.ToString());
        }

        protected void imgCH_Click(object sender, ImageClickEventArgs e)
        {
            Comun.EstablecerCulturaUsuario("zh-CN");
            Response.Redirect(this.Request.Url.ToString());
        }


    }
}
