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


namespace AVE
{
    public partial class Carrito4 : System.Web.UI.Page
    {
        DLLGestionVenta.ProcesarVenta objVenta;
        Int64 UniqueId = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["ImporteDescuentos"] = "0.0";
                ViewState["SubTotal"] = "0.0";
                ViewState["ImportePagar"] = "0.0";
                ViewState["ImportePromociones"] = "0.0";

                if (Session["IdCarrito"] != null)
                {
                    cargaCarrito();
                    poneTotales();
                    CargarPagosCarritos();
                }

                GetClientesCarrito();

                if (HiddenTipoCliente.Value.ToString() == "E" || HiddenTipoCliente.Value.ToString().Equals(""))
                {
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                }
                RadioButtonlTipoPago.Items[1].Attributes.Add("class", "Relleno1");
            }
            else
            {
                string CtrlID = string.Empty;
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



            //eventos a capturar desde javascript
            //nomcliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('ctl00_ContentPlaceHolder1_BtnCliente').click();return false;}} else {return true}; ");
            // txtPago.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('ctl00_ContentPlaceHolder1_ButPagar').click();return false;}} else {return true}; ");


            //RadioButtonlTipoPago.Attributes.Add("onclick", "HacerClickPonerTarjeta('" + RadioButtonlTipoPago.ClientID + "'," + RadioButtonlTipoPago.Items.Count + ")");


            //RadioButtonlTipoPago.Attributes.Add("onclick", string.Format("HacerClickPonerTarjeta('{0}',{1});", RadioButtonlTipoPago.ClientID, RadioButtonlTipoPago.Items.Count));
            //            ((RadioButtonList)RadioButtonlTipoPago).ClearSelection();


            // OJO CON ESTO
            divDetalle.Visible = (gvCarrito.Rows.Count > 0);

        }

        #region "Metodos Privados"

        public void GetClientesCarrito()
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
                        nomcliente.Text = Ds.Tables[0].Rows[0]["id_Cliente"].ToString();

