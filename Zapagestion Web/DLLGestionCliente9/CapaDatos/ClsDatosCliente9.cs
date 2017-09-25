using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DLLGestionCliente9.Models;


namespace DLLGestionCliente9.CapaDatos
{
    public partial class ClsCapaDatos9
    {
        public int UpdateHistoricoErr309(string strMetodoWS, string strEntrada, string Tienda) {
            int result = 0;
            string StrSQl = "";
            StrSQl = "UPDATE WS_HISTORICO SET Estado='0' WHERE Metodo='" + strMetodoWS + "' ";
            StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + strEntrada + "' AND Estado='1'";
            try{
                ActualizarSQL(StrSQl);
                result = 1;
            }
            catch(SqlException ex){
                log.Error("Exception UpdateHistoricoErr309"+ ex.Message.ToString());
            }
            return result;
        
        }
        public Int16 Save_Hist_HistoricoWS(long lngEmpleado, string strMetodoWS, string strEntrada, string StrSalida, string strEstado, long intError, string strTicket, string Tienda, DateTime FechaSesion)
        {
            try
            {
                string StrSQl;
                long intReintentos = 0;
                bool blnMetodoReintento = false;
                string strObs = "";

                switch (strMetodoWS)
                {
                    case "CONFIRMAOPERACION":
                    case "ENVIATICKET":
                    case "ALTASOCIO":

                        //if (strEstado == "0" && intError==0)
                        //{
                        //    StrSQl = "UPDATE WS_HISTORICO SET Estado='0' WHERE Metodo='" + strMetodoWS + "' ";
                        //    StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + strEntrada + "' AND Estado='1'";
                        //    ActualizarSQL(StrSQl);
                        //    intReintentos = 1;
                        //}
                        break;
                    case "ACTUALIZASOCIO":
                        // if (strEstado == "0" && intError==0)
                        //{
                        //    StrSQl = "UPDATE WS_HISTORICO SET Estado='0' WHERE Metodo='" + strMetodoWS + "' ";
                        //    StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + strEntrada + "' AND Estado='1'";
                        //    ActualizarSQL(StrSQl);
                        //    intReintentos = 1;
                        //}
                        break;
                    case "SOLICITACAMBIOTARJETA":
                    case "CONFIRMACIONCAMBIOTARJETA":
                        blnMetodoReintento = true;
                        StrSQl = "SELECT COUNT(IdHistorico) FROM WS_HISTORICO WITH (NOLOCK)  WHERE Metodo='" + strMetodoWS + "' ";
                        StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + (strEntrada) + "' AND Salida='" + (StrSalida) + "'";
                        intReintentos = GetCountTable(StrSQl);
                        if ((intReintentos) > 0)
                        {
                            StrSQl = "UPDATE WS_HISTORICO SET Estado='0' WHERE Metodo='" + strMetodoWS + "' ";
                            StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + strEntrada + "' ";
                            StrSQl += " AND Estado='1' AND Salida='" + (StrSalida) + "'";
                            ActualizarSQL(StrSQl);
                        }
                        break;
                    default:
                        break;
                }



                if (blnMetodoReintento)
                {
                    switch (intError)
                    {
                        case 0:
                            //*** 10 reintentos
                            if (intReintentos >= 9) { strEstado = "0"; }
                            break;
                        case 200:
                        case 201:
                        case 202:
                        case 203:
                        case 331:
                        case 400:
                            //'*** 5 reintentos
                            if (intReintentos >= 4) { strEstado = "0"; }
                            break;
                        default:
                            //'*** sin reintentos
                            strEstado = "0";
                            break;
                    }
                }

                strObs = "";
                if (intError != -1)
                {
                    strObs = "(" + intError + ") - " + GetNameError("SELECT Descripcion FROM WS_ERRORES_NET WITH (NOLOCK)  WHERE IdError=" + intError);
                }


                if (intReintentos == 0)
                {
                    StrSQl = "INSERT INTO WS_HISTORICO(IdTienda,FSesion,IdEmpleado,Metodo,Entrada,Salida,Estado,Observaciones,FechaModificacion,IdTicket) VALUES(";
                    StrSQl += "'" + Tienda + "',CONVERT(DATETIME,'" + FechaSesion.ToShortDateString() + "',103)," + lngEmpleado + ",";
                    StrSQl += "'" + strMetodoWS + "','" + strEntrada + "','" + StrSalida + "','" + strEstado + "','" + strObs + "',";
                    StrSQl += "Getdate(),'" + strTicket + "')";

                    ActualizarSQL(StrSQl);
                }


                return 1;
            }

            catch (Exception ex)
            {
                log.Error(ex);
                CerrarConexion(Conex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


       

        #region "Operaciones BD Datos Cliente 9"
        public List<Cliente9> GetDatCliente(String strCliente, DateTime Fecha)
        {
            try
            {

                String StrSQl;
                DataSet Ds;
                Cliente9 objc9;
                List<Cliente9> LstCliente;
                double Num;
                String StrTarjeta = String.Empty;
                String numTarjeta = String.Empty;

                LstCliente = new List<Cliente9>();


                StrSQl = " SELECT cg.Id_Cliente,cg.Nombre,cg.Apellidos,tarj.NumTarjeta,ct.Tipo_Cliente,tarj.idTarjeta";
                StrSQl += ",ct.Tipo_CE,tarj.IdSocioC9,cg.cif,cg.apellido1 + ' ' + cg.apellido2 AS Apellidos2 ,isnull(tarj.Descripcion,'') as Nivel, cg.email, ct.Tipo_Cliente ,ct.Tipo_CE,isnull(cg.Telefono,'') Telefono, isnull(cg.TelefonoMovil,'') TelefonoMovil, convert(datetime,cg.Fecha_Nacimiento,103) Fecha_Nacimiento ";
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
                        objc9 = new Cliente9();

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
                        objc9.esEmpleado = dr["Tipo_Cliente"].ToString().ToUpper() == "EMPLEADO" ? "1" : "0";
                        LstCliente.Add(objc9);

                    }
                }


                return LstCliente;
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
            finally {
                CerrarConexion(Conex);
            }

        }
        public List<Cliente9Gral> GetDatClienteActualizacion(String strCliente, DateTime Fecha)
        {
            try
            {

                String StrSQl;
                DataSet Ds;
                Cliente9Gral objc9;
                Cliente9 cli9;
                Cliente9Extent extCli9;
                List<Cliente9Gral> LstCliente;
                double Num;
                String StrTarjeta = String.Empty;
                String numTarjeta = String.Empty;

                LstCliente = new List<Cliente9Gral>();


                StrSQl = " SELECT cg.Id_Cliente,cg.Nombre,cg.Apellidos,tarj.NumTarjeta,ct.Tipo_Cliente,tarj.idTarjeta";
                StrSQl += ",ct.Tipo_CE,tarj.IdSocioC9,cg.cif,cg.apellido1 + ' ' + cg.apellido2 AS Apellidos2 ,isnull(tarj.Descripcion,'') as Nivel, cg.email, ct.Tipo_Cliente ,ct.Tipo_CE,isnull(cg.Telefono,'') Telefono, isnull(cg.TelefonoMovil,'') TelefonoMovil, convert(datetime,cg.Fecha_Nacimiento,103) Fecha_Nacimiento ";
                StrSQl += ",isnull(Direccion,'')Direccion,isnull(cg.CodPostal,'')CodPostal,isnull(cg.Poblacion,'')Poblacion,isnull(cg.Provincia,'')Provincia ";
                StrSQl += ",isnull(cg.Telefono2,'')Telefono2,isnull(cg.Fax,'') Fax,isnull(cg.Talla,'')Talla,isnull(cg.Contacto,'') Contacto ";
                StrSQl += ",isnull(cg.Observaciones,'')Observaciones,isnull(cg.Pais,'')Pais,isnull(cg.Cargo,'') Cargo,isnull(cg.TelefonoMovil,'')TelefonoMovil";
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
                        objc9 = new Cliente9Gral();
                        cli9 = new Cliente9();
                        extCli9 = new Cliente9Extent();
                        objc9.c9 = cli9;
                        objc9.c9Extent = extCli9;
                        objc9.c9.Id_Cliente = int.Parse(dr["Id_cliente"].ToString());
                        objc9.c9.Cliente = dr["Apellidos"].ToString() + " " + dr["Nombre"].ToString();
                        objc9.c9.Nombre = dr["Nombre"].ToString();
                        objc9.c9.Apellidos = dr["Apellidos"].ToString();
                        objc9.c9.Telefono = dr["Telefono"].ToString();
                        objc9.c9.Movil = dr["TelefonoMovil"].ToString();
                        if (dr["Fecha_Nacimiento"] != null)
                            objc9.c9.FechaNacimiento = dr["Fecha_Nacimiento"].ToString();
                        else
                            objc9.c9.FechaNacimiento = "";
                        objc9.c9.NivelActual = dr["Nivel"].ToString();
                        objc9.c9.Email = ((dr["email"] != DBNull.Value) ? dr["email"].ToString() : String.Empty);
                        StrTarjeta = ((dr["idTarjeta"] != DBNull.Value) ? dr["idTarjeta"].ToString() : String.Empty);
                        numTarjeta = ((dr["NumTarjeta"] != DBNull.Value) ? dr["NumTarjeta"].ToString() : String.Empty);
                        objc9.c9.NumTarjeta = numTarjeta;
                        objc9.c9.esEmpleado = dr["Tipo_Cliente"].ToString().ToUpper() == "EMPLEADO" ? "1" : "0";
                        objc9.c9Extent.Cargo = dr["Cargo"].ToString();
                        objc9.c9Extent.Estado = dr["Provincia"].ToString();
                        objc9.c9Extent.Poblacion = dr["Poblacion"].ToString();
                        objc9.c9Extent.Comentarios = dr["Observaciones"].ToString();
                        objc9.c9Extent.Contacto = dr["Contacto"].ToString();
                        objc9.c9Extent.CP = dr["CodPostal"].ToString();
                        objc9.c9Extent.Direccion = dr["Direccion"].ToString();
                        objc9.c9Extent.Fax = dr["Fax"].ToString();
                        objc9.c9Extent.Pais = dr["Pais"].ToString();
                        objc9.c9Extent.Talla = dr["Talla"].ToString();
                        objc9.c9Extent.TfnoTrabajo = dr["Telefono2"].ToString();
                        
                        LstCliente.Add(objc9);
                    }
                }


                return LstCliente;
            }
            catch (SqlException sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
            finally
            {
                CerrarConexion(Conex);
            }

        }
        public int AniadeIdC9(string IdSocioC9, string Nombre,string Apellidos, string idTarjeta, string IdCliente, SqlTransaction sqlTrans) {
            int result = 0;
            string strSQL="";
        
            strSQL = "UPDATE N_CLIENTES_TARJETAS_FIDELIDAD SET IdSocioC9='"+IdSocioC9 +"', NombreC9='"+ Nombre + " " + Apellidos + "'";
            strSQL += " WHERE IdTarjeta=" + idTarjeta + " AND IdCliente=" + IdCliente;
         
            try {
                ActualizarSQL(strSQL, sqlTrans,1);
            }
            catch (SqlException ex) {
                 log.Error("Exception AniadeIdC9:" + ex.Message.ToString());
            }
            return result;
        }
        public int NuevoCliente9(Cliente9 client,string txtFechaCaducidad, string IdEmpleado,double dblID, SqlTransaction  sqlTrans) {
            
            int result = 0;
            
            try
            {

                result = InsertCliente(client, IdEmpleado, sqlTrans);
                if (result > 0) result = InsertClienteFidelidad(client, IdEmpleado, txtFechaCaducidad, sqlTrans, dblID,0);
                
            }
            catch (SqlException sqlEx) {
                log.Error("Exception NuevoCliente9:"+ sqlEx.Message.ToString());
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
           

            return result;
        
        }
        public int CheckCliente9(int idCliente, ref int Existe)
        {
            int result = 1;
            String StrSQl;
            DataSet Ds;
            try
            {
                StrSQl="select count(*) ExisteC9 from N_CLIENTES_TARJETAS_FIDELIDAD WHERE IdCliente="+idCliente;
                Ds = GEtSQLDataset(StrSQl);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        Existe = Convert.ToInt32(dr["ExisteC9"].ToString());
                        result = 1;
                    }
                }
            }
            catch (SqlException ex) {
                log.Error("Exception CheckCliente9:" + ex.Message.ToString());
            }
            return result;
        }
        // se guardan los datos del cliente en N_CLIENTES_GENERAL
        private int InsertCliente(Cliente9 client, string IdEmpleado, SqlTransaction sqltrans){
            int result = 0;
            string tipoEmpleado =ObtieneTipoCliente9();
            string StrSQL = "INSERT INTO N_CLIENTES_GENERAL([id_Cliente],[Nombre],[Apellidos],[Cif],[Direccion],[CodPostal],";
            StrSQL += "[Poblacion],[Provincia],[Telefono],[Movil],TelefonoMovil,[Fax],[email],Fecha_Nacimiento,[Fecha_Alta],[Talla],[Contacto]";
            StrSQL +=",[Observaciones],[id_Tipo],IdEmpleadoAlta) values(" + client.Id_Cliente + ",'"+ client.Nombre.ToString()+"','";
            StrSQL += client.Apellidos.ToString() + "','','','',";
            StrSQL += "'','','" + client.Telefono.ToString() + "','" + client.Movil + "','"+client.Movil+"','','" + client.Email.ToString() +"','"+client.FechaNacimiento+ "',GETDATE(),'','',";
            StrSQL += "''," + tipoEmpleado + "," + IdEmpleado + ")";
            try
            {
              
                ActualizarSQL(StrSQL, sqltrans,1);
                result = 1;
            }
            catch (Exception ex)
            {
                log.Error("Exception InsertCliente:" + ex.Message.ToString());

            }

            return result;
        }
        public int UpdateClienteGeneral(Cliente9 client, SqlTransaction sqlTrans) {
            int result = 0;
            
            string StrSQL = "UPDATE N_CLIENTES_GENERAL  set Nombre ='"+client.Nombre+"',Apellidos ='"+client.Apellidos+"',Telefono ='"+client.Telefono +"',";
            StrSQL += " Movil ='" + client.Movil + "',TelefonoMovil='"+client.Movil+"',Fecha_Nacimiento='"+ client.FechaNacimiento+"',email='" + client.Email + "' where Id_Cliente=" + client.Id_Cliente;
           
            try
            {

                ActualizarSQL(StrSQL, sqlTrans, 1);
                result = 1;
            }
            catch (Exception ex)
            {
                log.Error("Exception UpdateClienteGeneral:" + ex.Message.ToString());

            }

            return result;
        }
        // se guardan los datos correspondientes a la tarjeta cliente 9
        private int InsertClienteFidelidad(Cliente9 client, string IdEmpleado,string txtFechaCaducidad, SqlTransaction sqltrans, double dblID, int yaEsC9){
            int result = 0;
            string strDescripcion="CLIENTE 9";
            string fNac = Convert.ToDateTime(client.FechaNacimiento).ToShortDateString();
            DateTime fechNacimiento = Convert.ToDateTime(fNac);
            
            int edad = Convert.ToInt32(CalculaEdad(fechNacimiento));
            if (edad >= 12 && edad <= 21) strDescripcion = "FIRST SHOELOVER";
            
            string StrSQL = "";
            try
            {
                if (yaEsC9 == 0)
                {
                    StrSQL = "INSERT INTO N_CLIENTES_TARJETAS_FIDELIDAD(IdTarjeta,IdCliente,IdEmpleado,NumTarjeta,KeyTarjeta,Descripcion,FechaEmision,FechaCaducidad,FechaModificacion) VALUES(";
                    StrSQL += dblID + "," + client.Id_Cliente + "," + IdEmpleado + ",'" + client.NumTarjeta + "','AVE','" + strDescripcion + "',GETDATE(), CONVERT(DATETIME,'";
                    StrSQL += txtFechaCaducidad + "',103),GETDATE())";
                }
                else {

                    StrSQL = "UPDATE N_CLIENTES_TARJETAS_FIDELIDAD set IdTarjeta =" + dblID + ",IdEmpleado=" + IdEmpleado + ",NumTarjeta ='" + client.NumTarjeta + "',KeyTarjeta='AVE',Descripcion ='" + strDescripcion + "',FechaEmision=getdate()";
                    StrSQL += " ,FechaCaducidad =CONVERT(DATETIME,'" + txtFechaCaducidad + "',103),FechaModificacion =getdate() where IdCliente=" + client.Id_Cliente;
                }
                log.Error("SQL INSERT FIDELIDAD: " + StrSQL);
                ActualizarSQL(StrSQL, sqltrans,1);
                result = 1;
            }
            catch (Exception ex)
            {
                log.Error("Exception InsertClienteFidelidad:" + ex.Message.ToString());

            }

            return result;
        
        }

