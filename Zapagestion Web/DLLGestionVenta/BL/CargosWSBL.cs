using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DLLGestionVenta.ServicioModdoOnline;
using DLLGestionVenta.CapaDatos;
using log4net;
using System.Reflection;
using ModdoOnline.Utiles;
using System.Data.SqlClient;

namespace DLLGestionVenta.BL
{
    class CargosWSBL
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static int InvokeGetCargoWs(int idCargo, string idTienda)
        {
            Log.Info(Traza.INICIO);

            try
            {
                DataSet DSOUT = new DataSet();
                DataTable tbCargo = new DataTable("NumCargo");
                DataRow fila = null;
                string keyNew = idCargo.ToString();

                int result = 0;

                tbCargo.Columns.Add("IdCargo", System.Type.GetType("System.Int64"));
                tbCargo.Columns.Add("IdCargoOut", System.Type.GetType("System.Int64"));

                fila = tbCargo.NewRow();
                fila[0] = idCargo;
                tbCargo.Rows.Add(fila);

                DSOUT.Tables.Add(tbCargo);

                SModdoOnlineSoapClient objSModdo = new SModdoOnlineSoapClient();

                result = objSModdo.Get_Entidades_GENERIC(ref DSOUT, idCargo.ToString(), Entidades.GetNumCargo, DateTime.Today, "NINEWESTMEX|8vJSRsOHMZg9u9Ir", idTienda, 0, 0);

                if (result == 1)
                {
                    if (DSOUT.Tables[0].Rows.Count > 0)
                    {
                        if (Int64.Parse(DSOUT.Tables[0].Rows[0][0].ToString()) != Int64.Parse(DSOUT.Tables[0].Rows[0][1].ToString()))
                        {
                            keyNew = DSOUT.Tables[0].Rows[0][1].ToString();
                        }
                    }
                }

                return Convert.ToInt32(keyNew);

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

        public static void InvokeUpCargosWs(Int16 Retif,int idCargoWs, string idTienda)
        {
            
            int Result;

            try
            {
                DataSet Ds = new DataSet();

                Result = 0;

                Ds = CargosWSDAL.GetDataSetCargos(Convert.ToInt64(idCargoWs), idTienda , DateTime.Today, Retif);

                Ds = CargosWSDAL.GetFormatoFechaSRegional(Ds);

                SModdoOnlineSoapClient  objSModdo = new SModdoOnlineSoapClient();

                Result = objSModdo.SetupCargos(ref Ds, "NINEWESTMEX|8vJSRsOHMZg9u9Ir", DateTime.Today, idTienda, idCargoWs.ToString());

                //registramos en el historico web services???
                //objCapaDatosCargos.InsertarHistoricoWs(ParametrosEntrada, Result);

            }
            catch (Exception ex)
            {
                //objCapaDatosCargos.InsertarHistoricoWs(ParametrosEntrada, Result);
                throw ex;

            }
            finally
            {
                Log.Info(Traza.FINAL);
            }
        }
    }
}
