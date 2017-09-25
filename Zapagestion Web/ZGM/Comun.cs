using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.Security;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Net;
using CapaDatos;

namespace AVE
{
    public static class Comun
    {
        static ClsCapaDatos objCapaDatos;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClsCapaDatos));        

        public static void EstablecerCulturaUsuario(string cultura)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constantes.CteCookie.Idioma, cultura));
        }

        public static AVEComprobarLogin ComprobarLogin(string usuario_in)
        {
            AVEComprobarLogin oComprobarLogin = new AVEComprobarLogin();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "GetComprobarLogin";
                    command.Parameters.AddWithValue("@Usuario", usuario_in);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        oComprobarLogin.IdEmpleado = (int)reader["IdEmpleado"];
                        oComprobarLogin.ControlP = reader["ControlP"].ToString();
                        oComprobarLogin.Usuarios = reader["Usuarios"].ToString();
                    }

                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }
            return oComprobarLogin;
        }
        public static int checkTiendaCamper()
        {


            int valor = 0;
            int TiendaCamper = 0;
            try
            {
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                DataSet Ds;

                string strSQL = "";


                strSQL = "select Valor from CONFIGURACIONES_TPV_AVANZADO WHERE NombreCampo='UsoCliente9' and IdTienda =(Select Tienda from configuraciones_tpv)";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                     valor = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
                     if (valor == 0) TiendaCamper = 1;
                     
                }

                Ds.Dispose();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return TiendaCamper;

        }
        public static void CargaConfiguracionMIT()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "Select * from configuraciones_mit where id_terminal=" + HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();                    
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        HttpContext.Current.Session[Constantes.Session.IdCompany] = reader["COMPANY"];
                        HttpContext.Current.Session[Constantes.Session.IdBranch] = reader["BRANCH"];
                        HttpContext.Current.Session[Constantes.Session.CdMerchant] = reader["MERCHANT"];
                        HttpContext.Current.Session[Constantes.Session.Country] = reader["COUNTRY"];
                        HttpContext.Current.Session[Constantes.Session.CdUser] = reader["CCUSER"];
                        HttpContext.Current.Session[Constantes.Session.CdPassword] = reader["CCPASSWORD"];
                        HttpContext.Current.Session[Constantes.Session.Semilla] = reader["Semilla"];
                        HttpContext.Current.Session[Constantes.Session.MerchantAMEX] = reader["MERCHANTAMEX"]; 
                    }
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }

        }
        public  static bool CargarUsuarioSesion(Guid userGuid)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                int esTR = 0;
                string FSes = DateTime.Now.ToShortDateString();
                try
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["EntornoTR"] != null) esTR = 1;
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AVE_ObtenerDatosLogin";
                    command.Parameters.AddWithValue("@Usuario", userGuid);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        HttpContext.Current.Session[Constantes.Session.IdEmpleado] = reader["IdEmpleado"];
                        HttpContext.Current.Session[Constantes.Session.Usuario] = Membership.GetUser(userGuid).UserName;   //Membership.GetUser() todavía no tiene al usuario recién registrado
                        HttpContext.Current.Session[Constantes.Session.IdTienda] = reader["IdTienda"];
                        HttpContext.Current.Session[Constantes.Session.NombreTienda] = reader["NombreTienda"];
                        HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = reader["FechaPedido"];
                    }

                    reader.Close();

                    command = new SqlCommand();
                    command.CommandText = "AVE_LeeDatosIniciales";
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Usuario", HttpContext.Current.Session[Constantes.Session.IdEmpleado].ToString());


                    SqlDataReader reader1 = command.ExecuteReader();

                    if (reader1.Read())
                    {

                        //ACL. Si es AVE THINK RETAIL SE COJE COMO FECHA DE SESION LA FECHA DE HOY
                        if (esTR == 1)
                        {
                            HttpContext.Current.Session[Constantes.Session.FechaSesion] = Convert.ToDateTime(FSes);
                        }
                        else
                        {
                            HttpContext.Current.Session[Constantes.Session.FechaSesion] = Convert.ToDateTime(reader1["FechaActiva"].ToString());
                        }



                        HttpContext.Current.Session[Constantes.CteCookie.IdTerminal] = reader1["Terminal"].ToString();
                    }



                }
                catch (Exception ex)
                {
                    log.Error("Exception cargar_usuario:" + ex.Message.ToString());
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();

                return true;
            }

        }

        // MJM 20/03/2014 INICIO
        //
        // Se cambia a este método para comprobar el usuario usando el nombre de maquina introducido en vez de la ip.
        //
        public static bool CargarUsuarioSesion(string user_in, string nombreMaquina)
        {
            bool RegistroTerminal = false;

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AVE_LeeDatosIniciales";
                    command.Parameters.AddWithValue("@Usuario", user_in);
                    command.Parameters.AddWithValue("@Maquina", nombreMaquina);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        HttpContext.Current.Session[Constantes.Session.IdEmpleado] = reader["IdEmpleado"];
                        HttpContext.Current.Session[Constantes.Session.Usuario] = reader["Nombre"].ToString() + ' ' + reader["Apellidos"].ToString();
                        HttpContext.Current.Session[Constantes.Session.IdTienda] = reader["IdTienda"];
                        HttpContext.Current.Session[Constantes.Session.NombreTienda] = reader["NombreTienda"];
                        HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = reader["FechaPedido"];
                        HttpContext.Current.Session[Constantes.Session.FechaSesion] = Convert.ToDateTime(reader["FechaActiva"].ToString());
                        HttpContext.Current.Session[Constantes.CteCookie.IdTerminal] = reader["Terminal"].ToString();
                        RegistroTerminal = true;
                    }

                    reader.Close();


                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }

            return RegistroTerminal; 

        }
        // MJM 20/03/2014 FIN
        public static bool CargarUsuarioSesion(string user_in)
        {
           
           bool RegistroTerminal=false; 
            
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
              

              try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AVE_LeeDatosIniciales";
                    command.Parameters.AddWithValue("@Usuario", user_in);
                    command.Parameters.AddWithValue("@Maquina", System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                    SqlDataReader reader = command.ExecuteReader();
                   
                    if (reader.Read())
                    {
                        HttpContext.Current.Session[Constantes.Session.IdEmpleado] = reader["IdEmpleado"];
                        HttpContext.Current.Session[Constantes.Session.Usuario] = reader["Nombre"].ToString() + ' ' +reader["Apellidos"].ToString();   
                        HttpContext.Current.Session[Constantes.Session.IdTienda] = reader["IdTienda"];
                        HttpContext.Current.Session[Constantes.Session.NombreTienda] = reader["NombreTienda"];
                        HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = reader["FechaPedido"];
                        HttpContext.Current.Session[Constantes.Session.FechaSesion] = Convert.ToDateTime(reader["FechaActiva"].ToString());
                        HttpContext.Current.Session[Constantes.CteCookie.IdTerminal] = reader["Terminal"].ToString();
                        RegistroTerminal =true;
                    }

                    reader.Close();

                    
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }

            return RegistroTerminal; 

        }

        public static void InsertarProductoNegado(string articulo, string talla, string Descripcion,ref String TipoNegado,String TiendaSelecionada)
        {
            SqlConnection cadenaConexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());

            string sp = "AVE_RegistrarParNegado";

            SqlParameter[] param = new SqlParameter[6];

            if (TiendaSelecionada == String.Empty)
            {
                param[0] = new SqlParameter("@IdTienda", Contexto.IdTienda);
            }
            else
            {
                param[0] = new SqlParameter("@IdTienda", TiendaSelecionada);
            }
            
            param[1] = new SqlParameter("@idArticulo", articulo);
            param[2] = new SqlParameter("@Talla", talla);
            param[3] = new SqlParameter("@Fecha", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            param[4] = new SqlParameter("@Descripcion", Descripcion);

            param[5] = new SqlParameter("@TipoNegado","0");
            param[5].Direction =ParameterDirection.InputOutput;

            SqlCommand Comm = new SqlCommand();
            Comm.CommandType = CommandType.StoredProcedure;
            Comm.CommandText = sp;
            Comm.Connection = cadenaConexion;

            cadenaConexion.Open();

            Comm.Parameters.AddRange(param);

            Comm.ExecuteNonQuery();
          
            TipoNegado =Comm.Parameters[5].Value.ToString();

            cadenaConexion.Close();
  

        }

        public static bool RemoteFileExists(string url, int timeout)     
        {         
          try        
          {                         
             HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
            //   El timeout es en milisegundos             
            request.Timeout = timeout;            
            request.Method = "GET";            
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;             
              //Regresa TRUE si el codigo de esdado es == 200            
           return (response.StatusCode == HttpStatusCode.OK);         
          }
          catch        
          {             
            //Si ocurre una excepcion devuelve false             
          return false;         
          
          }     
        }



      
        public static bool CheckURLWs(string url, int timeout)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(delegateHttpSsl);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //   El timeout es en milisegundos     
                
                request.Proxy = null;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = timeout;
                request.Method = "GET";
                ServicePointManager.SecurityProtocol =   SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Regresa TRUE si el codigo de esdado es == 200            
                return (response.StatusCode == HttpStatusCode.OK);
                
            }
            catch(Exception ex)
            {
                //Si ocurre una excepcion devuelve false  
                log.Error("CheckUrlWs. Error:" + ex.Message.ToString());
                return false;

            }
        }

        private static bool delegateHttpSsl(object obj, System.Security.Cryptography.X509Certificates.X509Certificate c1, System.Security.Cryptography.X509Certificates.X509Chain c2, System.Net.Security.SslPolicyErrors c3)
        {
            return true;
        }

        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        /*Modificación de código para quitar el membership de asp.net y hacerlo custom*/
        /*RG-17-02-2014*/
        /// <summary>
        /// funcion para decodificar la contraseña de la base de datos 
        /// </summary>
        /// <param name="strCadena"></param>
        /// <returns></returns>
        public static String DecodifPWD(String strCadena)
        {
            string[] arrParte;
            long lngValor = 0;
            long parR = 0;
            long parG = 0;
            long parB = 0;
            int intI = 0;
            string StrPass = string.Empty;

            arrParte = strCadena.Split('#');
            for (intI = 0; intI <= arrParte.Length - 1; intI++)
            {
                if (!string.IsNullOrEmpty(arrParte[intI]))
                {
                    lngValor = Convert.ToInt64(arrParte[intI]);
                    parR = Convert.ToInt64(lngValor) % 0x100;
                    lngValor = lngValor / 0x100;
                    parG = Convert.ToInt64(lngValor) % 0x100;
                    lngValor = lngValor / 0x100;
                    parB = Convert.ToInt64(lngValor) % 0x100;
                    StrPass = StrPass + Convert.ToChar(parR) + Convert.ToChar(parG) + Convert.ToChar(parB);
                }
            }
            StrPass = StrPass.Replace(Convert.ToChar(0).ToString(), "");
            return StrPass;
        }
        public static string GetTarjetaNine(string lngClienteID, DateTime fecha)
        {
            string functionReturnValue = "";

            try
            {
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                
                DataSet Ds;

                string strSQL = null;

                if (lngClienteID != "-1")
                {
                    strSQL = "SELECT tarj.NumTarjeta FROM N_CLIENTES_GENERAL cg WITH (NOLOCK) INNER JOIN N_CLIENTES_TARJETAS_FIDELIDAD tarj WITH (NOLOCK) ";
                    strSQL += " ON cg.Id_cliente=tarj.IdCliente WHERE cg.Id_Cliente=" + lngClienteID;
                    strSQL += " AND tarj.FechaCaducidad >= CONVERT(DATETIME,'" + fecha.ToShortDateString() + "',103) AND ISNULL(tarj.IdBaja,0)=0 ORDER BY tarj.IdTarjeta DESC ";

                    Ds = objCapaDatos.GEtSQLDataset(strSQL);


                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        functionReturnValue = Ds.Tables[0].Rows[0]["NumTarjeta"].ToString().Trim();
                    }
                    else
                    {
                        functionReturnValue = "";
                    }

                    Ds.Dispose();
                }
                else
                {
                    functionReturnValue = "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }
       
        public static int  SolicitaRedencionDevo(string IdTienda, string IdEmpleado, string terminal, DateTime fechaActual, string Fpago, string Cliente, double importe,ref string strAutorizacion, ref string strTarjeta)
        {
            string tarjetaNine = "";
            int result = 0;
            try
            {
                tarjetaNine = GetTarjetaNine(Cliente, fechaActual);
                strTarjeta = tarjetaNine;
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
              
                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (Comun.CheckURLWs(url, 10000))
                {
                    DLLGestionVenta.Models.VENTA _v = new DLLGestionVenta.Models.VENTA();

                    _v.Id_Tienda = IdTienda;
                    _v.ID_TERMINAL = terminal;
                    _v.IdCajero = int.Parse(IdEmpleado);
                    _v.Fecha = fechaActual;
                    _v.Id_Empleado = int.Parse(IdEmpleado);
                    
                    Cliente9.cls_Cliente9 C9p = new Cliente9.cls_Cliente9(_v);
                    C9p.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    ws.cls_Cliente9.SolicitaRedencion sr = new ws.cls_Cliente9.SolicitaRedencion();
                    if (Fpago == "PUNTOS NINE") {
                        sr.intTipo = 1;
                        sr.strTarjeta = tarjetaNine;
                        sr.dblMonto = importe*-1;
                        sr.strTienda = IdTienda;
                        sr.idTerminal =terminal;
                        sr.lngCajero = int.Parse(IdEmpleado);
                        C9p.InvokeWS_OperacionesPendientes(1, String.Empty, true);
                       
                    }
                    else if (Fpago == "BOLSA 5")
                    {
                        sr.intTipo = 3;
                        sr.strTarjeta = tarjetaNine;
                        sr.dblMonto = 0;
                        sr.strTienda = IdTienda;
                        sr.idTerminal = terminal;
                        sr.lngCajero = int.Parse(IdEmpleado);

                        C9p.InvokeWS_OperacionesPendientes(3, String.Empty, true);
                       
                    }
                    else if (Fpago == "PAR 9")
                    {
                        sr.intTipo = 3;
                        sr.strTarjeta = tarjetaNine;
                        sr.dblMonto = 0;
                        sr.strTienda = IdTienda;
                        sr.idTerminal = terminal;
                        sr.lngCajero = int.Parse(IdEmpleado);

                        C9p.InvokeWS_OperacionesPendientes(2, String.Empty, true);
                    }

                    String ret = c9.InvokeWS_SolicitaRedencionDev(ref sr,IdEmpleado,IdTienda,fechaActual);

                    if (sr.strBitRedencionP == "1") result = 0;
                    else {
                        strAutorizacion = sr.strNoAutorizacion;
                        result = 1;
                    }

               }


            }
            catch (Exception sqlEx)
            {
                throw new Exception(string.Format("Excepcion: {0} ---- {1}", sqlEx.Message, sqlEx.StackTrace), sqlEx.InnerException);
            }
            return result;
            
        }
         public static void  CheckPending( string IdTienda,string IdEmpleado,string terminal,DateTime fechaActual)
        {
            try
            {
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();

                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (Comun.CheckURLWs(url, 10000))
                {
                    DLLGestionVenta.Models.VENTA _v = new DLLGestionVenta.Models.VENTA();

                    _v.Id_Tienda = IdTienda;
                    _v.ID_TERMINAL = terminal;
                    _v.IdCajero = int.Parse(IdEmpleado);
                    _v.Fecha = fechaActual;
                    _v.Id_Empleado = int.Parse(IdEmpleado);

                    Cliente9.cls_Cliente9 C9p = new Cliente9.cls_Cliente9(_v);
                    C9p.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    C9p.InvokeWS_OperacionesPendientes(1, String.Empty, true);
                    C9p.InvokeWS_OperacionesPendientes(2, String.Empty, true);
                    C9p.InvokeWS_OperacionesPendientes(3, String.Empty, true);
                }
                 
            
            }
            catch (Exception sqlEx)
            {
                throw new Exception(string.Format("Excepcion: {0} ---- {1}", sqlEx.Message, sqlEx.StackTrace), sqlEx.InnerException);
            }    
        } 
    

        public static void  CheckPending()
        {
            try
            {
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();

                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (Comun.CheckURLWs(url, 10000))
                {
                    DLLGestionVenta.Models.VENTA _v = new DLLGestionVenta.Models.VENTA();

                    _v.Id_Tienda = AVE.Contexto.IdTienda;
                    _v.ID_TERMINAL = AVE.Contexto.IdTerminal;
                    _v.IdCajero = int.Parse(AVE.Contexto.IdEmpleado);
                    _v.Fecha = AVE.Contexto.FechaSesion;
                    _v.Id_Empleado = int.Parse(AVE.Contexto.IdEmpleado);

                    Cliente9.cls_Cliente9 C9p = new Cliente9.cls_Cliente9(_v);
                    C9p.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    C9p.InvokeWS_OperacionesPendientes(1, String.Empty, true);
                    C9p.InvokeWS_OperacionesPendientes(2, String.Empty, true);
                    C9p.InvokeWS_OperacionesPendientes(3, String.Empty, true);
                }
                 
            
            }
            catch (Exception sqlEx)
            {
                throw new Exception(string.Format("Excepcion: {0} ---- {1}", sqlEx.Message, sqlEx.StackTrace), sqlEx.InnerException);
            }    
        } 
    }

    public class AVEComprobarLogin
    {
        public string ControlP { get; set; }
        public int IdEmpleado { get; set; }
        public string Usuarios { get; set; }

        public AVEComprobarLogin()
        {
            this.ControlP = string.Empty;
            this.IdEmpleado = -1;
            this.Usuarios = string.Empty;
        }
    }

}
