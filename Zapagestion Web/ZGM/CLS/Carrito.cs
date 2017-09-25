using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;
using DLLGestionVenta.Models;

namespace AVE.CLS
{
    [Serializable]
    public class CCarrito
    {

        // TODO: Sacar esta region a otros archivos
        #region "Clases auxiliares" 
        [Serializable]
        public class CLineaCarrito
        {
            public int IdCarritoDetalle { get; set; }
            public int IdCarrito { get; set; }
            public int IdArticulo { get; set; }
            public int idCabeceroDetalle { get; set; }
            public int Cantidad { get; set; }
            public float PVPORI { get; set; }
            public float PVPACT { get; set; }
            public float DTOArticulo { get; set; }
            public int IdPromocion { get; set; }
            public int IdPedido { get; set; }
            public int IdNombreTalla { get; set; }
            public string Descripcion { get; set; }
            public string Color { get; set; }
            public string ModeloProveedor { get; set; }
            public int IdTemporada { get; set; }
            public int IdProveedor { get; set; }
            public int IdTienda { get; set; }
            public float DtoPromo { get; set; }
            public string Promocion { get; set; }
            public string TipoArticulo { get; set; }
        }

        [Serializable]
        public class CDatosCliente9
        {
            public double pares { get; set; }
            public int paresRegalado { get; set; }
            public double Bolsa { get; set; }
            public int BolsaRegalado { get; set; }
            public double ParesAcumulados { get; set; }
            public double BolsasAcumuladas { get; set; }
            public double Puntos9 { get; set; }

            public int TotalPagosPar9 {get; set;}
            public int TotalPagosBolsa5 { get; set; }
            public int TotalPagosPuntos9 {get; set;}

            public CLIENTE9 Cliente9 { get; set; }

            public CDatosCliente9(CLIENTE9 pCliente9)
            {
                Cliente9 = pCliente9;
            }

            public bool TienePar9(int idCarrito)
            { 
                bool bRet = false;
                try
                {
                    ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                    bRet = (pares > 0 && c9.GetPAR9(idCarrito));
                }
                catch (Exception ex)
                { 
                    // TODO: Manejar posible erorr del webservice
                }
                return bRet;
            }

            public bool TieneBolsas(int idCarrito)
            { 
                bool bRet = false;
                try
                {
                    ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                    bRet = (Bolsa > 0 && c9.GetBOLSA5(idCarrito));
                }
                catch (Exception ex)
                { 
                    // TODO: Manejar posible erorr del webservice
                }

                return bRet;
            }

        }

        [Serializable]
        public class CDatosCarrito
        {
            public double ImporteDescuento { get; set; }
            public double SubTotal { get; set; }
            public double ImportePagar { get; set; }
            public double ImportePromociones { get; set; }

        }

        #endregion

        #region "Declaraciones"

        DataSet DsPromo;
        
        public int IdCarrito {get; set;}
        public DateTime  FechaCreacion {get; set;}
        public int Id_Cliente {get; set;}
        public string Usuario {get; set;}
        public string Maquina {get; set;}
        public string Fecha_Modificacion {get; set;}
        public int EstadoCarrito {get; set;}
        public string TarjetaCliente { get; set; }

        private CLIENTE9 objCliente;
        public CLIENTE9 DatosCliente { get { return objCliente; } }
        public CDatosCliente9 DatosCliente9 { get; set; }
        public CDatosCarrito DatosCarrito { get; set; }

        // Propiedad por ahora
        public float TotalPagado { get; set; } 

        private List<CLineaCarrito> m_Lineas;
        public List<CLineaCarrito> Lineas { get {return m_Lineas;} }

        public string ConnectionString { get; set; }

        #endregion

        public CCarrito(string szConnectionString)
        {
            m_Lineas = new List<CLineaCarrito>();
            DatosCarrito = new CDatosCarrito();
            objCliente = new CLIENTE9();
            ConnectionString = szConnectionString;
        }

