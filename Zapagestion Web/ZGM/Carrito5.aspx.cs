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
using AVE.CLS;
using DLLGestionVenta;

namespace AVE
{
    public partial class Carrito5 : CLS.Cls_Session
    {

        #region "Private"

        DataSet DsPromo;
        private const string szObjCarrito = "objCarrito";
        private const string szClienteNine = "ClienteNine";

        private CCarrito ObjCarrito
        {
            get
            {
                if (ViewState[szObjCarrito] == null)
                {
                    this.CargaCarrito();
                }
                return (CCarrito)ViewState[szObjCarrito];
            }

            set { ViewState[szObjCarrito] = value; }
        }

        private void CargaCarrito()
        { 

            CCarrito carrito = new CCarrito(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString());
            ObjCarrito = carrito;

            if (carrito.CargaCarrito(Session["idCarrito"]))
            {
                ProcesarVenta objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                RepeaterPagos.DataSource = carrito.CargaPagos();
                RepeaterPagos.DataBind();

                if (carrito.DatosCliente.Cliente != null)
                {
                    this.MostrarCliente();
                }

                this.MostrarTotales();
            }
        }

        private void BuscarCliente(string sTexto)
        {

            List<CLIENTE9> lista = new List<CLIENTE9>();
            int nClientes = ObjCarrito.AñadirCliente(sTexto, ref lista);

            switch (nClientes)
            {
                case 0:
                    MostrarMensaje("No se encontraron coincidencias", false);
                    break;
                case 1:

                    this.RecalcularCarrito();
                    this.MostrarCliente();

                    bool bTarjetaValida = false;

                    // Validamos si es tarjeta Cliente 9
                    if (sTexto.Length == 16)
                    {

                        bTarjetaValida = ObjCarrito.TarjetaValida(sTexto);
                
                        if (bTarjetaValida)
                        {
                            this.CargaCliente9(sTexto);
                        }
                        else
                        {
                            MostrarMensaje("Tarjeta no homologada en Zapagestion.", true);
                            return;
                        }
                    }

                    this.CargaCarrito();
                    break;
                default:
                    this.MostrarListaClientes(lista);
                    break;
            }
            this.SetControls();
        }

        private void RecalcularCarrito()
        {
            string sMensaje = string.Empty;
            ObjCarrito.RecalcularCarrito(ref sMensaje);

            if (sMensaje.Trim().Length > 0)
            {
                MostrarMensaje(sMensaje, false);
            }
        }

