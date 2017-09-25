using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DLLGestionVenta.Models;

namespace CapaDatos
{
    public partial class ClsCapaDatos
    {

        public Int16 Save_Hist_HistoricoWS(long lngEmpleado, string strMetodoWS, string strEntrada, string StrSalida, string strEstado, long intError, string strTicket, string Tienda, DateTime FechaSesion)
        {
            try
            {
                string StrSQl;
                long intReintentos = 0;
                bool blnMetodoReintento = false;
                string strObs = "";

                //StrSQl = "INSERT INTO [AVE_CARRITO_PAGOS] ([IdCarrito],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe]) values (";
                //StrSQl += _Pago.IdCarrito + ",'" + _Pago.TipoPago + "','" + _Pago.TipoPagoDetalle + "','" + _Pago.NumTarjeta + "',";
                //StrSQl += _Pago.Importe + ")";


                switch (strMetodoWS)
                {
                    case "CONFIRMAOPERACION":
                    case "ENVIATICKET":
                    case "ALTASOCIO":
                    case "ACTUALIZASOCIO":
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


                switch (strMetodoWS)
                {
                    case "ENVIATICKET":
                    case "ALTASOCIO":

                        if (strEstado == "0")
                        {
                            StrSQl = "UPDATE WS_HISTORICO SET Estado='0' WHERE Metodo='" + strMetodoWS + "' ";
                            StrSQl += " AND IdTienda='" + Tienda + "' AND Entrada='" + strEntrada + "' AND Estado='1'";
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


    }
}
