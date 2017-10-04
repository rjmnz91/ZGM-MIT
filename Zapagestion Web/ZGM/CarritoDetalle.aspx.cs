using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.Services;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using DLLGestionVenta.Models;
using System.Web.UI.HtmlControls;
using CapaDatos;
using AjaxControlToolkit;

namespace AVE
{
    public partial class CarritoDetalle : CLS.Cls_Session
    {
        // MJM 27/05/2014 LOG
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string amt = "", ccNum = "", appidLbl = "", merchPago = "", correo = "", auth = "", operation = "";
        //

        DLLGestionVenta.ProcesarVenta objVenta;
        DataSet DsPromo;

        #region "Modificaciones Marcos"

        private void CargaEntrega()
        {

            try
            {
                if (Session["idCarrito"] != null)
                {
                    // refresh
                    cboOUEstado.DataBind();
                    //ACL.si se castea con (int)Session["idCarrito"], da una excepcion
                    int idCarrito = Convert.ToInt32(Session["idCarrito"]);

                    ClsCapaDatos objDatos = new ClsCapaDatos();
                    objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                    DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCarrito(idCarrito);
                    txtOUNombre.Text = entrega.Nombre;
                    txtOUApellidos.Text = entrega.Apellidos;
                    txtOUEmail.Text = entrega.Email;
                    txtOUDireccion.Text = entrega.Direccion;
                    txtOUNoInterior.Text = entrega.NoInterior;
                    txtOUNoExterior.Text = entrega.NoExterior;
                    cboOUEstado.SelectedValue = entrega.Estado;
                    txtOUCiudad.Text = entrega.Ciudad;
                    txtOUColonia.Text = entrega.Colonia;
                    txtOUCodigoPostal.Text = entrega.CodPostal;
                    txtOUTelfCelular.Text = entrega.TelfMovil;
                    txtOUTelfFijo.Text = entrega.TelfFijo;
                    txtOUReferenciaLlegada.Text = entrega.Referencia;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }


        private void CargaEntregaArt()
        {

            try
            {
                if (Session["idCarrito"] != null)
                {
                    // refresh
                    cboOUEstado.DataBind();
                    //ACL.si se castea con (int)Session["idCarrito"], da una excepcion
                    int idCarrito = Convert.ToInt32(Session["idCarrito"]);
                    PanelEntregasArt.Visible = true;

                    ClsCapaDatos objDatos = new ClsCapaDatos();
                    objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                    DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCarrito(idCarrito, idCarritoDetalle);
                    txtNombreArt.Text = entrega.Nombre;
                    txtApellidosArt.Text = entrega.Apellidos;
                    txtEmailArt.Text = entrega.Email;
                    txtDireccionArt.Text = entrega.Direccion;
                    txtInteriorArt.Text = entrega.NoInterior;
                    txtExteriorArt.Text = entrega.NoExterior;
                    ListEstadoArt.SelectedValue = entrega.Estado;
                    txtCiudadArt.Text = entrega.Ciudad;
                    txtColoniaArt.Text = entrega.Colonia;
                    txtCpArt.Text = entrega.CodPostal;
                    txtCelArt.Text = entrega.TelfMovil;
                    txtTelArt.Text = entrega.TelfFijo;
                    txtObservacionesArt.Text = entrega.Referencia;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
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

                //  RadioButtonlTipoPago.Items[0].Selected = false;
                //  RadioButtonlTipoPago.Items[1].Selected = false;
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



                CalculoPromocion_CarritoCliente();
                cargaCarrito();
                poneTotales();
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
                        RadioButton3.Enabled = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())));
                        objCliente.ParValido = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())));