        private void MostrarMensaje(string sMensaje, bool bError)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), string.Format("alert('{0}');", sMensaje ), true);
        }

        private void MostrarListaClientes(List<CLIENTE9> lista)
        {
            cboClientes.DataTextField = "Cliente";
            cboClientes.DataValueField = "ID_Cliente";
            cboClientes.DataSource = lista;
            cboClientes.DataBind();
        }

        private void MostrarCliente()
        {
            txtBuscar.Text = ObjCarrito.DatosCliente.Cliente;

            if (ObjCarrito.DatosCliente.Empleado_cliente != null)
            {
                optNotaEmpleado.Visible = ObjCarrito.DatosCliente.Empleado_cliente.EsEmpleado;
            }
            else
            {
                optNotaEmpleado.Visible = false;
            }

            optCliente9.Visible = (Session[szClienteNine] != null);
            //if (Session[szClienteNine] != null)
            //{
            //    optCliente9.Visible = true;
            //    //this.CargaCliente9();
            //}
            //else
            //{
            //    optCliente9.Visible = false;
            //}

        }

        private void CargaCliente9(string sTarjeta)
        {

            CLIENTE9 objCliente;
            ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
            ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
            cb.idTargeta = sTarjeta;
            cb.idTienda = AVE.Contexto.IdTienda;
            cb.idTerminal = AVE.Contexto.IdTerminal;

            //nomcliente.Text = TarjetaCliente.Text.ToString();
            //GetClienteActual();
            //CalculoPromocion_CarritoCliente();
            //cargaCarrito();
            //poneTotales();
            //CargarPagosCarritos();

            String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
            if (!String.IsNullOrEmpty(ret))
            {
                ret = ret.Replace("Puntos Net", "Cliente 9");
                //Label10.Text = "0";
                //Label11.Text = "0";
                //Label12.Text = "0";
                //Label13.Text = "0(0)";
                //Label14.Text = "0(0)";
                //RadioButton3.Enabled = false;
                //RadioButton4.Enabled = false;
            }

            if (Session[szClienteNine] == null)
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
                Session[szClienteNine] = objCliente;

            }
            else
            {
                 objCliente = ((CLIENTE9)Session[szClienteNine]);
            }

            double pares;
            int paresRegalado;
            double Bolsa;
            int BolsaRegalado;

            //TarjetaCliente.Text = objCliente.NumTarjeta.ToString();
            //nomcliente.Text = TarjetaCliente.Text;
            //HiddenTipoCliente.Value = "9";

            Label10.Text = (objCliente.SaldoPuntosAnt - objCliente.PuntosPagados).ToString();

            pares = objCliente.ParesAcumuladosAnt - (objCliente.ParPagado * 8);
            paresRegalado = (pares < 8 ? 0 : int.Parse(Math.Truncate(pares / 8).ToString()));
            Bolsa = objCliente.BolsasAcumuladasAnt - (objCliente.BolsaPagada * 4);
            BolsaRegalado = (Bolsa < 4 ? 0 : int.Parse(Math.Truncate(Bolsa / 4).ToString()));

            Label11.Text = (paresRegalado > 0 ? objCliente.dblSaldoPares9.ToString() : "0");
            Label12.Text = (BolsaRegalado > 0 ? objCliente.dblSaldoBolsa5.ToString() : "0");
            Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
            Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";

            //VALIDAMOS PARES
            optParesAcumulados.Enabled = (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9(ObjCarrito.IdCarrito, Int64.Parse(pares.ToString())));

            //VALIDAMOS BOLSA5
            optBolsasAcumuladas.Enabled = (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5(ObjCarrito.IdCarrito, objCliente.NivelActual, Int64.Parse(Bolsa.ToString())));
        }

        private void MostrarTotales()
        {
            lblSubTotal.Text = FormateaNumero(ObjCarrito.DatosCarrito.SubTotal.ToString());
            lblImporteDTOs.Text = FormateaNumero(ObjCarrito.DatosCarrito.ImporteDescuento.ToString());
            lblTotalPagar.Text = FormateaNumero(ObjCarrito.DatosCarrito.ImportePagar.ToString());

            double totalPagos = ObjCarrito.TotalPagado;
            double porpagar = (ObjCarrito.DatosCarrito.ImportePagar - totalPagos);
            
            TotPendiente.Text = FormateaNumero(porpagar.ToString());
            hidPendiente.Value = porpagar.ToString();

            this.MostrarImportePorPagar();
        }

        private void MostrarImportePorPagar()
        {

            double porpagar = (ObjCarrito.DatosCarrito.ImportePagar - ObjCarrito.TotalPagado);

            hidTotal.Value = porpagar.ToString();

            if (optTarjeta.Checked || optNotaEmpleado.Checked)
            {
                txtPagar.Text = porpagar.ToString();  // FormateaNumero(ObjCarrito.DatosCarrito.ImportePagar.ToString());
            }
            else // Cliente 9
            {
                if (optPuntos9.Checked)
                {
                    txtPagar.Text = ObjCarrito.DatosCliente9.Puntos9.ToString();
                }

                if (optParesAcumulados.Checked)
                {
                    txtPagar.Text = ObjCarrito.DatosCliente9.pares.ToString();
                }

                if (optBolsasAcumuladas.Checked)
                {
                    txtPagar.Text = ObjCarrito.DatosCliente9.Bolsa.ToString();
                }
            }
        }

        private void SetControls()
        {
            // hay cliente ?
            txtBuscar.ReadOnly = (ObjCarrito.DatosCliente.Id_Cliente > 0);
            btnBuscar.Visible = (ObjCarrito.DatosCliente.Id_Cliente <= 0);
            cboClientes.Visible = (ObjCarrito.DatosCliente.Id_Cliente <= 0 && cboClientes.Items.Count > 1);

            this.MostrarPanelesPago();

        }

        private void MostrarPanelesPago()
        {
            pnlPagoTarjeta.Visible = optTarjeta.Checked;
            pnlPagoNota.Visible = optNotaEmpleado.Checked;
            pnlPagoCliente9.Visible = optCliente9.Checked;

            pnlPagar.Visible = (optTarjeta.Checked || optNotaEmpleado.Checked || optCliente9.Checked);

        }

        #endregion

        #region "MPOS"
        [WebMethod(EnableSession = true)]
        public static xmlmpos getdatosMPOS()
        {
            xmlmpos oObject = getMPOSSession("");

            return oObject;
        }

        [WebMethod(EnableSession = true)]
        public static string getdatosEncriptadosMPOS(string Amount, string Tarjeta)
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
            objPago.TipoPagoDetalle = Tarjeta;
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

            xmlmpos oObject = getMPOSSession(Amount);

            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            StringWriterUtf8 text = new StringWriterUtf8();

            xmlSerializer.Serialize(text, oObject, namespaces);

            sVd = text.ToString();
            sVd = sVd.Replace("\r\n", "");
            // sVd = R4.Encrypt(semilla,sVd); //TODO: Comprobar que esta clase RC4 funciona correctamente. 
            sVd = encripta.StringToHexString(encripta.Salaa(sVd, semilla));
            return sVd;
        }


        private static xmlmpos getMPOSSession(string Amount)
        {
            xmlmpos oObject = new xmlmpos();
            if (HttpContext.Current.Session[Constantes.Session.IdCompany] != null)
            {
                oObject.amount = Amount.Replace(",", "");
                oObject.reference = HttpContext.Current.Session["IdCarrito"].ToString() + "/" + AVE.Contexto.IdTienda + " /" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                //"5122013974"; //TODO: Falta definir como conseguir la referencia
                oObject.id_company = HttpContext.Current.Session[Constantes.Session.IdCompany].ToString();
                oObject.id_branch = HttpContext.Current.Session[Constantes.Session.IdBranch].ToString();
                oObject.cd_merchant = HttpContext.Current.Session[Constantes.Session.CdMerchant].ToString();
                oObject.currency = "MXN"; //TODO: Falta definir como conseguir la moneda
                oObject.country = HttpContext.Current.Session[Constantes.Session.Country].ToString();
                oObject.cd_user = HttpContext.Current.Session[Constantes.Session.CdUser].ToString();
                oObject.password = HttpContext.Current.Session[Constantes.Session.CdPassword].ToString();
                oObject.cd_usrtrx = "USR MPOS";
            }

            return oObject;
        }
        #endregion