        public int TryCierraConexion() {
            int result = 0;
            try
            {
                
                if (Conex.State == ConnectionState.Open) {
                    CerrarConexion(Conex);
                    result = 1;
                }
                else result = 1;
            }
            catch (Exception ex) {
                log.Error("Exception TryCierraConexion" + ex.Message.ToString());
            }

            return result;

        }
        public int TryAbreConexion(ref SqlTransaction sqlTrans)
        {
            int result = 0;
            try
            {
                if (Conex.State == ConnectionState.Closed)
                {
                    AbrirConexion(Conex);
                    sqlTrans = Conex.BeginTransaction();
                    result = 1;
                }
                else result = 1;
            }
            catch (Exception ex)
            {
                log.Error("Exception TryAbreConexion:" + ex.Message.ToString());
            }

            return result;

        }

        public int EliminaCliente9(string IdCliente, string numTarjeta){
            int result =0;
            string strSQL="";
            try{
              AbrirConexion(Conex);
              strSQL = "DELETE FROM N_CLIENTES_TARJETAS_FIDELIDAD WHERE IdCliente=" + IdCliente + " and NumTarjeta='" + numTarjeta + "'";
                ActualizarSQL(strSQL);
                result = 1;
            }
            catch(SqlException ex){
                log.Error("Exception Eliminacliente9:"+ ex.Message.ToString());
            }
            finally{
                CerrarConexion(Conex);
            }
            return result;
        }
        //Se pasa un cliente normal a Cliente 9. Ya debe existir en N_CLIENTES_GENERAL.
        
                     
        public int UpdateClienteTo9(Cliente9 client,  string fechaCaducidad,string IdEmpleado, double dblID, SqlTransaction SqlTrans, int yaEsC9)
        {
            int result = 0;

            try
            {
                result = InsertClienteFidelidad(client, IdEmpleado, fechaCaducidad,SqlTrans,dblID,yaEsC9);
            }
            catch (SqlException sqlEx)
            {
                log.Error("Exception NuevoCliente9:" + sqlEx.Message.ToString());
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
          

            return result;
        }
        #endregion
        #region "CambioPlastico"
        public int ActualizaDatosCambioBD(string IdCliente, string IdEmpleado, string Terminal, string Tienda, string TjtActual, string IdTarjetaAnterior, string NivelActual, SqlTransaction sqlTrans)
        {
            int result = 0;
            string strSQL = "";
            DataSet Ds;
            String FCaducidadAlta = "31/12/2099";
            string Nivel = "";
            string idSocio9 ="";
            string NombreSocio = "";
            string FCadAyer = DateTime.Today.AddDays(-1).ToShortDateString();
            if (NivelActual.ToUpper() == "BÁSICO" || NivelActual.ToUpper() == "BÀSICO") Nivel = "CLIENTE 9";
            else Nivel = NivelActual.ToUpper();
            try
            {

                strSQL = "select idSocioC9,NombreC9 from N_CLIENTES_TARJETAS_FIDELIDAD where IdTarjeta=" + IdTarjetaAnterior + " and IdCliente=" + IdCliente;
                Ds = GEtSQLDataset(strSQL, sqlTrans);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        idSocio9 = dr["idSocioC9"].ToString();
                        NombreSocio = dr["NombreC9"].ToString();
                        break;
                    }
                }

                strSQL = "Update N_CLIENTES_TARJETAS_FIDELIDAD set FechaCaducidad= convert(datetime,'"+ FCadAyer +"',103), FechaModificacion = getdate() ";
                strSQL += "where IdTarjeta=" + IdTarjetaAnterior + " and IdCliente=" + IdCliente;
                ActualizarSQL(strSQL, sqlTrans, 1);
                
                double dblID = Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmss"));

                strSQL = "INSERT INTO N_CLIENTES_TARJETAS_FIDELIDAD(IdTarjeta,IdCliente,IdEmpleado,NumTarjeta,KeyTarjeta,Descripcion,FechaEmision,FechaCaducidad,FechaModificacion,IdSocioC9,NombreC9) VALUES(";
                strSQL += dblID + "," + IdCliente + "," + IdEmpleado + ",'" + TjtActual + "','AVE','" + Nivel + "',getdate(),convert(datetime,'" + FCaducidadAlta + "',103),getdate(),'" + idSocio9 + "','" + NombreSocio + "')";
                ActualizarSQL(strSQL, sqlTrans, 1);
                
                result = 1;


            }
            catch (SqlException ex)
            {
                log.Error("Exception InsertaCarritoLinea:" + ex.Message.ToString());
            }


