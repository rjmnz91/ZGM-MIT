using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DLLGestionCliente9.Models;
using DLLGestionCliente9.CapaDatos;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Configuration;



namespace DLLGestionCliente9
{
    public class ClsCliente9
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        String StrCadenaConexion;
        String idTerminal;
        DateTime _Fecha;
        ClsCapaDatos9 objCapaDatos;
        String FCaducidadAlta = "31/12/2099";
        float iva;
        const String PaswordWs = "8vJSRsOHMZg9u9Ir";
        public String ConexString
        {
            set { StrCadenaConexion = value; }
        }

        /// <summary>
        /// Terminal de la venta
        /// </summary>
        public String Terminal
        {
            set { idTerminal = value; }
        }

        public DateTime FechaSesion
        {
            set { _Fecha = value; }
        }
        #region "Utilidades generales"
        public int CompruebaIdOk(long idCliTmp)
        {
            int result = 1;


            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.CompruebaIdCliente(idCliTmp);

            }
            catch (Exception ex)
            {
                log.Error("Exception CompruebaIdOk." + ex.Message.ToString());
            }
            return result;


        }
        public long Rand(long Low, long High)
        {
            Random rnd = new Random();
            return (Convert.ToInt64((High - Low + 1) * rnd.Next(0,1) + Low));
        }
        public int  GetContadorTiendas (string strTienda, ref int NumTienda, ref int ContadorTiendas, ref bool blnAlmacen)
        {
            int result = 0;
            try {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.CuentaTiendas(strTienda, ref  NumTienda, ref ContadorTiendas, ref blnAlmacen);
            
            }
            catch (Exception ex) {
                log.Error("Exception GetContadorTiendas." + ex.Message.ToString());
            }

            return result;

        }
        #endregion
        #region "AltaCliente 9"
        //Se utiliza para obtener los datos de busqueda de cliente en la administración de cliente nine
        public List<Cliente9> GetClienteAdm9(String Cliente, DateTime Fecha)
        {
            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;

                return objCapaDatos.GetDatCliente(Cliente, Fecha);
            }

            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }
        public List<Cliente9Gral> GetClienteActualizacion(String Cliente, DateTime Fecha)
        {
            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;

                return objCapaDatos.GetDatClienteActualizacion(Cliente, Fecha);
            }

            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
        }
        public int  UpdateHistoricoErr309(string strMetodoWS, string strEntrada, string Tienda){

            int result = 1;


            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.UpdateHistoricoErr309(strMetodoWS,  strEntrada,  Tienda);
            }
            catch (Exception ex)
            {
                log.Error("Exception EliminaCliente9." + ex.Message.ToString());
            }
            return result;
        
        
        
        }
        public int EliminaClienteNine(string IdCliente, string numTarjeta) {
            int result = 1;


            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.EliminaCliente9(IdCliente, numTarjeta);
            }
            catch (Exception ex)
            {
                log.Error("Exception EliminaCliente9." + ex.Message.ToString());
            }
            return result;
        
        }
        public int AniadeNuevoCliente(ref Cliente9 client, string IdEmpleado, string IdTienda, string IdTerminal, ref int error309, ref string msgWS)
        {
            int result = 0;
            SqlTransaction sqlTrans;
            string cadConexion = "";
            msgWS = "";
            string IdSocioC9 = "";// "123456";
            client.Id_Cliente = 0;

            double dblID = Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmss"));
            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                sqlTrans = objCapaDatos.GetTransaction();
                cadConexion = sqlTrans.Connection.ConnectionString;
                result = objCapaDatos.TryAbreConexion(ref sqlTrans);
                client.Id_Cliente = GeneraIdClienteLibre(-1, IdTienda);

                if (client.Id_Cliente > 0)
                {
                    result = objCapaDatos.NuevoCliente9(client, FCaducidadAlta, IdEmpleado, dblID, sqlTrans);

                    if (result > 0)
                    {

                        result = ws_AltaSocio(client, IdEmpleado, IdTienda, IdTerminal, ref msgWS, ref IdSocioC9, cadConexion);
                        if (result == 309) {
                            error309 = 1;
                            sqlTrans.Commit();
                        
                        } 
                            
                        if (result > 0 )
                        {
                            if (error309 != 1)
                            { 
                                objCapaDatos.AniadeIdC9(IdSocioC9, client.Nombre.ToString(), client.Apellidos.ToString(), dblID.ToString(), client.Id_Cliente.ToString(), sqlTrans);
                               
                                sqlTrans.Commit();
                                objCapaDatos.TryCierraConexion();
                                try
                                {
                                    InvokeUpCliente9Ws(IdTienda, client.Id_Cliente.ToString(), 3);
                                }
                                catch (Exception ex)
                                {
                                    msgWS = "TODO OK. Pero no se pudo subir el cliente al WS ONLINE";
                                    log.Error("Error al subir el cliente al ws online" + ex.Message.ToString());
                                }
                                
                                result = 1;
                            }
                        }
                        else sqlTrans.Rollback();
                    }
                    else sqlTrans.Rollback();
                }
                objCapaDatos.TryCierraConexion();
            }
                

            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
            return result;
        }
        public int CkeckCliente9BD(int IdCliente, ref int yaEsC9) {
            int result = 1;


            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.CheckCliente9(IdCliente, ref yaEsC9);

            }
            catch (Exception ex)
            {
                log.Error("Exception CompruebaIdOk." + ex.Message.ToString());
            }
            return result;


        
        
        }
        public int UpdateClientetoNine(ref Cliente9 client, string IdEmpleado, string IdTienda, string IdTerminal, ref int error309, ref string msgWS)
        {
            int result = 0;
            SqlTransaction sqlTrans;
            int yaEsC9 = 0;
             msgWS = "";
            string IdSocioC9="";
            string cadConexion ="";
            double dblID = Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmss"));

            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            sqlTrans = objCapaDatos.GetTransaction();
            try
            {
                cadConexion = sqlTrans.Connection.ConnectionString;
                result = objCapaDatos.TryAbreConexion(ref sqlTrans);
                result= CkeckCliente9BD(client.Id_Cliente, ref yaEsC9);
                
                result = objCapaDatos.UpdateClienteGeneral(client,sqlTrans);
                result = objCapaDatos.UpdateClienteTo9(client, FCaducidadAlta, IdEmpleado, dblID, sqlTrans,yaEsC9);
                
                if (result > 0)
                {
                    result = ws_AltaSocio(client, IdEmpleado, IdTienda, IdTerminal, ref msgWS, ref IdSocioC9,  cadConexion);
                    if (result == 309)
                    {
                        error309 = 1;
                        sqlTrans.Commit();

                    }
                    if (result > 0)
                    {
                        if (error309 != 1)
                        {
                            objCapaDatos.AniadeIdC9(IdSocioC9, client.Nombre.ToString(), client.Apellidos.ToString(), dblID.ToString(), client.Id_Cliente.ToString(), sqlTrans);
                            
                            sqlTrans.Commit();
                            objCapaDatos.TryCierraConexion();
                            try
                            {
                                InvokeUpCliente9Ws(IdTienda, client.Id_Cliente.ToString(), 3);
                            }
                            catch (Exception ex)
                            {
                                msgWS = "TODO OK. Pero no se pudo subir el cliente al WS ONLINE";
                                log.Error("Error al subir el cliente al ws online." + ex.Message.ToString());
                            }

                            result = 1;
                        }
                    }
                    else sqlTrans.Rollback();
                }
                else sqlTrans.Rollback();
                objCapaDatos.TryCierraConexion();


            }
            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                sqlTrans.Rollback();
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }
            finally { 
            
            }
            return result;
        }
     
        private string GetCadenaEnvioWS(Cliente9 client, string strEmpleado, string IdTienda, string IdTerminal) 
        {
            string str_ws_ParamSend="";
            string ws_FechaAniversario="";
            string ws_FechaCumple="";
            string ws_ApellidoPat="";
            string  ws_ApellidoMat="";
            string ws_TfnoTrabajo="";
            string ws_Email2="";
            string ws_Tienda="";
            string ws_Terminal="";
            
            string [] apels;

            DateTime FAniversario = Convert.ToDateTime(client.Aniversario);
            DateTime FCumple = Convert.ToDateTime(client.FechaNacimiento);
            ws_FechaAniversario = FAniversario.ToString("yyyy/MM/dd");
            ws_FechaCumple = FCumple.ToString("yyyy/MM/dd");
    
            ws_ApellidoPat = "";
            ws_ApellidoMat="";
            if (client.Apellidos!=""){ 
                apels=client.Apellidos.Trim().Split(' ');
                ws_ApellidoPat= apels[0].ToString();
                for (int i = 1; i< apels.Length; i++)
                {
                    ws_ApellidoMat += apels[i].ToString() + " ";
                }
                ws_ApellidoMat= ws_ApellidoMat.Trim();
            }
            
            ws_Tienda= IdTienda.Replace("T-", "");
            ws_Terminal = IdTerminal != "" ? IdTerminal : "";
            str_ws_ParamSend = ws_Tienda + "|" + ws_Terminal + "|" + strEmpleado + "|" + client.NumTarjeta.ToString() + "|" + ws_FechaAniversario;
            str_ws_ParamSend += "|" + ws_FechaCumple + "|" + client.Nombre.ToString() + "|" + ws_ApellidoPat.Trim() + "|";
            str_ws_ParamSend +=  ws_ApellidoMat.ToString() + "|" + client.Telefono.ToString() + "|" + client.Movil.ToString()+ "|"+ws_TfnoTrabajo+"|" + client.Email.ToString() +"|"+ ws_Email2.Trim();

            return str_ws_ParamSend;
                
    }
       
        public int  ws_AltaSocio(Cliente9 client, string IdEmpleado, string Tienda, string IDTerminal, ref string msgWS, ref string IdSocioC9,string cadConexion)
        {
            int result = 0;
            WSCliente9 wsNine = new WSCliente9();
            wsNine.ConexString = cadConexion;    
           string  paramInput = GetCadenaEnvioWS(client, IdEmpleado, Tienda, IDTerminal);
            result = wsNine.InvokeWS_AltaSocio(paramInput, Tienda, IdEmpleado, ref msgWS, ref IdSocioC9);

            return result;
        
        }
         private int ObtieneRango(ref long RangoIni, ref long RangoFin, int option){
            int result = 0;
            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            try
            {
                result = objCapaDatos.ObtieneRango(ref RangoIni, ref RangoFin, option);
            }
            catch (Exception ex)
            {
                log.Error("Exception ObtieneRango:" + ex.Message.ToString());
            }
            return result;
        }
        public int GeneraIdClienteLibre(double dCliInitial, string strTienda)
        {
            long numConstante = 86400;
            int valido = 0;
            int result = 0;
            long IdCliAux = 0;
            double dblID = dCliInitial;
            long LngRangoAux = 0;
            int NumTienda = 0;
            int ContadorTiendas = 0;
            long NumBase = 0;
            long RangoCargos = 0;
            long InicioRango;
            long FinRango;
            long InicioRangoAux = 0;
            long FinRangoAux = 0;
            bool blnAlmacen = false;
            int IdReturn =0;
            DateTime Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString().ToString());
            DateTime FechaIni = Convert.ToDateTime(Convert.ToDateTime("31/12/2010").ToShortDateString());
            result = GetContadorTiendas(strTienda, ref NumTienda, ref ContadorTiendas, ref blnAlmacen);
            blnAlmacen = false;
            TimeSpan t = Fecha - FechaIni;
            NumBase = t.Seconds;
            //NumBase = DateTime.DateDiff("s", FechaIni, Fecha) //ponemos como base 31/12/2010 para emprezar

            RangoCargos = Convert.ToInt64(Convert.ToInt32(numConstante / ContadorTiendas));
            //ALMACENES LE DAMOS PESO DE 5 TIENDAS
            if (blnAlmacen)
            {
                InicioRango = NumBase + (RangoCargos * (NumTienda - 4)) + 1;
                FinRango = InicioRango + (RangoCargos * 5) - 1;
            }
            else
            {
                InicioRango = NumBase + (RangoCargos * (NumTienda)) - (RangoCargos) + 1;
                FinRango = NumBase + (RangoCargos * (NumTienda));
                LngRangoAux = (FinRango - InicioRango) / 2;  // vamos a partir el rango en dos para asignar la 2 parte a la central
                InicioRango = InicioRango + LngRangoAux;
            }

            IdCliAux = Rand(InicioRango, FinRango);
            int noLibre = CompruebaIdOk(IdCliAux);
            if (noLibre == 0)
            {
                IdReturn = Convert.ToInt32(IdCliAux);
                return IdReturn;
            }
            // el id no esta libre y hay que volver a generar uno nuevo, para validarlo.
            else
            {
                InicioRangoAux = InicioRango + numConstante - LngRangoAux;
                FinRangoAux = FinRango + numConstante;
                valido = ObtieneRango(ref InicioRango, ref FinRango, 0);
            }
            if (valido == 0)
            {
                    valido = ObtieneRango(ref InicioRangoAux, ref FinRangoAux, 1);
                    if (valido > 0) IdReturn = Convert.ToInt32(FinRangoAux);
            }
            else IdReturn = Convert.ToInt32(InicioRango);
            
            return IdReturn;
        }
        #endregion
        #region "Actualiza C9"
        private string GetCadenaEnvioWSActualizacion(Cliente9 client, Cliente9Extent clientExtend,   string strEmpleado, string IdTienda, string IdTerminal)
        {
            string str_ws_ParamSend = "";
            string ws_FechaAniversario = "";
            string ws_FechaCumple = "";
            string ws_ApellidoPat = "";
            string ws_ApellidoMat = "";
            string ws_TfnoTrabajo = "";
            string ws_Email2 = "";
            string ws_Tienda = "";
            string ws_Terminal = "";

            string[] apels;

            DateTime FAniversario = Convert.ToDateTime(client.Aniversario);
            DateTime FCumple = Convert.ToDateTime(client.FechaNacimiento);
            ws_FechaAniversario = FAniversario.ToString("yyyy/MM/dd");
            ws_FechaCumple = FCumple.ToString("yyyy/MM/dd");
            ws_TfnoTrabajo = clientExtend.TfnoTrabajo.ToString();

            ws_ApellidoPat = "";
            ws_ApellidoMat = "";
            if (client.Apellidos != "")
            {
                apels = client.Apellidos.Trim().Split(' ');
                ws_ApellidoPat = apels[0].ToString();
                for (int i = 1; i < apels.Length; i++)
                {
                    ws_ApellidoMat += apels[i].ToString() + " ";
                }
                ws_ApellidoMat = ws_ApellidoMat.Trim();
            }

            ws_Tienda = IdTienda.Replace("T-", "");
            ws_Terminal = IdTerminal != "" ? IdTerminal : "";
            str_ws_ParamSend = ws_Tienda + "|" + ws_Terminal + "|" + strEmpleado + "|" + client.NumTarjeta.ToString() + "|" + ws_FechaAniversario;
            str_ws_ParamSend += "|" + ws_FechaCumple + "|" + client.Nombre.ToString() + "|" + ws_ApellidoPat.Trim() + "|";
            str_ws_ParamSend += ws_ApellidoMat.ToString() + "|" + client.Telefono.ToString() + "|" + client.Movil.ToString() + "|" + ws_TfnoTrabajo + "|" + client.Email.ToString() + "|" + ws_Email2.Trim();

            return str_ws_ParamSend;

        }

        private int ws_ActualizaSocio(Cliente9 client,Cliente9Extent clientExtend ,string IdEmpleado, string Tienda, string IDTerminal, ref string msgWS, ref string IdSocioC9, string cadConexion)
        {
            int result = 0;
            WSCliente9 wsNine = new WSCliente9();
            wsNine.ConexString = cadConexion; 

            string paramInput = GetCadenaEnvioWSActualizacion(client,clientExtend, IdEmpleado, Tienda, IDTerminal);
            result = wsNine.InvokeWS_ActualizaSocio(paramInput, Tienda, IdEmpleado, ref msgWS, ref IdSocioC9);

            return result;

        }
        public int ActualizaDatosCliente(Models.Cliente9 client, Models.Cliente9Extent clientExtend, string IdEmpleado, string IdTienda, string IdTerminal, ref string  strWS, ref string IdSocioC9, ref int error309){
            int result = 0;
            SqlTransaction sqlTrans;
            string cadConexion = "";
            error309=0;
            strWS = "";
            IdSocioC9 = "";
            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            sqlTrans = objCapaDatos.GetTransaction();
            cadConexion = sqlTrans.Connection.ConnectionString;
            try
            {
              

                result = objCapaDatos.TryAbreConexion(ref sqlTrans);
                result = objCapaDatos.ActualizacionCliente9(client, clientExtend, IdEmpleado,sqlTrans);
                if(result > 0)
                {
                    result = ws_ActualizaSocio(client, clientExtend, IdEmpleado, IdTienda, IdTerminal, ref strWS, ref IdSocioC9, cadConexion);
                    if (result == 309)
                    {
                        error309 = 1;
                        sqlTrans.Commit();

                     }
                     if (result > 0)
                     {
                        if (error309 != 1)
                        {
                            objCapaDatos.ActualizaIdC9(IdSocioC9, client, sqlTrans);
                            sqlTrans.Commit();
                        }
                    }
                    else sqlTrans.Rollback();
                }
                else sqlTrans.Rollback();
                objCapaDatos.TryCierraConexion();
            }
            catch (Exception ex) {
                log.Error("ClsCliente9.ActualizaCliente:" + ex.Message.ToString());
                sqlTrans.Rollback();
            }

            return result;

        }
        public int GuardaDatosFacturacion(Models.FacturacionC9 DatosFact, string RFCOld) {
            int result = 0;
            try
            {

                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.SetDatosFacturacion(DatosFact, RFCOld);

            }
            catch (Exception ex) {
                log.Error("Exception GuardaDatosFacturacion:" + ex.Message.ToString());
            }

            return result;
        }

        public List<Models.FacturacionC9> GetDatosFacturacion(string IdCliente) {
            List<Models.FacturacionC9> datosFac = new List<Models.FacturacionC9>();
            int result = 0;
            
            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result= objCapaDatos.GetDatosFacturacion (IdCliente, ref datosFac);
            }
            catch(Exception ex){
                log.Error("Exception GetDatosFacturacion:"+ ex.Message.ToString());
            }
            return datosFac;
        
        
        }
     
        #endregion
        #region "CambioPlasticoC9"
        private string SetParamEnvioWS(string strTienda, string IdTerminal, string TarjetaActual, string TarjetaAnterior, string tipoCambio)
        {
            string str_ws_ParamSend = "";
            
            string ws_Tienda = "";
            string ws_Terminal = "";



            ws_Tienda = strTienda.Replace("T-", "");
            ws_Terminal = IdTerminal != "" ? IdTerminal : "";
            str_ws_ParamSend = ws_Tienda + "|" + Convert.ToInt32(ws_Terminal).ToString() + "|" + TarjetaAnterior + "|" + TarjetaActual + "|" + tipoCambio;

            return str_ws_ParamSend;

        }

        public int ws_CambioTJT(string strIdTicket, string IdEmpleado, string Tienda, string IdTerminal, string TarjetaNueva, string TarjetaConsolidad, string tipoCambio, ref string msgWS, string cadConexion)
        {
            int result = 0;
            string resSolicitud = "";
            string resConfirmation ="";
            string paramInputConfirmacion = "";
            
            WSCliente9 wsNine = new WSCliente9();
            string [] arrTruncate;
            wsNine.ConexString = cadConexion;
            string paramInput = SetParamEnvioWS(Tienda, IdTerminal, TarjetaNueva, TarjetaConsolidad, tipoCambio);
              
            resSolicitud = wsNine.InvokeWS_SolicitudCambioTarjeta(paramInput,IdEmpleado,strIdTicket,  Tienda, ref msgWS);
            if (resSolicitud != "")
            { 
            
                arrTruncate =   resSolicitud.Split('|');
                string     ws_Tienda = Tienda.Replace("T-", "");
                string ws_Terminal = IdTerminal != "" ? IdTerminal : "1";
                int ws_TerminalFormat = Convert.ToInt32(ws_Terminal);
                string strAutorization= arrTruncate[1].ToString();
                int position = strIdTicket.IndexOf("/");

                string strTicketNew = "C9AVT" + ws_Tienda + ws_TerminalFormat.ToString("D2") + ws_Terminal + strIdTicket.Substring(0, position);
                paramInputConfirmacion=strAutorization +"|"+ strTicketNew;
                resConfirmation = wsNine.InvokeWS_ConfirmacionCambioTarjeta(paramInputConfirmacion, strIdTicket, IdEmpleado, Tienda, ref msgWS);
                result = Convert.ToInt32(resConfirmation == "" ? "0" : resConfirmation);
            }

            return result;

        }

        public int InsertaCarritoCambio(NuevoCambioC9 cambioC9,string idEmpleado, string idTerminal,string idTienda, string idMaquina, ref string idCarrito, ref int iDuplicado)
        {
            int result = 0;
            
            int idArticulo = 0;
            int idPedido = 0;
            int cabDetalle = 0;
            double PVP=0;
            SqlTransaction sqlTrans;
            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            sqlTrans = objCapaDatos.GetTransaction();
            try
            {
                
                
                idArticulo = objCapaDatos.GetIdArticulo(cambioC9.Referencia, ref PVP,ref cabDetalle, sqlTrans);
                if (idCarrito != "")
                    iDuplicado = objCapaDatos.CheckCambiosDuplicados(idArticulo,idCarrito, sqlTrans);
                if (iDuplicado < 1)
                {
                    //insertar pedido
                    result = objCapaDatos.InsertarPedido(idArticulo, idTerminal, PVP, idTienda, idEmpleado, sqlTrans, ref idPedido);
                    if (result > 0)
                    {
                        if (idCarrito == "")
                            result = objCapaDatos.InsertarCarrito(idEmpleado, sqlTrans, ref idCarrito, cambioC9.TarjetaActual, cambioC9.NuevaTarjeta, cambioC9.IdCliente, idMaquina);
                        else
                            result = objCapaDatos.UpdatarCarrito(idEmpleado, sqlTrans, ref idCarrito, cambioC9.TarjetaActual, cambioC9.NuevaTarjeta, cambioC9.IdCliente, idMaquina);

                        if (result > 0)
                        {

                            result = objCapaDatos.InsertaCarritoLinea(idCarrito, idArticulo, PVP, idPedido, sqlTrans, cabDetalle);

                        }
                    }
                }

                if (result > 0)
                    sqlTrans.Commit();
                else
                {
                     sqlTrans.Rollback();
                    log.Error("Realizando Rollback en la creación del carrito para cambios en Cliente 9");
                
                }
            }
            catch(Exception ex){
                log.Error("Exception InsertarCarritoCambio:" + ex.Message.ToString());
                sqlTrans.Rollback();   
            }

            return result;
        } 

        public static string GetURLWsOnline(String ServicioWsOnline)
        {
            ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");

            string address = String.Empty;

            for (int i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name == ServicioWsOnline)
                {
                    address = clientSection.Endpoints[i].Address.ToString();
                    break;
                }
            }

            return address;

        }
        public static bool CheckURLWs(string url, int timeout)
        {
            try
            {
                System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
                //   El timeout es en milisegundos             
                request.Timeout = timeout;
                request.Method = "GET";
                System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse;
                //Regresa TRUE si el codigo de esdado es == 200            
                return (response.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch
            {
                //Si ocurre una excepcion devuelve false             
                return false;

            }
        }

        internal virtual void InvokeUpCliente9Ws(string idTienda, string idCliente,int opcion)
        {

            Int16 Result = 0;
            String DIRFTP = String.Empty;
            String Online = String.Empty;
            string accion = "";
            DateTime fecha = DateTime.Now;
            short opcAccion = Convert.ToInt16(opcion);

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
              
                DataSet Ds = new DataSet();
                ServicioModdoOnline.SModdoOnlineSoapClient _wsOnline;


                objCapaDatos.GetConfOnline(idTienda, ref DIRFTP, ref Online);

                if (Online == "1")
                {
                    String Url = GetURLWsOnline("SModdoOnlineSoap");
                    accion = (opcion == 1) ? "SUBIDA" : "SUBIDA TARJETA";
                    //codigo comentado sobre comprobar una url
                    if (CheckURLWs(Url, 10000))
                    {
                        Ds = objCapaDatos.GeDataSetUpClientes(Convert.ToInt32(idCliente), opcion);
                        _wsOnline = new ServicioModdoOnline.SModdoOnlineSoapClient();
                        Result = _wsOnline.SetUpDownClientes(ref Ds, DIRFTP, fecha, idTienda, idCliente, Convert.ToInt16(opcion));

                        //registramos en el historico web services
                        objCapaDatos.InsertarHistoricoWs(idTienda, fecha, accion, "UP", idCliente, "", DIRFTP, Result);
                    }
                    else
                    {
                        objCapaDatos.InsertarHistoricoWs(idTienda, fecha, accion, "UP", idCliente, "", DIRFTP, 0);

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                objCapaDatos.InsertarHistoricoWs(idTienda, _Fecha, "CLIENTES", "UP", idCliente, "", DIRFTP, Result);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int CambioPlastico(DataSet ds, string strIdTicket, string IdCarrito, string Tienda, string Terminal, string NivelConsolidado, ref string msgWS)
        {
            int result = 0;
            string TjtActual = "";
            string TjtConsolidada = "";
            string IdTarjetaAnterior = "";
            bool esCambio = false;
            string NivelActual = "";
            string IdEmpleado = "";
            string cadConexion = "";
            string IdCliente = "";
           
            SqlTransaction sqlTrans;

            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            sqlTrans = objCapaDatos.GetTransaction();
            try
            {
                cadConexion = sqlTrans.Connection.ConnectionString;
                result = objCapaDatos.TryAbreConexion(ref sqlTrans);

                esCambio = ValidaVentaPlastico(ds, ref NivelActual, NivelConsolidado);
                if (esCambio)
                {

                    if (ObtieneDatosTarjeta(IdCarrito, ref TjtActual, ref TjtConsolidada, ref IdEmpleado, ref IdCliente, ref IdTarjetaAnterior,sqlTrans) > 0)
                    {
                        int tipoCambio = getIdUpgrade(NivelActual.ToUpper(), NivelConsolidado.ToUpper());
                        result = CambioTjTWs(IdEmpleado, strIdTicket, IdCarrito, Terminal, Tienda, TjtActual, TjtConsolidada, tipoCambio, ref msgWS, cadConexion);
                        if (result > 0)
                        {
                            result = objCapaDatos.TryAbreConexion(ref sqlTrans);
                            result = objCapaDatos.ActualizaDatosCambioBD(IdCliente, IdEmpleado, Terminal, Tienda, TjtActual, IdTarjetaAnterior, NivelActual, sqlTrans);
                            if (result > 0)
                            {
                                sqlTrans.Commit();
                                objCapaDatos.TryCierraConexion();

                                //se sube los datos al online
                                try
                                {
                                    InvokeUpCliente9Ws(Tienda, IdCliente, 3);
                                }
                                catch (Exception ex)
                                {
                                    msgWS = "TODO OK. Pero no se pudo subir el cliente al WS ONLINE";
                                    log.Error("Error al subir el cliente al ws online." + ex.Message.ToString());
                                }


                            }
                            else sqlTrans.Rollback();

                        }
                    }

                }
                objCapaDatos.TryCierraConexion();
            }
            catch (Exception ex) {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            return result;
            
        
        }
        
        public int ValidaCambiosTJT(string idCarrito, string referencias, ref int NumArticulos)
        {
         int result = 0;
            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = StrCadenaConexion;
                result = objCapaDatos.GetItemCarrito(idCarrito, referencias, ref NumArticulos);
            }
            catch (Exception ex)
            {
                log.Error("Exception CompruebaIdOk." + ex.Message.ToString());
            }
            return result;
    
        }
        public int ObtieneDatosTarjeta(string IdCarrito, ref string  TarjetaActual, ref string TarjetaConsolidada, ref string IdEmpleado , ref string IdCliente, ref string idTarjetaAnterior,SqlTransaction sqlTrans ){
        int result = 0;
        try
        {
            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = StrCadenaConexion;
            result = objCapaDatos.GetDatosTarjeta(IdCarrito, ref TarjetaActual, ref TarjetaConsolidada, ref IdEmpleado, ref IdCliente, ref idTarjetaAnterior,sqlTrans);
        }
        catch (Exception ex)
        {
            log.Error("Exception CompruebaIdOk." + ex.Message.ToString());
        }
        return result;
    }

        public int CambioTjTWs(string strIdTicket,string IdEmpleado, string idCarrito, string IdTerminal, string IdTienda, string tjtActual, string tjtConsolidada, int tipoCambio,ref string msgWS, string cadConexion) {

            int result = 0;
            result = EnviaCambioTjTWs9(IdEmpleado, strIdTicket, idCarrito, IdTerminal, IdTienda, tjtActual, tjtConsolidada, tipoCambio, ref msgWS, cadConexion);
            return result;
        }
        public int EnviaCambioTjTWs9(string IdEmpleado, string strIdTicket, string idCarrito, string IdTerminal, string IdTienda, string tjtActual, string tjtConsolidada, int tipoCambio,ref string  msgWS, string cadConexion )
        {
            int result = 0;
            result = ws_CambioTJT( IdEmpleado,strIdTicket, IdTienda, IdTerminal, tjtActual, tjtConsolidada, tipoCambio.ToString(), ref msgWS, cadConexion);

            return result;
        }
        public bool ValidaVentaPlastico(DataSet ds, ref string NivelAct, string NivelConsolidado)
        {
            bool result = false;
            
            foreach (DataRow data in ds.Tables[1].Rows)
            {
                bool esCambio = false;
                esCambio = EsPlasticoC9(data["CodigoAlfa"].ToString(), ref NivelAct);
                if (esCambio)
                {
                    if (NivelAct == "") NivelAct = NivelConsolidado;
                    result = true;
                }
            }
            return result;
        }

        public bool EsPlasticoC9(string strReferencia, ref string NivelActual) 
        {
                         
            bool result = false;
            switch(strReferencia)
            {
                case "108303000332":   
                    result = true;
                    NivelActual = "";
                    break;
                case "750000000161":
                    result = true;
                    NivelActual="SHOELOVER";
                    break;
                case "750000000291":
                    result = true;
                    NivelActual = "";
                    break;
                case "750000000436":
                    result = true;
                    NivelActual="BASICO";
                    break;
                case "750000000437":
                    result = true;
                    NivelActual= "FIRST SHOELOVER";
                    break;
            }
            return result;
        
        }
        public int getIdUpgrade(string NivelActual, string NivelConsolidado)
        { 
         int result = 0;
         if (NivelConsolidado == "BÁSICO") NivelConsolidado = "BASICO";
         if (NivelActual == "BÁSICO") NivelActual = "BASICO";
            //nivel actual es el que nos ha devuelto el ws al que es aspirante
            if (NivelActual == NivelConsolidado)
            {
                result = 1;       //*** reemplazo
            }
            else{
                switch (NivelActual.ToUpper())
                {
                    case "FIRST SHOELOVER":
                         if ((NivelConsolidado == "CLIENTE 9")|| (NivelConsolidado == "BASICO")  )result =5; //'*** Downgrade basica a first shoelover
                         break;
                    case "FIRSTSHOELOVER":
                         if ((NivelConsolidado == "CLIENTE 9") || (NivelConsolidado == "BASICO")) result = 5; //'*** Downgrade basica a first shoelover
                         break;
                    case "SHOELOVER":
                         if ((NivelConsolidado == "CLIENTE 9") || (NivelConsolidado == "BASICO")) result = 2; //*** Upgrade basica a shoelover
                         else if (NivelConsolidado == "FIRST SHOELOVER") result = 4; //*** Upgrade first shoelover a shoelover
                         break;
                    case "CLIENTE 9":
                        if (NivelConsolidado == "FIRST SHOELOVER") result =3; //*** Upgrade first shoelover a basica
                        break;
                    case "BASICO":
                        if (NivelConsolidado == "FIRST SHOELOVER") result = 3; //*** Upgrade first shoelover a basica
                        break;
                }
            }
            return result;
        
        }
        #endregion
    }
}
