using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DLLGestionVenta.Models;
using CapaDatos;



namespace DLLGestionVenta.CapaDatos
{
    static class StockTiendaDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<StockTienda> GetStockTiendasLineaCarrito(string idLineaCarrito) {
            DataSet dsStockTiendasLineaCarrito;
            SqlCommand myCommand;
            SqlDataAdapter adapter;
            SqlConnection myConnection;
            List<StockTienda> listadoStockTiendas = new List<StockTienda>();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT Stock,IdTienda FROM AVE_CARRITO_STOCK WHERE IdLineaCarrito = " + idLineaCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            adapter = new SqlDataAdapter(myCommand);
            dsStockTiendasLineaCarrito = new DataSet();

            adapter.Fill(dsStockTiendasLineaCarrito);
            myConnection.Close();

            foreach (DataRow  row in dsStockTiendasLineaCarrito.Tables[0].Rows)
            {
                StockTienda stock = new StockTienda();
                stock.stock = Convert.ToInt32(row["Stock"].ToString());
                stock.tienda = row["IdTienda"].ToString();
                listadoStockTiendas.Add(stock);
            }

            return listadoStockTiendas;
        }

        public static DataSet GetLineasCarrito(string idCarrito)
        {
            DataSet dsLineasCarrito;
            SqlCommand myCommand;
            SqlDataAdapter adapter;
            SqlConnection myConnection;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT * FROM AVE_CARRITO_LINEA WHERE Id_Carrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            adapter = new SqlDataAdapter(myCommand);
            dsLineasCarrito = new DataSet();

            adapter.Fill(dsLineasCarrito);

            myConnection.Close();

            return dsLineasCarrito;
        }

        public static string GetReferencia(string IdArticulo)
        {
            string referencia = string.Empty;
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT CodigoAlfa FROM ARTICULOS WHERE IdArticulo = " + IdArticulo;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                referencia = myCommand.ExecuteScalar().ToString();

                return referencia;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string GetTallaLineaCarrito(string idPedido)
        {
            string talla = string.Empty;
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT Talla FROM AVE_PEDIDOS WHERE IdPedido = " + idPedido;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                talla = myCommand.ExecuteScalar().ToString();

                return talla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool HayEnTienda(string idLineaCarrito, string idTienda)
        {
            try
            {
                int stock;
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT stock FROM AVE_CARRITO_STOCK WHERE IdLineaCarrito = " + idLineaCarrito + " AND IdTienda = " + idTienda;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                stock = Convert.ToInt32(myCommand.ExecuteScalar().ToString());

                if (stock > 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool HayEnRestoTiendas(string idLineaCarrito, string idTienda)
        {
            try
            {
                int stock;
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT ISNULL(SUM(stock),0) FROM AVE_CARRITO_STOCK WHERE IdLineaCarrito = " + idLineaCarrito + " AND IdTienda NOT IN (" + idTienda + ")";

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                stock = Convert.ToInt32(myCommand.ExecuteScalar().ToString());

                if (Convert.IsDBNull(myCommand.ExecuteScalar()) || (stock <= 0))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDescripcionArticulo(string idArticulo)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string descripcion;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Descripcion FROM ARTICULOS WHERE IdArticulo = " + idArticulo;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            descripcion = myCommand.ExecuteScalar().ToString();

            myConnection.Close();
            return descripcion;
        }

        public static void ActualizarReservaThinkRetail(int idPedido, string reservaThinkRetail, string estadoReservaThinkRetail) 
        {
            try
            {
                string sql;
                string reserva = reservaThinkRetail;
                if (reserva.Length > 7990) reserva.Substring(0, 7990);
                ClsCapaDatos datos = new ClsCapaDatos();

                sql = "UPDATE AVE_CARRITO_LINEA SET ReservaThinkRetail = '" + reserva + "' , EstadoReservaThinkRetail = '" + estadoReservaThinkRetail + "' WHERE IdPedido = " + idPedido;

                datos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString(); ;
                datos.ActualizarSQL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtieneUltimoTicketTR(string IdTienda, string IdTerminal)
        {
            string idTicket = "";
            SqlConnection myConnection;
            SqlCommand myCommand;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            myConnection = new SqlConnection(connectionString);
            try
            {
                String StrSql;
                int iTicket = 0;

                StrSql = "SELECT isnull(max(Id_ticket),0) from N_tickets where Id_Tienda='" + IdTienda + "' and ID_TERMINAL='" + IdTerminal + "'";
               
                myConnection.Open();
                
                myCommand = new SqlCommand(StrSql, myConnection);
               
                if (Convert.IsDBNull(myCommand.ExecuteScalar()))   idTicket = "";
                else  idTicket = myCommand.ExecuteScalar().ToString();
                if (idTicket != "")
                {
                    string[] split = idTicket.Split('/');
                    iTicket = Convert.ToInt32(split[0]) + 1;
                    idTicket = iTicket.ToString("D6");
                }
                else {
                    iTicket = Convert.ToInt32("1");
                    idTicket = iTicket.ToString("D6");
                }
            }
            finally {
                myConnection.Close();
            }
            return idTicket;
        }

        public static string GetReservaThinkRetail(string idPedido)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string reservaThinkRetail = string.Empty;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT ReservaThinkRetail FROM AVE_CARRITO_LINEA WHERE IdPedido = " + idPedido;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                reservaThinkRetail = "NULL";
                return reservaThinkRetail;
            }
            else
            {
                reservaThinkRetail = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return reservaThinkRetail;
            }
        }

        public static string GetEstadoReservaThinkRetail(string idPedido)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string EstadoReservaThinkRetail = string.Empty;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT EstadoReservaThinkRetail FROM AVE_CARRITO_LINEA WHERE IdPedido = " + idPedido;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                EstadoReservaThinkRetail = "NULL";
                return EstadoReservaThinkRetail;
            }
            else
            {
                EstadoReservaThinkRetail = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return EstadoReservaThinkRetail;
            }
        }


    }
}
