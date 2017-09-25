using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLGestionVenta.Models;
using System.Drawing;
using DLLGestionCliente9.Models;
using DLLGestionCliente9;
using System.Configuration;

namespace AVE
{
    public partial class CambioPlastico : System.Web.UI.Page
    {
        DLLGestionCliente9.Models.Cliente9 client9;
        DLLGestionCliente9.Models.NuevoCambioC9 cambioC9;
        DLLGestionCliente9.ClsCliente9 objNine;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //

        DLLGestionVenta.ProcesarVenta objVenta;

        public int compruebaSlash(string ruta)
        {
            int valor = 0;

            int ultimo = ruta.Length;
            string slash = ruta.Substring(ultimo - 1, 1);
            if (slash == "/") valor = 1;

            return valor;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string  idCarrito = "";
            string Url = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "ValidaTipoCambio();", true);
            RadioButtonlTipoCambio.Attributes.Add("onclick", "ValidaTipoCambio()");
            RadioButtonlTipoCambio.Attributes.Add("onchange", "limpiaControles()");
            if (Session["IdCarrito"] != null) idCarrito = Session["IdCarrito"].ToString();
            if (ConfigurationManager.AppSettings["EntornoTR"] != null)
                if (Request.QueryString["IdCarrito"] != null) idCarrito = Request.QueryString["IdCarrito"].ToString();
            if (idCarrito !="")
            {
                string appPath = "";
                appPath = HttpContext.Current.Request.ApplicationPath;

                if (appPath == "") appPath = "/";

                if (compruebaSlash(appPath) == 0) appPath = appPath + "/";
                
                 
                int ArtiCarrito = CheckArticulosCarrito(idCarrito);
                var miMaster = (MasterPageNine)this.Page.Master;
                miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
               
                if (ConfigurationManager.AppSettings["EntornoTR"] != null)
                {
                    Url = appPath + "CarritoDetalleHermes.aspx";
                }
                else
                {
                    miMaster.CambiarEstadoImagenCarrito(true);
                    Url = appPath + "CarritoDetalle.aspx";
                }
                miMaster.AniadeRefImagenCarrito(Url);

                
            }

        }
        protected void btnBuscaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                GetClienteActual(nomcliente.Text);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
        }
        protected void tipCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hidNuevoCambio.Value = this.tipCambio.SelectedItem.Value.ToString();
        }
        protected void tipNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hidNuevoNivel.Value = this.tipNivel.SelectedItem.Value.ToString();
        }
        private int CheckCambiosEnCarrito (string idCarrito, string referencias, ref int NumArticulos){

            int result = 0;
           
            objNine = new DLLGestionCliente9.ClsCliente9();
            objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            result = objNine.ValidaCambiosTJT(idCarrito, referencias, ref NumArticulos );

            return result;
        
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
        
        public int InsertarEnCarrito(NuevoCambioC9 cambioC9, string idEmpleado, string idTerminal, string idTienda, ref string idCarrito, ref int iDuplicado )
        {
           
            int result = 0;
            
            
            string idMaquina = null;
              String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();
            try
            {
                
                 if (HttpContext.Current == null)
                 {
                        idMaquina = "127.0.0.1"; //Si llega HttpContext.Current  (Request) a null, le asignamos el valor del host local para que no salte excepcion
                 }
                 else
                 {
                        idMaquina = (string)System.Web.HttpContext.Current.Request.UserHostAddress;
                 }
                objNine = new DLLGestionCliente9.ClsCliente9();
                objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                if (Session["IdCarrito"] != null) idCarrito = Session["IdCarrito"].ToString();
                if (ConfigurationManager.AppSettings["EntornoTR"] != null)
                    if (Request.QueryString["IdCarrito"] != null) idCarrito = Request.QueryString["IdCarrito"].ToString();                      
                result = objNine.InsertaCarritoCambio(cambioC9,idEmpleado, idTerminal,idTienda, idMaquina, ref idCarrito, ref iDuplicado);

            }
            catch (Exception ex)
            {
                log.Error( ex.Message, ex);
                throw ex;
            }
           finally
            {
            }
            return result;

        }
        protected int ConsultaBeneficios(NuevoCambioC9 cambC9, ref ws.cls_Cliente9.ConsultaBeneficios cb)
        {
            int result = 0;
            ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
            cb.idTargeta = cambC9.TarjetaActual;
            cb.idTienda = AVE.Contexto.IdTienda;
            cb.idTerminal = AVE.Contexto.IdTerminal;
  
             String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

                if (!Comun.CheckURLWs(url, 10000))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                    return 0;
                }

                String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
                if (!String.IsNullOrEmpty(ret))
                {
                    result = 2;
                }
                else
                {
                    if (nomcliente.Text.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "homolagadoZapa", "alert('La tarjeta no esta homolagada en Zapagestion.');", true);
                        return 0;
                    }

                    if (c9.GetblnHomologadoC9(cambC9.TarjetaActual)) //, Int64.Parse(nomcliente.Text.ToString())))
                    {
                        result = 1;
                    }
                }
            return result;


        }
        protected int ValidaTarjeta_PuntosNet(NuevoCambioC9 cambio9, int option)
        {
            int result = 0;
            string msjValidacion="";
            client9 = new DLLGestionCliente9.Models.Cliente9();
            
            ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
            //se hace una consulta de beneficios para validar el nivel actual 
            result = ConsultaBeneficios(cambio9, ref cb);
            if (result == 1)
            { 
                //Validamos los datos recibidos en la consulta con el cambio que se quiere realizar
                if (cambio9.Referencia== "750000000161" && (cb.strNivelActual.ToUpper()) == "SHOE LOVERS")
                    msjValidacion ="No es posible vender tarjeta upgrade shoelover a un socio con nivel shoe lovers.";
                else if (cambio9.Referencia == "750000000161" && !cb.blnShoeLover)
                    msjValidacion="No es posible vender tarjeta upgrade shoelover a un socio que no es candidato a shoelover.";
                else if( cambio9.Referencia =="750000000437" && (cb.strNivelActual.ToUpper()) == "FIRST SHOELOVERS")    
                    msjValidacion ="No es posible vender tarjeta upgrade first shoelover a un socio con nivel first shoelovers.";
                else if( cambio9.Referencia =="750000000437" && cb.strCandFirstC9.ToUpper() =="NO")    
                    msjValidacion="No es posible vender tarjeta upgrade first shoelover a un socio que no es candidato a first shoelover.";
                else if(cambio9.Referencia=="750000000436" && (cb.strNivelActual.ToUpper()) == "BASICO")    
                    msjValidacion ="No es posible vender tarjeta upgrade básica a un socio con nivel básico.";
                else if(cambio9.Referencia=="750000000436" && cb.strCandBasicoC9.ToUpper() == "FALSE")
                    msjValidacion="No es posible vender tarjeta upgrade básica a un socio que no es candidato a tarjeta básica.";

                if (msjValidacion != "")
                {
                    result = 0;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Validacion Cliente 9", "alert('" + msjValidacion + "');", true);
                    
                }
                else result = 1;
            }

            return result;
        
        }
        protected void btnReemplazarTjt_Click(object sender, EventArgs e)
        {
            int result = 0;
            string idCarrito ="";
            string Url = "";
            string referencias = "'750000000291','108303000332'";
            int NumArticulos = 0;
            int iDuplicado =0;
            if (Page.IsValid)
            {
                try
                {
                    cambioC9 = new DLLGestionCliente9.Models.NuevoCambioC9();
                    if (hidIdCliente.Value != null)
                        cambioC9.IdCliente = Convert.ToInt32(hidIdCliente.Value.ToString());
                    hidNuevoCambio.Value = this.tipCambio.SelectedItem.Value;
                    cambioC9.Referencia = this.hidNuevoCambio.Value;
                    cambioC9.NuevaTarjeta = this.txtNumTarjeta.Text;
                    cambioC9.TarjetaActual = this.hidNumeroTarjetaCliente9.Value;
                    if (hidIdCliente.Value != null)
                    {
                        if(idCarrito != "")
                        result = CheckCambiosEnCarrito(idCarrito, referencias, ref  NumArticulos);
                        if (NumArticulos == 0)
                        {
                            result = InsertarEnCarrito(cambioC9, Contexto.IdEmpleado, Contexto.IdTerminal, Contexto.IdTienda, ref idCarrito, ref iDuplicado);
                            if (result > 0)
                            {
                                int ArtiCarrito = CheckArticulosCarrito(idCarrito);
                                Session["IdCarrito"] = idCarrito;
                                var miMaster = (MasterPageNine)this.Page.Master;
                                miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                                LimpiaControles();
                                ValidaControles();
                                HabilitaValidaciones();

                                string appPath = "";
                                appPath = HttpContext.Current.Request.ApplicationPath;
                                if (ConfigurationManager.AppSettings["EntornoTR"] != null)
                                {
                                   Url = "~/carritodetalleHERMES.aspx?idCarrito=" + idCarrito;
                                }
                                else
                                {
                                    miMaster.CambiarEstadoImagenCarrito(true);
                                    Url =  "~/CarritoDetalle.aspx";
                                }
                                miMaster.AniadeRefImagenCarrito(Url);
                                //ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Reemplazo de Tarjeta, ha sido añadido al Carrito');", true);
                                Response.Redirect(Url);

                            }
                            else
                            {
                                if (iDuplicado >= 1)
                                {
                                    LimpiaControles();
                                    ValidaControles();
                                    HabilitaValidaciones();

                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('No se puede solicitar más de un reemplazo en el mismo carrito.');", true);

                                }
                                else
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Error al añadir al carrito el Reemplazo de  Tarjeta.');", true);
                            }
                        }
                        else
                        {

                            //sacar alertar
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    log.Error("Excepticon btnCambiarNivel:" + ex.Message.ToString());
                }

            }
        }
        private void HabilitaValidaciones()
        {
            this.RFVApellidos.Enabled = true;
            this.RFVFecha.Enabled = true;
            this.RFVMail.Enabled = true;
            this.RFVMovil.Enabled = true;
            this.RFVNombre.Enabled = true;
            this.RFVTelefono.Enabled = true;
            this.reqApellidos.Enabled = true;
            this.reqEmail.Enabled = true;
            this.reqMovil.Enabled = true;
            this.reqNombre.Enabled = true;
            this.reqTelefono.Enabled = true;
            this.reqTJT.Enabled = true;

        }
        private void LimpiaControles()
        {
            this.txtNombre.Text = "";
            this.txtTarjetaActual.Text = "";
            this.nomcliente.Text = "";
            this.txtApellidos.Text = "";
            this.txtEmail.Text = "";
            this.txtFecNac.Text = "";
            this.txtMovil.Text = "";
            this.txtTfnoCasa.Text = "";
            this.txtNivel.Text = "";
            txtNumTarjeta.Text = "22114005";
            ValidaControles();
        }
        protected void btnCambiarNivel_Click(object sender, EventArgs e)
        {
            int result =0;
            string idCarrito = "";
            int iDuplicado = 0;
            string referencias = "'750000000161','750000000437','750000000436'";
            int NumArticulos = 0;
            
            if (Page.IsValid)
            {
                try
                {
                    if (hidEsEmpleado.Value == "0")
                    {
                        cambioC9 = new DLLGestionCliente9.Models.NuevoCambioC9();
                        if (hidIdCliente.Value != null)
                            cambioC9.IdCliente = Convert.ToInt32(hidIdCliente.Value.ToString());
                        cambioC9.Referencia = this.hidNuevoNivel.Value;
                        cambioC9.NuevaTarjeta = this.txtNumTarjeta.Text;
                        cambioC9.TarjetaActual = this.hidNumeroTarjetaCliente9.Value;
                        cambioC9.NivelAnterior = this.hidNivel.Value;
                        cambioC9.NuevoNivel = this.tipNivel.SelectedItem.Text.ToString();
                        if (hidIdCliente.Value != null)
                        {
                            if(idCarrito !="")
                                result = CheckCambiosEnCarrito(idCarrito, referencias, ref  NumArticulos);
                            if (NumArticulos == 0)
                            {
                                result = ValidaTarjeta_PuntosNet(cambioC9, 1);
                                //Se ha validado que el cliente este registrado y que los datos del cambio de nivel sean validos.
                                if (result > 0)
                                {
                                    result = InsertarEnCarrito(cambioC9, Contexto.IdEmpleado, Contexto.IdTerminal, Contexto.IdTienda, ref idCarrito, ref iDuplicado);
                                    if (result > 0)
                                    {
                                        int ArtiCarrito = CheckArticulosCarrito(idCarrito);
                                        Session["IdCarrito"] = idCarrito;

                                        var miMaster = (MasterPageNine)this.Page.Master;
                                        miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));
                                        LimpiaControles();
                                        ValidaControles();
                                        HabilitaValidaciones();
                                        string appPath = "";
                                        string Url = "";
                                        appPath = HttpContext.Current.Request.ApplicationPath;
                                        if (ConfigurationManager.AppSettings["EntornoTR"] != null)
                                        {
                                            Url = "~/carritodetalleHERMES.aspx?idCarrito=" + idCarrito;
                                        }
                                        else
                                        {
                                            miMaster.CambiarEstadoImagenCarrito(true);
                                            Url =  "~/CarritoDetalle.aspx";
                                        }
                                        miMaster.AniadeRefImagenCarrito(Url);
                                        Response.Redirect(Url);
                                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Cambio de Nivel ha sido añadido al Carrito');", true);

                                    }
                                    else
                                    {
                                        if (iDuplicado >= 1)
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('No se puede solicitar más de un reemplazo en el mismo carrito.');", true);
                                        else
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Error al intentar añadir el cambio de nivel al carrito.');", true);

                                    }
                                }
                            }
                            else 
                            { 
                            
                                //sacar alerta de que hay mas de un articulo de cambio de nivel en el carrito.
                            
                            }
                        }
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Error, El Cliente es Empleado.');", true);
                    
                    }
                }
                catch(Exception ex){
                    log.Error("Excepticon btnCambiarNivel:" + ex.Message.ToString());
                }

            }
        }
        
        protected void LstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                nomcliente.Text = LstClientes.SelectedItem.Text;
                GetClienteActual(LstClientes.SelectedValue.ToString());
                if (txtNumTarjeta.Text == "") txtNumTarjeta.Text = "22114005";
                LstClientes.Visible = false;
                nomcliente.Visible = true;

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private void ValidaControles()
        {
            Color colorCelDes = new Color();
            colorCelDes = ColorTranslator.FromHtml("#B2A8AD");
            txtNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            txtNombre.BackColor = txtNombre.Text.Length > 0 ? colorCelDes : Color.White;
            txtApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            txtApellidos.BackColor = txtApellidos.Text.Length > 0 ? colorCelDes : Color.White;
            txtEmail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtEmail.BackColor = txtEmail.Text.Length > 0 ? colorCelDes : Color.White;
            txtFecNac.Enabled = txtFecNac.Text.Length > 0 ? false : true;
            txtFecNac.BackColor = txtFecNac.Text.Length > 0 ? colorCelDes : Color.White;
            txtTfnoCasa.Enabled = txtTfnoCasa.Text.Length > 0 ? false : true;
            txtTfnoCasa.BackColor = txtTfnoCasa.Text.Length > 0 ? colorCelDes : Color.White;
            txtMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
            txtMovil.BackColor = txtMovil.Text.Length > 0 ? colorCelDes : Color.White;
            txtNivel.Enabled = txtNivel.Text.Length > 0 ? false : true;
            txtNivel.BackColor = txtMovil.Text.Length > 0 ? colorCelDes : Color.White;
            txtTarjetaActual.BackColor = txtTarjetaActual.Text.Length > 0 ? colorCelDes : Color.White;
            txtTarjetaActual.Enabled = txtTarjetaActual.Text.Length > 0 ? false : true;
        }
        private void GetClienteActual(string sCliente)
        {
            
            objNine = new DLLGestionCliente9.ClsCliente9();
            objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            List<DLLGestionCliente9.Models.Cliente9> objclient = objNine.GetClienteAdm9(sCliente, Contexto.FechaSesion);
            


            if (objclient.Count > 0)
            {
                if (objclient.Count == 1)
                {
                    txtNombre.Text = objclient[0].Nombre;
                    txtApellidos.Text = objclient[0].Apellidos;
                    txtEmail.Text = objclient[0].Email;
                    txtFecNac.Text = Convert.ToDateTime(objclient[0].FechaNacimiento.ToString()).ToShortDateString();
                    txtTfnoCasa.Text = objclient[0].Telefono.ToString();
                    txtMovil.Text = objclient[0].Movil.ToString();
                    txtNivel.Text = objclient[0].NivelActual.ToString();
                    hidNivel.Value = txtNivel.Text;
                    txtTarjetaActual.Text = objclient[0].NumTarjeta.ToString();
                    hidNumeroTarjetaCliente9.Value = objclient[0].NumTarjeta.ToString();
                    hidIdCliente.Value = objclient[0].Id_Cliente.ToString();
                    hidEsEmpleado.Value = objclient[0].esEmpleado.ToString();
                    if (txtNumTarjeta.Text == "") txtNumTarjeta.Text = "22114005";
                    ValidaControles();



                }
                else
                {
                    DLLGestionCliente9.Models.Cliente9 objclientAux;
                    objclientAux = new DLLGestionCliente9.Models.Cliente9();
                    objclientAux.Id_Cliente = -1;
                    objclientAux.Cliente = "";

                    objclient.Insert(0, objclientAux);
                    LstClientes.DataTextField = "Cliente";
                    LstClientes.DataValueField = "ID_Cliente";
                    LstClientes.DataSource = objclient;
                    LstClientes.DataBind();
                    hidIdCliente.Value = "";
                    hidEsEmpleado.Value = "";
                    nomcliente.CssClass = "ocul1";
                    LstClientes.CssClass = "visi1";
                    txtNombre.Text = "";
                    txtApellidos.Text = "";
                    txtEmail.Text = "";
                    txtFecNac.Text = "";
                    txtTfnoCasa.Text = "";
                    txtMovil.Text = "";
                    txtNumTarjeta.Text = "";
                    txtNivel.Text = "";

                }
            }
        }
    }
}