using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace AVE
{
    public static class Estadisticas
    {
        public static void InsertarBusqueda(string articulo, string talla, string usuario, string idTerminal)
        {
            SqlConnection cadenaConexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());

            string sp = "AVE_EstadisticasGuardar";

            SqlParameter[] param = new SqlParameter[11];

            param[0] = new SqlParameter("@Accion", "Buscar");
            param[1] = new SqlParameter("@Articulo", articulo);
            param[2] = new SqlParameter("@Talla", talla);
            param[3] = new SqlParameter("@Usuario", usuario);
            param[4] = new SqlParameter("@IdTerminal", idTerminal);

           SqlHelper.ExecuteNonQuery(cadenaConexion, sp, param);
        }

        public static void InsertarBusqueda(string articulo, string talla, string usuario, string idTerminal,String Seccion,String Marca,String Corte,String Material,String Color,String Comentario)
        {
            SqlConnection cadenaConexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());

            string sp = "AVE_EstadisticasGuardar";

            SqlParameter[] param = new SqlParameter[11];

            param[0] = new SqlParameter("@Accion", "Registrar Petición");
            param[1] = new SqlParameter("@Articulo", articulo);
            param[2] = new SqlParameter("@Talla", talla);
            param[3] = new SqlParameter("@Usuario", usuario);
            param[4] = new SqlParameter("@IdTerminal", idTerminal);
            param[5] = new SqlParameter("@Seccion", Seccion);
            param[6] = new SqlParameter("@Marca", Marca);
            param[7] = new SqlParameter("@Corte", Corte);
            param[8] = new SqlParameter("@Material", Material );
            param[9] = new SqlParameter("@color", Color );
            param[10] = new SqlParameter("@Comentario", Comentario);
          

            SqlHelper.ExecuteNonQuery(cadenaConexion, sp, param);
        }
    }
}