            return result;
        }

        public int GetItemCarrito( string idCarrito, string referencias, ref int  NumArticulos){
            int result = 0;
            DataSet Ds;
            try
            {
                string strSQL = "";
                strSQL = "select COUNT(*) Contador from AVE_CARRITO carr inner join  AVE_CARRITO_LINEA lin on carr.IdCarrito = lin.id_Carrito";
                strSQL += "inner join ARTICULOS art on art.IdArticulo = lin.idArticulo where carr.IdCarrito =" + idCarrito;
                strSQL += "AND art.CodigoAlfa in(" + referencias + ") ";


                Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        NumArticulos = Convert.ToInt32(dr["Contador"].ToString());
                        result = 1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception GetItemCarrito:" + ex.Message.ToString());
            }

            return result;
        
        
        }
        public int GetDatosTarjeta(string IdCarrito, ref string TarjetaActual, ref string TarjetaConsolidada, ref string idEmpleado, ref string IdCliente, ref string idTarjetaAnterior, SqlTransaction sqlTrans)
        {
            int result = 0;
            DataSet Ds;
            try
            {
                string sql = "SELECT isnull(carr.TarjetaCliente,'') IdTarjeta,isnull(TJT.NumTarjeta,'') TarjetaActual, isnull(carr.TarjetaClienteNueva,'') TarjetaClienteNueva, carr.Usuario, carr.Id_Cliente FROM AVE_CARRITO carr ";
                sql +=" inner join N_CLIENTES_TARJETAS_FIDELIDAD TJT ON TJT.IdTarjeta = carr.TarjetaCliente  WHERE IDCARRITO= "+ IdCarrito ;
                

                Ds = GEtSQLDataset(sql);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        TarjetaActual = dr["TarjetaClienteNueva"].ToString();
                        TarjetaConsolidada = dr["TarjetaActual"].ToString();
                        idEmpleado = dr["Usuario"].ToString();
                        IdCliente = dr["Id_Cliente"].ToString();
                        idTarjetaAnterior = dr["IdTarjeta"].ToString();
                        result = 1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception GetIdArticulo:" + ex.Message.ToString());
            }

            return result;
        
        
        }
        public int CheckCambiosDuplicados(int idArticulo, string idCarrito, SqlTransaction sqlTrans) {
            int result = 1;
              DataSet Ds;
            try
            {
                string sql = "SELECT count(*) contador FROM AVE_CARRITO_LINEA WHERE ID_CARRITO= "+ idCarrito + " and idArticulo = "+idArticulo;
                

                Ds = GEtSQLDataset(sql, sqlTrans);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        int idContador = Convert.ToInt32(dr["Contador"].ToString());
                        result = idContador;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception GetIdArticulo:" + ex.Message.ToString());
            }

            return result;
        
        
        
        }
             public int GetIdArticulo(string referencia, ref double PVP, ref int idCabDet, SqlTransaction sqlTrans) { 
            int idArticulo = 0;
            DataSet Ds;
            try
            {
                string sql = "SELECT art.IdArticulo, art.PrecioVenta, cab.Id_Cabecero_Detalle FROM ARTICULOS art inner join CABECEROS_DETALLES cab ";
                sql +=" ON cab.Id_Cabecero = art.Id_Cabecero WHERE CodigoAlfa = '" + referencia + "'";
                
                Ds = GEtSQLDataset(sql, sqlTrans);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        idArticulo = Convert.ToInt32(dr["IdArticulo"].ToString());
                        PVP = Convert.ToDouble(dr["PrecioVenta"].ToString());
                        idCabDet = Convert.ToInt32(dr["Id_Cabecero_Detalle"].ToString());
                        break;
                    }
                }
            }
            catch (Exception ex) {
                log.Error("Exception GetIdArticulo:" + ex.Message.ToString());
            }
           
            return idArticulo;
        }
        public int InsertarPedido(int idArticulo, string IdTerminal,double Precio, string IdTienda,string idEmpleado,SqlTransaction sqlTrans, ref int IdPedido ){
            int result =0;
            string strSQL = "";
            DataSet Ds;
            try
            {
                strSQL = "Insert into AVE_Pedidos(idArticulo, Talla, Unidades, Precio,IdTienda,IdEstadoPedido,IdEstadoSolicitud,Fecha_Creacion,IdTerminal, IdEmpleado)";
                strSQL += " values(" + idArticulo + ",'',1," + Precio + ",'" + IdTienda + "',1,6,GETDATE()," + IdTerminal + "," + idEmpleado + ")";
                strSQL += " SELECT @@IDENTITY";
                
                Ds = GEtSQLDataset(strSQL,sqlTrans);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        IdPedido = Convert.ToInt32(dr[0].ToString());
                        result = 1;
                        break;
                    }
                }
            }
            catch(SqlException ex){
                log.Error("Exception InsertaPedido:" + ex.Message.ToString());
            }
            return result;
        }
        public int InsertaCarritoLinea(string idCarrito,int idArticulo, double PVP, int idPedido,  SqlTransaction sqlTrans,int idCabecero) 
        {
            int result = 0;
            string strSQL = "";
            try
            {
                strSQL = "insert into AVE_CARRITO_LINEA(id_Carrito,IdArticulo,Id_Cabecero_Detalle,Cantidad,PVPORI,PVPACT,idPedido)";
                strSQL += " values(" + idCarrito + "," + idArticulo + ","+idCabecero+",1," + PVP + "," + PVP + "," + idPedido + ")";
                ActualizarSQL(strSQL, sqlTrans,1);
                result = 1;
            }
            catch(SqlException ex){
                log.Error("Exception InsertaCarritoLinea:"+ ex.Message.ToString());
            }



            return result;
        }
        public int InsertarCarrito(string idEmpleado, SqlTransaction sqlTrans, ref string idCarrito, string TarjetaActual, string NuevaTarjeta, int IdCliente, string Maquina) {
            int result = 0;
            DataSet Ds;
            string strSQL = "";
            try
            {
                strSQL = "insert into AVE_CARRITO(Id_Cliente, FechaCreacion,Usuario,Maquina,EstadoCarrito,TarjetaCliente, TarjetaClienteNueva) values(";
                strSQL += IdCliente + ",getdate(),'" + idEmpleado + "','" + Maquina + "',0,'" + TarjetaActual + "','" + NuevaTarjeta + "')";
                strSQL += " SELECT @@IDENTITY";

                Ds = GEtSQLDataset(strSQL, sqlTrans);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        idCarrito = dr[0].ToString();
                        result = 1;
                        break;
                    }
                }

            }
            catch (SqlException ex) {
                log.Error("Exception InsertarCarrito en BD:" + ex.Message.ToString());
            }

            return result;
        }
        public int UpdatarCarrito(string idEmpleado, SqlTransaction sqlTrans, ref string idCarrito, string TarjetaActual, string NuevaTarjeta, int IdCliente, string Maquina)
        {
            int result = 0;
            
            string strSQL = "";
            try
            {
                strSQL = "Update AVE_CARRITO SET Id_Cliente= " + IdCliente + ", Usuario ='"+idEmpleado+"',Maquina='"+Maquina+"',TarjetaCliente='"+TarjetaActual+"',";
                strSQL += "TarjetaClienteNueva='" + NuevaTarjeta + "' WHERE IDCARRITO =" + idCarrito;

                ActualizarSQL(strSQL, sqlTrans, 1);
                result = 1;

            }
            catch (SqlException ex)
            {
                log.Error("Exception UpdatarCarrito en BD:" + ex.Message.ToString());
            }

            return result;
        }
   
