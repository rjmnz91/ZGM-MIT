using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DLLGestionVenta;
using DLLGestionVenta.Models;
using System.Diagnostics;
using System.Text;
using System.Reflection;


namespace CapaDatos
{
    public partial class ClsCapaDatos
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClsCapaDatos));

        protected SqlConnection Conex;
        protected SqlConnection Conex2;
        protected SqlTransaction SqlTrans;
        protected VENTA _Venta;
        float _IvaParametro = 16;

        #region Conexiones BBDD

        public String ConexString
        {
            set { Conex.ConnectionString = value; }
        }


        public ClsCapaDatos()
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
        public string GetTicketInicio()
        {
            string StrTicket = "";
            DataSet Ds;
            try
            {

                string StrSql = "  select  isnull(TicketInicio,0)  from CONFIGURACIONES_TPV ";
                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    StrTicket = Ds.Tables[0].Rows[0][0].ToString();
                    StrTicket += (Int64.Parse(StrTicket) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el siguiente ticket para enviar a MIT." + ex.Message.ToString());
            }
            return StrTicket;
        }
        public string ObtieneUltimoTicket(string IdTienda, string IdTerminal)
        {
            string idTicket = "";
            try
            {
                String StrSql;
                int iTicket = 0;
                DataSet Ds;

                StrSql = "SELECT isnull(max(Id_ticket),0) from N_tickets where Id_Tienda='" + IdTienda + "' and ID_TERMINAL='" + IdTerminal + "'";

                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    idTicket = Ds.Tables[0].Rows[0][0].ToString();
                }
                else idTicket = GetTicketInicio();


                if (idTicket != "")
                {
                    string[] split = idTicket.Split('/');
                    iTicket = Convert.ToInt32(split[0]) + 1;
                    idTicket = iTicket.ToString("D6");
                }
                else
                {
                    iTicket = Convert.ToInt32("1");
                    idTicket = iTicket.ToString("D6");

                }


            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

            return idTicket;
        }

        /// <summary>
        /// funcion que nos calcula el numero de ticket
        /// </summary>
        /// <param name="idTienda"></param>
        /// <param name="idTerminal"></param>
        /// <returns></returns>
        public String GetNewTicket(String idTienda, String idTerminal)
        {
            try
            {
                String StrSql;
                String StrTicket = String.Empty;
                DataSet Ds;

                StrSql = "SELECT isnull(MAX(Id_Ticket),0) FROM N_TICKETS WITH (NOLOCK) WHERE Id_Tienda='" + idTienda + "'";

                if (idTerminal.Length > 0) { StrSql += "AND ID_TERMINAL='" + idTerminal + "'"; }

                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows[0][0].ToString() != "0")
                {
                    StrTicket = Ds.Tables[0].Rows[0][0].ToString();
                    StrTicket = (Int32.Parse(StrTicket.Substring(0, StrTicket.IndexOf("/"))) + 1).ToString();
                }
                else
                {
                    StrSql = "  select  isnull(TicketInicio,0)  from CONFIGURACIONES_TPV ";
                    Ds = GEtSQLDataset(StrSql);

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        StrTicket = Ds.Tables[0].Rows[0][0].ToString();
                        StrTicket += (Int64.Parse(StrTicket) + 1).ToString();

                    }
                    else StrTicket = "1";

                }
                if (StrTicket == "0") StrTicket = "1";
                StrTicket = new string('0', (6 - StrTicket.Length)).ToString() + StrTicket + "/";

                if (idTerminal.Length > 0) { StrTicket += idTerminal + "/"; }

                StrTicket += idTienda;

                return StrTicket;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        /// <summary>
        /// recupera el tipo de iva configurada por parametros
        /// </summary>
        /// <returns></returns>
        public float IVAPRODUCTO()
        {
            try
            {
                String StrSql;
                String StrTicket = String.Empty;
                DataSet Ds;
                float IVA = 0;
                StrSql = "select  ivaproducto from ParametrosGenerales ";

                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    IVA = float.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }

                return IVA;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        /// <summary>
        /// obtener el iva del producto
        /// </summary>
        /// <param name="Idarticulo"></param>
        /// <param name="IVAParametro"></param>
        /// <param name="Tienda"></param>
        /// <returns></returns>
        public List<IVA> IVAARTICULO(string Idarticulo, float IVAParametro, String Tienda)
        {
            try
            {
                String StrSql;
                String StrTicket = String.Empty;
                DataSet Ds;
                float IVA = 0;
                Int64 idGrupo = 0;
                List<IVA> _lstIva;
                List<IVA> LstIVAAux;
                String StrGrupo = String.Empty;

                IVA = IVAParametro;

                StrSql = "SELECT TipoIVA,isnull(idGrupo,0) as IdGrupo,idarticulo FROM ARTICULOS WITH (NOLOCK)  WHERE IdArticulo in (" + Idarticulo + ")";

                Ds = GEtSQLDataset(StrSql, SqlTrans);

                _lstIva = new List<IVA>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    IVA _Iva;
                    _Iva = new IVA();
                    _Iva.Iva = Convert.ToDouble((dr["TipoIVA"] != DBNull.Value ? dr["TipoIVA"] : IVAParametro));
                    _Iva.IdGrupo = Int16.Parse(dr[1].ToString());
                    _Iva.Id_Articulo = int.Parse(dr[2].ToString());
                    _Iva.IVAnombre = "IVA PRODUCTO";

                    if (_Iva.IdGrupo > 0) { StrGrupo += _Iva.IdGrupo.ToString() + ","; }

                    _lstIva.Add(_Iva);

                }

                LstIVAAux = new List<IVA>();
                LstIVAAux = _lstIva;


                if (StrGrupo.Length > 0)
                {
                    { StrGrupo = StrGrupo.Substring(0, StrGrupo.Length - 1); }


                    StrSql = "SELECT distinct gg.Grupo,gt.IVA,g.idGrupo FROM CT_SUBGRUPOS_TIENDAS gt WITH (NOLOCK) ";
                    StrSql += " INNER JOIN CT_SUBGRUPOS g WITH (NOLOCK) ON gt.IdSubGrupo=g.IdSubGrupo ";
                    StrSql += " INNER JOIN CT_GRUPOS gg WITH (NOLOCK) ON g.IdGrupo=gg.IdGrupo ";
                    StrSql += " WHERE g.IdGrupo in (" + StrGrupo + ") AND gt.IdTienda='" + Tienda + "'";


                    Ds = GEtSQLDataset(StrSql, SqlTrans);





                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        List<IVA> _Grupo = _lstIva.FindAll(p => p.IdGrupo == Int16.Parse(Ds.Tables[0].Rows[0]["IdGrupo"].ToString()));

                        for (int i = 0; i < _Grupo.Count; i++)
                        {
                            LstIVAAux.Remove(_Grupo[i]);

                            _Grupo[i].Iva = Convert.ToDouble(dr["IVA"].ToString());
                            _Grupo[i].IVAnombre = dr["Grupo"].ToString();


                            LstIVAAux.Add(_Grupo[i]);

                        }
                    }
                }


                _lstIva = LstIVAAux;
                return _lstIva;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        public DataSet GetSolicitud(Int64 idPedido)
        {
            try
            {
                String Strsql = String.Empty;
                DataSet Ds;

                Strsql = " Select AVE_PEDIDOS.IdPEdido,AVE_PEDIDOS.IdArticulo, Unidades,IdTienda,IdEmpleado,c.Id_Cabecero_Detalle, ";
                Strsql += " (Select  Tienda from CONFIGURACIONES_TPV)  as TiendaDestino from ave_pedidos ";
                Strsql += " inner join Articulos on AVE_PEDIDOS.IdArticulo=ARTICULOS.IdArticulo inner join CABECEROS_DETALLES c on ";
                Strsql += " Articulos.Id_Cabecero=c.Id_Cabecero and AVE_PEDIDOS.Talla=c.Nombre_Talla where IdPedido =" + idPedido;
                Strsql += "  select  (select DirFtp from ParametrosGenerales) as DirFTP, (Select convert(datetime,fechaActiva,103)  from CONFIGURACIONES_TPV) as fechaActiva ";


                Ds = GEtSQLDataset(Strsql);

                return Ds;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }

        public Int16 InsertarEntradaCargoAutoMatico(DataTable DTCargos, String Tienda)
        {
            try
            {
                SqlCommand SqlCargos;
                SqlParameter objParametro;


                if (DTCargos.Rows.Count > 0)
                {
                    AbrirConexion(Conex);
                    SqlTrans = Conex.BeginTransaction();

                    foreach (DataRow DT in DTCargos.Rows)
                    {

                        SqlCargos = new SqlCommand();
                        SqlCargos.CommandType = CommandType.StoredProcedure;
                        SqlCargos.CommandText = "AVE_CargoAutomaticoEntrada";
                        SqlCargos.Connection = Conex;
                        SqlCargos.Transaction = SqlTrans;

                        objParametro = new SqlParameter();
                        objParametro.ParameterName = "@IdCargo";
                        objParametro.SqlDbType = SqlDbType.Int;
                        objParametro.Value = DT["IdCargo"];

                        SqlCargos.Parameters.Add(objParametro);

                        objParametro = new SqlParameter();
                        objParametro.ParameterName = "@idPedido";
                        objParametro.SqlDbType = SqlDbType.Int;
                        objParametro.Value = DT["IdPedido"];

                        SqlCargos.Parameters.Add(objParametro);

                        objParametro = new SqlParameter();
                        objParametro.ParameterName = "@Fecha";
                        objParametro.SqlDbType = SqlDbType.DateTime;
                        objParametro.Value = DT["Fecha"];

                        SqlCargos.Parameters.Add(objParametro);

                        objParametro = new SqlParameter();
                        objParametro.ParameterName = "@tienda";
                        objParametro.SqlDbType = SqlDbType.VarChar;
                        objParametro.Size = 10;
                        objParametro.Value = Tienda;

                        SqlCargos.Parameters.Add(objParametro);

                        SqlCargos.ExecuteNonQuery();

                    }

                    SqlTrans.Commit();
                    CerrarConexion(Conex);

                }

                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected internal DataSet GeDataSetUpClientes(int idCliente, int TipoAccion)
        {



            try
            {

                SqlCommand SqlDatosUpClientes;
                SqlDataAdapter Adaptador;
                DataSet Ds = new DataSet();

                AbrirConexion(Conex);

                SqlDatosUpClientes = new SqlCommand();
                SqlDatosUpClientes.CommandType = CommandType.StoredProcedure;
                SqlDatosUpClientes.CommandText = "WS_UpClientesOnline";
                SqlDatosUpClientes.Connection = Conex;

                SqlParameter objParametro = new SqlParameter();
                objParametro.ParameterName = "@id_Cliente";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = idCliente;

                SqlDatosUpClientes.Parameters.Add(objParametro);


                objParametro = new SqlParameter();
                objParametro.ParameterName = "@TipoAccion";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = TipoAccion;

                SqlDatosUpClientes.Parameters.Add(objParametro);

                Adaptador = new SqlDataAdapter(SqlDatosUpClientes);

                Adaptador.Fill(Ds);

                CerrarConexion(Conex);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }


        }


        protected internal DataSet GetDataSetEntradaCargos(Int64 IdCargo, string StrTienda, DateTime FechaSesion)
        {


            try
            {
                SqlCommand SqlDatosCargos;
                SqlDataAdapter Adaptador;
                DataSet Ds = new DataSet();

                AbrirConexion(Conex);

                SqlDatosCargos = new SqlCommand();
                SqlDatosCargos.CommandType = CommandType.StoredProcedure;
                SqlDatosCargos.CommandText = "WS_UpEntradaCargosOnline";
                SqlDatosCargos.Connection = Conex;

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

                Adaptador = new SqlDataAdapter(SqlDatosCargos);

                Adaptador.Fill(Ds);

                CerrarConexion(Conex);

                return Ds;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        #endregion

        #region RecuperarDatos

        #region RecuperarClienteEmpleado
        public List<CLIENTE9> GetDatCliente(String strCliente, DateTime Fecha)
        {
            try
            {

                String StrSQl;
                DataSet Ds;
                CLIENTE9 objc9;
                List<CLIENTE9> LstCliente;
                double Num;
                String StrTarjeta = String.Empty;
                String numTarjeta = String.Empty;

                LstCliente = new List<CLIENTE9>();


                StrSQl = " SELECT cg.Id_Cliente,cg.Nombre,cg.Apellidos,tarj.NumTarjeta,ct.Tipo_Cliente,tarj.idTarjeta";
                StrSQl += ",ct.Tipo_CE,tarj.IdSocioC9,cg.cif,cg.apellido1 + ' ' + cg.apellido2 AS Apellidos2 ,isnull(tarj.Descripcion,'') as Nivel, cg.email, ct.Tipo_Cliente ,ct.Tipo_CE,isnull(cg.Telefono,'') Telefono, isnull(cg.TelefonoMovil,'') TelefonoMovil, convert(datetime,Fecha_Nacimiento,103) Fecha_Nacimiento ";
                StrSQl += " FROM N_CLIENTES_GENERAL cg WITH (NOLOCK)   ";
                StrSQl += " LEFT JOIN (SELECT MAX(IdTarjeta) AS IdTarjeta ";
                StrSQl += " ,IdCliente FROM N_CLIENTES_TARJETAS_FIDELIDAD WITH (NOLOCK) WHERE ISNULL(FechaCaducidad, '31/12/2999') >= ";
                StrSQl += " CONVERT(DATETIME, '" + Fecha.ToShortDateString() + "',103) AND isnull(IdBaja, 0) = 0 	GROUP BY IdCliente ";
                StrSQl += " ) tarj1 ON cg.Id_Cliente = tarj1.IdCliente LEFT JOIN N_CLIENTES_TARJETAS_FIDELIDAD tarj WITH (NOLOCK) ";
                StrSQl += "  ON tarj.IdCliente = cg.Id_Cliente AND tarj.IdTarjeta = tarj1.IdTarjeta LEFT JOIN N_CLIENTES_TIPO ct WITH (NOLOCK) ";
                StrSQl += "  ON cg.Id_Tipo = ct.Id_Tipo WHERE cg.Id_cliente <> 0 AND cg.FECHA_BAJA IS NULL 	AND ( ";
                StrSQl += "  cast (cg.Id_Cliente as varchar) = '" + strCliente + "'";
                StrSQl += " OR cg.Cif LIKE '%" + strCliente + "%'";
                StrSQl += " or cast(tarj.NumTarjeta as varchar) ='" + strCliente + "'";
                if (!double.TryParse(strCliente, out Num)) { StrSQl += " OR ltrim(rtrim(replace(cg.apellidos,' ','') + replace(cg.Nombre,' ',''))) LIKE '%" + strCliente.Replace(" ", "").ToString() + "%' OR ltrim(rtrim(replace(cg.Nombre,' ','') + replace(cg.apellidos,' ',''))) LIKE '%" + strCliente.Replace(" ", "").ToString() + "%'"; }
                StrSQl += " ) ORDER BY cg.Apellidos,cg.Nombre ";

                Ds = GEtSQLDataset(StrSQl);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        objc9 = new CLIENTE9();

                        objc9.Id_Cliente = int.Parse(dr["Id_cliente"].ToString());
                        objc9.Cliente = dr["Apellidos"].ToString() + " " + dr["Nombre"].ToString();
                        objc9.Nombre = dr["Nombre"].ToString();
                        objc9.Apellidos = dr["Apellidos"].ToString();
                        objc9.Telefono = dr["Telefono"].ToString();
                        objc9.Movil = dr["TelefonoMovil"].ToString();
                        if (dr["Fecha_Nacimiento"] != null)
                            objc9.FechaNacimiento = dr["Fecha_Nacimiento"].ToString();
                        else
                            objc9.FechaNacimiento = "";
                        objc9.NivelActual = dr["Nivel"].ToString();
                        objc9.Email = ((dr["email"] != DBNull.Value) ? dr["email"].ToString() : String.Empty);
                        StrTarjeta = ((dr["idTarjeta"] != DBNull.Value) ? dr["idTarjeta"].ToString() : String.Empty);
                        numTarjeta = ((dr["NumTarjeta"] != DBNull.Value) ? dr["NumTarjeta"].ToString() : String.Empty);
                        objc9.NumTarjeta = numTarjeta;
                        LstCliente.Add(objc9);

                    }
                }


                return LstCliente;
            }

            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        public List<CLIENTE9> GetClienteNomCliente(String strCliente, DateTime Fecha, Int64 idCarrito, String Tienda)
        {

            try
            {

                String StrSQl;
                DataSet Ds;
                CLIENTE9 objc9;
                List<CLIENTE9> LstCliente;
                double Num;
                String StrTarjeta = String.Empty;
                String numTarjeta = String.Empty;

                LstCliente = new List<CLIENTE9>();


                StrSQl = " SELECT cg.Id_Cliente,cg.Nombre,cg.Apellidos,tarj.NumTarjeta,ct.Tipo_Cliente,tarj.idTarjeta";
                StrSQl += ",ct.Tipo_CE,tarj.IdSocioC9,cg.cif,cg.apellido1 + ' ' + cg.apellido2 AS Apellidos2 ,isnull(tarj.Descripcion,'') as Nivel, cg.email, ct.Tipo_Cliente ,ct.Tipo_CE ";
                StrSQl += " FROM N_CLIENTES_GENERAL cg WITH (NOLOCK)   ";
                StrSQl += " LEFT JOIN (SELECT MAX(IdTarjeta) AS IdTarjeta ";
                StrSQl += " ,IdCliente FROM N_CLIENTES_TARJETAS_FIDELIDAD WITH (NOLOCK) WHERE ISNULL(FechaCaducidad, '31/12/2999') >= ";
                StrSQl += " CONVERT(DATETIME, '" + Fecha.ToShortDateString() + "',103) AND isnull(IdBaja, 0) = 0 	GROUP BY IdCliente ";
                StrSQl += " ) tarj1 ON cg.Id_Cliente = tarj1.IdCliente LEFT JOIN N_CLIENTES_TARJETAS_FIDELIDAD tarj WITH (NOLOCK) ";
                StrSQl += "  ON tarj.IdCliente = cg.Id_Cliente AND tarj.IdTarjeta = tarj1.IdTarjeta LEFT JOIN N_CLIENTES_TIPO ct WITH (NOLOCK) ";
                StrSQl += "  ON cg.Id_Tipo = ct.Id_Tipo WHERE cg.Id_cliente <> 0 AND cg.FECHA_BAJA IS NULL 	AND ( ";
                StrSQl += "  cast (cg.Id_Cliente as varchar) = '" + strCliente + "'";
                StrSQl += " OR cg.Cif LIKE '%" + strCliente + "%'";
                StrSQl += " or cast(tarj.NumTarjeta as varchar) ='" + strCliente + "'";
                if (!double.TryParse(strCliente, out Num)) { StrSQl += " OR ltrim(rtrim(replace(cg.apellidos,' ','') + replace(cg.Nombre,' ',''))) LIKE '%" + strCliente.Replace(" ", "").ToString() + "%' OR ltrim(rtrim(replace(cg.Nombre,' ','') + replace(cg.apellidos,' ',''))) LIKE '%" + strCliente.Replace(" ", "").ToString() + "%'"; }
                StrSQl += " ) ORDER BY cg.Apellidos,cg.Nombre ";

                Ds = GEtSQLDataset(StrSQl);
                //ACL.30-12-2014
                log.Info("Buscando cli" + StrSQl);
                if (Ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {

                        objc9 = new CLIENTE9();

                        objc9.Id_Cliente = int.Parse(dr["Id_cliente"].ToString());
                        objc9.Cliente = dr["Apellidos"].ToString() + " " + dr["Nombre"].ToString();
                        objc9.NivelActual = dr["Nivel"].ToString();
                        objc9.Email = ((dr["email"] != DBNull.Value) ? dr["email"].ToString() : String.Empty);
                        StrTarjeta = ((dr["idTarjeta"] != DBNull.Value) ? dr["idTarjeta"].ToString() : String.Empty);
                        numTarjeta = ((dr["NumTarjeta"] != DBNull.Value) ? dr["NumTarjeta"].ToString() : String.Empty);
                        objc9.NumTarjeta = numTarjeta;


                        if (Ds.Tables[0].Rows.Count == 1)
                        {

                            if (dr["Tipo_Cliente"].ToString() == "empleado" || dr["Tipo_CE"].ToString() == "E")
                            {
                                Empleado objEmpleado;
                                objEmpleado = new Empleado();
                                objEmpleado.EsEmpleado = true;
                                NotasHabilitadas(objc9.Id_Cliente, ref objEmpleado);
                                ComprobarNumNotasEmpleado(Fecha, ref objEmpleado, Tienda, objc9.Id_Cliente);
                                objc9.Empleado_cliente = objEmpleado;

                            }

                        }

                        LstCliente.Add(objc9);
                    }

                    if (Ds.Tables[0].Rows.Count == 1)
                    {
                        //ACL.ESTO BORRA EL NUMERO DE TARJETA CUANDO SE HA PERDIDO LA SESION
                        // if (!numTarjeta.Equals(strCliente)) { StrTarjeta = String.Empty; }
                        log.Info("updatamos el carrito");
                        StrSQl = "Update AVE_CARRITO set id_cliente=" + Ds.Tables[0].Rows[0]["Id_cliente"].ToString();
                        StrSQl += ",TarjetaCliente='" + StrTarjeta + "' where idCarrito=" + idCarrito;
                        ActualizarSQL(StrSQl);
                        log.Info("sql upd carrito " + StrSQl);

                    }
                    else
                    {

                        StrSQl = "Update AVE_CARRITO set id_cliente=0  where idCarrito=" + idCarrito;
                        ActualizarSQL(StrSQl);

                    }

                }
                else
                {

                    StrSQl = "Update AVE_CARRITO set id_cliente=0  where idCarrito=" + idCarrito;
                    ActualizarSQL(StrSQl);

                }

                return LstCliente;
            }

            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }


        }

        private void NotasHabilitadas(Int64 IdCliente, ref Empleado _empleado)
        {
            try
            {

                String StrSQL;
                DataSet Ds;

                StrSQL = "SELECT IdEmpleado,isnull(Num_Abono,0) as Num_Abono FROM EMPLEADOS WITH (NOLOCK)  WHERE Id_Cliente=" + IdCliente;

                Ds = GEtSQLDataset(StrSQL);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        _empleado.NotaEmpleado = (int.Parse(Ds.Tables[0].Rows[0]["Num_Abono"].ToString()) == 0 ? false : true);
                        _empleado.idEmpleado = int.Parse(Ds.Tables[0].Rows[0]["IdEmpleado"].ToString());

                    }

                }

            }

            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        private void ComprobarNumNotasEmpleado(DateTime Fecha, ref Empleado _empleado, String Tienda, Int64 idCliente)
        {
            String Strsql;
            DataSet Ds;
            Strsql = "";

            try
            {

                Strsql = "select  Valor from CONFIGURACIONES_TPV_AVANZADO where NombreCampo ='NumNotaEmpleado' and IdTienda='" + Tienda + "'   ";

                Strsql += " SELECT isnull(SUM(t.Nop),0) FROM ( SELECT DISTINCT COUNT(DISTINCT t.Id_ticket) as NOp FROM N_TICKETS_ESTADOS t WITH (NOLOCK) ";
                Strsql += " INNER JOIN N_TICKETS_FPAGOS fp WITH (NOLOCK) ON t.Id_tienda=fp.Id_Tienda AND t.Id_Ticket=fp.Id_Ticket AND ";
                Strsql += "convert(varchar(10),t.Fecha,103)=convert(varchar(10),fp.Fecha,103) WHERE fp.Fpago='NOTA EMPLEADO' AND fp.Importe>0 AND t.Id_Cliente= " + idCliente.ToString();
                Strsql += " and fp.Fecha between DATEADD(MONTH,-3, CONVERT(datetime,'" + Fecha.ToShortDateString() + "',103))  and CONVERT(datetime,'" + Fecha.ToShortDateString() + " 23:59:59',103)";
                Strsql += "  Union  SELECT DISTINCT COUNT(DISTINCT t.Id_ticket)*-1 FROM N_TICKETS_ESTADOS t WITH (NOLOCK)  INNER JOIN N_TICKETS_FPAGOS fp WITH (NOLOCK) ";
                Strsql += "  ON t.Id_tienda=fp.Id_Tienda AND t.Id_Ticket=fp.Id_Ticket AND  convert(varchar(10),t.Fecha,103)=convert(varchar(10),fp.Fecha,103)  ";
                Strsql += "  WHERE fp.Fpago='NOTA EMPLEADO' AND fp.Importe<0 AND t.Id_Cliente=" + idCliente + " and fp.Fecha between DATEADD(MONTH,-3, ";
                Strsql += " CONVERT(datetime,'" + Fecha.ToShortDateString() + " 23:59:59',103))   and CONVERT(datetime,'" + Fecha.ToShortDateString() + "',103)) t ";

                Ds = GEtSQLDataset(Strsql);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        _empleado.NumNotaempleado = Int16.Parse(Ds.Tables[0].Rows[0][0].ToString());

                    }

                    if (Ds.Tables[1].Rows.Count > 0)
                    {
                        _empleado.NotaEmpleadoGastadas = Int16.Parse(Ds.Tables[1].Rows[0][0].ToString());

                    }
                }
            }

            catch (SqlException sqlEx)
            {
                log.Error(Strsql);
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        #endregion

        #region GetStockSolicitudes

        public bool ComprobarStockSolicitudes(Int64 idPedido, Int64 IdCarrito)
        {
            try
            {
                String StrSql = String.Empty;
                DataSet Ds;
                bool blnStock = true;
                int cantidad;

                StrSql = " select E.*, ave_pedidos.Unidades as CantP from AVE_PEDIDOS inner join Articulos Ar on AVE_PEDIDOS.IdArticulo=Ar.IdArticulo ";
                StrSql += " inner join CABECEROS_DETALLES C on Ar.Id_Cabecero =C.Id_Cabecero and C.Nombre_Talla=AVE_PEDIDOS.Talla ";
                StrSql += " inner join N_EXISTENCIAS E on Ar.IdArticulo=E.IdArticulo and C.Id_Cabecero_Detalle=E.Id_Cabecero_Detalle and ";
                StrSql += " AVE_PEDIDOS.IdTienda=E.IdTienda where IdPedido=" + idPedido.ToString();

                StrSql += " SELECT AR.idArticulo ,Ar.id_Cabecero_Detalle, isnull(SUM(cantidad), 0) as  cantidad, AVE_PEDIDOS.IdTienda ";
                StrSql += " FROM AVE_CARRITO_LINEA AR INNER JOIN AVE_PEDIDOS ON AVE_PEDIDOS.IdArticulo = Ar.IdArticulo ";
                StrSql += " INNER JOIN CABECEROS_DETALLES C ON Ar.Id_cabecero_detalle = C.Id_Cabecero_Detalle AND C.Nombre_Talla = AVE_PEDIDOS.Talla ";
                StrSql += " and AR.idPedido=AVE_PEDIDOS.IdPedido ";
                StrSql += "WHERE id_Carrito = " + IdCarrito.ToString() + " and AVE_PEDIDOS.IdPedido <> " + idPedido.ToString();
                StrSql += " group by IdTienda,AR.idArticulo ,Ar.id_Cabecero_detalle ";




                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    cantidad = int.Parse(Ds.Tables[0].Rows[0]["Cantidad"].ToString()) - int.Parse(Ds.Tables[0].Rows[0]["CantP"].ToString());

                    if (Ds.Tables.Count > 1)
                    {
                        DataView Dv = Ds.Tables[1].DefaultView;
                        Dv.RowFilter = "idArticulo=" + Ds.Tables[0].Rows[0]["IdArticulo"].ToString() + " and id_cabecero_detalle=" + Ds.Tables[0].Rows[0]["id_cabecero_detalle"].ToString() + " and idTienda='" + Ds.Tables[0].Rows[0]["IdTienda"].ToString() + "'";

                        if (Dv.Count > 0) { cantidad -= int.Parse(Dv[0]["Cantidad"].ToString()); }

                    }

                    if (cantidad < 0) { blnStock = false; }
                }
                else
                {
                    blnStock = false;

                }

                return blnStock;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }


        #endregion

        #endregion
        #region RegistrarVenta
        public Int16 ActualizarUpVentas(VENTA objVenta)
        {
            try
            {
                Int64 idauto = 0;
                _Venta = objVenta;

                //_IvaParametro = IVAPRODUCTO(); 

                AbrirConexion(Conex);
                SqlTrans = Conex.BeginTransaction();

                InsertarTicket(ref idauto);

                InsertarTicketDetalle(idauto);

                InsertarTicketEstados();

                ActualizaExistencias();

                InsertarHistorico();

                InsertarTicketFpagos();

                InsertarTicketDescuentos();

                InsertarTRANS();

                SqlTrans.Commit();
                CerrarConexion(Conex);


                return 1;
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }
        public Int16 ActualizaClienteNineDevo(VENTA objVenta)
        {
            Int16 result = 0;
            try
            {

                result = InsertarTicketSaldosNine(objVenta);
                return result;
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                // SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }

        public Int16 ActualizaClienteNine(VENTA objVenta)
        {
            Int16 result = 0;
            try
            {

                result = InsertarTicketSaldosNine();

                return result;
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                // SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }

        }


        protected Int16 InsertarTicket(ref Int64 idAuto)
        {
            try
            {
                SqlCommand SqlTicket;
                SqlParameter objParametro;


                SqlTicket = new SqlCommand();
                SqlTicket.CommandType = CommandType.StoredProcedure;
                SqlTicket.CommandText = "AVE_InsertarTicket";
                SqlTicket.Connection = Conex;
                SqlTicket.Transaction = SqlTrans;

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Ticket";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 20;
                objParametro.Value = _Venta.Id_Ticket;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Tienda";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.Id_Tienda;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Empleado";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = _Venta.Id_Empleado;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Abonado_Empleado";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = _Venta.Id_Abonado_Empleado;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Pago";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = _Venta.Id_Pago;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Fecha";
                objParametro.SqlDbType = SqlDbType.DateTime;
                objParametro.Value = _Venta.Fecha;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@NombreTarjeta";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 50;
                objParametro.Value = _Venta.NombreTarjeta;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@TotalEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.TotalEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@cEfectivoEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.cEfectivoEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@cValeEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.cValeEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@cTarjetaEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.cTarjetaEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@cCuentaEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.cCuentaEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@TotalParaValeEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.TotalParaValeEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@cRecibidoEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.cRecibidoEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@DescuentoEuro";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.DescuentoEuro;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@ComisionVenta";
                objParametro.SqlDbType = SqlDbType.Float;
                objParametro.Value = _Venta.ComisionVenta;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_Cliente_N";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = _Venta.Id_Cliente_N;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@ID_TERMINAL";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.ID_TERMINAL;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Id_SubPago";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.Id_SubPago;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdCajero";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Value = _Venta.IdCajero;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@NumFactura";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 50;
                objParametro.Value = _Venta.NumFactura;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Comentarios";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 100;
                objParametro.Value = _Venta.Comentarios;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@CPostal";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.CodPostal;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdMonedaCliente";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.IdMonedaCliente;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdMonedaTienda";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = _Venta.IdMonedaTienda;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@CambioTienda";
                objParametro.SqlDbType = SqlDbType.Decimal;
                objParametro.Precision = 16;
                objParametro.Scale = 2;
                objParametro.Value = DBNull.Value;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@CIFSociedad";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 50;
                objParametro.Value = _Venta.CIFSociedad;

                SqlTicket.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IDAuto";
                objParametro.SqlDbType = SqlDbType.Int;
                objParametro.Direction = ParameterDirection.Output;

                SqlTicket.Parameters.Add(objParametro);

                SqlTicket.ExecuteNonQuery();

                idAuto = Int64.Parse(SqlTicket.Parameters["@IDAuto"].Value.ToString());


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTicketDetalle(Int64 idauto)
        {
            try
            {
                SqlCommand SqlTicketDetalle;
                SqlParameter objParametro;
                SqlCommand SqlActualizarZapa;
                double dto = 0;
               
                foreach (VENTA_DETALLE D in _Venta.V_DETALLE)
                {
                    dto = 0;
                    if (D.Pvp_Vig > D.ImporteEuros)
                    {
                        dto = Math.Round(100 - ((Convert.ToDouble(D.ImporteEuros) * 100) / (Convert.ToDouble(D.Pvp_Vig))), 2);
                        D.DtoEuroArticulo = dto;
                    }


                    SqlTicketDetalle = new SqlCommand();
                    SqlTicketDetalle.CommandType = CommandType.StoredProcedure;
                    SqlTicketDetalle.CommandText = "AVE_TICKETDETALLE";
                    SqlTicketDetalle.Connection = Conex;
                    SqlTicketDetalle.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Auto";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = idauto;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Articulo";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = D.Id_Articulo;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_cabecero_detalle";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = D.Id_cabecero_detalle;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlTicketDetalle.Parameters.Add(objParametro);


                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ImporteEuros";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.ImporteEuros;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Estado";
                    objParametro.SqlDbType = SqlDbType.SmallInt;
                    objParametro.Value = D.Estado * -1;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@MotivoCambioPrecio";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = D.MotivoCambioPrecio;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@DtoEuroArticulo";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.DtoEuroArticulo;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ComisionPremio";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.ComisionPremio;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdAlmacen";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DBNull.Value;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ImporteBase";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.ImporteBase;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@motivo_devolucion";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = D.motivo_devolucion;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cancelado";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = DBNull.Value;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Comentarios";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = D.Comentarios;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IVA";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = D.IVA;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Pvp_Vig";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.Pvp_Vig;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdPosicion";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = D.IdPosicion;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Asesor";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = D.Asesor;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ComisionAsesor";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = D.ComisionAsesor;

                    SqlTicketDetalle.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdConcesionTienda";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = D.IdConcesionTienda;

                    SqlTicketDetalle.Parameters.Add(objParametro);


                    SqlTicketDetalle.ExecuteNonQuery();

                    string insertDatosEnvio = string.Format(@"INSERT INTO N_TICKETS_OBSERVACIONES(id_ticket,id_posicion,id_tienda,fecha,nombre,Email,Calle,numExt,numInt,Colonia,CP,Estado,Municipio,telefono,Comentarios,tienda)

SELECT '{0}' id_ticket, '{1}' id_posicion, '{2}' id_Tienda, GETDATE() fecha, nombre + ' ' + apellidos nombre, email, direccion, NoExterior, NoInterior, colonia, codigoPostal,  (select nombre from N_PROVINCIAS where id=estado) estado, ciudad, telefonofijo, ReferenciaLlegada,'1' tienda  from AVE_CARRITO_ENTREGAS where idcarrito = (SELECT id_carrito from ave_carrito_linea where id_Carrito_detalle = {3})"
, _Venta.Id_Ticket, D.IdPosicion, _Venta.Id_Tienda, D.id_Carrito_Detalle);

                    string insertDatosEnvio2 = string.Format(@"INSERT INTO N_TICKETS_OBSERVACIONES(id_auto_detalles,nombre,Email,Calle,numExt,numInt,Colonia,CP,Estado,Municipio,telefono,Comentarios,tienda)

SELECT (select isnull(MAX(id_auto_detalles),0)+1 from N_TICKETS_OBSERVACIONES) id_Auto_detalles, nombre + ' ' + apellidos nombre, email, direccion, NoExterior, NoInterior, colonia, codigoPostal,estado, ciudad, telefonofijo, ReferenciaLlegada,'1' tienda  from AVE_CARRITO_ENTREGAS where idcarrito = (SELECT id_carrito from ave_carrito_linea where id_Carrito_detalle = {3})"
, _Venta.Id_Ticket, D.IdPosicion, _Venta.Id_Tienda, D.id_Carrito_Detalle);

                     SqlActualizarZapa = new SqlCommand();
                    SqlActualizarZapa.CommandType = CommandType.Text;
                    SqlActualizarZapa.CommandText = insertDatosEnvio;
                    SqlActualizarZapa.Connection = Conex;
                    SqlActualizarZapa.CommandTimeout = 0;
                    SqlActualizarZapa.Transaction = SqlTrans;
                    SqlActualizarZapa.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTicketEstados()
        {
            try
            {
                SqlCommand SqlTicketEstados;
                SqlParameter objParametro;

                foreach (VENTA_DETALLE DT in _Venta.V_DETALLE)
                {

                    SqlTicketEstados = new SqlCommand();
                    SqlTicketEstados.CommandType = CommandType.StoredProcedure;
                    SqlTicketEstados.CommandText = "WS_InsertarTicketsEstado";
                    SqlTicketEstados.Connection = Conex;
                    SqlTicketEstados.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.ID_TERMINAL;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Ticket";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = _Venta.Id_Ticket;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cliente";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.Id_Cliente_N;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha;


                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Operacion";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = (DT.Estado > 0 ? "VENTA" : "DEVOLUCION");

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumFactura";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = _Venta.NumFactura;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Articulo";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_Articulo;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cabecero_detalle";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_cabecero_detalle;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cantidad";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Estado * -1;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Importe";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.ImporteEuros;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@DtoEuroArticulo";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.DtoEuroArticulo;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda_Venta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal_Venta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);


                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Ticket_Venta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Venta";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Modificacion";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = DateTime.Now;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdEmpleado";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.Id_Empleado;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CIFSociedad";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = _Venta.CIFSociedad;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@MotivoCambioPrecio";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = DT.MotivoCambioPrecio;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Asesor";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = DT.Asesor;

                    SqlTicketEstados.Parameters.Add(objParametro);


                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdConcesionTienda";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.IdConcesionTienda;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@TipoFactura";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 3;
                    objParametro.Value = "AUT";

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ReferenciaP";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@FormaPago";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DBNull.Value;

                    SqlTicketEstados.Parameters.Add(objParametro);

                    SqlTicketEstados.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 ActualizaExistencias()
        {
            try
            {
                SqlCommand SqlActualizaExistencias;
                SqlParameter objParametro;


                foreach (VENTA_DETALLE DT in _Venta.V_DETALLE)
                {

                    SqlActualizaExistencias = new SqlCommand();
                    SqlActualizaExistencias.CommandType = CommandType.StoredProcedure;
                    SqlActualizaExistencias.CommandText = "AVE_ActualizarExistencias";
                    SqlActualizaExistencias.Connection = Conex;
                    SqlActualizaExistencias.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdArticulo";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_Articulo;

                    SqlActualizaExistencias.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdTienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlActualizaExistencias.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cabecero_Detalle";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_cabecero_detalle;

                    SqlActualizaExistencias.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cantidad";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Estado;

                    SqlActualizaExistencias.Parameters.Add(objParametro);


                    SqlActualizaExistencias.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarHistorico()
        {
            try
            {
                SqlCommand SqlHistorico;
                SqlParameter objParametro;


                foreach (VENTA_DETALLE DT in _Venta.V_DETALLE)
                {

                    SqlHistorico = new SqlCommand();
                    SqlHistorico.CommandType = CommandType.StoredProcedure;
                    SqlHistorico.CommandText = "WS_InsertarHistorico";
                    SqlHistorico.Connection = Conex;
                    SqlHistorico.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdArticulo";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_Articulo;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Concepto";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = (DT.Estado > 0 ? "Venta" : "Devolución");

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Origen";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = (DT.Estado > 0 ? _Venta.Id_Tienda : "Cliente");

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Destino";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = (DT.Estado > 0 ? "Cliente" : _Venta.Id_Tienda);

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdEmpleado";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.Id_Empleado;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha.ToShortDateString();

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cabecero_Detalle";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_cabecero_detalle;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cantidad";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Estado * -1;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Numero";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = _Venta.Id_Ticket;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdTienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@sEtiqueta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = "T";

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Modificacion";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha.ToShortDateString();

                    SqlHistorico.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@RefFP";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = DBNull.Value;

                    SqlHistorico.Parameters.Add(objParametro);

                    SqlHistorico.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTicketFpagos()
        {
            try
            {
                SqlCommand SqlTicketPagos;
                SqlParameter objParametro;


                foreach (VENTA_PAGOS DT in _Venta.V_PAGO)
                {

                    SqlTicketPagos = new SqlCommand();
                    SqlTicketPagos.CommandType = CommandType.StoredProcedure;
                    SqlTicketPagos.CommandText = "WS_InsertarTicketsFPagos";
                    SqlTicketPagos.Connection = Conex;
                    SqlTicketPagos.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Ticket";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = _Venta.Id_Ticket;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdOrden";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.IdOrden;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.ID_TERMINAL;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@FPago";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = DT.FPago;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@FPagoDetalle";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = DT.FPagoDetalle;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Tipo";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 25;
                    objParametro.Value = DT.Tipo;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@TipoOrigen";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 15;
                    objParametro.Value = DT.TipoOrigen;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Importe";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.Importe;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Divisa";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 3;
                    objParametro.Value = DT.Divisa;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@OtraDivisa";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 3;
                    objParametro.Value = DT.OtraDivisa;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@OtraDivisaImporte";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.OtraDivisaImporte;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@OtraDivisaCambio";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.OtraDivisaCambio;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjeta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 16;
                    objParametro.Value = DT.NumTarjeta;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjetaAutoriza";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 16;
                    objParametro.Value = DT.NumTarjetaAutoriza;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjetaOperacion";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 16;
                    objParametro.Value = DT.NumTarjetaOperacion;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ValeId";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.ValeId;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ValeTienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = DT.ValeTienda;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Visto_Pago";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = DT.Visto_Pago;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdFP";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.IdFP;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@IdConcesionTienda";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.IdConcesionTienda;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CuotaIVA";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.CuotaIVA;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Empleado";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.Id_Empleado;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NomTitular";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = DT.NomTitular;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NomEntidad";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = DT.NomEntidad;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@FVencimiento";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = DBNull.Value;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@RefFP";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = DBNull.Value;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cliente";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = DT.Id_Cliente;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Modificacion";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha;

                    SqlTicketPagos.Parameters.Add(objParametro);

                    SqlTicketPagos.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTicketSaldosNine()
        {
            try
            {
                SqlCommand SqlTicketSaldos;
                SqlParameter objParametro;


                if (_Venta.CLient9 != null)
                {
                    if (_Venta.CLient9.Cliente == null || _Venta.CLient9.Cliente == "") _Venta.CLient9.Cliente = "-1";
                    AbrirConexion(Conex);

                    SqlTicketSaldos = new SqlCommand();
                    SqlTicketSaldos.CommandType = CommandType.StoredProcedure;
                    SqlTicketSaldos.CommandText = "WS_InsertarTicketsSaldosNine";
                    SqlTicketSaldos.Connection = Conex;


                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Ticket";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = _Venta.Id_Ticket;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.ID_TERMINAL;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cliente";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.CLient9.Id_Cliente;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cliente";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = _Venta.CLient9.Cliente;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjeta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = _Venta.CLient9.NumTarjeta;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@SaldoPuntosAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.SaldoPuntosAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@PuntosRedimidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.PuntosRedimidos;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@PuntosObtenidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.PuntosObtenidos;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@SaldoPuntosAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.SaldoPuntosAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@dblSaldoPares9";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.CLient9.dblSaldoPares9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesAcumuladosAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.ParesAcumuladosAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesRedimidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.ParesRedimidos;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesAcumuladosAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.ParesAcumuladosAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@dblSaldoBolsa5";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.dblSaldoBolsa5;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasAcumuladasAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.BolsasAcumuladasAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasRedimidas";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.BolsasRedimidas;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasAcumuladasAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = _Venta.CLient9.BolsasAcumuladasAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Aniversario";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.CLient9.Aniversario;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cumpleaños";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = _Venta.CLient9.Cumpleaños;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NivelActual";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = _Venta.CLient9.NivelActual;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataShoeLover";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = _Venta.CLient9.CandidataShoeLover;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaPuntos9";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.CLient9.NumConfirmaPuntos9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaPar9";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.CLient9.NumConfirmaPar9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaBolsa5";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.CLient9.NumConfirmaBolsa5;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjetaNew";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = _Venta.CLient9.NumTarjetaNew;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BenC9";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = _Venta.CLient9.BenC9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Modificacion";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = _Venta.Fecha;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataBasico";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = _Venta.CLient9.CandidatoBasico;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataFirstShoeLover";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = _Venta.CLient9.CandidatoFirstShoeLover;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    SqlTicketSaldos.ExecuteNonQuery();


                    CerrarConexion(Conex);
                }

                return 1;

            }
            catch (SqlException sqlEx)
            {

                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }


        protected Int16 InsertarTicketSaldosNine(VENTA objVenta)
        {
            try
            {
                SqlCommand SqlTicketSaldos;
                SqlParameter objParametro;


                if (objVenta.CLient9 != null)
                {
                    if (objVenta.CLient9.Cliente == null || objVenta.CLient9.Cliente == "") objVenta.CLient9.Cliente = "-1";
                    AbrirConexion(Conex);

                    SqlTicketSaldos = new SqlCommand();
                    SqlTicketSaldos.CommandType = CommandType.StoredProcedure;
                    SqlTicketSaldos.CommandText = "WS_InsertarTicketsSaldosNine";
                    SqlTicketSaldos.Connection = Conex;


                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Ticket";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = objVenta.Id_Ticket;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = objVenta.Id_Tienda;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = objVenta.ID_TERMINAL;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = objVenta.Fecha;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Cliente";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = objVenta.CLient9.Id_Cliente;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cliente";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = objVenta.CLient9.Cliente;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjeta";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = objVenta.CLient9.NumTarjeta;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@SaldoPuntosAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.SaldoPuntosAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@PuntosRedimidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = 0;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@PuntosObtenidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.PuntosObtenidos;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@SaldoPuntosAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.SaldoPuntosAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@dblSaldoPares9";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Size = 10;
                    objParametro.Value = objVenta.CLient9.dblSaldoPares9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesAcumuladosAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.ParesAcumuladosAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesRedimidos";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.ParesRedimidos;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@ParesAcumuladosAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.ParesAcumuladosAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@dblSaldoBolsa5";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.dblSaldoBolsa5;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasAcumuladasAnt";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.BolsasAcumuladasAnt;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasRedimidas";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.BolsasRedimidas;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BolsasAcumuladasAct";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = objVenta.CLient9.BolsasAcumuladasAct;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Aniversario";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = objVenta.CLient9.Aniversario;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Cumpleaños";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 10;
                    objParametro.Value = objVenta.CLient9.Cumpleaños;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NivelActual";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = objVenta.CLient9.NivelActual;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataShoeLover";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = objVenta.CLient9.CandidataShoeLover;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaPuntos9";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = objVenta.CLient9.NumConfirmaPuntos9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaPar9";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = objVenta.CLient9.NumConfirmaPar9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumConfirmaBolsa5";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = objVenta.CLient9.NumConfirmaBolsa5;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NumTarjetaNew";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 30;
                    objParametro.Value = objVenta.CLient9.NumTarjetaNew;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@BenC9";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = objVenta.CLient9.BenC9;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha_Modificacion";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = objVenta.Fecha;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataBasico";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = objVenta.CLient9.CandidatoBasico;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@CandidataFirstShoeLover";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 2;
                    objParametro.Value = objVenta.CLient9.CandidatoFirstShoeLover;

                    SqlTicketSaldos.Parameters.Add(objParametro);

                    SqlTicketSaldos.ExecuteNonQuery();
                    SqlTrans.Commit();

                    CerrarConexion(Conex);
                }

                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTicketDescuentos()
        {
            try
            {
                SqlCommand SqlTicketDescuento;
                SqlParameter objParametro;

                if (_Venta.DescuentoEuro > 0)
                {

                    foreach (VENTA_DETALLE DT in _Venta.V_DETALLE)
                    {

                        if (DT.Detalle_Descuento != null)
                        {

                            foreach (VENTA_ARTICULO_DESCUENTO DA in DT.Detalle_Descuento)
                            {

                                SqlTicketDescuento = new SqlCommand();
                                SqlTicketDescuento.CommandType = CommandType.StoredProcedure;
                                SqlTicketDescuento.CommandText = "WS_InsertarTicketsDescuento";
                                SqlTicketDescuento.Connection = Conex;
                                SqlTicketDescuento.Transaction = SqlTrans;

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Id_Ticket";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 20;
                                objParametro.Value = _Venta.Id_Ticket;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@IdPosicion";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DA.IdPosicion;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@IdOrden";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DA.IdOrden;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Id_Articulo";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DT.Id_Articulo;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Id_Cabecero_Detalle";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DT.Id_cabecero_detalle;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Id_Tienda";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 10;
                                objParametro.Value = _Venta.Id_Tienda;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Id_Terminal";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 10;
                                objParametro.Value = _Venta.ID_TERMINAL;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Fecha";
                                objParametro.SqlDbType = SqlDbType.DateTime;
                                objParametro.Value = _Venta.Fecha;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@FPago";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 50;
                                objParametro.Value = DA.FPago;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@FPagoDetalle";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 50;
                                objParametro.Value = DA.FPagoDetalle;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@ImporteBase";
                                objParametro.SqlDbType = SqlDbType.Float;
                                objParametro.Value = DA.ImporteBase;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@ImporteDto";
                                objParametro.SqlDbType = SqlDbType.Float;
                                objParametro.Value = DA.ImporteDto;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@ImporteDtoPor";
                                objParametro.SqlDbType = SqlDbType.Float;
                                objParametro.Value = DA.ImporteDtoPor;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@PromocionId";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DA.PromocionId;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@PromocionName";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 50;
                                objParametro.Value = DA.PromocionName;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@PromocionTarjeta";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 50;
                                objParametro.Value = DA.PromocionTarjeta;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@PromocionPuntos";
                                objParametro.SqlDbType = SqlDbType.Int;
                                objParametro.Value = DA.PromocionPuntos;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Visto_Descuento";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 1;
                                objParametro.Value = DA.Visto_Descuento;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@NumCertificado";
                                objParametro.SqlDbType = SqlDbType.VarChar;
                                objParametro.Size = 100;
                                objParametro.Value = DA.NumCertificado;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@Fecha_Modificacion";
                                objParametro.SqlDbType = SqlDbType.DateTime;
                                objParametro.Value = DateTime.Now;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                objParametro = new SqlParameter();
                                objParametro.ParameterName = "@DevoDTO";
                                objParametro.SqlDbType = SqlDbType.Bit;
                                objParametro.Value = 0;

                                SqlTicketDescuento.Parameters.Add(objParametro);

                                SqlTicketDescuento.ExecuteNonQuery();

                            }

                        }

                    }

                }

                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }

        protected Int16 InsertarTRANS()
        {
            try
            {
                SqlCommand SqlTRANS;
                SqlParameter objParametro;


                foreach (NTRANS DT in _Venta.TRANS)
                {

                    DT.NTicket = _Venta.Id_Ticket;

                    SqlTRANS = new SqlCommand();
                    SqlTRANS.CommandType = CommandType.StoredProcedure;
                    SqlTRANS.CommandText = "AVE_HISTORICOTRANS";
                    SqlTRANS.Connection = Conex;
                    SqlTRANS.Transaction = SqlTrans;

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Tienda";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 50;
                    objParametro.Value = _Venta.Id_Tienda;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Id_Terminal";
                    objParametro.SqlDbType = SqlDbType.Int;
                    objParametro.Value = _Venta.ID_TERMINAL;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Fecha";
                    objParametro.SqlDbType = SqlDbType.DateTime;
                    objParametro.Value = DT.FechaSesion;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Concepto";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 100;
                    objParametro.Value = DT.Concepto;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@NTicket";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 20;
                    objParametro.Value = DT.NTicket;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Importe";
                    objParametro.SqlDbType = SqlDbType.Float;
                    objParametro.Value = DT.Importe;

                    SqlTRANS.Parameters.Add(objParametro);

                    objParametro = new SqlParameter();
                    objParametro.ParameterName = "@Visto_Trans";
                    objParametro.SqlDbType = SqlDbType.VarChar;
                    objParametro.Size = 1;
                    objParametro.Value = 0;

                    SqlTRANS.Parameters.Add(objParametro);



                    SqlTRANS.ExecuteNonQuery();

                }


                return 1;

            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                SqlTrans.Rollback();
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }
        #endregion
        #region Carrito
        public Int64 RegistrarPagoCarritoEfectivo(Carrito_Pago _Pago, bool PagadoOk = true)
        {

            if (PagadoOk)
            {
                try
                {
                    String StrSQl;

                    StrSQl = " if (select count(IdCarrito) from AVE_CARRITO_Pagos where TipoPago='EFECTIVO' and IdCarrito=" + _Pago.IdCarrito;
                    StrSQl += " )=0 begin ";
                    StrSQl += " INSERT INTO [AVE_CARRITO_PAGOS] ([IdCarrito],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe],[PagadoOk]) values (";
                    StrSQl += _Pago.IdCarrito + ",'" + _Pago.TipoPago + "','" + _Pago.TipoPagoDetalle + "','" + _Pago.NumTarjeta + "',";
                    StrSQl += _Pago.Importe.ToString() + ",1) end  ";
                    StrSQl += " else begin ";
                    StrSQl += " UPDATE AVE_CARRITO_PAGOS Set Importe=Importe + " + _Pago.Importe.ToString();
                    StrSQl += " where IdCarrito=" + _Pago.IdCarrito.ToString();
                    StrSQl += " END ";

                    ActualizarSQL(StrSQl);

                    return 1;
                }

                catch (Exception ex)
                {
                    log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    CerrarConexion(Conex);
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                Int64 retval = -1;

                try
                {
                    SqlCommand SqlActualizarZapa;

                    AbrirConexion(Conex);

                    SqlActualizarZapa = new SqlCommand();
                    SqlActualizarZapa.CommandType = CommandType.StoredProcedure;
                    SqlActualizarZapa.CommandText = "AVE_RegistrarPagoCarrito";
                    SqlActualizarZapa.Connection = Conex;
                    SqlActualizarZapa.CommandTimeout = 0;

                    SqlActualizarZapa.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = _Pago.IdCarrito;
                    SqlActualizarZapa.Parameters.Add("@TipoPago", SqlDbType.VarChar).Value = _Pago.TipoPago;
                    SqlActualizarZapa.Parameters.Add("@TipoPagoDetalle", SqlDbType.VarChar).Value = _Pago.TipoPagoDetalle;
                    SqlActualizarZapa.Parameters.Add("@NumTarjeta", SqlDbType.VarChar).Value = _Pago.NumTarjeta;
                    SqlActualizarZapa.Parameters.Add("@Importe", SqlDbType.Float).Value = _Pago.Importe;

                    retval = Convert.ToInt64(SqlActualizarZapa.ExecuteScalar());

                    CerrarConexion(Conex);

                }
                catch (Exception ex)
                {
                    log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw new Exception(ex.Message, ex.InnerException);
                }

                return retval;
            }
        }

        public Int64 RegistrarPagoCarrito(Carrito_Pago _Pago, bool PagadoOk = true)
        {

            if (PagadoOk)
            {
                try
                {
                    String StrSQl;

                    StrSQl = " if (select count(IdCarrito) from AVE_CARRITO_Pagos where TipoPago='NOTA EMPLEADO' and IdCarrito=" + _Pago.IdCarrito;
                    StrSQl += " )=0 begin ";
                    StrSQl += " INSERT INTO [AVE_CARRITO_PAGOS] ([IdCarrito],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe],[PagadoOk]) values (";
                    StrSQl += _Pago.IdCarrito + ",'" + _Pago.TipoPago + "','" + _Pago.TipoPagoDetalle + "','" + _Pago.NumTarjeta + "',";
                    StrSQl += _Pago.Importe.ToString() + ",1) end  ";
                    StrSQl += " else begin ";
                    StrSQl += " UPDATE AVE_CARRITO_PAGOS Set Importe=Importe + " + _Pago.Importe.ToString();
                    StrSQl += " where IdCarrito=" + _Pago.IdCarrito.ToString();
                    StrSQl += " END ";

                    ActualizarSQL(StrSQl);

                    return 1;
                }

                catch (Exception ex)
                {
                    log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    CerrarConexion(Conex);
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                Int64 retval = -1;

                try
                {
                    SqlCommand SqlActualizarZapa;

                    AbrirConexion(Conex);

                    SqlActualizarZapa = new SqlCommand();
                    SqlActualizarZapa.CommandType = CommandType.StoredProcedure;
                    SqlActualizarZapa.CommandText = "AVE_RegistrarPagoCarrito";
                    SqlActualizarZapa.Connection = Conex;
                    SqlActualizarZapa.CommandTimeout = 0;

                    SqlActualizarZapa.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = _Pago.IdCarrito;
                    SqlActualizarZapa.Parameters.Add("@TipoPago", SqlDbType.VarChar).Value = _Pago.TipoPago;
                    SqlActualizarZapa.Parameters.Add("@TipoPagoDetalle", SqlDbType.VarChar).Value = _Pago.TipoPagoDetalle;
                    SqlActualizarZapa.Parameters.Add("@NumTarjeta", SqlDbType.VarChar).Value = _Pago.NumTarjeta;
                    SqlActualizarZapa.Parameters.Add("@Importe", SqlDbType.Float).Value = _Pago.Importe;

                    retval = Convert.ToInt64(SqlActualizarZapa.ExecuteScalar());

                    CerrarConexion(Conex);

                }
                catch (Exception ex)
                {
                    log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw new Exception(ex.Message, ex.InnerException);
                }

                return retval;
            }
        }
        public bool GetArtValido(string idArticulo)
        {
            bool result = true;
            string participa = "";
            try
            {
                DataSet ds;

                String strSQL = "SELECT participa_c9 FROM articulos where IdArticulo = " + idArticulo;

                ds = GEtSQLDataset(strSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow data in ds.Tables[0].Rows)
                    {
                        participa = data["participa_c9"].ToString();
                        if (participa.ToUpper().Trim() == "PARTICIPA") result = true;
                        else result = false;

                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                log.Error("Exception al obtener el total de artículos TR: " + ex.Message.ToString());
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);

            }


        }
        public DataSet GetTotalArtTR(string idCarrito)
        {

            try
            {
                DataSet ds;

                String strSQL = "SELECT sum(PVPORI - DTOArticulo) totalTR FROM AVE_CARRITO_LINEA carr inner join articulos art on art.IdArticulo = carr.IdArticulo ";
                strSQL += " WHERE carr.id_Carrito=" + idCarrito + " and UPPER(art.participa_c9) = 'PARTICIPA'";

                ds = GEtSQLDataset(strSQL);
                return ds;

            }
            catch (Exception ex)
            {
                log.Error("Exception al obtener el total de artículos TR: " + ex.Message.ToString());
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);

            }


        }

        public Int16 ActualizarCarrito(Int64 idCarrito, string sTicket, DateTime FechaSesion)
        {
            try
            {
                String StrSQl;

                StrSQl = "update [AVE_CARRITO] set EstadoCarrito=1 where idCarrito=" + idCarrito.ToString();
                ActualizarSQL(StrSQl);


                // MJM 17/03/2014 INICIO
                StrSQl = "INSERT into AVE_CARRITO_IMPRESION (IdCarrito, idTicket, FechaSesion, Estado, FechaImpresion) " +
                          "VALUES (@IdCarrito, @idTicket, @FechaSesion, @Estado, @FechaImpresion) ";

                SqlCommand cmdImpresion;

                AbrirConexion(Conex);

                cmdImpresion = new SqlCommand();
                cmdImpresion.CommandType = CommandType.Text;
                cmdImpresion.CommandText = "INSERT into AVE_CARRITO_IMPRESION (IdCarrito, idTicket, FechaSesion, Estado, FechaImpresion) " +
                                           "VALUES (@IdCarrito, @idTicket, @FechaSesion, @Estado, @FechaImpresion) ";
                cmdImpresion.Connection = Conex;
                cmdImpresion.CommandTimeout = 0;

                cmdImpresion.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                cmdImpresion.Parameters.Add("@idTicket", SqlDbType.VarChar).Value = sTicket;
                cmdImpresion.Parameters.Add("@FechaSesion", SqlDbType.DateTime).Value = FechaSesion;
                cmdImpresion.Parameters.Add("@Estado", SqlDbType.Bit).Value = 0;
                cmdImpresion.Parameters.Add("@FechaImpresion", SqlDbType.DateTime).Value = DateTime.Now;

                Int64 insertado = Convert.ToInt64(cmdImpresion.ExecuteScalar());

                CerrarConexion(Conex);

                // MJM 17/03/2014 FIN

                return 1;
            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Int16 ActualizarCarrito(Int64 idCarrito, string sTicket, DateTime FechaSesion, decimal gastosEnvio)
        {
            try
            {
                String StrSQl;

                StrSQl = "update [AVE_CARRITO] set EstadoCarrito=1, GastosEnvio = " + gastosEnvio + " where idCarrito=" + idCarrito.ToString();
                ActualizarSQL(StrSQl);


                // MJM 17/03/2014 INICIO
                StrSQl = "INSERT into AVE_CARRITO_IMPRESION (IdCarrito, idTicket, FechaSesion, Estado, FechaImpresion) " +
                          "VALUES (@IdCarrito, @idTicket, @FechaSesion, @Estado, @FechaImpresion) ";

                SqlCommand cmdImpresion;

                AbrirConexion(Conex);

                cmdImpresion = new SqlCommand();
                cmdImpresion.CommandType = CommandType.Text;
                cmdImpresion.CommandText = "INSERT into AVE_CARRITO_IMPRESION (IdCarrito, idTicket, FechaSesion, Estado, FechaImpresion) " +
                                           "VALUES (@IdCarrito, @idTicket, @FechaSesion, @Estado, @FechaImpresion) ";
                cmdImpresion.Connection = Conex;
                cmdImpresion.CommandTimeout = 0;

                cmdImpresion.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                cmdImpresion.Parameters.Add("@idTicket", SqlDbType.VarChar).Value = sTicket;
                cmdImpresion.Parameters.Add("@FechaSesion", SqlDbType.DateTime).Value = FechaSesion;
                cmdImpresion.Parameters.Add("@Estado", SqlDbType.Bit).Value = 0;
                cmdImpresion.Parameters.Add("@FechaImpresion", SqlDbType.DateTime).Value = DateTime.Now;

                Int64 insertado = Convert.ToInt64(cmdImpresion.ExecuteScalar());

                CerrarConexion(Conex);

                // MJM 17/03/2014 FIN

                return 1;
            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }



        public DataSet GetCarritoTicket(string idTicket)
        {
            String StrSql = String.Empty;
            DataSet Ds;
            StrSql = "  SELECT [IdCarritoPago],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe], isnull([Auth],'') as Auth, isnull([Foliocpagos],'') as  Foliocpagos FROM [AVE_CARRITO_PAGOS]  PAG ";
            StrSql += "inner join AVE_CARRITO CARR ON  CARR.IdCarrito= PAG.IdCarrito where CARR.Id_Ticket='" + idTicket + "' and pag.PagadoOk = 1";

            Ds = GEtSQLDataset(StrSql);

            return Ds;
        }

        public DataSet GetCarritoActual(Int64 IdCarrito)
        {
            try
            {
                String StrSql = String.Empty;
                DataSet Ds;


                StrSql = "SELECT  [IdCarrito],  (select  tienda from configuraciones_tpv) as Tienda,[IdCarrito],[FechaCreacion],[id_Cliente],[Usuario],[Maquina],[Fecha_Modificacion],TarjetaCliente";
                StrSql += ",[EstadoCarrito],(select sum(cantidad*pvpAct) from [AVE_CARRITO_LINEA]  where [Id_Carrito]=" + IdCarrito.ToString() + ") as Total FROM [AVE_CARRITO] where [IdCarrito]=" + IdCarrito.ToString();
                StrSql += "  SELECT [id_Carrito_Detalle],ar.[idArticulo],[Id_cabecero_detalle],[Cantidad],[PVPORI] ";
                StrSql += " ,[PVPACT],[DTOArticulo],AVE_CARRITO_LINEA.[idPromocion],[idPedido],PrecioCostoReal,isnull(TipoArticulo,'') as TipoCorte, isnull(promocion,'') as Promocion, Ar.CodigoAlfa CodigoAlfa FROM [AVE_CARRITO_LINEA] ";
                StrSql += " inner join Articulos Ar on AVE_CARRITO_LINEA.idArticulo=Ar.idArticulo ";
                StrSql += " Left Join tbpr_promociones t on  AVE_CARRITO_LINEA.idPromocion=t.idPromocion ";
                StrSql += "  where [Id_Carrito]=" + IdCarrito.ToString();

                StrSql += " SELECT [IdCarritoPago],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe], isnull([Auth],'') as Auth, isnull([Foliocpagos],'') as  Foliocpagos FROM [AVE_CARRITO_PAGOS]  where [IdCarrito]=" + IdCarrito.ToString() + " and PagadoOk = 1";

                Ds = GEtSQLDataset(StrSql);

                return Ds;

            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public DataSet GetCarritoPromo(Int64 IdCarrito)
        {
            try
            {
                String StrSql = String.Empty;
                String StrPromo = String.Empty;
                DataSet Ds;


                StrSql = "SELECT id_Cliente ,[idArticulo],[Id_cabecero_detalle],[Cantidad],[PVPORI],[PVPACT],[DTOArticulo],[idPromocion],id_Carrito_Detalle, ";
                StrSql += "  ( select SUM(PVPACT*cantidad) from AVE_CARRITO_LINEA where id_Carrito=" + IdCarrito.ToString() + ") as Total";
                StrSql += " FROM [AVE_CARRITO_LINEA] L inner join AVE_CARRITO A on L.id_Carrito=A.IdCarrito ";
                StrSql += "  where id_Carrito=" + IdCarrito.ToString() + " order by id_Carrito_Detalle";

                StrPromo = " SELECT AVE_CARRITO_LINEA_PROMO.idPromo, AVE_CARRITO_LINEA_PROMO.DtoPromo, AVE_CARRITO_LINEA_PROMO.DescriPromo, ";
                StrPromo += " AVE_CARRITO_LINEA_PROMO.DescriAmpliaPromo, AVE_CARRITO_LINEA_PROMO.id_linea_Carrito FROM  AVE_CARRITO_LINEA INNER JOIN AVE_CARRITO_LINEA_PROMO ";
                StrPromo += " ON dbo.AVE_CARRITO_LINEA.id_Carrito_Detalle = dbo.AVE_CARRITO_LINEA_PROMO.id_linea_Carrito ";
                StrPromo += " WHERE  AVE_CARRITO_LINEA.id_Carrito = " + IdCarrito.ToString();

                Ds = GEtSQLDataset(StrSql + StrPromo);

                return Ds;

            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }


        public String GetNumTarjeta(int idCliente, DateTime Fecha)
        {
            try
            {
                String Strsql = String.Empty;
                String StrTarjeta = String.Empty;
                DataSet Ds;

                Strsql = "SELECT NumTarjeta  FROM N_CLIENTES_TARJETAS_FIDELIDAD WITH (NOLOCK) WHERE ISNULL(FechaCaducidad, '31/12/2999') >= ";
                Strsql += " CONVERT(DATETIME, '" + Fecha.ToShortDateString() + "',103)  and idcliente=" + idCliente;

                Ds = GEtSQLDataset(Strsql);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        StrTarjeta = Ds.Tables[0].Rows[0][0].ToString();
                    }

                }

                return StrTarjeta;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        public String ActualizaCarritoPromo(List<AVEGestorPromociones.GestorPromociones.Promocion> LstPromo)
        {
            try
            {
                String StrSql = String.Empty;
                String Mensaje = String.Empty;


                foreach (AVEGestorPromociones.GestorPromociones.Promocion objPromo in LstPromo)
                {

                    if (objPromo.MensajeError.Length > 0) { Mensaje += objPromo.MensajeError + "\\r"; }

                    StrSql = "DELETE from AVE_CARRITO_LINEA_PROMO where id_linea_Carrito=" + objPromo.LineaCarritoDetalle;

                    ActualizarSQL(StrSql);

                    if (objPromo.Promo == null)
                    {
                        StrSql = "Update [AVE_CARRITO_LINEA] set DTOArticulo=0 ,idPromocion=0 where id_Carrito_Detalle=" + objPromo.LineaCarritoDetalle;
                        ActualizarSQL(StrSql);


                    }
                    else
                    {
                        if (objPromo.Promo.Count() > 1)
                        {

                            StrSql = "Update [AVE_CARRITO_LINEA] set DTOArticulo=0 ,idPromocion=0 where id_Carrito_Detalle=" + objPromo.LineaCarritoDetalle;
                            ActualizarSQL(StrSql);

                            foreach (AVEGestorPromociones.GestorPromociones.DetallePromo D in objPromo.Promo)
                            {
                                StrSql = "INSERT INTO [AVE_CARRITO_LINEA_PROMO] ([id_linea_Carrito],[idPromo],[DtoPromo] ";
                                StrSql += ",[DescriPromo],[DescriAmpliaPromo])  VALUES ( ";
                                StrSql += objPromo.LineaCarritoDetalle + "," + D.Idpromo + "," + D.DtoPromo + ",";
                                StrSql += "'" + D.DescriPromo + "','" + D.DescriAmpliaPromo + "')";

                                ActualizarSQL(StrSql);

                            }
                        }
                        else
                        {



                            foreach (AVEGestorPromociones.GestorPromociones.DetallePromo D in objPromo.Promo)
                            {
                                double DescuentoEuro = 0;

                                if (D.PromocionSelecionada)
                                {
                                    StrSql = "Update AVE_CARRITO_LINEA set idPromocion=" + D.Idpromo;

                                    DescuentoEuro = Math.Round(objPromo.Pvp_Vig - objPromo.Pvp_Venta, 0);

                                    StrSql += ",  DTOArticulo=" + DescuentoEuro;


                                    StrSql += " where id_Carrito_detalle=" + objPromo.LineaCarritoDetalle;

                                    ActualizarSQL(StrSql);

                                }
                            }
                        }
                    }
                }

                return Mensaje;
            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public DataSet ObtenerPromoLinea(Int64 IdCarrito)
        {
            try
            {
                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "select  * from ( ";
                StrSql += "select id_linea_Carrito,idPromo,DescriPromo  from AVE_CARRITO_LINEA_PROMO P ";
                StrSql += " inner join AVE_CARRITO_LINEA A on P.id_linea_Carrito=A.id_Carrito_Detalle ";
                StrSql += " where id_Carrito=" + IdCarrito.ToString();
                StrSql += " union select id_linea_Carrito,0,'Selecciona Promoción' ";
                StrSql += " from AVE_CARRITO_LINEA_PROMO P inner join AVE_CARRITO_LINEA A on ";
                StrSql += " P.id_linea_Carrito=A.id_Carrito_Detalle where id_Carrito=" + IdCarrito.ToString();
                StrSql += " ) LineaPromo  order by idpromo ";

                Ds = GEtSQLDataset(StrSql);

                return Ds;

            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public bool ComprobarCarritoCerrado(Int64 idCarrito)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;
                bool blnValidado = false;

                log.Error("ComprobarCarritoCerrado");
                StrSql = "select  IdCarrito  from AVE_CARRITO where IdCarrito=" + idCarrito.ToString();
                StrSql += " and (EstadoCarrito=0 or EstadoCarrito= 3)";
                log.Error("query=" + StrSql);
                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    blnValidado = true;
                    log.Error("carrito todavia abierto");
                }
                else
                {
                    log.Error("carrito cerrado");
                }

                return blnValidado;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public double ComprobarLimiteCredito(DateTime Fecha, Int64 idCarrito)
        {
            String StrSQl = String.Empty;
            String StrNota = String.Empty;
            double Importe = 0;
            DataSet Ds;
            DateTime Fini = new DateTime(Fecha.Year, Fecha.Month, 1);
            DateTime Ffin = Fecha.AddDays(1);

            try
            {

                StrSQl = " SELECT isnull(SUM(abs(Importe)*Cantidad*-1),0) as Importe  FROM N_TICKETS_ESTADOS WITH (NOLOCK) WHERE Id_Cliente in ";
                StrSQl += " ( select id_Cliente  from AVE_CARRITO where IdCarrito= " + idCarrito + ")";
                StrSQl += " and Fecha between CONVERT(DATETIME,'" + Fini.ToShortDateString() + "',103)  AND CONVERT(DATETIME,'" + Ffin.ToShortDateString() + "',103) ";

                // recuperamos el importe pagado en la nota de empleado

                StrNota = "  select isnull(Importe,0)  from AVE_CARRITO_PAGOS where IdCarrito=" + idCarrito.ToString();
                StrNota += " and TipoPago='NOTA EMPLEADO' ";


                Ds = GEtSQLDataset(StrSQl + StrNota);

                //recuperamos los tickets anteriores del mes
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Importe += double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }

                // el carrito actual

                if (Ds.Tables[1].Rows.Count > 0)
                {
                    Importe += double.Parse(Ds.Tables[1].Rows[0][0].ToString());
                }

                return Importe;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public double GetLimiteCredito(Int64 idCarrito)
        {
            String Strsql = String.Empty;
            DataSet Ds;
            double LimiteCredito = 0;

            try
            {

                Strsql = " select limitecredito from n_clientes_general WITH (NOLOCK)  where id_cliente in ";
                Strsql += "  ( select id_Cliente  from AVE_CARRITO where IdCarrito= " + idCarrito + ")";

                Ds = GEtSQLDataset(Strsql);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    LimiteCredito = double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }

                return LimiteCredito;

            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        #endregion

        #region Metodos  Genericos

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
        internal DataSet GEtSQLDataset(String StrSQL, SqlTransaction SqlTRans)
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
        internal void ActualizarSQL(string StrSql, SqlTransaction SqlTRans)
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

        #endregion

        #region "Pagos"

        public string DamePlazos(Int64 idCarritoPago)
        {
            string strPlazos = "";

            try
            {




                string strSql = string.Format("select TipoPagoDetalle from AVE_CARRITO_PAGOS where idCarritoPago ={0}", idCarritoPago);

                SqlCommand cmd = new SqlCommand(strSql);
                AbrirConexion(Conex);
                cmd.Connection = Conex;
                strPlazos = Convert.ToString(cmd.ExecuteScalar());
                CerrarConexion(Conex);
                return strPlazos;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public bool ValidarPago(Int64 IdCarritoPago, string foliocpagos, string auth, string cc_number, string tipopagodetalle)
        {
            //Dado que la columna tipoDetalle, ya tiene los plazow y mit nos devuelve el banco
            //antes de que se machaque, se añade el banco + nº de plazos
            string strNumPlazos = "";
            //  string strTipoPagos = tipopagodetalle + "- tarjeta " + cc_number;
            strNumPlazos = DamePlazos(IdCarritoPago);

            tipopagodetalle = tipopagodetalle + " " + strNumPlazos + "-tarjeta " + cc_number; ;
            string StrSql = string.Format("UPDATE AVE_CARRITO_PAGOS SET Auth = '{0}', NumTarjeta= '{1}', Foliocpagos = '{2}', PagadoOK = 1, TipoPagoDetalle='{4}' where idCarritoPago = {3}",
                                          auth, cc_number, foliocpagos, IdCarritoPago, tipopagodetalle);

            //string StrSql = string.Format("UPDATE AVE_CARRITO_PAGOS SET Auth = '{0}', NumTarjeta= '{1}', Foliocpagos = '{2}', PagadoOK = 1, TipoPagoDetalle='{4}', TipoPago='{5}' where idCarritoPago = {3}",
            //                                auth, cc_number, foliocpagos, IdCarritoPago, tipopagodetalle, strTipoPagos);

            try
            {
                ActualizarSQL(StrSql);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public bool LogRespuestaPago(string XML, string metodo, string idTienda, string IdTerminal, string IdEmpleado, int IdCarritoPago)
        {
            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                string StrSql = string.Format("INSERT INTO [AVE_RESPUESTA_PAGO] " +
               "([Fecha],[XML],[Metodo],[IdTienda],[IdTerminal],[IdEmpleado],[IdCarritoPago]) VALUES" +
               "(GETDATE(),'{0}','{1}','{2}','{3}','{4}',{5})", XML, metodo, idTienda, IdTerminal, IdEmpleado, IdCarritoPago);

                sCmd = new SqlCommand();
                sCmd.CommandType = CommandType.Text;
                sCmd.CommandText = StrSql;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;
                sCmd.ExecuteNonQuery();

                CerrarConexion(Conex);

                return true;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #region "Carrito"

        public int AniadeCarrito(string strEmpleado, string IdCliente, string Maquina)
        {
            SqlCommand sql;
            SqlParameter parameter;
            SqlParameter returnParameter;
            int idCarrito = -1;

            sql = new SqlCommand();
            sql.CommandType = CommandType.StoredProcedure;
            sql.CommandText = "AVE_InsertaCarrito";

            try
            {

                AbrirConexion(Conex);
                sql.Connection = Conex;


                returnParameter = CrearParametro("@ReturnVal", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                sql.Parameters.Add(returnParameter);

                parameter = CrearParametro("@Usuario", SqlDbType.NVarChar, 256, strEmpleado, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@IdCliente", SqlDbType.Int, 0, IdCliente, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Maquina", SqlDbType.NVarChar, 250, Maquina, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@EstadoCarrito", SqlDbType.Int, 0, 0, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                sql.ExecuteNonQuery();

                if (returnParameter.Value == null || returnParameter.Value == DBNull.Value || Convert.ToInt32(returnParameter.Value) <= 0)
                    throw new Exception(string.Format("No se ha podido crear el carrito."));

                idCarrito = Convert.ToInt32(returnParameter.Value);




            }
            catch (Exception ex)
            { }

            return idCarrito;

        }
        public int AniadeArticuloCarrito(int idCarrito, string idReferencia, string IdTienda, string IdEmpleado, ref string strError)
        {
            int result = 0;
            strError = "";
            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);
                String SP = "AVE_AniadeArticuloCarrito";

                sCmd = new SqlCommand();
                // SqlParameter param = new SqlParameter("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@Referencia", idReferencia);
                sCmd.Parameters.AddWithValue("@IdTienda", IdTienda);
                sCmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                sCmd.Parameters.AddWithValue("@StrError", strError).Direction = ParameterDirection.InputOutput;
                sCmd.Parameters["@strError"].Size = 2000;

                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.CommandText = SP;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;

                int numrows = sCmd.ExecuteNonQuery();
                strError = sCmd.Parameters["@strError"].Value.ToString();

                CerrarConexion(Conex);

                result = numrows;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            return result;

        }
        public bool CancelaPagoCarritoBD(string idCarrito)
        {
            bool result = true;
            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                String SP = "AVE_EliminaPagoCarritoPendiente";

                sCmd = new SqlCommand();
                // SqlParameter param = new SqlParameter("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@idCarrito", idCarrito);


                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.CommandText = SP;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;

                sCmd.ExecuteNonQuery();

                CerrarConexion(Conex);

                return result;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        public bool EliminaCarritoBD(string idCarrito, string IdEmpleado, string Maquina)
        {
            bool result = true;
            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                String SP = "AVE_CancelaCarritoPendiente";

                sCmd = new SqlCommand();
                // SqlParameter param = new SqlParameter("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@idUsuario", IdEmpleado);
                sCmd.Parameters.AddWithValue("@idMaquina", Maquina);

                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.CommandText = SP;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;

                sCmd.ExecuteNonQuery();

                CerrarConexion(Conex);

                return result;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public bool EliminaCarritoBD(string idCarrito)
        {
            bool result = true;
            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                String SP = "AVE_EliminaCarritoWS";

                sCmd = new SqlCommand();
                // SqlParameter param = new SqlParameter("@idCarrito", idCarrito);
                sCmd.Parameters.AddWithValue("@idCarrito", idCarrito);

                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.CommandText = SP;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;

                sCmd.ExecuteNonQuery();

                CerrarConexion(Conex);

                return result;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public int TotalLineasCarrito(int idCarrito)
        {
            int iRetVal = 0;

            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                string StrSql = string.Format("select count(*) from ave_carrito_linea where id_Carrito = {0}", idCarrito);

                sCmd = new SqlCommand();
                sCmd.CommandType = CommandType.Text;
                sCmd.CommandText = StrSql;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;
                iRetVal = (int)sCmd.ExecuteScalar();

                CerrarConexion(Conex);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //throw new Exception(ex.Message, ex.InnerException);
                iRetVal = 0;
            }

            return iRetVal;

        }

        public ENTREGA_CARRITO ObtenerEntregaCarrito(int idCarrito, int id_carrito_detalle=0)
        {
            ENTREGA_CARRITO entrega = new ENTREGA_CARRITO();

            try
            {
                string filtro = string.Empty;
                if (id_carrito_detalle!=0)
                {
                    filtro = " = "+id_carrito_detalle;
                }
                else
                {
                    filtro = "is null";
                }
                string szSQL = string.Format("Select * from AVE_CARRITO_ENTREGAS WHERE IDCARRITO = {0}  and id_Carrito_detalle {1}", idCarrito,filtro);
                DataSet ds = GEtSQLDataset(szSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    entrega.idCarrito = idCarrito;
                    entrega.idCarritoEntrega = (int)dr["idCarritoEntrega"];
                    entrega.Nombre = dr["Nombre"].ToString();
                    entrega.Apellidos = dr["Apellidos"].ToString();
                    entrega.Email = dr["Email"].ToString();
                    entrega.Direccion = dr["Direccion"].ToString();
                    entrega.NoInterior = dr["NoInterior"].ToString();
                    entrega.NoExterior = dr["NoExterior"].ToString();
                    entrega.Estado = dr["Estado"].ToString();
                    entrega.Ciudad = dr["Ciudad"].ToString();
                    entrega.Colonia = dr["Colonia"].ToString();
                    entrega.CodPostal = dr["CodigoPostal"].ToString();
                    entrega.TelfMovil = dr["TelefonoCelular"].ToString();
                    entrega.TelfFijo = dr["TelefonoFijo"].ToString();
                    entrega.Referencia = dr["ReferenciaLlegada"].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                int i = 0;
                i++;
            }


            return entrega;
        }

        public FACTURACION_CARRITO ObtenerFacturacionCarrito(int idCarrito)
        {
            FACTURACION_CARRITO facturacion = new FACTURACION_CARRITO();

            try
            {
                string szSQL = string.Format("Select * from AVE_CARRITO_FACTURACION WHERE IDCARRITO = {0}", idCarrito);
                DataSet ds = GEtSQLDataset(szSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    facturacion.idCarritoFacturacion = Convert.ToInt32(dr["IdCarritoFacturacion"].ToString());
                    facturacion.idCarrito = idCarrito;
                    facturacion.Nombre = dr["Nombre"].ToString();
                    facturacion.Rfc = dr["Rfc"].ToString();
                    facturacion.Direccion = dr["Direccion"].ToString();
                    facturacion.NoInterior = dr["NoInterior"].ToString();
                    facturacion.NoExterior = dr["NoExterior"].ToString();
                    facturacion.Estado = dr["Estado"].ToString();
                    facturacion.Ciudad = dr["Ciudad"].ToString();
                    facturacion.Colonia = dr["Colonia"].ToString();
                    facturacion.CodPostal = dr["CodigoPostal"].ToString();
                    facturacion.TelfMovil = dr["TelefonoCelular"].ToString();
                    facturacion.TelfFijo = dr["TelefonoFijo"].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                int i = 0;
                i++;
            }


            return facturacion;
        }

        public ENTREGA_CARRITO ObtenerEntregaCliente(int idCliente)
        {
            ENTREGA_CARRITO entrega = new ENTREGA_CARRITO();

            try
            {
                string szSQL = string.Format("Select * from AVE_CARRITO_ENTREGAS WHERE IdCliente = {0}  and id_Carrito_detalle is  null", idCliente);
                DataSet ds = GEtSQLDataset(szSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    entrega.IdCliente = idCliente;
                    entrega.idCarritoEntrega = (int)dr["idCarritoEntrega"];
                    entrega.Nombre = dr["Nombre"].ToString();
                    entrega.Apellidos = dr["Apellidos"].ToString();
                    entrega.Email = dr["Email"].ToString();
                    entrega.Direccion = dr["Direccion"].ToString();
                    entrega.NoInterior = dr["NoInterior"].ToString();
                    entrega.NoExterior = dr["NoExterior"].ToString();
                    entrega.Estado = dr["Estado"].ToString();
                    entrega.Ciudad = dr["Ciudad"].ToString();
                    entrega.Colonia = dr["Colonia"].ToString();
                    entrega.CodPostal = dr["CodigoPostal"].ToString();
                    entrega.TelfMovil = dr["TelefonoCelular"].ToString();
                    entrega.TelfFijo = dr["TelefonoFijo"].ToString();
                    entrega.Referencia = dr["ReferenciaLlegada"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                int i = 0;
                i++;
            }


            return entrega;
        }

        public bool HayEntregaEnCarrito(int idCarrito)
        {
            bool bRet = false;
            string szSQL = string.Format("select count(1) from ave_carrito_entregas where idcarrito = {0}  and id_Carrito_detalle is  null", idCarrito);

            try
            {
                // datasets sin tipo...
                DataSet Ds = GEtSQLDataset(szSQL);

                bRet = ((int)Ds.Tables[0].Rows[0][0] > 0);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        public bool HayEntregaEnCarritoFacturacion(int idCarrito)
        {
            bool bRet = false;
            string szSQL = string.Format("select count(1) from ave_carrito_facturacion where idcarrito = {0}", idCarrito);

            try
            {
                // datasets sin tipo...
                DataSet Ds = GEtSQLDataset(szSQL);

                bRet = ((int)Ds.Tables[0].Rows[0][0] > 0);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        public bool ActualizaCarritoLinea(string idCarrito, string IdTicket)
        {
            bool bRet = true;
            string strSQLU = "";
            string strSQL = "select Id_Auto_Detalles,Idposicion, Id_Articulo,Id_cabecero_detalle from N_TICKETS_DETALLES DET ";
            strSQL += " inner join N_TICKETS  TI on ti.Id_Auto= det.Id_Auto ";
            strSQL += "where ti.Id_Ticket='" + IdTicket + "' order by Idposicion";

            try
            {
                DataSet Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {

                        strSQLU = "update AVE_CARRITO_LINEA SET Id_Auto_Detalles= " + row["Id_Auto_Detalles"].ToString();
                        strSQLU += " where id_carrito_detalle in (SELECT F.id_carrito_detalle FROM  (SELECT id_carrito_detalle,id_carrito,IdArticulo,id_cabecero_detalle,ROW_NUMBER() OVER (ORDER BY id_carrito_detalle) AS Posicion";
                        strSQLU += " FROM [AVE_CARRITO_LINEA] where id_carrito = " + idCarrito + " ) f";
                        strSQLU += " WHERE f.posicion=" + row["Idposicion"].ToString() + " and f.IdArticulo =" + row["Id_Articulo"];
                        strSQLU += " and f.id_cabecero_detalle=" + row["Id_cabecero_detalle"].ToString() + " AND f.id_Carrito =" + idCarrito + ")";

                        ActualizarSQL(strSQLU);
                        log.Info("SQL actualiza ave_carrito_linea:" + strSQLU);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }
            return bRet;
        }
        public bool AniadeTicket(string idCarrito, string IdTicket)
        {
            bool bRet = true;
            string strSQL = "UPDATE AVE_CARRITO " +
               "SET Id_Ticket = '{1}'" +
               " WHERE " + "IdCarrito = {0}";

            try
            {
                string szSQL = string.Empty;
                szSQL = string.Format(strSQL, idCarrito, IdTicket);
                ActualizarSQL(szSQL);
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }
            return bRet;
        }

        public bool deleteDireccionArt(ENTREGA_CARRITO entrega)
        {
            bool bRet = true;

            string deleteCarritoDir = string.Format("delete AVE_CARRITO_ENTREGAS where id_Carrito_Detalle={0} and IdCarrito='{1}'", entrega.id_Carrito_Detalle, entrega.idCarrito);

            try
            {
                ActualizarSQL(deleteCarritoDir);
              
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        public bool GuardarDireccionArticulo(ENTREGA_CARRITO entrega)
        {
            bool bRet = true;

            string deleteCarritoDir = string.Format("delete AVE_CARRITO_ENTREGAS where id_Carrito_Detalle={0} and IdCarrito='{1}'", entrega.id_Carrito_Detalle, entrega.idCarrito);

            string szInsertEntrega =string.Format( @"INSERT INTO AVE_CARRITO_ENTREGAS 
                (IdCarrito, Nombre, Apellidos, Email, Direccion, NoInterior, NoExterior ,Estado ,Ciudad ,Colonia,
                CodigoPostal ,TelefonoCelular ,TelefonoFijo, ReferenciaLlegada, idCliente,id_Carrito_Detalle)
                VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}',{15})", entrega.idCarrito, entrega.Nombre, entrega.Apellidos, entrega.Email,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo, entrega.Referencia, entrega.IdCliente,entrega.id_Carrito_Detalle);
            try
            {
                ActualizarSQL(deleteCarritoDir);
                ActualizarSQL(szInsertEntrega);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        
        public bool ActualizarEntrega(ENTREGA_CARRITO entrega)
        {
            bool bRet = true;
            string szInsertEntrega = "INSERT INTO AVE_CARRITO_ENTREGAS " +
                "(IdCarrito, Nombre, Apellidos, Email, Direccion, NoInterior, NoExterior ,Estado ,Ciudad ,Colonia," +
                "CodigoPostal ,TelefonoCelular ,TelefonoFijo, ReferenciaLlegada, idCliente)" +
                "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}')";

            string szUpdateEntrega = "UPDATE AVE_CARRITO_ENTREGAS " +
                "SET Nombre = '{1}', Apellidos = '{2}', Email = '{3}'" +
                ",Direccion = '{4}', NoInterior = '{5}' ,NoExterior = '{6}'" +
                ",Estado = '{7}', Ciudad = '{8}' ,Colonia = '{9}', CodigoPostal = '{10}'" +
                ",TelefonoCelular = '{11}', TelefonoFijo = '{12}', ReferenciaLlegada = '{13}', idCliente = '{14}' " +
                "WHERE " +
                "IdCarrito = {0} and id_Carrito_detalle is null";

            try
            {
                string szSQL = string.Empty;
                if (HayEntregaEnCarrito(entrega.idCarrito)) // Si hay entrega actualizar.
                {
                    // UPDATE
                    szSQL = string.Format(szUpdateEntrega, entrega.idCarrito, entrega.Nombre, entrega.Apellidos, entrega.Email,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo, entrega.Referencia, entrega.IdCliente, entrega.idCarritoEntrega);
                }
                else
                {
                    // INSERT
                    szSQL = string.Format(szInsertEntrega, entrega.idCarrito, entrega.Nombre, entrega.Apellidos, entrega.Email,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo, entrega.Referencia, entrega.IdCliente);
                }

                ActualizarSQL(szSQL);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        public bool ActualizarEntregaTienda(ENTREGA_CARRITO entrega)
        {
            bool bRet = true;
            string szInsertEntrega = "INSERT INTO AVE_CARRITO_ENTREGAS " +
                "(IdCarrito, Nombre, Apellidos, Email, Direccion, NoInterior, NoExterior ,Estado ,Ciudad ,Colonia," +
                "CodigoPostal ,TelefonoCelular ,TelefonoFijo, ReferenciaLlegada,IdTiendaEnvio)" +
                "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}')";

            string szUpdateEntrega = "UPDATE AVE_CARRITO_ENTREGAS " +
                "SET Nombre = '{1}', Apellidos = '{2}', Email = '{3}'" +
                ",Direccion = '{4}', NoInterior = '{5}' ,NoExterior = '{6}'" +
                ",Estado = '{7}', Ciudad = '{8}' ,Colonia = '{9}', CodigoPostal = '{10}'" +
                ",TelefonoCelular = '{11}', TelefonoFijo = '{12}', ReferenciaLlegada = '{13}', IdTiendaEnvio = '{14}' " +
                "WHERE " +
                "IdCarrito = {0}  and id_Carrito_detalle is null";

            try
            {
                string szSQL = string.Empty;
                if (HayEntregaEnCarrito(entrega.idCarrito)) // Si hay entrega actualizar.
                {
                    // UPDATE
                    szSQL = string.Format(szUpdateEntrega, entrega.idCarrito, entrega.Nombre, entrega.Apellidos, entrega.Email,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo, entrega.Referencia, entrega.tiendaEnvio, entrega.idCarritoEntrega);
                }
                else
                {
                    // INSERT
                    szSQL = string.Format(szInsertEntrega, entrega.idCarrito, entrega.Nombre, entrega.Apellidos, entrega.Email,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo, entrega.Referencia, entrega.tiendaEnvio);
                }

                ActualizarSQL(szSQL);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }

        public bool ActualizarEntregaFacturacion(FACTURACION_CARRITO entrega)
        {
            bool bRet = true;
            string szInsertEntrega = "INSERT INTO AVE_CARRITO_FACTURACION " +
                "(IdCarrito, Nombre, Rfc, Direccion, NoInterior, NoExterior ,Estado ,Ciudad ,Colonia," +
                "CodigoPostal ,TelefonoCelular ,TelefonoFijo)" +
                "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')";

            string szUpdateEntrega = "UPDATE AVE_CARRITO_FACTURACION " +
                "SET Nombre = '{1}', Rfc = '{2}'" +
                ",Direccion = '{3}', NoInterior = '{4}' ,NoExterior = '{5}'" +
                ",Estado = '{6}', Ciudad = '{7}' ,Colonia = '{8}', CodigoPostal = '{9}'" +
                ",TelefonoCelular = '{10}', TelefonoFijo = '{11}'" +
                "WHERE " +
                "IdCarrito = {0}";

            try
            {
                string szSQL = string.Empty;
                if (HayEntregaEnCarritoFacturacion(entrega.idCarrito)) // Si hay entrega actualizar.
                {
                    // UPDATE
                    szSQL = string.Format(szUpdateEntrega, entrega.idCarrito, entrega.Nombre, entrega.Rfc,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo);
                }
                else
                {
                    // INSERT
                    szSQL = string.Format(szInsertEntrega, entrega.idCarrito, entrega.Nombre, entrega.Rfc,
                        entrega.Direccion, entrega.NoInterior, entrega.NoExterior, entrega.Estado, entrega.Ciudad,
                        entrega.Colonia, entrega.CodPostal, entrega.TelfMovil, entrega.TelfFijo);
                }

                ActualizarSQL(szSQL);

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRet = false;
            }

            return bRet;
        }


        protected internal DataSet GetPagoCarrito(Int64 IdCarrito)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "select  Rtrim(TipoPago + ' ' + TipoPagoDetalle) as Tipo,Importe from AVE_CARRITO_PAGOS ";
                StrSql += " where IdCarrito=" + IdCarrito + " and PagadoOK = 1 order by IdCarritoPago ";

                Ds = GEtSQLDataset(StrSql);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        public DataSet DatosRFC(string idRFC)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "SELECT CIF as RFC, [Nombre] ,[Direccion], [Portal],[Puerta]" +
                    ",[Poblacion],[Provincia],[Barriada],[CodPostal],Telefono,Movil " +
                " FROM [N_CLIENTES_FACTURACION] where CIF = '" + idRFC + "'";

                Ds = GEtSQLDataset(StrSql);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int EliminaArticulo(string idArticulo, string idCarrito, SqlTransaction transaction)
        {
            bool flag = false;
            int num = 0;
            int num2 = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT IdPedido FROM AVE_CARRITO_LINEA WHERE ID_CARRITO =" + idCarrito + " AND IDARTICULO=" + idArticulo);
                if (transaction == null)
                {
                    this.AbrirConexion(this.Conex);
                    command.Connection = this.Conex;
                }
                else
                {
                    command.Connection = this.Conex;
                    command.Transaction = transaction;
                }
                num2 = Convert.ToInt32(command.ExecuteScalar());
                if (num2 > 0)
                {
                    string strSql = string.Concat(new object[] { "DELETE FROM AVE_CARRITO_LINEA WHERE IDPEDIDO=", num2, "AND ID_CARRITO= ", idCarrito, "AND IDARTICULO= ", idArticulo });
                    this.ActualizarSQL(strSql, transaction);
                    strSql = "DELETE FROM AVE_PEDIDOS WHERE IDPEDIDO=" + num2;
                    this.ActualizarSQL(strSql, transaction);
                    num = 1;
                }
            }
            catch (Exception exception)
            {
                log.Error("exception al borrar el pedido" + exception.Message.ToString());
            }
            finally
            {
                if (flag)
                {
                    this.CerrarConexion(this.Conex);
                }
            }
            return num;
        }

        public int ValidarNuevo(string idCarrito, string idArticulo, SqlTransaction transaction)
        {
            DataSet set = new DataSet();
            int num = 0;
            try
            {
                string strSQL = "SELECT count(*) contador form ave_carrito_linea where id_carrito =" + idCarrito + " and  IDARTICULO=" + idArticulo;
                set = this.GEtSQLDataset(strSQL, transaction, 1);
                if (set.Tables.Count <= 0)
                {
                    return num;
                }
                if (set.Tables[0].Rows.Count <= 0)
                {
                    return num;
                }
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    num = Convert.ToInt32(row["contador"].ToString());
                }
            }
            catch (Exception)
            {
            }
            return num;
        }

        public bool ValidaGastosEnvioCarrito(int idCarrito, string codigoAlfa)
        {
            string cmdText = "SELECT IdArticulo FROM ARTICULOS WHERE CodigoAlfa = '" + codigoAlfa + "'";
            bool flag = false;
            DataSet set = new DataSet();
            int num = 0;
            try
            {
                SqlCommand command = new SqlCommand(cmdText);
                this.AbrirConexion(this.Conex);
                command.Connection = this.Conex;
                num = Convert.ToInt32(command.ExecuteScalar());
                this.CerrarConexion(this.Conex);
                cmdText = string.Concat(new object[] { "SELECT COUNT(*) Contador FROM AVE_CARRITO_LINEA WHERE ID_CARRITO=", idCarrito, " and IDARTICULO= ", num });
                set = this.GEtSQLDataset(cmdText);
                if (set.Tables.Count <= 0)
                {
                    return flag;
                }
                if (set.Tables[0].Rows.Count <= 0)
                {
                    return flag;
                }
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if (row["Contador"].ToString() == "0")
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return flag;
        }
        public void getDatosEnvio(string idArticulo, ref float precioOriginal, ref float precioActualizado, SqlTransaction transaction)
        {
            DataSet set = new DataSet();
            string strSQL = "SELECT PrecioVentaEuro FROM ARTICULOS WHERE IDARTICULO=" + idArticulo;
            set = this.GEtSQLDataset(strSQL, transaction, 1);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    precioOriginal = float.Parse(row["PrecioVentaEuro"].ToString());
                    precioActualizado = precioOriginal;
                }
            }
        }

        internal DataSet GEtSQLDataset(string StrSQL, SqlTransaction SqlTRans, int opcion)
        {
            DataSet set2;
            try
            {
                SqlCommand selectCommand = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = StrSQL,
                    Connection = this.Conex,
                    Transaction = SqlTRans,
                    CommandTimeout = 0
                };
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                set2 = dataSet;
            }
            catch (SqlException exception)
            {
                log.Error(MethodBase.GetCurrentMethod().Name, exception);
                throw new Exception(exception.Message, exception.InnerException);
            }
            return set2;
        }


        protected internal int GetPagoEfectivo()
        {

            try
            {

                String StrSql = String.Empty;
                int iNumPagoEfective = 0;

                StrSql = "select ISNULL(Valor,0) from CONFIGURACIONES_TPV_AVANZADO where upper(NombreCampo)='RecibirEfectivoZGM'";

                SqlCommand cmd = new SqlCommand(StrSql);
                AbrirConexion(Conex);
                cmd.Connection = Conex;
                iNumPagoEfective = Convert.ToInt32(cmd.ExecuteScalar());
                CerrarConexion(Conex);
                return iNumPagoEfective;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }


        }
        protected internal int GetArticulosCarrito(string IdCarrito)
        {

            try
            {

                String StrSql = String.Empty;
                int iNumArticulos = 0;

                StrSql = "select COUNT(*) from AVE_CARRITO_LINEA where id_Carrito=" + IdCarrito;

                SqlCommand cmd = new SqlCommand(StrSql);
                AbrirConexion(Conex);
                cmd.Connection = Conex;
                iNumArticulos = Convert.ToInt32(cmd.ExecuteScalar());
                CerrarConexion(Conex);
                return iNumArticulos;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        protected internal DataSet GetClienteCarrito(Int64 IdCarrito)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "select  id_Cliente,isnull(TarjetaCliente,'') as idTarjeta from AVE_CARRITO ";
                StrSql += " where IdCarrito=" + IdCarrito.ToString();

                Ds = GEtSQLDataset(StrSql);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public DataSet GetClienteCarritoFacturacion(int idCliente)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "select TOP 1 CIF,Nombre,Direccion,Barriada as Colonia, Portal as NoExterior, Puerta as NoInterior,CodPostal,Provincia,Poblacion,Pais, Movil from N_CLIENTES_FACTURACION  ";
                StrSql += " where Id_Cliente=" + idCliente.ToString() + " ORDER BY FechaModificacion DESC";

                Ds = GEtSQLDataset(StrSql);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public DataSet GetClienteGeneral(int idCliente)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = "select TOP 1 Nombre,Apellidos,email,Direccion,Provincia,Poblacion, CodPostal, Telefono, Telefono2 from N_CLIENTES_GENERAL  ";
                StrSql += " where id_Cliente=" + idCliente.ToString() + " ORDER BY Fecha_Modificacion DESC";

                Ds = GEtSQLDataset(StrSql);

                return Ds;
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public void InsertarLogCarrito(int idCarrito, string xmlEnvio, string xmlRespuesta, int resultado)
        {

            SqlCommand sql;
            SqlParameter parameter;
            SqlConnection myConnection;
            try
            {
                sql = new SqlCommand();
                myConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());

                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_InsertarLogCarrito";
                sql.Connection = myConnection;

                sql.Connection.Open();

                parameter = CrearParametro("@Idcarrito", SqlDbType.Int, 0, idCarrito, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@XmlEnvioHermes", SqlDbType.VarChar, -1, xmlEnvio, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@XmlRespuestaHermes", SqlDbType.VarChar, -1, xmlRespuesta, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Resultado", SqlDbType.Int, 0, resultado, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                sql.ExecuteNonQuery();

                sql.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarCarritoPedidoHermes(int idCarrito, string IdPedidoHermes)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());
            string sql = "UPDATE AVE_CARRITO SET Fecha_Modificacion = GETDATE(), IdPedidoHermes = '" + IdPedidoHermes + "'  WHERE IdCarrito = " + idCarrito;
            try
            {

                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                myCommand.ExecuteScalar();

                myConnection.Close();
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                myConnection.Close();
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        #endregion

        #region "TPV"

        public bool HaySesionActivaTPV()
        {
            // 
            bool bRetVal = false;

            try
            {
                SqlCommand sCmd;

                AbrirConexion(Conex);

                string StrSql = string.Format("select isnull(count(*),0) from configuraciones_tpv WHERE Sesion =1");

                sCmd = new SqlCommand();
                sCmd.CommandType = CommandType.Text;
                sCmd.CommandText = StrSql;
                sCmd.Connection = Conex;
                sCmd.CommandTimeout = 0;
                int count = (int)sCmd.ExecuteScalar();

                bRetVal = (count == 1);

                CerrarConexion(Conex);
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                bRetVal = false;
            }

            return bRetVal;
        }


        #region "Stock en Tienda / Otras tiendas"

        public int StockArticuloEnTienda(string idArticulo, string idTienda)
        {
            return StockEnTienda(idArticulo, idTienda, false);
        }

        public int StockArticuloOtrasTiendas(string idArticulo, string idTiendaAExcluir)
        {

            return StockEnTienda(idArticulo, idTiendaAExcluir, true);
        }

        private int StockEnTienda(string idArticulo, string idTienda, bool bMostrarOtrasTiendas)
        {

            int iRet = 0;

            try
            {

                string sSigno = (bMostrarOtrasTiendas ? "<>" : "=");
                string szSQL = string.Format("select ISNULL(sum(cantidad),0) from n_existencias AS Stock " +
                                             "where idarticulo = {0} and idtienda {1} '{2}'", idArticulo, sSigno, idTienda);

                DataSet ds = GEtSQLDataset(szSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    iRet = (int)ds.Tables[0].Rows[0][0];
                }

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                iRet = 0;
            }

            return iRet;
        }

        #endregion

        #endregion

        #region "Metodos Datos Ws Online"
        protected internal DataSet GeDataSetUpVentas(string StridTicket, string StrTienda, DateTime FechaSesion)
        {


            try
            {
                SqlCommand SqlDatosVentas;
                SqlDataAdapter Adaptador;
                DataSet Ds = new DataSet();

                AbrirConexion(Conex);

                SqlDatosVentas = new SqlCommand();
                SqlDatosVentas.CommandType = CommandType.StoredProcedure;
                SqlDatosVentas.CommandText = "WS_UpVentasOnline";
                SqlDatosVentas.Connection = Conex;

                SqlParameter objParametro = new SqlParameter();
                objParametro.ParameterName = "@StrIdTicket";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 20;
                objParametro.Value = StridTicket;

                SqlDatosVentas.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@IdTienda";
                objParametro.SqlDbType = SqlDbType.VarChar;
                objParametro.Size = 10;
                objParametro.Value = StrTienda;

                SqlDatosVentas.Parameters.Add(objParametro);

                objParametro = new SqlParameter();
                objParametro.ParameterName = "@Fecha";
                objParametro.SqlDbType = SqlDbType.DateTime;
                objParametro.Value = FechaSesion;

                SqlDatosVentas.Parameters.Add(objParametro);

                Adaptador = new SqlDataAdapter(SqlDatosVentas);

                Adaptador.Fill(Ds);

                CerrarConexion(Conex);

                return Ds;

            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        protected internal void InsertarHistoricoWs(String Tienda, DateTime FechaSesion, String Metodo, String Accion, String Key, String SubKey, String DirFTP, Int16 StrREsult)
        {
            try
            {

                SqlCommand SqlInsHistoricoWSOnline;

                AbrirConexion(Conex);

                SqlInsHistoricoWSOnline = new SqlCommand();
                SqlInsHistoricoWSOnline.CommandType = CommandType.StoredProcedure;
                SqlInsHistoricoWSOnline.CommandText = "WS_RegistrarHistorico_Online";
                SqlInsHistoricoWSOnline.Connection = Conex;

                SqlParameter objParametros = new SqlParameter();
                objParametros.ParameterName = "@IdTienda";
                objParametros.SqlDbType = SqlDbType.VarChar;
                objParametros.Size = 10;
                objParametros.Value = Tienda;

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                objParametros = new SqlParameter();
                objParametros.ParameterName = "@FSesion";
                objParametros.SqlDbType = SqlDbType.DateTime;
                objParametros.Value = FechaSesion;

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                objParametros = new SqlParameter();
                objParametros.ParameterName = "@Metodo";
                objParametros.SqlDbType = SqlDbType.VarChar;
                objParametros.Size = 50;
                objParametros.Value = Metodo;

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                objParametros = new SqlParameter();
                objParametros.ParameterName = "@Entrada";
                objParametros.SqlDbType = SqlDbType.VarChar;
                objParametros.Size = 3000;
                objParametros.Value = Metodo + "|" + Accion + "|" + Key + "|" + SubKey + "|" + FechaSesion + "|" + Tienda + "|" + DirFTP;

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                objParametros = new SqlParameter();
                objParametros.ParameterName = "@Estado";
                objParametros.SqlDbType = SqlDbType.VarChar;
                objParametros.Size = 1;
                objParametros.Value = StrREsult.ToString();

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                objParametros = new SqlParameter();
                objParametros.ParameterName = "@IdKey";
                objParametros.SqlDbType = SqlDbType.VarChar;
                objParametros.Size = 20;
                objParametros.Value = Key;

                SqlInsHistoricoWSOnline.Parameters.Add(objParametros);

                SqlInsHistoricoWSOnline.ExecuteNonQuery();

                CerrarConexion(Conex);

            }

            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        protected internal void GetConfOnline(String Tienda, ref String DIRTP, ref String Online)
        {
            try
            {

                String StrSql = String.Empty;
                DataSet Ds;

                StrSql = " select (select DIRFTP from parametrosGenerales) as DIRFTP,(select ";
                StrSql += " valor from CONFIGURACIONES_TPV_AVANZADO  where nombrecampo='OnLineWs' ";
                StrSql += "  and idtienda='" + Tienda + "') as Online ";

                Ds = GEtSQLDataset(StrSql);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DIRTP = Ds.Tables[0].Rows[0]["DIRFTP"].ToString();
                    Online = Ds.Tables[0].Rows[0]["Online"].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region "Metodos CarritoAVE_WS"

        /// <summary>
        /// Inserta un pedido en la tabla AVE_PEDIDOS
        /// </summary>
        /// <param name="idArticulo">Identificador del articulo</param>
        /// <param name="talla">Talla del articulo</param>
        /// <param name="precio">Precio del articulo</param>
        /// <param name="idTienda">Identificador de la tienda</param>
        /// <param name="stock">stock disponible para esa tienda</param>
        /// <param name="transaction">la transaccion de la operacion</param>
        /// <returns>Identificador del pedido creado</returns>
        public int InsertarPedido(int idArticulo, string talla, SqlTransaction transaction)
        {

            //Por defecto, unidades vale 1
            int unidades = 1;

            //Usuario, IdTerminal e IdEmpleado se pasan nulos al procedimiento,
            //ya que hasta que no se logueen los usuarios de HERMES EN AVE no disponemos de esos valores.
            try
            {
                int numeroPedido = 0;
                SqlCommand sql;
                SqlParameter parameter;
                SqlParameter returnParameter;

                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_PedidosCrear";

                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    sql.Connection = Conex;
                }
                else
                {
                    sql.Connection = Conex;
                    sql.Transaction = transaction;
                }



                returnParameter = CrearParametro("@ReturnVal", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                sql.Parameters.Add(returnParameter);

                parameter = CrearParametro("@IdArticulo", SqlDbType.Int, 0, idArticulo, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Talla", SqlDbType.VarChar, 6, talla, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Unidades", SqlDbType.SmallInt, 0, unidades, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Precio", SqlDbType.Money, 0, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@IdTienda", SqlDbType.VarChar, 10, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@IdEmpleado", SqlDbType.Int, 0, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Usuario", SqlDbType.NVarChar, 256, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Stock", SqlDbType.Int, 0, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@IdTerminal", SqlDbType.SmallInt, 0, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);


                sql.ExecuteNonQuery();
                if (returnParameter.Value == null || returnParameter.Value == DBNull.Value || Convert.ToInt32(returnParameter.Value) <= 0)
                    throw new Exception(string.Format("No se ha podido crear el pedido. Parametros: {0},{1},{2},{3},{4},{5}", idArticulo, talla));

                numeroPedido = Convert.ToInt32(returnParameter.Value);

                return numeroPedido;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (transaction == null)
                    CerrarConexion(Conex);
            }
        }

        /// <summary>
        /// Crea un carrito nuevo, Inserta en la tabla AVE_CARRITO
        /// </summary>
        /// <param name="transaction">Transaccion de la operacion</param>
        /// <returns>Identificador del carrito</returns>
        public int InsertarCarrito(SqlTransaction transaction, string idMaquina)
        {
            int idCarrito = 0;
            int idCliente = 0;


            try
            {
                SqlCommand sql;
                SqlParameter parameter;
                SqlParameter returnParameter;

                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_InsertaCarrito";

                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    sql.Connection = Conex;
                }
                else
                {
                    sql.Connection = Conex;
                    sql.Transaction = transaction;
                }

                returnParameter = CrearParametro("@ReturnVal", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                sql.Parameters.Add(returnParameter);

                parameter = CrearParametro("@Usuario", SqlDbType.NVarChar, 256, DBNull.Value, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@IdCliente", SqlDbType.Int, 0, idCliente, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@Maquina", SqlDbType.NVarChar, 250, idMaquina, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                parameter = CrearParametro("@EstadoCarrito", SqlDbType.Int, 0, 0, ParameterDirection.Input);
                sql.Parameters.Add(parameter);

                sql.ExecuteNonQuery();

                if (returnParameter.Value == null || returnParameter.Value == DBNull.Value || Convert.ToInt32(returnParameter.Value) <= 0)
                    throw new Exception(string.Format("No se ha podido crear el carrito."));

                idCarrito = Convert.ToInt32(returnParameter.Value);

                return idCarrito;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Inserta una linea en el carrito. Se inserta en la tabla AVE_CARRITO_LINEA
        /// </summary>
        /// <param name="idArticulo">Identificador del articulo</param>
        /// <param name="idCarrito">Identificador del carrito</param>
        /// <param name="idPedido">Identificador del pedido</param>
        /// <param name="talla">Talla del articulo</param>
        /// <param name="cantidad">unidades del articulo</param>
        /// <param name="transaction">Transaccion de la operacion</param>
        /// <returns>Duelve el identificador de la linea del carrito introducida</returns>
        public int InsertarLineaCarrito(int idArticulo, int idCarrito, int idPedido, string talla, float precioOriginal, float precioActualizado, SqlTransaction transaction)
        {

            SqlCommand sql;
            SqlParameter parametro;
            SqlParameter returnParameter;
            int idLineaCarrito = 0;

            try
            {
                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_GuardaDetalleCarritoHermes";
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    sql.Connection = Conex;
                }
                else
                {

                    sql.Connection = Conex;
                    sql.Transaction = transaction;
                }

                returnParameter = CrearParametro("@ReturnVal", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                sql.Parameters.Add(returnParameter);

                parametro = CrearParametro("@IdArticulo", SqlDbType.Int, 0, idArticulo, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@IdCarrito", SqlDbType.Int, 0, idCarrito, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@IdPedido", SqlDbType.Int, 0, idPedido, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@Talla", SqlDbType.NVarChar, 250, talla, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@Cantidad", SqlDbType.Int, 0, 1, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@pvpORI", SqlDbType.Float, 0, precioOriginal, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@pvpACT", SqlDbType.Float, 0, precioActualizado, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                sql.ExecuteNonQuery();

                if (returnParameter.Value == null || returnParameter.Value == DBNull.Value || Convert.ToInt32(returnParameter.Value) <= 0)
                    throw new Exception(string.Format("No se ha podido crear el carrito.Parametros: {0},{1},{2},{3},{4}", idArticulo, idCarrito, idPedido, talla));

                idLineaCarrito = Convert.ToInt32(returnParameter.Value);

                return idLineaCarrito;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

            }
        }

        public void InsertarStockTiendaArticulo(int idLineaCarrito, int idCarrito, string tienda, int stock, SqlTransaction transaction)
        {

            SqlCommand sql;
            SqlParameter parametro;

            try
            {
                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_InsertarStockTiendaArticulo";
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    sql.Connection = Conex;
                }
                else
                {

                    sql.Connection = Conex;
                    sql.Transaction = transaction;
                }

                parametro = CrearParametro("@IdLineaCarrito", SqlDbType.Int, 0, idLineaCarrito, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@IdCarrito", SqlDbType.Int, 0, idCarrito, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@Tienda", SqlDbType.VarChar, 10, tienda, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@stock", SqlDbType.Int, 0, stock, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                sql.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Dado que cuando se introduzca el cliente, ya se han añadido promociones al carrito
        // se borra de cada línea de carrito y se aplica el descuento de directivo
        public int aplicaDtoDirectivo(int idCarrito, double dto)
        {
            int result = 0;
            try
            {
                if (dto > 0)
                {
                    string strSQL = "update AVE_CARRITO_LINEA SET DTOArticulo=((PVPORI*100)/" + dto + "), IdPromocion=0 where id_Carrito = " + idCarrito;
                    ActualizarSQL(strSQL);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return result;

        }
        //acl, PARA CLIENTES DIRECTIVOS APLICAR PORCENTAJE DESCUENTO
        public double validarDtoCliente(int idCliente, string idTienda)
        {
            double porcentaje = 0;

            try
            {
                string sql = "SELECT ISNULL(Descuento,0) FROM N_CLIENTES_TIENDAS WITH (NOLOCK) WHERE Id_Tienda='" + idTienda + "' AND Id_Cliente=" + idCliente;

                SqlCommand cmd = new SqlCommand(sql);

                AbrirConexion(Conex);
                cmd.Connection = Conex;

                porcentaje = Convert.ToDouble(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                CerrarConexion(Conex);
            }
            return porcentaje;

        }
        /// <summary>
        /// Comprueba si existe o no un carrito para su posterior insercción o eliminación
        /// </summary>
        /// <param name="idCarrito">Identificador del carrito</param>
        /// <param name="transaction">Transaccion de la operacion</param>
        /// <returns>true/false dependiendo si existe o no el carrito</returns>
        public void ValidarCarrito(int idCarrito, SqlTransaction transaction)
        {
            bool closeConection = false;
            bool existe;


            try
            {
                string sql = "SELECT COUNT(*) IdCarrito FROM AVE_CARRITO WHERE IdCarrito = '" + idCarrito + "'";

                SqlCommand cmd = new SqlCommand(sql);
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    cmd.Connection = Conex;
                }
                else
                {
                    cmd.Connection = Conex;
                    cmd.Transaction = transaction;
                }

                existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;

                if (!existe)
                {
                    throw new Exception(string.Format("No existe el carrito {0}", idCarrito));
                }

                sql = "SELECT estadoCarrito FROM AVE_CARRITO WHERE IdCarrito = '" + idCarrito + "'";
                cmd = new SqlCommand(sql);
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    cmd.Connection = Conex;

                }
                else
                {
                    cmd.Connection = Conex;
                    cmd.Transaction = transaction;

                }

                /// Si el carrito está pagado devolvemos una excepcion.
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 1 || Convert.ToInt32(cmd.ExecuteScalar()) == 2)
                    throw new Exception(string.Format("Carrito {0} pagado/cancelado. No se pueden añadir nuevas lineas.", idCarrito));


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (closeConection)
                    CerrarConexion(Conex);
            }
        }

        /// <summary>
        /// Elimina un carrito, las lineas de ese carrito y el pedido de cada linea de carrito
        /// </summary>
        /// <param name="idCarrito">identificador de carrito</param>
        /// <param name="transaction">Transacción de la operación</param>
        /// <returns>true si se ha podido eliminar el carrito, y si no, la excepción</returns>
        public bool EliminaCarrito(int idCarrito, SqlTransaction transaction)
        {
            SqlCommand sql;
            SqlParameter parametro;
            bool closeConection = false;
            int eliminado;

            try
            {
                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_EliminaCarritoWS";
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    sql.Connection = Conex;
                }
                else
                {
                    sql.Connection = Conex;
                    sql.Transaction = transaction;
                }


                parametro = CrearParametro("@IdCarrito", SqlDbType.Int, 0, idCarrito, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                eliminado = sql.ExecuteNonQuery();

                if (eliminado > 0)
                {
                    transaction.Commit();
                    return true;

                }
                else
                {
                    throw new Exception(string.Format("No se ha podido eliminar el carrito {0}}", idCarrito));
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (closeConection)
                    CerrarConexion(Conex);
            }

        }

        /// <summary>
        /// Comprueba el valor
        /// </summary>
        /// <param name="Valor">Valor para updatar el valor UsoCliente9</param>
        /// <returns>0/1
        public int TiendaCamper(int valor)
        {
            int result = 0;
            string valCliente = "";
            string sql = "select Isnull(Valor,'')Valor from CONFIGURACIONES_TPV_AVANZADO WHERE NombreCampo='UsoCliente9' and IdTienda =(Select Tienda from configuraciones_tpv)";
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                AbrirConexion(Conex);
                cmd.Connection = Conex;
                valCliente = Convert.ToString(cmd.ExecuteScalar());
                if (valCliente != "")
                {
                    //Solo updatamos si el valor que tiene es diferente que el que nos envia hermes
                    if (Convert.ToInt32(valCliente) != valor)
                    {

                        sql = "Update CONFIGURACIONES_TPV_AVANZADO SET Valor='" + valor + "' where  NombreCampo='UsoCliente9'  and IdTienda =(Select Tienda from configuraciones_tpv)";
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteNonQuery();
                        if (result > 0) result = 1;
                    }
                    else result = 1;
                }
                else
                {

                    sql = "declare @Tienda varchar(50) ";
                    sql += "Select @Tienda=Tienda from configuraciones_tpv ";
                    sql += "insert into CONFIGURACIONES_TPV_AVANZADO (IdTienda,NombreCampo,TipoCampo,Valor, Fecha_Modificacion) ";
                    sql += "values(@Tienda,'UsoCliente9','varchar(1)','" + valor + "',GETDATE())";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    result = cmd.ExecuteNonQuery();
                    if (result > 0) result = 1;
                }

            }
            catch (Exception ex)
            {
                log.Error("Error al insertar/updatar UsoCliente9." + ex.Message.ToString());

            }
            finally
            {

                CerrarConexion(Conex);
            }
            return result;
        }

        /// <summary>
        /// Inserta una devolucion en bd
        /// </summary>
        /// <param name="IdTicket">Identificador del TICKET</param>
        /// <param name="IdTienda">Identificador de la Tienda</param>
        /// <param name="IdArticulo">Identificador del articulo</param>
        /// <param name="strTalla">Nombre de la talla</param>
        /// <returns>ok./NOK + EXCEPCION
        public int InsertaDevolucion(string IdTicket, string orden, string IdTienda, int IdArticulo, string strTalla, ref string resultado)
        {
            SqlCommand sql;
            SqlParameter parametro;
            int posicion = Convert.ToInt32(orden);

            try
            {
                sql = new SqlCommand();
                sql.CommandType = CommandType.StoredProcedure;
                sql.CommandText = "AVE_InsertaDevolucion";
                AbrirConexion(Conex);
                sql.Connection = Conex;


                parametro = CrearParametro("@IdTicket", SqlDbType.VarChar, 20, IdTicket, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@posicion", SqlDbType.Int, 0, posicion, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@IdTienda", SqlDbType.VarChar, 10, IdTienda, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@IdArticulo", SqlDbType.Int, 0, IdArticulo, ParameterDirection.Input);
                sql.Parameters.Add(parametro);

                parametro = CrearParametro("@Talla", SqlDbType.VarChar, 30, strTalla, ParameterDirection.Input);
                sql.Parameters.Add(parametro);


                parametro = CrearParametro("@Respuesta", SqlDbType.VarChar, 2048, resultado, ParameterDirection.Output);
                sql.Parameters.Add(parametro);

                int resul = sql.ExecuteNonQuery();
                resultado = sql.Parameters["@Respuesta"].Value.ToString();
                return 1;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

            }
            finally
            {
                CerrarConexion(Conex);
            }

        }


        /// <summary>
        /// Crea un parametro para añadirlo a la llamada del procedimiento almacenado
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        /// <param name="DataTypeBd">Tipo de datos sql</param>
        /// <param name="size">tamaño del parametro</param>
        /// <param name="value">valor del parametro</param>
        /// <param name="parameterDirection">Direccion del parametro (input,output,returnvalue...)</param>
        /// <returns></returns>
        private SqlParameter CrearParametro(string parameterName, SqlDbType DataTypeBd, int size, object value, ParameterDirection parameterDirection)
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
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Obtiene el identificador del articulo partiendo de su referencia
        /// </summary>
        /// <param name="referencia">Referencia del articulo</param>
        /// <param name="transaction">Transacción de la operación</param>
        /// <returns>Identificador del articulo</returns>
        public int GetIdArticulo(string referencia, SqlTransaction transaction)
        {
            bool closeConection = false;
            int idArticulo = 0;

            try
            {
                string sql = "SELECT IdArticulo FROM ARTICULOS WHERE CodigoAlfa = '" + referencia + "'";

                SqlCommand cmd = new SqlCommand(sql);
                if (transaction == null)
                {
                    AbrirConexion(Conex);
                    cmd.Connection = Conex;
                }
                else
                {
                    cmd.Connection = Conex;
                    cmd.Transaction = transaction;
                }

                idArticulo = Convert.ToInt32(cmd.ExecuteScalar());

                return idArticulo;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (closeConection)
                    CerrarConexion(Conex);
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


    }
}