        public bool CargaCarrito(object pIdCarrito)
        {
            bool bRet = true;
            int intCarrito;

            // Obtener idCarrito
            if (!int.TryParse(pIdCarrito.ToString(), out intCarrito))
            {
                bRet = false;
                return bRet;
            }

            this.IdCarrito = intCarrito;

            try
            {
                DLLGestionVenta.ProcesarVenta objVenta;
                objVenta = new DLLGestionVenta.ProcesarVenta() { ConexString = this.ConnectionString };
                DsPromo = objVenta.GetPromoCarritoLinea(this.IdCarrito);

                ClsCapaDatos objDatos = new ClsCapaDatos();
                objDatos.ConexString = this.ConnectionString;
                DataSet dsCarrito = objDatos.GetCarritoActual(this.IdCarrito);

                if (dsCarrito.Tables.Count == 0)
                {
                    bRet = false;
                }
                else
                {
                    DataRow tblCarrito = dsCarrito.Tables[0].Rows[0];
                    this.Id_Cliente = int.Parse(tblCarrito["ID_Cliente"].ToString());
                    this.Usuario = tblCarrito["Usuario"].ToString();
                    this.Maquina = tblCarrito["Maquina"].ToString();
                    this.Fecha_Modificacion = tblCarrito["Fecha_Modificacion"].ToString();
                    this.EstadoCarrito = int.Parse(tblCarrito["EstadoCarrito"].ToString());
                    this.TarjetaCliente = tblCarrito["TarjetaCliente"].ToString();

                    this.CargaLineas();

                    if (this.Id_Cliente > 0)
                    {
                        this.AñadirClientePorID(this.Id_Cliente.ToString());
                    }

                }

                bRet = true;

            }
            catch (Exception ex)
            {
                // TODO incluir en el texto de la excepción el método y la linea.
                throw new Exception(string.Format( "Error en los datos del carrito"),ex);
            }
            return bRet;
        }

        private void CargaLineas()
        {
            ClsCapaDatos objDatos = new ClsCapaDatos();
            objDatos.ConexString = this.ConnectionString;
            m_Lineas = new List<CLineaCarrito>();
            DataSet dsLineas = objDatos.GetCarritoActual(this.IdCarrito);

            double fDescuento = 0;
            double fImporteDescuento = 0;
            double fPrecioORI = 0;
            double fprecioAct = 0;
            double fDescuentoPromo = 0;

            if (dsLineas.Tables[0].Rows.Count > 0)
            {
                CLineaCarrito linea;
                foreach (DataRow dr in dsLineas.Tables[1].Rows)
                {
                    linea = new CLineaCarrito();
                    linea.IdCarritoDetalle = int.Parse(dr["id_Carrito_Detalle"].ToString());
                    linea.IdCarrito = this.IdCarrito;
                    linea.IdArticulo = int.Parse(dr["IdArticulo"].ToString());
                    linea.idCabeceroDetalle = int.Parse(dr["Id_cabecero_detalle"].ToString());
                    linea.Cantidad = int.Parse(dr["Cantidad"].ToString());
                    linea.PVPORI = float.Parse(dr["PVPORI"].ToString());
                    linea.PVPACT = float.Parse(dr["PVPACT"].ToString());
                    linea.DTOArticulo = float.Parse(dr["DTOArticulo"].ToString());
                    linea.IdPromocion = int.Parse(dr["IdPromocion"].ToString());
                    linea.IdPedido = int.Parse(dr["IdPedido"].ToString());
                    //linea.IdNombreTalla = int.Parse(dr["IdNombreTalla"].ToString());
                    //linea.Descripcion = dr["Descripcion"].ToString();
                    //linea.Color = dr["Color"].ToString();
                    //linea.ModeloProveedor = dr["ModeloProveedor"].ToString();
                    //linea.IdTemporada = int.Parse(dr["IdTemporada"].ToString());
                    //linea.IdProveedor = int.Parse(dr["IdProveedor"].ToString());
                    //linea.IdTienda = int.Parse(dr["IdTienda"].ToString());
                    //linea.DtoPromo = float.Parse(dr["DtoPromo"].ToString());
                    linea.Promocion = dr["Promocion"].ToString();
                    linea.TipoArticulo = dr["TIPOCORTE"].ToString();

                    fImporteDescuento = Convert.ToDouble(dr["DTOArticulo"]);
                    fPrecioORI = Convert.ToDouble(dr["PVPORI"]);
                    fprecioAct = Convert.ToDouble(dr["PVPACT"]);
                    //fDescuentoPromo = Convert.ToDouble(dr[ "DtoPromo"]);
                    fDescuento = 100 * (fPrecioORI - fprecioAct) / fPrecioORI;

                    fDescuento = fDescuento / 100;
                    if (fImporteDescuento > 0)
                    {
                        //CultureInfo info = CultureInfo.GetCultureInfo("es-MX");
                        fDescuento = fDescuento / 100;
                        //((Label)e.Row.FindControl("lblPorDescuento")).Text = Convert.ToDouble(fDescuento).ToString("P2", info);
                    }

                    this.DatosCarrito.ImporteDescuento += (fImporteDescuento + fDescuentoPromo);
                    this.DatosCarrito.SubTotal += fPrecioORI;
                    this.DatosCarrito.ImportePagar = ((fPrecioORI - fImporteDescuento) - fDescuentoPromo);
                    this.DatosCarrito.ImportePromociones += 0;
                    m_Lineas.Add(linea);
                }
            }
        }

