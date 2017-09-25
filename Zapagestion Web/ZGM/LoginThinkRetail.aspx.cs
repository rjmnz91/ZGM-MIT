using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace AVE
{
    public partial class LoginThinkRetail : CLS.Cls_Session
    {
        private string UrlHermesAppSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString() != null)
                    return System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString();
                else
                    return string.Empty;
            }
        }
        private string PaginaHermesToLoginAppSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PaginaHermesToLogin"].ToString() != null)
                    return System.Configuration.ConfigurationManager.AppSettings["PaginaHermesToLogin"].ToString();
                else
                    return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Contexto.IdEmpleado == null)
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                return;
            }

            string nombreUsuario = GetNombreUsuario(Contexto.IdEmpleado);
            string apellidosUsuario = GetApellidosUsuario(Contexto.IdEmpleado);
            
            //string urlToRedirect = Request.QueryString["UrlToRedirect"];
            Response.Redirect(UrlHermesAppSettings + "?idUser=" + Contexto.IdEmpleado +"&nombre=" + nombreUsuario + "&apellidos=" + apellidosUsuario);
        }

        private string GetNombreUsuario(string idUsuario)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string nombreUsuario;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Nombre FROM Empleados WHERE IdEmpleado = " + idUsuario;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return string.Empty;
            }
            else
            {
                nombreUsuario = Convert.ToString(myCommand.ExecuteScalar());
                myConnection.Close();
                return nombreUsuario;
            }
        }

        private string GetApellidosUsuario(string idUsuario)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string apellidosUsuario;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Apellidos FROM Empleados WHERE IdEmpleado = " + idUsuario;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return string.Empty;
            }
            else
            {
                apellidosUsuario = Convert.ToString(myCommand.ExecuteScalar());
                myConnection.Close();
                return apellidosUsuario;
            }
        }
    }
}