#region "GridView"

        protected void Page_Load(object sender, EventArgs e)
        {

            //Session["IdCarrito"] = 288;


            if (Session["IdCarrito"] == null)
            {
                Response.Redirect("~/Inicio.aspx");
                return;
            }

            if (!Page.IsPostBack) // Primera carga
            {
                this.CargaCarrito();
            }

            this.SetControls();

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

        protected void gvCarrito_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Double fDescuento = 0;
            Double fImporteDescuento = 0;
            Double fPrecioORI = 0;
            Double fprecioAct = 0;
            double fDescuentoPromo = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

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

                Label lblaPagar = (Label)e.Row.FindControl("lblPagar");
                if (lblaPagar != null)
                {
                    lblaPagar.Text = FormateaNumero((fPrecioORI - fImporteDescuento - fDescuentoPromo).ToString());
                }
            }
        }

        protected void gvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (DsPromo == null)
            {
                DLLGestionVenta.ProcesarVenta objVenta = new ProcesarVenta();
                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                DsPromo = objVenta.GetPromoCarritoLinea(Int64.Parse(Session["IdCarrito"].ToString()));
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist;
                DataView dvLineaPromo;
                String IdLineaCarrito = gvCarrito.DataKeys[e.Row.DataItemIndex].Value.ToString();

                //para identificar el registro del carrito
                
                //e.Row.ID += "CARR-" + Session["IdCarrito"].ToString() + "-" + UniqueId;
                //UniqueId += 1;

                //Ponemos valores al boton de eliminacion
                ImageButton btn = (ImageButton)e.Row.FindControl("imgBorrar");
                if (ObjCarrito.TotalPagado > 0)
                {
                    // Si hay algún pago quitamos el botón
                    btn.Visible = false;
                }
                else
                {
                    btn.Attributes.Add("onclick", "javascript:return ValidarEliminarCarrito()");
                }

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
                        if (pnl != null)
                            lblDescriPromo.Text = "<br />";

                        pnl.Attributes.Add("style", "display:block");
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

#endregion

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarCliente(txtBuscar.Text);
        }

        protected void optTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            this.MostrarPanelesPago();
        }

        protected void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cb = (DropDownList)sender;
            string sCliente = cb.SelectedItem.Text;
            cb.Items.Clear();
            this.BuscarCliente(sCliente);
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Borrar")
            {
                ObjCarrito.BorrarLinea( int.Parse(e.CommandArgument.ToString()) );
                this.RecalcularCarrito();
                this.CargaCarrito();
                gvCarrito.DataBind();
            }

        }

        protected void btnEnviarPOS_Click(object sender, EventArgs e)
        {
            if (Session["IdCarrito"] == null)
                return;

            DataView dv;
            this.lblPOS.Text = "";
            EnviaPOS.SelectParameters["IdTerminal"].DefaultValue = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
            EnviaPOS.SelectParameters["IdTienda"].DefaultValue = HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
            EnviaPOS.SelectParameters["IdCarrito"].DefaultValue = ObjCarrito.IdCarrito.ToString(); // Session["IdCarrito"].ToString();
            EnviaPOS.SelectParameters["IdUsuario"].DefaultValue = Contexto.IdEmpleado;
            EnviaPOS.SelectParameters["dPrecio"].DefaultValue = ObjCarrito.DatosCarrito.ImportePagar.ToString();
            //if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
            //    EnviaPOS.SelectParameters["dPrecio"].DefaultValue = ViewState["ImportePagar"].ToString();
            //else
            //    EnviaPOS.SelectParameters["dPrecio"].DefaultValue = "0";
            

            dv = (DataView)EnviaPOS.Select(new DataSourceSelectArguments());
            if (dv != null && dv.Count > 0)
            {
                if (dv[0][0] != null && dv[0][0].ToString() != "")
                {
                    this.lblPOS.Text = "Terminal: " + dv[0][0].ToString().Split('/')[1] + ". "
                                        + Resources.Resource.EnEspera + ": " + dv[0][0].ToString().Split('/')[0];
                    //this.divDetalle.Visible = false;
                    this.divResumen.Visible = true;
                    Session["IdCarrito"] = null;
                    //this.divPagos.Visible = false;
                }
                else
                {
                    this.divResumen.Visible = true;
                    this.lblPOS.Text = "No se ha podido poner el ticket en espera";
                }
            }
        }

        /// <summary>
        /// evento que va a capturar el pago y registrar toda la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPagar_Click(object sender, EventArgs e)
        {
            double Num;
            Carrito_Pago objPago;

            DLLGestionVenta.ProcesarVenta objVenta = new DLLGestionVenta.ProcesarVenta();
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            objPago = new Carrito_Pago();
            ///pago por nota de empleado
            if (optNotaEmpleado.Checked)
            {
                if (double.TryParse(txtPagar.Text.ToString(), out Num))
                {
                    objPago.IdCarrito = ObjCarrito.IdCarrito;
                    objPago.TipoPago = "NOTA EMPLEADO";
                    objPago.TipoPagoDetalle = "";
                    objPago.NumTarjeta = "";
                    objPago.Importe = float.Parse(txtPagar.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                    objVenta.PagoCarrito(objPago);
                    txtPagar.Text = String.Empty;

                    //RadioButton1.Checked = false;

                    //nomcliente.Enabled = false;
                    //BtnCliente.Enabled = false;
                    //TarjetaCliente.Enabled = false;
                    //ButClient.Enabled = false;
                    //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                }

            }
            else
            {
                if (optCliente9.Checked)
                {

                    ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                    VENTA _v = new VENTA();

                    _v.Id_Tienda = AVE.Contexto.IdTienda;
                    _v.ID_TERMINAL = AVE.Contexto.IdTerminal;
                    _v.IdCajero = int.Parse(AVE.Contexto.IdEmpleado);
                    _v.Fecha = AVE.Contexto.FechaSesion;
                    _v.Id_Empleado = int.Parse(AVE.Contexto.IdEmpleado);

                    Cliente9.cls_Cliente9 C9p = new Cliente9.cls_Cliente9(_v);
                    C9p.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                    ws.cls_Cliente9.SolicitaRedencion sr = new ws.cls_Cliente9.SolicitaRedencion();

                    if (optParesAcumulados.Checked)
                    {
                        // par 9

                        sr.intTipo = 2;
                        sr.strTarjeta = ((CLIENTE9)Session[szClienteNine]).NumTarjeta;
                        sr.dblMonto = 0;
                        sr.strTienda = AVE.Contexto.IdTienda;
                        sr.idTerminal = AVE.Contexto.IdTerminal;
                        sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);
                        C9p.InvokeWS_OperacionesPendientes(2, String.Empty, true);
                        String ret = c9.InvokeWS_SolicitaRedencion(ref sr);

                        if (sr.strBitRedencionP == "1")
                        {
                            // ScriptManager.RegisterStartupScript(this, typeof(Page), "PAR9", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                            return;
                        }

                        ((CLIENTE9)Session[szClienteNine]).ParPagado = 1;


                        objPago.IdCarrito = ObjCarrito.IdCarrito;
                        objPago.TipoPago = "PAR 9";
                        objPago.TipoPagoDetalle = "";
                        objPago.NumTarjeta = sr.strNoAutorizacion;
                        objPago.Importe = float.Parse(txtPagar.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                        objVenta.PagoCarrito(objPago);
                    }
                    else
                    {
                        if (optBolsasAcumuladas.Checked)
                        {
                            // bolsas

                            sr.intTipo = 3;
                            sr.strTarjeta = ((CLIENTE9)Session[szClienteNine]).NumTarjeta;
                            sr.dblMonto = 0;
                            sr.strTienda = AVE.Contexto.IdTienda;
                            sr.idTerminal = AVE.Contexto.IdTerminal;
                            sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);
                            C9p.InvokeWS_OperacionesPendientes(3, String.Empty, true);
                            String ret = c9.InvokeWS_SolicitaRedencion(ref sr);

                            if (sr.strBitRedencionP == "1")
                            {
                                // ScriptManager.RegisterStartupScript(this, typeof(Page), "BOLSAS", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                                return;
                            }

                            objPago.IdCarrito = ObjCarrito.IdCarrito;
                            objPago.TipoPago = "BOLSA 5";
                            objPago.TipoPagoDetalle = "";
                            objPago.NumTarjeta = sr.strNoAutorizacion;
                            objPago.Importe = float.Parse(txtPagar.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                            objVenta.PagoCarrito(objPago);


                            ((CLIENTE9)Session[szClienteNine]).BolsaPagada = 1;

                        }
                        else
                        {
                            // puntos 9

                            sr.intTipo = 1;
                            sr.strTarjeta = ((CLIENTE9)Session[szClienteNine]).NumTarjeta;
                            sr.dblMonto = float.Parse(txtPagar.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));
                            sr.strTienda = AVE.Contexto.IdTienda;
                            sr.idTerminal = AVE.Contexto.IdTerminal;
                            sr.lngCajero = Int64.Parse(AVE.Contexto.IdEmpleado);
                            C9p.InvokeWS_OperacionesPendientes(1, String.Empty, true);

                            String ret = c9.InvokeWS_SolicitaRedencion(ref sr);
                            if (sr.strBitRedencionP == "1")
                            {
                                // ScriptManager.RegisterStartupScript(this, typeof(Page), "Puntos9", "alert('La solicitud de redención no se ha podido tramitar.');", true);
                                return;
                            }

                            objPago.IdCarrito = ObjCarrito.IdCarrito;
                            objPago.TipoPago = "PUNTOS NINE";
                            objPago.TipoPagoDetalle = "";
                            objPago.NumTarjeta = sr.strNoAutorizacion;
                            objPago.Importe = float.Parse(txtPagar.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                            objVenta.PagoCarrito(objPago);
                            txtPagar.Text = String.Empty;

                            ((CLIENTE9)Session[szClienteNine]).PuntosPagados = objPago.Importe;

                        }
                    }
                }

                //}
                //vaciamos controles

                // ((RadioButtonList)RadioButtonlTipoPago).ClearSelection();

            }

            this.CargaCarrito();
            this.SetControls();
            gvCarrito.DataBind();

        }


    }
}