                        if (Session["ClienteNine"] != null)
                        {
                            double pares;
                            int paresRegalado;
                            double Bolsa;
                            int BolsaRegalado;
                            ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                            //c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
                            ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
                            //cb.idTargeta = nomcliente.Text.ToString();
                            cb.idTargeta = Ds.Tables[0].Rows[0]["id_Cliente"].ToString();
                            cb.idTienda = AVE.Contexto.IdTienda;
                            cb.idTerminal = AVE.Contexto.IdTerminal;
                            CLIENTE9 objCliente = ((CLIENTE9)Session["ClienteNine"]);

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
                            if (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString()), Int64.Parse(pares.ToString())))
                            {
                                RadioButton3.Enabled = true;
                            }
                            else
                            {
                                RadioButton3.Enabled = false;
                            }

                            //VALIDAMOS BOLSA5
                            if (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString()), objCliente.NivelActual, Int64.Parse(Bolsa.ToString())))
                            {
                                RadioButton4.Enabled = true;
                            }
                            else
                            {
                                RadioButton4.Enabled = false;
                            }


                        }


                        BCliente_Click(sender, e);


                    }

                }
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
        private void cargaCarrito()
        {
            gvCarrito.DataBind();

            //DataView dv;
            //Session["IdCarrito"] = 29;
            //if (Session["IdCarrito"] != null)
            //{
            //    AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
            //    dv = (DataView)AVE_CarritoObtener.Select(DataSourceSelectArguments.Empty);
            //    this.gvCarrito.DataSource = dv;
            //    gvCarrito.DataBind();

            //}
        }
        private void poneTotales()
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
                divenviaPos.Attributes.Add("style", "float: left; height: 40px; width: 100px;");

            }
            else
            {
                divEtiqDescuento.Visible = false;
                divImporteDescuento.Visible = false;
                Descuentobr.Visible = false;
                divenviaPos.Attributes.Add("style", "float: left; height: 30px; width: 100px;");
            }
        }

        private void CargarPagosCarritos()
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
                    nomcliente.Enabled = false;
                    BtnCliente.Enabled = false;
                    //TarjetaCliente.Enabled = false;
                    //ButClient.Enabled = false;

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
                RadioButtonlTipoPago.CssClass = "ocul1";
                RadioButton1.CssClass = "Ocultarcontrol";
                btnEnviarPOS.Visible = false;
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
        #endregion
        protected void btnEnviarPOS_Click(object sender, EventArgs e)
        {
            if (Session["IdCarrito"] == null)
                return;

            DataView dv;
            this.lblPOS.Text = "";
            EnviaPOS.SelectParameters["IdTerminal"].DefaultValue = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
            EnviaPOS.SelectParameters["IdTienda"].DefaultValue = HttpContext.Current.Session[Constantes.Session.IdTienda].ToString();
            EnviaPOS.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
            EnviaPOS.SelectParameters["IdUsuario"].DefaultValue = Contexto.IdEmpleado;
            if (ViewState["ImportePagar"] != null && ViewState["ImportePagar"].ToString() != "")
                EnviaPOS.SelectParameters["dPrecio"].DefaultValue = ViewState["ImportePagar"].ToString();
            else
                EnviaPOS.SelectParameters["dPrecio"].DefaultValue = "0";

            dv = (DataView)EnviaPOS.Select(new DataSourceSelectArguments());
            if (dv != null && dv.Count > 0)
            {
                if (dv[0][0] != null && dv[0][0].ToString() != "")
                {
                    this.lblPOS.Text = "Terminal: " + dv[0][0].ToString().Split('/')[1] + ". "
                                        + Resources.Resource.EnEspera + ": " + dv[0][0].ToString().Split('/')[0];
                    this.divDetalle.Visible = false;
                    this.divResumen.Visible = true;
                    Session["IdCarrito"] = null;
                    this.divPagos.Visible = false;

                }
                else
                {
                    this.divResumen.Visible = true;
                    this.lblPOS.Text = "No se ha podido poner el ticket en espera";
                }
            }
        }

        protected void gvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //para identificar el registro del carrito
                //e.Row.ID += "CARR-" + Session["IdCarrito"].ToString() + "-" + UniqueId;
                UniqueId += 1;


                //Ponemos valores al boton de eliminacion
                ImageButton btn = (ImageButton)e.Row.FindControl("imgBorrar");

                //btn.Attributes.Add("onclick", "javascript:return ValidarEliminarCarrito()");


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
        protected void gvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int id_carrito_detalle = (int)gvCarrito.DataKeys[e.RowIndex].Value;
            //AVE_CarritoEliminar.DeleteParameters["IdLineaCarrito"].DefaultValue = id_carrito_detalle.ToString();
            //AVE_CarritoEliminar.Delete();

            int id_carrito_detalle = (int)gvCarrito.DataKeys[e.RowIndex].Value;
            AVE_CarritoObtener.DeleteParameters["IdLineaCarrito"].DefaultValue = id_carrito_detalle.ToString();
            AVE_CarritoObtener.Delete();
            AVE_CarritoObtener.DataBind();

        }
        protected void gvCarrito_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void AVE_CarritoEliminar_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            cargaCarrito();
            poneTotales();
            CargarPagosCarritos();
            CalculoPromocion_Carrito();

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio);
        }


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

        /// <summary>
        /// metodo para capturar el envneto keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BCliente_Click(object sender, EventArgs e)
        {
            objVenta = new DLLGestionVenta.ProcesarVenta();
            Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            List<CLIENTE9> objclient = objVenta.GetCliente(nomcliente.Text.ToString(), Contexto.FechaSesion, idCarrito, Contexto.IdTienda);


            if (objclient.Count > 0)
            {
                if (objclient.Count == 1)
                {
                    CalculoPromocion_Carrito();
                    cargaCarrito();
                    poneTotales();
                    CargarPagosCarritos();
                    Nombre.Text = objclient[0].Cliente;
                    Email.Text = objclient[0].Email.ToString();
                    txtemail.Text = objclient[0].Email.ToString();
                    Shoelover.Text = objclient[0].NivelActual;
                    //nomcliente.Text = objclient[0].Id_Cliente.ToString();
                    nomcliente.Text = objclient[0].Cliente;
                    CLiente.Visible = true;
                    RadioButton1.CssClass = "Ocultarcontrol";
                    LstClientes.CssClass = "ocul1";
                    nomcliente.CssClass = "visi1";
                    BtnCliente.Visible = true;

                    if (objclient[0].NivelActual.ToString().Length > 0)
                    {
                        RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                    }
                    else
                    {
                        RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    }


                    if (objclient[0].Empleado_cliente != null)
                    {
                        if (objclient[0].Empleado_cliente.EsEmpleado)
                        {
                            RadioButton1.CssClass = "VisibleControl";
                            RadioButton1.Enabled = false;
                            RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                            HiddenTipoCliente.Value = "E";
                            if (objclient[0].Empleado_cliente.NumNotaempleado >= objclient[0].Empleado_cliente.NotaEmpleadoGastadas)
                            {

                                RadioButton1.Enabled = true;
                                RadioButton1.Focus();


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
                Nombre.Text = "";
                Email.Text = "";
                Shoelover.Text = "";
                nomcliente.Text = "";
                CLiente.Visible = false;
                RadioButton1.CssClass = "Ocultarcontrol";
                RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
            }

        }
        /// <summary>
        /// evento que va a capturar el pago y registrar toda la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButPagar_Click(object sender, EventArgs e)
        {
            double Num;
            Carrito_Pago objPago;


            objVenta = new DLLGestionVenta.ProcesarVenta();
            Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            objPago = new Carrito_Pago();

            ///pago por nota de empleado
            if (RadioButton1.Checked)
            {
                if (double.TryParse(txtPago.Text.ToString(), out Num))
                {
                    objPago.IdCarrito = idCarrito;
                    objPago.TipoPago = "NOTA EMPLEADO";
                    objPago.TipoPagoDetalle = "";
                    objPago.NumTarjeta = "";
                    objPago.Importe = float.Parse(txtPago.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"));

                    objVenta.PagoCarrito(objPago);
                    txtPago.Text = String.Empty;

                    RadioButton1.Checked = false;

                    nomcliente.Enabled = false;
                    BtnCliente.Enabled = false;
                    //TarjetaCliente.Enabled = false;
                    //ButClient.Enabled = false;
                    RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
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
                if (RadioButtonlTipoPago.SelectedValue.ToString() == "9")
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

                        nomcliente.Enabled = false;
                        BtnCliente.Enabled = false;
                        //TarjetaCliente.Enabled = false;
                        //ButClient.Enabled = false;


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

                            nomcliente.Enabled = false;
                            BtnCliente.Enabled = false;
                            //TarjetaCliente.Enabled = false;
                            //ButClient.Enabled = false;
                        }
                        else
                        {
                            // puntos 9

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

                            nomcliente.Enabled = false;
                            BtnCliente.Enabled = false;
                            //TarjetaCliente.Enabled = false;
                            //ButClient.Enabled = false;


                        }
                    }
                }

                //}
                //vaciamos controles
                //  CargarPagosCarritos();
                ((RadioButtonList)RadioButtonlTipoPago).ClearSelection();
            }

            CargarPagosCarritos();

        }
        /// <summary>
        /// procedimeinto finalizar la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButFinalizarVenta_Click(object sender, EventArgs e)
        {

            String idTicket;
            CLIENTE9 C9;
            objVenta = new DLLGestionVenta.ProcesarVenta();
            Int64 idCarrito = Int64.Parse(Session["IdCarrito"].ToString());
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            objVenta.Terminal = HttpContext.Current.Session[Constantes.CteCookie.IdTerminal].ToString();
            objVenta.FechaSesion = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString());
            Application.Lock();

            C9 = new CLIENTE9();
            if (Session["ClienteNine"] != null) { C9 = (CLIENTE9)Session["ClienteNine"]; }

            idTicket = objVenta.EjecutaVenta(idCarrito, C9);

            Session["IdCarrito"] = null;

            Application.UnLock();

            FinalizaVenta obFventa;

            obFventa = new FinalizaVenta();

            obFventa.Ticket = idTicket;
            obFventa.cliente = Nombre.Text.ToString();
            obFventa.Entrega = String.Empty;

            Session["FVENTA"] = obFventa;
            Session["ClienteNine"] = null;

            Response.Redirect("FinalizaCompra.aspx");

        }

        protected void LstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            nomcliente.Text = LstClientes.SelectedValue.ToString();
            BCliente_Click(sender, e);


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

                int idCarrito = (int)Session["IdCarrito"];
                string szNombre = txtOUNombre.Text;
                string szApellidos = txtOUApellidos.Text;
                string szEmail = txtOUEmail.Text;
                string szDireccion = txtOUDireccion.Text;
                string szNoInterior = txtOUNoInterior.Text;
                string szNoExterior = txtOUNoExterior.Text;
                string szEstado = cboOUEstado.Text;
                string szCiudad = txtOUCiudad.Text;
                string szColonia = txtOUColonia.Text;
                string szCodPostal = txtOUCodigoPostal.Text;
                string szTelfMovil = txtOUTelfCelular.Text;
                string szTelfFijo = txtOUTelfFijo.Text;
                string szReferencia = txtOUReferenciaLlegada.Text;

                string szInsertEntrega = string.Format("INSERT INTO AVE_CARRITO_ENTREGAS" +
                                            "(IdCarrito, Nombre, Apellidos, Email, Direccion, NoInterior, NoExterior ,Estado ,Ciudad ,Colonia," +
                                            "CodigoPostal ,TelefonoCelular ,TelefonoFijo, ReferenciaLlegada)" +
                                            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13})",
                                            idCarrito, szNombre, szApellidos, szEmail, szDireccion, szNoInterior, szNoExterior, szEstado, szCiudad,
                                            szColonia, szCodPostal, szTelfMovil, szTelfFijo, szReferencia);

                //using (DataSet ds = objCapaDatos.GEtSQLDataset( string.Format("Select count(*) as Total from AVE_CARRITO_ENTREGAS where idCarrito = {0}", idCarrito ) ))
                //{
                //}


                objCapaDatos.ActualizarSQL(szInsertEntrega);

                cmdConfirmarEntrega.Visible = false;
                lblEntregaConfirmada.Visible = true;

            }
            catch (Exception ex)
            {
                // algo habrá que hacer aquí...
            }

        }


        protected void BC9_Click(object sender, EventArgs e)
        {

            //RadioButtonlTipoPago.Items[0].Selected = false;
            //RadioButtonlTipoPago.Items[1].Selected = false;
            //ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
            ////c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString; 
            //ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
            ////cb.idTargeta = TarjetaCliente.Text.ToString();
            //cb.idTienda = AVE.Contexto.IdTienda;
            //cb.idTerminal = AVE.Contexto.IdTerminal;
            //int pares = 0;
            //int Bolsas = 0;

            ////nomcliente.Text = TarjetaCliente.Text.ToString();
            //BCliente_Click(sender, e);


            //String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
            //if (!String.IsNullOrEmpty(ret))
            //{
            //    ret = ret.Replace("Puntos Net", "Cliente 9");
            //    Label10.Text = "0";
            //    Label11.Text = "0";
            //    Label12.Text = "0";
            //    Label13.Text = "0(0)";
            //    Label14.Text = "0(0)";
            //    RadioButton3.Enabled = false;
            //    RadioButton4.Enabled = false;

            //}
            //else
            //{
            //    if (nomcliente.Text.Length == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagadoZapa", "alert('La tarjeta no esta homolagada en Zapagestion.');", true);
            //        return;
            //    }



            //    if (c9.GetblnHomologadoC9(TarjetaCliente.Text.ToString(), Int64.Parse(nomcliente.Text.ToString())))
            //    {
            //        CLIENTE9 objCliente;
            //        if (Session["ClienteNine"] != null)
            //        {
            //            if (((CLIENTE9)Session["ClienteNine"]).NumTarjeta.ToString() != TarjetaCliente.Text.ToString())
            //            {
            //                Session["ClienteNine"] = null;
            //            }
            //        }
            //        if (Session["ClienteNine"] == null)
            //        {
            //            objCliente = new CLIENTE9();
            //            objCliente.Aniversario = cb.aniversario;
            //            objCliente.Fecha = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            //            objCliente.Cumpleaños = cb.cumpleanos;
            //            objCliente.Email = cb.mail;
            //            objCliente.Cliente = cb.nombre;
            //            objCliente.CandidataShoeLover = (cb.shoelover == "true" ? "SI" : "NO");
            //            objCliente.Empleado_cliente = null;
            //            objCliente.NumTarjetaNew = String.Empty;
            //            objCliente.ParesRedimidos = 0;
            //            objCliente.PuntosRedimidos = 0;
            //            objCliente.SaldoPuntosAnt = Convert.ToDouble(cb.puntos);
            //            objCliente.SaldoPuntosAct = 0;
            //            objCliente.ParesRedimidos = 0;
            //            objCliente.PuntosObtenidos = 0;
            //            objCliente.ParesAcumuladosAnt = Convert.ToDouble(cb.paresAcumulados);
            //            objCliente.NumTarjeta = cb.idTargeta;
            //            objCliente.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
            //            objCliente.dblSaldoPares9 = cb.dblSaldoPares9;
            //            objCliente.NumConfirmaBolsa5 = Int16.Parse(cb.bolsasAcumuladas);
            //            objCliente.NivelActual = cb.strNivelActual;
            //            objCliente.BolsasAcumuladasAnt = double.Parse(cb.bolsasAcumuladas);
            //            objCliente.BenC9 = "1";
            //            objCliente.BolsaPagada = 0;
            //            objCliente.ParPagado = 0;
            //            objCliente.PuntosPagados = 0;
            //            Session["ClienteNine"] = objCliente;

            //        }
            //        else
            //        {
            //            objCliente = (CLIENTE9)Session["ClienteNine"];
            //        }


            //        cb.paresAcumulados = (Int16.Parse(cb.paresAcumulados) - (objCliente.ParPagado * 8)).ToString();
            //        cb.bolsasAcumuladas = (Int16.Parse(cb.bolsasAcumuladas) - (objCliente.BolsaPagada * 4)).ToString();

            //        if (Int16.Parse(cb.paresAcumulados) > 0) { pares = (Int16.Parse(cb.paresAcumulados) / 8); }
            //        if (Int16.Parse(cb.bolsasAcumuladas) > 0) { Bolsas = (Int16.Parse(cb.bolsasAcumuladas) / 4); }

            //        Label10.Text = (double.Parse(cb.puntos) - objCliente.PuntosPagados).ToString();
            //        Label11.Text = (Int16.Parse(cb.paresAcumulados) > 0 ? cb.promedioPar : "0");
            //        Label12.Text = (Int16.Parse(cb.bolsasAcumuladas) > 0 ? cb.promedioBolsa : "0");
            //        Label13.Text = cb.paresAcumulados + "(" + pares.ToString() + ")";
            //        Label14.Text = cb.bolsasAcumuladas + "(" + Bolsas.ToString() + ")";

            //        //VALIDAMOS PAR9 
            //        if (Int64.Parse(pares.ToString()) > 0 & c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString())))
            //        {
            //            RadioButton3.Enabled = true;
            //            objCliente.ParValido = true;

            //        }
            //        else
            //        {
            //            RadioButton3.Enabled = false;
            //            objCliente.ParValido = false;
            //        }

            //        //VALIDAMOS BOLSA5
            //        if (Int64.Parse(Bolsas.ToString()) > 0 & c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString())))
            //        {
            //            objCliente.BolsaValido = true;
            //            RadioButton4.Enabled = true;
            //        }
            //        else
            //        {
            //            RadioButton4.Enabled = false;
            //            objCliente.BolsaValido = false;
            //        }

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagado", "alert('La Tarjeta introducida no esta homologada.');", true);
            //        //cliente
            //    }

            //}
            ////nomcliente.Text = TarjetaCliente.Text.ToString();
            ////BCliente_Click(sender, e);
            //if (Session["ClienteNine"] != null && nomcliente.Text.ToString().Length > 0) { ((CLIENTE9)Session["ClienteNine"]).Id_Cliente = int.Parse(nomcliente.Text.ToString()); }


        }

        private void CalculoPromocion_Carrito()
        {

            DLLGestionVenta.ProcesarVenta objVenta;

            if (Session["IdCarrito"] != null)
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();

                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                objVenta.GetObjCarritoPromocion(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion);
            }
        }

        protected void imgBorrar_Click(object sender, EventArgs e)
        {
            string idLineaCarrito = ((ImageButton)sender).CommandArgument;
            AVE_CarritoObtener.DeleteParameters["IdLineaCarrito"].DefaultValue = idLineaCarrito;
            AVE_CarritoObtener.Delete();
            gvCarrito.DataBind();
            poneTotales();
            CargarPagosCarritos();


        }
    }
}