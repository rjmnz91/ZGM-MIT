using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Data;
using ModdoOnline.Utiles;
using System.Data.SqlClient;

namespace DLLGestionVenta.CapaDatos
{
    class CargosWSDAL
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static DataSet GetDataSetCargos(Int64 IdCargo, string StrTienda, DateTime FechaSesion, Int16 Retif)
        {

            Log.Info(Traza.INICIO + Traza.GetParameters(IdCargo, StrTienda, FechaSesion, Retif));

            try
            {
                SqlCommand SqlDatosCargos = default(SqlCommand);
                SqlDataAdapter Adaptador = default(SqlDataAdapter);
                DataSet Ds = new DataSet();
                SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString);

                SqlDatosCargos = new SqlCommand();
                SqlDatosCargos.CommandType = CommandType.StoredProcedure;
                SqlDatosCargos.CommandText = "WS_UpCargosOnline";
                SqlDatosCargos.Connection = connection;

                SqlParameter objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdCargo";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = IdCargo;

                SqlDatosCargos.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdTienda";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = StrTienda;

                SqlDatosCargos.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Fecha";
                objParametro.SqlDbType = SqlDbType.DateTime;
                objParametro.Value = FechaSesion;

                SqlDatosCargos.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Retif";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = Retif;

                SqlDatosCargos.Parameters.Add(objParametro);

                Adaptador = new SqlDataAdapter(SqlDatosCargos);

                Adaptador.Fill(Ds);

                SqlDatosCargos.Connection.Close();

                Log.Info(string.Format(Traza.RETURN, Traza.GetDataSetValues("Ds", Ds)));
                return Ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Log.Info(Traza.FINAL);
            }

        }

        public void AbrirConexion(ref SqlConnection cnn)
        {
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }
        public void CerrarConexion(ref SqlConnection cnn)
        {
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public static DataSet GetFormatoFechaSRegional(DataSet ds)
        {

            Log.Info(Traza.INICIO + Traza.GetParameters(ds));
            Log.Debug(Traza.GetDataSetValues("ds", ds));

            try
            {
                int i = 0;


                for (i = 0; i <= ds.Tables.Count - 1; i++)
                {
                    foreach (DataColumn column in ds.Tables[i].Columns)
                    {
                        if (object.ReferenceEquals(column.DataType, System.Type.GetType("System.DateTime")))
                        {
                            column.DateTimeMode = DataSetDateTime.Unspecified;
                        }
                    }
                }

                Log.Info(string.Format(Traza.RETURN, Traza.GetDataSetValues("ds", ds)));
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Log.Info(Traza.FINAL);
            }
        }

        #region Insertar Cargo en AVE

        public static int InsertarCargosAVE(string idTiendaOrigen, string idTiendaDestino, int idArticulo, int idEmpleado, string talla)
        {
            int idCargo;
            //Obtener idCargo(tienda)
            idCargo = ObtenerIdCargo(idTiendaDestino);
            
            //Insertar cargo en sus tablas
            InsertarCargo(idCargo, idTiendaOrigen, idTiendaDestino, idEmpleado, idArticulo, talla);

            return idCargo;
        }

        private static int ObtenerIdCargo(string idTienda)
        {
            SqlCommand sql;
            SqlParameter parameter;
            int idCargo;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; ;
            SqlConnection connection = new SqlConnection(connectionString);
            
            sql = new SqlCommand();
            sql.CommandType = CommandType.StoredProcedure;
            sql.CommandText = "AVE_CargosIdCargoGenerar";
            sql.Connection = connection;

            sql.Connection.Open();

            parameter = CrearParametro("@idTienda", SqlDbType.VarChar, 10, idTienda, ParameterDirection.Input);

            sql.Parameters.Add(parameter);

            idCargo = Convert.ToInt32(sql.ExecuteScalar().ToString());
            sql.Connection.Close();

            return idCargo;

        }

        private static void InsertarCargo(int idCargo, string idTiendaOrigen, string idTiendaDestino, int idEmpleado, int idArticulo, string talla)
        {
            
            try
            {
                string connectionString;
                SqlConnection myConnection; 
                SqlCommand sql;
                SqlParameter parameter;
               
                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_InsertarCargoAC";

                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                myConnection = new SqlConnection(connectionString);

                sql.Connection = myConnection;
                sql.Connection.Open();
               
                parameter = CrearParametro("@idCargo", SqlDbType.Int, 0, idCargo, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@idTiendaOrigen", SqlDbType.VarChar, 10, idTiendaOrigen, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@idTiendaDestino", SqlDbType.VarChar, 10, idTiendaDestino, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@idArticulo", SqlDbType.Int, 0, idArticulo, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@idEmpleado", SqlDbType.Int, 0, idEmpleado, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@talla", SqlDbType.VarChar, 0, talla, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                sql.ExecuteNonQuery();

                sql.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        private static SqlParameter CrearParametro(string parameterName, SqlDbType DataTypeBd, int size, object value, ParameterDirection parameterDirection)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();

                parameter.ParameterName = parameterName;
                parameter.SqlDbType = DataTypeBd;
                if (size != 0)
                {
                    parameter.Size = size;
                }
                if (value != null)
                {
                    parameter.Value = value;
                }
                parameter.Direction = parameterDirection;


                return parameter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


    }


}

