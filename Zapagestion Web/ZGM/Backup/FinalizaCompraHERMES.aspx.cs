using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos;
using System.Data;
using System.Globalization;

namespace AVE
{
    public partial class FinalizaCompraHERMES : CLS.Cls_Session
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        DLLGestionVenta.ProcesarVenta objVenta;
        DataSet DsPromo;

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

        private string TipoEnvioQueryString
        {
            get
            {
                string tipoEnvio = Request.QueryString["TipoEnvio"];

                if (tipoEnvio != null)
                    return tipoEnvio;
                else
                    return null;
            }
        }

        private decimal GastosEnvioAppSettings
        {
            get
            {
                decimal gastosEnvio;
                if (decimal.TryParse(System.Configuration.ConfigurationManager.AppSettings["GastosEnvioHERMES"].ToString(), out gastosEnvio))
                    return gastosEnvio;
                else
                    return 0;
            }
        }

        private string UrlHermesSession
        {

            get
            {
                if (Session["UrlHermes"] != null && !string.IsNullOrEmpty(Session["UrlHermes"].ToString()))


                    return Session["UrlHermes"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))

                    Session["UrlHermes"] = value.IndexOf("http") <= 0 ? "http://" + value : value;
                else
                    Session["UrlHermes"] = null;
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
        //private int ObtenerCliente() {
        //    int resultado = 0;
        //    SqlConnection myConnection;
        //    SqlCommand myCommand;
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
        //    string sql = "SELECT id_Cliente FROM AVE_CARRITO WHERE IdCarrito = " + IdCarritoQueryString.ToString();
        //    myConnection = new SqlConnection(connectionString);
        //    try
        //    {
        //        myConnection.Open();
        //        myCommand = new SqlCommand(sql, myConnection);
        //        resultado = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally {
        //        myConnection.Close();
        //    }

        //    return resultado;
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            //int IdCliente;
            if (!IsPostBack)
            {
                DataSet dsTienda;
                FinalizaVenta fVenta;
                GetDatosPedido();
                CargaCarrito();
               // IdCliente=ObtenerCliente();
                PoneTotales();
                cmdInicio.Text = "Nueva " + System.Environment.NewLine + "Compra";
                //divBotonHermes.InnerText = "Nueva " + System.Environment.NewLine + "Compra";

                if (TipoEnvioQueryString.ToString() != "D")
                {
                    decimal gastosEnvio = 0;
                    divDatosEntregaTienda.Visible = true;
                    dsTienda = GetDatosEntregaTienda(TipoEnvioQueryString.ToString());
                    SetDatosEntregaTienda(dsTienda);
                    lblGastosEnvioRecurso.Visible = true;
                    lblGastosEnvio.Text = FormateaNumero(gastosEnvio.ToString());

                }
                else
                {
                    divDatosEntregaDomicilio.Visible = true;
                    lblGastosEnvioRecurso.Visible = true;
                    lblGastosEnvio.Text = FormateaNumero(GastosEnvioAppSettings.ToString());
                    GetDatosEntregaDomicilio();
                }

                if (Session["FVENTA"] != null)
                {
                    fVenta = (FinalizaVenta)Session["FVENTA"];

                    //NumTicket.Text = fVenta.Ticket;
                    //NomCliente.Text = fVenta.cliente;
                    //Entrega.Text = fVenta.Entrega;

                    Session["FVENTA"] = null;
                    Session["objCliente"] = null;
                }

            }
            
        }

        //protected void cmdImprimirTicket_Click(object sender, EventArgs e)
        //{

