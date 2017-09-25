using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DLLGestionVenta;
using DLLGestionVenta.Models;
using System.IO;

namespace AVE
{
    public partial class RespuestaPago : CLS.Cls_Session
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string sRedirectPage = "~/CarritoDetalle.aspx"; // Por defecto redirigir a Login, por si llega a esta página manualmente

            if (!Page.IsPostBack && Session["IdCarrito"] != null && Request["xml_response"] != null)
            {

                int idCarritoPago = int.Parse(HttpContext.Current.Session["IdCarritoPago"].ToString());

                // Desencriptar respuesta
                String parameter = Request["xml_response"].ToString();
                string semilla = HttpContext.Current.Session[Constantes.Session.Semilla].ToString();
                rc4 encripta = new rc4();
                string respuesta = encripta.Pura(encripta.hexStringToString(parameter), semilla);

                // Log Respuesta

                CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
                objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                objDatos.LogRespuestaPago(respuesta, "RespuestaPago.Page_Load()", Constantes.Session.IdTienda, "-1", Constantes.Session.IdEmpleado, idCarritoPago);

                string foliocpagos = string.Empty;
                string auth = string.Empty;
                string cc_number = string.Empty;
                string response = string.Empty;
                string cd_error = string.Empty;
                string nb_error = string.Empty;
                string cc_type = string.Empty;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(respuesta);

                //Cargar los valores de la respuesta 
                foreach (XmlNode node in xml["webpaympos_response"].ChildNodes)
                {

                    switch (node.Name)
                    {
                        case "response":
                            response = node.InnerText;
                            break;
                        case "cc_number":
                            cc_number = node.InnerText;
                            break;
                        case "auth":
                            auth = node.InnerText;
                            break;
                        case "foliocpagos":
                            foliocpagos = node.InnerText;
                            break;
                        case "cd_error":
                            cd_error = node.InnerText;
                            break;
                        case "nb_error":
                            nb_error = node.InnerText;
                            break;
                        case "cc_type":
                            cc_type = node.InnerText;
                            break;
                        default:
                            break;
                    }
                }

                // Evaluar respuesta del servicio ( approved / denied / error )
                switch (response)
                {
                    case "approved":
                        ProcesarVenta mObjVenta = new ProcesarVenta();
                        mObjVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                        mObjVenta.ValidarPago(idCarritoPago, foliocpagos, auth, cc_number, cc_type);
                        break;
                    case "denied":
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "denied_scr", string.Format("alert('Operación denegada: {0} - {1}');", cd_error, nb_error), true);
                        break;
                    case "error":
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_scr", string.Format("alert('Error al procesar el pago: {0} - {1}');", cd_error, nb_error), true);
                        break;
                }

            }

            if (sRedirectPage != string.Empty)
            {
                Response.Redirect(sRedirectPage, false);
                return; // Importante esto para que no se produzca una excepción desde IIS.
            }
        }
    }
}