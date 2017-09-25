using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using CapaDatos;
using System.Data.SqlClient;

namespace AVE.CLS
{
    public class HermesXMLDocument
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
        private const string TAG_CANTIDAD = "CANTIDAD";

        private const string TAG_NUMERO_PEDIDO_ORIGINAL = "NUMERO_PEDIDO_ORIGINAL";
        private const string TAG_FECHA_PEDIDO = "FECHA_PEDIDO";
        private const string TAG_TIPO = "TIPO";
        private const string THINKRETAL = "THINKRETAL";
        private const string TAG_TIPO_ENVIO = "TIPO_ENVIO";
        private const string TAG_ID_TIENDA = "ID_TIENDA";

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
        #endregion

        public static XDocument GenerarXML(DataView dvVenta, DataSet dsFormasPago, int idCarrito, string idTienda, string idtiendaEnvio)
        {
            ///OBTENER DATOS CON GESTION VENTAS
            ///
            XDocument solicitud = null;
            List<XElement> elmentosSolicitud = new List<XElement>();

            elmentosSolicitud.AddRange(generarCabecera(idCarrito, idTienda, idtiendaEnvio));
            if (string.IsNullOrEmpty(idtiendaEnvio))
                elmentosSolicitud.AddRange(generarEntrega(idCarrito));
            elmentosSolicitud.Add(new XElement(TAG_FORMA_PAGO, null));
            elmentosSolicitud.Add(generarProductos(dvVenta));
            elmentosSolicitud.Add(generarDescuentos(dvVenta));
            elmentosSolicitud.Add(generarFormasPago(dsFormasPago));

            XElement solictud = new XElement(TAG_SOLICITUD, elmentosSolicitud);

            XElement Solicitudes = new XElement(TAG_SOLICITUDES, solictud);

            solicitud = new XDocument(Solicitudes);

            return solicitud;
        }

        private static List<XElement> generarCabecera(int idCarrito, string idTienda, string idTiendaEnvio)
        {
            List<XElement> cabecera = new List<XElement>();

            cabecera.Add(new XElement(TAG_NUMERO_PEDIDO_ORIGINAL, idCarrito));
            cabecera.Add(new XElement(TAG_FECHA_PEDIDO, DateTime.Now.ToString()));
            cabecera.Add(new XElement(TAG_TIPO, (THINKRETAL)));
            if (!string.IsNullOrEmpty(idTiendaEnvio))
            {
                cabecera.Add(new XElement(TAG_TIPO_ENVIO, "TIENDA"));
                cabecera.Add(new XElement(TAG_ID_TIENDA, idTiendaEnvio));
            }
            else
            {
                cabecera.Add(new XElement(TAG_TIPO_ENVIO, "DOMICILIO"));
                cabecera.Add(new XElement(TAG_ID_TIENDA, null));
            }


            return cabecera;
        }


        private static XElement generarProductos(DataView dvVenta)
        {
            List<XElement> productos = new List<XElement>();
            string referencia = string.Empty;
            foreach (DataRow row in dvVenta.Table.Rows)
            {
                referencia = GetReferencia(row["idArticulo"].ToString());
                productos.Add(generarProducto(referencia, referencia, row["Nombre_Talla"].ToString(), Convert.ToDecimal(row["PVPACT"].ToString()), 1));
            }
            return new XElement(TAG_PRODUCTOS, productos.ToArray());
        }



        private static XElement generarProducto(string codigoAlfa, string referencia, string talla, decimal importe, int cantidad)
        {
            return new XElement(TAG_PRODUCTO,
                 new XElement(TAG_CODIGO_ALFA, new XCData(codigoAlfa)),
                 new XElement(TAG_REFERENCIA, new XCData(referencia)),
                 new XElement(TAG_TALLA, new XCData(talla)),
                 new XElement(TAG_IMPORTE_PRODUCTO, new XCData(importe.ToString())),
                 new XElement(TAG_CANTIDAD, new XCData(cantidad.ToString())));

        }

        private static XElement generarFormasPago(DataSet dsFormasPago)
        {
            List<XElement> formasPago = new List<XElement>();

            DataTable dt = dsFormasPago.Tables[0];

            foreach (DataRow row in dt.Rows)
            {

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
            return new XElement(TAG_DESCUENTO,
                 new XElement(TAG_DESCRIPCION_DESCUENTO, new XCData(descripcion)),
                 new XElement(TAG_IMPORTE_DESCUENTO, new XCData(importe.ToString())));
        }

        private static List<XElement> generarEntrega(int idCarrito)
        {
            string[] apellidos = null;
            string apellido1, apellido2;

            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCarrito(idCarrito);

            List<XElement> listaEntrega = new List<XElement>();

            //separamos los apellidos para intoducir apellido1 y apellido2 

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
            listaEntrega.Add(new XElement(TAG_POBLACION_ENVIO, new XCData(entrega.Ciudad)));
            listaEntrega.Add(new XElement(TAG_PROVINCIA_ENVIO, new XCData(entrega.Colonia)));
            listaEntrega.Add(new XElement(TAG_CODIGO_POSTAL_ENVIO, new XCData(entrega.CodPostal)));
            listaEntrega.Add(new XElement(TAG_TELEFONO_ENVIO, new XCData(entrega.TelfFijo)));
            listaEntrega.Add(new XElement(TAG_TELEFONO_MOVIL_ENVIO, new XCData(entrega.TelfMovil)));
            listaEntrega.Add(new XElement(TAG_EMAIL_ENVIO, new XCData(entrega.Email)));
            listaEntrega.Add(new XElement(TAG_OBSERVACIONES, new XCData(entrega.Referencia)));

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
    }
}