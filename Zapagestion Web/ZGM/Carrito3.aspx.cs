using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using DLLGestionVenta;
using DLLGestionVenta.Models;
using CapaDatos;

namespace AVE
{
    public partial class Carrito3 : System.Web.UI.Page
    {

        #region "Heredado del antiguo carrito"

        private void CalculoPromocion_Carrito()
        {
            ProcesarVenta objVenta;
            if (Session["IdCarrito"] != null)
            {
                objVenta = new ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.GetObjCarritoPromocion((Int64)Session[szVarIdCarrito], AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion);
            }
        }

        public string ObtenerURL(string idTemporada, string idProveedor, string ModeloProveedor)
        {
            string rutaLocal;
            string rutaVision = String.Empty;
            // Construimos la ruta en Local
            rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"] + "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";

            if (idTemporada.Length < 2) { idTemporada += "0" + idTemporada; }

            if (idProveedor.Length < 3) { idProveedor = string.Concat(System.Collections.ArrayList.Repeat("0", 3 - idProveedor.Length).ToArray()) + idProveedor.ToString(); }
            // Construimos la ruta en Local
            rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"] + "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";


            if (rutaLocal.IndexOf("http://") == 0)
            {
                if (Comun.RemoteFileExists(rutaLocal, 1000))
                {
                    return rutaLocal;
                }
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }

            }
            else
                if (System.IO.File.Exists(Server.MapPath(rutaLocal)))
                    return rutaLocal;
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }

        }

        /// <summary>
        /// Devuelve un numero formateado segun la norma es-ES
        /// </summary>
        /// <param name="sNumero"></param>
        /// <returns></returns>
        public string FormateaNumero(string sNumero)
        {
            string sVd = "0.00";
            CultureInfo info = CultureInfo.GetCultureInfo("es-MX");
            if (sNumero != "")
            {
                sVd = (Convert.ToDouble(sNumero)).ToString("c", info).Replace("€", "");
            }
            return sVd;
        }

        #endregion

        private class SaldoTarjeta
        {
            public double Puntos9 { get; set; }
        }

        // Variables ViewState
        private const string szVarIdCliente = "IdCliente";
        private const string szVarIdCarrito = "IdCarrito";
        private const string szVarClienteNine = "ClienteNine";
        private const string szVarClienteZapa = "ClienteZapa";

        ProcesarVenta objVenta;
        Int64 UniqueId = 0;

        #region "Métodos Privados"

        private CLIENTE9 EstablecerCliente(CLIENTE9 cliente)
        {

            Session[szVarClienteZapa] = cliente;
            // Comprobar si es Cliente 9;

            // TODO: Mostrarlo en pantalla

            return cliente;
        }

        private void MostrarListaCliente(List<CLIENTE9> clientes)
        {
            // TODO Mostrar el combo de clientes
            //LstClientes.DataTextField = "Cliente";
            //LstClientes.DataValueField = "ID_Cliente";
            //LstClientes.DataSource = objclient;
            //LstClientes.DataBind();

        }

        private void MostrarMensaje(string sMensaje, bool bError)
        {
            lblMensaje.Text = sMensaje;
            pnlMensaje.CssClass = (bError ? "ui-state-highlight" : "ui-state-error");
            pnlMensaje.Visible = true;
        }

        private string ConsultaWsBeneficios(string sTarjeta, int idCliente)
        {
            string sRet = string.Empty;

            ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
            ws.cls_Cliente9.ConsultaBeneficios objConsultaBeneficios = new ws.cls_Cliente9.ConsultaBeneficios();
            objConsultaBeneficios.idTargeta = sTarjeta;
            objConsultaBeneficios.idTienda = AVE.Contexto.IdTienda;
            objConsultaBeneficios.idTerminal = AVE.Contexto.IdTerminal;

            String ret = c9.InvokeWS_ConsultaBeneficios(ref objConsultaBeneficios);
            if (!String.IsNullOrEmpty(ret))
            {
                sRet = ret.Replace("Puntos Net", "Cliente 9");
            }

            return sRet;
        }
        private bool BuscarClienteNine(string sTarjeta)
        {
            bool bRet = true;

            try
            {

                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                ws.cls_Cliente9.ConsultaBeneficios objConsultaBeneficios = new ws.cls_Cliente9.ConsultaBeneficios();
                objConsultaBeneficios.idTargeta = sTarjeta;
                objConsultaBeneficios.idTienda = AVE.Contexto.IdTienda;
                objConsultaBeneficios.idTerminal = AVE.Contexto.IdTerminal;
                int pares = 0;
                int Bolsas = 0;

                String ret = c9.InvokeWS_ConsultaBeneficios(ref objConsultaBeneficios);
                if (!String.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("Puntos Net", "Cliente 9");
                }
                else
                {
                    if (Session[szVarClienteZapa] != null)
                    {
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagadoZapa", "alert('La tarjeta no esta homolagada en Zapagestion.');", true);
                        // Hay que saber si la tarjeta es válida pero no está homologada. ¿ Formato del numero tarjeta ?
                        throw new Exception("No encontrado.");
                    }

                    // Si hay un cliente y lo encontró a traves de la tarjeta
                    CLIENTE9 cliente = (CLIENTE9)Session[szVarClienteZapa];

                    if (c9.GetblnHomologadoC9(sTarjeta, cliente.Id_Cliente))
                    {
                        CLIENTE9 objCliente;
                        if (Session["ClienteNine"] == null)
                        {
                            #region "crear nuevo cliente9 en Session[ClienteNine]"
                            objCliente = new CLIENTE9();
                            objCliente.Aniversario = objConsultaBeneficios.aniversario;
                            objCliente.Fecha = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                            objCliente.Cumpleaños = objConsultaBeneficios.cumpleanos;
                            objCliente.Email = objConsultaBeneficios.mail;
                            objCliente.Cliente = objConsultaBeneficios.nombre;
                            objCliente.CandidataShoeLover = (objConsultaBeneficios.shoelover == "true" ? "SI" : "NO");
                            objCliente.Empleado_cliente = null;
                            objCliente.NumTarjetaNew = String.Empty;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosRedimidos = 0;
                            objCliente.SaldoPuntosAnt = Convert.ToDouble(objConsultaBeneficios.puntos);
                            objCliente.SaldoPuntosAct = 0;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosObtenidos = 0;
                            objCliente.ParesAcumuladosAnt = Convert.ToDouble(objConsultaBeneficios.paresAcumulados);
                            objCliente.NumTarjeta = objConsultaBeneficios.idTargeta;
                            objCliente.dblSaldoBolsa5 = objConsultaBeneficios.dblSaldoBolsa5;
                            objCliente.dblSaldoPares9 = objConsultaBeneficios.dblSaldoPares9;
                            objCliente.NumConfirmaBolsa5 = Int16.Parse(objConsultaBeneficios.bolsasAcumuladas);
                            objCliente.NivelActual = objConsultaBeneficios.strNivelActual;
                            objCliente.BolsasAcumuladasAnt = double.Parse(objConsultaBeneficios.bolsasAcumuladas);
                            objCliente.BenC9 = "1";
                            objCliente.BolsaPagada = 0;
                            objCliente.ParPagado = 0;
                            objCliente.PuntosPagados = 0;
                            Session["ClienteNine"] = objCliente;
                            #endregion
                        }
                        else
                        {
                            objCliente = (CLIENTE9)Session["ClienteNine"];
                        }

                        objConsultaBeneficios.paresAcumulados = (Int16.Parse(objConsultaBeneficios.paresAcumulados) - (objCliente.ParPagado * 8)).ToString();
                        objConsultaBeneficios.bolsasAcumuladas = (Int16.Parse(objConsultaBeneficios.bolsasAcumuladas) - (objCliente.BolsaPagada * 4)).ToString();
                        if (Int16.Parse(objConsultaBeneficios.paresAcumulados) > 0) { pares = (Int16.Parse(objConsultaBeneficios.paresAcumulados) / 8); }
                        if (Int16.Parse(objConsultaBeneficios.bolsasAcumuladas) > 0) { Bolsas = (Int16.Parse(objConsultaBeneficios.bolsasAcumuladas) / 4); }

                        
                        //Label10.Text = (double.Parse(cb.puntos) - objCliente.PuntosPagados).ToString();
                        //Label11.Text = (Int16.Parse(cb.paresAcumulados) > 0 ? cb.promedioPar : "0");
                        //Label12.Text = (Int16.Parse(cb.bolsasAcumuladas) > 0 ? cb.promedioBolsa : "0");
                        //Label13.Text = cb.paresAcumulados + "(" + pares.ToString() + ")";
                        //Label14.Text = cb.bolsasAcumuladas + "(" + Bolsas.ToString() + ")";

                        //VALIDAMOS PAR9 
                        //if (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())))
                        //{
                        //    RadioButton3.Enabled = true;
                        //    objCliente.ParValido = true;
                        //}
                        //else
                        //{
                        //    RadioButton3.Enabled = false;
                        //    objCliente.ParValido = false;
                        //}

                        ////VALIDAMOS BOLSA5
                        //if (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())))
                        //{
                        //    objCliente.BolsaValido = true;
                        //    RadioButton4.Enabled = true;
                        //}
                        //else
                        //{
                        //    RadioButton4.Enabled = false;
                        //    objCliente.BolsaValido = false;
                        //}

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagado", "alert('La Tarjeta introducida no esta homologada.');", true);
                        // Cliente
                    }
                }
                //nomcliente.Text = TarjetaCliente.Text.ToString();
                //BCliente_Click(sender, e);
                //if (Session["ClienteNine"] != null && nomcliente.Text.ToString().Length > 0) { ((CLIENTE9)Session["ClienteNine"]).Id_Cliente = int.Parse(nomcliente.Text.ToString()); }
            }
            catch (Exception ex)
            {
                bRet = false;
                MostrarMensaje(ex.Message, true);
            }

            return bRet;
        }

        private bool BuscarCliente(string sTexto)
        {
            bool bRet = true;

            try
            {
                Int64 idCarrito = Int64.Parse(Session[szVarIdCarrito].ToString()); // Que cosa más sucia por dios
                objVenta = new ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                List<CLIENTE9> listaClientesZAPA = objVenta.GetCliente(sTexto, Contexto.FechaSesion, idCarrito, Contexto.IdTienda);
                if (listaClientesZAPA.Count == 0)
                {
                    // No encontrado
                    MostrarMensaje("No se han encontrado coincidencias.", true);
                    bRet = false;
                }
                else
                {
                    if (listaClientesZAPA.Count == 1)
                    {
                        //CalculoPromocion_Carrito(); // <- veremos a ver con esto que hacemos...
                        CLIENTE9 cliente = EstablecerCliente(listaClientesZAPA[0]);
                        ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                        ws.cls_Cliente9.ConsultaBeneficios objConsultaBeneficios = new ws.cls_Cliente9.ConsultaBeneficios();
                        objConsultaBeneficios.idTargeta = sTexto;
                        objConsultaBeneficios.idTienda = AVE.Contexto.IdTienda;
                        objConsultaBeneficios.idTerminal = AVE.Contexto.IdTerminal;
                        int pares = 0;
                        int Bolsas = 0;

                        //if (c9.GetblnHomologadoC9(sTarjeta, cliente.Id_Cliente))

                        //if ConsultaWsBeneficios(

                    }
                    else
                    {
                        MostrarListaCliente(listaClientesZAPA);
                        // Aqui se devuelve false porque todavía no se encontró un cliente.
                        // solo se asigna al seleccionarlo de la lista.
                        bRet = false;
                    }
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                MostrarMensaje(ex.Message, true);
            }
            return bRet;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            { 
                // Postback

            }
            else
            {
                // Inicializar
                Session[szVarIdCarrito] = 190;
                optTarjetaMIT.Checked = true;
            }
        }

        protected void cmdBuscarCliente_Click(object sender, EventArgs e)
        {
            this.BuscarCliente( txtBuscarCliente.Text );
        }

    }
}