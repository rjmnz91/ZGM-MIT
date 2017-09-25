using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Reflection;
using System.Diagnostics;
using CapaDatos;
using System.Web;
using DLLGestionVenta;
using DLLGestionVenta.Models;


namespace AVE.Servicios
{

    public class CarritoAVE_WS : ICarritoAVE_WS
    {
        //Logger
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///Inserta un pedido, un carrito nuevo, y linea de carrito para ese carrito
        /// </summary>
        /// <param name="idCarrito">identificador de carrito, si viene a -1 quiere decir que hay que crear nuevo carrito, si no, se añade en el existente</param>
        /// <param name="referencia">referencia del articulo</param>
        /// <param name="talla">talla del articulo</param>
        /// <param name="listaStockTiendas">Lista con el stock y la tienda</param>
        /// <returns>Identificador del carrito, si hay error, devuelve la excepción</returns>
        public int InsertarEnCarrito(int idCarrito, string referencia, string talla, List<StockTienda> listaStockTiendas, float precioOriginal, float precioActualizado)
        {
            int idArticulo;
            int idPedido;
            int idLineaCarrito;

            string idMaquina = null;

            ClsCapaDatos capaDatos = new ClsCapaDatos();
            SqlTransaction transaction = null;
            try
            {

                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                transaction = capaDatos.GetTransaction();

                /// idCarrito = -1 se trata de un carrito nuevo. Si no se trabaja sobre el carrito indicado.
                if (idCarrito == -1)
                {
                    //obtener idarticulo buscando por referencia
                    idArticulo = capaDatos.GetIdArticulo(referencia, transaction);

                    //insertar pedido
                    idPedido = capaDatos.InsertarPedido(idArticulo, talla, transaction);

                    //Insertar carrito
                    if (HttpContext.Current == null)
                    {
                        idMaquina = "127.0.0.1"; //Si llega HttpContext.Current  (Request) a null, le asignamos el valor del host local para que no salte excepcion
                    }
                    else
                    {
                        idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                    }

                    idCarrito = capaDatos.InsertarCarrito(transaction, idMaquina);

                    //insertar linea carrito
                    idLineaCarrito = capaDatos.InsertarLineaCarrito(idArticulo, idCarrito, idPedido, talla, precioOriginal, precioActualizado, transaction);

                    //Insertamos todo el stock de las tiendas de ese articulo
                    if (listaStockTiendas != null && listaStockTiendas.Count > 0)
                    {
                        foreach (StockTienda item in listaStockTiendas)
                        {
                            if (item.stock > 0 && item.tienda != null)
                                capaDatos.InsertarStockTiendaArticulo(idLineaCarrito, idCarrito, item.tienda, item.stock, transaction);
                        }
                    }

                    transaction.Commit();

                    return idCarrito;

                }
                else
                {
                    //Comprobar siempre que el carrito existe
                    capaDatos.ValidarCarrito(idCarrito, transaction);

                    //obtener idarticulo buscando por referencia
                    idArticulo = capaDatos.GetIdArticulo(referencia, transaction);

                    //insertar pedido
                    idPedido = capaDatos.InsertarPedido(idArticulo, talla, transaction);

                    //insertar linea carrito a ese idCarrito
                    idLineaCarrito = capaDatos.InsertarLineaCarrito(idArticulo, idCarrito, idPedido, talla, precioOriginal, precioActualizado, transaction);

                    //Insertamos todo el stock de las tiendas de ese articulo
                    if (listaStockTiendas != null && listaStockTiendas.Count > 0)
                    {
                        foreach (StockTienda item in listaStockTiendas)
                        {
                            if (item.stock > 0 && item.tienda != null)
                                capaDatos.InsertarStockTiendaArticulo(idLineaCarrito, idCarrito, item.tienda, item.stock, transaction);
                        }
                    }

                    transaction.Commit();

                    return idCarrito;

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                Log.Error(capaDatos.GetParameters(idCarrito, referencia, talla) + ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (capaDatos != null)
                    capaDatos.ReleaseTransaction();
            }

        }

        /// <summary>
        /// Elimina el carrito, las lineas de ese carrito, y el pedido correspondiente a cada linea del carrito.Cada línea es un pedido
        /// </summary>
        /// <param name="idCarrito">Identificador del carrito</param>
        /// <returns>true/false dependiendo si se ha podido eliminar o no</returns>
        public bool EliminarCarrito(int idCarrito)
        {
            bool eliminado;
            ClsCapaDatos capaDatos = new ClsCapaDatos();
            SqlTransaction transaction = null;

            try
            {
                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                transaction = capaDatos.GetTransaction();

                //Comprobar siempre que el carrito existe
                capaDatos.ValidarCarrito(idCarrito, transaction);

                eliminado = capaDatos.EliminaCarrito(idCarrito, transaction);

                if (eliminado)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                Log.Error(capaDatos.GetParameters(idCarrito) + ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (capaDatos != null)
                    capaDatos.ReleaseTransaction();
            }
        }


        /// <summary>
        /// Inserta una devolucion
        /// </summary>
        /// <param name="IdTicket">Identificador del TICKET</param>
        /// <param name="IdTienda">Identificador de la Tienda</param>
        /// <param name="IdArticulo">Identificador del articulo</param>
        /// <param name="strTalla">Nombre de la talla</param>
        /// <returns>ok./NOK + EXCEPCION
        public string InsertaDevolucion(string IdTicket, string orden, string IdTienda, int IdArticulo, string strTalla)
        {
            string result = "";
            string IdTicketOld = IdTicket;
            string Id_NewTicket = "";
            string Terminal="";
            double TotalEuro = 0;
            double ImporteDescontadoC9 = 0;
            string Id_Cliente_N = "0";
            string cadConex = "";
            DateTime fecha;
            string IdEmpleado="";
            int devoNine = 0;
            string FPago = "";
            int resulWS = 0;
            string strAutorizacion = "";
            string strTarjeta = "";
            
            ProcesarVenta objVenta = new ProcesarVenta();
            int nOK = 0;
            ClsCapaDatos capaDatos = new ClsCapaDatos();
            try
            {
                Log.Error("entrando los datos recibidos son Idticket: " + IdTicket + ", orden:" + orden +",Tienda:" + IdTienda + ",art:" + IdArticulo + ",talla"+ strTalla );
                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                cadConex = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                nOK = capaDatos.InsertaDevolucion(IdTicket, orden, IdTienda, IdArticulo, strTalla, ref result);
                if (nOK != 1)
                {
                    Log.Error("Error en la BD. Al intentar crear el nuevo ticket de devolucion." + result);
                }
                else 
                {
                    if (result.Length > 0)
                    {
                        string responseBD = result.Substring(0, 2);
                        if (responseBD == "OK")
                        {
                            string respuesta = result.Substring(3, result.Length - 3);
                            string[] Datos = respuesta.Split(new Char[] { '|' });
                            int longi = Datos.Length;
                            if (longi > 0)
                            {
                                Id_NewTicket = Datos[0].ToString();
                                devoNine = Convert.ToInt32(Datos[1].ToString());
                                if (devoNine > 0)
                                {
                                    Log.Error("cliente nine devuelto");
                                    FPago = Datos[2].ToString();
                                    Terminal = Datos[3].ToString();
                                    TotalEuro = Convert.ToDouble(Datos[4].ToString());
                                    ImporteDescontadoC9 = Convert.ToDouble(Datos[5].ToString());
                                    Id_Cliente_N = Datos[6].ToString();
                                    fecha = Convert.ToDateTime(Datos[7].ToString());
                                    IdEmpleado = Datos[8].ToString();
                                    DateTime fechaActual = DateTime.Now;
                                    resulWS = Comun.SolicitaRedencionDevo(IdTienda, IdEmpleado, Terminal, fechaActual, FPago, Id_Cliente_N, TotalEuro, ref strAutorizacion, ref strTarjeta);
                                    if (resulWS == 1 && strAutorizacion != "")
                                    {
                                        result = objVenta.RedencionDevolucion(Id_NewTicket, IdTicketOld, Terminal, IdTienda, TotalEuro, ImporteDescontadoC9, Id_Cliente_N, Convert.ToString(IdArticulo), cadConex, fecha, IdEmpleado, FPago, strTarjeta, strAutorizacion);
                                        Log.Info("Generada la solicitud de redencion con autorizacion n:" + strAutorizacion);
                                    }
                                    else
                                    {
                                        return result = "NOK. NO SE PUDO EJECUTAR LA SOLICITUD REDENCION PREVIA";

                                    }
                                    if (result == "OK" || result == "OK.") return result = "OK." + Id_NewTicket;
                                }
                                result = "OK." + Id_NewTicket;
                            }

                        }
                    }
                    else return result="NOK. No se devolvio información para hacer la redención de la devolución";
                }

            }
            catch (Exception ex) {
                Log.Error("excepcion " + ex.Message.ToString());
             //   Log.Error(capaDatos.GetParameters(IdTicket,orden,IdTienda,IdArticulo,strTalla) + ex.Message, ex);
                result = "NOK.Error NET." + ex.Message.ToString();
                throw ex;
            }
            
            return result;
        }
        /// <summary>
        /// Inserta el valor de UsoClienteNine en TRETAIL
        /// </summary>
        /// <param name="Valor">Valor a guardar para UsoClienteNine</param>
        /// <param name="IdTienda">Identificador de la Tienda</param>
        /// <param name="IdArticulo">Identificador del articulo</param>
        /// <param name="strTalla">Nombre de la talla</param>
        /// <returns>0/1
        public int TiendaCamper(int Valor)
        {
            int result = 0;
            try
            {
                ClsCapaDatos capaDatos = new ClsCapaDatos();
                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                result = capaDatos.TiendaCamper(Valor);

            }
            catch (Exception ex) {
                Log.Error("Error al  updatar Tienda Camper." + ex.Message.ToString());
            }
            return result;
        }
    }
}
