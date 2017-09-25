using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;

namespace AVE.CLS
{
    public class ClsAlmacenPiagui
    {

        public string obtenerAlmacen(string usuario, string password)
        {
            string resultado = "";

            try
            {
                using (ServicioAlmacenCentralPiagui.wsAlmacenCentral servicioWeb = new ServicioAlmacenCentralPiagui.wsAlmacenCentral())
                {
                    System.Net.CookieContainer cookie = new System.Net.CookieContainer();
                    servicioWeb.CookieContainer = cookie;

                    //Conectamos con el servicio web
                    if (servicioWeb.Login(usuario, password) == true)
                    {
                        //Obtenemos el almacén
                        XmlNode xml = servicioWeb.getInventario("");
                        xml = xml.ChildNodes[0].ChildNodes[0];
                        resultado = xml["Almacen"].InnerText;
                    }
                    else
                    {
                        resultado = "";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = "";
            }
            return resultado;
        }

        public List<string> obtenerExistencias(string usuario, string password, string material)
        {
            List<string> resultado = new List<string>();
            
            try
            {
                using (ServicioAlmacenCentralPiagui.wsAlmacenCentral servicioWeb = new ServicioAlmacenCentralPiagui.wsAlmacenCentral())
                {
                    System.Net.CookieContainer cookie = new System.Net.CookieContainer();
                    servicioWeb.CookieContainer = cookie;

                    //Conectamos con el servicio web
                    if (servicioWeb.Login(usuario, password) == true)
                    {
                        if (!string.IsNullOrEmpty(material))
                        {
                            //Obtenemos el inventario del artículo recibido
                            XmlNode xml = servicioWeb.getInventario(material);
                            
                            //Obtenemos las tallas
                            xml = xml.ChildNodes[0].ChildNodes[0].ChildNodes[3];
                            //Recorremos las tallas
                            int i = 0;
                            foreach (XmlNode xmlTalla in xml.ChildNodes)
                            {
                                //Aumentamos el tamaño del array
                                //Array.Resize(ref resultado, resultado.Length + 2);
                                //Guardamos la talla y la cantidad
                                resultado.Add(xmlTalla["Codigo"].InnerText + "#" + xmlTalla["Cantidad"].InnerText);
                                i++;
                            }
                        }
                        else
                        {
                            resultado = null;

                        }
                    }
                    else
                    {
                        resultado = null;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = null;
            }

            return resultado;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="noTraspaso"></param>
        /// <param name="centro"></param>
        /// <param name="almacen"></param>
        /// <param name="tienda"></param>
        /// <param name="material"></param>
        /// <param name="talla"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public string[] crearPedido(string usuario, string password, string noTraspaso, string centro, string almacen, string tienda, string material, string talla, int cantidad)
        {
            string[] resultado = new string[4];

            try
            {
                using (ServicioAlmacenCentralPiagui.wsAlmacenCentral servicioWeb = new ServicioAlmacenCentralPiagui.wsAlmacenCentral())
                {
                    System.Net.CookieContainer cookie = new System.Net.CookieContainer();
                    servicioWeb.CookieContainer = cookie;

                    //Conectamos con el servicio web
                    if (servicioWeb.Login(usuario, password) == true)
                    {
                        if (!string.IsNullOrEmpty(noTraspaso) & !string.IsNullOrEmpty(centro) & !string.IsNullOrEmpty(almacen) & !string.IsNullOrEmpty(tienda) & !string.IsNullOrEmpty(material) & !string.IsNullOrEmpty(talla) & cantidad > 0)
                        {
                            //Creamos el pedido
                            XmlNode xml = servicioWeb.creaPedidoAC(noTraspaso, centro, almacen, tienda, material, talla, cantidad);
                            //Devolvemos el resultado
                            resultado[0] = xml["Resultado"]["Status"].InnerText;
                            //Status
                            resultado[1] = xml["Resultado"]["Mensajes"]["Mensaje"]["Descripcion"].InnerText;
                            //Descripción
                            resultado[2] = xml["Resultado"]["Pedido"].InnerText;
                            //Pedido
                            resultado[3] = xml["Resultado"]["Entrega"].InnerText;
                            //Entrega
                        }
                        else
                        {
                            resultado[0] = "NO";
                            //Status
                            resultado[1] = "Algún parámetro no es correcto o está vacío";
                            //Descripción
                            resultado[2] = "";
                            //Pedido
                            resultado[3] = "";
                            //Entrega
                        }
                    }
                    else
                    {
                        resultado[0] = "NO";
                        //Status
                        resultado[1] = "No se ha podido conectar con el servicio web";
                        //Descripción
                        resultado[2] = "";
                        //Pedido
                        resultado[3] = "";
                        //Entrega
                    }
                }
            }
            catch (Exception ex)
            {
                resultado[0] = "NO";
                //Status
                resultado[1] = "Excepción: " + ex.Message;
                //Descripción
                resultado[2] = "";
                //Pedido
                resultado[3] = "";
                //Entrega
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el codigoAlfa del articulo partiendo de su idArticulo
        /// </summary>
        /// <param name="referencia">Referencia del articulo</param>
        /// <returns>Referencia del articulo</returns>
        public string GetReferencia(string IdArticulo)
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

                return referencia ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}