        public DataSet CargaPagos()
        {
            DataSet Ds;

            DLLGestionVenta.ProcesarVenta objVenta = new DLLGestionVenta.ProcesarVenta();
            objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            Ds = objVenta.GetPago(this.IdCarrito);

            float total = 0;

            if (Ds.Tables.Count > 0)
            {
                
                //this.DatosCliente9.TotalPagosPar9 = (from DataRow Dr in Ds.Tables[0].Rows where Dr["Tipo"].ToString().ToUpper().Equals("PAR 9") select Dr).Count();
                //this.DatosCliente9.TotalPagosBolsa5 = (from DataRow Dr in Ds.Tables[0].Rows where Dr["Tipo"].ToString().ToUpper().Equals("BOLSA 5") select Dr).Count();
                //this.DatosCliente9.TotalPagosPuntos9 = (from DataRow Dr in Ds.Tables[0].Rows where Dr["Tipo"].ToString().ToUpper().Equals("PUNTOS 9") select Dr).Count();

                foreach (DataRow Dr in Ds.Tables[0].Rows)
                {
                    total  += float.Parse(Dr["Importe"].ToString());

                    if (Dr["Tipo"].ToString().Equals("PAR 9"))
                    {
                        this.DatosCliente9.TotalPagosPar9++;
                    }
                    if (Dr["Tipo"].ToString().Equals("BOLSA 5"))
                    {
                        this.DatosCliente9.TotalPagosBolsa5++;
                    }
                    if (Dr["Tipo"].ToString().Equals("PUNTOS 9"))
                    {
                        this.DatosCliente9.TotalPagosPar9++;
                    }
                }

                DataRow row = Ds.Tables[0].NewRow();
                row["Tipo"] = "Total Pagado:";
                row["Importe"] = total;
                this.TotalPagado = total;

                Ds.Tables[0].Rows.Add(row);
                Ds.AcceptChanges();

            }
            else
            {
                //divResumen.Visible = false;
            }

            return Ds;

            //float fSubtotal = 0;
            //float fDescuento = 0;
            //float fTotalPagar = 0;

            //if (float.TryParse(ViewState["SubTotal"].ToString(), out fSubtotal))
            //{
            //    fDescuento = float.Parse(ViewState["ImporteDescuentos"].ToString());
            //    fTotalPagar = (fSubtotal - fDescuento);
            //}

            //float fTotal = (fTotalPagar - TotalPagado);
            //TotPendiente.Text = FormateaNumero(fTotal.ToString());

            //if (fTotal == 0 && fTotalPagar > 0)
            //{
            //    Divfinalizar.Visible = true;
            //    RadioButtonlTipoPago.CssClass = "ocul1";
            //    RadioButton1.CssClass = "Ocultarcontrol";
            //    btnEnviarPOS.Visible = false;
            //    RadioButton1.CssClass = "Ocultarcontrol";
            //}

            //if (Session["ClienteNine"] != null)
            //{
            //    int pares;
            //    int paresRegalado;
            //    int Bolsa;
            //    int BolsaRegalado;

            //    CLIENTE9 objCliente = (CLIENTE9)(Session["ClienteNine"]);

            //    if (objCliente.BolsaPagada > 0 || objCliente.ParPagado > 0 || objCliente.PuntosPagados > 0)
            //    {
            //        Label10.Text = (double.Parse(Label10.Text.ToString()) - objCliente.PuntosPagados).ToString();
            //        pares = int.Parse(Label13.Text.ToString().Split('(')[0].ToString());
            //        paresRegalado = int.Parse(Label13.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
            //        Bolsa = int.Parse(Label14.Text.ToString().Split('(')[0].ToString());
            //        BolsaRegalado = int.Parse(Label14.Text.ToString().Split('(')[1].ToString().Replace(")", ""));
            //        pares = pares - (int.Parse(objCliente.ParPagado.ToString())) * 8;
            //        paresRegalado = (paresRegalado > 0 ? paresRegalado - int.Parse(objCliente.ParPagado.ToString()) : 0);
            //        Bolsa = Bolsa - (int.Parse(objCliente.BolsaPagada.ToString())) * 4;
            //        BolsaRegalado = (BolsaRegalado > 0 ? BolsaRegalado - int.Parse(objCliente.BolsaPagada.ToString()) : 0);
            //        Label11.Text = (paresRegalado > 0 ? Label11.Text : "0");
            //        Label12.Text = (BolsaRegalado > 0 ? Label12.Text : "0");
            //        Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
            //        Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";
            //        //VALIDAMOS PAR9 
            //        if (Int64.Parse(paresRegalado.ToString()) > 0)
            //        {
            //            RadioButton3.Enabled = true;
            //        }
            //        else
            //        {
            //            RadioButton3.Enabled = false;
            //        }
            //        //VALIDAMOS BOLSA5
            //        if (Int64.Parse(BolsaRegalado.ToString()) > 0)
            //        {
            //            RadioButton4.Enabled = true;
            //        }
            //        else
            //        {
            //            RadioButton4.Enabled = false;
            //        }
            //    }
            //}

        }

