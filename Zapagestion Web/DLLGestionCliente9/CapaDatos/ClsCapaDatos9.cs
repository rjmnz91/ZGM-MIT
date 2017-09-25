using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DLLGestionCliente9.Models;
using System.Diagnostics;
using System.Reflection;

namespace DLLGestionCliente9.CapaDatos
{
    public partial class ClsCapaDatos9
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClsCapaDatos9));
        protected SqlConnection Conex;
        protected SqlTransaction SqlTrans;
        
        
        #region Conexiones BBDD

        public String ConexString
        {
            set { Conex.ConnectionString = value; }
        }


        public ClsCapaDatos9()
        {
            Conex = new SqlConnection();

        }

        public SqlTransaction GetTransaction()
        {
            SqlTransaction transaction = null;

            AbrirConexion(Conex);
            transaction = Conex.BeginTransaction();

            return transaction;

        }

        public void ReleaseTransaction()
        {
            CerrarConexion(Conex);
        }

        protected void AbrirConexion(SqlConnection Cnn)
        {
            try
            {
                Cnn.Open();
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        
        protected void CerrarConexion(SqlConnection cnn)
        {
            try
            {

                cnn.Close();

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        #endregion
       
        #region "Metodos Log AVECarrito_WS"

        /// <summary>
        /// Obtiene los parametros de entrada de un metodo para guardarlos en el archivo de log con sus valores 
        /// </summary>
        /// <param name="parameters">Todos los parametros de entrada, tiene que estar en orden </param>
        /// <returns>string con todos los parametros del metodo y sus valores</returns>
        public string GetParameters(params object[] parameters)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();

                StringBuilder sb = new StringBuilder();

                sb.Append(" (");

                if (parameters != null || parameters.Count() > 0)
                {
                    int i = 0;
                    foreach (ParameterInfo paramaterInfo in stackTrace.GetFrame(1).GetMethod().GetParameters())
                    {
                        sb.Append(string.Format("{0} = {1}, ", paramaterInfo.Name, parameters[i] != null ? parameters[i].ToString() : string.Empty));
                        i++;
                    }
                }
                sb.Append(") ");


                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    return sb.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return string.Empty;
            }
        }

        #endregion

    
       #region Metodos  Genericos

        
        public DataSet GEtSQLDataset(String StrSQL)
        {

            try
            {

                SqlCommand SqlGetDatosZapa;
                DataSet DS;
                SqlDataAdapter Adaptador;

                AbrirConexion(Conex);

                SqlGetDatosZapa = new SqlCommand();
                SqlGetDatosZapa.CommandType = CommandType.Text;
                SqlGetDatosZapa.CommandText = StrSQL;
                SqlGetDatosZapa.Connection = Conex;
                SqlGetDatosZapa.CommandTimeout = 0;

                Adaptador = new SqlDataAdapter(SqlGetDatosZapa);

                DS = new DataSet();

                Adaptador.Fill(DS);

                CerrarConexion(Conex);

                return DS;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        protected DataSet GEtSQLDataset(String StrSQL, SqlTransaction SqlTRans)
        {

            try
            {

                SqlCommand SqlGetDatosZapa;
                DataSet DS;
                SqlDataAdapter Adaptador;


                SqlGetDatosZapa = new SqlCommand();
                SqlGetDatosZapa.CommandType = CommandType.Text;
                SqlGetDatosZapa.CommandText = StrSQL;
                SqlGetDatosZapa.Connection = Conex;
                SqlGetDatosZapa.Transaction = SqlTRans;
                SqlGetDatosZapa.CommandTimeout = 0;

                Adaptador = new SqlDataAdapter(SqlGetDatosZapa);

                DS = new DataSet();

                Adaptador.Fill(DS);

             //   CerrarConexion(Conex);

                return DS;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        public void ActualizarSQL(string StrSql)
        {
            try
            {
                SqlCommand SqlActualizarZapa;

                AbrirConexion(Conex);

                SqlActualizarZapa = new SqlCommand();
                SqlActualizarZapa.CommandType = CommandType.Text;
                SqlActualizarZapa.CommandText = StrSql;
                SqlActualizarZapa.Connection = Conex;
                SqlActualizarZapa.CommandTimeout = 0;
                SqlActualizarZapa.ExecuteNonQuery();

                CerrarConexion(Conex);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        protected void ActualizarSQL(string StrSql, SqlTransaction SqlTrans, int option)
        {
            //ACL, se utiliza para el alta de cliente, ya que se abre la transaccion sin abrir Conex.
            //todos los datos de conexion estan en la transaccion
            try
            {
                SqlCommand SqlActualizarZapa;

                SqlActualizarZapa = new SqlCommand();
                SqlActualizarZapa.CommandType = CommandType.Text;
                SqlActualizarZapa.CommandText = StrSql;
                SqlActualizarZapa.Connection = SqlTrans.Connection;
                SqlActualizarZapa.Transaction = SqlTrans;
                SqlActualizarZapa.CommandTimeout = 0;
                SqlActualizarZapa.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        protected void ActualizarSQL(string StrSql, SqlTransaction SqlTRans)
        {
            try
            {
                SqlCommand SqlActualizarZapa;

                SqlActualizarZapa = new SqlCommand();
                SqlActualizarZapa.CommandType = CommandType.Text;
                SqlActualizarZapa.CommandText = StrSql;
                SqlActualizarZapa.Connection = Conex;
                SqlActualizarZapa.Transaction = SqlTRans;
                SqlActualizarZapa.CommandTimeout = 0;
                SqlActualizarZapa.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void CopyDataset(String StrTable, DataSet Ds)
        {
            try
            {
                SqlBulkCopy CopyZapa;
                Int16 i;

                AbrirConexion(Conex);

                CopyZapa = new SqlBulkCopy(Conex);

                CopyZapa.DestinationTableName = StrTable;

                for (i = 0; i <= Ds.Tables[0].Columns.Count - 1; i++)
                {
                    CopyZapa.ColumnMappings.Add(i, i);
                    CopyZapa.ColumnMappings[i].SourceOrdinal = i;
                    CopyZapa.ColumnMappings[i].DestinationOrdinal = i;
                    CopyZapa.ColumnMappings[i].SourceColumn = Ds.Tables[0].Columns[i].ColumnName;
                    CopyZapa.ColumnMappings[i].DestinationColumn = Ds.Tables[0].Columns[i].ColumnName;
                }

                CopyZapa.WriteToServer(Ds.Tables[0]);

                CerrarConexion(Conex);

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }


        }
        #endregion
  }
}