using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using CapaDatos;
using System.Data.SqlClient;

namespace DLLGestionVenta.BL
{
    public class HermesXMLDocumentBL
    {
        #region ConstantesXML
        private const string TAG_SOLICITUDES = "SOLICITUDES";
        private const string TAG_SOLICITUD = "SOLICITUD";

        private const string TAG_PRODUCTOS = "PRODUCTOS";
        private const string TAG_PRODUCTO = "PRODUCTO";

        private const string TAG_CODIGO_ALFA = "CODIGO_ALFA";
        private const string TAG_REFERENCIA = "REFERENCIA";
        private const string TAG_TALLA = "TALLA";
        private const string TAG_IMPORTE_PRODUCTO = "IMPORTE";
        private const string TAG_IMPORTE_REBAJADO_PRODUCTO = "IMPORTE_REBAJADO";
        private const string TAG_CANTIDAD = "CANTIDAD";
        private const string TAG_NUMERO_PEDIDO_ORIGINAL_PRODUCTO = "NUMERO_PEDIDO_ORIGINAL";
        private const string TAG_ID_TIENDA_RESERVA = "ID_TIENDA_RESERVA";
        private const string TAG_DESCRIPCION_RESERVA = "DESCRIPCION_RESERVA";

        private const string TAG_NUMERO_PEDIDO_ORIGINAL = "NUMERO_PEDIDO_ORIGINAL";
        private const string TAG_FECHA_PEDIDO = "FECHA_PEDIDO";
        private const string TAG_TIPO = "TIPO";
        private const string THINKRETAL = "THINKRETAL";
        private const string TAG_TIPO_ENVIO = "TIPO_ENVIO";
        private const string TAG_ID_TIENDA = "ID_TIENDA";
        private const string TAG_FACTURA_REQUERIDA = "FACTURA_REQUERIDA";
        private const string TAG_CLIENTE_FACTURA = "ID_CLIENTE_EXTERNO";

        private const string TAG_NOMBRE_ENVIO = "NOMBRE_ENVIO";
        private const string TAG_APELLIDO_1_ENVIO = "APELLIDO_1_ENVIO";
        private const string TAG_APELLIDO_2_ENVIO = "APELLIDO_2_ENVIO";
        private const string TAG_DOMICILIO_ENVIO = "DOMICILIO_ENVIO";
        private const string TAG_POBLACION_ENVIO = "POBLACION_ENVIO";
        private const string TAG_PROVINCIA_ENVIO = "PROVINCIA_ENVIO";
        private const string TAG_CODIGO_POSTAL_ENVIO = "CODIGO_POSTAL_ENVIO";
        private const string TAG_TELEFONO_ENVIO = "TELEFONO_ENVIO";
        private const string TAG_TELEFONO_MOVIL_ENVIO = "TELEFONO_MOVIL_ENVIO";
        private const string TAG_EMAIL_ENVIO = "EMAIL_ENVIO";
        private const string TAG_NUMERO_INTERIOR_ENVIO = "NUMERO_INTERIOR_ENVIO";
        private const string TAG_NUMERO_EXTERIOR_ENVIO = "NUMERO_EXTERIOR_ENVIO";

        private const string TAG_NOMBRE = "NOMBRE";
        private const string TAG_RFC = "RFC";
        private const string TAG_DIRECCION = "DOMICILIO";
        private const string TAG_ESTADO = "PROVINCIA";
        private const string TAG_CIUDAD = "POBLACION";
        private const string TAG_PAIS = "PAIS";
        private const string TAG_CODIGO_POSTAL = "CODIGO_POSTAL";
        private const string TAG_TELEFONO = "TELEFONO";
        private const string TAG_TELEFONO_MOVIL = "TELEFONO_MOVIL";
        private const string TAG_COLONIA = "COLONIA";
        private const string TAG_NUMERO_INTERIOR = "NUMERO_INTERIOR";
        private const string TAG_NUMERO_EXTERIOR = "NUMERO_EXTERIOR";

