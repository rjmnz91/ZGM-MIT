using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLLGestionVenta.Models;
using System.Data;
using DLLGestionVenta.CapaDatos;


namespace DLLGestionVenta.BL
{

    public class StockTiendaBL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string KEY_ARTICULO_FORMAT = "{0}#{1}#{2}";
        private const string KEY_TIENDA_ECOMMERCE = "ECOMMERCE";
        private const string KEY_TIENDA_CENTRAL = "0270";
        private const string KEY_TIENDA_EXTERNA = "0472";
        
        
        public List<string> ComprobarStock(string idCarrito, string idTienda, string idUsuario, string userHermess, string passHermess,string siteHermes, string idTerminal,string Tienda)
        {
            Dictionary<string, int> stockSolicitado = new Dictionary<string, int>();
            string referencia;
            string talla;
            string idLineaCarrito;
            string idArticulo;
            string idPedido;
            string precioArticulo;
            string idTicket;

            //Obtenemos las lineas del carrito
            DataSet dsLineasCarrito = StockTiendaDAL.GetLineasCarrito(idCarrito);
            List<string> articulosSinStock = new List<string>();
            

            foreach (DataRow row in dsLineasCarrito.Tables[0].Rows)
            {
                int hayStock = 0;

                idArticulo = row["IdArticulo"].ToString();
                referencia = StockTiendaDAL.GetReferencia(row["IdArticulo"].ToString());
                talla = StockTiendaDAL.GetTallaLineaCarrito(row["IdPedido"].ToString());
                idLineaCarrito = row["id_Carrito_Detalle"].ToString();
                idPedido = row["IdPedido"].ToString();
                precioArticulo = row["PVPACT"].ToString();
 
                idTicket= StockTiendaDAL.ObtieneUltimoTicketTR(Tienda,idTerminal);
                if (idTicket != "")
                    idTicket = idTicket + "/" + idTerminal + "/" + Tienda + "-" + idPedido;
                else
                    log.Error("No se ha obtenido ticket");
                

                HermesModaliaWebServiceReference.HermesImplClient wsStock = new HermesModaliaWebServiceReference.HermesImplClient();
                

                //if (!string.IsNullOrEmpty(idTienda))
                //{
                    string stock = wsStock.getStock(userHermess, passHermess, siteHermes, referencia, talla);
                    hayStock = Convert.ToInt32(stock);
                //}
                
                //if (!hayStock) hayStock = StockThinkRetail(referencia, talla, idPedido, KEY_TIENDA_CENTRAL,KEY_TIENDA_EXTERNA, idUsuario, userThinkretail, passThinkretail,idTicket);

                
                //if (!hayStock) hayStock = StockThinkRetail(referencia, talla, idPedido, KEY_TIENDA_ECOMMERCE,KEY_TIENDA_EXTERNA, idUsuario, userThinkretail, passThinkretail,idTicket);

                //if (!hayStock) hayStock = StockEnTiendas(referencia, talla, null, idLineaCarrito, ref stockSolicitado);

                if (hayStock <= 0) articulosSinStock.Add(idLineaCarrito + "#" + StockTiendaDAL.GetDescripcionArticulo(idArticulo) + "#" + precioArticulo);

            }
            return articulosSinStock;
        }