        //    string sTipoInforme = "Ticket";

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString))
        //    {
        //        cn.Open();

        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {

        //            //cmd.CommandText = string.Format("Select top 1 isnull(ImporteEuro,0) from N_TICKETS WHERE id_ticket = {0}", NumTicket.Text);
        //            //cmd.CommandType = System.Data.CommandType.Text;
        //            //float Total = (float)cmd.ExecuteScalar();

        //            cmd.CommandText = "AVE_PRINT_VENTA_DETALLES";
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            cmd.Parameters.Add(new SqlParameter("@SESSIONID", Session.SessionID));
        //            cmd.Parameters.Add(new SqlParameter("@TDA", AVE.Contexto.IdTienda));
        //            cmd.Parameters.Add(new SqlParameter("@TICK", NumTicket.Text));
        //            cmd.Parameters.Add(new SqlParameter("@TICK_SIMPLIF", "0"));
        //            cmd.Parameters.Add(new SqlParameter("@POS_MONEDA", "IZQ"));
        //            cmd.Parameters.Add(new SqlParameter("@MONEDA", "$"));
        //            cmd.Parameters.Add(new SqlParameter("@TEXTOPAGO", ""));
        //            cmd.Parameters.Add(new SqlParameter("@FECHA", AVE.Contexto.FechaSesion.ToString("dd/MM/yyyy")));
        //            cmd.ExecuteNonQuery();

        //            // Vista del ticket.
        //            cmd.CommandText = string.Format("SELECT * FROM PR_VIEW_TICKET_{0}", Session.SessionID.ToString());
        //            cmd.CommandType = System.Data.CommandType.Text;
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                // Cliente 9
        //                if (reader["LineaC9"] == null || reader["LineaC9"].ToString().Length > 0)
        //                {
        //                    sTipoInforme = "TicketC9";
        //                }
        //            }
        //        }
        //        cn.Close();
        //    }

        //    string szURL = string.Format("~/ImprimirTicket.aspx?Tipo={0}&idTicket={1}", sTipoInforme, NumTicket.Text);
        //    Response.Redirect(szURL, false);
        //    return;

        //}

        private void GetDatosEntregaDomicilio()
        {

            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            DLLGestionVenta.Models.ENTREGA_CARRITO entrega = objDatos.ObtenerEntregaCarrito((int)IdCarritoQueryString);

            lblDireccionEntrega.Text = entrega.Direccion + "  " + entrega.NoExterior + " - " + entrega.NoInterior;
            lblTelefonoFijoEntrega.Text = "Tel " + entrega.TelfFijo;
            lblTelefonoMovilEntrega.Text = "Cel " + entrega.TelfMovil;
            lblCiudadEntrega.Text = entrega.Ciudad;
            lblColoniaEntrega.Text = entrega.Colonia;
            lblCodigoPostalEntrega.Text = entrega.CodPostal;
            lblEstadoEntrega.Text = entrega.Estado;
            lblEmailEntrega.Text = "Email " + entrega.Email;
        }


        private DataSet GetDatosEntregaTienda(string idTienda)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            DataSet dsFormasPago;
            SqlDataAdapter adapter;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            string strSql = "SELECT Direccion, Localidad, Telefono, Fax, CodPostal, Loc, Email, Observaciones FROM TIENDAS WHERE IdTienda = '" + idTienda + "'";
            myCommand = new SqlCommand(strSql, myConnection);

            dsFormasPago = new DataSet();
            adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(dsFormasPago, "FormasPago");
            myConnection.Close();

            return dsFormasPago;
        }

        private void SetDatosEntregaTienda(DataSet dsTienda) { 
        
            DataTable  dt =  dsTienda.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                lblDireccionEntregaTienda.Text = row["Direccion"].ToString();
                lblTelefonoFijoEntregaTienda.Text = "Tel " + row["Telefono"].ToString();
                lblTelefonoMovilEntregaTienda.Text = "Fax " + row["Fax"].ToString();
                lblCiudadEntregaTienda.Text = row["Localidad"].ToString();
                lblColoniaEntregaTienda.Text = row["Loc"].ToString();
                lblCodigoPostalEntregaTienda.Text = row["CodPostal"].ToString();
                lblEstadoEntregaTienda.Text = row["Observaciones"].ToString();
                lblEmailEntregaTienda.Text = "Email " + row["Email"].ToString();
            }
        }

        private void GetDatosPedido()
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            DataSet dsDatosPedido;
            SqlDataAdapter adapter;

            int idCliente;
            string pedido;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            int idEmpleado = Int32.Parse(Contexto.IdEmpleado);
            GetNombreApellidosEmpleado(idEmpleado);

            myConnection = new SqlConnection(connectionString);

            myConnection.Open();
            string strSql = "SELECT CARR.IdPedidoHermes as Pedido, PED.IdEmpleado as Empleado , CARR.id_Cliente as Cliente ";
            strSql = strSql + "FROM AVE_CARRITO_LINEA LINEA INNER JOIN AVE_PEDIDOS PED ON LINEA.idPedido = PED.IdPedido ";
            strSql = strSql + "INNER JOIN AVE_CARRITO CARR ON LINEA.id_Carrito = CARR.IdCarrito WHERE CARR.IdCarrito = " + IdCarritoQueryString.ToString();
            myCommand = new SqlCommand(strSql, myConnection);

            dsDatosPedido = new DataSet();
            adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(dsDatosPedido, "DatosPedido");
            myConnection.Close();

            DataTable dt = dsDatosPedido.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                pedido = Convert.ToString(row["Pedido"]);
                idCliente = Int32.Parse(row["Cliente"].ToString());
                GetNombreApellidosCliente(idCliente);
                lblNumeroPedido.Text = pedido;
            }

        }

        private void GetNombreApellidosCliente(int IdCliente)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string cliente;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Nombre, Apellidos FROM N_CLIENTES_GENERAL WHERE id_Cliente = " + IdCliente;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            SqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                cliente = Convert.ToString(reader[0]) + " " + reader[1];
                lblCliente.Text = cliente;
            }
        }

        private void GetNombreApellidosEmpleado(int IdEmpleado)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            string empleado;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();


            string sql = "SELECT Nombre, Apellidos FROM Empleados WHERE IdEmpleado = " + IdEmpleado;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            SqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                empleado = Convert.ToString(reader[0]) + " " + reader[1];
                lblAsesorVentas.Text = empleado;
            }
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

                if (IdCarritoQueryString != null)
                {
                    objVenta = new DLLGestionVenta.ProcesarVenta();
                    objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                   /* if (Session["EsDirectivo"] != null)
                    {
                        if (Session["EsDirectivo"].ToString() == "0")
                        {
                            DsPromo = objVenta.GetPromoCarritoLinea(Convert.ToInt64(IdCarritoQueryString));
                        }
                    }
                    else*/
                        DsPromo = objVenta.GetPromoCarritoLinea(Convert.ToInt64(IdCarritoQueryString));

                    AVE_CarritoObtener.SelectParameters["IdCarrito"].DefaultValue = IdCarritoQueryString.ToString();
                    if (Session["IdCarrito"]!= null)
                        AVE_CarritoObtenerDirec.SelectParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                    else
                        AVE_CarritoObtenerDirec.SelectParameters["IdCarrito"].DefaultValue = IdCarritoQueryString.ToString();
                   /* if (Session["EsDirectivo"] != null)
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

                }
            }
            catch (Exception e)
            {
                log.Error(e);
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
                rutaLocal = UrlHermesAppSettings + string.Format(ConfigurationManager.AppSettings["Foto.RutaLocalHermes"], GetReferencia(idArticulo));


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

                return referencia;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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

        public string getIdArticulo(string idLineaCarrito)
        {
            string idArticulo = string.Empty;
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                string sql = "SELECT idArticulo FROM AVE_CARRITO_LINEA WHERE id_Carrito_Detalle = " + idLineaCarrito;

                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                myCommand = new SqlCommand(sql, myConnection);
                idArticulo = myCommand.ExecuteScalar().ToString();

                return idArticulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
                    Panel divforanea = (Panel)e.Row.FindControl("divforanea");
                    DropDownList ddlSelecionarPromocion = (DropDownList)e.Row.FindControl("ddlSelecionarPromocion");


                    float fImporteDescuento = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DTOArticulo"));
                    float fPrecioORI = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "PVPORI"));
                    float fprecioAct = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "PVPACT"));
                    float fDescuentoPromo = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DTOPROMO"));
                    float fDescuento = 0;

                    String idTienda = DataBinder.Eval(e.Row.DataItem, "IdTienda").ToString();
                    String IdLineaCarrito = gvCarrito.DataKeys[e.Row.DataItemIndex].Value.ToString();


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
                if (GetGastosEnvio(IdCarritoQueryString) > 0)
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
                        //divResumenPago.Visible = false;
                        #region BT Quitar Enviar a POS
                        //divenviaPos.Visible = false; 
                        #endregion
                        //divFormaPagoCliente.Visible = false;
                        //divPendientePago.Visible = false;
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

        protected void cmdInicio_Click(object sender, EventArgs e)
        {
            try
            {

                log.Error("el UrlHermesQueryString es:" + UrlHermesQueryString);
                
                Response.Redirect(UrlHermesQueryString);
               // Server.Transfer(UrlHermesQueryString);
            }
            catch (Exception ex) { log.Error("exception cmdInicio:" + ex.Message.ToString()); }
        }

    }
}