        private const string TAG_COLONIA_ENVIO = "COLONIA_ENVIO";
        private const string TAG_OBSERVACIONES = "OBSERVACIONES";
        private const string TAG_IMPORTE_TOTAL = "IMPORTE_TOTAL";

        private const string TAG_DESCUENTOS = "DESCUENTOS";
        private const string TAG_DESCUENTO = "DESCUENTO";
        private const string TAG_DESCRIPCION_DESCUENTO = "DESCRIPCION";
        private const string TAG_IMPORTE_DESCUENTO = "IMPORTE";

        private const string TAG_FORMAS_DE_PAGO = "FORMAS_DE_PAGO";
        private const string TAG_FORMA_PAGO = "FORMA_PAGO";
        private const string TAG_DESCRIPCION_PAGO = "DESCRIPCION";
        private const string TAG_IMPORTE_PAGO = "IMPORTE";

        private const string TAG_GASTOS_ENVIO = "GASTOS_ENVIO";

        private const string TAG_ESTADO_PEDIDO = "ESTADO_PEDIDO";
        private const string TAG_COMENTARIOS = "COMENTARIOS";

        #endregion

        # region Keys WebConfig

        public static string UserHERMESWSAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserHERMESWS"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["UserHERMESWS"].ToString();
                else
                    return "ERROR";
            }
        }

        public static string PassHERMESWSAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["PassHERMESWS"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["PassHERMESWS"].ToString();
                else
                    return "ERROR";
            }
        }

        public static string IdSiteHERMESWSAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IdSiteHERMESWS"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["IdSiteHERMESWS"].ToString();
                else
                    return "ERROR";
            }
        }

        #endregion

        public static string EnviarXMLHermes(DataView dvVenta, DataSet dsFormasPago, int idCarrito, string idTienda, string idTiendaEnvio, decimal gastosEnvio, float importeTotal,string strEmail, string  strNombre,string comentarios,string idAlmacenCentral,string idClienteFact,ref string mensajeOut)
        {
            string xmlRespuesta;
            string respuesta;
            string resp = string.Empty;
            string idPedidoHermes = string.Empty;
            ClsCapaDatos capaDatos = new ClsCapaDatos();

            XDocument solicitudesXML = HermesXMLDocumentBL.GenerarXML(dvVenta, dsFormasPago, idCarrito, idTienda, idTiendaEnvio, gastosEnvio, importeTotal, strEmail,strNombre,idAlmacenCentral, comentarios,idClienteFact);

            //Enviar datos al webservice con los datos de la venta

            HermesModaliaWebServiceReference.HermesImplClient webservice = new HermesModaliaWebServiceReference.HermesImplClient();
            xmlRespuesta = webservice.insertOrder(UserHERMESWSAppSettings, PassHERMESWSAppSettings, IdSiteHERMESWSAppSettings, solicitudesXML.ToString());
          //  xmlRespuesta = "<?xml version=\"1.0\" encoding=\"iso-8859-1\" standalone=\"yes\" ?><PEDIDOS><PEDIDO> <NUMERO_PEDIDO_ORIGINAL>1015</NUMERO_PEDIDO_ORIGINAL> <RESPUESTA><![CDATA[OK]]></RESPUESTA> <ERROR><![CDATA[]]></ERROR> <PEDIDO_HERMES>N166002</PEDIDO_HERMES></PEDIDO></PEDIDOS>";

            if (xmlRespuesta.IndexOf("<RESPUESTA>") > 0)
            {
                int firstResp = xmlRespuesta.IndexOf("<RESPUESTA>") + "<RESPUESTA>".Length;
                int lastResp = xmlRespuesta.LastIndexOf("</RESPUESTA>");
                respuesta = xmlRespuesta.Substring(firstResp, lastResp - firstResp);

                if (respuesta.Contains("OK"))
                {
                    int firstIdPedido = xmlRespuesta.IndexOf("<PEDIDO_HERMES>") + "<PEDIDO_HERMES>".Length;
                    int lastIdPedido = xmlRespuesta.LastIndexOf("</PEDIDO_HERMES>");
                    idPedidoHermes = xmlRespuesta.Substring(firstIdPedido, lastIdPedido - firstIdPedido);

                    capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 1);

                    return idPedidoHermes;
                }
                else
                {
                    int firstError = xmlRespuesta.IndexOf("<ERROR>") + "<ERROR>".Length;
                    int lastError = xmlRespuesta.LastIndexOf("</ERROR>");
                    mensajeOut = xmlRespuesta.Substring(firstError, lastError - firstError);
                    mensajeOut.Trim('<','!','[',']');

                    capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 0);
                    return string.Empty;
                }
            }
            else {
                capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 0);
                return string.Empty;
            }
        }

        private static XDocument GenerarXML(DataView dvVenta, DataSet dsFormasPago, int idCarrito, string idTienda, string idtiendaEnvio, decimal gastosEnvio, float importeTotal, string strEmail, string strNombre,string idAlmacenCentral, string comentarios,string idClienteFac)
        {
            ///OBTENER DATOS CON GESTION VENTAS
            ///
            XDocument solicitud = null;
            List<XElement> elementosSolicitud = new List<XElement>();

            elementosSolicitud.AddRange(generarCabecera(idCarrito, idTienda, idtiendaEnvio, comentarios, idClienteFac));
            if (string.IsNullOrEmpty(idtiendaEnvio))
                elementosSolicitud.AddRange(generarEntrega(idCarrito));
            else
            {
                elementosSolicitud.AddRange(generarNomMail(strEmail, strNombre));
                elementosSolicitud.AddRange(generarFacturacion(idCarrito));
               
            }
            elementosSolicitud.Add(new XElement(TAG_IMPORTE_TOTAL, new XCData(importeTotal.ToString())));
            elementosSolicitud.Add(new XElement(TAG_GASTOS_ENVIO, new XCData(gastosEnvio.ToString())));
            elementosSolicitud.Add(new XElement(TAG_FORMA_PAGO, string.Empty));
            elementosSolicitud.Add(generarProductos(dvVenta,idAlmacenCentral));
            elementosSolicitud.Add(generarDescuentos(dvVenta));
            elementosSolicitud.Add(generarFormasPago(dsFormasPago));

            XElement solictud = new XElement(TAG_SOLICITUD, elementosSolicitud);

            XElement Solicitudes = new XElement(TAG_SOLICITUDES, solictud);

            solicitud = new XDocument(Solicitudes);

            return solicitud;
        }

        public static string EnviarXMLHermesCancelacion(int idCarrito, ref string mensajeOut)
        {
            string xmlRespuesta;
            string respuesta;
            string resp = string.Empty;
            string idPedidoHermes = string.Empty;
            ClsCapaDatos capaDatos = new ClsCapaDatos();

            XDocument solicitudesXML = HermesXMLDocumentBL.GenerarXMLCancelacion(idCarrito);

            //Enviar datos al webservice con los datos de la venta

            HermesModaliaWebServiceReference.HermesImplClient webservice = new HermesModaliaWebServiceReference.HermesImplClient();
            xmlRespuesta = webservice.insertOrder(UserHERMESWSAppSettings, PassHERMESWSAppSettings, IdSiteHERMESWSAppSettings, solicitudesXML.ToString());
            //  xmlRespuesta = "<?xml version=\"1.0\" encoding=\"iso-8859-1\" standalone=\"yes\" ?><PEDIDOS><PEDIDO> <NUMERO_PEDIDO_ORIGINAL>1015</NUMERO_PEDIDO_ORIGINAL> <RESPUESTA><![CDATA[OK]]></RESPUESTA> <ERROR><![CDATA[]]></ERROR> <PEDIDO_HERMES>N166002</PEDIDO_HERMES></PEDIDO></PEDIDOS>";

            if (xmlRespuesta.IndexOf("<RESPUESTA>") > 0)
            {
                int firstResp = xmlRespuesta.IndexOf("<RESPUESTA>") + "<RESPUESTA>".Length;
                int lastResp = xmlRespuesta.LastIndexOf("</RESPUESTA>");
                respuesta = xmlRespuesta.Substring(firstResp, lastResp - firstResp);

                if (respuesta.Contains("OK"))
                {
                    int firstIdPedido = xmlRespuesta.IndexOf("<PEDIDO_HERMES>") + "<PEDIDO_HERMES>".Length;
                    int lastIdPedido = xmlRespuesta.LastIndexOf("</PEDIDO_HERMES>");
                    idPedidoHermes = xmlRespuesta.Substring(firstIdPedido, lastIdPedido - firstIdPedido);

                    capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 1);

                    return idPedidoHermes;
                }
                else
                {
                    int firstError = xmlRespuesta.IndexOf("<ERROR>") + "<ERROR>".Length;
                    int lastError = xmlRespuesta.LastIndexOf("</ERROR>");
                    mensajeOut = xmlRespuesta.Substring(firstError, lastError - firstError);
                    mensajeOut.Trim('<', '!', '[', ']');

                    capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 0);
                    return string.Empty;
                }
            }
            else
            {
                capaDatos.InsertarLogCarrito(idCarrito, solicitudesXML.ToString(), xmlRespuesta, 0);
                return string.Empty;
            }
        }

        private static XDocument GenerarXMLCancelacion(int idCarrito)
        {
            ///OBTENER DATOS CON GESTION VENTAS
            ///
            XDocument solicitud = null;
            List<XElement> elementosSolicitud = new List<XElement>();

            elementosSolicitud.AddRange(generarCabeceraCancelacion(idCarrito));
            elementosSolicitud.Add(new XElement(TAG_ESTADO_PEDIDO, new XCData("8")));
            
            XElement solictud = new XElement(TAG_SOLICITUD, elementosSolicitud);

            XElement Solicitudes = new XElement(TAG_SOLICITUDES, solictud);

            solicitud = new XDocument(Solicitudes);

            return solicitud;
        }



        private static List<XElement> generarCabecera(int idCarrito, string idTienda, string idTiendaEnvio,string comentarios,string idClienteFac)
        {
            List<XElement> cabecera = new List<XElement>();

            bool requiereFactura = GetFacturacion(idCarrito);

            cabecera.Add(new XElement(TAG_NUMERO_PEDIDO_ORIGINAL, idCarrito));
            cabecera.Add(new XElement(TAG_FECHA_PEDIDO, DateTime.Now.ToString()));
            cabecera.Add(new XElement(TAG_TIPO, (THINKRETAL)));
            cabecera.Add(new XElement(TAG_COMENTARIOS, comentarios));
            cabecera.Add(new XElement(TAG_FACTURA_REQUERIDA, requiereFactura));
            if (requiereFactura) cabecera.Add(new XElement(TAG_CLIENTE_FACTURA, idClienteFac));
            if (!string.IsNullOrEmpty(idTiendaEnvio))
            {
                cabecera.Add(new XElement(TAG_TIPO_ENVIO, "TIENDA"));
                cabecera.Add(new XElement(TAG_ID_TIENDA, idTiendaEnvio));
            }
            else
            {
                cabecera.Add(new XElement(TAG_TIPO_ENVIO, "DOMICILIO"));
                cabecera.Add(new XElement(TAG_ID_TIENDA, string.Empty));
            }

            return cabecera;
        }

        private static List<XElement> generarCabeceraCancelacion(int idCarrito)
        {
            List<XElement> cabecera = new List<XElement>();

            cabecera.Add(new XElement(TAG_NUMERO_PEDIDO_ORIGINAL, idCarrito));
            cabecera.Add(new XElement(TAG_FECHA_PEDIDO, DateTime.Now.ToString()));
            cabecera.Add(new XElement(TAG_TIPO, (THINKRETAL)));

            return cabecera;
        }


        private static XElement generarProductos(DataView dvVenta, string idAlmacenCentral)
        {
            List<XElement> productos = new List<XElement>();
            string referencia = string.Empty;
            foreach (DataRow row in dvVenta.Table.Rows)
            {
                referencia = GetReferencia(row["idArticulo"].ToString());
                productos.Add(generarProducto(referencia, referencia, row["Nombre_Talla"].ToString(), Convert.ToDecimal(row["PVPORI"].ToString()), Convert.ToDecimal(row["PVPACT"].ToString()), 1, row["idPedido"].ToString(), idAlmacenCentral));
            }
            return new XElement(TAG_PRODUCTOS, productos.ToArray());
        }



        private static XElement generarProducto(string codigoAlfa, string referencia, string talla, decimal importe, decimal importeRebajado, int cantidad, string idPedido, string idAlmacenCentral)
        {
            string estadoReserva = CapaDatos.StockTiendaDAL.GetEstadoReservaThinkRetail(idPedido);

            if (estadoReserva == "NULL" || estadoReserva == "ERROR")//Si no tenemos el ok del serivicio, solo almacenamos la descripcion de la respuesta
            {
                return new XElement(TAG_PRODUCTO,
                new XElement(TAG_CODIGO_ALFA, new XCData(codigoAlfa)),
                new XElement(TAG_REFERENCIA, new XCData(referencia)),
                new XElement(TAG_TALLA, new XCData(talla)),
                new XElement(TAG_IMPORTE_PRODUCTO, new XCData(importe.ToString())),
                new XElement(TAG_IMPORTE_REBAJADO_PRODUCTO, new XCData(importeRebajado.ToString())),
                new XElement(TAG_NUMERO_PEDIDO_ORIGINAL_PRODUCTO, new XCData(idPedido)),
                new XElement(TAG_DESCRIPCION_RESERVA, new XCData(CapaDatos.StockTiendaDAL.GetReservaThinkRetail(idPedido))),
                new XElement(TAG_CANTIDAD, new XCData(cantidad.ToString())));
            }
            else //EstadoReserva == "OK" guardamos tambien el id de Almacen Central
            {
                return new XElement(TAG_PRODUCTO,
                new XElement(TAG_CODIGO_ALFA, new XCData(codigoAlfa)),
                new XElement(TAG_REFERENCIA, new XCData(referencia)),
                new XElement(TAG_TALLA, new XCData(talla)),
                new XElement(TAG_IMPORTE_PRODUCTO, new XCData(importe.ToString())),
                new XElement(TAG_IMPORTE_REBAJADO_PRODUCTO, new XCData(importeRebajado.ToString())),
                new XElement(TAG_NUMERO_PEDIDO_ORIGINAL_PRODUCTO, new XCData(idPedido)),
                new XElement(TAG_DESCRIPCION_RESERVA, new XCData(CapaDatos.StockTiendaDAL.GetReservaThinkRetail(idPedido))),
                new XElement(TAG_ID_TIENDA_RESERVA, new XCData(idAlmacenCentral)),
                new XElement(TAG_CANTIDAD, new XCData(cantidad.ToString())));
            }

           

        }

        private static XElement generarFormasPago(DataSet dsFormasPago)
        {
            List<XElement> formasPago = new List<XElement>();

            DataTable dt = dsFormasPago.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                if (row["TipoPagoDetalle"].ToString()!="")
                formasPago.Add(generarFormaPago(row["TipoPagoDetalle"].ToString(), Convert.ToDecimal(row["Importe"].ToString())));
                else
                    formasPago.Add(generarFormaPago(row["TipoPago"].ToString(), Convert.ToDecimal(row["Importe"].ToString())));
            }
            return new XElement(TAG_FORMAS_DE_PAGO, formasPago.ToArray());
        }

        private static XElement generarFormaPago(string descripcion, decimal importe)
        {
            return new XElement(TAG_FORMA_PAGO,
                 new XElement(TAG_DESCRIPCION_PAGO, new XCData(descripcion.ToString())),
                 new XElement(TAG_IMPORTE_PAGO, new XCData(importe.ToString())));
        }

        private static XElement generarDescuentos(DataView dvVenta)
        {
            List<XElement> descuentos = new List<XElement>();

            foreach (DataRow row in dvVenta.Table.Rows)
            {
                descuentos.Add(generarDescuento(row["promocion"].ToString(), Convert.ToDecimal(row["DtoPromo"])));
            }
            return new XElement(TAG_DESCUENTOS, descuentos.ToArray());
        }

        private static XElement generarDescuento(string descripcion, decimal importe)
        {
            if (importe > 0)
            {
                return new XElement(TAG_DESCUENTO,
                     new XElement(TAG_DESCRIPCION_DESCUENTO, new XCData(descripcion)),
                     new XElement(TAG_IMPORTE_DESCUENTO, new XCData(importe.ToString())));
            }
            else
            {
                return new XElement(TAG_DESCUENTO,
                     new XElement(TAG_DESCRIPCION_DESCUENTO, string.Empty),
                     new XElement(TAG_IMPORTE_DESCUENTO, string.Empty));
            }
        }

        private static List<XElement> generarNomMail(string strMail, string strNombre) {

            List<XElement> listaEntrega = new List<XElement>();
            listaEntrega.Add(new XElement(TAG_NOMBRE_ENVIO, new XCData(strNombre)));
            listaEntrega.Add(new XElement(TAG_EMAIL_ENVIO, new XCData(strMail)));

            return listaEntrega;
        
        }
        private static List<XElement> generarEntrega(int idCarrito)
        {
            string[] apellidos = null;
            string apellido1, apellido2;

            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCarrito(idCarrito);

            List<XElement> listaEntrega = new List<XElement>();

            //separamos los apellidos para intoducir apellido1 y apellido2, si no, se mete lo que haya en entrega.Apellidos en los 2 apellidos para evitar errores 

            if (entrega.Apellidos.IndexOf(" ") > 0)
            {
                apellidos = entrega.Apellidos.Split(new Char[] { ' ' });
                apellido1 = apellidos[0];
                apellido2 = apellidos[1];
            }
            else
            {
                apellido1 = entrega.Apellidos;
                apellido2 = entrega.Apellidos;
            }

            listaEntrega.Add(new XElement(TAG_NOMBRE_ENVIO, new XCData(entrega.Nombre)));
            listaEntrega.Add(new XElement(TAG_APELLIDO_1_ENVIO, new XCData(apellido1)));
            listaEntrega.Add(new XElement(TAG_APELLIDO_2_ENVIO, new XCData(apellido2)));
            listaEntrega.Add(new XElement(TAG_DOMICILIO_ENVIO, new XCData(entrega.Direccion)));
            listaEntrega.Add(new XElement(TAG_NUMERO_INTERIOR_ENVIO, new XCData(entrega.NoInterior)));
            listaEntrega.Add(new XElement(TAG_NUMERO_EXTERIOR_ENVIO, new XCData(entrega.NoExterior)));
            listaEntrega.Add(new XElement(TAG_POBLACION_ENVIO, new XCData(entrega.Ciudad)));
            listaEntrega.Add(new XElement(TAG_PROVINCIA_ENVIO, new XCData(entrega.Estado)));
            listaEntrega.Add(new XElement(TAG_COLONIA_ENVIO, new XCData(entrega.Colonia)));
            listaEntrega.Add(new XElement(TAG_CODIGO_POSTAL_ENVIO, new XCData(entrega.CodPostal)));
            listaEntrega.Add(new XElement(TAG_TELEFONO_ENVIO, new XCData(entrega.TelfFijo)));
            listaEntrega.Add(new XElement(TAG_TELEFONO_MOVIL_ENVIO, new XCData(entrega.TelfMovil)));
            listaEntrega.Add(new XElement(TAG_EMAIL_ENVIO, new XCData(entrega.Email)));
            listaEntrega.Add(new XElement(TAG_OBSERVACIONES, new XCData(entrega.Referencia)));

            bool requiereFacturacion = GetFacturacion(idCarrito);

            if (requiereFacturacion) 
            {
                DLLGestionVenta.Models.FACTURACION_CARRITO facturacion = objDatos.ObtenerFacturacionCarrito(idCarrito);

                listaEntrega.Add(new XElement(TAG_NOMBRE, new XCData(facturacion.Nombre)));
                listaEntrega.Add(new XElement(TAG_RFC, new XCData(facturacion.Rfc)));
                listaEntrega.Add(new XElement(TAG_DIRECCION, new XCData(facturacion.Direccion)));
                listaEntrega.Add(new XElement(TAG_NUMERO_INTERIOR, new XCData(facturacion.NoInterior)));
                listaEntrega.Add(new XElement(TAG_NUMERO_EXTERIOR, new XCData(facturacion.NoExterior)));
                listaEntrega.Add(new XElement(TAG_ESTADO, new XCData(facturacion.Estado)));
                listaEntrega.Add(new XElement(TAG_CIUDAD, new XCData(facturacion.Ciudad)));
                listaEntrega.Add(new XElement(TAG_COLONIA, new XCData(facturacion.Colonia)));
                listaEntrega.Add(new XElement(TAG_PAIS, new XCData("")));
                listaEntrega.Add(new XElement(TAG_CODIGO_POSTAL, new XCData(facturacion.CodPostal)));
                listaEntrega.Add(new XElement(TAG_TELEFONO, new XCData("")));
                listaEntrega.Add(new XElement(TAG_TELEFONO_MOVIL, new XCData("")));
            }

            return listaEntrega;

        }
        private static List<XElement> generarFacturacion(int idCarrito)
        {
            List<XElement> listaEntrega = new List<XElement>();
            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            bool requiereFacturacion = GetFacturacion(idCarrito);

            if (requiereFacturacion)
            {
                DLLGestionVenta.Models.FACTURACION_CARRITO facturacion = objDatos.ObtenerFacturacionCarrito(idCarrito);

                listaEntrega.Add(new XElement(TAG_NOMBRE, new XCData(facturacion.Nombre)));
                listaEntrega.Add(new XElement(TAG_RFC, new XCData(facturacion.Rfc)));
                listaEntrega.Add(new XElement(TAG_DIRECCION, new XCData(facturacion.Direccion)));
                listaEntrega.Add(new XElement(TAG_NUMERO_INTERIOR, new XCData(facturacion.NoInterior)));
                listaEntrega.Add(new XElement(TAG_NUMERO_EXTERIOR, new XCData(facturacion.NoExterior)));
                listaEntrega.Add(new XElement(TAG_ESTADO, new XCData(facturacion.Estado)));
                listaEntrega.Add(new XElement(TAG_CIUDAD, new XCData(facturacion.Ciudad)));
                listaEntrega.Add(new XElement(TAG_COLONIA, new XCData(facturacion.Colonia)));
                listaEntrega.Add(new XElement(TAG_PAIS, new XCData("")));
                listaEntrega.Add(new XElement(TAG_CODIGO_POSTAL, new XCData(facturacion.CodPostal)));
                listaEntrega.Add(new XElement(TAG_TELEFONO, new XCData("")));
                listaEntrega.Add(new XElement(TAG_TELEFONO_MOVIL, new XCData("")));
            }

            return listaEntrega;
        }
        private static string GetReferencia(string IdArticulo)
        {
            string referencia = string.Empty;
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT CodigoAlfa FROM ARTICULOS WHERE IdArticulo = " + IdArticulo;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                referencia = myCommand.ExecuteScalar().ToString();

                return referencia;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool GetFacturacion(int idCarrito)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            bool requiereFacturacion;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT RequiereFacturacion FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return false;
            }
            else
            {
                requiereFacturacion = Convert.ToBoolean(myCommand.ExecuteScalar());
                myConnection.Close();

                return requiereFacturacion;
            }
        }

        private void InsertarLogAveHermes(int idCarrito, string xmlEnvio, string xmlRespuesta, int resultado) {

            try
            {
                ClsCapaDatos capaDatos = new ClsCapaDatos();

                capaDatos.InsertarLogCarrito(idCarrito, xmlEnvio, xmlRespuesta,resultado);
            }
            catch (Exception ex)
            {    
                throw ex;
            }

        }
    }
}