        public void BorrarLinea(int idLinea)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "AVE_eliminaLineaCarrito";
                    cmd.Parameters.AddWithValue("@IdLineaCarrito", idLinea);
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }

        public bool TarjetaValida(string sTarjeta)
        {
            bool bRet = false;

            if (sTarjeta.Length == 16)
            {
                try
                {
                    ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                    bRet = c9.GetblnHomologadoC9(sTarjeta, this.DatosCliente.Id_Cliente);

                }
                catch (Exception ex)
                {
                    // Posible tarjeta no homologada

                    // Propagación de mensajes 
                }
            }

            return bRet;

        }

        // Recibe un texto e intenta cargar el cliente desde el.
        // TODO: Sobrecargas para saber que se está buscando.
        // Devuelve el numero de clientes que se encontraron, y la lista objclient rellena en caso de haber varios
        public int AñadirCliente(string sTexto, ref List<CLIENTE9> pObjListaClientes)
        {
            int iRetVal;
            DLLGestionVenta.ProcesarVenta objVenta;
            objVenta = new DLLGestionVenta.ProcesarVenta();
            objVenta.ConexString = this.ConnectionString;

            pObjListaClientes = objVenta.GetCliente(sTexto, Contexto.FechaSesion, this.IdCarrito, Contexto.IdTienda);
            iRetVal = pObjListaClientes.Count;

            if (pObjListaClientes.Count > 0)
            {
                if (pObjListaClientes.Count == 1)
                {

                    objCliente = pObjListaClientes[0];

                    //CalculoPromocion_Carrito();
                    //cargaCarrito();
                    //poneTotales();
                    //CargarPagosCarritos();
                    //Nombre.Text = objclient[0].Cliente;
                    //Email.Text = objclient[0].Email.ToString();
                    //txtemail.Text = objclient[0].Email.ToString();
                    //Shoelover.Text = objclient[0].NivelActual;
                    //nomcliente.Text = objclient[0].Id_Cliente.ToString();
                    //CLiente.Visible = true;
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //LstClientes.CssClass = "ocul1";
                    //nomcliente.CssClass = "visi1";
                    //BtnCliente.Visible = true;

                    if (pObjListaClientes[0].NivelActual.ToString().Length > 0)
                    {
                        // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
                    }
                    else
                    {
                        // RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                    }


                    if (pObjListaClientes[0].Empleado_cliente != null)
                    {
                        if (pObjListaClientes[0].Empleado_cliente.EsEmpleado)
                        {
                            //RadioButton1.CssClass = "VisibleControl";
                            //RadioButton1.Enabled = false;
                            //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
                            //HiddenTipoCliente.Value = "E";
                            //if (objclient[0].Empleado_cliente.NumNotaempleado >= objclient[0].Empleado_cliente.NotaEmpleadoGastadas)
                            //{
                            //    RadioButton1.Enabled = true;
                            //    RadioButton1.Focus();
                            //}
                        }
                    }
                }
                else
                {
                    CLIENTE9 objclientAux;
                    objclientAux = new CLIENTE9();
                    objclientAux.Id_Cliente = -1;
                    objclientAux.Cliente = "";

                    //RadioButton1.CssClass = "Ocultarcontrol";
                    
                    pObjListaClientes.Insert(0, objclientAux);
                    objCliente = pObjListaClientes[0];

                    //LstClientes.DataTextField = "Cliente";
                    //LstClientes.DataValueField = "ID_Cliente";
                    //LstClientes.DataSource = objclient;
                    //LstClientes.DataBind();
                    //nomcliente.CssClass = "ocul1";
                    //RadioButton1.CssClass = "VisibleControl";
                    //LstClientes.CssClass = "visi1";
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //Nombre.Text = "";
                    //Email.Text = "";
                    //Shoelover.Text = "";
                    //nomcliente.Text = "";
                    //CLiente.Visible = false;
                    //BtnCliente.Visible = false;
                    //RadioButton1.CssClass = "Ocultarcontrol";
                    //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");

                }
            }
            else
            {
                objCliente = new CLIENTE9();
                //Nombre.Text = "";
                //Email.Text = "";
                //Shoelover.Text = "";
                //nomcliente.Text = "";
                //CLiente.Visible = false;
                //RadioButton1.CssClass = "Ocultarcontrol";
                //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "ocul1");
            }


            return iRetVal;
        }

        private void AñadirClientePorID(string pszIdCliente)
        {
            // llamo al otro método, con más tiempo hago una query... xD
            List<CLIENTE9> inservibleList = new List<CLIENTE9>();

            int retVal = this.AñadirCliente(pszIdCliente, ref inservibleList);

        }

        private bool AñadirCliente9(string sTexto)
        {
            bool bRet = false;
            try
            {

                ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
                ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();

                cb.idTargeta = sTexto;
                cb.idTienda = AVE.Contexto.IdTienda;
                cb.idTerminal = AVE.Contexto.IdTerminal;

                CLIENTE9 cliente9 = new CLIENTE9();
            
                String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
                if (!String.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("Puntos Net", "Cliente 9");
                    bRet = false;
                }
                else
                {
                    //if (!c9.GetblnHomologadoC9(sTexto, Cliente.Id_Cliente))
                    //{
                    //    throw new
                    //        Exception("Tarjeta no homologada en Zapagestion.");
                    //}
                    //else
                    //{
                        cliente9.Aniversario = cb.aniversario;
                        cliente9.Fecha = Convert.ToDateTime(HttpContext.Current.Session[Constantes.Session.FechaSesion].ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                        cliente9.Cumpleaños = cb.cumpleanos;
                        cliente9.Email = cb.mail;
                        cliente9.Cliente = cb.nombre;
                        cliente9.CandidataShoeLover = (cb.shoelover == "true" ? "SI" : "NO");
                        cliente9.Empleado_cliente = null;
                        cliente9.NumTarjetaNew = String.Empty;
                        cliente9.ParesRedimidos = 0;
                        cliente9.PuntosRedimidos = 0;
                        cliente9.SaldoPuntosAnt = Convert.ToDouble(cb.puntos);
                        cliente9.SaldoPuntosAct = 0;
                        cliente9.ParesRedimidos = 0;
                        cliente9.PuntosObtenidos = 0;
                        cliente9.ParesAcumuladosAnt = Convert.ToDouble(cb.paresAcumulados);
                        cliente9.NumTarjeta = cb.idTargeta;
                        cliente9.dblSaldoBolsa5 = cb.dblSaldoBolsa5;
                        cliente9.dblSaldoPares9 = cb.dblSaldoPares9;
                        cliente9.NumConfirmaBolsa5 = Int16.Parse(cb.bolsasAcumuladas);
                        cliente9.NivelActual = cb.strNivelActual;
                        cliente9.BolsasAcumuladasAnt = double.Parse(cb.bolsasAcumuladas);
                        cliente9.BenC9 = "1";
                        cliente9.BolsaPagada = 0;
                        cliente9.ParPagado = 0;
                        cliente9.PuntosPagados = 0;
                        //ACL
                        cliente9.CandidatoBasico = cb.strCandBasicoC9;
                        cliente9.CandidatoFirstShoeLover = cb.strCandFirstC9;
                    //}

                    // Beneficios cliente 9
                    int pares = 0;
                    int Bolsas = 0;
                    double paresAcumulados = (Int16.Parse(cb.paresAcumulados) - (cliente9.ParPagado * 8));
                    double bolsasAcumuladas = (Int16.Parse(cb.bolsasAcumuladas) - (cliente9.BolsaPagada * 4));

                    if (Int16.Parse(cb.paresAcumulados) > 0) { 
                        pares = (Int16.Parse(cb.paresAcumulados) / 8); 
                    }
                    if (Int16.Parse(cb.bolsasAcumuladas) > 0) { 
                        Bolsas = (Int16.Parse(cb.bolsasAcumuladas) / 4); 
                    }

                    CDatosCliente9 datoscliente9 = new CDatosCliente9(cliente9);
                    datoscliente9.pares = pares;
                    datoscliente9.Bolsa = Bolsas;

                    // puntos 9
                    //Label10.Text = (double.Parse(cb.puntos) - cliente9.PuntosPagados).ToString();
                    datoscliente9.Puntos9 = (double.Parse(cb.puntos) - cliente9.PuntosPagados);
                    //Pares acumulados
                    //Label13.Text = cb.paresAcumulados + "(" + pares.ToString() + ")";
                    datoscliente9.ParesAcumulados = double.Parse(cb.paresAcumulados);
                    // bolsas acumuladas
                    //Label14.Text = cb.bolsasAcumuladas + "(" + Bolsas.ToString() + ")";
                    datoscliente9.BolsasAcumuladas = double.Parse(cb.bolsasAcumuladas);

                    this.DatosCliente9 = datoscliente9;

                    bRet= true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                throw (ex);
            }
            return bRet;
        }
        //private void CargaDatosClienteNine()
        //{
        //    if (this.Id_Cliente != 0)
        //    {
        //        double pares;
        //        int paresRegalado;
        //        double Bolsa;
        //        int BolsaRegalado;

        //        ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
        //        ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
        //        cb.idTargeta = this.Id_Cliente.ToString();
        //        cb.idTienda = AVE.Contexto.IdTienda;
        //        cb.idTerminal = AVE.Contexto.IdTerminal;
        //        CLIENTE9 objCliente = ((CLIENTE9)Session["ClienteNine"]);

        //        //HiddenTipoCliente.Value = "9";
        //        //RadioButtonlTipoPago.Items[0].Attributes.Add("class", "visi1");
        //        //Label10.Text = (objCliente.SaldoPuntosAnt - objCliente.PuntosPagados).ToString();

        //        pares = objCliente.ParesAcumuladosAnt - (objCliente.ParPagado * 8);
        //        paresRegalado = (pares < 8 ? 0 : int.Parse(Math.Truncate(pares / 8).ToString()));
        //        Bolsa = objCliente.BolsasAcumuladasAnt - (objCliente.BolsaPagada * 4);
        //        BolsaRegalado = (Bolsa < 4 ? 0 : int.Parse(Math.Truncate(Bolsa / 4).ToString()));
        //        //Label11.Text = (paresRegalado > 0 ? objCliente.dblSaldoPares9.ToString() : "0");
        //        //Label12.Text = (BolsaRegalado > 0 ? objCliente.dblSaldoBolsa5.ToString() : "0");
        //        //Label13.Text = pares.ToString() + "(" + paresRegalado.ToString() + ")";
        //        //Label14.Text = Bolsa + "(" + BolsaRegalado.ToString() + ")";

        //        //VALIDAMOS PAR9 
        //        if (Int64.Parse(paresRegalado.ToString()) > 0 && c9.GetPAR9(Int64.Parse(Session["IdCarrito"].ToString()), Int64.Parse(pares.ToString())))
        //        {
        //            // option pares acumulados
        //            //RadioButton3.Enabled = true;
        //        }
        //        else
        //        {
        //            // option pares acumulados
        //            //RadioButton3.Enabled = false;
        //        }

        //        //VALIDAMOS BOLSA5
        //        if (Int64.Parse(BolsaRegalado.ToString()) > 0 && c9.GetBOLSA5(Int64.Parse(Session["IdCarrito"].ToString()), objCliente.NivelActual, Int64.Parse(Bolsa.ToString())))
        //        {
        //            // option bolsas acumuladas
        //            //RadioButton4.Enabled = true;
        //        }
        //        else
        //        {
        //            // option bolsas acumuladas
        //            //RadioButton4.Enabled = false;
        //        }

        //        //BCliente_Click(sender, e);

        //    }
        //}

        public void EliminarCarrito()
        { }

        public bool EliminarLineaCarrito(int idLinea)
        {
            bool bRet = true;

            try
            {
                bRet = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bRet;
        }

        public bool AñadirLineaCarrito(CLineaCarrito Linea)
        {
            return true;
        }

        public bool AñadirLineaCarrito(CLineaCarrito Linea, int idCarrito)
        { 
            return true;
        }

        public bool RecalcularCarrito(ref string sMensaje)
        {
            bool bRet = true;

            try
            {
                DLLGestionVenta.ProcesarVenta objVenta;
                objVenta = new DLLGestionVenta.ProcesarVenta();
                objVenta.ConexString = this.ConnectionString;
                sMensaje = objVenta.GetObjCarritoPromocionClientes(Int64.Parse(this.IdCarrito.ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion);
                bRet = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bRet;
        }
        
    }
}