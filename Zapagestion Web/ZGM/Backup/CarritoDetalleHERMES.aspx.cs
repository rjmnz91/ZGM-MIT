using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.Services;
using System.Xml.Serialization;
using DLLGestionVenta.Models;
using System.Web.UI.HtmlControls;
using CapaDatos;
using System.Xml.Linq;
using AVE.CLS;
using DLLGestionVenta.BL;


namespace AVE
{
    public partial class CarritoDetalleHERMES : CLS.Cls_Session
    {

        private const string SESSION_ESTADO_PROCESO_INI = "INI";
        private const string SESSION_ESTADO_PROCESO_PAGO = "PAGO";
        private const string SESSION_ESTADO_PROCESO_ENTREGA = "ENTREGA";
        private const string SESSION_ESTADO_PROCESO_FIN = "FIN";
        


        // MJM 27/05/2014 LOG
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //

        DLLGestionVenta.ProcesarVenta objVenta;
        DataSet DsPromo;
        bool pagado;
        private int? IdCarritoQueryString
        {
            get
            {
                int idCarrito;

                if (Request.QueryString["IdCarrito"] != null && int.TryParse(Request.QueryString["IdCarrito"], out idCarrito))
                    return idCarrito;
                else
                    return null;
            }
        }

        private string UrlHermesQueryString
        {
            get
            {
                if (Request.QueryString["UrlHermes"] != null && !string.IsNullOrEmpty(Request.QueryString["IdCarrito"]))
                    return Request.QueryString["UrlHermes"];
                else
                    return string.Empty;
            }
        }

        private string SessionStateQueryString
        {
            get
            {
                if (Request.QueryString["SessionState"] != null && !string.IsNullOrEmpty(Request.QueryString["IdCarrito"]))
                    return Request.QueryString["SessionState"];
                else
                    return string.Empty;
            }
        }

        private string UrlHermesSession
        {

            get
            {
                if (Session["UrlHermes"] != null && !string.IsNullOrEmpty(Session["UrlHermes"].ToString()))


                    return Session["UrlHermes"].ToString();
                else
                    return string.Empty; ;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))