        private static bool StockThinkRetail(string referencia, string talla, string idPedido, string almacen, string almacenSalida, string idUsuario, string userThinkretail, string passThinkretail, string idTicket)
        {
            try
            {
                bool hayStock = false;
        
                //string keyArticuloTienda = string.Empty;
                // keyArticuloTienda = string.Format(KEY_ARTICULO_FORMAT, referencia, talla, KEY_CENTRAL);

                ThinkRetailServiceWeb.wsThinkRetail wsThinkRetail = new ThinkRetailServiceWeb.wsThinkRetail();
               // wsThinkRetail.Url = System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString();
                

                List<ThinkRetailServiceWeb.Row_Input> row_Inputs = new List<ThinkRetailServiceWeb.Row_Input>();
                row_Inputs.Add(new ThinkRetailServiceWeb.Row_Input() { Cantidad = 1, NumMaterial = referencia, Posicion = 001, Talla = talla });

                
                wsThinkRetail.CookieContainer = new System.Net.CookieContainer();
                

                if (wsThinkRetail.Login(userThinkretail, passThinkretail))
                {
                    
                    ThinkRetailServiceWeb.Salida respuesta = wsThinkRetail.CreaReservaTR(idTicket, almacen, almacenSalida ,idUsuario, row_Inputs.ToArray());
                    
                    if (respuesta.Sts.Equals("ERROR"))
                    {
                        hayStock = false;
                    }
                    else
                    {
                        hayStock = true;
                        
                    }
                    try
                    {
                        string Descrip = "";

                        if (respuesta.Mensajes != null)
                        {
                            foreach (ThinkRetailServiceWeb.Mensaje msjs in respuesta.Mensajes)
                            {
                                Descrip = Descrip + msjs.Descripcion;

                            }
                            log.Error("Descripcion_Reserva: " + Descrip);
                        }
                        else {
                            log.Error("respuesta.Mensajes devuelto es null ");
                        }
                        
                       
                        
                        CapaDatos.StockTiendaDAL.ActualizarReservaThinkRetail(Convert.ToInt32(idPedido), Descrip, respuesta.Sts);
                   }
                   catch (Exception ex)
                        {
                            log.Error(ex.Message.ToString());
                        }
                }

                //StockTienda stockCentral = new StockTienda();
                //stockCentral.tienda = KEY_ECOMMERCE;
                //stockCentral.stock = 0; //Lo que devuelva el servicio.

                //hayStock = ValidarStockEnTienda(keyArticuloTienda, stockCentral, ref stockSolicitado);

                return hayStock;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        private static bool StockEnTiendas(string referencia, string talla, string idTienda, string idLineaCarrito, ref Dictionary<string, int> stockSolicitado)
        {

            bool hayStock = false;
            string keyArticuloTienda = string.Empty;

            List<StockTienda> stockTiendasLineaCarrito = new List<StockTienda>();
            stockTiendasLineaCarrito = StockTiendaDAL.GetStockTiendasLineaCarrito(idLineaCarrito);


            if (!string.IsNullOrEmpty(idTienda))
            {
                keyArticuloTienda = string.Format(KEY_ARTICULO_FORMAT, referencia, talla, idTienda);
                StockTienda stockTiendaLineaCarrito = stockTiendasLineaCarrito.FirstOrDefault(t => t.tienda == idTienda);
                hayStock = ValidarStockEnTienda(keyArticuloTienda, stockTiendaLineaCarrito, ref stockSolicitado);
            }
            else
            {

                foreach (StockTienda stockTiendaLineaCarrito in stockTiendasLineaCarrito)
                {
                    keyArticuloTienda = string.Format(KEY_ARTICULO_FORMAT, referencia, talla, stockTiendaLineaCarrito.tienda);
                    hayStock = ValidarStockEnTienda(keyArticuloTienda, stockTiendaLineaCarrito, ref stockSolicitado);
                    if (hayStock)
                        break;
                }
            }

            return hayStock;
        }

        private static bool ValidarStockEnTienda(string keyArticuloTienda, StockTienda stockTiendaLineaCarrito, ref Dictionary<string, int> stockSolicitado)
        {
            bool hayStock = false;

            if (stockTiendaLineaCarrito != null && !stockSolicitado.ContainsKey(keyArticuloTienda))
            {
                /// Hay stock en la tienda pero aun no se ha solicitado nada.
                if (stockTiendaLineaCarrito.stock > 0)
                    hayStock = true;
                else
                    hayStock = false;
            }
            else if (stockTiendaLineaCarrito != null && stockSolicitado.ContainsKey(keyArticuloTienda) && stockSolicitado[keyArticuloTienda] < stockTiendaLineaCarrito.stock)
            {
                /// Hay stock en la tienda  se ha solicitado stock y aun no lo superamos.
                hayStock = true;
            }
            else
            {
                ///O no hay stock en la tienda o se ha rebasado el stock solicitado
                hayStock = false;
            }


            /// Insertamos o actualizamos stock solicitado por referecia, talla y tienda.
            if (hayStock)
            {
                if (stockSolicitado.ContainsKey(keyArticuloTienda))
                    stockSolicitado[keyArticuloTienda]++;
                else
                    stockSolicitado.Add(keyArticuloTienda, 1);
            }
            return hayStock;
        }

        public bool cancelarReservaAC(string idCarrito, string idTienda, string idUsuario, string userThinkretail, string passThinkretail, string idTerminal, string Tienda)
     
           {
               Dictionary<string, int> stockSolicitado = new Dictionary<string, int>();
               string referencia;
               string talla;
               string idLineaCarrito;
               string idArticulo;
               string idPedido;
               string idTicket;

               //Obtenemos las lineas del carrito
               DataSet dsLineasCarrito = StockTiendaDAL.GetLineasCarrito(idCarrito);
               List<string> articulosSinStock = new List<string>();

               ThinkRetailServiceWeb.wsThinkRetail wsThinkRetail = new ThinkRetailServiceWeb.wsThinkRetail();

                wsThinkRetail.CookieContainer = new System.Net.CookieContainer();


                if (wsThinkRetail.Login(userThinkretail, passThinkretail))
                {
                    foreach (DataRow row in dsLineasCarrito.Tables[0].Rows)
                    {
                        string estadoReserva = row["EstadoReservaThinkRetail"].ToString();
                        if (estadoReserva != null && estadoReserva.Equals("OK"))
                        {

                            referencia = StockTiendaDAL.GetReferencia(row["IdArticulo"].ToString());
                            talla = StockTiendaDAL.GetTallaLineaCarrito(row["IdPedido"].ToString());
                            idLineaCarrito = row["id_Carrito_Detalle"].ToString();
                            idPedido = row["IdPedido"].ToString();

                            idTicket = StockTiendaDAL.ObtieneUltimoTicketTR(Tienda, idTerminal);
                            if (idTicket != "")
                            {
                                idTicket = idTicket + "/" + idTerminal + "/" + Tienda + "-" + idPedido;
                                ThinkRetailServiceWeb.SalidaMsg respuesta = wsThinkRetail.CancelaReservaTR(idTicket, KEY_TIENDA_CENTRAL);
                                try
                                {
                                    string Descrip = "";
                                    log.Error("Descripcion:" + respuesta.Descripcion);

                                    if (respuesta.Mensajes != null)
                                    {
                                        foreach (ThinkRetailServiceWeb.Mensaje msjs in respuesta.Mensajes)
                                        {
                                            Descrip = Descrip + msjs.Descripcion;

                                        }
                                        log.Error("Descripcion_Cancelacion_Carro: " + Descrip);
                                    }
                                    else
                                    {
                                        log.Error("respuesta.Mensajes devuelto es null ");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    log.Error(ex.Message.ToString());
                                }
                            }
                            else
                            {
                                log.Error("No se ha obtenido ticket");
                            }

                        }
                    }
                }
               return true;
        }

    }


}
