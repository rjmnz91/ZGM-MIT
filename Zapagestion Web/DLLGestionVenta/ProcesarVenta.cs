using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DLLGestionVenta.Models;
using CapaDatos;
using Cliente9;
using AVEGestorPromociones.GestorPromociones;
using System.Net.NetworkInformation;
using System.ServiceModel.Configuration;
using System.Configuration;
using DLLGestionCliente9;

namespace DLLGestionVenta
{
    public class ProcesarVenta
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProcesarVenta));

        String StrCadenaConexion;
        String idTerminal;
        DateTime _Fecha;
        ClsCapaDatos objCapaDatos;
        VENTA _Venta;
        float iva;
        const String PaswordWs = "8vJSRsOHMZg9u9Ir";

        //ADRIANA ZAMORA GONZALEZ 23/06/2017
        //Agregar campo para cambio de efectivo, Kayako [!?]
        double _cambioEfectivo;

        #region Propiedades y constructor
        /// <summary>
        /// Dll para generar el proceso de la venta
        /// </summary>
       public ProcesarVenta() 
       {
        
       }
       /// <summary>
       /// Conexion de la bbbdd
       /// </summary>
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

       //ADRIANA ZAMORA GONZALEZ 23/06/2017
       //Agregar campo para cambio de efectivo, Kayako [!?]
       public double cambioEfectivo
       {
           set { _cambioEfectivo = value; }
       }       

       #endregion
  

       #region "Carrito"

       public int AniadeCarrito(string IdCliente, string IdEmpleado, string Maquina)
       {
           int result = 0;

           try
           {
               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               result = objCapaDatos.AniadeCarrito(IdEmpleado, IdCliente, Maquina);
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
           return result;


       }
       public int AniadeArticuloCarrito(int idCarrito, string idReferencia, string IdTienda, string IdEmpleado,ref string strError) {
           int result = 0;
           try 
           { 
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = StrCadenaConexion;
                result=objCapaDatos.AniadeArticuloCarrito(idCarrito,idReferencia,IdTienda,IdEmpleado, ref strError);
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
           return result;
       
       }
       public String PagoCarritoNotaEmpleado(Carrito_Pago _Pago, bool PagadoOk = true)
       {
           try
           {
               String Result="0";
               double LimiteCredito = 0;
               double ImporteConsumido = 0; 

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               LimiteCredito = objCapaDatos.GetLimiteCredito(_Pago.IdCarrito);
               ImporteConsumido =objCapaDatos.ComprobarLimiteCredito(_Fecha,_Pago.IdCarrito);

               if (ImporteConsumido > LimiteCredito)
               {
                   Result = "El Monto de la venta supera el Límite del credito mensual. ";
               }
               else
               {
                   Result = objCapaDatos.RegistrarPagoCarrito(_Pago, PagadoOk).ToString();
               } 
               
               return Result;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }

       public Int64 PagoCarritoEfectivo(Carrito_Pago _Pago, bool PagadoOk = true)
       {
           try
           {
               Int64 Result;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Result = objCapaDatos.RegistrarPagoCarritoEfectivo(_Pago, PagadoOk);

               return Result;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }
        public Int64 PagoCarrito(Carrito_Pago _Pago, bool PagadoOk = true )
       {
           try
           {
               Int64 Result;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Result = objCapaDatos.RegistrarPagoCarrito(_Pago, PagadoOk);

               return Result; 

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }

        public int GetPagoEfectivo() {
            int tipoPagoMoney = 0;
            try
            {
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = StrCadenaConexion;
                tipoPagoMoney = objCapaDatos.GetPagoEfectivo();
                return tipoPagoMoney;
            }
            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
            

        
        }
        public int GetArticulosCarrito(string idCarrito){
            int cantidadArticulos = 0 ;
            try
            {
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = StrCadenaConexion;
                cantidadArticulos = objCapaDatos.GetArticulosCarrito(idCarrito);
                return cantidadArticulos;
            }
            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);
            }
            
        }
       public DataSet GetClienteCarritoActual(Int64 IdCarrito)
       {
           try
           {
               DataSet Ds;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Ds = objCapaDatos.GetClienteCarrito(IdCarrito);

               return Ds;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }  
       public bool ValidarPago(Int64 IdCarritoPago, string foliocpagos, string auth, string cc_number,string tipopagodetalle)
       {
           bool bRet = true;


           try
           {

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               bRet = objCapaDatos.ValidarPago(IdCarritoPago, foliocpagos, auth, cc_number,tipopagodetalle);

           }
           catch (Exception ex)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
               bRet = false;
               throw new Exception(ex.Message, ex.InnerException);
           }
           return bRet;
       }

       public DataSet GetPago(Int64 IdCarrito)
       {
           try
           {
               DataSet Ds;
               
               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Ds = objCapaDatos.GetPagoCarrito(IdCarrito);

               return Ds; 

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }

       public Int16 GetPedidoCarrito(Int64 idCarrito, ref List<String> lstSolicitudes)
       {
           try
           {
               String StrSql = String.Empty;
               DataSet Ds;

               StrSql = "select idPedido from AVE_CARRITO_LINEA where id_Carrito=" + idCarrito.ToString();

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Ds = objCapaDatos.GEtSQLDataset(StrSql);

               foreach (DataRow dr in Ds.Tables[0].Rows)
               {
                   lstSolicitudes.Add(dr[0].ToString());

               }
               return 1;
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }

       #endregion
         
       #region TIckect Venta
       public String EjecutaVenta(VENTA objVenta)
       {
           try
           {
               String StrResult;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;
               _Venta = objVenta;
               StrResult = EjecutaVenta();
               return StrResult;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }

       public String EjecutaVenta(Int64 IdCarrito, CLIENTE9 Client, DateTime FechaSesion, string comentario, ref string msgWS)
       {
           try
           {
               String StrResult;
               DataSet Ds;
               DataView Dv;
               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;


               if (!objCapaDatos.ComprobarCarritoCerrado(IdCarrito)) { return "ERROR" ; }
               
               Ds = objCapaDatos.GetCarritoActual(IdCarrito);
                              
               
               _Venta = GEtobjVenta(Ds, comentario);
               
               //ACL. 08-07-2014.Se asigna el comentario para que se grabe en n_tickets.
               _Venta.Comentarios = comentario;

               Dv = Ds.Tables[2].DefaultView; 
               Dv.RowFilter="TipoPago in ('PAR 9','BOLSA 5','PUNTOS NINE')";

               StrResult = EjecutaVenta(Dv,Client);

               //ACL AQUI VENDRÍA LA COMPROBACION SI EN EL TICKET VIENE UN CAMBIO DE PLASTICO
               ClsCliente9 objNine = new ClsCliente9();
               objNine.ConexString = StrCadenaConexion;
               
               if (Client.NivelActual != null)
               {
                    int result = objNine.CambioPlastico(Ds, StrResult, IdCarrito.ToString(), _Venta.Id_Tienda.ToString(), _Venta.ID_TERMINAL.ToString(), Client.NivelActual.ToString(), ref  msgWS);
                    //ACL
                    result = OperacionesPendientesCambioTJT();
               }
               objCapaDatos.ActualizarCarrito(IdCarrito, StrResult, FechaSesion); 
               //se sube al online la nueva tarjeta de fidelidad.
               InvokeUpCliente9Ws(3);
               //llamada pasa subir el ticket a la web services del online
               
               InvokeUpVentaWs();

               return StrResult;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(string.Format("Excepcion: {0} ---- {1}", sqlEx.Message, sqlEx.StackTrace), sqlEx.InnerException);
           }
       }

        
        //Comprueba en el carrito el total de los articulos que son thinkRetail.
        
        public double ObtieneTotalArticulosTR(string idCarrito){
            double total =0;
            DataSet ds;
           
            try
            {
                objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = StrCadenaConexion;
                ds = objCapaDatos.GetTotalArtTR(idCarrito);
                if( ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in ds.Tables[0].Rows)
                    {
                        total = Convert.ToDouble(dt["TotalTR"].ToString());
                    }
                }
                
            }
            catch (Exception ex) { 
            
            }

            return total;


        }
        public String EjecutaVenta(Int64 IdCarrito, CLIENTE9 Client, DateTime FechaSesion, string comentario, decimal gastosEnvio,ref string msgWS, string Entorno = "" )
       {
           try
           {
               String StrResult;
               DataSet Ds;
               DataView Dv;
               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;


               if (!objCapaDatos.ComprobarCarritoCerrado(IdCarrito)) { return "ERROR"; }

               Ds = objCapaDatos.GetCarritoActual(IdCarrito);


               _Venta = GEtobjVenta(Ds, comentario, Entorno);

               //ACL. 08-07-2014.Se comenta la asignación, ya que en el metodo anterior, se esta metiendo a piñon 
               //fijo un comentario.En el caso de que no fuera necesario el comentario, se podría habilitar.
               //_Venta.Comentarios = comentario;

               Dv = Ds.Tables[2].DefaultView;
               Dv.RowFilter = "TipoPago in ('PAR 9','BOLSA 5','PUNTOS NINE')";

               StrResult = EjecutaVenta(Dv, Client);
               //ACL AQUI VENDRÍA LA COMPROBACION SI EN EL TICKET VIENE UN CAMBIO DE PLASTICO
               if (Client.NivelActual != null)
               {

                   ClsCliente9 objNine = new ClsCliente9();

                   objNine.ConexString = StrCadenaConexion;
                   int result = objNine.CambioPlastico(Ds, StrResult, IdCarrito.ToString(), _Venta.Id_Tienda.ToString(), _Venta.ID_TERMINAL.ToString(), Client.NivelActual.ToString(), ref  msgWS);

                   result = OperacionesPendientesCambioTJT();
               }
               //ACL
               objCapaDatos.ActualizarCarrito(IdCarrito, StrResult, FechaSesion, gastosEnvio);
               //se sube al online la nueva tarjeta de fidelidad.
               InvokeUpCliente9Ws(3);
               //llamada pasa subir el ticket a la web services del online
               
               InvokeUpVentaWs();

               return StrResult;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(string.Format("Excepcion: {0} ---- {1}", sqlEx.Message, sqlEx.StackTrace), sqlEx.InnerException);
           }
       }
       private int ConfirmaOperacionDevo(string FPago, string strautorizacion, cls_Cliente9 C9) {
           int result = 0;
           int tipo = 0;
           string tipoOperacion = "1";
          
           if (FPago.Equals("PAR 9"))
           {
               tipo = 2;
           }
           else
           {
               if (FPago.Equals("BOLSA 5"))
               {
                   tipo = 3;
               }
               else
               {
                   tipo = 1;
               }

              string  resultWS =C9.Invoke_ConfirmacionDevo(tipo, strautorizacion, tipoOperacion);
              result = Convert.ToInt32(resultWS);
           }
           return result;
       }
       
      private int OperacionesPendientesCambioTJT()
       {
           int result = 0;
           try
           {

               cls_Cliente9 c9 = new cls_Cliente9(_Venta);
               c9.ConexString = StrCadenaConexion;
               c9.InvokeWS_OperacionesPendientes(-2, "", false);

           }
           catch (Exception ex) {
               log.Error("Error en operaciones pendidentes:" + ex.Message.ToString());
           
           }
           return result;

       }
       private double ConfirmarRedenciones(DataView DvCLiente9,ref CLIENTE9 objClient)
       {
           try
           {
               double dblImporte = 0;

                objClient.ParesRedimidos = 0;
                objClient.NumConfirmaPar9 = 0;
                objClient.BolsasRedimidas = 0;
                objClient.NumConfirmaBolsa5 = 0;
                objClient.PuntosRedimidos = 0;
                objClient.NumConfirmaPuntos9 = 0;

                                                     
               foreach (DataRowView dr in DvCLiente9)
               {
                   dblImporte +=double.Parse(dr["Importe"].ToString());

                    cls_Cliente9 c9 = new cls_Cliente9(_Venta);
                    c9.ConexString = StrCadenaConexion;
                    
                   if (dr["TipoPago"].ToString().Equals("PAR 9"))
                   {
                       objClient.ParesRedimidos = 1;
                       objClient.NumConfirmaPar9 = int.Parse(dr["NumTarjeta"].ToString());
                       //c9.ProcesarRespuesta__ConfirmaOperacion("2" + "|" + dr["NumTarjeta"].ToString() + "|1");
                       c9.InvokeWS_OperacionesPendientes(2, dr["NumTarjeta"].ToString(), false);

                   }
                   else
                   {
                       if (dr["TipoPago"].ToString().Equals("BOLSA 5"))
                       {
                           c9.InvokeWS_OperacionesPendientes(3, dr["NumTarjeta"].ToString(),false );
                           objClient.BolsasRedimidas = 1;
                           objClient.NumConfirmaBolsa5 =int.Parse(dr["NumTarjeta"].ToString());


                       }
                       else
                       {
                           //Puntos nine
                           c9.InvokeWS_OperacionesPendientes(1, dr["NumTarjeta"].ToString(),false);
                           objClient.PuntosRedimidos = double.Parse(dr["importe"].ToString());
                           objClient.NumConfirmaPuntos9 = int.Parse(dr["NumTarjeta"].ToString());
                       }  

                   } 
               }

               return dblImporte; 
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }
       
       public VENTA GEtobjVenta(DataSet Ds, string comment, string strEntorno="")
       {
           try
           {
               VENTA objVenta;
               List<VENTA_DETALLE> LstVentaDetalle;
               List<VENTA_PAGOS> LstPagos;
               List<NTRANS> lstTrans; 
               int i;
               DataView DvPAgosC9;
               NTRANS _trans;
               bool articuloValido = true;
               double TotalEurosTR=0;


               objVenta = new VENTA();
               LstVentaDetalle=new List<VENTA_DETALLE>();
               LstPagos = new List<VENTA_PAGOS>();
               lstTrans = new List<NTRANS>();
               int posC = 0;


               foreach (DataRow dr in Ds.Tables[0].Rows)
               {
                   

                   _trans = new NTRANS();
                   posC = 0;
                   posC = comment.IndexOf(("MODALIA"));

                   objVenta.ID_TERMINAL = idTerminal;
                   objVenta.Id_Tienda = dr["Tienda"].ToString();
                   objVenta.Fecha = _Fecha;
                   objVenta.Id_Cliente_N = int.Parse(dr["id_Cliente"].ToString());
                   objVenta.Id_Empleado = int.Parse(dr["Usuario"].ToString());
                   objVenta.Id_Abonado_Empleado = 0;
                   objVenta.TotalEuro = double.Parse(dr["Total"].ToString());
                   objVenta.IdCajero = int.Parse(dr["Usuario"].ToString());
                  
                    objVenta.DescuentoEuro=0;
                   objVenta.cCuentaEuro = 0;
                   objVenta.cEfectivoEuro = 0;
                   objVenta.TotalParaValeEuro = 0;
                   objVenta.cRecibidoEuro = 0;
                   objVenta.ComisionVenta = 0;
                   objVenta.NumFactura = string.Empty;
                   if (posC >= 0)
                       objVenta.Comentarios =  comment;
                   else
                        objVenta.Comentarios ="VENTA AVE -"  + dr["idcarrito"].ToString()+ ". "+ comment;
                   objVenta.CodPostal = string.Empty;
                   objVenta.IdMonedaCliente = string.Empty;
                   objVenta.IdMonedaTienda = string.Empty;
                   objVenta.CambioTienda = null;
                   objVenta.CIFSociedad = string.Empty;
                   objVenta.cTarjetaEuro = 0;
                   objVenta.NombreTarjeta = String.Empty;
                   objVenta.cValeEuro = 0;
                   objVenta.Id_Pago = 3;
                   objVenta.Id_SubPago = "3";

                   if (dr["TarjetaCliente"].ToString().Length>0)
                   {
                       objVenta.DeslizaTarjeta = true;
                   }
                   else
                   {
                       objVenta.DeslizaTarjeta = false;
                   }   
                                      

                   _trans.Concepto = (objVenta.Id_Cliente_N > 0 ? "Ventas a Clientes con Factura" : "Ventas a Clientes");
                   _trans.FechaSesion = objVenta.Fecha;
                   _trans.Importe = objVenta.TotalEuro;

                   lstTrans.Add(_trans);

               }

               
               i=0;

               TotalEurosTR = objVenta.TotalEuro;
               foreach (DataRow dr in Ds.Tables[1].Rows)
               {
                   i++;
                   
                   VENTA_DETALLE _ventaDetalle;
                   _ventaDetalle =new VENTA_DETALLE();
                   _ventaDetalle.Pvp_Or =double.Parse(dr["PVPORI"].ToString());
                   _ventaDetalle.Pvp_Vig = double.Parse(dr["PVPACT"].ToString());
                   _ventaDetalle.Id_Articulo = int.Parse(dr["idArticulo"].ToString());
                   _ventaDetalle.Id_cabecero_detalle = int.Parse(dr["id_Cabecero_Detalle"].ToString());
                    _ventaDetalle.id_Carrito_Detalle = int.Parse(dr["id_Carrito_Detalle"].ToString());
                    _ventaDetalle.Coste_Articulo = double.Parse(dr["PrecioCostoReal"].ToString()); 
                   _ventaDetalle.ImporteEuros = double.Parse(dr["PVPACT"].ToString());
                   _ventaDetalle.Estado = short.Parse(( int.Parse(dr["Cantidad"].ToString())).ToString());
                   _ventaDetalle.MotivoCambioPrecio = String.Empty;
                   _ventaDetalle.IdPosicion = i;
                   _ventaDetalle.DtoEuroArticulo = 0;
                   _ventaDetalle.ComisionPremio = 0;
                   _ventaDetalle.motivo_devolucion = "";
                   _ventaDetalle.IdConcesionTienda = 0;
                   _ventaDetalle.Asesor = String.Empty;
                   _ventaDetalle.ComisionAsesor = 0;
                   _ventaDetalle.Comentarios = "Solicitud AVE-" + dr["idpedido"].ToString() + ". " + comment;
                   _ventaDetalle.CorteArt = dr["TipoCorte"].ToString();

                   if (strEntorno.ToUpper() == "TR") {
                       objCapaDatos = new ClsCapaDatos();
                       objCapaDatos.ConexString = StrCadenaConexion;
                       articuloValido = objCapaDatos.GetArtValido(_ventaDetalle.Id_Articulo.ToString());
                   
                   }
                   
                   if (articuloValido)
                   {
                       if (Convert.ToDouble(dr["DTOArticulo"].ToString()) > 0)
                       {

                           GetGenerarDescuentoPromo(ref objVenta, ref _ventaDetalle, Convert.ToDouble(dr["DTOArticulo"].ToString()), dr["Promocion"].ToString(), int.Parse(dr["idPromocion"].ToString()));

                       }


                      
                   }
                   else {
                       if (TotalEurosTR > 0 && TotalEurosTR > _ventaDetalle.ImporteEuros)
                           TotalEurosTR = objVenta.TotalEuro -_ventaDetalle.ImporteEuros;
                   }
                   LstVentaDetalle.Add(_ventaDetalle);
                   
               }


               DvPAgosC9 = Ds.Tables[2].DefaultView;

               DvPAgosC9.RowFilter = "TipoPago in ('PAR 9','BOLSA 5', 'PUNTOS NINE')";
        
               double TotalEuro = objVenta.TotalEuro;
               double TotalEuroDesc = TotalEuro;
               double totalEuroDescTR=  TotalEurosTR;

               foreach (DataRowView dr9 in DvPAgosC9)
               {

                   VENTA_DETALLE _vd = LstVentaDetalle[0]; 
                   
                   switch (dr9["TipoPago"].ToString())
                   {
                       case "PAR 9":
                           GetGenerarDescuento(ref objVenta, ref _vd, dr9, totalEuroDescTR);
                           break;
                       case "BOLSA 5":
                           GetGenerarDescuento(ref objVenta, ref _vd, dr9, totalEuroDescTR);
                           break;
                       case "PUNTOS NINE":
                           LstVentaDetalle = ProrratearCliente9(ref objVenta, LstVentaDetalle, double.Parse(dr9["Importe"].ToString()), TotalEurosTR, totalEuroDescTR);
                           break;
                   }

                   TotalEuro -= double.Parse(dr9["Importe"].ToString());
                   TotalEurosTR -= double.Parse(dr9["Importe"].ToString());
               }    

               //DvPAgosC9.RowFilter = "TipoPago='PUNTOS NINE'";  
          
               //if (DvPAgosC9.Count>0)
               //{
                 
               //    double TotalEuro=objVenta.TotalEuro;
               //    LstVentaDetalle= ProrratearCliente9(ref objVenta, LstVentaDetalle,  double.Parse(DvPAgosC9[0]["Importe"].ToString()),TotalEuro); 

               //}
               
               
               i=0;


               DvPAgosC9.RowFilter = "TipoPago not in ('PAR 9','BOLSA 5','PUNTOS NINE')";

               string tipPago = "";
               foreach (DataRowView dr in DvPAgosC9)
               {
                   VENTA_PAGOS VP;
                   VP = new VENTA_PAGOS();

                   i++;
                   
                   VP.Fecha = objVenta.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                   VP.Importe = double.Parse(dr["importe"].ToString());
                   VP.NomEntidad = "";
                   VP.NomTitular="";
                   VP.TipoOrigen = "VENTA";
                   VP.Divisa = "MXN";
                   VP.OtraDivisa = "";
                   VP.OtraDivisaCambio = 0;
                   VP.OtraDivisaImporte = 0;
                   VP.IdOrden = i;
                   VP.IdConcesionTienda = 0;
                   VP.CuotaIVA = 0;
                   VP.Id_Cliente = objVenta.Id_Cliente_N;
                   VP.ValeId = 0;
                   VP.ValeTienda = string.Empty;
                   VP.Visto_Pago=String.Empty;
                   VP.RefFP = String.Empty;


                   if (dr["TipoPAgo"].ToString() == System.Configuration.ConfigurationManager.AppSettings["TarjetaTipo"].ToString())
                   {
                       VP.Tipo = "TARJETA/CARD";
                       tipPago = "TARJETA/CARD";
                       objVenta.NombreTarjeta += dr["TipoPagoDetalle"].ToString() + ",";
                       objVenta.cTarjetaEuro += double.Parse(dr["importe"].ToString());
                       objVenta.Id_Pago = 2;
                       objVenta.Id_SubPago = "2";
                       VP.IdFP = 13;
                       VP.FPago = dr["TipoPAgo"].ToString();
                       VP.FPagoDetalle = dr["TipoPagoDetalle"].ToString();
                       VP.NumTarjeta =  dr["NumTarjeta"].ToString();
                       VP.NumTarjetaAutoriza = dr["Auth"].ToString();
                       VP.NumTarjetaOperacion = dr["Foliocpagos"].ToString();
                       objVenta.cValeEuro = 0;
                       objVenta.cRecibidoEuro += double.Parse(dr["importe"].ToString());
                   }
                   else
                   {

                       if (dr["TipoPAgo"].ToString() == "EFECTIVO")
                       {
                           VP.Tipo = "EFECTIVO/CASH";
                           
                           objVenta.cEfectivoEuro = double.Parse(dr["importe"].ToString());
                           if (tipPago == "")
                           {
                               objVenta.Id_Pago = 1;
                               objVenta.Id_SubPago = "1";
                               VP.IdFP = 1;
                           }
                           else
                           {
                               objVenta.Id_Pago = 3;
                               objVenta.Id_SubPago = "3";
                               VP.IdFP = 12;
                           }
                          
                           VP.FPago = dr["TipoPago"].ToString();
                           VP.FPagoDetalle = "";
                           VP.NumTarjeta = "";
                           VP.NumTarjetaAutoriza = "";
                           VP.NumTarjetaOperacion = "";
                           objVenta.NombreTarjeta = "";
                           objVenta.cTarjetaEuro = 0;
                           objVenta.cRecibidoEuro += double.Parse(dr["importe"].ToString());

                           //ADRIANA ZAMORA GONZALEZ 23/06/2017
                           //Agregar campo para cambio de efectivo, Kayako [!?]
                           if (_cambioEfectivo > 0)
                           {
                               VENTA_PAGOS VCambio;
                               VCambio = new VENTA_PAGOS();
                               //VCambio = VP;
                               //VCambio.IdOrden = VCambio.IdOrden + 1;
                               //VCambio.Importe = _cambioEfectivo * -1;
                               //VCambio.TipoOrigen = "VUELTA";

                               VCambio.Fecha = objVenta.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                               VCambio.Importe = _cambioEfectivo * -1;
                               VCambio.NomEntidad = "";
                               VCambio.NomTitular = "";
                               VCambio.TipoOrigen = "VUELTA";
                               VCambio.Divisa = "MXN";
                               VCambio.OtraDivisa = "";
                               VCambio.OtraDivisaCambio = 0;
                               VCambio.OtraDivisaImporte = 0;
                               VCambio.IdOrden = i+1;
                               VCambio.IdConcesionTienda = 0;
                               VCambio.CuotaIVA = 0;
                               VCambio.Id_Cliente = objVenta.Id_Cliente_N;
                               VCambio.ValeId = 0;
                               VCambio.ValeTienda = string.Empty;
                               VCambio.Visto_Pago = String.Empty;
                               VCambio.RefFP = String.Empty;

                               VCambio.Tipo = "EFECTIVO/CASH";

                               //objVenta.cEfectivoEuro = double.Parse(dr["importe"].ToString());
                               if (tipPago == "")
                               {
                                   //objVenta.Id_Pago = 1;
                                   //objVenta.Id_SubPago = "1";
                                   VCambio.IdFP = 2;
                               }
                               else
                               {
                                   //objVenta.Id_Pago = 3;
                                   //objVenta.Id_SubPago = "3";
                                   VCambio.IdFP = 13;
                               }

                               VCambio.FPago = dr["TipoPago"].ToString();
                               VCambio.FPagoDetalle = "";
                               VCambio.NumTarjeta = "";
                               VCambio.NumTarjetaAutoriza = "";
                               VCambio.NumTarjetaOperacion = "";
                               //objVenta.NombreTarjeta = "";
                               //objVenta.cTarjetaEuro = 0;
                               //objVenta.cRecibidoEuro += double.Parse(dr["importe"].ToString());

                               LstPagos.Add(VCambio);
                           }
                           

                       }
                       else
                       {
                           VP.Tipo = "OTRAS/OTHERS";
                           objVenta.cValeEuro = double.Parse(dr["importe"].ToString());
                           tipPago = "OTRAS";
                           objVenta.Id_Pago = 3;
                           objVenta.Id_SubPago = "3";
                           VP.IdFP = 12;
                           VP.FPago = dr["TipoPago"].ToString();
                           VP.FPagoDetalle = "";
                           VP.NumTarjeta = "";
                           VP.NumTarjetaAutoriza = "";
                           VP.NumTarjetaOperacion = "";
                           objVenta.NombreTarjeta = "";
                           objVenta.cTarjetaEuro = 0;
                           objVenta.cRecibidoEuro += double.Parse(dr["importe"].ToString());
                       }
                   } 

                   LstPagos.Add(VP);
               }

               //metemos un registro 
               if (DvPAgosC9.Count == 0)
               {
                   VENTA_PAGOS VP;
                   VP = new VENTA_PAGOS();

                   i++;

                   VP.Fecha = objVenta.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                   VP.Importe =0;
                   VP.NomEntidad = "";
                   VP.NomTitular = "";
                   VP.TipoOrigen = "VENTA";
                   VP.Divisa = "MXN";
                   VP.OtraDivisa = "";
                   VP.OtraDivisaCambio = 0;
                   VP.OtraDivisaImporte = 0;
                   VP.IdOrden = i;
                   VP.IdConcesionTienda = 0;
                   VP.CuotaIVA = 0;
                   VP.Id_Cliente = objVenta.Id_Cliente_N;
                   VP.ValeId = 0;
                   VP.ValeTienda = string.Empty;
                   VP.Visto_Pago = String.Empty;
                   VP.RefFP = "EFEC";

                   VP.Tipo = "EFECTIVO/CASH";
                   objVenta.NombreTarjeta ="" ;
                   objVenta.cTarjetaEuro = 0;
                   objVenta.Id_Pago = 3;
                   objVenta.Id_SubPago = "3";
                   VP.IdFP = 1;
                   VP.FPago = "EFECTIVO";
                   VP.FPagoDetalle = "";
                   VP.NumTarjeta = "";
                   VP.NumTarjetaAutoriza = "";
                   VP.NumTarjetaOperacion = "";
                   objVenta.cValeEuro = 0;
                   objVenta.cRecibidoEuro = 0;

                   LstPagos.Add(VP);

               }

               if (Ds.Tables[2].Rows.Count > 1)
               {
                   objVenta.Id_Pago = 3;
                   objVenta.Id_SubPago = "3";
               }

               if (objVenta.NombreTarjeta.Length > 0) { objVenta.NombreTarjeta = objVenta.NombreTarjeta.Substring(0, objVenta.NombreTarjeta.Length - 1); }

               objVenta.V_PAGO = LstPagos.AsEnumerable<VENTA_PAGOS>();
               objVenta.V_DETALLE = LstVentaDetalle.AsEnumerable <VENTA_DETALLE>();
               lstTrans[0].Importe = objVenta.TotalEuro;
               objVenta.TRANS=lstTrans.AsEnumerable<NTRANS>();
               return objVenta; 
           
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);
           } 
       }

       private List<VENTA_DETALLE> ProrratearCliente9(ref VENTA _V, List<VENTA_DETALLE> lstDetalle, double ImporteCliente9,double TotalVenta, double totalVentaDesc)
       {
           try
           {
               int i = 0;
               double ImporteBase=0;
               double dblDescImp = ImporteCliente9;
             
              

               for (i = 0; i <= lstDetalle.Count -1; i++)
               {
                   
                       ImporteBase = 0;
                       lstDetalle[i].ImporteBaseP = lstDetalle[i].ImporteEuros * lstDetalle[i].Estado;
                       ImporteBase = lstDetalle[i].ImporteBaseP;
                       lstDetalle[i].DescuentoP = 0;


                       if (ImporteCliente9 != 0 && lstDetalle[i].ImporteBaseP != 0)
                       {
                           if (i < lstDetalle.Count - 1)
                           {

                               lstDetalle[i].ImporteBaseP = Math.Round((ImporteCliente9 * ImporteBase) / TotalVenta, 0);
                               lstDetalle[i].DescuentoP = Math.Round((lstDetalle[i].ImporteBaseP * 100) / ImporteBase, 2);
                               dblDescImp = dblDescImp - lstDetalle[i].ImporteBaseP;
                               //ImporteCliente9 = ImporteCliente9 - lstDetalle[i].ImporteBaseP; 
                           }
                           else
                           {
                               //lstDetalle[i].ImporteBaseP = ImporteCliente9;
                               lstDetalle[i].ImporteBaseP = dblDescImp;
                               lstDetalle[i].DescuentoP = Math.Round((lstDetalle[i].ImporteBaseP * 100) / ImporteBase, 2);
                           }
                       }

                       lstDetalle[i] = GetGenerarDescuentoPunto9(ref _V, lstDetalle[i], lstDetalle[i].ImporteBaseP, "PUNTOS NINE", totalVentaDesc);

                   
               }

               return lstDetalle; 


           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);
           }

       }

       private void GetGenerarDescuentoPromo(ref VENTA _v, ref VENTA_DETALLE vLinea, double Descuento,String Promocion,int idPromocion)
       {
          
           try
           {
              
                   VENTA_ARTICULO_DESCUENTO LineaDescuento;
                   List<VENTA_ARTICULO_DESCUENTO> LstDescuento;
                   double Importeeuros;

                   if (Promocion.Length > 50) { Promocion = Promocion.Substring(0, 50); }

                   Importeeuros = vLinea.ImporteEuros;
                   vLinea.ImporteEuros = vLinea.ImporteEuros - Descuento;
                   vLinea.DtoEuroArticulo = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);
                   vLinea.MotivoCambioPrecio += " GESTOR PROMOCIONES" + "||";

                   if (vLinea.Detalle_Descuento != null)
                   {
                       LstDescuento = vLinea.Detalle_Descuento.ToList<VENTA_ARTICULO_DESCUENTO>();
                   }
                   else
                   {
                       LstDescuento = new List<VENTA_ARTICULO_DESCUENTO>();
                   }

                   LineaDescuento = new VENTA_ARTICULO_DESCUENTO();
                   LineaDescuento.IdPosicion = vLinea.IdPosicion;
                   LineaDescuento.IdOrden = LstDescuento.Count + 1;
                   LineaDescuento.FPago = Promocion;
                   LineaDescuento.FPagoDetalle = "GESTOR PROMOCIONES";
                   LineaDescuento.ImporteBase = Importeeuros;
                   LineaDescuento.ImporteDto = Descuento;
                   LineaDescuento.ImporteDtoPor = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);
                   _v.DescuentoEuro += Descuento;
                   _v.TotalEuro -= Descuento;
                   LineaDescuento.NumCertificado = String.Empty;
                   LineaDescuento.PromocionId = idPromocion;
                   LineaDescuento.PromocionName = String.Empty;
                   LineaDescuento.PromocionPuntos = 0;
                   LineaDescuento.PromocionTarjeta = String.Empty;
                   LineaDescuento.Visto_Descuento = "1";
                   LineaDescuento.NumCertificado = String.Empty;

                   LstDescuento.Add(LineaDescuento);

                   vLinea.Detalle_Descuento = LstDescuento.AsEnumerable<VENTA_ARTICULO_DESCUENTO>();
               

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);
           }
       }   
       private void GetGenerarDescuento(ref VENTA _v, ref VENTA_DETALLE vLinea,DataRowView dr,double TotalEuro)
       {
          
           try
           {
               VENTA_ARTICULO_DESCUENTO LineaDescuento;
               List<VENTA_ARTICULO_DESCUENTO> LstDescuento;
               double Importeeuros;
               

                   Importeeuros = vLinea.ImporteEuros; 
                   vLinea.ImporteEuros = vLinea.ImporteEuros - Convert.ToDouble(dr["importe"].ToString());
                  // vLinea.DtoEuroArticulo += Math.Round(100 * (TotalEuro - vLinea.ImporteEuros) / TotalEuro, 2);
                   vLinea.DtoEuroArticulo = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);
                   vLinea.MotivoCambioPrecio += dr["TipoPAgo"].ToString() + "||"; 

                   if (vLinea.Detalle_Descuento != null)
                   {
                       LstDescuento = vLinea.Detalle_Descuento.ToList<VENTA_ARTICULO_DESCUENTO>();
                   }
                   else
                   {
                       LstDescuento=new List<VENTA_ARTICULO_DESCUENTO>();
                   }

                   LineaDescuento = new VENTA_ARTICULO_DESCUENTO();
                   LineaDescuento.IdPosicion = vLinea.IdPosicion;
                   LineaDescuento.IdOrden = LstDescuento.Count + 1;
                   LineaDescuento.FPago = dr["TipoPAgo"].ToString();
                   LineaDescuento.FPagoDetalle = dr["TipoPAgo"].ToString();
                   LineaDescuento.ImporteBase = Importeeuros;
                   LineaDescuento.ImporteDto = Convert.ToDouble(dr["importe"].ToString());
                   LineaDescuento.ImporteDtoPor = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);
                   _v.DescuentoEuro += Convert.ToDouble(dr["importe"].ToString());
                   _v.TotalEuro -= Convert.ToDouble(dr["importe"].ToString());
                   LineaDescuento.NumCertificado = String.Empty;
                   LineaDescuento.PromocionId = 0;
                   LineaDescuento.PromocionName = String.Empty;
                   LineaDescuento.PromocionPuntos = 0;
                   LineaDescuento.PromocionTarjeta = String.Empty;
                   LineaDescuento.Visto_Descuento = "1";
                   LineaDescuento.NumCertificado = String.Empty;

                   LstDescuento.Add(LineaDescuento);

                   vLinea.Detalle_Descuento = LstDescuento.AsEnumerable<VENTA_ARTICULO_DESCUENTO>();
               
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);
           }   
       }
       private VENTA_DETALLE GetGenerarDescuentoPunto9(ref VENTA _v, VENTA_DETALLE vLinea, double Importe, String TipoPago,double totalVenta)
       {
           try
           {
               VENTA_ARTICULO_DESCUENTO LineaDescuento;
               List<VENTA_ARTICULO_DESCUENTO> LstDescuento;
               double Importeeuros;

               Importeeuros = vLinea.ImporteEuros;
           /*    if (vLinea.ImporteEuros == Importe)
                   vLinea.ImporteEuros = totalVenta - Importe;
               else*/
                   vLinea.ImporteEuros = vLinea.ImporteEuros - Importe;
               vLinea.DtoEuroArticulo = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);

             
          //     vLinea.DtoEuroArticulo +=  Math.Round(100 * (totalVenta - vLinea.ImporteEuros) / totalVenta,2); 
               vLinea.MotivoCambioPrecio += TipoPago + "||";
 
               if (vLinea.Detalle_Descuento != null)
               {
                   LstDescuento = vLinea.Detalle_Descuento.ToList<VENTA_ARTICULO_DESCUENTO>();
               }
               else
               {
                   LstDescuento = new List<VENTA_ARTICULO_DESCUENTO>();
               }

               LineaDescuento = new VENTA_ARTICULO_DESCUENTO();
               LineaDescuento.IdPosicion = vLinea.IdPosicion;
               LineaDescuento.IdOrden = LstDescuento.Count + 1;
               LineaDescuento.FPago = TipoPago;
               LineaDescuento.FPagoDetalle = TipoPago;
               LineaDescuento.ImporteBase = Importeeuros;
               LineaDescuento.ImporteDto = Importe;
               LineaDescuento.ImporteDtoPor = Math.Round(100 * (Importeeuros - vLinea.ImporteEuros) / Importeeuros, 2);
               _v.DescuentoEuro += Importe;
               _v.TotalEuro -= Importe; 
               LineaDescuento.NumCertificado = String.Empty;
               LineaDescuento.PromocionId = 0;
               LineaDescuento.PromocionName = String.Empty;
               LineaDescuento.PromocionPuntos = 0;
               LineaDescuento.PromocionTarjeta = String.Empty;
               LineaDescuento.Visto_Descuento = "1";
               LineaDescuento.NumCertificado = String.Empty;

               LstDescuento.Add(LineaDescuento);

               vLinea.Detalle_Descuento = LstDescuento.AsEnumerable<VENTA_ARTICULO_DESCUENTO>();

               return vLinea; 

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);
           }
       }

       private String EjecutaVenta()
       {
           try
           {
               Int16 Result = 0;
               String strREsult = String.Empty;

               CalculoIVAVENTA();
               _Venta.Id_Ticket = objCapaDatos.GetNewTicket(_Venta.Id_Tienda, _Venta.ID_TERMINAL);
               
               _Venta.Fecha = _Venta.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
               Result = objCapaDatos.ActualizarUpVentas(_Venta);

               



               if (Result == 1) { strREsult = _Venta.Id_Ticket; }

               return strREsult;

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }


      public string RedencionDevolucion(string Id_Ticket,string Id_TicketOld,string Terminal,string Tienda, double TotalEuro, double ImporteDescontadoC9, string Id_Cliente_N, string idArticulo,string cadenaConex,DateTime fecha, string IdEmpleado, string FPago, string Tarjeta, string strAutorizacion) {

          string result = "NOK.Error rendencion puntos";
          string idTarjeta = "";
          DataSet ds;
          DataView dv;
          
          double dblParesAcumuladosAnt=0;
          double dblBolsasAcumuladasAnt=0;
          
          VENTA objVenta = new VENTA();
          objVenta.Id_Empleado = Convert.ToInt32(IdEmpleado);
          objVenta.Id_Tienda = Tienda;
          objVenta.ID_TERMINAL = Terminal;
          objVenta.Fecha = fecha;
          objVenta.Id_Cliente_N=Convert.ToInt32(Id_Cliente_N);
          objVenta.Id_Ticket = Id_Ticket;
          
          


          try
          {
              cls_Cliente9 C9 = new cls_Cliente9(objVenta);
              C9.ConexString = cadenaConex;

              CLIENTE9 objClient = new CLIENTE9();
              objClient.NumTarjeta = Tarjeta;
              objClient.Id_Cliente = Convert.ToInt32(Id_Cliente_N);
              objVenta.CLient9 = objClient;

              objCapaDatos = new ClsCapaDatos();
              objCapaDatos.ConexString = cadenaConex;

              ds = objCapaDatos.GetCarritoTicket(Id_TicketOld);
              dv = ds.Tables[0].DefaultView;
              dv.RowFilter = "TipoPago in ('PAR 9','BOLSA 5','PUNTOS NINE')";
              objClient.SaldoPuntosAnt = 0;

              cls_Cliente9.ConsultaBeneficios cz = new cls_Cliente9.ConsultaBeneficios();
              cz.idTargeta = objVenta.CLient9.NumTarjeta.ToString();
              cz.idTienda = objVenta.Id_Tienda;
              cz.idTerminal = objVenta.ID_TERMINAL;
              try
              {
                  String ret = C9.InvokeWS_ConsultaBeneficios(ref cz);
                  objClient.SaldoPuntosAnt = double.Parse(cz.puntos);
                  dblBolsasAcumuladasAnt = double.Parse(cz.bolsasAcumuladas);
                  dblParesAcumuladosAnt = double.Parse(cz.paresAcumulados);

                  
              }
              catch (Exception ex)
              {
                  log.Error("Excepcion en1º ConsultaBeneficion" + ex.Message.ToString());
                  result = "NOK. EXCEPCION 1º CONSULTA BENEFICIOS:" + ex.Message.ToString();
              }
              int resultOperacion = ConfirmaOperacionDevo(FPago, strAutorizacion, C9);
              if (resultOperacion == 0) {
                  log.Error("ERROR EN LA CONFIRMACION DE LA OPERACION");
                  return result = "NOK. ERROR EN LA CONFIRMACION DE OPERACION";
              
              }
              log.Info("Confirmada la operacion de devolucion con resultado " + resultOperacion);

              cls_Cliente9.EnviaTicket eTicket = new cls_Cliente9.EnviaTicket();


              String EntradaC9 = C9.GeneraParamSend_EnviaTicketDevo(Id_Ticket, Id_TicketOld, Terminal, Tienda, TotalEuro, TotalEuro + ImporteDescontadoC9, Int64.Parse(Id_Cliente_N), idTarjeta, fecha, IdEmpleado, dv, strAutorizacion);
              log.Info("Entrada envíada al ticket: " + EntradaC9);
              eTicket.blnOk = true;


              C9.InvokeWS_EnviaTicket(ref eTicket, Id_Ticket, EntradaC9, String.Empty);
             // if (eTicket.blnOk)
              //{
                 
                  if (objVenta.Id_Cliente_N > 0)
                  {
                      if (objVenta.CLient9.NumTarjeta != null)
                      {

                          cls_Cliente9.ConsultaBeneficios cb = new cls_Cliente9.ConsultaBeneficios();
                          cb.idTargeta = objVenta.CLient9.NumTarjeta.ToString();
                          cb.idTienda = objVenta.Id_Tienda;
                          cb.idTerminal = objVenta.ID_TERMINAL;
                          try
                          {
                              String ret = C9.InvokeWS_ConsultaBeneficios(ref cb);
                          }
                          catch (Exception ex)
                          {
                              log.Error("Excepcion en ConsultaBeneficion" + ex.Message.ToString());
                              result = "NOK. EXCEPCION CONSULTA BENEFICIOS:" + ex.Message.ToString();
                          }
                          //metemos en la clase
                          objClient.Cliente = cb.nombre;
                          
                          objClient.PuntosObtenidos = double.Parse(cb.puntos)- objClient.SaldoPuntosAnt;
                          objClient.SaldoPuntosAct = double.Parse(cb.puntos);
                          objClient.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
                          objClient.ParesAcumuladosAnt = dblParesAcumuladosAnt;
                          objClient.BolsasAcumuladasAnt = dblBolsasAcumuladasAnt;
                          objClient.BolsasAcumuladasAct = double.Parse(cb.bolsasAcumuladas);
                          objClient.ParesAcumuladosAct = double.Parse(cb.paresAcumulados);
                          objClient.dblSaldoPares9 = cb.dblSaldoPares9;
                          objClient.BolsasRedimidas = 0;
                          objClient.ParesRedimidos = 0;
                          objClient.CandidatoBasico = cb.strCandBasicoC9;
                          objClient.CandidatoFirstShoeLover = cb.strCandFirstC9;
                          objClient.NumConfirmaPuntos9 = Convert.ToInt32(cb.lngNumConfirmaPuntos9);
                          objClient.NumConfirmaPar9 = Convert.ToInt32(cb.lngNumConfirmaPar9);
                          objClient.NumConfirmaBolsa5 = Convert.ToInt32(cb.lngNumConfirmaBolsa5);
                          objClient.Cumpleaños = cb.cumpleanos;
                          objClient.Aniversario = cb.aniversario;

                          if (objVenta.CLient9.Cliente == "" || objVenta.CLient9.Cliente == null) objVenta.CLient9.Cliente = "-1";
                          try
                          {
                              _Venta = objVenta;
                              objCapaDatos.ActualizaClienteNineDevo(objVenta);
                          }
                          catch (Exception ex)
                          {
                              log.Error("Excepcion en Actualizacion Beneficios" + ex.Message.ToString());
                              result = "NOK. EXCEPCION ACTUALIZANDO BENEFICION:" + ex.Message.ToString();
                          }

                      }
                  }

                  result = "OK";
             /* }
              else
              {
                  log.Error(" Error en el enviaTicket");
                  result = "NOK. Error en el enviaTicket";

              }*/
          }
          catch (Exception ex) {
              log.Error(ex.Message.ToString());
              result = "NOK.EXCEPCION:" + ex.Message.ToString();
          }
                 return result;             
       }
       private String EjecutaVenta(DataView Dv,CLIENTE9 obj9 )
       {
           try
           {
               Int16 Result = 0;
               String strREsult = String.Empty;
               cls_Cliente9 C9;
               double ImporteDescontadoC9;

               CalculoIVAVENTA();

               _Venta.Id_Ticket = objCapaDatos.GetNewTicket(_Venta.Id_Tienda, _Venta.ID_TERMINAL);
               _Venta.Fecha=_Venta.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second) ;
                Result=objCapaDatos.ActualizarUpVentas(_Venta);
             
               //Solicitamos Redencion            

               ImporteDescontadoC9= ConfirmarRedenciones(Dv,ref obj9);

               _Venta.CLient9 = obj9; 
 
               // if (obj9.NumTarjeta !=null) 
                // envia Ticket
               // {
                 
                 C9 = new cls_Cliente9(_Venta);
               
                 C9.ConexString =StrCadenaConexion; 
                 cls_Cliente9.EnviaTicket eTicket = new cls_Cliente9.EnviaTicket();
                 
                 eTicket.blnOk = true;

                 if (_Venta.Id_Cliente_N == 0) { _Venta.Id_Cliente_N = -1; }


                 String EntradaC9 = C9.GeneraParamSend_EnviaTicket(_Venta.Id_Ticket, _Venta.TotalEuro + ImporteDescontadoC9, _Venta.TotalEuro, Int64.Parse(_Venta.Id_Cliente_N.ToString()), Dv);

               //acl
                
               
                 C9.InvokeWS_EnviaTicket(ref eTicket,_Venta.Id_Ticket,EntradaC9,String.Empty);


               //  if (eTicket.blnOk)
               //  {
                     //acl. segun mario, aqui habría que hacer un checkpending

                     if (_Venta.Id_Cliente_N > 0)
                     {
                         if (_Venta.CLient9.NumTarjeta == null)
                         {

                             #region Sin Tarjeta

                             _Venta.CLient9.NumTarjeta = objCapaDatos.GetNumTarjeta(_Venta.Id_Cliente_N, _Venta.Fecha);

                             if  (_Venta.CLient9.NumTarjeta.Length >0)
                             {    

                             cls_Cliente9.ConsultaBeneficios cb = new cls_Cliente9.ConsultaBeneficios();
                             cb.idTargeta = _Venta.CLient9.NumTarjeta.ToString();
                             cb.idTienda = _Venta.Id_Tienda;
                             cb.idTerminal = _Venta.ID_TERMINAL;

                             String ret = C9.InvokeWS_ConsultaBeneficios(ref cb);


                             obj9.PuntosObtenidos = 0;
                             obj9.SaldoPuntosAct = 0;
                             obj9.dblSaldoBolsa5 = 0;
                             obj9.BolsasAcumuladasAct = 0;
                             obj9.ParesAcumuladosAct = 0;
                             obj9.dblSaldoPares9 = 0;
                             obj9.Id_Cliente=_Venta.Id_Cliente_N;
                             obj9.NumConfirmaBolsa5 = 0;
                             obj9.NumConfirmaPar9 = 0;
                             obj9.NumConfirmaPuntos9 = 0;
                             obj9.NivelActual = cb.strNivelActual;
                             obj9.Cliente = cb.nombre ; 
                             obj9.CandidataShoeLover =(cb.shoelover=="true"? "SI":"NO") ;
                             obj9.Aniversario = (cb.aniversario=="null/null" ? "":cb.aniversario);
                             obj9.Cumpleaños  = (cb.cumpleanos  == "null/null" ? "" : cb.cumpleanos );
                             obj9.SaldoPuntosAnt = 0;
                             obj9.ParesAcumuladosAnt = 0;
                             obj9.BolsasAcumuladasAnt = 0;
                             obj9.NumTarjetaNew = "";
                             obj9.BenC9 = "0";
                                 //ACL. se AÑADEN DOS CAMPOS MAS PARA EL NIVEL C9
                             obj9.CandidatoBasico = cb.strCandBasicoC9;
                             obj9.CandidatoFirstShoeLover = cb.strCandFirstC9;

                             objCapaDatos.ActualizaClienteNine(_Venta);

                             }

                             #endregion
                         }
                         else
                         {

                             cls_Cliente9.ConsultaBeneficios cb = new cls_Cliente9.ConsultaBeneficios();
                             cb.idTargeta = _Venta.CLient9.NumTarjeta.ToString();
                             cb.idTienda = _Venta.Id_Tienda;
                             cb.idTerminal = _Venta.ID_TERMINAL;
                             try
                             {
                                 String ret = C9.InvokeWS_ConsultaBeneficios(ref cb);
                             }
                             catch(Exception ex)
                             {
                                 log.Error(ex.Message);
                             }
                             //metemos en la clase
                             
                             obj9.PuntosObtenidos = double.Parse(cb.puntos) - obj9.SaldoPuntosAnt + obj9.PuntosRedimidos;
                             obj9.SaldoPuntosAct = double.Parse(cb.puntos);
                             obj9.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
                             obj9.BolsasAcumuladasAct = double.Parse(cb.bolsasAcumuladas);
                             obj9.ParesAcumuladosAct = double.Parse(cb.paresAcumulados);
                             obj9.dblSaldoPares9 = cb.dblSaldoPares9;
                             //ACL.
                             obj9.CandidatoBasico = cb.strCandBasicoC9;
                             obj9.CandidatoFirstShoeLover = cb.strCandFirstC9;
                             
                             if (_Venta.CLient9.Cliente == "" || _Venta.CLient9.Cliente== null) _Venta.CLient9.Cliente = "-1";
                             objCapaDatos.ActualizaClienteNine(_Venta);

                         }
                     }
                 //}
               
               // }


               if (Result ==1) {strREsult=_Venta.Id_Ticket; }

               return strREsult;
               
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
       }
       /// <summary>
       /// confirmamos redencion
       /// </summary>
       /// <param name="Dv"></param>
       /// <returns></returns>
       private Int16 ConfirmaRedencion(DataView Dv)
       {
           try
           {               
               foreach (DataRowView dr in Dv)
               {


               }
               return 1;
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }  
       private void CalculoIVAVENTA()
       {
           try
           {
               String StrArticulo = String.Empty;
               iva = objCapaDatos.IVAPRODUCTO();
               List<IVA> lstIVA;
               List<VENTA_DETALLE> AR_DETALLE;

               foreach (VENTA_DETALLE DV in _Venta.V_DETALLE)
               {
                   StrArticulo += DV.Id_Articulo.ToString() + ",";

               }

               if (StrArticulo.Length > 0) { StrArticulo = StrArticulo.Substring(0, StrArticulo.Length - 1); }

               lstIVA=objCapaDatos.IVAARTICULO(StrArticulo,iva,_Venta.Id_Tienda);

               AR_DETALLE = _Venta.V_DETALLE.ToList<VENTA_DETALLE>();

               for (int i = 0; i < AR_DETALLE.Count; i++)
               {
                   IVA _IA =lstIVA.Find(p => p.Id_Articulo  == AR_DETALLE[i].Id_Articulo);

                   //calculo de iva y base imponible
                   AR_DETALLE[i].IVA = _IA.Iva;
                   AR_DETALLE[i].ImporteBase = AR_DETALLE[i].ImporteEuros / (1 + (_IA.Iva / 100));
               }

               _Venta.V_DETALLE = AR_DETALLE.AsEnumerable<VENTA_DETALLE>();

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);




           }

       }
       #endregion

       #region Traspaso Automatico Solicitudes

       public Int16 GenerarTraspasoAutomatico(Int64 idPedido)
       {
           try
           {
               GenerarCargoEntrada(idPedido);
               return 1;

           }

           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }

       private Int16 GenerarCargoEntrada(Int64 idPEdido)
       {
           try
           {
               DataSet Ds;
               DataSet DsSolicitud;
               String DirFTP;
               DateTime Fecha;
               ServicioModdoOnline.SModdoOnlineSoapClient _wsOnline;
               Int16 Result;
               String Tienda;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;

               Ds = objCapaDatos.GetSolicitud(idPEdido);
                           

               if (Ds.Tables.Count == 2)
               {
                   DsSolicitud = new DataSet("SOL");
                   DsSolicitud.Tables.Add(Ds.Tables[0].Copy());

                   DirFTP = Ds.Tables[1].Rows[0]["DirFTP"].ToString();
                   Fecha = Convert.ToDateTime(Ds.Tables[1].Rows[0]["FechaActiva"].ToString());
                   Tienda=Ds.Tables[0].Rows[0]["TiendaDestino"].ToString();

                   _wsOnline = new ServicioModdoOnline.SModdoOnlineSoapClient();
                   Result=_wsOnline.Get_Entidades_GENERIC(ref DsSolicitud,"0", ServicioModdoOnline.Entidades.CARGOAUTAVE, Fecha, DirFTP + "|" + PaswordWs, Ds.Tables[0].Rows[0]["TiendaDestino"].ToString(), 0, 0);

                   if (Result == 1)
                   {
                      //objCapaDatos.InsertarEntradaCargoAutoMatico(DsSolicitud.Tables[0], Tienda);

                      //foreach (DataRow Dr in DsSolicitud.Tables[0].Rows)
                      //{
                      //    Ds = new DataSet();
                      //    Ds=objCapaDatos.GetDataSetEntradaCargos(Int64.Parse(Dr["idCargo"].ToString()),Tienda,Fecha);
                      //    Result = _wsOnline.UpEntradaCargo(ref Ds, DirFTP + "|" + PaswordWs, Fecha, Tienda, Dr["idCargo"].ToString());
                      //} 
                   }
               }  

               return 1;
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }
           
       }

       #endregion

       #region Promociones
       /// <summary>
       /// metodo que me calcula todo el carrito
       /// </summary>
       /// <param name="IdCarrito"></param>
       /// <param name="IdTienda"></param>
       /// <param name="Fecha"></param>
       /// <returns></returns>
       public String GetObjCarritoPromocion (Int64 IdCarrito,String IdTienda,DateTime Fecha,bool EnviarMensaje =false)  
       {    
          try
           {
               Promocion objPromo;
               DataView DvPromo=null;
               List<Promocion> LstPromo=null;
               DataSet Ds;
               Int16 i=0;
               String rMensaje = String.Empty; 

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString=StrCadenaConexion;
               
              
               Ds=objCapaDatos.GetCarritoPromo(IdCarrito);

               if (Ds.Tables.Count > 1) { DvPromo = Ds.Tables[1].DefaultView; } 


               if (Ds.Tables[0].Rows.Count > 0)
               {
                   LstPromo = new List<Promocion>();

                   foreach (DataRow Dr in Ds.Tables[0].Rows)
                   {
                       i += 1;
                       objPromo = new Promocion();
                       objPromo.Id_Articulo = int.Parse(Dr["idArticulo"].ToString());
                       objPromo.ClienteID = int.Parse(Dr["id_cliente"].ToString());
                       objPromo.Id_cabecero_detalle = int.Parse(Dr["Id_cabecero_detalle"].ToString());
                       objPromo.Pvp_Or = double.Parse(Dr["PVPORI"].ToString());
                       objPromo.Pvp_Vig = double.Parse(Dr["PVPACT"].ToString());
                       objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString());
                       //objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString()) - double.Parse(Dr["DTOArticulo"].ToString());
                       //objPromo.DtoEuro = double.Parse(Dr["DTOArticulo"].ToString());
                       objPromo.DtoEuro = 0;
                       objPromo.NumeroLineaRecalculo = i;
                       objPromo.LineaCarritoDetalle = int.Parse(Dr["id_Carrito_Detalle"].ToString());
                       objPromo.Tipo = TipoAccion.VENTA;
                       objPromo.Id_Tienda = IdTienda;
                       objPromo.Unidades = Dr["Cantidad"].ToString();
                       objPromo.FSesion = Convert.ToDateTime(Fecha.ToShortDateString());
                       objPromo.TotalVenta = double.Parse(Dr["total"].ToString());
                       objPromo.Promo = null;

                       if (int.Parse((Dr["idPromocion"].ToString())) > 0)
                       {
                           List<DetallePromo> LstDetallePromo;
                           LstDetallePromo = new List<DetallePromo>();

                           DetallePromo DPromoSel;
                           DPromoSel = new DetallePromo();
                           DPromoSel.Idpromo = int.Parse((Dr["idPromocion"].ToString()));
                           DPromoSel.DescriAmpliaPromo = "";
                           DPromoSel.DescriPromo = "";
                           DPromoSel.DtoPromo = Math.Round((objPromo.Pvp_Vig - objPromo.Pvp_Venta == 0 ? 0 : 100 * (objPromo.Pvp_Vig - objPromo.Pvp_Venta) / (objPromo.Pvp_Vig)), 0);
                           DPromoSel.PromocionSelecionada = true;
                           LstDetallePromo.Add(DPromoSel);
                           objPromo.Promo = LstDetallePromo.AsEnumerable<DetallePromo>();

                       }
                                              

                       if (DvPromo != null)
                       {
                           DvPromo.RowFilter = "id_linea_Carrito=" + int.Parse(Dr["id_Carrito_Detalle"].ToString());


                           if (DvPromo.Count > 0)
                           {

                               List<DetallePromo> LstDetallePromo;

                               LstDetallePromo = new List<DetallePromo>();

                               foreach (DataRowView Drv in DvPromo)
                               {
                                   DetallePromo DPromo;
                                   DPromo = new DetallePromo();
                                   DPromo.Idpromo = int.Parse(Drv["idPromo"].ToString());
                                   DPromo.DtoPromo = double.Parse(Drv["DtoPromo"].ToString());
                                   DPromo.DescriPromo = Drv["DescriPromo"].ToString();
                                   DPromo.DescriAmpliaPromo = Drv["DescriAmpliaPromo"].ToString();

                                   LstDetallePromo.Add(DPromo);
                               }

                               objPromo.Promo = LstDetallePromo.AsEnumerable<DetallePromo>();
                           }
                           else
                           {
                               // quitar esta parte para promociones
                           }

                           LstPromo.Add(objPromo);
                       }
                       else
                       {
                           objPromo.Promo = null;
                           LstPromo.Add(objPromo);

                       }
                   }
               }


              

               if (LstPromo != null)
               {
                   IEnumerable<Promocion> IePromocion = CalculoPromocion(LstPromo.AsEnumerable<Promocion>());
                   rMensaje = objCapaDatos.ActualizaCarritoPromo(IePromocion.ToList<Promocion>());
               } 
             
              //return rMensaje;
               return  (EnviarMensaje ? rMensaje : String.Empty);
                               
            }
            catch (Exception sqlEx)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
                throw new Exception(sqlEx.Message, sqlEx.InnerException);

            }  

       }
       /// <summary>
       /// metodo que recalcula el carrito cuando seleciona el usuario una promocion cuando tiene mas de 1
       /// </summary>
       /// <param name="IdCarrito"></param>
       /// <param name="IdTienda"></param>
       /// <param name="Fecha"></param>
       /// <param name="IdPromocion"></param>
       /// <param name="id_linea_Carrito_Detalle"></param>
       public String GetObjCarritoPromocion(Int64 IdCarrito, String IdTienda, DateTime Fecha,Int32 IdPromocion,int id_linea_Carrito_Detalle,bool EnviarMensaje=false)
       {
           try
           {
               Promocion objPromo;
               DataView DvPromo = null;
               List<Promocion> LstPromo = null;
               List<DetallePromo> LstDetallePromo;
               DataSet Ds;
               Int16 i = 0;
               String rMensaje = String.Empty;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;


               Ds = objCapaDatos.GetCarritoPromo(IdCarrito);

               if (Ds.Tables.Count > 1) { DvPromo = Ds.Tables[1].DefaultView; }


               if (Ds.Tables[0].Rows.Count > 0)
               {
                   LstPromo = new List<Promocion>();

                   foreach (DataRow Dr in Ds.Tables[0].Rows)
                   {
                       i += 1;
                       objPromo = new Promocion();
                       objPromo.Id_Articulo = int.Parse(Dr["idArticulo"].ToString());
                       objPromo.ClienteID = int.Parse(Dr["id_cliente"].ToString());
                       objPromo.Id_cabecero_detalle = int.Parse(Dr["Id_cabecero_detalle"].ToString());
                       objPromo.Pvp_Or = double.Parse(Dr["PVPORI"].ToString());
                       objPromo.Pvp_Vig = double.Parse(Dr["PVPACT"].ToString());
                       objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString());
                       //objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString()) - double.Parse(Dr["DTOArticulo"].ToString());
                       //objPromo.DtoEuro = double.Parse(Dr["DTOArticulo"].ToString());
                       objPromo.DtoEuro = 0;
                       objPromo.NumeroLineaRecalculo = i;
                       objPromo.LineaCarritoDetalle = int.Parse(Dr["id_Carrito_Detalle"].ToString());
                       objPromo.Tipo = TipoAccion.RECALCULO;
                       objPromo.Id_Tienda = IdTienda;
                       objPromo.Unidades = Dr["Cantidad"].ToString();
                       objPromo.FSesion = Convert.ToDateTime(Fecha.ToShortDateString());
                       objPromo.TotalVenta = double.Parse(Dr["total"].ToString());
                                            

                       if (DvPromo != null)
                       {
                           DvPromo.RowFilter = "id_linea_Carrito=" + int.Parse(Dr["id_Carrito_Detalle"].ToString());

                           if (int.Parse(Dr["id_Carrito_Detalle"].ToString()) == id_linea_Carrito_Detalle)
                           {
                               DvPromo.RowFilter += " and idPromo=" + IdPromocion; 

                           }  

                           if (DvPromo.Count > 0)
                           {


                               LstDetallePromo = new List<DetallePromo>();

                               foreach (DataRowView Drv in DvPromo)
                               {
                                   DetallePromo DPromo;
                                   DPromo = new DetallePromo();
                                   DPromo.Idpromo = int.Parse(Drv["idPromo"].ToString());
                                   DPromo.DtoPromo = double.Parse(Drv["DtoPromo"].ToString());
                                   DPromo.DescriPromo = Drv["DescriPromo"].ToString();
                                   DPromo.DescriAmpliaPromo = Drv["DescriAmpliaPromo"].ToString();
                                   DPromo.PromocionSelecionada=(DvPromo.Count==1?true:false);
                                  
                                   LstDetallePromo.Add(DPromo);
                               }

                               objPromo.Promo = LstDetallePromo.AsEnumerable<DetallePromo>();
                           }
                           else
                           {

                               if (int.Parse((Dr["idPromocion"].ToString())) > 0)
                               {
                                   LstDetallePromo = new List<DetallePromo>();

                                   DetallePromo DPromoSel;
                                   DPromoSel = new DetallePromo();
                                   DPromoSel.Idpromo = int.Parse((Dr["idPromocion"].ToString()));
                                   DPromoSel.DescriAmpliaPromo = "";
                                   DPromoSel.DescriPromo = "";
                                   DPromoSel.DtoPromo = Math.Round((objPromo.Pvp_Vig - objPromo.Pvp_Venta == 0 ? 0 : 100 * (objPromo.Pvp_Vig - objPromo.Pvp_Venta) / (objPromo.Pvp_Vig)), 0);
                                   DPromoSel.PromocionSelecionada = true;
                                   LstDetallePromo.Add(DPromoSel);
                                   objPromo.Promo = LstDetallePromo.AsEnumerable<DetallePromo>();

                               }
                               else
                               {
                                   objPromo.Promo = null;

                               }
                         }

                           LstPromo.Add(objPromo);
                       }
                       else
                       {
                           objPromo.Promo = null;
                           LstPromo.Add(objPromo);

                       }
                   }
               }


               IEnumerable<Promocion> IePromocion = CalculoPromocion(LstPromo.AsEnumerable<Promocion>());


               rMensaje = objCapaDatos.ActualizaCarritoPromo(IePromocion.ToList<Promocion>());

               //quitamos mensaje 
               // return rMensaje;

               return  (EnviarMensaje ? rMensaje : String.Empty);


           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }
       public String GetObjCarritoPromocionClientes(Int64 IdCarrito, String IdTienda, DateTime Fecha, bool EnviarMensaje=false)
       {
           try
           {
               Promocion objPromo;
               DataView DvPromo = null;
               List<Promocion> LstPromo = null;
               DataSet Ds;
               Int16 i = 0;
               String rMensaje = String.Empty;

               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;


               Ds = objCapaDatos.GetCarritoPromo(IdCarrito);

               if (Ds.Tables.Count > 1) { DvPromo = Ds.Tables[1].DefaultView; }


               if (Ds.Tables[0].Rows.Count > 0)
               {
                   LstPromo = new List<Promocion>();

                   foreach (DataRow Dr in Ds.Tables[0].Rows)
                   {
                       i += 1;
                       objPromo = new Promocion();
                       objPromo.Id_Articulo = int.Parse(Dr["idArticulo"].ToString());
                       objPromo.ClienteID = int.Parse(Dr["id_cliente"].ToString());
                       objPromo.Id_cabecero_detalle = int.Parse(Dr["Id_cabecero_detalle"].ToString());
                       objPromo.Pvp_Or = double.Parse(Dr["PVPORI"].ToString());
                       objPromo.Pvp_Vig = double.Parse(Dr["PVPACT"].ToString());
                       objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString());
                       //objPromo.Pvp_Venta = double.Parse(Dr["PVPACT"].ToString()) - double.Parse(Dr["DTOArticulo"].ToString());
                       //objPromo.DtoEuro = double.Parse(Dr["DTOArticulo"].ToString());
                       objPromo.DtoEuro = 0;
                       objPromo.NumeroLineaRecalculo = i;
                       objPromo.LineaCarritoDetalle = int.Parse(Dr["id_Carrito_Detalle"].ToString());
                       objPromo.Tipo = TipoAccion.VENTA;
                       objPromo.Id_Tienda = IdTienda;
                       objPromo.Unidades = Dr["Cantidad"].ToString();
                       objPromo.FSesion = Convert.ToDateTime(Fecha.ToShortDateString());
                       objPromo.TotalVenta = double.Parse(Dr["total"].ToString());
                       objPromo.Promo = null;
                                          

                       if (DvPromo != null)
                       {
                           DvPromo.RowFilter = "id_linea_Carrito=" + int.Parse(Dr["id_Carrito_Detalle"].ToString());


                           if (DvPromo.Count > 0)
                           {

                               List<DetallePromo> LstDetallePromo;

                               LstDetallePromo = new List<DetallePromo>();

                               foreach (DataRowView Drv in DvPromo)
                               {
                                   DetallePromo DPromo;
                                   DPromo = new DetallePromo();
                                   DPromo.Idpromo = int.Parse(Drv["idPromo"].ToString());
                                   DPromo.DtoPromo = double.Parse(Drv["DtoPromo"].ToString());
                                   DPromo.DescriPromo = Drv["DescriPromo"].ToString();
                                   DPromo.DescriAmpliaPromo = Drv["DescriAmpliaPromo"].ToString();

                                   LstDetallePromo.Add(DPromo);
                               }

                               objPromo.Promo = LstDetallePromo.AsEnumerable<DetallePromo>();
                           }
                           else
                           {
                               // quitar esta parte para promociones
                           }

                           LstPromo.Add(objPromo);
                       }
                       else
                       {
                           objPromo.Promo = null;
                           LstPromo.Add(objPromo);

                       }
                   }
               }


               if (LstPromo==null) { return rMensaje; } 


               IEnumerable<Promocion> IePromocion = CalculoPromocion(LstPromo.AsEnumerable<Promocion>());


               rMensaje = objCapaDatos.ActualizaCarritoPromo(IePromocion.ToList<Promocion>());

              // return rMensaje;
             return  (EnviarMensaje ? rMensaje : String.Empty);

           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       } 
       private IEnumerable<Promocion> CalculoPromocion(IEnumerable<Promocion> objPromo)
       {
           try
           {
               IEnumerable<Promocion> LstPromo = null;
               AVEGestorPromociones.clsPromociones ENtCalcPromo;

               ENtCalcPromo = new AVEGestorPromociones.clsPromociones();
               ENtCalcPromo.ConexionString = StrCadenaConexion;

               LstPromo= ENtCalcPromo.Promocion_Calculo(objPromo);

               return LstPromo.AsEnumerable<Promocion>();
          
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }

       }
       public DataSet GetPromoCarritoLinea(Int64 idCarrito)
       {
           try
           {
               objCapaDatos = new ClsCapaDatos();
               objCapaDatos.ConexString = StrCadenaConexion;
               DataSet Ds;

               Ds= objCapaDatos.ObtenerPromoLinea(idCarrito);

               return Ds;
           }
           catch (Exception sqlEx)
           {
               log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
               throw new Exception(sqlEx.Message, sqlEx.InnerException);

           }  

       }   

       #endregion

       #region funcionesComunes

       /// <summary>
       /// funcion para recuperar la url de una binding
       /// </summary>
       /// <param name="ServicioWsOnline"></param>
       /// <returns></returns>
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
               System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as  System.Net.HttpWebRequest;
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

       #endregion

       #region "Comprobaciones Genericas AVE"
         public bool ComprobarStock(Int64 idPedido, Int64 idCarrito)
      {
          try
          {
              bool ExisteStock = false;

              objCapaDatos = new ClsCapaDatos();
              objCapaDatos.ConexString = StrCadenaConexion;

              ExisteStock = objCapaDatos.ComprobarStockSolicitudes(idPedido, idCarrito);

              return ExisteStock;

          }
          catch (Exception sqlEx)
          {
              log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
              throw new Exception(sqlEx.Message, sqlEx.InnerException);

          }
      }
         public List<CLIENTE9> GetCliente(String Cliente, DateTime Fecha, Int64 IdCarrito, String Tienda)
      {
          try
          {
              objCapaDatos = new ClsCapaDatos();
              objCapaDatos.ConexString = StrCadenaConexion;

              return objCapaDatos.GetClienteNomCliente(Cliente, Fecha, IdCarrito, Tienda);
          }

          catch (Exception sqlEx)
          {
              log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
              throw new Exception(sqlEx.Message, sqlEx.InnerException);

          }
      }  
      //Se utiliza para obtener los datos de busqueda de cliente en la administración de cliente nine
        public List<CLIENTE9> GetClienteAdm9(String Cliente, DateTime Fecha)
      {
          try
          {
              objCapaDatos = new ClsCapaDatos();
              objCapaDatos.ConexString = StrCadenaConexion;

              return objCapaDatos.GetDatCliente(Cliente, Fecha);
          }

          catch (Exception sqlEx)
          {
              log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlEx);
              throw new Exception(sqlEx.Message, sqlEx.InnerException);

          }
      }  

        
       #endregion
       
       #region "Metodos consumir WSModdoOnline"
        internal virtual void InvokeUpVentaWs()
         {

             Int16 Result = 0;
             String DIRFTP = String.Empty;
             String Online = String.Empty;

             try
             {

                 DataSet Ds = new DataSet();
                 ServicioModdoOnline.SModdoOnlineSoapClient _wsOnline;


                 objCapaDatos.GetConfOnline(_Venta.Id_Tienda, ref DIRFTP, ref Online);

                 if (Online == "1")
                 {
                     String Url = GetURLWsOnline("SModdoOnlineSoap");
                     //codigo comentado sobre comprobar una url
                     if (CheckURLWs(Url, 10000))
                     {
                         Ds = objCapaDatos.GeDataSetUpVentas(_Venta.Id_Ticket, _Venta.Id_Tienda, _Fecha);

                         _wsOnline = new ServicioModdoOnline.SModdoOnlineSoapClient();

                         Result = _wsOnline.SetUpVentas(Ds, DIRFTP + "|" + PaswordWs, _Fecha, _Venta.Id_Tienda, _Venta.Id_Ticket);
                         //registramos en el historico web services
                         objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, "UPVENTAS", "UP", _Venta.Id_Ticket, "", DIRFTP, Result);
                     }
                     else
                     {
                         objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, "UPVENTAS", "UP", _Venta.Id_Ticket, "", DIRFTP, 0);

                     }
                 }
             }
             catch (Exception ex)
             {
                 log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                 objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, "UPVENTAS", "UP", _Venta.Id_Ticket, "", DIRFTP, Result);
                 throw new Exception(ex.Message, ex.InnerException);
             }
         }
        internal virtual void InvokeUpCliente9Ws(int opcion)
        {

            Int16 Result = 0;
            String DIRFTP = String.Empty;
            String Online = String.Empty;
            string accion = "";
            short opcAccion = Convert.ToInt16(opcion);

            try
            {

                DataSet Ds = new DataSet();
                ServicioModdoOnline.SModdoOnlineSoapClient _wsOnline;


                objCapaDatos.GetConfOnline(_Venta.Id_Tienda, ref DIRFTP, ref Online);

                if (Online == "1")
                {
                    String Url = GetURLWsOnline("SModdoOnlineSoap");
                    accion= (opcion == 1) ?  "SUBIDA": "SUBIDA TARJETA";
                    //codigo comentado sobre comprobar una url
                    if (CheckURLWs(Url, 10000))
                    {
                        Ds = objCapaDatos.GeDataSetUpClientes(Convert.ToInt32(_Venta.Id_Cliente_N), opcion);
                        _wsOnline = new ServicioModdoOnline.SModdoOnlineSoapClient();
                        Result = _wsOnline.SetUpDownClientes(ref Ds, DIRFTP, _Fecha, _Venta.Id_Tienda.ToString(), _Venta.Id_Cliente_N.ToString(), Convert.ToInt16(opcion));
                        
                        //registramos en el historico web services
                        objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, accion, "UP", _Venta.Id_Cliente_N.ToString(), "", DIRFTP, Result);
                    }
                    else
                    {
                        objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, accion, "UP", _Venta.Id_Cliente_N.ToString(), "", DIRFTP, 0);

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                objCapaDatos.InsertarHistoricoWs(_Venta.Id_Tienda, _Fecha, "CLIENTES", "UP", _Venta.Id_Cliente_N.ToString(), "", DIRFTP, Result);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
       #endregion 
         



    }



}