#endregion
        #region "Actualiza C9"

        private int UpdateCliente(Cliente9 client, Cliente9Extent datCli, string idEmpleado, SqlTransaction sqlTrans)
        {
            int result = 0;
            string strSQL="";
            try
            {
                strSQL ="UPDATE N_CLIENTES_GENERAL SET  [Nombre]='" + client.Nombre+ "', Apellidos='"+ client.Apellidos +"', Direccion='"+ datCli.Direccion +"',";
                strSQL+="CodPostal ='"+datCli.CP +"', Poblacion='"+ datCli.Poblacion + "', Provincia='"+ datCli.Estado +"', Telefono='"+ client.Telefono +"',Telefono2='"+datCli.TfnoTrabajo +"',";
                strSQL +="Fax='"+datCli.Fax +"',email='"+client.Email + "',Talla='"+datCli.Talla + "',Contacto='"+datCli.Contacto+"',Observaciones='"+datCli.Comentarios + "', Fecha_Nacimiento='"+ client.FechaNacimiento +"',";
                strSQL +="FechaModificacion=GETDATE(), Pais='"+ datCli.Pais +"', Cargo="+datCli.Cargo +"',Movil="+client.Movil +"'";
                strSQL +=" WHERE Id_Cliente="+client.Id_Cliente;

                ActualizarSQL(strSQL,sqlTrans,1);
                result = 1;
 
            }
            catch (SqlException ex)
            {
                log.Error("Exception UpdateCliente:"+ ex.Message.ToString());

            }
            return result;
        
        }

        //private int InsertClienteFacturacion(Cliente9 client, FacturacionC9 datFact, string idEmpleado, SqlTransaction sqlTrans)
        //{ 
        //     int result = 0;
        //    string strSQL = "";
        //    try
        //    {
        //        strSQL = "INSERT INTO N_CLIENTES_FACTURACION VALUES([Id_cliente],[CIF],[Nombre],[Apellidos],[Direccion],[Portal],[Puerta]";
        //        strSQL+=",[Barriada],[Poblacion],[Provincia],[Pais],[CodPostal],[Telefono],[Movil],[Fax],[FechaModificacion],[EnviarFactMail],[Email])";
        //        strSQL+="VALUES("+ client.Id_Cliente +",'"+ datFact.RFC +"','"+datFact.Nombre+"',''," +datFact.Direccion+"','"+datFact.NExterior+"','";
        //        strSQL+= datFact.NInterior +"','"+datFact.Colonia+"','"+ datFact.Poblacion + "','"+ datFact.Estado +"','"+ datFact.Pais +"', '";
        //        strSQL+= datFact.CP + "', '"+ datFact.Telefono +"','"+ datFact.Movil +"','"+datFact.Fax +"', GETDATE(),"+ datFact.EnvFactMail +",'"+ datFact.EMail+"')";
               
                    
 
        //        ActualizarSQL(strSQL, sqlTrans);
        //        result = 1;

        //    }
        //    catch (SqlException ex)
        //    {
        //        log.Error("Exception InsertClienteFacturacion:" + ex.Message.ToString());

        //    }
        //    return result;
        
        
        //}
        //private int UpdateClienteFacturacion(Cliente9 client, FacturacionC9 datFact, string idEmpleado, SqlTransaction sqlTrans)
        //{
        //    int result = 0;
        //    string strSQL = "";
        //    try
        //    {
        //        strSQL = "UPDATE N_CLIENTES_FACTURACION SET  CIF='"+ datFact.RFC +"',Nombre="+ datFact.Nombre+"', Apellidos='', Direccion='"+ datFact.Direccion +"',";
        //        strSQL+=" Portal ='"+datFact.NExterior +"',Puerta="+ datFact.NInterior +"', Barriada='"+datFact.Colonia+"',Poblacion='"+ datFact.Poblacion + "',";
        //        strSQL+= " Provincia='"+ datFact.Estado +"', Pais='"+ datFact.Pais +"', CodPostal='"+ datFact.CP + "', Telefono='"+ datFact.Telefono +"', Movil='"+ datFact.Movil +"',";
        //        strSQL+=" Fax='"+ datFact.Fax +"', FechaModificacion=GETDATE(), EnviarFactMail="+ datFact.EnvFactMail +",Email='"+ datFact.EMail;
        //        strSQL += "  WHERE Id_Cliente =" + client + " and RFC='" + datFact.RFC + "'";
                    
 
        //        ActualizarSQL(strSQL, sqlTrans,1);
        //        result = 1;

        //    }
        //    catch (SqlException ex)
        //    {
        //        log.Error("Exception UpdateClienteFacturacion:" + ex.Message.ToString());

        //    }
        //    return result;
        
        //}
        //public int ActualizaCliente(Cliente9 client, Cliente9Extent datCli,FacturacionC9 datFactC9, string idEmpleado, SqlTransaction sqlTrans, int accion) {
            
        //    int result = 0;
        //    try
        //    {

        //        result = UpdateCliente(client, datCli, idEmpleado, SqlTrans);
        //        if (result > 0)
        //        {
        //            if(accion== 0)//INSERT
        //                result =UpdateClienteFacturacion(client, datFactC9, idEmpleado, SqlTrans);
        //            else
        //                result = InsertClienteFacturacion(client, datFactC9, idEmpleado, SqlTrans);
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        log.Error("Exception ActualizaCliente:" + sqlEx.Message.ToString());
        //        throw new Exception(sqlEx.Message, sqlEx.InnerException);
        //    }

        //    return result;
        
        //}
        #endregion
        #region "ActualizaClinete"
        public int ActualizacionCliente9(Cliente9 client, Cliente9Extent clientExtend, string IdEmpleado, SqlTransaction sqlTrans) {
            int result = 0;
            string strSQL = "";
            try
            {
                strSQL = "UPDATE N_CLIENTES_GENERAL SET [Nombre]='"+client.Nombre +"',[Apellidos] ='"+client.Apellidos +"',[Direccion]='"+ clientExtend.Direccion;
                strSQL+= "',[CodPostal]='"+clientExtend.CP+"',[Poblacion]='"+clientExtend.Poblacion+"',[Provincia]='"+clientExtend.Estado;
                strSQL+= "',[Telefono]='"+client.Telefono +"',[Telefono2]='"+clientExtend.TfnoTrabajo+"',[Fax]='"+clientExtend.Fax +"',[email]='"+client.Email;
                strSQL+= "',[Talla]='"+ clientExtend.Talla+"',[Contacto]='"+ clientExtend.Contacto+"',[Observaciones]='"+clientExtend.Comentarios+"',[Fecha_Nacimiento]='"+client.FechaNacimiento;
                strSQL += "',[Pais]='" + clientExtend.Pais + "',[Cargo]='" + clientExtend.Cargo + "',[Movil]='" + client.Movil + "',[TelefonoMovil]='" + client.Movil + "' ";
                strSQL += " WHERE Id_Cliente=" + client.Id_Cliente;
                ActualizarSQL(strSQL, sqlTrans, 1);
                result = 1;

            }
            catch (SqlException ex)
            {
                log.Error("Exceptin SetDatosFacturacion:" + ex.Message.ToString());
            }

            return result;
        }
        public int ActualizaIdC9(string IdSocioC9, Cliente9 client, SqlTransaction sqlTrans) {
            int result = 0;
            string strSQL = "";
            try
            {
                strSQL = "UPDATE N_CLIENTES_TARJETAS_FIDELIDAD set NumTarjeta ='" + client.NumTarjeta + "',FechaModificacion =getdate()";
                strSQL += " ,IdSocioC9='" + IdSocioC9 + "', NombreC9='" + client.Nombre + " " + client.Apellidos + "'  where IdCliente=" + client.Id_Cliente+" and NumTarjeta ='" +client.NumTarjeta +"'";
                ActualizarSQL(strSQL, sqlTrans,1);
                result = 1;

            }
            catch (SqlException ex)
            {
                log.Error("Exception ActualizaIdC9:" + ex.Message.ToString());

            }
            return result;
        
        }
        public int SetDatosFacturacion(Models.FacturacionC9 DFact, string RFCOld){
            int result = 0;
            string strSQL = "";
            try
            {
               
                if (DFact.Accion == "INS")
                {
                    strSQL="INSERT INTO N_CLIENTES_FACTURACION(Id_Cliente,[CIF],[Nombre],[Apellidos],[Direccion],[Portal],[Puerta],[Barriada],[Poblacion]";
                    strSQL+=",[Provincia],[Pais],[CodPostal],[Telefono],[Movil],[Fax],[FechaModificacion],[EnviarFactMail],[Email]) VALUES(";
                    strSQL+= DFact.IdCliente +",'"+DFact.RFC +"','"+ DFact.Nombre+"','','"+ DFact.Direccion+"','"+DFact.NExterior+"','"+DFact.NInterior+"','";
                    strSQL+= DFact.Colonia+"','"+DFact.Poblacion+"','"+DFact.Estado+"','"+DFact.Pais+"','"+DFact.CP+"','"+DFact.Telefono+"','";
                    strSQL += DFact.Movil + "','" + DFact.Fax + "',GETDATE(),'" + DFact.EnvFactMail + "','" + DFact.EMail + "')";
                }
                else {
                    strSQL = "UPDATE N_CLIENTES_FACTURACION SET CIF='" + DFact.RFC + "',NOMBRE='" + DFact.Nombre + "',Direccion='" + DFact.Direccion + "',Portal='" + DFact.NExterior;
                    strSQL += "',Puerta='" + DFact.NInterior + "',Barriada='" + DFact.Colonia + "',Poblacion='" + DFact.Poblacion + "',Provincia='" + DFact.Estado + "',Pais='" + DFact.Pais + "',CodPostal='";
                    strSQL += DFact.CP + "',Telefono='" + DFact.Telefono + "',Movil='" + DFact.Movil + "',Fax='" + DFact.Fax + "',FechaModificacion=GETDATE(),EnviarFactMail='" + DFact.EnvFactMail + "',Email='" + DFact.EMail + "'";
                    strSQL += " WHERE Id_Cliente=" + DFact.IdCliente + " and CIF='" + RFCOld + "'";
                }
                ActualizarSQL(strSQL);
                result = 1;

            }
            catch (SqlException ex)
            {
                log.Error("Exceptin SetDatosFacturacion:" + ex.Message.ToString());
            }
           
            return result;
        }
        public int GetDatosFacturacion(string IdCliente, ref List<FacturacionC9> datosFac) {
            int result = 0;
            string strSQL = "";
            DataSet Ds;
            try
            {

                strSQL="SELECT [CIF],[Nombre],[Direccion],[Portal],[Puerta],[Barriada],[Poblacion],[Provincia],[Pais],[CodPostal],[Telefono] ";
                strSQL += " ,[Movil],[Fax],[EnviarFactMail],[Email] FROM [N_CLIENTES_FACTURACION] WHERE Id_Cliente=" + IdCliente;

                Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables.Count > 0)
                { 
                   
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        FacturacionC9 factAux = new FacturacionC9();
                        factAux.RFC = dr["CIF"].ToString();
                        factAux.Nombre = dr["Nombre"].ToString();
                        factAux.Direccion = dr["Direccion"].ToString();
                        factAux.NExterior = dr["Portal"].ToString();
                        factAux.NInterior = dr["Puerta"].ToString();
                        factAux.Colonia = dr["Barriada"].ToString();
                        factAux.Poblacion = dr["Poblacion"].ToString();
                        factAux.Estado = dr["Provincia"].ToString();
                        factAux.Pais = dr["Pais"].ToString();
                        factAux.CP = dr["CodPostal"].ToString();
                        factAux.Telefono = dr["Telefono"].ToString();
                        factAux.Movil = dr["Movil"].ToString();
                        factAux.Fax = dr["Fax"].ToString();
                        factAux.EMail = dr["Email"].ToString();
                        factAux.EnvFactMail = Convert.ToInt32(dr["EnviarFactMail"].ToString());
                        datosFac.Add(factAux);

                    }
                    result = 1;
                }

            }
            catch (SqlException ex) {
                log.Error("Exception GetDatosFacturacionBD:"+ ex.Message.ToString());
            }
            finally{
            CerrarConexion(Conex);
            }

            return result;
        }
        #endregion
        #region "Metodo generales"
        private string ObtieneTipoCliente9()
        {
            string tipo = "";
            string strSQL = "select isnull(id_tipo,'')Id_Tipo from n_clientes_tipo WHERE LTRIM(RTRIM(UPPER(Tipo_Cliente))) ='CLIENTE 9'";
            DataSet Ds;
            try
            {

                Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        tipo = dr["Id_Tipo"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error("Exception ObtieneTipoCliente9:" + ex.Message.ToString());

            }
            return tipo;
        }
       
        public string GetNameError(string Strsql)
        {
            DataSet Ds;
            string strError = "";

            try
            {

                Ds = GEtSQLDataset(Strsql);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        strError = (Ds.Tables[0].Rows[0][0].ToString());

                    }

                }

                return strError;

            }

            catch (SqlException sqlEx)
            {
                log.Error(sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }

        public long GetCountTable(string Strsql)
        {
            DataSet Ds;
            long lngCount = 0;

            try
            {

                Ds = GEtSQLDataset(Strsql);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        lngCount = Int16.Parse(Ds.Tables[0].Rows[0][0].ToString());

                    }

                }

                return lngCount;

            }

            catch (SqlException sqlEx)
            {
                log.Error(sqlEx);
                CerrarConexion(Conex);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
        }
        private string CalculaEdad(DateTime FNacimiento)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - FNacimiento.Year;
            if (now < FNacimiento.AddYears(age)) age--;

            return age.ToString();
        }
        public int CompruebaIdCliente(long IdCli)
        {
            int result = 1;
            String StrSQl;
            DataSet Ds;
            try
            {
                StrSQl = "SELECT Count(*) NumClientes FROM N_CLIENTES_GENERAL WITH (NOLOCK) WHERE Id_Cliente=" + IdCli;
                Ds = GEtSQLDataset(StrSQl);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        result = int.Parse(dr["NumClientes"].ToString());
                    }
                }

            }
            catch (SqlException ex)
            {
                log.Error("Exception CompruebaIdCliente." + ex.Message.ToString());
            }
            return result;
        }
        public int ObtieneRango(ref long InicioRango, ref long FinRango, int option)
        {
            int result = 0;
            int encontrado = 0;
            string strSQL = "";
            DataSet Ds;
            try
            {
                strSQL = "SELECT isnull(Id_Cliente,0) Id_Cliente  FROM N_CLIENTES_GENERAL WITH (NOLOCK) WHERE Id_Cliente>=" + InicioRango + " AND Id_Cliente<=" + FinRango;
                if (option == 0) strSQL += " order by 1";
                else strSQL += " order by 1 DESC";
                Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        if (option == 0)
                        {
                            if ((Math.Abs(int.Parse(dr["Id_Cliente"].ToString())) - InicioRango) > 0)
                            {
                                result = 1;
                                encontrado = 1;
                                break;
                            }
                            else InicioRango++;
                        }
                        else 
                        {
                            if ((FinRango - Math.Abs(int.Parse(dr["Id_Cliente"].ToString()))) > 0)
                            {
                                result = 1;
                                encontrado = 1;
                                break;
                            }
                            else FinRango--;
                        
                        }
                    }
                    if (option == 0)
                    {
                        if ((InicioRango <= FinRango) && encontrado == 0) result = 1;
                    }
                    else {
                        if ((FinRango >= InicioRango) && encontrado == 0) result = 1;
                    }

                }
                else result = 1;

            }
            catch (SqlException ex)
            {
                log.Error("Exception ObtieneRango: " + ex.Message.ToString());

            }

            return result;

        }

        public int CuentaTiendas(string strTienda, ref  int NumTienda, ref int ContadorTiendas, ref bool blnAlmacen)
        {
            int result = 0;
            string strSQL = "";
            int contador = 0;
            DataSet Ds;
            try
            {
                strSQL = "select t.idTienda IdTienda, isnull((select id_Grupo  from GRUPOS_DETALLES gd WITH (NOLOCK) ";
                strSQL += " where gd.id_Grupo=0 AND gd.Id_Tienda=t.Idtienda),1 ) as Almacen ";
                strSQL += " from tiendas t WITH (NOLOCK) Where t.Tiendas_Activo = 1  order by t.idTienda ";

                Ds = GEtSQLDataset(strSQL);
                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        if (dr["Almacen"].ToString() == "1")
                        {//no almacen
                            ContadorTiendas++;
                            if (dr["IdTienda"].ToString().ToUpper() == strTienda.ToUpper())
                            {
                                blnAlmacen = false;
                                NumTienda = ContadorTiendas;
                            }
                        }
                        else
                        {// almacen
                            contador = ContadorTiendas + 5;
                            if (dr["IdTienda"].ToString().ToUpper() == strTienda.ToUpper())
                            {
                                blnAlmacen = true;
                                NumTienda = ContadorTiendas;
                            }
                        }
                    }
                    result = 1;
                }
            }
            catch (SqlException ex)
            {
                log.Error("Exception CuentaTiendas:" + ex.Message.ToString());
            }
            return result;


        }
        #endregion
        #region "WSONLINE"
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

    }
}
