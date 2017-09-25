using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AVE
{
/// <summary>
/// Información referente al usuario autenticado actualmente en la aplicación
/// </summary>
    public static class Contexto
    {
        public static string IdEmpleado
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session[Constantes.Session.IdEmpleado] == null)
                    {
                        Comun.CargarUsuarioSesion((Guid)Membership.GetUser().ProviderUserKey);
                    }
                    return HttpContext.Current.Session[Constantes.Session.IdEmpleado].ToString();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        //public static string Usuario
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Constantes.Session.Usuario] == null)
        //        {
        //            Comun.CargarUsuarioSesion((Guid)Membership.GetUser().ProviderUserKey);
        //        }
        //        return HttpContext.Current.Session[Constantes.Session.Usuario].ToString();
        //    }
        //}

        public static string Usuario { get; set; }


        public static string IdTienda
        {
            get
            {
                if (HttpContext.Current.Session[Constantes.Session.IdTienda] == null)
                {
                    Comun.CargarUsuarioSesion((Guid)Membership.GetUser().ProviderUserKey);
                }
                return HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
            }
           
        }

        public static string NombreTienda
        {
            get
            {
                if (HttpContext.Current.Session[Constantes.Session.NombreTienda] == null)
                {
                    Comun.CargarUsuarioSesion((Guid)Membership.GetUser().ProviderUserKey);
                }
                return HttpContext.Current.Session[Constantes.Session.NombreTienda].ToString();
            }
        }

        public static string IdTerminal
        {
            get
            {
                if (HttpContext.Current.Session[Constantes.CteCookie.IdTerminal] != null)
                    return HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
                else
                    return string.Empty;
            }
          
        }

        public static DateTime FechaSesion
        {
            get
            {
                if (HttpContext.Current.Session[Constantes.Session.FechaSesion] == null)
                {
                    Comun.CargarUsuarioSesion((Guid)Membership.GetUser().ProviderUserKey);
                }
                return Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString());
            }
        }
     
    }

    [Serializable]
    [XmlRoot(Namespace = "")]
    public class xmlmpos
    {
        [XmlElement]
        public string amount { get; set; }

        [XmlElement]
        public string reference { get; set; }

        [XmlElement]
        public string id_company { get; set; }

        [XmlElement]
        public string id_branch { get; set; }

        [XmlElement]
        public string cd_merchant { get; set; }

        [XmlElement]
        public string cc_type { get; set; }

        [XmlElement]
        public string currency { get; set; }

        [XmlElement]
        public string country { get; set; }

        [XmlElement]
        public string cd_user { get; set; }

        [XmlElement]
        public string password { get; set; }

        [XmlElement]
        public string cd_usrtrx { get; set; }

        [XmlElement]
        public string e_mail { get; set; }

    }

    [Serializable]
    [XmlRoot(Namespace = "")]
    public class xmldatFacturacion
    {
        [XmlElement]
        public string RFC { get; set; }

        [XmlElement]
        public string nombre { get; set; }

        [XmlElement]
        public string direccion { get; set; }

        [XmlElement]
        public string interior { get; set; }

        [XmlElement]
        public string exterior { get; set; }

        [XmlElement]
        public string estado { get; set; }

        [XmlElement]
        public string ciudad { get; set; }

        [XmlElement]
        public string colonia { get; set; }

        [XmlElement]
        public string cp { get; set; }
        [XmlElement]
        public string telefono { get; set; }

        [XmlElement]
        public string movil { get; set; }

    }
    // Clase que hereda de StringWriter y sobreescribe el Encoding por defecto
    // Así se puede generar XML codificado en UTF-8     
    public class StringWriterUtf8 : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }

    public class FinalizaVenta
    {
        public string cliente { get; set; }
        public string Ticket { get; set; }
        public string Entrega { get; set; }
    } 

}