                    Session["UrlHermes"] = value;
                else
                    Session["UrlHermes"] = null;
            }
        }

        private string EstadoProcesoSession
        {

            get
            {

                if (ViewState["EstadoProceso"] != null && !string.IsNullOrEmpty(ViewState["EstadoProceso"].ToString()))
                    return ViewState["EstadoProceso"].ToString();
                else
                    return string.Empty; ;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))

                    ViewState["EstadoProceso"] = value;
                else
                    ViewState["EstadoProceso"] = null;
            }
        }

        private int? IdCarritoSession
        {
            get
            {
                int idCarrito;

                if (Session["IdCarrito"] != null && int.TryParse(Session["IdCarrito"].ToString(), out idCarrito))
                    return idCarrito;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    Session["IdCarrito"] = value.Value;
                else
                    Session["IdCarrito"] = null;
            }
        }


        private string EmailSession
        {

            get
            {
                if (this.Session["email"] == null || (this.Session["email"] != null && string.IsNullOrEmpty(this.Session["email"].ToString())))
                    return null;
                else
                    return this.Session["email"].ToString();

            }
            set
            {
                this.Session["email"] = value;
            }
        }
        private decimal GastosEnvioAppSettings
        {
            get
            {
                decimal gastosEnvio;
                if (Session["IdCarrito"] != null && decimal.TryParse(System.Configuration.ConfigurationManager.AppSettings["GastosEnvioHERMES"].ToString(), out gastosEnvio))
                    return gastosEnvio;
                else
                    return 0;


            }
        }

        private string UrlHermesAppSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString() != null)
                    return System.Configuration.ConfigurationManager.AppSettings["UrlHermes"].ToString();
                else
                    return string.Empty;
            }
        }
        private string UrlImagenesHermesAppSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["UrlImagenesHermes"].ToString() != null)
                    return System.Configuration.ConfigurationManager.AppSettings["UrlImagenesHermes"].ToString();
                else
                    return string.Empty;
            }
        }
        private string UserThinkRetailAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserThinkRetail"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["UserThinkRetail"].ToString();
                else
                    return "ERROR";
            }
        }

        private string PassThinkRetailAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["PassThinkRetail"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["PassThinkRetail"].ToString();
                else
                    return "ERROR";
            }
        }

        private string IdAlmacenCentralAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IdAlmacenCentral"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["IdAlmacenCentral"].ToString();
                else
                    return "ERROR";
            }
        }

        #region "Modificaciones Marcos"
        private void CargaDatosFacturacion()
        {
            int idCliente;
            try
            {

                // refresh
                cboOUEstado.DataBind();
                cboOUEstadoF.DataBind();
                cboOUEstadoFacturacion.DataBind();

                ClsCapaDatos objDatos = new ClsCapaDatos();
                objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                if (IdCarritoSession != null)
                    idCliente = GetClienteCarrito(IdCarritoSession);
                else
                    idCliente = GetClienteCarrito(IdCarritoQueryString);

                if (idCliente != 0)
                {
                   

                    GetClienteCarritoFacturacion(idCliente);
                    if (txtOUNombreFacturacion.Text == "")
                    {
                        DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCliente(idCliente);
                        if (entrega != null)//Si tiene datos en ave_carrito_entregas cargamos de ahi, sino, cargamos de n_clientes_generales
                        {
                            
                            txtOUNombreFacturacion.Text = entrega.Nombre;
                            txtOUDireccionFacturacion.Text = entrega.Direccion;
                            txtOUColoniaFacturacion.Text = entrega.Colonia;
                            txtOUNoInteriorFacturacion.Text = entrega.NoInterior;
                            txtOUNoExteriorFacturacion.Text = entrega.NoExterior;
                            txtOUCodigoPostalFacturacion.Text = entrega.CodPostal;
                            txtOUCiudadFacturacion.Text = entrega.Ciudad;
                            cboOUEstadoFacturacion.SelectedValue = GetIdProvincia(entrega.Estado);
                        }
                        else
                        {
                            GetClienteGeneral(idCliente);
                        }
                    
                    
                    
                    }
                }


            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        
        
        
        }
        private void CargaEntrega()
        {
            int idCliente;
            try
            {

                // refresh
                cboOUEstado.DataBind();
                cboOUEstadoF.DataBind();
                cboOUEstadoFacturacion.DataBind();

                ClsCapaDatos objDatos = new ClsCapaDatos();
                objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                if (IdCarritoSession != null)
                    idCliente = GetClienteCarrito(IdCarritoSession);
                else
                    idCliente = GetClienteCarrito(IdCarritoQueryString);

                if (idCliente != 0)
                {
                    DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCliente(idCliente);

                    if (entrega != null)//Si tiene datos en ave_carrito_entregas cargamos de ahi, sino, cargamos de n_clientes_generales
                    {
                        txtOUNombre.Text = entrega.Nombre;
                        txtOUApellidos.Text = entrega.Apellidos;
                        txtOUemail.Text = entrega.Email;
                        txtOUDireccion.Text = entrega.Direccion;
                        txtOUNoInterior.Text = entrega.NoInterior;
                        txtOUNoExterior.Text = entrega.NoExterior;
                        if (entrega.Estado != null)
                            cboOUEstado.SelectedValue = GetIdProvincia(entrega.Estado);
                        txtOUCiudad.Text = entrega.Ciudad;
                        txtOUColonia.Text = entrega.Colonia;
                        txtOUCodigoPostal.Text = entrega.CodPostal;
                        txtOUTelfCelular.Text = entrega.TelfMovil;
                        txtOUTelfFijo.Text = entrega.TelfFijo;
                        txtOUReferenciaLlegada.Text = entrega.Referencia;

                        txtOUNombreF.Text = entrega.Nombre;
                        txtOUApellidosF.Text = entrega.Apellidos;
                        txtOUemailF.Text = entrega.Email;
                        txtOUDireccionF.Text = entrega.Direccion;
                        txtOUNoInteriorF.Text = entrega.NoInterior;
                        txtOUNoExteriorF.Text = entrega.NoExterior;
                        if (entrega.Estado != null)
                            cboOUEstadoF.SelectedValue = GetIdProvincia(entrega.Estado);
                        txtOUCiudadF.Text = entrega.Ciudad;
                        txtOUColoniaF.Text = entrega.Colonia;
                        txtOUCodigoPostalF.Text = entrega.CodPostal;
                        txtOUTelfCelularF.Text = entrega.TelfMovil;
                        txtOUTelfFijoF.Text = entrega.TelfFijo;
                        txtOUReferenciaLlegada.Text = entrega.Referencia;
                    }
                    else 
                    { 
                        //Cargamos de n_clientes_generales
                        GetClienteGeneral(idCliente);
                    }

                    GetClienteCarritoFacturacion(idCliente);

                }


            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void ReseteaEntrega()
        {

            txtOUNombre.Text = "";
            txtOUApellidos.Text = "";
            txtOUemail.Text = "";
            txtOUDireccion.Text = "";
            txtOUNoInterior.Text = "";
            txtOUNoExterior.Text = "";
            cboOUEstado.SelectedIndex = 0;
            txtOUCiudad.Text = "";
            txtOUColonia.Text = "";
            txtOUCodigoPostal.Text = "";
            txtOUTelfCelular.Text = "";
            txtOUTelfFijo.Text = "";
            txtOUReferenciaLlegada.Text = "";
        }

        private void ReseteaEntregaFacturacion()
        {
            txtOUNombreF.Text = "";
            txtOUApellidosF.Text = "";
            txtOUemailF.Text = "";
            txtOUDireccionF.Text = "";
            txtOUNoInteriorF.Text = "";
            txtOUNoExteriorF.Text = "";
            cboOUEstadoF.SelectedIndex = 0;
            txtOUCiudadF.Text = "";
            txtOUColoniaF.Text = "";
            txtOUCodigoPostalF.Text = "";
            txtOUTelfCelularF.Text = "";
            txtOUTelfFijoF.Text = "";
            txtOUReferenciaLlegadaF.Text = "";

            txtOUNombreFacturacion.Text = "";
            txtOURfcFacturacion.Text = "";
            txtOUDireccionFacturacion.Text = "";
            txtOUNoInteriorFacturacion.Text = "";
            txtOUNoExteriorFacturacion.Text = "";
            cboOUEstadoFacturacion.SelectedIndex = 0;
            txtOUCiudadFacturacion.Text = "";
            txtOUColoniaFacturacion.Text = "";
            txtOUCodigoPostalFacturacion.Text = "";
           //acl. lo comento ya que esta comentado en el aspx
          //  txtOUTelfFijoFacturacion.Text = "";
           // txtOUTelfCelularFacturacion.Text = "";


        }

        private void InicializarVariablesSesion()
        {
            Session["ClienteNine"] = null;
            Session["objCliente"] = null;
            Session["IdCarrito"] = null;
            Session["objCliente"] = null;
            Session["FVENTA"] = null;
            Session["IdCarritoPago"] = null;
        }
        private void cargaClienteNineSesion(string sTarjeta) 
        {
            try
            {

                RadioButtonlTipoPago.Items[0].Selected = false;
                RadioButtonlTipoPago.Items[1].Selected = false;
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                //c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
                ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
                cb.idTargeta = sTarjeta;
                cb.idTienda = AVE.Contexto.IdTienda;
                cb.idTerminal = AVE.Contexto.IdTerminal;
                int pares = 0;
                int Bolsas = 0;

                //nomcliente.Text = TarjetaCliente.Text.ToString();
               // nomcliente.Text = sTarjeta;

              
                CalculoPromocion_CarritoCliente();
                CargaCarrito();
                PoneTotales();
                CargarPagosCarritos();


                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (!Comun.CheckURLWs(url, 10000))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                    return;
                }

                String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
                if (!String.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("Puntos Net", "Cliente 9");
                    Label10.Text = "0";
                    Label11.Text = "0";
                    Label12.Text = "0";
                    Label13.Text = "0(0)";
                    Label14.Text = "0(0)";
                    RadioButton3.Enabled = false;
                    RadioButton4.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", string.Format("alert('{0}');", ret), true);
                    return;
                }
                else
                {
                    if (nomcliente.Text.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagadoZapa", "alert('La tarjeta no esta homolagada en Zapagestion.');", true);
                        return;
                    }



                    if (c9.GetblnHomologadoC9(sTarjeta)) //, Int64.Parse(nomcliente.Text.ToString())))
                    {


                        CLIENTE9 objCliente;

                        if (Session["ClienteNine"] != null)
                        {
                            if (((CLIENTE9)Session["ClienteNine"]).NumTarjeta.ToString() != sTarjeta)
                            {
                                Session["ClienteNine"] = null;
                            }
                        }

                        if (Session["ClienteNine"] == null)
                        {
                            objCliente = new CLIENTE9();
                            objCliente.Aniversario = cb.aniversario;
                            objCliente.Fecha = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                            objCliente.Cumpleaños = cb.cumpleanos;
                            objCliente.Email = cb.mail;
                            objCliente.Cliente = cb.nombre;
                            objCliente.CandidataShoeLover = (cb.shoelover == "true" ? "SI" : "NO");
                            objCliente.Empleado_cliente = null;
                            objCliente.NumTarjetaNew = String.Empty;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosRedimidos = 0;
                            objCliente.SaldoPuntosAnt = Convert.ToDouble(cb.puntos);
                            objCliente.SaldoPuntosAct = 0;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosObtenidos = 0;
                            objCliente.ParesAcumuladosAnt = Convert.ToDouble(cb.paresAcumulados);
                            objCliente.NumTarjeta = cb.idTargeta;
                            objCliente.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
                            objCliente.dblSaldoPares9 = cb.dblSaldoPares9;
                            objCliente.NumConfirmaBolsa5 = Int16.Parse(cb.bolsasAcumuladas);
                            objCliente.NivelActual = cb.strNivelActual;
                            objCliente.BolsasAcumuladasAnt = double.Parse(cb.bolsasAcumuladas);
                            objCliente.BenC9 = "1";
                            objCliente.BolsaPagada = 0;
                            objCliente.ParPagado = 0;
                            objCliente.PuntosPagados = 0;
                            //ACL. NUEVO NIVEL C9
                            objCliente.CandidatoBasico = cb.strCandBasicoC9;
                            objCliente.CandidatoFirstShoeLover = cb.strCandFirstC9;
                            Session["ClienteNine"] = objCliente;
                            Session["objCliente"] = null;
                        }
                        else
                        {
                            objCliente = (CLIENTE9)Session["ClienteNine"];
                        }

                        cb.paresAcumulados = (Int16.Parse(cb.paresAcumulados) - (objCliente.ParPagado * 8)).ToString();
                        cb.bolsasAcumuladas = (Int16.Parse(cb.bolsasAcumuladas) - (objCliente.BolsaPagada * 4)).ToString();

                        if (Int16.Parse(cb.paresAcumulados) > 0) { pares = (Int16.Parse(cb.paresAcumulados) / 8); }
                        if (Int16.Parse(cb.bolsasAcumuladas) > 0) { Bolsas = (Int16.Parse(cb.bolsasAcumuladas) / 4); }

                        Label10.Text = (double.Parse(cb.puntos) - objCliente.PuntosPagados).ToString();
                        Label11.Text = (Int16.Parse(cb.paresAcumulados) > 0 ? cb.promedioPar : "0");
                        Label12.Text = (Int16.Parse(cb.bolsasAcumuladas) > 0 ? cb.promedioBolsa : "0");
                        Label13.Text = cb.paresAcumulados + "(" + pares.ToString() + ")";
                        Label14.Text = cb.bolsasAcumuladas + "(" + Bolsas.ToString() + ")";

                        //VALIDAMOS PAR9 
                        RadioButton3.Enabled = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString())));
                        objCliente.ParValido = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString())));

                        //VALIDAMOS BOLSA5
                        objCliente.BolsaValido = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString())));
                        RadioButton4.Enabled = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString())));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homologado", "alert('La Tarjeta introducida no esta homologada.');", true);
                        //cliente
                    }

                }
                //nomcliente.Text = TarjetaCliente.Text.ToString();
                //BCliente_Click(sender, e);
                if (Session["ClienteNine"] != null && nomcliente.Text.ToString().Length > 0) { ((CLIENTE9)Session["ClienteNine"]).Id_Cliente = int.Parse(hidIdCliente.Value.ToString()); }

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private void BuscarClienteNine(string sTarjeta)
        {
            
        
            try
            {

                RadioButtonlTipoPago.Items[0].Selected = false;
                RadioButtonlTipoPago.Items[1].Selected = false;
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                //c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
                ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
                cb.idTargeta = sTarjeta;
                cb.idTienda = AVE.Contexto.IdTienda;
                cb.idTerminal = AVE.Contexto.IdTerminal;
                int pares = 0;
                int Bolsas = 0;

                //nomcliente.Text = TarjetaCliente.Text.ToString();
                nomcliente.Text = sTarjeta;

                GetClienteActual(sTarjeta);

                CalculoPromocion_CarritoCliente();
                CargaCarrito();
                PoneTotales();
                CargarPagosCarritos();


                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (!Comun.CheckURLWs(url, 10000))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                    return;
                }

                String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
                if (!String.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("Puntos Net", "Cliente 9");
                    Label10.Text = "0";
                    Label11.Text = "0";
                    Label12.Text = "0";
                    Label13.Text = "0(0)";
                    Label14.Text = "0(0)";
                    RadioButton3.Enabled = false;
                    RadioButton4.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", string.Format("alert('{0}');", ret), true);
                    return;
                }
                else
                {
                    if (nomcliente.Text.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagadoZapa", "alert('La tarjeta no esta homolagada en Zapagestion.');", true);
                        return;
                    }



                    if (c9.GetblnHomologadoC9(sTarjeta)) //, Int64.Parse(nomcliente.Text.ToString())))
                    {


                        CLIENTE9 objCliente;

                        if (Session["ClienteNine"] != null)
                        {
                            if (((CLIENTE9)Session["ClienteNine"]).NumTarjeta.ToString() != sTarjeta)
                            {
                                Session["ClienteNine"] = null;
                            }
                        }

                        if (Session["ClienteNine"] == null)
                        {
                            objCliente = new CLIENTE9();
                            objCliente.Aniversario = cb.aniversario;
                            objCliente.Fecha = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                            objCliente.Cumpleaños = cb.cumpleanos;
                            objCliente.Email = cb.mail;
                            objCliente.Cliente = cb.nombre;
                            objCliente.CandidataShoeLover = (cb.shoelover == "true" ? "SI" : "NO");
                            objCliente.Empleado_cliente = null;
                            objCliente.NumTarjetaNew = String.Empty;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosRedimidos = 0;
                            objCliente.SaldoPuntosAnt = Convert.ToDouble(cb.puntos);
                            objCliente.SaldoPuntosAct = 0;
                            objCliente.ParesRedimidos = 0;
                            objCliente.PuntosObtenidos = 0;
                            objCliente.ParesAcumuladosAnt = Convert.ToDouble(cb.paresAcumulados);
                            objCliente.NumTarjeta = cb.idTargeta;
                            objCliente.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
                            objCliente.dblSaldoPares9 = cb.dblSaldoPares9;
                            objCliente.NumConfirmaBolsa5 = Int16.Parse(cb.bolsasAcumuladas);
                            objCliente.NivelActual = cb.strNivelActual;
                            objCliente.BolsasAcumuladasAnt = double.Parse(cb.bolsasAcumuladas);
                            objCliente.BenC9 = "1";
                            objCliente.BolsaPagada = 0;
                            objCliente.ParPagado = 0;
                            objCliente.PuntosPagados = 0;
                            //ACL. NUEVO NIVEL C9
                            objCliente.CandidatoBasico = cb.strCandBasicoC9;
                            objCliente.CandidatoFirstShoeLover = cb.strCandFirstC9;
                            Session["ClienteNine"] = objCliente;
                            Session["objCliente"] = null;
                        }
                        else
                        {
                            objCliente = (CLIENTE9)Session["ClienteNine"];
                        }

                        cb.paresAcumulados = (Int16.Parse(cb.paresAcumulados) - (objCliente.ParPagado * 8)).ToString();
                        cb.bolsasAcumuladas = (Int16.Parse(cb.bolsasAcumuladas) - (objCliente.BolsaPagada * 4)).ToString();

                        if (Int16.Parse(cb.paresAcumulados) > 0) { pares = (Int16.Parse(cb.paresAcumulados) / 8); }
                        if (Int16.Parse(cb.bolsasAcumuladas) > 0) { Bolsas = (Int16.Parse(cb.bolsasAcumuladas) / 4); }

                        Label10.Text = (double.Parse(cb.puntos) - objCliente.PuntosPagados).ToString();
                        Label11.Text = (Int16.Parse(cb.paresAcumulados) > 0 ? cb.promedioPar : "0");
                        Label12.Text = (Int16.Parse(cb.bolsasAcumuladas) > 0 ? cb.promedioBolsa : "0");
                        Label13.Text = cb.paresAcumulados + "(" + pares.ToString() + ")";
                        Label14.Text = cb.bolsasAcumuladas + "(" + Bolsas.ToString() + ")";

                        //VALIDAMOS PAR9 
                        RadioButton3.Enabled = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString())));
                        objCliente.ParValido = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString())));

                        //VALIDAMOS BOLSA5
                        objCliente.BolsaValido = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString())));
                        RadioButton4.Enabled = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString())));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homologado", "alert('La Tarjeta introducida no esta homologada.');", true);
                        //cliente
                    }

                }
                //nomcliente.Text = TarjetaCliente.Text.ToString();
                //BCliente_Click(sender, e);
                if (Session["ClienteNine"] != null && nomcliente.Text.ToString().Length > 0) { ((CLIENTE9)Session["ClienteNine"]).Id_Cliente = int.Parse(hidIdCliente.Value.ToString()); }

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }

        private void ValidarCargaCliente()
        {
            try
            {
                if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
                if (Session["TiendaCamper"].ToString() != "0") Session["ClienteNine"] = null;
                if (Session["ClienteNine"] != null)
                {
                    //nomcliente.Text = ((CLIENTE9)Session["ClienteNine"]).Cliente;
                    GetClientesCarrito();
                }
                else
                {
                    
                    GetClientesCarrito();
                    
                    //if (Session["objCliente"] != null)
                    //{
                    //    GetClienteActual((CLIENTE9)Session["objCliente"]);
                    //}
                    //else
                    //{
                    //    GetClientesCarrito();
                    //}
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        // Devuelve el codigo alfa, se llama desde el grid del carrito.
        protected string CodigoAlfaDesdeIdArticulo(object idArticulo)
        {
            string sRet = string.Empty;

            try
            {
                dsCodigoAlfaArticulo.SelectParameters["IdArticulo"].DefaultValue = idArticulo.ToString();
                DataTable dt = ((DataView)dsCodigoAlfaArticulo.Select(DataSourceSelectArguments.Empty)).ToTable();

                if (dt.Rows.Count > 0)
                {
                    sRet = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return sRet;
        }


        #endregion

        private int CheckArticulosCarrito(string idCarrito)
        {
            int numArticulos = 0;
            try
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objVenta.GetArticulosCarrito(idCarrito);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return numArticulos;
        }
        private int CheckArticulosCarritoHermes(int idCarrito)
        {
            int numArticulos = 0;
            try
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objVenta.GetArticulosCarrito(Convert.ToString(idCarrito));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return numArticulos;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //Session["IdCarrito"] = 413;
            decimal gastosEnvio;
            string tiendaEnvio = string.Empty;
            string numTarjeta="";
         //   bool EsDirectivo = false;
            float totalPendiente;
            try
            {
                this.TxtComentarios.Style.Add("visibility", "hidden");
                if (Contexto.IdEmpleado == null)
                {
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                    return;

                }
                
                if (!Page.IsPostBack)
                {
                   
                    if (!string.IsNullOrEmpty(UrlHermesQueryString))
                        UrlHermesSession = UrlHermesQueryString;
                     EstadoProcesoSession = SESSION_ESTADO_PROCESO_INI;
                   
                    if (!string.IsNullOrEmpty(Contexto.IdEmpleado))
                        AsignarUsuario();

                    lblComentario.Visible = true;
                    lblEmail.Visible = true;
                    // TxtComentarios.Visible = true;
                    txtemail.Visible = true;
                    pagado = false;
                    int idCarritoH = IdCarritoQueryString.Value;
                    int ArtiCarrito = CheckArticulosCarritoHermes(idCarritoH);
                    var miMaster = (MasterPageHermes)this.Master;
                    miMaster.MuestraArticulosCarritoHermes(Convert.ToString(ArtiCarrito));
                    
                    if (Session["IdCarrito"] != null)
                        ValidaTotalArticulosTR(Session["IdCarrito"].ToString());
                    //Si el carrito tiene gastos de envio, activamos el check de envio a domicilio
                    gastosEnvio = GetGastosEnvio(IdCarritoQueryString);

                    ///Envio express
                    if (gastosEnvio > 0)
                    {
                        rbEnvioDomicilio.Checked = true;
                        //rbEnvioDomicilio.Enabled = false;
                        //rbEnvioTienda.Enabled = false;
                        chkEnvioExpress.Checked = true;
                        //chkEnvioExpress.Enabled = false;
                        chkEnvioExpress.Visible = true;
                    }
                    else
                    {
                        tiendaEnvio = GetDescripcionTiendaEnvio(IdCarritoQueryString);

                        if (string.IsNullOrEmpty(tiendaEnvio))
                        {
                            rbEnvioDomicilio.Checked = true;
                            chkEnvioExpress.Checked = false;
                            chkEnvioExpress.Visible = true;
                            rbEnvioTienda.Checked = false;
                        }
                        else
                        {
                            //rbEnvioDomicilio.Checked = false;
                            //chkEnvioExpress.Checked = false;
                            //rbEnvioTienda.Checked = true;

                            rbEnvioTienda.Checked = true;
                            //rbEnvioTienda.Enabled = false;
                            //rbEnvioDomicilio.Enabled = false;
                            //chkEnvioExpress.Enabled = false;
                            lblEnvioSeleccionadoRecurso.Visible = true;
                            lblEnvioSeleccionado.Visible = true;
                            lblEnvioSeleccionadoRecurso.Text = "Tienda: ";
                            lblEnvioSeleccionado.Text = tiendaEnvio;

                        }
                    }

                    if (EstaPagadoyNoFinalizado(!IdCarritoSession.HasValue ? IdCarritoQueryString.Value : IdCarritoSession.Value))
                    {

                        rbEnvioDomicilio.Enabled = false;
                        rbEnvioTienda.Enabled = false;
                        chkEnvioExpress.Enabled = false;
                        rbFacturaNo.Enabled = false;
                        rbFacturaSi.Enabled = false;
                    }
                   
                    
                        if (!string.IsNullOrEmpty(IdCarritoQueryString.Value.ToString()))
                            Session["IdCarrito"] = IdCarritoQueryString.Value;
                    ValidarCargaCliente();
                  /*  EsDirectivo = GetNivelCliente(hidIdCliente.Value, Contexto.IdTienda, Convert.ToInt32(Session["IdCarrito"].ToString()));
                    if (EsDirectivo)
                    {
                        Session["EsDirectivo"] = 1;
                        labelDirectivo.Text = "Descuento General Cliente: "+Session["DTODirectivo"].ToString()+",00%";
                        if (Session["DTODirectivo"].ToString() == "100")
                        {
                            RadioButtonlTipoPago.CssClass = "Ocultarcontrol";
                            brlabelDirectivo.Visible = false;
                        }

                        
                        
                    }
                    else labelDirectivo.Text = "";
                    */
                    //Si el carrito tiene asignada una tienda de envío y ha efectuado algún pago, activamos el radiobutton e indicamos tienda seleccionada
                    tiendaEnvio = GetTiendaEnvio(IdCarritoQueryString);

                   if (!string.IsNullOrEmpty(tiendaEnvio))
                    {
                        rbEnvioTienda.Checked = true;
                        rbEnvioTienda.Enabled = false;
                        rbEnvioDomicilio.Enabled = false;
                        chkEnvioExpress.Enabled = false;
                        lblEnvioSeleccionadoRecurso.Visible = true;
                        lblEnvioSeleccionado.Visible = true;
                        lblEnvioSeleccionadoRecurso.Text = "Tienda: ";
                        lblEnvioSeleccionado.Text = tiendaEnvio;
                    }
                    else
                   {
                        rbEnvioTienda.Checked = false;
                    }
                   if (Session["IdCarrito"] != null)
                   {
                       if (ValidaGastosEnvio()) this.chkEnvio.Checked = true;
                       else chkEnvio.Checked = false;

                   }
                    if (IdCarritoQueryString.HasValue)
                    {
                        IdCarritoSession = IdCarritoQueryString;
                        //   int ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());

                        // var miMaster = (MasterPageHermes)this.Master;
                        //miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                        CargaCarrito();
                    
                       // PoneTotales();
                       // CargarPagosCarritos();
                       // ValidarCargaCliente();
                    }
                    else // idcarrito == null
                    {
                        Response.Redirect(UrlHermesSession, true);
                        return;
                    }

                    if (SessionStateQueryString != string.Empty)//Controlar si venimos de RespuestaPago o no.
                    {
                        
                        if (Session["ClienteNine"] != null)
                        {
                            log.Info("LOAD. Existe cliente nine en sesion");
                            numTarjeta = ((CLIENTE9)Session["ClienteNine"]).NumTarjeta;
                            log.Error("LOAD. El nº de tarjeta es " + numTarjeta.ToString());
                            GetClienteActual(numTarjeta);
                            log.Info("LOAD. SESSION NINE SI. Hemos obtenido el cliente actual" );
                            nomcliente.Text = numTarjeta;
                            BtnCliente.Enabled = true;
                        }
                        else
                        {
                            log.Info("LOAD. NO Existe cliente nine en sesion");
                            if (Session["objCliente"] != null)
                            {
                                numTarjeta = ((CLIENTE9)Session["objCliente"]).Cliente;
                                log.Info("LOAD. El nº de tarjeta es " + numTarjeta.ToString());
                                GetClienteActual(numTarjeta);
                                log.Info("LOAD. SESSION NINE NO. Hemos obtenido el cliente actual");
                                nomcliente.Text = numTarjeta;
                                BtnCliente.Enabled = true;
                            }
                            else {
                                if (Session["ANombreDe"]!=null)  this.txtNomCli.Text=Session["ANombreDe"].ToString();
                            }
                        }

                        EstadoProcesoSession = SESSION_ESTADO_PROCESO_PAGO;
                        CargarEstadoProceso();
                        divPendientePago.Style.Add("margin-left", "500px");
                    }

                    if (HiddenTipoCliente.Value.ToString() == "E" || HiddenTipoCliente.Value.ToString().Equals(""))
                    {
                        RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    }

                    RadioButtonlTipoPago.Items[1].Attributes.Add("class", "Relleno1");
                    RadioButtonlTipoPago.Attributes.Add("onclick", string.Format("HacerClickPonerTarjeta('{0}',{1});", RadioButtonlTipoPago.ClientID, RadioButtonlTipoPago.Items.Count));

                    if (!string.IsNullOrEmpty(SessionStateQueryString))
                    {
                        EstadoProcesoSession = SESSION_ESTADO_PROCESO_PAGO;
                        CargarEstadoProceso();

                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                        {
                            decimal gastos = GetGastosEnvio(IdCarritoQueryString);
                            if (gastos == 0 && !string.IsNullOrEmpty(tiendaEnvio))
                            {
                                BtnContinuaPago.Visible = false;
                                BtnFinalizaVenta.Visible = true;
                            }
                            else
                            {
                                BtnContinuaPago.Visible = true;
                                BtnFinalizaVenta.Visible = false;
                            }


                        }
                    }

                }
                else
                {
                    string CtrlID = string.Empty;
                    if (Request.Form[hidSourceID.UniqueID] != null ||
                        Request.Form[hidSourceID.UniqueID] != string.Empty)
                    {
                        CtrlID = Request.Form[hidSourceID.UniqueID];
                    }

                    //if (Session["IdCarrito"] != null)
                    //{
                    //    if (ValidaGastosEnvio()) this.chkEnvio.Checked = true;
                    //    else chkEnvio.Checked = false;

                    //}
                    //ControlarPosicionTotalPendiente();

                    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_ENTREGA)
                    {
                        TxtComentarios.Visible = false;
                        BtnContinuaPago.Style.Add("visibility", "hidden");
                    }

                    #region BT Quitar enviar a POS
                    //if (CtrlID != "ctl00_ContentPlaceHolder1_btnEnviarPOS")
                    //{
                    //    //ViewState["ImporteDescuentos"] = "0.0";
                    //    //ViewState["SubTotal"] = "0.0";
                    //    //ViewState["ImportePagar"] = "0.0";
                    //    //ViewState["ImportePromociones"] = "0.0";
                    //} 
                    #endregion


                }
                if (Session["IdCarrito"] != null)
                    ValidaTotalArticulosTR(Session["IdCarrito"].ToString());

                PoneTotales();
                CargarPagosCarritos();
                //if (string.IsNullOrEmpty(SessionStateQueryString))
                
                if (!string.IsNullOrEmpty(SessionStateQueryString))
                {
                    if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                    {
                        decimal gastos = GetGastosEnvio(IdCarritoQueryString);
                        if (gastos == 0 && !string.IsNullOrEmpty(tiendaEnvio))
                        {
                            BtnContinuaPago.Visible = false;
                            BtnFinalizaVenta.Visible = true;
                        }
                        else
                        {
                            BtnContinuaPago.Visible = true;
                            BtnFinalizaVenta.Visible = false;
                        }


                    }
                }

                if (HiddenTipoCliente.Value.ToString() == "E" || HiddenTipoCliente.Value.ToString().Equals(""))
                {
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                }

                RadioButtonlTipoPago.Items[1].Attributes.Add("class", "Relleno1");
                

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
        #region "Metodos Privados"

        //private void ControlarPosicionTotalPendiente()
        //{
        //    float totalPendiente;
        //    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_INI && !optEntregaOtraUbicacion.Checked)
        //    {
        //        divPendientePago.Style.Add("margin-left", "569px");
        //        divPendientePago.Style.Add("border-top", "1px");
        //        divPendientePago.Style.Add("width", "263px");
        //    }
        //    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_PAGO && optEntregaOtraUbicacion.Checked)
        //    {
        //        divPendientePago.Style.Add("margin-left", "740px");
        //    }
        //    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_PAGO && !optEntregaOtraUbicacion.Checked)
        //    {
        //        divPendientePago.Style.Add("margin-left", "569px");
        //    }
        //    if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0 && EstadoProcesoSession == SESSION_ESTADO_PROCESO_PAGO)
        //    {
        //        divPendientePago.Style.Add("margin-left", "911px");
        //    }

        //}

        public void GetClientesCarrito()
        {

            try
            {

                if (Session["IdCarrito"] != null)
                {
                    DataSet Ds;
                    objVenta = new DLLGestionVenta.ProcesarVenta();
                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    Ds = objVenta.GetClienteCarritoActual(Int64.Parse(Session["IdCarrito"].ToString()));

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        
                        if (Int64.Parse(Ds.Tables[0].Rows[0]["id_Cliente"].ToString()) != 0)
                        {
                            
                            object sender = new object();
                            EventArgs e = new EventArgs();

                            //nomcliente.Text = Ds.Tables[0].Rows[0]["id_Cliente"].ToString();
                            hidIdCliente.Value = Ds.Tables[0].Rows[0]["id_Cliente"].ToString();

                            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
                            if (Session["TiendaCamper"].ToString() != "0")
                            {
                                Session["ClienteNine"] = null;
                            } 
                            if (Session["ClienteNine"] != null)
                            {
                                double pares;
                                int paresRegalado;
                                double Bolsa;
                                int BolsaRegalado;
                                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                                //c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
                                ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();

                                CLIENTE9 objCliente = ((CLIENTE9)Session["ClienteNine"]);

                                cb.idTargeta = objCliente.NumTarjeta;
                                cb.idTienda = AVE.Contexto.IdTienda;
                                cb.idTerminal = AVE.Contexto.IdTerminal;

                                //TarjetaCliente.Text = objCliente.NumTarjeta.ToString();
                                //nomcliente.Text = TarjetaCliente.Text;

                                HiddenTipoCliente.Value = "9";
                                RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");

                                Label10.Text = (objCliente.SaldoPuntosAnt - objCliente.PuntosPagados).ToString();

                                pares = objCliente.ParesAcumuladosAnt - (objCliente.ParPagado * 8);
                                paresRegalado = (pares < 8 ? 0 : int.Parse(Math.Truncate(pares / 8).ToString()));
                                Bolsa = objCliente.BolsasAcumuladasAnt - (objCliente.BolsaPagada * 4);
                                BolsaRegalado = (Bolsa < 4 ? 0 : int.Parse(Math.Truncate(Bolsa / 4).ToString()));

                                Label11.Text = (paresRegalado > 0 ? objCliente.dblSaldoPares9.ToString() : "0");
                                Label12.Text = (BolsaRegalado > 0 ? objCliente.dblSaldoBolsa5.ToString() : "0");
                                Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
                                Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";

                                //VALIDAMOS PAR9 
                                if (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString()), Int64.Parse(pares.ToString())) && objCliente.ParPagado == 0)
                                {
                                    RadioButton3.Enabled = true;
                                }
                                else
                                {
                                    RadioButton3.Checked = false;
                                    RadioButton3.Enabled = false;
                                }

                                //VALIDAMOS BOLSA5
                                if (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString()), objCliente.NivelActual, Int64.Parse(Bolsa.ToString())) && objCliente.BolsaPagada == 0)
                                {
                                    RadioButton4.Enabled = true;
                                }
                                else
                                {
                                    RadioButton4.Checked = false;
                                    RadioButton4.Enabled = false;
                                }

                                //validamos puntos 9   
                                if (objCliente.PuntosPagados == 0)
                                {
                                    RadioButton2.Enabled = true;
                                }
                                else
                                {
                                    RadioButton2.Checked = false;
                                    RadioButton2.Enabled = false;
                                }
                            }
                            else
                            {
                                GetClienteActual(hidIdCliente.Value);
                                //GetClienteActual(nomcliente.Text);
                            }
                        }
                        if (Session["ClienteNine"] != null) ValidaTotalArticulosTR(Session["IdCarrito"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void validarCliente()
        {
            if (Session["ClienteNine"] != null)
            {
                double pares;
                int paresRegalado;
                double Bolsa;
                int BolsaRegalado;
                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                //c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
                ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();

                CLIENTE9 objCliente = ((CLIENTE9)Session["ClienteNine"]);

                cb.idTargeta = objCliente.NumTarjeta;
                cb.idTienda = AVE.Contexto.IdTienda;
                cb.idTerminal = AVE.Contexto.IdTerminal;

                //TarjetaCliente.Text = objCliente.NumTarjeta.ToString();
                //nomcliente.Text = TarjetaCliente.Text;

                HiddenTipoCliente.Value = "9";
                RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");

                Label10.Text = (objCliente.SaldoPuntosAnt - objCliente.PuntosPagados).ToString();

                pares = objCliente.ParesAcumuladosAnt - (objCliente.ParPagado * 8);
                paresRegalado = (pares < 8 ? 0 : int.Parse(Math.Truncate(pares / 8).ToString()));
                Bolsa = objCliente.BolsasAcumuladasAnt - (objCliente.BolsaPagada * 4);
                BolsaRegalado = (Bolsa < 4 ? 0 : int.Parse(Math.Truncate(Bolsa / 4).ToString()));

                Label11.Text = (paresRegalado > 0 ? objCliente.dblSaldoPares9.ToString() : "0");
                Label12.Text = (BolsaRegalado > 0 ? objCliente.dblSaldoBolsa5.ToString() : "0");
                Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
                Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";

                //VALIDAMOS PAR9 
                if (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9TR(Int64.Parse(Session["IdCarrito"].ToString()), Int64.Parse(pares.ToString())) && objCliente.ParPagado == 0)
                {
                    RadioButton3.Enabled = true;
                }
                else
                {
                    RadioButton3.Checked = false;
                    RadioButton3.Enabled = false;
                }

                //VALIDAMOS BOLSA5
                if (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5TR(Int64.Parse(Session["IdCarrito"].ToString()), objCliente.NivelActual, Int64.Parse(Bolsa.ToString())) && objCliente.BolsaPagada == 0)
                {
                    RadioButton4.Enabled = true;
                }
                else
                {
                    RadioButton4.Checked = false;
                    RadioButton4.Enabled = false;
                }

                //validamos puntos 9   
                if (objCliente.PuntosPagados == 0)
                {
                    RadioButton2.Enabled = true;
                }
                else
                {
                    RadioButton2.Checked = false;
                    RadioButton2.Enabled = false;
                }
            }
        }

        public string ObtenerURL(string idArticulo)
        {

            string ruta = string.Empty;

            try
            {
                string rutaLocal;
                string rutaVision = String.Empty;

                // Construimos la ruta en Local
                rutaLocal = UrlImagenesHermesAppSettings + string.Format(ConfigurationManager.AppSettings["Foto.RutaLocalHermes"], GetReferencia(idArticulo));

                log.Error("rutaLocal=" + rutaLocal);

                if (rutaLocal.IndexOf("http://") == 0)
                {
                    if (Comun.RemoteFileExists(rutaLocal, 1000))
                    {
                        ruta = rutaLocal;
                    }
                    else
                    {
                        rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                        ruta = rutaVision;
                    }

                }
                else
                {
                    if (System.IO.File.Exists(Server.MapPath(rutaLocal)))
                        ruta = rutaLocal;
                    else
                    {
                        rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                        ruta = rutaVision;
                    }
                }
                log.Error("ruta=" + ruta);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return ruta;
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
            try
            {
                if (sNumero != "")
                {
                    sVd = (Convert.ToDouble(sNumero)).ToString("c", info).Replace("€", "");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return sVd;
        }
        private int EliminaDatoHermes(string strTalla, string idCarrito){

            int result = 0;
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string strSQL=string.Format("DELETE FROM PEDIDO_LINEA WHERE ID_PEDIDO_LINEA IN (" +
            "SELECT top 1 ID_PEDIDO_LINEA FROM PEDIDO_LINEA " +
            " INNER JOIN PEDIDO ON PEDIDO.ID_PEDIDO = PEDIDO_LINEA.ID_PEDIDO" +
            " inner jOIN SUB_PRODUCTO SB ON SB.ID_SUBPRODUCTO = PEDIDO_LINEA.ID_SUBPRODUCTO" +
            " INNER JOIN TALLA ON TALLA.ID_TALLA = SB.ID_TALLA" +
            " WHERE PEDIDO.ID_PEDIDO_EXTERNO = '{0}' AND TALLA.EU='{1}')",idCarrito, strTalla);
            
            
    
            try {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hermes"].ToString();
                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(strSQL, myConnection);
               result = myCommand.ExecuteNonQuery();
            }
            finally
            {
                if (myConnection != null)
                    myConnection.Close();
            }
            return result;
        
        }
        private string DameTalla(string lineaCarrito, string idCarrito) {
            
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string strTalla = string.Empty;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                
                string sql = "select cab.Nombre_Talla from AVE_CARRITO_LINEA CARR" +
                " inner join CABECEROS_DETALLES CAB on CARR.Id_cabecero_detalle= CAB.Id_Cabecero_Detalle" +
                 " where id_Carrito=" + idCarrito + " and id_Carrito_Detalle=" + lineaCarrito;
  
                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);

                object result = myCommand.ExecuteScalar();

                if (Convert.IsDBNull(result) || string.IsNullOrEmpty(Convert.ToString(result)))
                    strTalla = string.Empty;
                else
                    strTalla = result.ToString();

                return strTalla;
            }
            finally
            {
                if (myConnection != null)
                    myConnection.Close();
            }

        }

        private int EliminaItemCarritoHermes(string itemcarrito, string IdCarrito)
        {
            int result = 0;
            string talla = string.Empty;
            talla = DameTalla(itemcarrito, IdCarrito);

            if (string.IsNullOrEmpty(talla)) {
                log.Error("Talla inexistente en la consulta de cabecero_detalle");
            }
            else {
                result= EliminaDatoHermes(talla, IdCarrito);
            }

            return result;
        }

        //ACL. Valido el total de los articulos tr del carrito.
        private void ValidaTotalArticulosTR(string IdCarrito) {
            objVenta = new DLLGestionVenta.ProcesarVenta();
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            this.hidTotalArtTR.Value =Convert.ToString( objVenta.ObtieneTotalArticulosTR(IdCarrito));
        }
        private void CargaCarrito()
        {
            try
            {
                DataView dv;

                ViewState["ImporteDescuentos"] = "0.0";
                ViewState["SubTotal"] = "0.0";
                ViewState["ImportePagar"] = "0.0";
                ViewState["ImportePromociones"] = "0.0";

                if (Session["IdCarrito"] != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();
                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                 /*   if (Session["EsDirectivo"] != null)
                    {
                        if (Session["EsDirectivo"].ToString() == "0")
                        {
                            DsPromo = objVenta.GetPromoCarritoLinea(Int64.Parse(Session["IdCarrito"].ToString()));
                        }
                    }
                    else*/
                        DsPromo = objVenta.GetPromoCarritoLinea(Int64.Parse(Session["IdCarrito"].ToString()));

                    AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                    AVE_CarritoObtenerDirec.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();

              /*      if (Session["EsDirectivo"] != null)
                    {
                        if (Session["EsDirectivo"].ToString() != "0")
                        {
                            dv = (DataView)AVE_CarritoObtenerDirec.Select(DataSourceSelectArguments.Empty);
                        }
                        else
                            dv = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
                    
                    }
                    else 
                    {*/
                        dv = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
                    /*}*/
                    this.gvCarrito.DataSource = dv;
                    gvCarrito.DataBind();

                    if (DsPromo != null)
                    {
                        if (DsPromo.Tables.Count > 0)
                        {
                            HiddenPromo.Value = (DsPromo.Tables[0].Rows.Count > 0 ? "P" : "");

                        }
                        else
                        {

                            HiddenPromo.Value = "";
                        }
                    }
                    else {
                        HiddenPromo.Value = "";
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        /* ACL.11/07/2014
         * Se utiliza, cuando existe una venta y se ha pagado previamente con puntos o bolsas, existe la posibilidad de
         * cancelar ese tipo de pago. Y poder reinicializar el pago. En este caso. Una vez eliminado los pagos en BD y
         * haber cancelado los pagos de Web Services, se inicializan los totales.
         * */
        private void ReinicializaTotales()
        {
            this.TotPendiente.Text = this.lblTotalPagar.Text;
        }

        private void PoneTotales()
        {
            decimal gastosEnvio;

            try
            {
                if (ViewState["ImporteDescuentos"] != null && ViewState["ImporteDescuentos"].ToString() != "")
                {
                    lblImporteDTOs.Text = FormateaNumero((Convert.ToDouble(ViewState["ImporteDescuentos"])).ToString());
                }
                if (ViewState["SubTotal"] != null && ViewState["SubTotal"].ToString() != "")
                {
                    lblSubTotal.Text = FormateaNumero((Convert.ToDouble(ViewState["SubTotal"])).ToString());
                }
                if (rbEnvioDomicilio.Checked && chkEnvioExpress.Checked)
                {
                    divGastosEnvio.Visible = true;
                    divGastosEnvioRecurso.Visible = true;
                    lblGastosEnvio.Text = FormateaNumero((Convert.ToDecimal(GastosEnvioAppSettings.ToString())).ToString());
                    gastosEnvio = Convert.ToDecimal(GastosEnvioAppSettings.ToString());
                }
                else
                {
                    divGastosEnvio.Visible = false;
                    divGastosEnvioRecurso.Visible = false;
                    gastosEnvio = 0;
                }


                if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
                {
                    double importePagar = Convert.ToDouble(ViewState["ImportePagar"]);

                    this.lblTotalPagar.Text = FormateaNumero((importePagar + Convert.ToDouble(gastosEnvio)).ToString());
                    if (gvCarrito.Rows.Count == 0)
                    {
                        divTotal.Visible = false;
                        divResumenPago.Visible = false;
                        #region BT Quitar Enviar a POS
                        //divenviaPos.Visible = false; 
                        #endregion
                        divFormaPagoCliente.Visible = false;
                        divPendientePago.Visible = false;
                    }
                }

                if (Convert.ToDouble(ViewState["ImporteDescuentos"]) > 0)
                {
                    divEtiqDescuento.Visible = true;
                    divImporteDescuento.Visible = true;
                    #region BT Quitar Enviar a POS
                    //divenviaPos.Attributes.Add("style", "float: left; height: 40px; width: 100px;"); 
                    #endregion

                }
                else
                {
                    divEtiqDescuento.Visible = false;
                    divImporteDescuento.Visible = false;
                    brDscuento.Visible = false;
                    #region BT Quitar Enviar a POS
                    //divenviaPos.Attributes.Add("style", "float: left; height: 30px; width: 100px;"); 
                    #endregion
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void CargarPagosCarritos()
        {
            try
            {      
                DataSet Ds;
               // DataSet Ds2 = new DataSet();
                float TotalPagado = 0;
                Int64 idCarrito;

                objVenta = new DLLGestionVenta.ProcesarVenta();
                if (IdCarritoSession != null)
                    idCarrito = Int64.Parse(IdCarritoSession.ToString());
                else
                    idCarrito = Int64.Parse(IdCarritoQueryString.ToString());

                if (EmailSession != null)
                {
                    txtemail.Text = EmailSession.ToString();
                }

                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                decimal gastosEnvio = 0;

                Ds = objVenta.GetPago(idCarrito);

                if (Ds.Tables.Count > 0)
                {
                    foreach (DataRow Dr in Ds.Tables[0].Rows)
                    {
                        TotalPagado += float.Parse(Dr["Importe"].ToString());

                        if (Dr["Tipo"].ToString().Equals("PAR 9"))
                        {
                            RadioButton3.Enabled = false;
                            RadioButton3.Checked = false;

                        }

                        if (Dr["Tipo"].ToString().Equals("BOLSA 5"))
                        {
                            RadioButton4.Enabled = false;
                            RadioButton4.Checked = false;

                        }

                        if (Dr["Tipo"].ToString().Equals("PUNTOS NINE"))
                        {
                            RadioButton2.Enabled = false;
                            RadioButton2.Checked = false;

                        }

                    }

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        #region BT Quitar enviar a POS
                        //btnEnviarPOS.Visible = false; 
                        #endregion
                        nomcliente.Enabled = false;
                        if (string.IsNullOrEmpty(SessionStateQueryString))
                            BtnCliente.Enabled = false;
                        else
                            BtnCliente.Enabled = true;
                        //TarjetaCliente.Enabled = false;
                        ButClient.Enabled = false;

                    }

                    DataRow rowTotal = Ds.Tables[0].NewRow();
                    rowTotal["Tipo"] = "Total Pagado:";
                    rowTotal["Importe"] = TotalPagado;

                    Ds.Tables[0].Rows.Add(rowTotal);
                    Ds.AcceptChanges();

                    RepeaterPago.DataSource = Ds;
                    RepeaterPago.DataBind();
                }
                else
                {
                    divResumenPago.Visible = false;
                }

                float fSubtotal = 0;
                float fDescuento = 0;
                float fTotalPagar = 0;

                if (rbEnvioDomicilio.Checked && chkEnvioExpress.Checked)
                {
                    gastosEnvio = Convert.ToDecimal(GastosEnvioAppSettings.ToString());
                }
                else
                {
                    gastosEnvio = 0;
                }

                if (float.TryParse(ViewState["SubTotal"].ToString(), out fSubtotal))
                {
                    fDescuento = float.Parse(ViewState["ImporteDescuentos"].ToString());
                    fTotalPagar = (fSubtotal - fDescuento + float.Parse(gastosEnvio.ToString()));
                }


                float fTotal = (fTotalPagar - TotalPagado);
                TotPendiente.Text = FormateaNumero(fTotal.ToString());
                 int requiereFacturacion = GetFacturacion(IdCarritoQueryString);

               
                if (fTotal == 0 && fTotalPagar > 0)
                {
                    RadioButtonlTipoPago.CssClass = "ocul1";
                    RadioButton1.CssClass = "Ocultarcontrol";
                    RadioButton1.Visible = false;
                    btnCancelaVenta.Visible = false;
                    #region BT Quitar enviar a POS
                    //btnEnviarPOS.Visible = false;
                    //btnBorrarCarrito.Visible = false;
                    #endregion
                    if (rbEnvioTienda.Checked && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        if (requiereFacturacion == -1)
                        {
                             BtnFinalizaVenta.Visible = true;
                             BtnContinuaPago.Visible = false;
                        }
                        else
                        {
                            BtnContinuaPago.Visible = true;
                            BtnFinalizaVenta.Visible = false;
                        }
                    }
                    else if (rbEnvioDomicilio.Checked && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        BtnContinuaPago.Visible = true;
                        BtnFinalizaVenta.Visible = false;
                    }
                    RadioButton1.CssClass = "Ocultarcontrol";
                }
                if (fTotal == 0 && fTotalPagar == 0)
                {
                    RadioButtonlTipoPago.CssClass = "ocul1";
                    RadioButton1.CssClass = "Ocultarcontrol";
                    RadioButton1.Visible = false;
                    btnCancelaVenta.Visible = false;
                    #region BT Quitar enviar a POS
                    //btnEnviarPOS.Visible = false;
                    //btnBorrarCarrito.Visible = false;
                    #endregion
                    if (rbEnvioTienda.Checked && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        if (requiereFacturacion == -1)
                        {
                            BtnFinalizaVenta.Visible = true;
                            BtnContinuaPago.Visible = false;
                        }
                        else
                        {
                            BtnContinuaPago.Visible = true;
                            BtnFinalizaVenta.Visible = false;
                        }
                    }
                    else if (rbEnvioDomicilio.Checked && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        BtnContinuaPago.Visible = true;
                        BtnFinalizaVenta.Visible = false;
                    }
                    RadioButton1.CssClass = "Ocultarcontrol";
                }
                if (Session["ClienteNine"] != null)
                {
                    int pares;
                    int paresRegalado;
                    int Bolsa;
                    int BolsaRegalado;

                    CLIENTE9 objCliente = (CLIENTE9)(Session["ClienteNine"]);

                    if (objCliente.BolsaPagada > 0 || objCliente.ParPagado > 0 || objCliente.PuntosPagados > 0)
                    {
                        Label10.Text = (double.Parse(Label10.Text.ToString()) - objCliente.PuntosPagados).ToString();

                        pares = int.Parse(Label13.Text.ToString().Split('(')[0].ToString());
                        paresRegalado = int.Parse(Label13.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
                        Bolsa = int.Parse(Label14.Text.ToString().Split('(')[0].ToString());
                        BolsaRegalado = int.Parse(Label14.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
                        pares = pares - (int.Parse(objCliente.ParPagado.ToString())) * 8;
                        paresRegalado = (paresRegalado > 0 ? paresRegalado - int.Parse(objCliente.ParPagado.ToString()) : 0);
                        Bolsa = Bolsa - (int.Parse(objCliente.BolsaPagada.ToString())) * 4;
                        BolsaRegalado = (BolsaRegalado > 0 ? BolsaRegalado - int.Parse(objCliente.BolsaPagada.ToString()) : 0);

                        Label11.Text = (paresRegalado > 0 ? Label11.Text : "0");
                        Label12.Text = (BolsaRegalado > 0 ? Label12.Text : "0");
                        Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
                        Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";


                        //VALIDAMOS PAR9 
                        if (Int64.Parse(paresRegalado.ToString()) > 0)
                        {
                            RadioButton3.Enabled = true;
                        }
                        else
                        {
                            RadioButton3.Enabled = false;
                        }

                        //VALIDAMOS BOLSA5
                        if (Int64.Parse(BolsaRegalado.ToString()) > 0)
                        {
                            RadioButton4.Enabled = true;
                        }
                        else
                        {
                            RadioButton4.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        #endregion

        #region BT Quitar enviar a POS
        //protected void btnEnviarPOS_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["IdCarrito"] == null)
        //            return;

        //        DataView dv;
        //        this.lblPOS.Text = "";
        //        EnviaPOS.SelectParameters["IdTerminal"].DefaultValue = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
        //        EnviaPOS.SelectParameters["IdTienda"].DefaultValue = HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
        //        EnviaPOS.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
        //        EnviaPOS.SelectParameters["IdUsuario"].DefaultValue = Contexto.IdEmpleado;
        //        if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
        //            EnviaPOS.SelectParameters["dPrecio"].DefaultValue = ViewState["ImportePagar"].ToString();
        //        else
        //            EnviaPOS.SelectParameters["dPrecio"].DefaultValue = "0";
        //        //ACL.08-07-2014. Se graba el comentario general del tiquet, aunque el ENVIO A POS
        //        //graba en la tabla nti
        //        EnviaPOS.SelectParameters["strComentario"].DefaultValue = TxtComentarios.Text;
        //        dv = (DataView)EnviaPOS.Select(new DataSourceSelectArguments());
        //        if (dv != null && dv.Count > 0)
        //        {
        //            if (dv[0][0] != null && dv[0][0].ToString() != "")
        //            {
        //                this.lblPOS.Text = "Terminal: " + dv[0][0].ToString().Split('/')[1] + ". "
        //                                    + Resources.Resource.EnEspera + ": " + dv[0][0].ToString().Split('/')[0];
        //                Session["IdCarrito"] = null;
        //                Session["ClienteNine"] = null;
        //                Session["objCliente"] = null;
        //                this.divDetalle.Visible = false;
        //                this.divPagos.Visible = false;
        //                imgEnvioPos.ImageUrl = "~/img/Ok.png";
        //            }
        //            else
        //            {
        //                this.lblPOS.Text = "No se ha podido poner el ticket en espera";
        //                imgEnvioPos.ImageUrl = "~/img/Error.png";
        //            }
        //            this.divResumen.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //} 
        #endregion
        protected void gvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    //Ponemos valores al boton de eliminacion
                    ImageButton imgBorrar = (ImageButton)e.Row.FindControl("imgBorrar");

                    Label lblPromocionBotas = (Label)e.Row.FindControl("lblPromocionBotas");
                    Label DescriPromo = (Label)e.Row.FindControl("DescriPromo");
                    Label lblPromobr = (Label)e.Row.FindControl("lblPromobr");
                    Label lblPrecioOriginal = (Label)e.Row.FindControl("lblPrecioOriginal");
                    Label lblPorcentajeDescuento = (Label)e.Row.FindControl("lblPorcentajeDescuento");
                    Label lblPorDescuento = (Label)e.Row.FindControl("lblPorDescuento");
                    Label lblImporteDTO = (Label)e.Row.FindControl("lblImporteDTO");
                    Label Label1 = (Label)e.Row.FindControl("Label1");
                    Label Label5 = (Label)e.Row.FindControl("Label5");
                    Label lblaPagar = (Label)e.Row.FindControl("lblPagar");

                    Panel divPromocionBotas = (Panel)e.Row.FindControl("divPromocionBotas");
                    HtmlControl divDescuentoPromocion = (HtmlControl)e.Row.FindControl("divDescuentoPromocion");
                    Panel divforanea = (Panel)e.Row.FindControl("divforanea");
                    DropDownList ddlSelecionarPromocion = (DropDownList)e.Row.FindControl("ddlSelecionarPromocion");
                    HtmlGenericControl laDescuento = (HtmlGenericControl)e.Row.FindControl("laDescuento");
                    HtmlGenericControl laImporteDescuento = (HtmlGenericControl)e.Row.FindControl("laImporteDescuento");
                    HtmlGenericControl PorcDescuento = (HtmlGenericControl)e.Row.FindControl("PorcDescuento");

                    float fImporteDescuento = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DTOArticulo"));
                    float fPrecioORI = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "PVPORI"));
                    float fprecioAct = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "PVPACT"));
                    float fDescuentoPromo = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DTOPROMO"));
                    float fDescuento = 0;

                    String idTienda = DataBinder.Eval(e.Row.DataItem, "IdTienda").ToString();
                    String IdLineaCarrito = gvCarrito.DataKeys[e.Row.DataItemIndex].Value.ToString();


                    imgBorrar.Attributes.Add("onclick", "javascript:return ValidarEliminarCarrito()");

                    if (lblPromocionBotas.Text != "-$0.00")
                    {
                        /// HAcemos visible el panel de promocion de botas
                        divPromocionBotas.Visible = true;
                        divPromocionBotas.Attributes.Add("style", "display:block");

                        // Tachamos el precio  original                        
                        lblPrecioOriginal.Attributes.Add("style", "text-decoration:line-through");

                        DescriPromo.Text = DataBinder.Eval(e.Row.DataItem, "promocion").ToString() + ":";
                        lblPromobr.Visible = true;
                        lblPromobr.Text = "<br />";

                        /// Establecemos el descuento de la promoción. 
                        /// TODO: Establecer el porcentaje en el descuento
                        if (fPrecioORI > 0)
                        {
                            divDescuentoPromocion.Visible = true;
                            float descuentoPromocion = ((fDescuentoPromo * 100) / fPrecioORI);
                            // /// Descartar decimales.
                            // lblPorcentajeDescuento.Text = string.Format("{0} %", Math.Round(descuentoPromocion, 2));
                        }
                    }
                    else
                    {
                        divPromocionBotas.Visible = false;
                        divPromocionBotas.Attributes.Add("style", "display:none; clear:left");
                    }
                    if (DsPromo != null)
                    {
                        /// Comprobamos que el articulo tiene mas de una promocion por linea
                        DataView dvLineaPromo = DsPromo.Tables[0].DefaultView;
                        dvLineaPromo.RowFilter = "id_linea_Carrito=" + IdLineaCarrito;
                        if (dvLineaPromo.Count > 0)
                        {
                            lblPromocionBotas.Visible = false;

                            ddlSelecionarPromocion.Visible = true;
                            ddlSelecionarPromocion.DataSource = dvLineaPromo;
                            ddlSelecionarPromocion.DataTextField = "DescriPromo";
                            ddlSelecionarPromocion.DataValueField = "idPromo";
                            ddlSelecionarPromocion.DataBind();

                            divPromocionBotas.Visible = true;
                            divPromocionBotas.Attributes.Add("style", "display:block");

                            lblPromobr.Text = "<br />";
                            DescriPromo.Text = "Promoción:";

                        }
                    }
                    /// Comprobamos si el articulo es de la misma tienda
                    if (idTienda != Contexto.IdTienda)
                    {
                        divforanea.Visible = true;
                        Label1.Text += " - " + DataBinder.Eval(e.Row.DataItem, "IdTienda").ToString();
                    }
                    else
                    {
                        divforanea.Visible = false;
                    }

                    /// Calculamos el descuenta
                    fDescuento = 100 * (fPrecioORI - fprecioAct) / fPrecioORI;
                    if (fImporteDescuento > 0)
                    {
                        CultureInfo info = CultureInfo.GetCultureInfo("es-MX");
                        fDescuento = fDescuento / 100;
                        lblPorDescuento.Text = Convert.ToDouble(fDescuento).ToString("P2", info);
                        lblPorDescuento.Visible = false;
                        //lblPorcentajeDescuento.Text = 
                        Label5.Text = "Dto " + Convert.ToDouble(fDescuento).ToString("P2", info);
                        lblPrecioOriginal.Attributes.Add("style", "text-decoration:line-through");
                    }
                    else
                    {
                        lblPorDescuento.Text = "0.00%";
                        lblPorDescuento.Visible = false;
                        Label5.Visible = false;
                        lblImporteDTO.Visible = false;
                        laDescuento.Visible = false;
                        laImporteDescuento.Visible = false;
                        PorcDescuento.Visible = false;

                    }

                    ViewState["ImporteDescuentos"] = Convert.ToDouble(ViewState["ImporteDescuentos"]) + fImporteDescuento + fDescuentoPromo;
                    ViewState["SubTotal"] = Convert.ToDouble(ViewState["SubTotal"]) + fPrecioORI;


                    if (lblaPagar != null)
                    {
                        lblaPagar.Text = FormateaNumero((fPrecioORI - fImporteDescuento - fDescuentoPromo).ToString());
                        ViewState["ImportePagar"] = Convert.ToDouble(ViewState["ImportePagar"]) + ((fPrecioORI - fImporteDescuento) - fDescuentoPromo);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Borrar")
                {
                    float totalPendiente;

                    if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Borrar", "alert('No puede eliminar un articulo si el carrito esta pagado');", true);
                        return;
                    }
                    //ACL. 29-12-2014. Eliminamos la linea de carrito de hermes. 
                    EliminaItemCarritoHermes(e.CommandArgument.ToString(), Session["IdCarrito"].ToString());

                    AVE_CarritoEliminar.DeleteParameters["IdLineaCarrito"].DefaultValue = e.CommandArgument.ToString();
                    AVE_CarritoEliminar.Delete();

                    

                    CargaCarrito();
                    PoneTotales();
                    CargarPagosCarritos();
                    GetClientesCarrito();

                    ClsCapaDatos objDatos = new ClsCapaDatos();
                    objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                    string sPage = "~/Inicio.aspx";
                    if (objDatos.TotalLineasCarrito(int.Parse(Session["IdCarrito"].ToString())) == 0)
                    {
                        Session["IdCarrito"] = null;
                        Session["ClienteNine"] = null;
                        Session["objCliente"] = null;
                        Response.Redirect(UrlHermesQueryString, true);
                        return;
                    }
                    else
                    {
                        CalculoPromocion_CarritoCliente();
                        sPage = "~/CarritoDetalleHERMES.aspx?IdCarrito=" + IdCarritoQueryString + "&UrlHermes=" + UrlHermesQueryString;
                    }
                    Response.Redirect(sPage, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }


        #region BT Quitar envio a POS
        //protected void btnAceptar_Click(object sender, EventArgs e)
        //{
        //    if (lblPOS.Text.Equals("No se ha podido poner el ticket en espera"))
        //    {
        //        this.divResumen.Visible = false;
        //        return;
        //    }

        //    Response.Redirect(Constantes.Paginas.Inicio);
        //} 
        #endregion

        [WebMethod(EnableSession = true)]
        public static xmlmpos getdatosMPOS()
        {
            xmlmpos oObject = getMPOSSession("", "", "", "");

            return oObject;
        }

        [WebMethod(EnableSession = true)]
        public static string getdatosEncriptadosMPOS(string Amount, string Tarjeta, string TipoMIT, string merchant, string Email)
        {
            // MJM 26/02/2014 INICIO

            Carrito_Pago objPago;

            // Esto se declara de nuevo, no se usa el que está definido a nivel de clase porque los WebMethods no lo ven.
            DLLGestionVenta.ProcesarVenta mObjVenta;

            mObjVenta = new DLLGestionVenta.ProcesarVenta();
            Int64 idCarrito = Int64.Parse(HttpContext.Current.Session["IdCarrito"].ToString());
            mObjVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            objPago = new Carrito_Pago();

            objPago.IdCarrito = idCarrito;
            objPago.TipoPago = System.Configuration.ConfigurationManager.AppSettings["TarjetaTipo"].ToString();

            if (TipoMIT.ToUpper().Equals("AMEX"))
            {
                objPago.TipoPagoDetalle = string.Format("{0} {1}", TipoMIT, Tarjeta);
            }
            else
            {
                objPago.TipoPagoDetalle = Tarjeta;
            }

            objPago.NumTarjeta = "";
            objPago.Importe = float.Parse(Amount, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

            Int64 idCarritoPago = mObjVenta.PagoCarrito(objPago, false);
            HttpContext.Current.Session["IdCarritoPago"] = idCarritoPago;

            // MJM 26/02/2014 FIN

            string sVd = "";
            string semilla = HttpContext.Current.Session[Constantes.Session.Semilla].ToString();

            rc4 encripta = new rc4();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            xmlmpos oObject = getMPOSSession(Amount, TipoMIT, merchant, Email);

            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            StringWriterUtf8 text = new StringWriterUtf8();

            xmlSerializer.Serialize(text, oObject, namespaces);

            sVd = text.ToString();
            //log.Error("xml enviado:" + sVd);
            sVd = sVd.Replace("\r\n", "");
            // sVd = R4.Encrypt(semilla,sVd); //TODO: Comprobar que esta clase RC4 funciona correctamente. 
            sVd = encripta.StringToHexString(encripta.Salaa(sVd, semilla));
            return sVd;
        }
        //ACL.26-08/2014. Creamos un nuevo tickets, para poder pasarlo como referencia para la compra por tarjeta MIT
        private static string getUltimoTicket()
        {
            string resultado = "";

            ClsCapaDatos Bdatos = new ClsCapaDatos();
            try
            {
                Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                resultado = Bdatos.ObtieneUltimoTicket(AVE.Contexto.IdTienda, AVE.Contexto.IdTerminal);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return resultado;
        }
        
        
        //MJM 23/04/2014
        //private static xmlmpos getMPOSSession(string Amount, String TipoMIT) 
        private static xmlmpos getMPOSSession(string Amount, String TipoMIT, string merchant, string email)
        {
            xmlmpos oObject = new xmlmpos();
            if (HttpContext.Current.Session[Constantes.Session.IdCompany] != null)
            {
                oObject.amount = Amount.Replace(",", "");
              
                string idTicket = getUltimoTicket();
                oObject.reference = HttpContext.Current.Session["IdCarrito"].ToString() + "-" + idTicket + "/" + AVE.Contexto.IdTerminal.Trim() + "/" + AVE.Contexto.IdTienda;
                //oObject.reference = HttpContext.Current.Session["IdCarrito"].ToString() + "/" + AVE.Contexto.IdTienda.Trim() + " /" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                //"5122013974"; //TODO: Falta definir como conseguir la referencia
                oObject.id_company = HttpContext.Current.Session[Constantes.Session.IdCompany].ToString();
                oObject.id_branch = HttpContext.Current.Session[Constantes.Session.IdBranch].ToString();

                // MJM 23/04/2014 INICIO
                //oObject.cd_merchant = (TipoMIT.Equals("V/MC") ? HttpContext.Current.Session[Constantes.Session.CdMerchant].ToString() : HttpContext.Current.Session[Constantes.Session.MerchantAMEX].ToString());
                oObject.cd_merchant = merchant;
                // MJM 23/04/2014 FIN

                // MJM 07/05/2014 INICIO
                oObject.cc_type = TipoMIT;
                // MJM 07/05/2014 FIN


                oObject.currency = "MXN"; //TODO: Falta definir como conseguir la moneda
                oObject.country = HttpContext.Current.Session[Constantes.Session.Country].ToString();
                oObject.cd_user = HttpContext.Current.Session[Constantes.Session.CdUser].ToString();
                oObject.password = HttpContext.Current.Session[Constantes.Session.CdPassword].ToString();
                oObject.cd_usrtrx = "USR MPOS";
                //Ya se comprobo que el cliente estuviera informado en carritodetalle.js, si no lo estuviera el mail
                //vendria a empty.
                if (email != "") oObject.e_mail = email;
                else oObject.e_mail = "";
            }

            return oObject;
        }

        /// <summary>
        /// metodo para capturar el envneto keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BCliente_Click(object sender, EventArgs e)
        {
         /*   bool EsDirectivo= false;
            Session["EsDirectivo"] = 0;*/
            try
            {
                if (nomcliente.Text.Trim().Length > 3)
                {
                    Int64 numTarjeta;

                    if ((nomcliente.Text.Trim().Length == 16) && Int64.TryParse(nomcliente.Text, out numTarjeta))
                    {
                        if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
                        //ACL. Es una tienda camper, no debería ejecutarse la consulta de beneficios.
                        if (Convert.ToInt32(Session["TiendaCamper"].ToString()) > 0)
                        {
                            GetClienteActual(nomcliente.Text);
                            CargaCarrito();
                            PoneTotales();
                            CargarPagosCarritos();
                            txtNomCli.Text = nomcliente.Text;
                        }
                        else
                        {
                            this.BuscarClienteNine(nomcliente.Text);

                            HiddenTipoCliente.Value = "9";
                            ValidaTotalArticulosTR(Session["IdCarrito"].ToString());
                        }
                    }
                    else
                    {
                       
                        GetClienteActual(nomcliente.Text);
                      /*  EsDirectivo = GetNivelCliente(hidIdCliente.Value, Contexto.IdTienda, Convert.ToInt32(Session["IdCarrito"].ToString()));
                        if (!EsDirectivo)
                        {*/
                         //   labelDirectivo.Text = "";
                            CalculoPromocion_CarritoCliente();
                     /*   }
                        else
                        {
                            Session["EsDirectivo"] = 1;
                            labelDirectivo.Text = "Descuento General Cliente: " + Session["DTODirectivo"].ToString() + ",00%";
                            if (Session["DTODirectivo"].ToString() == "100")
                            {
                                RadioButtonlTipoPago.CssClass = "Ocultarcontrol";
                                brlabelDirectivo.Visible = false;
                            }

                        }*/
                        CargaCarrito();
                        PoneTotales();
                        CargarPagosCarritos();
                        txtNomCli.Text = nomcliente.Text;
                        ValidaTotalArticulosTR(Session["IdCarrito"].ToString());
                       
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", "alert('Debe introducir más de 3 caracteres para buscar.');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
        /// <summary>
        /// ACL.11-07-2014. BOTÓN PARA CANCELAR O INICIALIZAR UN PAGO POR PUNTOS, BOLSAS, SIN PERDER EL CARRITO.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButCancelarPago_Click(object sender, EventArgs e)
        {
            try
            {
                string idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                string carrito = Convert.ToString(Session["IdCarrito"]);
                ClsCapaDatos Bdatos = new ClsCapaDatos();

                Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                bool res = Bdatos.EliminaCarritoBD(carrito, (string)Contexto.IdEmpleado, idMaquina);

                StockTiendaBL stock = new StockTiendaBL();

                 stock.cancelarReservaAC(IdCarritoSession.ToString(), null, Contexto.IdEmpleado, UserThinkRetailAppSettings, PassThinkRetailAppSettings, Contexto.IdTerminal, Contexto.IdTienda);
      
                //para cancelar todas las operaciones de cliente 9.
                Comun.CheckPending();


                /*  cargaCarrito();
                  poneTotales();
                  CargarPagosCarritos();
                  GetClientesCarrito();*/

                ReinicializaTotales();
                CargarPagosCarritos();
                GetClientesCarrito();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
        public int ValidaPagosNine(string importeParcial,ref double subTotal, ref double limiteDouble)
        {
            int result = 0;
            if (hidPagosNineTR.Value == "") hidPagosNineTR.Value = "0";
            double importeTmp = double.Parse(hidPagosNineTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) + double.Parse(importeParcial, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
            double TotalMaximo = double.Parse(hidTotalArtTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
            subTotal = importeTmp;
            if (TotalMaximo >= importeTmp) result = 1;
            else limiteDouble = TotalMaximo - double.Parse(hidPagosNineTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
            return result;

        }
        /// <summary>
        /// evento que va a capturar el pago y registrar toda la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButPagar_Click(object sender, EventArgs e)
        {

            try
            {
                float Num;
                float totalPendiente;
                Carrito_Pago objPago;
                String ResultPago = "0";
                double pagoPuntosNine = 0;
                double saldoPuntosNine = 0;
                double totalPuntosNine = 0;
                double totalPuntosPar = 0;
                double pagoPuntosPar = 0;
                double saldoPuntosPar = 0;
                double totalPuntosBolsa = 0;
                double pagoPuntosBolsa = 0;
                double saldoPuntosBolsa = 0;
                string tiendaSeleccionada = string.Empty;
                string lineaCarrito = string.Empty;
                string descripcion = string.Empty;
                string precioArticulo = string.Empty;
                string UserHermes = "";
                string PassHermes="";
                string SiteHermes ="";

                ClientScriptManager cs;
                string script;
                List<string> articulosSinStock = new List<string>();
                if (hidPuntos.Value != null) {
                    if (hidPuntos.Value != "")
                    {
                        RadioButton2.Checked = true;
                    }
                }
                UserHermes = System.Configuration.ConfigurationManager.AppSettings["UserHERMESWS"].ToString();
                PassHermes = System.Configuration.ConfigurationManager.AppSettings["PassHERMESWS"].ToString();
                SiteHermes = System.Configuration.ConfigurationManager.AppSettings["IdSiteHERMESWS"].ToString();
                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.FechaSesion = AVE.Contexto.FechaSesion;
                Session["ANombreDe"] = this.txtNomCli.Text;

                if (!rbEnvioDomicilio.Checked && !rbEnvioTienda.Checked)
                {
                    cs = Page.ClientScript;
                    script = "Debe seleccionar un tipo de envio antes de pagar";
                    cs.RegisterStartupScript(this.GetType(), "envio", "alert('" + script + "');", true);
                    return;
                }

                if (!rbFacturaSi.Checked && !rbFacturaNo.Checked)
                {
                    cs = Page.ClientScript;
                    script = "Debe seleccionar si requiere facturación antes de pagar";
                    cs.RegisterStartupScript(this.GetType(), "envio", "alert('" + script + "');", true);
                    return;
                }

                //Almacenar en session el email del pago para pasarlo a los datos de entrega.Se controla que sea obligatorio desde js
                EmailSession = txtemail.Text;

                objPago = new Carrito_Pago();

                //Deshabilitamos el radioButton de entrega en domicilio
                rbEnvioDomicilio.Enabled = false;
                rbEnvioTienda.Enabled = false;
                chkEnvioExpress.Enabled = false;
                rbFacturaNo.Enabled = false;
                rbFacturaSi.Enabled = false;

                //Recorrer cada linea de carrito y comprobar stock de ese articulo
                //Comprobar stock antes de pagar, si no cumple ninguna de las condiciones se eliminará el articulo del carrito.
                StockTiendaBL stock = new StockTiendaBL();
                
                if (rbEnvioDomicilio.Checked)//Envio a domicilio
                {
                    articulosSinStock = stock.ComprobarStock(IdCarritoSession.ToString(), null, Contexto.IdEmpleado, UserHermes, PassHermes,SiteHermes,Contexto.IdTerminal,Contexto.IdTienda);
                    guardarEmailEntrega(txtemail.Text, string.Empty);
                }
                else if (rbEnvioTienda.Checked) //Envio a tienda
                {
                    tiendaSeleccionada = GetTiendaEnvio(IdCarritoSession);

                   
                    
                   articulosSinStock = stock.ComprobarStock(IdCarritoSession.ToString(), tiendaSeleccionada, Contexto.IdEmpleado, UserHermes, PassHermes,SiteHermes,Contexto.IdTerminal,Contexto.IdTienda);
                    guardarEmailEntrega(txtemail.Text, tiendaSeleccionada);
                }
                //Tratamiento de los articulos que no tienen stock en ninguna tienda (se eliminan del carrito)
                if (articulosSinStock.Count == 1)
                {
                    foreach (string item in articulosSinStock)
                    {
                        lineaCarrito = item.Split('#')[0].ToString();
                        descripcion = item.Split('#')[1].ToString();
                        precioArticulo = item.Split('#')[2].ToString();

                        cs = Page.ClientScript;
                        script = "El artículo " + descripcion + " no tiene stock en ninguna de nuestras tiendas,se eliminará del carrito";
                        //script += " se procederá a la eliminación del articulo en el carrito";
                        //script = "Sin stock";
                        cs.RegisterStartupScript(this.GetType(), "SinStock", "confirm('" + script + "');", true);

                        AVE_CarritoEliminar.DeleteParameters["IdLineaCarrito"].DefaultValue = lineaCarrito;
                        AVE_CarritoEliminar.Delete();

                        txtPago.Text = Convert.ToString(float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) - float.Parse(precioArticulo));

                    }
                }
                else if (articulosSinStock.Count > 1)
                {
                    string cantidadArticulos = articulosSinStock.Count.ToString();
                    cs = Page.ClientScript;
                    script = "Hay " + cantidadArticulos + " sin stock en ninguna de nuestras tiendas, se eliminarán del carrito";
                    //script += " se procederá a la eliminación de estos articulos en el carrito";
                    //script = "Sin stock";
                    cs.RegisterStartupScript(this.GetType(), "SinStock", "confirm('" + script + "');", true);

                    foreach (string item in articulosSinStock)
                    {
                        lineaCarrito = item.Split('#')[0].ToString();
                        precioArticulo = item.Split('#')[2].ToString();
                        AVE_CarritoEliminar.DeleteParameters["IdLineaCarrito"].DefaultValue = lineaCarrito;
                        AVE_CarritoEliminar.Delete();

                        txtPago.Text = Convert.ToString(float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) - float.Parse(precioArticulo));

                    }
                }


                //Si hemos eliminado articulos sin stock, y el carrito se queda vacio, redireccionamos a HErmes
                ClsCapaDatos objDatos = new ClsCapaDatos();
                objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                if (objDatos.TotalLineasCarrito(int.Parse(Session["IdCarrito"].ToString())) == 0)
                {
                    Session["IdCarrito"] = null;
                    Session["ClienteNine"] = null;
                    Session["objCliente"] = null;
                    Response.Redirect(UrlHermesQueryString, true);
                    //return;
                }


                ///pago por nota de empleado
                if (RadioButton1.Checked)
                {
                    if (float.TryParse(txtPago.Text.ToString().Replace(",", ""), out Num))
                    {
                        objPago.IdCarrito = idCarrito;
                        objPago.TipoPago = "NOTA EMPLEADO";
                        objPago.TipoPagoDetalle = "";
                        objPago.NumTarjeta = "";
                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                        pagoPuntosPar = double.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));


                        if (gvCarrito.Rows.Count > 2)
                        {
                            lblPagoPuntosNine.Style.Add("font-size", "13px");
                        }

                        ResultPago = objVenta.PagoCarritoNotaEmpleado(objPago, true);

                        if (ResultPago != "0" && ResultPago != "1")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "NOTEMP", "alert('" + ResultPago + "');", true);
                            return;
                        }

                        lblPagoPuntosNine.Text = "- " + FormateaNumero(pagoPuntosPar.ToString());
                        lblPagoPuntosNine.Visible = true;
                        lblPagoPuntosNineRecurso.Text = "Nota Empleado:";
                        lblPagoPuntosNineRecurso.Visible = true;

                        txtPago.Text = String.Empty;

                        RadioButton1.Checked = false;

                        #region BT Quitar enviar a POS
                        //btnEnviarPOS.Visible = false; 
                        #endregion
                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                        {
                            BtnCliente.Enabled = false;
                            //TarjetaCliente.Enabled = false;
                            ButClient.Enabled = false;
                            btnCancelaVenta.Visible = false;
                            RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        }
                        nomcliente.Enabled = false;
                    }
                }
                else
                {
                    ///pago Tarjeta MIt

                    if (RadioButtonlTipoPago.SelectedValue.ToString() == "T")
                    {

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Pago tarjeta", "$(document).ready(function(){InicializaFormPOST();});", true);

                        //    objPago.IdCarrito = idCarrito;
                        //    objPago.TipoPago = System.Configuration.ConfigurationManager.AppSettings["TarjetaTipo"].ToString();
                        //    objPago.TipoPagoDetalle = lstTarjetas.SelectedItem.Text;
                        //    objPago.NumTarjeta = "";
                        //    objPago.Importe = float.Parse(txtPago.Text.ToString());

                        //    objVenta.PagoCarrito(objPago);
                        //    txtPago.Text = String.Empty;

                    }
                    //else
                    //{
                    //cliente 9
                    if (RadioButtonlTipoPago.SelectedValue.ToString() == "9")
                    {

                        ws.cls_Cliente9 c9 = new ws.cls_Cliente9();

                        String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                        if (!Comun.CheckURLWs(url, 10000))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                            return;
                        }

                        VENTA _v = new VENTA();

                        _v.Id_Tienda = AVE.Contexto.IdTienda;
                        _v.ID_TERMINAL = AVE.Contexto.IdTerminal;
                        _v.IdCajero = int.Parse(AVE.Contexto.IdEmpleado);
                        _v.Fecha = AVE.Contexto.FechaSesion;
                        _v.Id_Empleado = int.Parse(AVE.Contexto.IdEmpleado);

                        Cliente9.cls_Cliente9 C9p = new Cliente9.cls_Cliente9(_v);
                        C9p.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


                        ws.cls_Cliente9.SolicitaRedencion sr = new ws.cls_Cliente9.SolicitaRedencion();
                        if ((!RadioButton2.Checked) && (!RadioButton3.Checked) && (!RadioButton4.Checked)) RadioButton2.Checked = true;

                        if (RadioButton3.Checked)
                        {
                            // par 9
                            double limiteDouble = 0;
                                    double SubTotal = 0;
                                    if (ValidaPagosNine(this.txtPago.Text, ref SubTotal, ref  limiteDouble) > 0)
                                    {
                                        sr.intTipo = 2;
                                        sr.strTarjeta = ((CLIENTE9)Session["ClienteNine"]).NumTarjeta;
                                        sr.dblMonto = 0;
                                        sr.strTienda = AVE.Contexto.IdTienda;
                                        sr.idTerminal = AVE.Contexto.IdTerminal;
                                        sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);



                                        C9p.InvokeWS_OperacionesPendientes(2, String.Empty, true);


                                        String ret = c9.InvokeWS_SolicitaRedencion(ref sr);

                                        if (sr.strBitRedencionP == "1")
                                        {
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "PAR9", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                                            return;
                                        }


                                        ((CLIENTE9)Session["ClienteNine"]).ParPagado = 1;


                                        objPago.IdCarrito = idCarrito;
                                        objPago.TipoPago = "PAR 9";
                                        objPago.TipoPagoDetalle = "";
                                        objPago.NumTarjeta = sr.strNoAutorizacion;
                                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                                        totalPuntosPar = double.Parse(Label11.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        pagoPuntosPar = double.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        saldoPuntosPar = totalPuntosPar - pagoPuntosPar;


                                        lblPagoPuntosNine.Text = "- " + FormateaNumero(pagoPuntosPar.ToString());
                                        if (gvCarrito.Rows.Count > 2)
                                        {
                                            lblPagoPuntosNine.Style.Add("font-size", "13px");
                                        }
                                        lblSaldoNine.Text = FormateaNumero(saldoPuntosPar.ToString());
                                        lblPagoPuntosNine.Visible = true;
                                        lblPagoPuntosNineRecurso.Text = "Pago Par 9:";
                                        lblPagoPuntosNineRecurso.Visible = true;
                                        lblSaldoNine.Visible = true;
                                        lblSaldoNineRecurso.Text = "Saldo Par 9: ";
                                        lblSaldoNineRecurso.Visible = true;
                                        hrSaldo.Visible = true;

                                        objVenta.PagoCarrito(objPago);
                                        hidPagosNineTR.Value = Convert.ToString(double.Parse(hidPagosNineTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) + double.Parse(this.txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")));
                                        txtPago.Text = String.Empty;

                                        RadioButton3.Checked = false;

                                        #region BT Quitar enviar a POS
                                        //btnEnviarPOS.Visible = false; 
                                        #endregion
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }

                                        nomcliente.Enabled = false;
                                        
                                    }
                                    else 
                                    {
                                        //ACL. Se ha sobrepasado el limite de pagos nine.
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }

                                        nomcliente.Enabled = false;
                                        this.txtPago.Text = limiteDouble.ToString();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Pares", "alert('Se ha sobrepasado el límite de pagos Nine. Solo puede pagar $" + limiteDouble + ", con Pares Nine.');", true);
                                    }
                        }
                        else
                        {
                            if (RadioButton4.Checked)
                            {
                                // bolsas
                                    double limiteDouble = 0;
                                    double SubTotal = 0;
                                    if (ValidaPagosNine(this.txtPago.Text, ref SubTotal, ref  limiteDouble) > 0)
                                    {
                                        sr.intTipo = 3;
                                        sr.strTarjeta = ((CLIENTE9)Session["ClienteNine"]).NumTarjeta;
                                        sr.dblMonto = 0;
                                        sr.strTienda = AVE.Contexto.IdTienda;
                                        sr.idTerminal = AVE.Contexto.IdTerminal;
                                        sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);

                                        C9p.InvokeWS_OperacionesPendientes(3, String.Empty, true);

                                        String ret = c9.InvokeWS_SolicitaRedencion(ref sr);

                                        if (sr.strBitRedencionP == "1")
                                        {
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "BOLSAS", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                                            return;
                                        }

                                        objPago.IdCarrito = idCarrito;
                                        objPago.TipoPago = "BOLSA 5";
                                        objPago.TipoPagoDetalle = "";
                                        objPago.NumTarjeta = sr.strNoAutorizacion;
                                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                                        totalPuntosBolsa = double.Parse(Label12.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        pagoPuntosBolsa = double.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        saldoPuntosBolsa = totalPuntosBolsa - pagoPuntosBolsa;


                                        lblPagoPuntosNine.Text = "- " + FormateaNumero(pagoPuntosBolsa.ToString());
                                        if (gvCarrito.Rows.Count > 2)
                                        {
                                            lblPagoPuntosNine.Style.Add("font-size", "13px");
                                        }
                                        lblSaldoNine.Text = FormateaNumero(saldoPuntosBolsa.ToString());
                                        lblPagoPuntosNine.Visible = true;
                                        lblPagoPuntosNineRecurso.Text = "Pago Bolsa:";
                                        lblPagoPuntosNineRecurso.Visible = true;
                                        lblSaldoNineRecurso.Text = "Saldo Bolsa: ";
                                        lblSaldoNine.Visible = true;
                                        lblSaldoNineRecurso.Visible = true;
                                        hrSaldo.Visible = true;

                                        objVenta.PagoCarrito(objPago);
                                        hidPagosNineTR.Value = Convert.ToString(double.Parse(hidPagosNineTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) + double.Parse(this.txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")));
                                        txtPago.Text = String.Empty;
                                        ((CLIENTE9)Session["ClienteNine"]).BolsaPagada = 1;

                                        RadioButton4.Checked = false;

                                        #region BT Quitar enviar a POS
                                        //btnEnviarPOS.Visible = false; 
                                        #endregion
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }
                                        nomcliente.Enabled = false;
                                    }
                                    else 
                                    {
                                        //ACL. Se ha sobrepasado el limite de pagos nine.
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }

                                        nomcliente.Enabled = false;
                                        this.txtPago.Text = limiteDouble.ToString();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Bolsas", "alert('Se ha sobrepasado el límite de pagos Nine. Solo puede pagar $" + limiteDouble + ", con Bolsas Nine.');", true);
                                    }

                            }
                            else
                            {
                                // puntos 9

                                if (RadioButton2.Checked)
                                {
                                    double limiteDouble = 0;
                                    double SubTotal = 0;
                                    if (ValidaPagosNine(this.txtPago.Text, ref SubTotal, ref  limiteDouble) > 0)
                                    {
                                        sr.intTipo = 1;

                                        sr.strTarjeta = ((CLIENTE9)Session["ClienteNine"]).NumTarjeta;
                                        sr.dblMonto = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        sr.strTienda = AVE.Contexto.IdTienda;
                                        sr.idTerminal = AVE.Contexto.IdTerminal;
                                        sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);

                                        C9p.InvokeWS_OperacionesPendientes(1, String.Empty, true);

                                        String ret = c9.InvokeWS_SolicitaRedencion(ref sr);

                                        if (sr.strBitRedencionP == "1")
                                        {
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Puntos9", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                                            return;
                                        }


                                        objPago.IdCarrito = idCarrito;
                                        objPago.TipoPago = "PUNTOS NINE";
                                        objPago.TipoPagoDetalle = "";
                                        objPago.NumTarjeta = sr.strNoAutorizacion;
                                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                                        totalPuntosNine = double.Parse(Label10.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        pagoPuntosNine = double.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                                        saldoPuntosNine = totalPuntosNine - pagoPuntosNine;

                                        objVenta.PagoCarrito(objPago);
                                        hidPagosNineTR.Value = hidPagosNineTR.Value = Convert.ToString(double.Parse(hidPagosNineTR.Value, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")) + double.Parse(this.txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX")));
                                        txtPago.Text = String.Empty;

                                        lblPagoPuntosNine.Text = "- " + FormateaNumero(pagoPuntosNine.ToString());
                                        if (gvCarrito.Rows.Count > 2)
                                        {
                                            lblPagoPuntosNine.Style.Add("font-size", "13px");
                                        }
                                        lblSaldoNine.Text = FormateaNumero(saldoPuntosNine.ToString());
                                        lblPagoPuntosNine.Visible = true;
                                        lblPagoPuntosNineRecurso.Text = "Pago Puntos 9:";
                                        lblPagoPuntosNineRecurso.Visible = true;
                                        lblSaldoNine.Visible = true;
                                        lblSaldoNineRecurso.Text = "Saldo 9: ";
                                        lblSaldoNineRecurso.Visible = true;
                                        hrSaldo.Visible = true;
                                        
                                        
                                        ((CLIENTE9)Session["ClienteNine"]).PuntosPagados = objPago.Importe;

                                        RadioButton4.Checked = false;
                                        #region BT Quitar enviar a POS
                                        //btnEnviarPOS.Visible = false; 
                                        #endregion
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }
                                        nomcliente.Enabled = false;
                                    }
                                    else 
                                    { 
                                        //ACL. Se ha sobrepasado el limite de pagos nine.
                                        if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                                        {
                                            BtnCliente.Enabled = false;
                                            //TarjetaCliente.Enabled = false;
                                            ButClient.Enabled = false;
                                        }

                                        nomcliente.Enabled = false;
                                        this.txtPago.Text = limiteDouble.ToString();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Puntos9", "alert('Se ha sobrepasado el límite de pagos Nine. Solo puede pagar $" + limiteDouble + ", con Puntos Nine.');", true);
                                       // return;
                                        
                                    }
                                }

                            }
                        }
                    }
                    else if (RadioButtonlTipoPago.SelectedValue.ToString() == "T")
                    {
                        #region BT Quitar enviar a  POS
                        //this.btnBorrarCarrito.Enabled = false; 
                        #endregion
                    }

                    //}
                    //vaciamos controles
                    //  CargarPagosCarritos();

                    ((RadioButtonList)RadioButtonlTipoPago).ClearSelection();
                }

                SetPagado(idCarrito);
                CargaCarrito();
                PoneTotales();
                CargarPagosCarritos();
                GetClientesCarrito();

                divPendientePago.Style.Add("margin-left", "500px");
                //divTotal.Style.Add("margin-left", "569px");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        public void ActualizaCarritoLinea(string carrito, string ticket) {
            ClsCapaDatos objCapaDatos = new ClsCapaDatos();
            objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            try
            {
                objCapaDatos.ActualizaCarritoLinea(carrito, ticket);
            }
            catch (Exception ex)
            {
                log.Error("Error al añadir ticket");
            }
        
        
        }
        public void AniadeTicketCarrito(string carrito, string ticket)
        {

            ClsCapaDatos objCapaDatos = new ClsCapaDatos();
            objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            try
            {
                objCapaDatos.AniadeTicket(carrito, ticket);
            }
            catch (Exception ex)
            {
                log.Error("Error al añadir ticket");
            }

        }
        /// <summary>
        /// procedimeinto finalizar la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButFinalizarVenta_Click(object sender, EventArgs e)
        {
            try
            {

                //Page.Validate("validationiGroupSeleccionarTienda");

                //if (!Page.IsValid)
                //    return;

                String idTicket;
                CLIENTE9 C9;
                decimal gastosEnvio;
                string tipoEnvio = string.Empty;
                string idEnvioTienda = string.Empty;
                float importeTotal = 0;
                string idPedidoHermes = string.Empty;
                string mensajeOut = string.Empty;
                string idClienteFacturacion = string.Empty;
                bool bFacturacion = false;
                ClsCapaDatos capaDatos = new ClsCapaDatos();

                if (EstaPagado(!IdCarritoSession.HasValue ? IdCarritoQueryString.Value : IdCarritoSession.Value) && rbEnvioTienda.Checked)
                {
                    idEnvioTienda = GetTiendaEnvio(IdCarritoSession);
                    Response.Redirect("FinalizaCompraHERMES.aspx?IdCarrito=" + IdCarritoQueryString + "&TipoEnvio=" + idEnvioTienda + "&urlhermes=" + UrlHermesQueryString);
                }
                else if (EstaPagado(!IdCarritoSession.HasValue ? IdCarritoQueryString.Value : IdCarritoSession.Value) && rbEnvioDomicilio.Checked)
                {
                    Response.Redirect("FinalizaCompraHERMES.aspx?IdCarrito=" + IdCarritoQueryString + "&TipoEnvio=D" + "&urlhermes=" + UrlHermesQueryString);
                }

                if (IdCarritoSession == null)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CARRITOENV", "alert('El Carrito que has intentado finalizar ya esta registrado en el sistema.);", true);
                    Response.Redirect(UrlHermesQueryString, true);
                }

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito;
                if (IdCarritoSession != null)
                    idCarrito = Int64.Parse(IdCarritoSession.ToString());
                else
                    idCarrito = Int64.Parse(IdCarritoQueryString.ToString());

                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.Terminal = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
                objVenta.FechaSesion = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString());
                Application.Lock();

                C9 = new CLIENTE9();
                if (Session["ClienteNine"] != null) { C9 = (CLIENTE9)Session["ClienteNine"]; }
                //ACL. 08-07.2014. Le pasamos el valor de comentarios en el objeto venta, que será luego
                //el que se pase en el ticket.

                string idTiendaEnvio = string.Empty;
                if (gvTiendasEnvio.SelectedDataKey != null)
                    idTiendaEnvio = gvTiendasEnvio.SelectedDataKey.Values[0].ToString();
                else
                    idTiendaEnvio = GetTiendaEnvio(IdCarritoSession);
                //ACL.SE HA COMENTADO EN EL ASPX, el campo envio express
               /* if (rbEnvioDomicilio.Checked && chkEnvioExpress.Checked)
                
                {
                    gastosEnvio = GastosEnvioAppSettings;
                    tipoEnvio = "D";
                }*/
                //else if (rbEnvioDomicilio.Checked && !chkEnvioExpress.Checked)
                if (rbEnvioDomicilio.Checked)
                {
                    gastosEnvio = 0;
                    tipoEnvio = "D";
                }
                else
                {
                    gastosEnvio = 0;
                    tipoEnvio = idTiendaEnvio;
                }

                float.TryParse(lblTotalPagar.Text.Replace("$", ""), out importeTotal);
                DataView dvVenta;
                
                
              /*  if (Session["EsDirectivo"] != null)
                {
                    if (Session["EsDirectivo"].ToString() == "1")
                    {
                        AVE_CarritoObtenerDirec.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                        dvVenta = (DataView)AVE_CarritoObtenerDirec.Select(DataSourceSelectArguments.Empty);
                    }
                    else
                    {
                        AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                        dvVenta = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
                    }
                }
                else
                {*/
                    AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                    dvVenta = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
                /*}*/
                DataSet dsFormasPago = GetFormasPago();
                if (rbFacturaSi.Checked)
                {
                    idClienteFacturacion = GetClienteFacturacion(Session["IdCarrito"].ToString());
                    bFacturacion = true;
                }
                string strEmail = this.txtemail.Text;
                string strNombre = this.txtNomCli.Text;
               
                /// enviar xml a hermes antes de ejecutar la venta, si da error, return;
                idPedidoHermes = HermesXMLDocumentBL.EnviarXMLHermes(dvVenta, dsFormasPago, IdCarritoSession.Value, Contexto.IdTienda, idTiendaEnvio, gastosEnvio, importeTotal, strEmail, strNombre, TxtComentarios.Text, IdAlmacenCentralAppSettings, idClienteFacturacion, ref mensajeOut);
                

                /// -- Controlar respuesta hermes, si ok --> ejecuta venta y actualiza carrito con IdPedidoHermes, si no ok return
                if (idPedidoHermes == string.Empty)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    string script = "Error en pedido.Consulte pie de página para ver detalles";
                    cs.RegisterStartupScript(this.GetType(), "correcto", "confirm('" + script + "');", true);

                    ((MasterPageHermes)this.Master).actualizarLabel(mensajeOut.Substring(8));

                    if (rbEnvioDomicilio.Checked)
                    {
                        int requiereFactura = GetFacturacion(IdCarritoQueryString);

                        if (requiereFactura == 1)
                        {
                            cmdConfirmarEntregaFacturacion.Visible = true;
                            lblEntregaConfirmadaFacturacion.Visible = false;
                        }
                        else 
                        {
                            cmdConfirmarEntrega.Visible = true;
                            lblEntregaConfirmada.Visible = false;
                        }
                    }

                    BtnCliente.Visible = false;
                    CLiente.Visible = false;
                    CLiente.Style.Add("display", "none");

                    return;
                }

                capaDatos.ActualizarCarritoPedidoHermes(Convert.ToInt32(idCarrito), idPedidoHermes);

                string comentarios = " [MODALIA] - " + idPedidoHermes;

                string strEntorno = "TR";
                string strWS = "";
                idTicket = objVenta.EjecutaVenta(idCarrito, C9, AVE.Contexto.FechaSesion, comentarios, gastosEnvio, ref strWS, strEntorno);
                //falta añadir el metodo para guardar el ticket en el AVE_CARRITO
                AniadeTicketCarrito(Convert.ToString(idCarrito), idTicket);
                ActualizaCarritoLinea(Convert.ToString(idCarrito), idTicket);

                //Guardamos el idTicket en la BD de hermes
                SetTicketEnHermes(idPedidoHermes, idTicket);

                if (strWS != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE9", "alert('" + strWS + ".);", true);

                Session["IdCarrito"] = null;
                Application.UnLock();

                FinalizaVenta obFventa;

                obFventa = new FinalizaVenta();

                obFventa.Ticket = idTicket;
                obFventa.cliente = Nombre.Text.ToString();
                obFventa.Entrega = String.Empty;

                Session["FVENTA"] = obFventa;
                Session["ClienteNine"] = null;
                Session["objCliente"] = null;
                Session["email"] = null;
                string urlFin = "~/FinalizaCompraHERMES.aspx?IdCarrito=" + IdCarritoQueryString + "&TipoEnvio=" + tipoEnvio + "&urlhermes=" + UrlHermesQueryString;
                Server.Transfer(urlFin);
                //Response.Redirect("~/FinalizaCompraHERMES.aspx?IdCarrito=" + IdCarritoQueryString + "&TipoEnvio=" + tipoEnvio + "&urlhermes=" + UrlHermesQueryString);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
        }

        private bool EstaPagado(int idCarrito)
        {
            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT EstadoCarrito FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito.ToString();

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.ToInt32(myCommand.ExecuteScalar()) == 0)
            {
                myConnection.Close();
                return false;
            }
            else if (Convert.ToInt32(myCommand.ExecuteScalar()) == 3)
            {
                return false;
            }
            else
            {
                myConnection.Close();
                return true;
            }
        }

        private bool EstaPagadoyNoFinalizado(int idCarrito)
        {
            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT EstadoCarrito FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito.ToString();

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            if (Convert.ToInt32(myCommand.ExecuteScalar()) == 3)//el 3 se refiere al estadoCarrito
            {
                return true;
            }
            else
            {
                myConnection.Close();
                return false;
            }
        }

        private void AsignarTiendaEnvioEntrega(string idTiendaSeleccionada)
        {
            ClsCapaDatos objCapaDatos = new ClsCapaDatos();
            objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

            DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

            entrega.idCarrito = Convert.ToInt32(IdCarritoQueryString);
            entrega.Nombre = string.Empty;
            entrega.Apellidos = string.Empty;
            entrega.Email = string.Empty;
            entrega.Direccion = string.Empty;
            entrega.NoInterior = string.Empty;
            entrega.NoExterior = string.Empty;
            entrega.Estado = string.Empty;
            entrega.Ciudad = string.Empty;
            entrega.Colonia = string.Empty;
            entrega.CodPostal = string.Empty;
            entrega.TelfMovil = string.Empty;
            entrega.TelfFijo = string.Empty;
            entrega.Referencia = string.Empty;
            entrega.tiendaEnvio = idTiendaSeleccionada;

            objCapaDatos.ActualizarEntregaTienda(entrega);
        }

        private void guardarEmailEntrega(string email, string tiendaSeleccionada)
        {
            ClsCapaDatos objCapaDatos = new ClsCapaDatos();
            objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

            DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

            entrega.idCarrito = Convert.ToInt32(IdCarritoQueryString);
            entrega.Nombre = string.Empty;
            entrega.Apellidos = string.Empty;
            entrega.Email = email;
            entrega.Direccion = string.Empty;
            entrega.NoInterior = string.Empty;
            entrega.NoExterior = string.Empty;
            entrega.Estado = string.Empty;
            entrega.Ciudad = string.Empty;
            entrega.Colonia = string.Empty;
            entrega.CodPostal = string.Empty;
            entrega.TelfMovil = string.Empty;
            entrega.TelfFijo = string.Empty;
            entrega.Referencia = string.Empty;
            entrega.tiendaEnvio = tiendaSeleccionada;

            objCapaDatos.ActualizarEntregaTienda(entrega);
        }

        protected void rbTienda_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int i = row.RowIndex;
            foreach (GridViewRow rw in gvTiendasEnvio.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != i)
                    {
                        RadioButton rd = rw.FindControl("rbTienda") as RadioButton;
                        rd.Checked = false;
                    }
                    else
                    {
                        gvTiendasEnvio.SelectRow(i);
                    }
                }
            }
        }

        protected void rbEnvioTienda_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnvioTienda.Checked)
            {
                SetGastosEnvio(IdCarritoSession, 0);
                divTiendasEnvio.Visible = true;
                GetTiendas();
                divPendientePago.Style.Add("margin-left", "400px");
                chkEnvioExpress.Visible = false;
            }
            else
            {
                divTiendasEnvio.Visible = false;
            }
        }

        protected void rbEnvioDomicilio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnvioDomicilio.Checked)
            {
                chkEnvioExpress.Visible = true;
                lblEnvioSeleccionado.Visible = false;
                lblEnvioSeleccionadoRecurso.Visible = false;
            }
            else
            {
                chkEnvioExpress.Visible = false;
            }
        }

        protected void chkEnvioExpress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnvioExpress.Checked)
            {
                SetGastosEnvio(IdCarritoSession, GastosEnvioAppSettings);
                divTiendasEnvio.Visible = false;
                divPendientePago.Style.Add("margin-left", "500px");
                PoneTotales();
            }
            else
            {//Controlamos la posicio de la div pendientePago 
                divPendientePago.Style.Add("margin-left", "400px");
            }
        }

        protected void rbFacturaSi_CheckedChanged(object sender, EventArgs e)
        {
            SetFacturacion(IdCarritoQueryString.ToString(), true);
        }

        protected void rbFacturaNo_CheckedChanged(object sender, EventArgs e)
        {
            SetFacturacion(IdCarritoQueryString.ToString(), false);
        }

        protected void LstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nomcliente.Text = LstClientes.SelectedItem.Text;
                GetClienteActual(LstClientes.SelectedValue.ToString());
                CalculoPromocion_CarritoCliente();
                CargaCarrito();
                PoneTotales();
                CargarPagosCarritos();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        //protected void optEntregaOtraUbicacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    PoneTotales();
        //    // pnlOtraObicacion.Visible = optEntregaOtraUbicacion.Checked;
        //}

        protected void cmdConfirmarEntrega_Click(object sender, EventArgs e)
        {

            try
            {

                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

                entrega.idCarrito = (int)Session["IdCarrito"];
                entrega.Nombre = txtOUNombre.Text;
                entrega.Apellidos = txtOUApellidos.Text;
                entrega.Email = txtOUemail.Text;
                entrega.Direccion = txtOUDireccion.Text;
                entrega.NoInterior = txtOUNoInterior.Text;
                entrega.NoExterior = txtOUNoExterior.Text;
                entrega.Estado = cboOUEstado.SelectedItem.Text;
                entrega.Ciudad = txtOUCiudad.Text;
                entrega.Colonia = txtOUColonia.Text;
                entrega.CodPostal = txtOUCodigoPostal.Text;
                entrega.TelfMovil = txtOUTelfCelular.Text;
                entrega.TelfFijo = txtOUTelfFijo.Text;
                entrega.Referencia = txtOUReferenciaLlegada.Text;
                entrega.IdCliente = GetClienteCarrito(IdCarritoSession);

                objCapaDatos.ActualizarEntrega(entrega);

                cmdConfirmarEntrega.Visible = false;
                lblEntregaConfirmada.Visible = true;
                BtnCliente.Visible = false;
                CLiente.Visible = false;

                BtnFinalizaVenta.Visible = true;

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }

        protected void cmdConfirmarEntregaFacturacion_Click(object sender, EventArgs e)
        {

            try
            {
               
                    ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                    objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                 if (this.rbEnvioDomicilio.Checked)
                 {
                    DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

                    entrega.idCarrito = (int)Session["IdCarrito"];
                    entrega.Nombre = txtOUNombreF.Text;
                    entrega.Apellidos = txtOUApellidosF.Text;
                    entrega.Email = txtOUemailF.Text;
                    entrega.Direccion = txtOUDireccionF.Text;
                    entrega.NoInterior = txtOUNoInteriorF.Text;
                    entrega.NoExterior = txtOUNoExteriorF.Text;
                    entrega.Estado = cboOUEstadoF.SelectedItem.Text;
                    entrega.Ciudad = txtOUCiudadF.Text;
                    entrega.Colonia = txtOUColoniaF.Text;
                    entrega.CodPostal = txtOUCodigoPostalF.Text;
                    entrega.TelfMovil = txtOUTelfCelularF.Text;
                    entrega.TelfFijo = txtOUTelfFijoF.Text;
                    entrega.Referencia = txtOUReferenciaLlegadaF.Text;
                    entrega.IdCliente = GetClienteCarrito(IdCarritoSession);

                    objCapaDatos.ActualizarEntrega(entrega);
                }
                DLLGestionVenta.Models.FACTURACION_CARRITO facturacion = new DLLGestionVenta.Models.FACTURACION_CARRITO();

                facturacion.idCarrito = (int)Session["IdCarrito"];
                facturacion.Nombre = txtOUNombreFacturacion.Text;
                facturacion.Rfc = txtOURfcFacturacion.Text;
                facturacion.Direccion = txtOUDireccionFacturacion.Text;
                facturacion.NoInterior = txtOUNoInteriorFacturacion.Text;
                facturacion.NoExterior = txtOUNoExteriorFacturacion.Text;
                facturacion.Estado = cboOUEstadoFacturacion.SelectedItem.Text;
                facturacion.Ciudad = txtOUCiudadFacturacion.Text;
                facturacion.Colonia = txtOUColoniaFacturacion.Text;
                facturacion.CodPostal = txtOUCodigoPostalFacturacion.Text;
                facturacion.TelfMovil = "";
                facturacion.TelfFijo = "";
                facturacion.Pais = "";

                objCapaDatos.ActualizarEntregaFacturacion(facturacion);

                cmdConfirmarEntregaFacturacion.Visible = false;
                lblEntregaConfirmadaFacturacion.Visible = true;
                BtnCliente.Visible = false;
                CLiente.Visible = false;

                BtnFinalizaVenta.Visible = true;

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }



        protected void BC9_Click(object sender, EventArgs e)
        {
            this.BuscarClienteNine(nomcliente.Text);
        }

        private void CalculoPromocion_Carrito()
        {

            try
            {
                DLLGestionVenta.ProcesarVenta objVenta;
                String rMEnsaje = String.Empty;

                if (Session["IdCarrito"] != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();

                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    rMEnsaje = objVenta.GetObjCarritoPromocion(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion, true);

                    if (rMEnsaje.Length > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMEnsaje + "');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void CalculoPromocion_Carrito_Deleted()
        {
            try
            {

                DLLGestionVenta.ProcesarVenta objVenta;
                String rMEnsaje = String.Empty;

                if (Session["IdCarrito"] != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();

                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    rMEnsaje = objVenta.GetObjCarritoPromocionClientes(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion, true);

                    if (rMEnsaje.Length > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMEnsaje + "');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void CalculoPromocion_CarritoCliente()
        {

            try
            {
                DLLGestionVenta.ProcesarVenta objVenta;
                String rMEnsaje = String.Empty;

                if (Session["IdCarrito"] != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();

                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    rMEnsaje = objVenta.GetObjCarritoPromocionClientes(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion, true);

                    if (rMEnsaje.Length > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMEnsaje + "');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void ddlSelecionarPromocion_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                String IdPromocion;
                String IdCarritoDetalle;
                String rMensaje = String.Empty;
                DropDownList ddllist;
                DLLGestionVenta.ProcesarVenta v;


                DropDownList drop = (DropDownList)sender;

                foreach (GridViewRow row in gvCarrito.Rows)
                {
                    ddllist = (DropDownList)row.FindControl("ddlSelecionarPromocion");

                    if (drop.ClientID == ddllist.ClientID)
                    {
                        IdPromocion = drop.SelectedValue.ToString();
                        IdCarritoDetalle = gvCarrito.DataKeys[row.RowIndex].Values["id_carrito_detalle"].ToString();
                        objVenta = new DLLGestionVenta.ProcesarVenta();
                        objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                        rMensaje = objVenta.GetObjCarritoPromocion(Int64.Parse(Session["idCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion, Int32.Parse(IdPromocion), int.Parse(IdCarritoDetalle), true);
                        CargaCarrito();
                        PoneTotales();
                        CargarPagosCarritos();
                        ValidarCargaCliente();

                        if (rMensaje.Length > 1)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMensaje + "');", true);
                            return;
                        }

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private bool GetNivelCliente(string idCliente, string IdTienda,int idCarrito) {
            bool EsDirectivo = false;
            int result = 0;
            double dto = 0;
            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            try
            {
                if (idCliente != "")
                {
                    dto = objDatos.validarDtoCliente(Convert.ToInt32(idCliente), IdTienda);
                    if (dto > 0)
                    {
                        Session["DTODirectivo"] = dto;
                        result = objDatos.aplicaDtoDirectivo(idCarrito, dto);
                        if (result > 0) EsDirectivo = true;
                    }
                    else Session["DTODirectivo"] = null;
                }
                else Session["DTODirectivo"] = null;
            }
            catch (Exception ex) {
                log.Error("Error al obtener si el cliente es directivo:" + ex.Message.ToString());
            }
            
            return EsDirectivo;
        }
        private void GetClienteActual(CLIENTE9 cliente)
        {
            try
            {

                Nombre.Text = cliente.Cliente;
                Email.Text = cliente.Email.ToString();
                if (!string.IsNullOrEmpty(cliente.Email.ToString()))
                    txtemail.Text = cliente.Email.ToString();
                Shoelover.Text = cliente.NivelActual;

                txtOUemail.Text = cliente.Email.ToString();

                nomcliente.Text = cliente.Cliente;
                hidIdCliente.Value = cliente.Id_Cliente.ToString();

                CLiente.Visible = true;
                RadioButton1.CssClass = "Ocultarcontrol";
                LstClientes.CssClass = "ocul1";
                nomcliente.CssClass = "visi1";
                BtnCliente.Visible = true;

                if (cliente.NivelActual.ToString().Length > 0)
                {
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                }
                else
                {
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                }

                if (cliente.Empleado_cliente != null)
                {
                    if (cliente.Empleado_cliente.EsEmpleado && float.Parse(ViewState["ImportePagar"].ToString()) != 0)
                    {
                        RadioButton1.CssClass = "VisibleControl";
                        RadioButton1.Enabled = false;
                        RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        HiddenTipoCliente.Value = "E";
                        if (cliente.Empleado_cliente.NumNotaempleado >= cliente.Empleado_cliente.NotaEmpleadoGastadas)
                        {
                            RadioButton1.Enabled = true;
                            RadioButton1.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void GetClienteActual(string sCliente)
        {

            try
            {
                
                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
               
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                List<CLIENTE9> objclient = objVenta.GetCliente(sCliente, Contexto.FechaSesion, idCarrito, Contexto.IdTienda);


                if (objclient.Count > 0)
                {
                    if (objclient.Count == 1)
                    {
                        
                        Nombre.Text = objclient[0].Cliente;

                        Email.Text = objclient[0].Email.ToString();
                        this.txtNomCli.Text = objclient[0].Cliente;
                        if (!string.IsNullOrEmpty(objclient[0].Email.ToString()))
                            txtemail.Text = objclient[0].Email.ToString();
                        Shoelover.Text = objclient[0].NivelActual;
                        
                       
                        //Session["ClienteNine"] = null;

                        //                        nomcliente.Text = objclient[0].Id_Cliente.ToString();
                        nomcliente.Text = objclient[0].Cliente;
                        hidIdCliente.Value = objclient[0].Id_Cliente.ToString();
                       


                        CLiente.Visible = true;
                        RadioButton1.CssClass = "Ocultarcontrol";
                        LstClientes.CssClass = "ocul1";
                        nomcliente.CssClass = "visi1";
                        BtnCliente.Visible = true;
                        if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
                        if (objclient[0].NivelActual.ToString().Length > 0)
                        {
                                if (Session["TiendaCamper"].ToString() == "0") RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                                else RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        }
                        else
                        {
                            RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        }

                        if (objclient[0].NumTarjeta != "" && Session["ClienteNine"] == null)
                        {
                            if (Session["TiendaCamper"].ToString() == "0") cargaClienteNineSesion(objclient[0].NumTarjeta.ToString());
                            else Session["objCliente"] = objclient[0];
                        }
                        else
                        {
                            Session["objCliente"] = objclient[0];
                        }

                        if (objclient[0].Empleado_cliente != null)
                        {
                            if (objclient[0].Empleado_cliente.EsEmpleado)
                            {

                                //ACL 04/05/2016. SI ES UN EMPLEADO, BLOQUEAMOS QUE PUEDAN MODIFICAR EL EMAIL.
                                txtemail.Enabled = false;
                                //acl se mostrara o no dependiendo del valor de la clavee
                                if ((System.Configuration.ConfigurationManager.AppSettings["NotaEmpleado"].ToString().Trim()) != String.Empty)
                                {
                                    RadioButton1.CssClass = "VisibleControl";
                                    RadioButton1.Enabled = false;
                                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                                    HiddenTipoCliente.Value = "E";
                                    if (objclient[0].Empleado_cliente.NumNotaempleado >= objclient[0].Empleado_cliente.NotaEmpleadoGastadas)
                                    {
                                        this.lblAlerta.Visible = false;
                                        this.lblAlerta.InnerText = "";
                                        RadioButton1.Enabled = true;
                                        RadioButton1.Focus();
                                    }
                                    else
                                    {
                                        
                                        this.lblAlerta.Visible = true;
                                        this.lblAlerta.InnerText ="Nº Máximo de Notas Superada. Gastadas " +  objclient[0].Empleado_cliente.NotaEmpleadoGastadas + " de " + objclient[0].Empleado_cliente.NumNotaempleado;
                                    }
                                }
                                //
                            }
                            else 
                            {    
                                this.lblAlerta.Visible = false;
                                this.lblAlerta.InnerText="";
                                txtemail.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        hidIdCliente.Value = "";
                        CLIENTE9 objclientAux;
                        objclientAux = new CLIENTE9();
                        objclientAux.Id_Cliente = -1;
                        objclientAux.Cliente = "";
                        HiddenTipoCliente.Value = "9";
                        RadioButton1.CssClass = "Ocultarcontrol";
                        objclient.Insert(0, objclientAux);
                        LstClientes.DataTextField = "Cliente";
                        LstClientes.DataValueField = "ID_Cliente";
                        LstClientes.DataSource = objclient;
                        LstClientes.DataBind();
                        nomcliente.CssClass = "ocul1";
                        RadioButton1.CssClass = "VisibleControl";
                        LstClientes.CssClass = "visi1";
                        RadioButton1.CssClass = "Ocultarcontrol";
                        Nombre.Text = "";
                        Email.Text = "";
                        Shoelover.Text = "";
                        nomcliente.Text = "";
                        CLiente.Visible = false;
                        BtnCliente.Visible = false;
                        RadioButton1.CssClass = "Ocultarcontrol";
                        RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");

                    }
                }
                else
                {
                    log.Info("no se ha encontrado cliente " + sCliente);
                    Nombre.Text = "";
                    Email.Text = "";
                    Shoelover.Text = "";
                    nomcliente.Text = "";
                    CLiente.Visible = false;
                    RadioButton1.CssClass = "Ocultarcontrol";
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", "alert('No se han encontrado coincidencias.');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void CargarEstadoProceso()
        {

            //Pruebas, cambio de color en texto, simulando los pasos de carrito-pago-entrega

            if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_PAGO)
            {
                ValidarCargaCliente();

                int requiereFacturacion = GetFacturacion(IdCarritoQueryString);

                if (requiereFacturacion == -1)
                {
                    rbFacturaSi.Checked = false;
                    rbFacturaNo.Checked = false;
                }
                else
                {
                    if (requiereFacturacion == 1)
                    {
                        rbFacturaSi.Checked = true;
                    }
                    else
                    {
                        rbFacturaNo.Checked = true;
                    }
                }

                divRequiereFactura.Visible = true;
                divTotal.Visible = true;
                divFormaPagoCliente.Visible = true;
                divSeleccionEnvio.Visible = true;
                //optEntregaOtraUbicacion.Visible = true;
                hrPendientePago.Visible = false;
                divDatosDelpago.Visible = true;
                Label19.Text = "Total Pendiente";

                txtemail.Text = GetEmail(IdCarritoQueryString.ToString());
                if (txtemail.Text == "") {
                    if (this.Email.Text != "")
                    {
                        this.txtemail.Text = this.Email.Text;

                    }
                    else {
                        if (Session["ClienteNine"] != null)
                        {

                            CLIENTE9 objCli = (CLIENTE9)Session["ClienteNine"];
                            this.txtemail.Text = objCli.Email.ToString();
                        }
                        else
                        {
                            if (Session["objCliente"] != null)
                            {
                                CLIENTE9 objCli = (CLIENTE9)Session["objCliente"];
                                this.txtemail.Text = objCli.Email.ToString();
                            }
                            
                        }
                    
                    }
                                    
                }

                float totalPendiente;

                if (float.TryParse(TotPendiente.Text.Replace("$", ""), out totalPendiente) && totalPendiente <= 0)
                {
                    if (rbEnvioTienda.Checked && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        if (requiereFacturacion == 1)
                        {
                            BtnContinuaPago.Visible = true;
                            BtnFinalizaVenta.Visible = false;
                        }
                        else
                        {
                            BtnContinuaPago.Visible = false;
                            BtnFinalizaVenta.Visible = true;
                        }
                       
                        
                    }
                    else if (EstaPagado(!IdCarritoSession.HasValue ? IdCarritoQueryString.Value : IdCarritoSession.Value) && EstadoProcesoSession != SESSION_ESTADO_PROCESO_INI)
                    {
                        BtnContinuaPago.Visible = false;
                        BtnFinalizaVenta.Visible = true;
                        btnCancelaVenta.Visible = false;
                    }
                    else
                    {
                        BtnContinuaPago.Visible = true;
                        BtnFinalizaVenta.Visible = false;
                    }
                }
                else
                {
                    BtnContinuaPago.Visible = false;
                }

                divPago.Attributes["class"] = "pasoActivo";
                divProductosCarrito.Attributes["class"] = "pasoInactivo";
                divEntrega.Attributes["class"] = "pasoInactivo";

            }
            if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_ENTREGA)
            {
                divRequiereFactura.Visible = false;
                divDetalle.Visible = false;
                divTotal.Visible = false;
                //optEntregaOtraUbicacion.Visible = false;
                divSeleccionEnvio.Visible = false;
                divFormaPagoCliente.Visible = true;
                divDireccionEntrega.Visible = true;
                labelDirectivo.Visible = false;
                RadioButtonlTipoPago.CssClass = "Ocultarcontrol";
                TxtComentarios.Visible = false;
                divDatosDelpago.Visible = false;
                btnCancelaVenta.Visible = false;
                BtnContinuaPago.Visible = false;

                brBtnCliente.Visible = false;
                brlabelDirectivo.Visible = false;

                if (rbEnvioDomicilio.Checked)
                {
                    //rqfComentarios.Enabled = true;
                    //rqfEmail.Enabled = true;
                    rqfNombreCliente.Enabled = true;
                    LabelNom.Text = "Enviar a Nombre de *";

                    //divTiendasEnvio.Visible = false;
                    divFormaPagoCliente.Visible = true;
                    divDatosCliente.Visible = true;

                    int requiereFactura = GetFacturacion(IdCarritoQueryString);

                    if (requiereFactura == 1)
                    {
                        divOtraObicacion.Visible = false;
                        divOtraObicacionFacturacion.Visible = true;
                        divUbicacionFacturacion.Visible = true;
                       
                    }
                    else
                    {
                        divOtraObicacion.Visible = true;
                        divOtraObicacionFacturacion.Visible = false;
                        divUbicacionFacturacion.Visible =true;
                      
                        
                    }

                    BtnFinalizaVenta.Visible = false;

                    divcliente.Style.Add("padding-left", "200px");

                    CLiente.Visible = false;
                    lblNombreNumeroCliente.Visible = false;
                    nomcliente.Visible = false;
                    LstClientes.Visible = false;
                    BtnCliente.Visible = false;
                    brNombreNumeroCliente.Visible = false;
                    brComentarios.Visible = false;
                    brEmail.Visible = false;
                    txtemail.Visible = false;
                    lblEmail.Visible = false;

                    CargaEntrega();

                    if (EmailSession != null)
                        txtOUemail.Text = EmailSession;
                    else
                        txtOUemail.Text = txtemail.Text;

                }
                else
                {
                    int requiereFactura = GetFacturacion(IdCarritoQueryString);

                    if (requiereFactura == 1)
                    {
                        divFormaPagoCliente.Visible = true;
                        divDatosCliente.Visible = false;
                        divUbicacionFacturacion.Visible = false;

                    

                        if (requiereFactura == 1)
                        {
                            divOtraObicacion.Visible = false;
                            divOtraObicacionFacturacion.Visible = true;
                            divUbicacionFacturacion.Visible = false;
                        
                       
                        }
                   

                        BtnFinalizaVenta.Visible = false;

                        divcliente.Style.Add("padding-left", "200px");
                        CLiente.Visible = false;
                        lblNombreNumeroCliente.Visible = false;
                        nomcliente.Visible = false;
                        LstClientes.Visible = false;
                        BtnCliente.Visible = false;
                        brNombreNumeroCliente.Visible = false;
                        brComentarios.Visible = false;
                        brEmail.Visible = false;
                        txtemail.Visible = false;
                        lblEmail.Visible = false;
                        CargaDatosFacturacion();

                        if (EmailSession != null)
                            txtOUemail.Text = EmailSession;
                        else
                            txtOUemail.Text = txtemail.Text;

                    }
                    else
                    {
                     BtnFinalizaVenta.Visible = true;

                    }

               
                }

                divPago.Attributes["class"] = "pasoInactivo";
                divProductosCarrito.Attributes["class"] = "pasoInactivo";
                divEntrega.Attributes["class"] = "pasoActivo";
            }

        }

        #region BT Quitar enviar a  POS
        //protected void btnBorrarCarrito_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
        //        string carrito = Convert.ToString(Session["IdCarrito"]);
        //        //ACL-09-07-2014.INICIO. Si se carga un carrito desde BD, y se desea cancelar
        //        //se tiene que borrar de base de datos. Aunque lo debería hacer para todos los pedidos
        //        // una vez que los mete en session y los mete 
        //        ClsCapaDatos Bdatos = new ClsCapaDatos();

        //        Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
        //        bool res = Bdatos.EliminaCarritoBD(carrito, (string)Contexto.IdEmpleado, idMaquina);
        //        //ACL.FIN
        //        InicializarVariablesSesion();
        //        //para cancelar todas las operaciones de cliente 9.
        //        Comun.CheckPending();

        //        Response.Redirect("~/Inicio.aspx", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //    return;
        //} 
        #endregion

        //Se obtienen todas las tiendas de esa empresa
        private void GetTiendas()
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            DataSet dsTiendas;
            SqlDataAdapter adapter;
            int idEmpresa;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            //Obtenemos IdEmpresa para sacar las tiendas de ese grupo
            string sql = "SELECT IdEmpresa FROM TIENDAS WHERE IdTienda = '" + Contexto.IdTienda + "'";

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);
            idEmpresa = Convert.ToInt32(myCommand.ExecuteScalar());

            //ACL.23-10-2014. En la lista de tienda, no se sacan las tiendas de Modalia, T-09999, T-0270 y las que son de tipo Almacen
            //ACL.27-10-2014, se añade el campo observaciones para que lo muestre en vez del idTienda y se ordena la lista por nombre tienda.
            string strSql = "SELECT IdTienda,Observaciones, ( CCAA +' - '+ Localidad) Localidad, Direccion FROM Tiendas WHERE IdEmpresa = " + idEmpresa + " AND Tiendas_Activo = 1 and IdTienda not in('MO-1','T-0270','T-9999') ";
            strSql = strSql + " and IdTienda not in(select grude.Id_Tienda from GRUPOS_TIENDAS gruti inner join grupos_detalles grude on gruti.Id_Grupo = grude.Id_Grupo ";
            strSql = strSql + " inner join tiendas ti on ti.IdTienda= grude.Id_Tienda where NombreGrupo ='ALMACEN') ";
            strSql = strSql + " ORDER BY Observaciones collate Latin1_General_CI_AS ";
            myCommand = new SqlCommand(strSql, myConnection);

            dsTiendas = new DataSet();
            adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(dsTiendas, "Tiendas");
            myConnection.Close();

            gvTiendasEnvio.DataSource = dsTiendas;
            gvTiendasEnvio.DataBind();
        }


        private DataSet GetLineasCarrito(string idCarrito)
        {
            DataSet dsLineasCarrito;
            SqlCommand myCommand;
            SqlDataAdapter adapter;
            SqlConnection myConnection;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT * FROM AVE_CARRITO_LINEA WHERE Id_Carrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            adapter = new SqlDataAdapter(myCommand);
            dsLineasCarrito = new DataSet();

            adapter.Fill(dsLineasCarrito);

            myConnection.Close();

            return dsLineasCarrito;

        }

        private void pruebaConexionHermes()
        {
            SqlConnection myConnection;
            SqlCommand myCommand;
            decimal gastosEnvio;

            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hermes"].ToString();

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["Cn_Hermes"].ToString();

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
        }

        private decimal GetGastosEnvio(int? idCarrito)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            decimal gastosEnvio;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT GastosEnvio FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                gastosEnvio = 0;
                return gastosEnvio;
            }
            else
            {
                gastosEnvio = Convert.ToDecimal(myCommand.ExecuteScalar());
                myConnection.Close();
                return gastosEnvio;
            }
        }

        private int GetFacturacion(int? idCarrito)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string requiereFacturacion;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT RequiereFacturacion FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return -1;
            }
            else
            {
                requiereFacturacion = myCommand.ExecuteScalar().ToString();
                
                myConnection.Close();

                if (requiereFacturacion.Equals("True"))
                {
                    return 1;
                }
                else {
                    return 0;
                }
            }
        }

        private void SetGastosEnvio(int? idCarrito, decimal gastosEnvio)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "UPDATE AVE_CARRITO SET GastosEnvio = " + gastosEnvio + " WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            myCommand.ExecuteScalar();

            myConnection.Close();
        }

        private void SetPagado(Int64 idCarrito)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "UPDATE AVE_CARRITO SET EstadoCarrito = 3  WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            myCommand.ExecuteScalar();

            myConnection.Close();
        }

        private void SetTicketEnHermes(string idPedidoHermes, string idTicket)
        {

            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hermes"].ToString();

                string sql = "UPDATE PEDIDO SET numero_ticket = '" + idTicket + "' WHERE NUMERO = '" + idPedidoHermes + "'";

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);

                myCommand.ExecuteScalar();

                myConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static xmldatFacturacion getDataRFC(string idRFC)
        {
            xmldatFacturacion data = new xmldatFacturacion();
            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

            DataSet Ds = objDatos.DatosRFC(idRFC);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                data.RFC = Ds.Tables[0].Rows[0]["RFC"].ToString();
                data.nombre = Ds.Tables[0].Rows[0]["Nombre"].ToString();
                data.direccion = Ds.Tables[0].Rows[0]["Direccion"].ToString();
                data.ciudad = Ds.Tables[0].Rows[0]["Poblacion"].ToString();
                data.colonia = Ds.Tables[0].Rows[0]["Barriada"].ToString();
                data.estado = Ds.Tables[0].Rows[0]["Provincia"].ToString();
                data.cp = Ds.Tables[0].Rows[0]["CodPostal"].ToString();
                data.telefono = Ds.Tables[0].Rows[0]["Telefono"].ToString();
                data.movil = Ds.Tables[0].Rows[0]["Movil"].ToString();
                data.interior = Ds.Tables[0].Rows[0]["Puerta"].ToString();
                data.exterior = Ds.Tables[0].Rows[0]["Portal"].ToString();

            }

            return data;

        }
        [WebMethod(EnableSession = true)]
        public static xmldatFacturacion getdatosRFC(string idRFC)
           
        {
            xmldatFacturacion oObject = getDataRFC(idRFC);
           

            return oObject;
        }
        private void SetFacturacion(string idCarrito, bool requiereFactura)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string requiere = string.Empty;

            if (requiereFactura)
            {
                requiere = "1";
            }
            else
            {
                requiere = "0";
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "UPDATE AVE_CARRITO SET RequiereFacturacion = " + requiere + "  WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            myCommand.ExecuteScalar();

            myConnection.Close();
        }

        private string GetDescripcionArticulo(string idArticulo)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string descripcion;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Descripcion FROM ARTICULOS WHERE IdArticulo = " + idArticulo;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            descripcion = myCommand.ExecuteScalar().ToString();

            myConnection.Close();
            return descripcion;
        }

        private string GetDescripcionTiendaEnvio(int? idCarrito)
        {

            SqlConnection myConnection = null;

            SqlCommand myCommandTienda = null;
            string tiendaSeleccionada = string.Empty;
            string observacionesTiendaSeleccionada = string.Empty;

            try
            {
                tiendaSeleccionada = GetTiendaEnvio(idCarrito);


                if (string.IsNullOrEmpty(tiendaSeleccionada))
                {
                    return string.Empty;
                }
                else
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                    myConnection = new SqlConnection(connectionString);
                    myConnection.Open();
                    string sqlTiendas = "SELECT Observaciones FROM Tiendas WHERE idTienda = '" + tiendaSeleccionada + "'";
                    myCommandTienda = new SqlCommand(sqlTiendas, myConnection);
                    object resultTienda = myCommandTienda.ExecuteScalar();
                    if (Convert.IsDBNull(resultTienda) || string.IsNullOrEmpty(Convert.ToString(resultTienda)))
                        observacionesTiendaSeleccionada = string.Empty;
                    else
                        observacionesTiendaSeleccionada = resultTienda.ToString();

                    return observacionesTiendaSeleccionada + " (" + tiendaSeleccionada + ")";
                }




            }
            finally
            {
                if (myConnection != null)
                    myConnection.Close();
            }

        }

        private string GetTiendaEnvio(int? idCarrito)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string tiendaSeleccionada = string.Empty;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT IdTiendaEnvio FROM AVE_CARRITO_ENTREGAS WHERE IdCarrito = " + idCarrito;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);

                object result = myCommand.ExecuteScalar();

                if (Convert.IsDBNull(result) || string.IsNullOrEmpty(Convert.ToString(result)))
                    tiendaSeleccionada = string.Empty;
                else
                    tiendaSeleccionada = result.ToString();

                return tiendaSeleccionada;
            }
            finally
            {
                if (myConnection != null)
                    myConnection.Close();
            }

        }

        private string GetEmail(string idCarrito)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string email = string.Empty;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT Email FROM AVE_CARRITO_ENTREGAS WHERE IdCarrito = " + idCarrito;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);

                object result = myCommand.ExecuteScalar();

                if (Convert.IsDBNull(result) || string.IsNullOrEmpty(Convert.ToString(result)))
                    email = string.Empty;
                else
                    email = result.ToString();

                return email;
            }
            finally
            {
                if (myConnection != null)
                    myConnection.Close();
            }

        }

        private int GetClienteCarrito(int? idCarrito)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            int id_cliente;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Id_cliente FROM AVE_CARRITO WHERE IdCarrito = " + idCarrito;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (string.IsNullOrEmpty(Convert.ToString(myCommand.ExecuteScalar())) || Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return 0;
            }
            else
            {
                id_cliente = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
                return id_cliente;
            }
        }

        private string GetIdProvincia(string provincia)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string idProvincia;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Id FROM N_PROVINCIAS WHERE Nombre = '" + provincia + "'";

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);


            if (string.IsNullOrEmpty(Convert.ToString(myCommand.ExecuteScalar())) || Convert.IsDBNull(myCommand.ExecuteScalar()))
            {
                myConnection.Close();
                return string.Empty;
            }
            else
            {
                idProvincia = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return idProvincia;
            }
        }

        //Metodo que oculta los campos que no son necesarios para el envio a domicilio
        private void OcultarCamposCliente()
        {

            DivTajeta.Visible = false;
            Divpuntos.Visible = false;
            CLiente.Visible = false;

        }

        private void AsignarUsuario()
        {
            //Asignamos usuario una vez ha hecho login

            string sql;
            
            
            ClsCapaDatos datos = new ClsCapaDatos();

            sql = "UPDATE AVE_CARRITO SET Usuario = " + Contexto.IdEmpleado + " WHERE IdCarrito = " + IdCarritoQueryString;
            

            datos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString(); ;
            datos.ActualizarSQL(sql);
        }

        protected void BtnSeguirComprando_Click(object sender, EventArgs e)
        {
            //hay que redireccionar a la web de HERMES para continuar comprando
            Response.Redirect(UrlHermesSession);
        }

        protected void BtnContinuaPago_Click(object sender, EventArgs e)
        {
            if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_INI)
                EstadoProcesoSession = SESSION_ESTADO_PROCESO_PAGO;

            else if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_PAGO)
            {
               /* if(Session["EsDirectivo"]!= null)
                    if(Session["EsDirectivo"].ToString()=="1")
                        validaPagoDirectivo(Session["DTODirectivo"].ToString());*/
                EstadoProcesoSession = SESSION_ESTADO_PROCESO_ENTREGA;
            }

            else if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_ENTREGA)
                EstadoProcesoSession = SESSION_ESTADO_PROCESO_FIN;
            else
                EstadoProcesoSession = SESSION_ESTADO_PROCESO_INI;

            CargarEstadoProceso();
        }
        private void validaPagoDirectivo(string dto)
        { 
        
            //acl. para directivos con un 100% de descuentos no se esta grabando ningun tipo de pago
            // y aunque el importe es 0 luego al finalizar la venta, hay que enviarle un tipo de pago al
            //WEB SERVICE DE HERMES, para THINK RETAIL.
            if(dto =="100")
            {
                    string sql;

                    ClsCapaDatos datos = new ClsCapaDatos();

                    sql = "INSERT INTO AVE_CARRITO_PAGOS(IdCarrito,TipoPago,TipoPagoDetalle,NumTarjeta,Importe,PagadoOK) values("+IdCarritoQueryString+",'EFECTIVO','','',0,1)";

                    datos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString(); ;
                    try
                    {
                        datos.ActualizarSQL(sql);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error al grabar el tipo de pago para directivo:" + ex.Message.ToString());
                    }
                }
        
        }
        private string GetClienteFacturacion(string idCarrito) {
            SqlConnection myConnection;
            SqlCommand myCommand;
            string result="";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();

                string strSql = "select id_Cliente from ave_carrito " +
                                " where IdCarrito = " + IdCarritoSession;
                myCommand = new SqlCommand(strSql, myConnection);
                var idCliente = myCommand.ExecuteScalar();
                result = (idCliente == null ? string.Empty : idCliente.ToString());
                
            }
            finally {
                if (myConnection != null)  myConnection.Close();
            }

            return result;
        }
        private DataSet GetFormasPago()
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            DataSet dsFormasPago;
            SqlDataAdapter adapter;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            string strSql = "SELECT TipoPago,TipoPagoDetalle, Importe FROM AVE_CARRITO_PAGOS WHERE IdCarrito = " + IdCarritoSession + " and PagadoOK=1";
            myCommand = new SqlCommand(strSql, myConnection);

            dsFormasPago = new DataSet();
            adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(dsFormasPago, "FormasPago");
            myConnection.Close();

            return dsFormasPago;
        }

        public string GetReferencia(string IdArticulo)
        {
            string referencia = string.Empty;
            SqlConnection myConnection;
            SqlCommand myCommand;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT CodigoAlfa FROM ARTICULOS WHERE IdArticulo = " + IdArticulo;

            myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                referencia = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return referencia;

            }
            catch (Exception ex)
            {
                if (myConnection != null) myConnection.Close();
                throw ex;
            }

        }

        public string getIdArticulo(string idLineaCarrito)
        {
            string idArticulo = string.Empty;
            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT idArticulo FROM AVE_CARRITO_LINEA WHERE id_Carrito_Detalle = " + idLineaCarrito;

            myConnection = new SqlConnection(connectionString);
            try
            {
                
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                idArticulo = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return idArticulo;
            }
            catch (Exception ex)
            {
                if (myConnection != null) myConnection.Close();
                throw ex;
            }
        }

        public string GetTiendaEnvio(string IdCarrito)
        {
            string idEnvioTienda = string.Empty;
            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT IdTiendaEnvio FROM ARTICULOS WHERE IdCarrito = " + IdCarrito;

            myConnection = new SqlConnection(connectionString);

            try
            {
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                idEnvioTienda = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return idEnvioTienda;

            }
            catch (Exception ex)
            {
                if (myConnection != null) myConnection.Close();
                throw ex;
            }

        }

        public void GetClienteCarritoFacturacion(int idCliente)
        {
            try
            {
                DataSet Ds;

                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                Ds = objCapaDatos.GetClienteCarritoFacturacion(idCliente);

                if(Ds.Tables[0].Rows.Count > 0)
                {

                txtOURfcFacturacion.Text = Ds.Tables[0].Rows[0]["CIF"].ToString();
                txtOUNombreFacturacion.Text = Ds.Tables[0].Rows[0]["Nombre"].ToString();
                txtOUDireccionFacturacion.Text = Ds.Tables[0].Rows[0]["Direccion"].ToString();
                txtOUColoniaFacturacion.Text = Ds.Tables[0].Rows[0]["Colonia"].ToString();
                txtOUNoInteriorFacturacion.Text = Ds.Tables[0].Rows[0]["NoInterior"].ToString();
                txtOUNoExteriorFacturacion.Text = Ds.Tables[0].Rows[0]["NoExterior"].ToString();
                txtOUCodigoPostalFacturacion.Text = Ds.Tables[0].Rows[0]["CodPostal"].ToString();
                txtOUCiudadFacturacion.Text = Ds.Tables[0].Rows[0]["Poblacion"].ToString();
                //acl.lo comento ya que esta comentado en el aspx
             //   txtOUTelfCelularFacturacion.Text = Ds.Tables[0].Rows[0]["Movil"].ToString();
                
                if (Ds.Tables[0].Rows[0]["Provincia"] != null)
                  cboOUEstadoFacturacion.SelectedValue = GetIdProvincia(Ds.Tables[0].Rows[0]["Provincia"].ToString());
//acl.lo comento ya que esta comentado en el aspx
              //  txtOUPaisFacturacion.Text = Ds.Tables[0].Rows[0]["Pais"].ToString();

                }

            }
            catch (Exception sqlEx)
            {
                throw sqlEx;
            }
        }

        public void GetClienteGeneral(int idCliente)
        {
            try
            {
                DataSet Ds;

                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                Ds = objCapaDatos.GetClienteGeneral(idCliente);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    int facturacion = GetFacturacion(IdCarritoQueryString);
                    if (facturacion == 0)
                    {
                        txtOUNombre.Text = Ds.Tables[0].Rows[0]["Nombre"].ToString();
                        txtOUApellidos.Text = Ds.Tables[0].Rows[0]["Apellidos"].ToString();
                        txtOUemail.Text = Ds.Tables[0].Rows[0]["email"].ToString();
                        txtOUDireccion.Text = Ds.Tables[0].Rows[0]["Direccion"].ToString();
                        txtOUCodigoPostal.Text = Ds.Tables[0].Rows[0]["CodPostal"].ToString();
                        txtOUTelfCelular.Text = Ds.Tables[0].Rows[0]["Telefono"].ToString();
                        txtOUTelfFijo.Text = Ds.Tables[0].Rows[0]["Telefono2"].ToString();
                        txtOUCiudad.Text = Ds.Tables[0].Rows[0]["Poblacion"].ToString();

                        if (Ds.Tables[0].Rows[0]["Provincia"] != null && Ds.Tables[0].Rows[0]["Provincia"].ToString().Trim() !="")
                            cboOUEstado.SelectedValue = GetIdProvincia(Ds.Tables[0].Rows[0]["Provincia"].ToString());
                    }
                    else if (facturacion == 1) 
                    {
                        txtOUNombreF.Text = Ds.Tables[0].Rows[0]["Nombre"].ToString();
                        txtOUApellidosF.Text = Ds.Tables[0].Rows[0]["Apellidos"].ToString();
                        txtOUemailF.Text = Ds.Tables[0].Rows[0]["email"].ToString();
                        txtOUDireccionF.Text = Ds.Tables[0].Rows[0]["Direccion"].ToString();
                        txtOUCodigoPostalF.Text = Ds.Tables[0].Rows[0]["CodPostal"].ToString();
                        txtOUTelfCelularF.Text = Ds.Tables[0].Rows[0]["Telefono"].ToString();
                        txtOUTelfFijoF.Text = Ds.Tables[0].Rows[0]["Telefono2"].ToString();
                        txtOUCiudadF.Text = Ds.Tables[0].Rows[0]["Poblacion"].ToString();

                        if (Ds.Tables[0].Rows[0]["Provincia"] != null && Ds.Tables[0].Rows[0]["Provincia"].ToString().Trim() != "")
                            log.Error("el valor de provincia es:"+ Ds.Tables[0].Rows[0]["Provincia"].ToString() );
                            cboOUEstadoF.SelectedValue = GetIdProvincia(Ds.Tables[0].Rows[0]["Provincia"].ToString());
                    }

                }

            }
            catch (Exception sqlEx)
            {
                throw sqlEx;
            }
        }

        private string GetTallaLineaCarrito(string idPedido)
        {
            string talla = string.Empty;
            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            string sql = "SELECT Talla FROM AVE_PEDIDOS WHERE IdPedido = " + idPedido;

            myConnection = new SqlConnection(connectionString);
            try
            {
               
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                talla = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return talla;

            }
            catch (Exception ex)
            {
                if (myConnection != null)  myConnection.Close();
                throw ex;
            }
        }

        protected void cvTiendaSeleccionada_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = gvTiendasEnvio.SelectedRow != null;
        }

        protected void gvTiendasEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idTiendaSeleccionada = string.Empty;
            string tiendaSeleccionada = string.Empty;

            //tiendaSeleccionada = ((System.Web.UI.WebControls.CheckBox)(gvTiendasEnvio.SelectedRow.Cells[0].Controls[1])).Text.ToString() + "(" + gvTiendasEnvio.SelectedDataKey.Value.ToString() + ")";
            //El texto obtenido aqui se guarda en BD. Con está linea en vez de guradar el id de la tienda se guardaba las observaciones mas el id. Lo que provocaba el error por que excedia el tamaño del campo en la BD (VARCHAR(10))
            if (gvTiendasEnvio.SelectedDataKey != null)
            {
                idTiendaSeleccionada = gvTiendasEnvio.SelectedDataKey.Values[0].ToString();
                tiendaSeleccionada = gvTiendasEnvio.SelectedDataKey.Values[1].ToString() + " (" + idTiendaSeleccionada + ")";
            }

            lblEnvioSeleccionadoRecurso.Visible = true;
            lblEnvioSeleccionadoRecurso.Text = "Tienda: ";
            lblEnvioSeleccionado.Visible = true;
            lblEnvioSeleccionado.Text = tiendaSeleccionada;
            AsignarTiendaEnvioEntrega(idTiendaSeleccionada);
            divTiendasEnvio.Visible = false;
        }

        protected void btnCerrarPopUp_Click(object sender, EventArgs e)
        {
            divTiendasEnvio.Visible = false;
        }

        protected void btnReseteaEntrega_Click(object sender, EventArgs e)
        {
            ReseteaEntrega();
            BtnCliente.Visible = false;
            CLiente.Visible = false;
            cmdConfirmarEntrega.Visible = true;
            lblEntregaConfirmada.Visible = false;
        }

        protected void btnReseteaEntregaFacturacion_Click(object sender, EventArgs e)
        {
            ReseteaEntregaFacturacion();
            BtnCliente.Visible = false;
            CLiente.Visible = false;
            cmdConfirmarEntrega.Visible = true;
            lblEntregaConfirmadaFacturacion.Visible = false;
        }

        protected void btnCancelaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                string idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                string carrito = Convert.ToString(Session["IdCarrito"]);
                //ACL-09-07-2014.INICIO. Si se carga un carrito desde BD, y se desea cancelar
                //se tiene que borrar de base de datos. Aunque lo debería hacer para todos los pedidos
                // una vez que los mete en session y los mete 
                ClsCapaDatos Bdatos = new ClsCapaDatos();
                string mensajeOut = string.Empty;
                string respuesta = HermesXMLDocumentBL.EnviarXMLHermesCancelacion(Convert.ToInt32(carrito), ref mensajeOut);

                StockTiendaBL stock = new StockTiendaBL();

                stock.cancelarReservaAC(IdCarritoSession.ToString(), null, Contexto.IdEmpleado, UserThinkRetailAppSettings, PassThinkRetailAppSettings, Contexto.IdTerminal, Contexto.IdTienda);

                Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                bool res = Bdatos.EliminaCarritoBD(carrito);
                
      
                //ACL.FIN
                InicializarVariablesSesion();
                //para cancelar todas las operaciones de cliente 9.
                Comun.CheckPending();

                Response.Redirect(UrlHermesQueryString, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return;
        }
        private void EliminaGastosEnvioCarrito()
        {
            int idArticulo;
            
            string talla = "0";
            
            string idCarrito = Convert.ToString(Session["IdCarrito"]);
            string referencia = "116303000560";
            



            ClsCapaDatos capaDatos = new ClsCapaDatos();
            SqlTransaction transaction = null;
            try
            {

                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                transaction = capaDatos.GetTransaction();


                //Comprobar siempre que el carrito existe
                capaDatos.ValidarCarrito(Convert.ToInt32(idCarrito), transaction);

                //obtener idarticulo buscando por referencia
                idArticulo = capaDatos.GetIdArticulo(referencia, transaction);
               
                //borramos pedido
                int result = capaDatos.EliminaArticulo(idArticulo.ToString(), idCarrito, transaction);

                if (result > 0) transaction.Commit();
                else transaction.Rollback();





            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                log.Error("Error al insertar gastos de Envío" + ex.Message, ex);

            }
            finally
            {
                if (capaDatos != null)
                    capaDatos.ReleaseTransaction();
            }
        
        
        
        
        
        }
        private void AñadirGastosEnvio()
        {
            int idArticulo;
            int idPedido;
            string talla = "SIN";
            int idLineaCarrito;
            string idCarrito = Convert.ToString(Session["IdCarrito"]);
            string referencia = "116303000560";
            float precioOriginal = 0;
            float precioActualizado = 0;
            int result = 0;


            

            ClsCapaDatos capaDatos = new ClsCapaDatos();
            SqlTransaction transaction = null;
            try
            {

                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                transaction = capaDatos.GetTransaction();

               
                    //Comprobar siempre que el carrito existe
                    capaDatos.ValidarCarrito(Convert.ToInt32(idCarrito), transaction);

                    //obtener idarticulo buscando por referencia
                    idArticulo = capaDatos.GetIdArticulo(referencia, transaction);
                    result =capaDatos.ValidarNuevo(idCarrito,idArticulo.ToString(), transaction);
                    if (result == 0)
                    {
                        capaDatos.getDatosEnvio(idArticulo.ToString(), ref precioOriginal, ref precioActualizado, transaction);

                        //insertar pedido
                        idPedido = capaDatos.InsertarPedido(idArticulo, talla, transaction);

                        //insertar linea carrito a ese idCarrito

                        idLineaCarrito = capaDatos.InsertarLineaCarrito(idArticulo, Convert.ToInt32(idCarrito), idPedido, talla, precioOriginal, precioActualizado, transaction);

                    }

                    transaction.Commit();
                    lblGastosEnvio.Visible = true;
                    lblGastosEnvio.Text = Convert.ToString(precioOriginal);

                    //Response.Redirect(Request.RawUrl);

                

            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                
                log.Error("Error al insertar gastos de Envío" + ex.Message, ex);
               
            }
            finally
            {
                if (capaDatos != null)
                    capaDatos.ReleaseTransaction();
            }
        }
        private void EliminaGastosBD()
        {
            string idCarrito = Convert.ToString(Session["IdCarrito"]);
            string referencia = "116303000560";
            bool result = false;
            ClsCapaDatos capaDatos = new ClsCapaDatos();
            
            try
            {
                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                EliminaGastosEnvioCarrito();
            }
            catch (Exception ex)
            {
                log.Error("Exception al borrar gastos Envío" + ex.Message.ToString());
            }
        }
        private void EliminarGastosEnvio()
        {
            if (ValidaGastosEnvio()) 
            {
                EliminaGastosBD();
                
               
                
            }
        }
        private bool ValidaGastosEnvio()
        {
            bool resul = false;
             string idCarrito = Convert.ToString(Session["IdCarrito"]);
            string referencia = "116303000560";
            ClsCapaDatos capaDatos = new ClsCapaDatos();

            try
            {

                capaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                resul=capaDatos.ValidaGastosEnvioCarrito(Convert.ToInt32(idCarrito), referencia);
            }
            catch (Exception ex)
            {
                log.Error("Exception al validar GastosEnvio" + ex.Message.ToString());
            }
            return resul;
        
        }

        protected void chkEnvio_CheckedChanged(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (chkEnvio.Checked)
                {
                    AñadirGastosEnvio();
                    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_INI)
                        EstadoProcesoSession = SESSION_ESTADO_PROCESO_PAGO;

                    CargarEstadoProceso();
                   
                    Response.Redirect(Request.RawUrl);
                   
                }
                else
                {
                    EliminarGastosEnvio();
                    if (EstadoProcesoSession == SESSION_ESTADO_PROCESO_INI)
                        EstadoProcesoSession = SESSION_ESTADO_PROCESO_PAGO;

                    CargarEstadoProceso();
                    

                    Response.Redirect(Request.RawUrl);
                    
                }
            }
        }


    }
}