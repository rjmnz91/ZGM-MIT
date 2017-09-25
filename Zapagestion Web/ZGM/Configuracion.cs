using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Web;

namespace AVE
{
    public static class Configuracion
    {
        //String estático en el que se almacena la cadena de conexión a la BBDD, para que sea más fácil su utilización
        static string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
        
        public struct Clave
        {
            public const string ModoVisualizacion = "IdTipoVista";
        }

        private static bool VtaSearch = true;
        public static bool getVtaSearch()
        {
            return VtaSearch;
        }
        public static void setVtaSearch(bool value)
        {
            VtaSearch = value;
        }
        
        public static object ObtenerClave(string clave)
        {
            string sp = "AVE_ConfiguracionesObtener";

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@IdTienda", Contexto.IdTienda);

            DataSet ds = SqlHelper.ExecuteDataset(cadenaConexion, sp, param);

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains(clave))
                return ds.Tables[0].Rows[0][clave];
            else
                return string.Empty;
        }

        public static void ComprobarCompatibilidad()
        {
            String SP = "AVE_CambiaCompatibilidadBBDD";
            String StrCompatibilidad=String.Empty;
            try {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@level", SqlDbType.Int);
                param[0].Direction = ParameterDirection.Output;
                param[1] = new SqlParameter("@DDBB", SqlDbType.VarChar, 50);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(cadenaConexion, CommandType.StoredProcedure, SP, param);


                if (param[0].Value.ToString() != "90")
                {
                    StrCompatibilidad = "exec sys.sp_dbcmptlevel " + param[1].Value.ToString() + ", 90";
                    SqlHelper.ExecuteNonQuery(cadenaConexion, CommandType.Text, StrCompatibilidad);
                }  
            
            }
            catch (Exception ex)
            { }
            

        }
        /// <summary>
        /// comprobamos si hay Solicitud vendidas, para el caso de que se haya perdido la sesión
        /// para volver a mostrar el carrito.
        /// </summary>
        /// <returns></returns>
        public static bool ComprobarCarritoPendiente()
        {
            bool result = false;
            //ACL.Obtenemos el ultimo carrito pendiente con estado 0
            String SP = "AVE_UltimoCarritoPendiente";
            SqlDataReader DCarr;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdUsuario", Contexto.IdEmpleado);
          //  param[1] = new SqlParameter("@IdMaquina", System.Web.HttpContext.Current.Request.UserHostAddress);


            DCarr = SqlHelper.ExecuteReader(cadenaConexion, CommandType.StoredProcedure, SP, param);
            while (DCarr.Read())
            {
                if (DCarr[0].ToString() != "") { 
                    HttpContext.Current.Session["IdCarrito"] = DCarr[0].ToString();
                    result = true;
                }
            }
            DCarr.Close();

            return result;
        }

        /// <summary>
        /// comprobamos si hay solicitudPendiente
        /// </summary>
        /// <returns></returns>
        public static bool ComprobarSolicitudesPendientes()
        {
            String SP = "AVE_UltimoPedidoPendiente";
            DataSet Ds;
            DateTime FechaSesionPedido;
            
            Ds=SqlHelper.ExecuteDataset(cadenaConexion, CommandType.StoredProcedure, SP);

            if (HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido]!= null && HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] != DBNull.Value)
            {
               FechaSesionPedido=(DateTime)HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido];

            }
            else
            {
                FechaSesionPedido = DateTime.Now;

            }  
            if (Ds.Tables[0].Rows.Count > 0)
            {
                if (((DateTime)Ds.Tables[0].Rows[0]["Fecha_Creacion"]) > FechaSesionPedido  &&  Ds.Tables[0].Rows[0]["idTienda"].ToString()  == AVE.Contexto.IdTienda  )
                {   
                    //asignamos valor para que avise 1 vez
                    HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = (DateTime)Ds.Tables[0].Rows[0]["Fecha_Creacion"];
                    reproducirsonido();
                    return true; 
                }
                else
                {

                    return false; 
                }  

            }
            else
            {
                return false; 
            } 
          
        }

        public static void reproducirsonido() {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation="Sonido/Alarma.wav";
        }


    }
}