                        //VALIDAMOS BOLSA5
                        objCliente.BolsaValido = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())));
                        RadioButton4.Enabled = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())));
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

                //  RadioButtonlTipoPago.Items[0].Selected = false;
                //  RadioButtonlTipoPago.Items[1].Selected = false;
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
                cargaCarrito();
                poneTotales();
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
                        RadioButton3.Enabled = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())));
                        objCliente.ParValido = (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())));

                        //VALIDAMOS BOLSA5
                        objCliente.BolsaValido = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())));
                        RadioButton4.Enabled = (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())));
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
                    nomcliente.Text = ((CLIENTE9)Session["ClienteNine"]).Cliente;
                    GetClientesCarrito();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "rmInsPagNine('1');", true);
                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("9"));
                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("N"));
                    ListItem item = new ListItem();
                    item.Text = "Cliente 9";
                    item.Value = "9";
                    tipPagos.Items.Add(item);
                    HiddenTipoCliente.Value = "9";
                }
                else
                {
                    if (Session["objCliente"] != null)
                    {
                        GetClienteActual((CLIENTE9)Session["objCliente"]);
                    }
                    else
                    {
                        GetClientesCarrito();
                    }
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


        protected void Page_Load(object sender, EventArgs e)
        {
            PanelEntregasArt.Visible = false;
            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
            if (Session["TiendaCamper"].ToString() == "1") this.imgCliente9.Visible = false;
            else imgCliente9.Visible = true;

            // bool EsDirectivo = false;
            int ArtiCarrito = 0;
            //ListItem itemE =(ListItem) RadioButtonlTipoPago.Items.FindByValue("E");

            if (checkPagoEfectivo() == 0) tipPagos.Items.Remove(tipPagos.Items.FindByValue("E"));
            //if (checkPagoEfectivo() == 0) Page.ClientScript.RegisterStartupScript(this.GetType(), "", "rmPagEfectivo();", true);

            lblHead.Text = lblHead.Text.ToUpper();
            
            if (HiddenTipoCliente.Value != "")
            {

                if (HiddenTipoCliente.Value == "E")
                {
                    if (checkPagoEfectivo() == 0) Page.ClientScript.RegisterStartupScript(this.GetType(), "", "rmInsPagEmpleado('1');", true);
                    //tipPagos.Items[2].Attributes.Add("class", "visi1"); 
                    // tipPagos.Items[3].Attributes.Add("class", "ocul1");
                }
                else if (HiddenTipoCliente.Value == "9")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "rmInsPagNine('1');", true);
                    //tipPagos.Items[2].Attributes.Add("class", "ocul1");
                    //tipPagos.Items[3].Attributes.Add("class", "visi1");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "rmPagAll();", true);
                    //tipPagos.Items[2].Attributes.Add("class", "ocul1");
                    //tipPagos.Items[3].Attributes.Add("class", "ocul1");
                }
            }
            else
            {
                //tipPagos.Items[2].Attributes.Add("class", "ocul1");
                //tipPagos.Items[3].Attributes.Add("class", "ocul1");
            }
            //Session["IdCarrito"] = 413;

            if (Session["tipoPago"] != null)
            {
                string uri = HttpContext.Current.Request.Url.AbsoluteUri;
                if(!uri.Contains("vta")){
                string[] parameters = uri.Split('?');
                string[] tempValues = parameters[1].Split('$');
                amt = tempValues[0]; ccNum = tempValues[1]; appidLbl = tempValues[2]; merchPago = tempValues[3]; correo = tempValues[4]; auth = tempValues[5]; operation = tempValues[6];
                Payment.setAuth(auth);
                Payment.setOperation(operation);
                Session["tipoPago"] = null;
                pagoTarjeta();
                }else{
                    ;
                }
            }

            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["IdCarrito"] != null)
                    {
                        ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());
                        var miMaster = (MasterPage)this.Master;
                        miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                        /* EsDirectivo = GetNivelCliente(hidIdCliente.Value, Contexto.IdTienda, Convert.ToInt32(Session["IdCarrito"].ToString()));
                         if (EsDirectivo)
                         {
                             Session["EsDirectivo"] = 1;
                             labelDirectivo.Text = "Descuento General Cliente: " + Session["DTODirectivo"].ToString() + ",00%";
                         }
                         else labelDirectivo.Text = "";*/

                        cargaCarrito();
                        poneTotales();
                        CargarPagosCarritos();
                        ValidarCargaCliente();
                    }
                    else // idcarrito == null
                    {
                        Response.Redirect("~/Inicio.aspx", true);
                        return;
                    }
                    ViewState["ImporteDescuentos"] = "0.0";
                    ViewState["SubTotal"] = "0.0";
                    ViewState["ImportePagar"] = "0.0";
                    ViewState["ImportePromociones"] = "0.0";
                    if (HiddenTipoCliente.Value.ToString() == "E" || HiddenTipoCliente.Value.ToString().Equals(""))
                    {
                        // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        //        tipPagos.Items[3].Attributes.Add("class", "ocul1");
                    }
                    //RadioButtonlTipoPago.Items[1].Attributes.Add("class", "Relleno1");
                    // RadioButtonlTipoPago.Attributes.Add("onclick", string.Format("HacerClickPonerTarjeta('{0}',{1});", RadioButtonlTipoPago.ClientID, RadioButtonlTipoPago.Items.Count));
                    tipPagos.Attributes.Add("onchange", string.Format("habilitaDiv('{0}',{1});", tipPagos.ClientID, tipPagos.Items.Count));
                }
                else
                {
                    string uri = HttpContext.Current.Request.Url.AbsoluteUri;
                    if(uri.Contains("denied"))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", "alert('Transacción Denegada\nPor favor consulte a su banco');", true);
                    }
                    else if (uri.Contains("error"))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", "alert('Error en la transacción\nPor favor intente nuevamente');", true);
                    }
                    else if(!uri.Contains("?"))
                    {
                    }
                    divPnlPagoCte.Visible = true;
                    
                    string CtrlID = string.Empty;
                    if (Session["ClienteNine"] != null)
                    {
                        RadioButton2.Enabled = true;
                    }
                    if (Request.Form[hidSourceID.UniqueID] != null ||
                        Request.Form[hidSourceID.UniqueID] != string.Empty)
                    {
                        CtrlID = Request.Form[hidSourceID.UniqueID];
                    }
                    if (CtrlID != "ctl00_ContentPlaceHolder1_btnEnviarPOS")
                    {
                        //ViewState["ImporteDescuentos"] = "0.0";
                        //ViewState["SubTotal"] = "0.0";
                        //ViewState["ImportePagar"] = "0.0";
                        //ViewState["ImportePromociones"] = "0.0";
                    }
                }


                if (HiddenTipoCliente.Value.ToString() == "E" || HiddenTipoCliente.Value.ToString().Equals(""))
                {
                    // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    //          tipPagos.Items[3].Attributes.Add("class", "ocul1");
                }

                //  RadioButtonlTipoPago.Items[1].Attributes.Add("class", "Relleno1");

            }
            catch (Exception ex)
            {
                log.Error(ex);

            }

        }
        #region "Metodos Privados"

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
                                //  RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");

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
                                if (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString()), Int64.Parse(pares.ToString())) && objCliente.ParPagado == 0)
                                {
                                    RadioButton3.Enabled = true;
                                }
                                else
                                {
                                    RadioButton3.Checked = false;
                                    RadioButton3.Enabled = false;
                                }

                                //VALIDAMOS BOLSA5
                                if (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString()), objCliente.NivelActual, Int64.Parse(Bolsa.ToString())) && objCliente.BolsaPagada == 0)
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public string ObtenerURL(string idTemporada, string idProveedor, string ModeloProveedor)
        {

            string ruta = string.Empty;

            try
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
        private int checkPagoEfectivo()
        {


            int valor = 0;
            objVenta = new DLLGestionVenta.ProcesarVenta();
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            valor = objVenta.GetPagoEfectivo();

            return valor;

        }
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
        private void cargaCarrito()
        {
            try
            {
                DataView dv;
                //Session["IdCarrito"] = 29;
                if (Session["IdCarrito"] != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();
                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                    DsPromo = objVenta.GetPromoCarritoLinea(Int64.Parse(Session["IdCarrito"].ToString()));
                    AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                    dv = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
                    this.gvCarrito.DataSource = dv;
                    gvCarrito.DataBind();
                    if (DsPromo.Tables.Count > 0)
                    {
                        HiddenPromo.Value = (DsPromo.Tables[0].Rows.Count > 0 ? "P" : "");
                    }
                    else
                    {
                        HiddenPromo.Value = "";
                    }
                    CargaEntrega();
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
            divEtiqDescuento.Visible = false;
            divImporteDescuento.Visible = false;
            Descuentobr.Visible = false;
            //divenviaPos.Attributes.Add("style", "float: left; height: 30px; width: 100px;");
            this.TotPendiente.Text = this.lblTotalPagar.Text;
        }

        private void poneTotales()
        {
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
                if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
                {
                    this.lblTotalPagar.Text = FormateaNumero((Convert.ToDouble(ViewState["ImportePagar"])).ToString());
                    if (gvCarrito.Rows.Count == 0)
                    {
                        Total.Visible = false;
                        resumenPago.Visible = false;
                        divenviaPos.Visible = false;
                        divPagos.Visible = false;
                        Pendiente.Visible = false;
                    }
                }
                if (Convert.ToDouble(ViewState["ImporteDescuentos"]) > 0)
                {
                    divEtiqDescuento.Visible = true;
                    divImporteDescuento.Visible = true;
                    //divenviaPos.Attributes.Add("style", "float: left; height: 40px; width: 100px;");
                }
                else
                {
                    divEtiqDescuento.Visible = false;
                    divImporteDescuento.Visible = false;
                    Descuentobr.Visible = false;
                    //divenviaPos.Attributes.Add("style", "float: left; height: 30px; width: 100px;");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /***************************
         * Cuando se da a cancelar un pago realizado con puntos,bolsas o pares nine, se refrescan a los valores nine iniciales,
         **/
        /////////////////////////////
        private void DeshacerPagosNine()
        {
            try
            {
                if (Session["ClienteNine"] != null)
                {
                    int pares;
                    int paresRegalado;
                    int Bolsa;
                    int BolsaRegalado;

                    CLIENTE9 objCliente = (CLIENTE9)(Session["ClienteNine"]);

                    if (objCliente.BolsaPagada > 0 || objCliente.ParPagado > 0 || objCliente.PuntosPagados > 0)
                    {
                        Label10.Text = (double.Parse(Label10.Text.ToString()) + objCliente.PuntosPagados).ToString();

                        pares = int.Parse(Label13.Text.ToString().Split('(')[0].ToString());
                        paresRegalado = int.Parse(Label13.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
                        Bolsa = int.Parse(Label14.Text.ToString().Split('(')[0].ToString());
                        BolsaRegalado = int.Parse(Label14.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
                        pares = pares + (int.Parse(objCliente.ParPagado.ToString())) * 8;
                        paresRegalado = (paresRegalado > 0 ? paresRegalado + int.Parse(objCliente.ParPagado.ToString()) : 0);
                        Bolsa = Bolsa + (int.Parse(objCliente.BolsaPagada.ToString())) * 4;
                        BolsaRegalado = (BolsaRegalado > 0 ? BolsaRegalado + int.Parse(objCliente.BolsaPagada.ToString()) : 0);

                        Label11.Text = (paresRegalado > 0 ? Label11.Text : "0");
                        Label12.Text = (BolsaRegalado > 0 ? Label12.Text : "0");
                        Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
                        Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";
                        RadioButton2.Enabled = true;
                        RadioButton3.Enabled = true;
                        objCliente.PuntosPagados = 0;
                        objCliente.ParPagado = 0;
                        objCliente.BolsaPagada = 0;

                        ////VALIDAMOS PAR9 
                        //if (Int64.Parse(paresRegalado.ToString()) > 0)
                        //{
                        //    RadioButton3.Enabled = false;
                        //}
                        //else
                        //{
                        //    RadioButton3.Enabled = true;
                        //}

                        ////VALIDAMOS BOLSA5
                        //if (Int64.Parse(BolsaRegalado.ToString()) > 0)
                        //{
                        //    RadioButton4.Enabled = false;
                        //}
                        //else
                        //{
                        //    RadioButton4.Enabled = true;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private void DeshacerPagosCarrito()
        {
            try
            {
                DataSet Ds;
                float TotalPagado = 0;

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

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

                        if (Dr["Tipo"].ToString().Equals("BOLSA 5"))
                        {
                            RadioButton2.Enabled = false;
                            RadioButton2.Checked = false;

                        }

                    }

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        btnEnviarPOS.Visible = false;
                        nomcliente.Enabled = false;
                        BtnCliente.Enabled = false;
                        //TarjetaCliente.Enabled = false;
                        ButClient.Enabled = false;

                    }

                    DataRow row = Ds.Tables[0].NewRow();
                    row["Tipo"] = "Total Pagado:";
                    row["Importe"] = TotalPagado;

                    Ds.Tables[0].Rows.Add(row);
                    Ds.AcceptChanges();

                    RepeaterPago.DataSource = Ds;
                    RepeaterPago.DataBind();
                }
                else
                {
                    divResumen.Visible = false;
                }

                float fSubtotal = 0;
                float fDescuento = 0;
                float fTotalPagar = 0;

                if (float.TryParse(ViewState["SubTotal"].ToString(), out fSubtotal))
                {
                    fDescuento = float.Parse(ViewState["ImporteDescuentos"].ToString());
                    fTotalPagar = (fSubtotal - fDescuento);
                }

                float fTotal = (fTotalPagar - TotalPagado);
                TotPendiente.Text = FormateaNumero(fTotal.ToString());

                if (fTotal == 0 && fTotalPagar > 0)
                {
                    Divfinalizar.Visible = true;
                    showPago.Visible = false;
                    //  RadioButtonlTipoPago.CssClass = "ocul1";
                    //  RadioButton1.CssClass = "Ocultarcontrol";
                    //  RadioButton1.Visible = false;
                    btnEnviarPOS.Visible = false;
                    btnBorrarCarrito.Visible = false;
                    //  RadioButton1.CssClass = "Ocultarcontrol";
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
                float TotalPagado = 0;

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

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

                        if (Dr["Tipo"].ToString().Equals("BOLSA 5"))
                        {
                            RadioButton2.Enabled = false;
                            RadioButton2.Checked = false;

                        }

                    }

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        btnEnviarPOS.Visible = false;
                        nomcliente.Enabled = false;
                        BtnCliente.Enabled = false;
                        //TarjetaCliente.Enabled = false;
                        ButClient.Enabled = false;

                    }

                    DataRow row = Ds.Tables[0].NewRow();
                    row["Tipo"] = "Total Pagado:";
                    row["Importe"] = TotalPagado;

                    Ds.Tables[0].Rows.Add(row);
                    Ds.AcceptChanges();

                    RepeaterPago.DataSource = Ds;
                    RepeaterPago.DataBind();
                }
                else
                {
                    divResumen.Visible = false;
                }

                float fSubtotal = 0;
                float fDescuento = 0;
                float fTotalPagar = 0;

                if (float.TryParse(ViewState["SubTotal"].ToString(), out fSubtotal))
                {
                    fDescuento = float.Parse(ViewState["ImporteDescuentos"].ToString());
                    fTotalPagar = (fSubtotal - fDescuento);
                }

                float fTotal = (fTotalPagar - TotalPagado);

                if (fTotal >= 0)
                    TotPendiente.Text = FormateaNumero(fTotal.ToString());
                else
                {
                    Label17.Visible = true;
                    TDevolucion.Visible = true;
                    TotPendiente.Text = FormateaNumero("0");
                    TDevolucion.Text = FormateaNumero((fTotal * -1).ToString());
                    fTotal = 0;
                }
                if (fTotal == 0 && fTotalPagar > 0)
                {
                    Divfinalizar.Visible = true;
                    showPago.Visible = false;
                    //RadioButtonlTipoPago.CssClass = "ocul1";
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //RadioButton1.Visible = false;
                    btnEnviarPOS.Visible = false;
                    btnBorrarCarrito.Visible = false;
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    tipPagos.Visible = false;
                }
                if (fTotal == 0 && fTotalPagar == 0)
                {
                    Divfinalizar.Visible = true;
                    showPago.Visible = false;
                    lblforma.Visible = false;
                    DivPagar.Style.Add("display", "none");
                    //RadioButtonlTipoPago.CssClass = "ocul1";
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //RadioButton1.Visible = false;
                    btnEnviarPOS.Visible = false;
                    btnBorrarCarrito.Visible = false;
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    tipPagos.Visible = false;
                }
                else if (fTotal > 0)
                {
                    Divfinalizar.Visible = false;
                    showPago.Visible = true;
                    btnEnviarPOS.Visible = true;
                    btnBorrarCarrito.Visible = true;
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    tipPagos.Visible = true;

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

        protected void btnEnviarPOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["IdCarrito"] == null)
                    return;

                DataView dv;
                this.lblPOS.Text = "";
                string comment = TxtComentarios.Text;
                EnviaPOS.SelectParameters["IdTerminal"].DefaultValue = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
                EnviaPOS.SelectParameters["IdTienda"].DefaultValue = HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
                EnviaPOS.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                EnviaPOS.SelectParameters["IdUsuario"].DefaultValue = Contexto.IdEmpleado;
                if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
                    EnviaPOS.SelectParameters["dPrecio"].DefaultValue = ViewState["ImportePagar"].ToString();
                else
                    EnviaPOS.SelectParameters["dPrecio"].DefaultValue = "0";
                //ACL.08-07-2014. Se graba el comentario general del tiquet, aunque el ENVIO A POS
                //graba en la tabla nti
                comment = string.IsNullOrEmpty(TxtComentarios.Text) ? " " : TxtComentarios.Text; //Si no se ha introducido un comentario, se añade un blanco al parametro
                EnviaPOS.SelectParameters["strComentario"].DefaultValue = comment;
                dv = (DataView)EnviaPOS.Select(new DataSourceSelectArguments());
                if (dv != null && dv.Count > 0)
                {
                    if (dv[0][0] != null && dv[0][0].ToString() != "")
                    {
                        this.lblPOS.Text = "Terminal: " + dv[0][0].ToString().Split('/')[1] + ". "
                                            + Resources.Resource.EnEspera + ": " + dv[0][0].ToString().Split('/')[0];
                        Session["IdCarrito"] = null;
                        Session["ClienteNine"] = null;
                        Session["objCliente"] = null;
                        this.divDetalle.Visible = false;
                        this.divPagos.Visible = false;
                        imgEnvioPos.ImageUrl = "~/img/Ok.png";
                    }
                    else
                    {
                        this.lblPOS.Text = "No se ha podido poner el ticket en espera";
                        imgEnvioPos.ImageUrl = "~/img/Error.png";
                    }
                    this.divResumen.Visible = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        protected void gvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddllist;
                    DataView dvLineaPromo;
                    String IdLineaCarrito = gvCarrito.DataKeys[e.Row.DataItemIndex].Value.ToString();

                    //Ponemos valores al boton de eliminacion
                    ImageButton btn = (ImageButton)e.Row.FindControl("imgBorrar");

                    btn.Attributes.Add("onclick", "javascript:return ValidarEliminarCarrito()");

                    dvLineaPromo = DsPromo.Tables[0].DefaultView;
                    dvLineaPromo.RowFilter = "id_linea_Carrito=" + IdLineaCarrito;

                    Panel pnl = (Panel)e.Row.FindControl("divPromocionBotas");
                    pnl.Attributes.Add("style", "display:none; clear:left");

                    Label lblidPromocion = (Label)e.Row.FindControl("lblPromocionBotas");
                    if (lblidPromocion != null)
                    {
                        if (lblidPromocion.Text != "-$0.00")
                        {
                            Label lblDescriPromo = (Label)e.Row.FindControl("DescriPromo");
                            lblDescriPromo.Text = DataBinder.Eval(e.Row.DataItem, "promocion").ToString() + ":";
                            lblDescriPromo = (Label)e.Row.FindControl("lblPromobr");
                            pnl.Visible = true;
                            lblDescriPromo.Visible = true;
                            lblDescriPromo.Text = "<br />";
                            if (pnl != null)
                                pnl.Attributes.Add("style", "display:block");

                            // MJM 17/03/2014 INICIO
                            float pvpori = float.Parse(DataBinder.Eval(e.Row.DataItem, "PVPORI").ToString());
                            float dtopromo = float.Parse(DataBinder.Eval(e.Row.DataItem, "DTOPROMO").ToString());
                            if (pvpori > 0)
                            {
                                float descuentoPromocion = ((dtopromo * 100) / pvpori);
                                ((HtmlControl)e.Row.FindControl("divDescuentoPromocion")).Visible = true;
                                // descartar decimales.
                                ((Label)e.Row.FindControl("lblPorcentajeDescuento")).Text = string.Format("{0} %", Math.Round(descuentoPromocion, 2));
                            }

                            // MJM 17/03/2014 FIN
                            // vamos a tachar el precio DML
                            Label lblOriginal = (Label)e.Row.FindControl("lblPrecioOriginal");
                            lblOriginal.Attributes.Add("style", "text-decoration:line-through");

                        }
                    }
                    // para comprobar que el articulo tiene mas de una promocion por linea
                    if (dvLineaPromo.Count > 0)
                    {
                        lblidPromocion.Visible = false;
                        ddllist = (DropDownList)e.Row.FindControl("ddlSelecionarPromocion");
                        ddllist.Visible = true;
                        ddllist.DataSource = dvLineaPromo;
                        ddllist.DataTextField = "DescriPromo";
                        ddllist.DataValueField = "idPromo";
                        ddllist.DataBind();
                        pnl.Visible = true;
                        Label lbl = (Label)e.Row.FindControl("lblPromobr");
                        lbl.Text = "<br />";
                        lbl = (Label)e.Row.FindControl("DescriPromo");
                        lbl.Text = "Promoción:";
                        pnl.Attributes.Add("style", "display:block");
                    }

                    pnl = (Panel)e.Row.FindControl("divforanea");
                    pnl.Visible = false;
                    if (DataBinder.Eval(e.Row.DataItem, "IdTienda").ToString() != Contexto.IdTienda)
                    {
                        pnl.Visible = true;
                        Label lbl = (Label)e.Row.FindControl("Label1");
                        lbl.Text += " - " + DataBinder.Eval(e.Row.DataItem, "IdTienda").ToString();
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
                    AVE_CarritoEliminar.DeleteParameters["IdLineaCarrito"].DefaultValue = e.CommandArgument.ToString();
                    AVE_CarritoEliminar.Delete();
                    //alecxis 24 11 2016
                    //inicia
                    idCarritoDetalle = int.Parse(e.CommandArgument.ToString());
                    btnBorrarEntrega_Click(null, null);
                    //fin

                    //Para cuando se borre el ultimo articulo del carrito, no actualiza el label de total artículos
                    if (Session["IdCarrito"] != null)
                    {
                        int ArtiCarrito = CheckArticulosCarrito(Session["IdCarrito"].ToString());

                        var miMaster = (MasterPage)this.Master;
                        miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                    }
                    cargaCarrito();
                    poneTotales();
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
                        return;
                    }
                    else
                    {
                        CalculoPromocion_CarritoCliente();
                        sPage = "~/CarritoDetalle.aspx";
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


        protected void gvCarrito_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Double fDescuento = 0;
                Double fImporteDescuento = 0;
                Double fPrecioORI = 0;
                Double fprecioAct = 0;
                double fDescuentoPromo = 0;


                if (e.Row.RowType == DataControlRowType.Header)
                {
                    ViewState["ImporteDescuentos"] = "0.0";
                    ViewState["SubTotal"] = "0.0";
                    ViewState["ImportePagar"] = "0.0";
                    ViewState["ImportePromociones"] = "0.0";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // '<%#Bind("DTOArticulo","{0:0.00}")
                    fImporteDescuento = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DTOArticulo"));
                    fPrecioORI = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PVPORI"));
                    fprecioAct = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PVPACT"));
                    fDescuentoPromo = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DtoPromo"));


                    fDescuento = 100 * (fPrecioORI - fprecioAct) / fPrecioORI;
                    if (fImporteDescuento > 0)
                    {
                        CultureInfo info = CultureInfo.GetCultureInfo("es-MX");
                        fDescuento = fDescuento / 100;
                        ((Label)e.Row.FindControl("lblPorDescuento")).Text = Convert.ToDouble(fDescuento).ToString("P2", info);

                        //((Label)e.Row.FindControl("lblPorcentajeDescuento")).Text = 

                        Label lblOriginal = (Label)e.Row.FindControl("lblPrecioOriginal");
                        lblOriginal.Attributes.Add("style", "text-decoration:line-through");

                    }
                    else
                    {

                        ((Label)e.Row.FindControl("lblPorDescuento")).Text = "0.00%";
                        ((Label)e.Row.FindControl("lblPorDescuento")).Visible = false;
                        ((Label)e.Row.FindControl("Label5")).Visible = false;
                        ((Label)e.Row.FindControl("lblImporteDTO")).Visible = false;

                        var div = e.Row.FindControl("laDescuento") as HtmlGenericControl;
                        div.Visible = false;
                        var div1 = e.Row.FindControl("laImporteDescuento") as HtmlGenericControl;
                        div.Visible = false;
                        var div2 = e.Row.FindControl("PorcDescuento") as HtmlGenericControl;
                        div2.Visible = false;
                    }

                    ViewState["ImporteDescuentos"] = Convert.ToDouble(ViewState["ImporteDescuentos"]) + fImporteDescuento + fDescuentoPromo;
                    ViewState["SubTotal"] = Convert.ToDouble(ViewState["SubTotal"]) + fPrecioORI;
                    Label lblaPagar = (Label)e.Row.FindControl("lblPagar");
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lblPOS.Text.Equals("No se ha podido poner el ticket en espera"))
            {
                this.divResumen.Visible = false;
                return;
            }

            Response.Redirect(Constantes.Paginas.Inicio);
        }

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
            string semilla = "semilla";//HttpContext.Current.Session[Constantes.Session.Semilla].ToString();

            rc4 encripta = new rc4();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);


            xmlmpos oObject = getMPOSSession(Amount, TipoMIT, merchant, Email);

            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            StringWriterUtf8 text = new StringWriterUtf8();

            xmlSerializer.Serialize(text, oObject, namespaces);

            sVd = text.ToString();
            sVd = sVd.Replace("\r\n", "");
            // sVd = R4.Encrypt(semilla,sVd); //TODO: Comprobar que esta clase RC4 funciona correctamente. 
            sVd = encripta.StringToHexString(encripta.Salaa(sVd, semilla));
            return sVd;
        }

        public static Int64 sendPagoPOS(string Amount, string Tarjeta, string TipoMIT, string merchant, string Email)
        {
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
            return idCarritoPago;
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
                log.Error("IDCOMPANY: " + HttpContext.Current.Session[Constantes.Session.IdCompany].ToString());
                oObject.amount = Amount.Replace(",", "");
                //ACL.26-08/2014. Se envía como referencia, el idcarrito-IdTicket
                //oObject.reference = HttpContext.Current.Session["IdCarrito"].ToString() + "/" + AVE.Contexto.IdTienda.Trim() + " /" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string idTicket = getUltimoTicket();

                string referencia = HttpContext.Current.Session["IdCarrito"].ToString() + "-" + idTicket + "/" + AVE.Contexto.IdTerminal.Trim() + "/" + AVE.Contexto.IdTienda;
                log.Error("REFERENCE: " + referencia);

                oObject.reference = HttpContext.Current.Session["IdCarrito"].ToString() + "-" + idTicket + "/" + AVE.Contexto.IdTerminal.Trim() + "/" + AVE.Contexto.IdTienda;
                //ACL.26-08-2014.FIN
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
                else oObject.e_mail = " ";
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
            /*   bool EsDirectivo = false;
               Session["EsDirectivo"] = 0;*/

            // ModalPopupExtenderProcess.Visible = true;
            try
            {
                Session["Email_Cliente"] = null;
                this.txtemail.Text = "";
                this.Email.Text = "";
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
                            CalculoPromocion_CarritoCliente();
                            cargaCarrito();
                            poneTotales();
                            CargarPagosCarritos();
                        }
                        else
                            this.BuscarClienteNine(nomcliente.Text);
                    }
                    else
                    {

                        GetClienteActual(nomcliente.Text);
                        CalculoPromocion_CarritoCliente();
                        cargaCarrito();
                        poneTotales();
                        CargarPagosCarritos();
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
            finally
            {

                //   ModalPopupExtenderProcess.Visible = false;
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
                //ACL.05-08-2014.Se habilita el cliente al cancelar un pago, por si se desea cambiar el cliente
                nomcliente.Enabled = true;
                BtnCliente.Enabled = true;
                string idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                string carrito = Convert.ToString(Session["IdCarrito"]);
                ClsCapaDatos Bdatos = new ClsCapaDatos();

                Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                bool res = Bdatos.CancelaPagoCarritoBD(carrito);

                //para cancelar todas las operaciones de cliente 9.
                Comun.CheckPending();
                //Pintamos los controles en su estado original al pago del cliente nine
                DeshacerPagosCarrito();
                ReinicializaTotales();
                DeshacerPagosNine();
                // ((RadioButtonList)RadioButtonlTipoPago).ClearSelection();

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }


        /// <summary>
        /// evento que va a capturar el pago y registrar toda la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ButPagar_Click2(object sender, EventArgs e)
        {
            HyperLink noArt = (HyperLink)this.Master.FindControl("lblNumArt");
            string totArt = noArt.Text, totPago = TotPendiente.Text;
            totPago = totPago.Replace('$', ' ');
            //lstTipoMIT_SelectedIndexChanged(this, e);
            //if (mit.Equals("VMC "))
            //    cboPlazoNormal_SelectedIndexChanged(this, e);
            //else
            //    cboPlazoAmex_SelectedIndexChanged(this, e);
               
            //Response.Redirect(Constantes.Paginas.Carrito + "$totArt=" + totArt + "&totPago=" + totPago + "&mit=" + mit + "&mitV=" + mitCard + "&merchId=" + merchId + "&merchVal=" + merchVal);
            Response.Redirect(Constantes.Paginas.Carrito + "?" + totPago + "=vta" + Session["IdCarrito"].ToString()+"="+txtemail.Text);
        }

        protected void ButPagar_Click(object sender, EventArgs e)
        {
            try
            {
                float Num;

                Carrito_Pago objPago;
                String ResultPago = "0";

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.FechaSesion = AVE.Contexto.FechaSesion;

                objPago = new Carrito_Pago();
                string tPago = tipPagos.SelectedValue.ToString();
                if (tPago == "N")
                {
                    if (float.TryParse(txtPago.Text.ToString().Replace(",", ""), out Num))
                    {

                        objPago.IdCarrito = idCarrito;
                        objPago.TipoPago = "NOTA EMPLEADO";
                        Session["tipoPago"] = null;
                        objPago.TipoPagoDetalle = "";
                        objPago.NumTarjeta = "";
                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                        ResultPago = objVenta.PagoCarritoNotaEmpleado(objPago, true);

                        if (ResultPago != "0" && ResultPago != "1")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "NOTEMP", "alert('" + ResultPago + "');", true);
                        }

                        txtPago.Text = String.Empty;

                        btnEnviarPOS.Visible = false;
                        nomcliente.Enabled = false;
                        BtnCliente.Enabled = false;
                        //TarjetaCliente.Enabled = false;
                        ButClient.Enabled = false;
                        //this.tipPagos.Items[0].Selected = true;

                        // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    }
                }
                else
                {

                    if (tPago == "T")
                    {
                        if (this.txtemail.Text != "")
                        {
                            HyperLink noArt = (HyperLink)this.Master.FindControl("lblNumArt");
                            string totArt = noArt.Text, totPago = TotPendiente.Text;
                            totPago = totPago.Replace('$', ' ');
                            Session["Email_Cliente"] = txtemail.Text;
                            Session["tipoPago"] = "Tarjeta";
                            this.btnBorrarCarrito.Enabled = false;
                            Response.Redirect(Constantes.Paginas.Carrito + "?" + totPago + "=vta" + Session["IdCarrito"].ToString() + "=" + txtemail.Text,false);
                        }
                        else
                            ;
                            
                        
                    }
                    else if (tPago == "E")
                    {
                        objPago.IdCarrito = idCarrito;
                        objPago.TipoPago = "EFECTIVO";
                        objPago.TipoPagoDetalle = "";
                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                        Session["tipoPago"] = null;
                        objVenta.PagoCarritoEfectivo(objPago);
                        txtPago.Text = String.Empty;
                    }
                }

                cargaCarrito();
                poneTotales();
                CargarPagosCarritos();
                GetClientesCarrito();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
            }
        }

        public void pagoTarjeta()
        {
            float Num;

            Carrito_Pago objPago;
            String ResultPago = "0";

            objVenta = new DLLGestionVenta.ProcesarVenta();
            Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            objVenta.FechaSesion = AVE.Contexto.FechaSesion;

            objPago = new Carrito_Pago();

                objPago.IdCarrito = idCarrito;
                objPago.TipoPago = System.Configuration.ConfigurationManager.AppSettings["TarjetaTipo"].ToString();
            //    objPago.TipoPagoDetalle = lstTarjetas.SelectedItem.Text;
                objPago.TipoPagoDetalle = appidLbl;
                objPago.NumTarjeta = ccNum;
            //    objPago.Importe = float.Parse(txtPago.Text.ToString());
                objPago.Importe = float.Parse(amt, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                objVenta.PagoCarrito(objPago);
        }

        

        /*
        protected void ButPagar_Click(object sender, EventArgs e)
        {
            try
            {
                float Num;


                Carrito_Pago objPago;
                String ResultPago = "0";

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.FechaSesion = AVE.Contexto.FechaSesion;


                objPago = new Carrito_Pago();
                string tPago = tipPagos.SelectedValue.ToString();
                // tPago = hiddenOptPago.Value;
                ///pago por nota de empleado
                //if (RadioButton1.Checked)
                //if (tipPagos.Items[2].Selected)
                if (tPago == "N")
                {
                    if (float.TryParse(txtPago.Text.ToString().Replace(",", ""), out Num))
                    {

                        objPago.IdCarrito = idCarrito;
                        objPago.TipoPago = "NOTA EMPLEADO";
                        objPago.TipoPagoDetalle = "";
                        objPago.NumTarjeta = "";
                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));


                        ResultPago = objVenta.PagoCarritoNotaEmpleado(objPago, true);

                        if (ResultPago != "0" && ResultPago != "1")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "NOTEMP", "alert('" + ResultPago + "');", true);

                        }

                        txtPago.Text = String.Empty;

                        //  RadioButton1.Checked = false;
                        btnEnviarPOS.Visible = false;
                        nomcliente.Enabled = false;
                        BtnCliente.Enabled = false;
                        //TarjetaCliente.Enabled = false;
                        ButClient.Enabled = false;
                        //this.tipPagos.Items[0].Selected = true;

                        // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    }
                }
                else
                {
                    ///pago Tarjeta MIt
                    //if (RadioButtonlTipoPago.SelectedValue.ToString() == "T")
                    //{

                    //    objPago.IdCarrito = idCarrito;
                    //    objPago.TipoPago = System.Configuration.ConfigurationManager.AppSettings["TarjetaTipo"].ToString();
                    //    objPago.TipoPagoDetalle = lstTarjetas.SelectedItem.Text;
                    //    objPago.NumTarjeta = "";
                    //    objPago.Importe = float.Parse(txtPago.Text.ToString());

                    //    objVenta.PagoCarrito(objPago);
                    //    txtPago.Text = String.Empty;

                    //}
                    //else
                    //{
                    //cliente 9
                    //if (RadioButtonlTipoPago.SelectedValue.ToString() == "9")
                    //if (tipPagos.Items[3].Selected)
                    if (tPago == "9")
                    {
                        if (this.txtemail.Text == "")
                        {

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Pagar", "alert('El campo email debe estar informado.');", true);
                            return;
                        }
                        else Session["Email_Cliente"] = txtemail.Text;

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


                        if (RadioButton3.Checked)
                        {
                            // par 9

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

                            objVenta.PagoCarrito(objPago);
                            txtPago.Text = String.Empty;

                            RadioButton3.Checked = false;

                            btnEnviarPOS.Visible = false;
                            nomcliente.Enabled = false;
                            BtnCliente.Enabled = false;
                            //TarjetaCliente.Enabled = false;
                            ButClient.Enabled = false;


                        }
                        else
                        {
                            if (RadioButton4.Checked)
                            {
                                // bolsas

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

                                objVenta.PagoCarrito(objPago);
                                txtPago.Text = String.Empty;
                                ((CLIENTE9)Session["ClienteNine"]).BolsaPagada = 1;

                                RadioButton4.Checked = false;

                                btnEnviarPOS.Visible = false;
                                nomcliente.Enabled = false;
                                BtnCliente.Enabled = false;
                                //TarjetaCliente.Enabled = false;
                                ButClient.Enabled = false;
                            }
                            else
                            {
                                // puntos 9

                                if (RadioButton2.Checked)
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

                                    objVenta.PagoCarrito(objPago);
                                    txtPago.Text = String.Empty;

                                    ((CLIENTE9)Session["ClienteNine"]).PuntosPagados = objPago.Importe;

                                    RadioButton4.Checked = false;
                                    btnEnviarPOS.Visible = false;
                                    nomcliente.Enabled = false;
                                    BtnCliente.Enabled = false;
                                    //TarjetaCliente.Enabled = false;
                                    ButClient.Enabled = false;

                                }

                            }
                        }
                    }
                    //else if (RadioButtonlTipoPago.SelectedValue.ToString() == "T")
                    // else if (tipPagos.Items[0].Selected)
                    else if (tPago == "T")
                    {
                        if (this.txtemail.Text == "")
                        {

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Pagar", "alert('El campo email debe estar informado.');", true);
                            return;
                        }
                        else Session["Email_Cliente"] = txtemail.Text;
                        this.btnBorrarCarrito.Enabled = false;
                    }
                    //else if (RadioButtonlTipoPago.SelectedValue.ToString() == "E") {
                    //else if(tipPagos.Items[1].Selected){
                    else if (tPago == "E")
                    {
                        objPago.IdCarrito = idCarrito;
                        objPago.TipoPago = "EFECTIVO";
                        objPago.TipoPagoDetalle = "";
                        objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                        objVenta.PagoCarritoEfectivo(objPago);
                        txtPago.Text = String.Empty;
                    }

                    //}
                    //vaciamos controles
                    //  CargarPagosCarritos();

                    //((RadioButtonList)RadioButtonlTipoPago).ClearSelection();
                    //  tipPagos.Items[0].Selected= true;
                }



                cargaCarrito();
                poneTotales();
                CargarPagosCarritos();
                GetClientesCarrito();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {


                //   ModalPopupExtenderProcess.Visible = false;



            }
        }
        */
        
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
                //  ModalPopupExtenderProcess.Visible = true;
                /*if (this.txtemail.Text == "")
                 {

                     ScriptManager.RegisterStartupScript(this, typeof(Page), "Finalizar", "alert('El campo email debe estar informado.');", true);
                     return;
                 }*/

                String idTicket;
                CLIENTE9 C9;
                //  this.ButFinalizarVenta.Enabled = false;

                if (Session["IdCarrito"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CARRITOENV", "alert('El Carrito que has intentado finalizar ya esta registrado en el sistema.);", true);
                    Response.Redirect("~/Inicio.aspx", true);
                }

                //Si es posible

                objVenta = new DLLGestionVenta.ProcesarVenta();
                Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                objVenta.Terminal = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
                objVenta.FechaSesion = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString());

                //ADRIANA ZAMORA GONZALEZ 23/06/2017
                //Agregar campo para cambio de efectivo, Kayako [!?]
                if (TDevolucion.Text.Contains('$')) { 
                    if (double.Parse(TDevolucion.Text.Replace("$", "")) > 0.00)
                    {
                        objVenta.cambioEfectivo = double.Parse(TDevolucion.Text.Replace("$", ""));
                    }
                    else
                    {
                        objVenta.cambioEfectivo = 0;
                    }
                }
                else
                {
                    objVenta.cambioEfectivo = 0;
                }
                

                Application.Lock();

                C9 = new CLIENTE9();
                if (Session["ClienteNine"] != null) { C9 = (CLIENTE9)Session["ClienteNine"]; }
                //ACL. 08-07.2014. Le pasamos el valor de comentarios en el objeto venta, que será luego
                //el que se pase en el ticket.
                string strWS = "";
                idTicket = objVenta.EjecutaVenta(idCarrito, C9, AVE.Contexto.FechaSesion, TxtComentarios.Text, ref strWS);

                if (idTicket.Equals("ERROR"))
                {
                    Session["IdCarrito"] = null;
                    Session["FVENTA"] = null;
                    Session["objCliente"] = null;
                    Session["ClienteNine"] = null;
                    Application.UnLock();

                    //  ModalPopupExtenderProcess.Visible = false;

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CARRITOENV", "alert('El Carrito que has intentado finalizar ya esta registrado en el sistema.);", true);
                    Response.Redirect("~/Inicio.aspx", true);

                }
                else
                {

                    //falta añadir el metodo para guardar el ticket en el AVE_CARRITO
                    try
                    {
                        AniadeTicketCarrito(Session["IdCarrito"].ToString(), idTicket);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error al insertar el ticket en el carrito. Error " + ex.Message.ToString());
                    }

                }
                if (strWS != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE9", "alert('" + strWS + ".);", true);
                Session["IdCarrito"] = null;

                //Application.UnLock();

                FinalizaVenta obFventa;

                obFventa = new FinalizaVenta();

                obFventa.Ticket = idTicket;
                obFventa.cliente = Nombre.Text.ToString();
                obFventa.Entrega = String.Empty;

                Session["FVENTA"] = obFventa;
                Session["ClienteNine"] = null;
                Session["objCliente"] = null;

                string auth = Payment.getAuth();
                string operation = Payment.getOperation();

                //   ModalPopupExtenderProcess.Visible = false;


                Response.Redirect("FinalizaCompra.aspx",false);
            }
            catch (Exception ex)
            {
                //Response.Redirect("Inicio.aspx");
                Session["Error"] = ex;
                Session["lastURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect("Error.aspx");
                log.Error(ex);
            }
        }

        protected void LstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nomcliente.Text = LstClientes.SelectedItem.Text;
                GetClienteActual(LstClientes.SelectedValue.ToString());
                CalculoPromocion_CarritoCliente();
                cargaCarrito();
                poneTotales();
                CargarPagosCarritos();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void optEntregaOtraUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            pnlOtraObicacion.Visible = optEntregaOtraUbicacion.Checked;
        }

        protected void cmdConfirmarEntrega_Click(object sender, EventArgs e)
        {

            try
            {

                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

                entrega.idCarrito = Convert.ToInt32(Session["IdCarrito"]);
                entrega.Nombre = txtOUNombre.Text;
                entrega.Apellidos = txtOUApellidos.Text;
                entrega.Email = txtOUEmail.Text;
                entrega.Direccion = txtOUDireccion.Text;
                entrega.NoInterior = txtOUNoInterior.Text;
                entrega.NoExterior = txtOUNoExterior.Text;
                entrega.Estado = cboOUEstado.SelectedValue;
                entrega.Ciudad = txtOUCiudad.Text;
                entrega.Colonia = txtOUColonia.Text;
                entrega.CodPostal = txtOUCodigoPostal.Text;
                entrega.TelfMovil = txtOUTelfCelular.Text;
                entrega.TelfFijo = txtOUTelfFijo.Text;
                entrega.Referencia = txtOUReferenciaLlegada.Text;

                objCapaDatos.ActualizarEntrega(entrega);

                cmdConfirmarEntrega.Visible = false;
                lblEntregaConfirmada.Visible = true;
                lblEntregaConfirmada.Attributes["class"] = "alert alert-succes";

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
                        cargaCarrito();
                        poneTotales();
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

        private void GetClienteActual(CLIENTE9 cliente)
        {
            try
            {

                Nombre.Text = cliente.Cliente;
                Email.Text = cliente.Email.ToString();
                txtemail.Text = cliente.Email.ToString();
                Shoelover.Text = cliente.NivelActual;

                nomcliente.Text = cliente.Cliente;
                hidIdCliente.Value = cliente.Id_Cliente.ToString();

                CLiente.Visible = true;
                // RadioButton1.CssClass = "Ocultarcontrol";
                LstClientes.CssClass = "ocul1";
                nomcliente.CssClass = "visi1";
                BtnCliente.Visible = true;

                //if (cliente.NivelActual.ToString().Length > 0)
                //{
                //    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                //}
                //else
                //{
                //    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                //}
                if (cliente.NivelActual.ToString().Length > 0)
                {
                    //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                    //tipPagos.Items[3].Attributes.Add("class", "visi1");


                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("9"));
                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("N"));
                    ListItem item = new ListItem();
                    item.Text = "Cliente 9";
                    item.Value = "9";
                    tipPagos.Items.Add(item);
                    HiddenTipoCliente.Value = "9";

                    //tipPagos.Items[2].Attributes.Add("class", "ocul1");
                    tipPagos.Focus();

                }
                else
                {
                    //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    // tipPagos.Items[3].Attributes.Add("class", "ocul1");
                    // tipPagos.Items[2].Attributes.Add("class", "ocul1");
                    tipPagos.Focus();
                }


                if (cliente.Empleado_cliente != null)
                {
                    if (cliente.Empleado_cliente.EsEmpleado && float.Parse(ViewState["ImportePagar"].ToString()) != 0)
                    {

                        /* RadioButton1.CssClass = "VisibleControl";
                               RadioButton1.Enabled = false;
                               RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");*/
                        HiddenTipoCliente.Value = "E";
                        if (cliente.Empleado_cliente.NumNotaempleado >= cliente.Empleado_cliente.NotaEmpleadoGastadas)
                        {
                            // RadioButton1.Enabled = true;
                            // tipPagos.Items[2].Attributes.Add("class", "visi1");
                            tipPagos.Items.Remove(tipPagos.Items.FindByValue("N"));
                            tipPagos.Items.Remove(tipPagos.Items.FindByValue("9"));
                            ListItem item = new ListItem();
                            item.Text = "Notas Empleado";
                            item.Value = "N";
                            tipPagos.Items.Add(item);

                            //tipPagos.Items[2].Selected = true ;
                            tipPagos.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private bool GetNivelCliente(string idCliente, string IdTienda, int idCarrito)
        {
            bool EsDirectivo = false;
            int result = 0;
            double dto = 0;
            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            try
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
            catch (Exception ex)
            {
                log.Error("Error al obtener si el cliente es directivo:" + ex.Message.ToString());
            }

            return EsDirectivo;
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
                        ClientScriptManager cs = Page.ClientScript;
                        Nombre.Text = objclient[0].Cliente;
                        if (Session["Email_Cliente"] != null)
                        {
                            Email.Text = Session["Email_Cliente"].ToString();
                            txtemail.Text = Session["Email_Cliente"].ToString();
                        }

                        if (objclient[0].Email.ToString() != "")
                        {
                            Email.Text = objclient[0].Email.ToString();
                            txtemail.Text = objclient[0].Email.ToString();

                        }

                        Shoelover.Text = objclient[0].NivelActual;


                        Session["objCliente"] = objclient[0];
                        //ACL.09012015,SI SE PAGA PRIMERO CON TJTA Y LUEGO CON PUNTOS NINE, BORRA EL CLIENTE NINE QUE ESTA EN SSION
                        //Session["ClienteNine"] = null;

                        //nomcliente.Text = objclient[0].Id_Cliente.ToString();
                        nomcliente.Text = objclient[0].Cliente;
                        hidIdCliente.Value = objclient[0].Id_Cliente.ToString();

                        CLiente.Visible = true;

                        // RadioButton1.CssClass = "Ocultarcontrol";
                        LstClientes.CssClass = "ocul1";
                        nomcliente.CssClass = "visi1";
                        BtnCliente.Visible = true;

                        if (objclient[0].NivelActual.ToString().Length > 0)
                        {
                            //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                            // tipPagos.Items[3].Attributes.Add("class", "visi1");
                            //cs.RegisterStartupScript(this.GetType(), "", "rmInsPagNine('1');", true);
                            tipPagos.Items.Remove(tipPagos.Items.FindByValue("9"));
                            tipPagos.Items.Remove(tipPagos.Items.FindByValue("N"));
                            if (Session["TiendaCamper"] == null) Session["TiendaCamper"] = Comun.checkTiendaCamper();
                            if (Session["TiendaCamper"].ToString() == "0")
                            {
                                ListItem item = new ListItem();
                                item.Text = "Cliente 9";
                                item.Value = "9";
                                tipPagos.Items.Add(item);
                                HiddenTipoCliente.Value = "9";
                            }
                            else HiddenTipoCliente.Value = "";


                            // tipPagos.ClearSelection();
                            tipPagos.Focus();

                        }
                        else
                        {
                            //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");

                            //    tipPagos.Items[3].Attributes.Add("class", "ocul1");
                            //    tipPagos.Items[2].Attributes.Add("class", "ocul1");
                            tipPagos.Focus();
                        }
                        if (objclient[0].NumTarjeta != "" && Session["ClienteNine"] == null)
                        {
                            cargaClienteNineSesion(objclient[0].NumTarjeta.ToString());
                        }
                        else
                        {
                            Session["objCliente"] = objclient[0];
                        }

                        if (objclient[0].Empleado_cliente != null)
                        {
                            if (objclient[0].Empleado_cliente.EsEmpleado)
                            {
                                /* RadioButton1.CssClass = "VisibleControl";
                                 RadioButton1.Enabled = false;
                                 RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");*/
                                HiddenTipoCliente.Value = "E";
                                if (objclient[0].Empleado_cliente.NumNotaempleado >= objclient[0].Empleado_cliente.NotaEmpleadoGastadas)
                                {
                                    //RadioButton1.Enabled = true;
                                    //tipPagos.Items[2].Attributes.Add("class", "visi1");
                                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("N"));
                                    tipPagos.Items.Remove(tipPagos.Items.FindByValue("9"));
                                    ListItem item = new ListItem();
                                    item.Text = "Notas Empleado";
                                    item.Value = "N";
                                    tipPagos.Items.Add(item);
                                    //cs.RegisterStartupScript(this.GetType(), "", "rmInsPagEmpleado('1');", true);
                                    tipPagos.Focus();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Carrito", "alert('Numero de notas empleado agotadas.');", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        CLIENTE9 objclientAux;
                        objclientAux = new CLIENTE9();
                        objclientAux.Id_Cliente = -1;
                        objclientAux.Cliente = "";

                        //  RadioButton1.CssClass = "Ocultarcontrol";
                        objclient.Insert(0, objclientAux);
                        LstClientes.DataTextField = "Cliente";
                        LstClientes.DataValueField = "ID_Cliente";
                        LstClientes.DataSource = objclient;
                        LstClientes.DataBind();
                        hidIdCliente.Value = "";
                        nomcliente.CssClass = "ocul1";
                        //RadioButton1.CssClass = "VisibleControl";
                        LstClientes.CssClass = "visi1";
                        //RadioButton1.CssClass = "Ocultarcontrol";
                        Nombre.Text = "";
                        Email.Text = "";
                        Shoelover.Text = "";
                        nomcliente.Text = "";
                        CLiente.Visible = false;
                        BtnCliente.Visible = false;
                        //RadioButton1.CssClass = "Ocultarcontrol";
                        //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                        //   tipPagos.Items[2].Attributes.Add("class", "ocul1");
                        //    tipPagos.Items[3].Attributes.Add("class", "ocul1");

                    }
                }
                else
                {
                    Nombre.Text = "";
                    Email.Text = "";
                    Shoelover.Text = "";
                    nomcliente.Text = "";
                    CLiente.Visible = false;
                    //tipPagos.Items[2].Attributes.Add("class", "ocul1");
                    //tipPagos.Items[3].Attributes.Add("class", "ocul1");
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Buscar", "alert('No se han encontrado coincidencias.');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void btnBorrarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                string idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                string carrito = Convert.ToString(Session["IdCarrito"]);
                //ACL-09-07-2014.INICIO. Si se carga un carrito desde BD, y se desea cancelar
                //se tiene que borrar de base de datos. Aunque lo debería hacer para todos los pedidos
                // una vez que los mete en session y los mete 
                ClsCapaDatos Bdatos = new ClsCapaDatos();

                Bdatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                bool res = Bdatos.EliminaCarritoBD(carrito, (string)Contexto.IdEmpleado, idMaquina);
                //ACL.FIN
                InicializarVariablesSesion();
                //para cancelar todas las operaciones de cliente 9.
                Comun.CheckPending();

                Response.Redirect("~/Inicio.aspx", true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return;
        }

        public void BorrarDatosEntrega()
        {
            txtNombreArt.Text = "";
            txtApellidosArt.Text = "";
            txtEmailArt.Text = "";
            txtDireccionArt.Text = "";
            txtInteriorArt.Text = "";
            txtExteriorArt.Text = "";
            ListEstadoArt.SelectedValue = "0";
            txtCiudadArt.Text = "";
            txtColoniaArt.Text = "";
            txtCpArt.Text = "";
            txtCelArt.Text = "";
            txtTelArt.Text = "";
            txtObservacionesArt.Text = "";
        }

        static int idCarritoDetalle;
        protected void imgEnvio_Click(object sender, ImageClickEventArgs e)
        {
            CalculoPromocion_CarritoCliente();
            cargaCarrito();
            poneTotales();
            CargarPagosCarritos();

            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow rgv = (GridViewRow)imgbtn.NamingContainer;

            idCarritoDetalle = int.Parse(gvCarrito.DataKeys[rgv.RowIndex].Values["id_carrito_detalle"].ToString());
            BorrarDatosEntrega();
            CargaEntregaArt();
            ModalPopupExtender1.Show();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            CalculoPromocion_CarritoCliente();
            cargaCarrito();
            poneTotales();
            CargarPagosCarritos();
            PanelEntregasArt.Visible = false;
        }

        protected void btnBorrarEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    CalculoPromocion_CarritoCliente();
                    cargaCarrito();
                    poneTotales();
                    CargarPagosCarritos();
                }

                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

                entrega.idCarrito = Convert.ToInt32(Session["IdCarrito"]);

                entrega.id_Carrito_Detalle = idCarritoDetalle;
                objCapaDatos.deleteDireccionArt(entrega);
                PanelEntregasArt.Visible = false;


            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }

        protected void btnGuardarDir_Click(object sender, EventArgs e)
        {
            try
            {
                CalculoPromocion_CarritoCliente();
                cargaCarrito();
                poneTotales();
                CargarPagosCarritos();
                ClsCapaDatos objCapaDatos = new ClsCapaDatos();
                objCapaDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                DLLGestionVenta.Models.ENTREGA_CARRITO entrega = new DLLGestionVenta.Models.ENTREGA_CARRITO();

                entrega.idCarrito = Convert.ToInt32(Session["IdCarrito"]);
                entrega.Nombre = txtNombreArt.Text;
                entrega.Apellidos = txtApellidosArt.Text;
                entrega.Email = txtEmailArt.Text;
                entrega.Direccion = txtDireccionArt.Text;
                entrega.NoInterior = txtInteriorArt.Text;
                entrega.NoExterior = txtExteriorArt.Text;
                entrega.Estado = ListEstadoArt.SelectedValue;
                entrega.Ciudad = txtCiudadArt.Text;
                entrega.Colonia = txtColoniaArt.Text;
                entrega.CodPostal = txtCpArt.Text;
                entrega.TelfMovil = txtCelArt.Text;
                entrega.TelfFijo = txtTelArt.Text;
                entrega.Referencia = txtObservacionesArt.Text;
                entrega.id_Carrito_Detalle = idCarritoDetalle;
                objCapaDatos.GuardarDireccionArticulo(entrega);

                PanelEntregasArt.Visible = false;

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }

        protected void bntShowPago_Click(object sender, EventArgs e)
        {
            //divPnlPagoCte.Style.Remove("display");
            divPnlPagoCte.Visible = true;
        }

        protected void txtemail_TextChanged(object sender, EventArgs e)
        {
            Session["clientEmail"] = txtemail.Text;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio);
        }

        //protected void lstTipoMIT_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    mitCard = lstTipoMIT.SelectedValue.ToString();
        //    mit = lstTipoMIT.SelectedItem.ToString();
        //}

        //protected void cboPlazoNormal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    merchId = cboPlazoNormal.SelectedValue.ToString();
        //    merchVal=cboPlazoNormal.SelectedItem.ToString();
        //}

        //protected void cboPlazoAmex_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    merchId = cboPlazoAmex.SelectedValue.ToString();
        //    merchVal = cboPlazoAmex.SelectedItem.ToString();
        //}

        //
        //private void añadirCarrito()
        //{
        //    string idUsuario = Contexto.IdEmpleado;
        //    string idTerminal = Contexto.IdTerminal;
        //    string idTienda = Contexto.IdTienda;
        //    string idCliente = "0";
        //    string strError = "";
        //    int idCarrito = -1;


        //    //ACL AÑADIMOS EL CARRITO
        //    if (Session["IdCarrito"] == null && txtBuscar.Text != "")
        //    {
        //        DLLGestionVenta.ProcesarVenta ArtiV = new DLLGestionVenta.ProcesarVenta();
        //        ArtiV.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();



        //        idCarrito = ArtiV.AniadeCarrito(idCliente, idUsuario, idTerminal);
        //        Session["idCarrito"] = idCarrito;
        //    }
        //    //AÑADIMOS EL ARTICULO AL CARRITO
        //    if (Session["idCarrito"] != null && txtBuscar.Text != "")
        //    {
        //        idCarrito = Convert.ToInt32(Session["IdCarrito"].ToString());

        //        //logC.Error("Vamos a añadir el articulo al carrito");
        //        DLLGestionVenta.ProcesarVenta ArtiV = new DLLGestionVenta.ProcesarVenta();
        //        ArtiV.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
        //        if (ArtiV.AniadeArticuloCarrito(idCarrito, this.txtBuscar.Text, idTienda, idUsuario, ref strError) == 2)
        //        {
        //            this.txtBuscar.Text = "";
        //            //logC.Error("Articulo añadido al carrito");
        //            lnkCarrito.Visible = (Session["IdCarrito"] != null);
        //            lnkCarrito.PostBackUrl = Constantes.Paginas.Carrito;

        //        }
        //        else
        //        {
        //            this.txtBuscar.Text = "";
        //            string error = strError.Substring(4, strError.Length - 4);
        //            String script = String.Empty;
        //            //  logC.Error("No se pudo añadir el artículo, al carrito. " + error);
        //            script = "alert('No se pudo añadir el artículo, al carrito. " + error + "');";
        //            Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
        //        }
        //        this.txtBuscar.Focus();

        //    }



        //}
        //protected void lnkAniadir_Click(object sender, EventArgs e)
        //{
        //    añadirCarrito();

        //    return;

        //}
        //protected void lnkBuscar_Click(object sender, EventArgs e)
        //{

        //    string cad1 = "";
        //    string cad2 = "";


        //    int i = txtBuscar.Text.IndexOf('*');
        //    cad1 = txtBuscar.Text.Split('*')[0].ToString();
        //    if (i > -1)
        //        cad2 = txtBuscar.Text.Split('*')[1].ToString();

        //    //Insertar estadística
        //    Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);

        //    //Response.Redirect("StockEnTienda.aspx?Producto=" + cad1 + "&Talla=" + cad2);
        //    //Dirección a la que tiene qeu reenviar EleccionProducto
        //    string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);

        //    //Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
        //    //EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
        //    string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
        //                                 "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

        //    string sz = string.Format("{0}?{1}={2}&{3}={4}", Constantes.Paginas.EleccionProducto, Constantes.QueryString.FiltroArticulo,
        //        cad1, Constantes.QueryString.ReturnUrl, returnUrl);

        //    Response.Redirect(sz, true);
        //    return;

        //}

    }
}