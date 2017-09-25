using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLGestionCliente9;
using DLLGestionCliente9.Models;

using System.Drawing;
namespace AVE
{
    public partial class ActualizarCliente9 : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //

        DLLGestionCliente9.Models.Cliente9 client9 = new DLLGestionCliente9.Models.Cliente9();
        Cliente9Extent client9Extend = new Cliente9Extent();

        List<FacturacionC9> ListDatosFact;
        FacturacionC9 DatosFact = new FacturacionC9();
        DLLGestionCliente9.ClsCliente9 objNine = new DLLGestionCliente9.ClsCliente9();

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void Error309_Click(object sender, EventArgs e)
        {
            int result = 0;
            string strWs = hidMsjError.Value.ToString();
            try
            {
                result = objNine.UpdateHistoricoErr309("ACTUALIZASOCIO", strWs, Contexto.IdTienda);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Cliente Actualizado');", true);
                    hidMsjError.Value = "";
                }
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al actualizar al cliente');", true);
            }
            catch (Exception ex)
            {
                log.Error("Exception btnActivarTjt:" + ex.Message);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al actualizar al cliente');", true);
            }

        }

        protected void btnBuscaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();
                this.ListDatosFact = null;
                this.DatosFact = null;
                if (nomcliente.Text != "") { 
                    GetClienteActual(nomcliente.Text);
                    if (hidIdCliente.Value != "")
                    {
                        ListDatosFact = new List<FacturacionC9>();
                        getDatosFacturacion(hidIdCliente.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
        }
        protected void ValidacionesContexto()
        {

            if (Contexto.IdEmpleado != null)
                Session["IdEmpleado"] = Contexto.IdEmpleado;

            if (Contexto.IdTienda != null)
                Session["IdTienda"] = Contexto.IdTienda;
            if (Contexto.IdTerminal != null)
                Session["IdTerminal"] = Contexto.IdTerminal;

        }
        private void LimpiaControles()
        {
            hidIdCliente.Value = "";
            this.txtNombre.Text = "";
           
            this.txtApellidos.Text = "";
            this.txtEmail.Text = "";
            this.txtFecNac.Text = "";
            this.txtMovil.Text = "";
            this.txtTfnoCasa.Text = "";
            this.txtCargo.Text = "";
            this.txtColonia.Text = "";
            this.txtComentarios.Text = "";
            this.txtContacto.Text = "";
            this.txtCP.Text = "";
            this.txtDireccion.Text = "";
            this.txtEmail.Text = "";
            this.cboEstado.Items.FindByText(" ").Selected = true;
            this.txtFax.Text = "";
            this.txtFecNac.Text = "";
            this.txtMovil.Text = "";
            this.txtPais.Text = "";
            this.txtPoblacion.Text = "";
            this.txtTalla.Text = "";
            this.txtTfnoCasa.Text = "";
            this.txtTfnoTjo.Text = "";
            


           
        }
       
        protected void btnActivarFactura_Click(object sender, EventArgs e) {
            if (Panel1.Visible == true)
                this.Panel1.Visible = false;
            else
                this.Panel1.Visible = true;

        
        }
        protected void btnGuardarFactc_Click(object sender, EventArgs e) {

            string RFCOld = "";
            int result = 0;
            GuardaDatosFacturacion();

            if (ListaRFC.SelectedIndex == 0) DatosFact.Accion = "INS";
            else
            {
                DatosFact.Accion = "UPD";
                RFCOld = ListaRFC.SelectedItem.Text;
            }
            objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            result = objNine.GuardaDatosFacturacion(DatosFact, RFCOld);
            if (result > 0)
            {
                if (ListaRFC.SelectedIndex == 0)
                {
                    ListItem item = new ListItem();
                    item.Text = DatosFact.RFC;
                    item.Value = Convert.ToString(ListaRFC.Items.Count);
                    ListaRFC.Items.Add(item);
                    LimpiaControles();
                    ListaRFC.SelectedIndex = 0;
                    LimpiaControlesFacturacion();
                }
                else
                { 
                    ListaRFC.SelectedItem.Text = DatosFact.RFC;
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Datos Facturación", "alert('El RFC'" + DatosFact.RFC + "' ha sido guardado');", true);
            }
        
        }
        protected void btnActualizar_Click(object sender, EventArgs e) {
        String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();
        string strWS="";
        string IdSocio9 = "0";
        int error309 = 0;
        int result = 0;
        try
        {
            if (hidEsEmpleado.Value == "0")
            {

                ValidacionesContexto();
                if (!Comun.CheckURLWs(url, 10000))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                    return;
                }
                AsignaDatosNine();
                objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                result = objNine.ActualizaDatosCliente(client9, client9Extend, Session["IdEmpleado"].ToString(), Session["IdTienda"].ToString(), Session["IdTerminal"].ToString(), ref strWS, ref IdSocio9, ref error309);
                if (error309 == 1)
                {
                    hidMsjError.Value = strWS;
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "Numero de Tarjeta No Valido", "ConfirmarTJT('¿Desea actualizar tarjeta en TPV a pesar de mensaje devuelto por C9?');", true);
                }
                if (result > 0 && result != 309)
                {
                    LimpiaControles();
                    reiniciaLstCIF();
                    LimpiaControlesFacturacion();
                    this.nomcliente.Text = "";
                    ListDatosFact = null;
                    DatosFact = null;
                    ValidaControles();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El cliente con IdSocio:" + IdSocio9 + " ha sido actualizado en el sistema');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al actualizar el cliente, en el sistema:" + strWS + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Error, El Cliente es Empleado.');", true);
            }
        }
        catch (Exception ex) {
            log.Error("Exception Actualizar_click:" + ex.Message.ToString());
        }
        
        }
        protected void reiniciaLstCIF()
        {
            int contador = ListaRFC.Items.Count;
            for (int i = 1; contador > i; i++)
            {
                ListaRFC.Items.RemoveAt(1);
            }


        }
        protected void ListaRFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int indice = ListaRFC.SelectedIndex;
                if (this.ListDatosFact == null)
                    if (hidIdCliente.Value != "")
                    {
                        reiniciaLstCIF();
                        getDatosFacturacion(hidIdCliente.Value);
                    }
                if (indice > 0)
                    AsignaDatosFacturacion(indice);

                else
                {
                    LimpiaControlesFacturacion();
                }
                ListaRFC.SelectedIndex = indice;
            }
            catch (Exception ex) {
                log.Error("exception listaRFC:" + ex.Message.ToString());
            }
        }
        protected void LstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();
                reiniciaLstCIF();
                nomcliente.Text = LstClientes.SelectedItem.Text;
                GetClienteActual(LstClientes.SelectedValue.ToString());
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
            colorCelDes = ColorTranslator.FromHtml("#E5DCDC");
            txtNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            this.reqNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            txtNombre.BackColor = txtNombre.Text.Length > 0 ? colorCelDes : Color.White;
            txtApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            txtApellidos.BackColor = txtApellidos.Text.Length > 0 ? colorCelDes : Color.White;
            this.reqApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            txtEmail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtEmail.BackColor = txtEmail.Text.Length > 0 ? colorCelDes : Color.White;
            this.reqEmail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtFecNac.Enabled = txtFecNac.Text.Length > 0 ? false : true;
            txtFecNac.BackColor = txtFecNac.Text.Length > 0 ? colorCelDes : Color.White;
            this.regtxtFnac.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtTfnoCasa.Enabled = txtTfnoCasa.Text.Length > 0 ? false : true;
            txtTfnoCasa.BackColor = txtTfnoCasa.Text.Length > 0 ? colorCelDes : Color.White;
            this.reqTelefono.Enabled = txtTfnoCasa.Text.Length > 0 ? false : true;
            txtMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
            txtMovil.BackColor = txtMovil.Text.Length > 0 ? colorCelDes : Color.White;
            this.reqMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
        }
        private void LimpiaControlesFacturacion() {
            txtColonia.Text = "";
            txtCPFac.Text ="";
            txtDomFac.Text ="";
            cbEstadoFac.Text = "";
            //txtEstadoFac.Text ="";
            txtFaxFac.Text = "";
            txtMailFac.Text ="";
            txtMovilFac.Text = "";
            txtNomFac.Text ="";
            txtNumExt.Text = "";
            txtNumInt.Text = "";
            txtPaisFac.Text ="";
            txtPobFac.Text = "";
            txtRFC.Text = "";
            txtTfnoFac.Text ="";
            txtCPFac.Text = "";
            chkEnvMail.Checked = false;
            
        
        }
        private void GuardaDatosFacturacion()
        {
            DatosFact.IdCliente = Convert.ToInt32(hidIdCliente.Value);
            DatosFact.RFC = txtRFC.Text ;
            DatosFact.Nombre = txtNomFac.Text;
            DatosFact.Direccion = txtDomFac.Text;
            DatosFact.NExterior = txtNumExt.Text;
            DatosFact.NInterior = txtNumInt.Text;
            DatosFact.CP= txtCPFac.Text;
            DatosFact.Colonia= txtColonia.Text;
            DatosFact.Estado = cbEstadoFac.SelectedItem.Text;
            DatosFact.Poblacion = txtPobFac.Text;
            DatosFact.Pais = txtPaisFac.Text;
            DatosFact.Telefono = txtTfnoFac.Text;
            DatosFact.Fax = txtFaxFac.Text;
            DatosFact.Movil = txtMovilFac.Text;
            DatosFact.EMail = txtMailFac.Text;
            DatosFact.EnvFactMail = chkEnvMail.Checked == true ? 1 : 0;
        }

        private void AsignaDatosFacturacion(int indice){
            //Se disminuye en 1 ya que en el combo, hay un primer item que es el item "NUEVO", para dar de alta un registro;
            if (indice > 0) indice--;
            
            txtColonia.Text = ListDatosFact[indice].Colonia.ToString();
            txtCPFac.Text = ListDatosFact[indice].CP.ToString();
            txtDomFac.Text = ListDatosFact[indice].Direccion.ToString();

            ListItem itemToSelect =   this.cbEstadoFac.Items.FindByText(ListDatosFact[indice].Estado.ToString());

            if (itemToSelect != null)
            {
                itemToSelect.Selected = true;
            }

            txtFaxFac.Text = ListDatosFact[indice].Fax.ToString();
            txtMailFac.Text = ListDatosFact[indice].EMail.ToString();
            txtMovilFac.Text = ListDatosFact[indice].Movil.ToString();
            txtNomFac.Text = ListDatosFact[indice].Nombre.ToString();
            txtNumExt.Text = ListDatosFact[indice].NExterior.ToString();
            txtNumInt.Text = ListDatosFact[indice].NInterior.ToString();
            txtPaisFac.Text = ListDatosFact[indice].Pais.ToString();
            txtPobFac.Text = ListDatosFact[indice].Poblacion.ToString();
            txtRFC.Text = ListDatosFact[indice].RFC.ToString();
            txtTfnoFac.Text = ListDatosFact[indice].Telefono.ToString();
            txtCPFac.Text = ListDatosFact[indice].CP.ToString();
            chkEnvMail.Checked = ListDatosFact[indice].EnvFactMail.ToString() == "1" ? true : false;
            
        }
        private void AsignaDatosNine()
        {
            client9.Id_Cliente = hidIdCliente.Value != "" ? Convert.ToInt32(hidIdCliente.Value) : 0;
            client9.Nombre = txtNombre.Text;
            client9.Apellidos = txtApellidos.Text;
            client9.FechaNacimiento = txtFecNac.Text;
            client9.Email = txtEmail.Text;
            client9.Telefono = txtTfnoCasa.Text;
            client9.Movil = txtMovil.Text;
            client9.NumTarjeta = hidNumeroTarjetaCliente9.Value;
            client9.Aniversario = DateTime.Now.ToShortDateString();
            client9Extend.Cargo = txtCargo.Text;
            client9Extend.Comentarios = txtComentarios.Text;
                       
            client9Extend.Contacto = txtContacto.Text;
            client9Extend.Direccion = txtDireccion.Text;
            client9Extend.Estado = this.cboEstado.SelectedItem.Text;
            client9Extend.Fax = txtFax.Text;
            client9Extend.Pais = txtPais.Text;
            client9Extend.Poblacion = txtPoblacion.Text;
            client9Extend.Talla = txtTalla.Text;
            client9Extend.TfnoTrabajo = txtTfnoTjo.Text;
            client9Extend.CP = txtCP.Text;
            
        }
        private int CargaDatosCombo(List<FacturacionC9> itemFact)
        {
            int contador = 1;
            int result = 0;
            try
            {
                foreach (FacturacionC9 fac in itemFact)
                {
                    ListItem item = new ListItem();
                    item.Text = fac.RFC.ToString();
                    item.Value = contador.ToString();
                    this.ListaRFC.Items.Add(item);
                    contador++;
                }
                result = contador;
            }
            catch (Exception ex) {
                log.Error("Exception Cargando RFCS in dropdown." + ex.Message.ToString());
            }
                return  result;
        }
        private void getDatosFacturacion(string sCliente)
        {
            objNine = new DLLGestionCliente9.ClsCliente9();
            int indice = 0;
            int numItems = 0;
            objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            List<DLLGestionCliente9.Models.FacturacionC9> Factclient = objNine.GetDatosFacturacion(sCliente);
            if (Factclient.Count > 0)
            {
                ListDatosFact = Factclient;
                numItems = CargaDatosCombo(ListDatosFact);
                if (numItems >= 1) indice = 1;
                AsignaDatosFacturacion(indice);
                ListaRFC.SelectedIndex = indice;
            }

        }
        private void GetClienteActual(string sCliente)
        {
            try
            {
                objNine = new DLLGestionCliente9.ClsCliente9();
                objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                List<DLLGestionCliente9.Models.Cliente9Gral> objclient = objNine.GetClienteActualizacion(sCliente, Contexto.FechaSesion);


                if (objclient.Count > 0)
                {
                    if (objclient.Count == 1)
                    {
                        
                        txtNombre.Text = objclient[0].c9.Nombre;
                        txtApellidos.Text = objclient[0].c9.Apellidos;
                        txtEmail.Text = objclient[0].c9.Email;
                        txtFecNac.Text = Convert.ToDateTime(objclient[0].c9.FechaNacimiento.ToString()).ToShortDateString();
                        txtTfnoCasa.Text = objclient[0].c9.Telefono.ToString();
                        txtMovil.Text = objclient[0].c9.Movil.ToString();
                        txtCargo.Text = objclient[0].c9Extent.Cargo.ToString();
                        txtComentarios.Text = objclient[0].c9Extent.Comentarios.ToString();
                        txtContacto.Text = objclient[0].c9Extent.Contacto.ToString();
                        txtCP.Text = objclient[0].c9Extent.CP.ToString();
                        txtDireccion.Text = objclient[0].c9Extent.Direccion.ToString();
                        ListItem itemToSelect = cboEstado.Items.FindByText(objclient[0].c9Extent.Estado.ToString());

                        if (itemToSelect != null)
                        {
                            itemToSelect.Selected = true;
                        }
                        
                       // txtEstado.Text = objclient[0].c9Extent.Estado.ToString();
                        txtFax.Text = objclient[0].c9Extent.Fax.ToString();
                        txtPais.Text = objclient[0].c9Extent.Pais.ToString();
                        txtPoblacion.Text = objclient[0].c9Extent.Poblacion.ToString();
                        txtTalla.Text = objclient[0].c9Extent.Talla.ToString();
                        txtTfnoTjo.Text = objclient[0].c9Extent.TfnoTrabajo.ToString();
                        hidEsEmpleado.Value = objclient[0].c9.esEmpleado.ToString();
                        hidNumeroTarjetaCliente9.Value = objclient[0].c9.NumTarjeta.ToString();
                        hidIdCliente.Value = objclient[0].c9.Id_Cliente.ToString();
                        
                        ValidaControles();
                    }
                    else
                    {
                        DLLGestionCliente9.Models.Cliente9Gral objclientAux;
                        objclientAux = new DLLGestionCliente9.Models.Cliente9Gral();
                        objclientAux.c9.Id_Cliente = -1;
                        objclientAux.c9.Cliente = "";

                        objclient.Insert(0, objclientAux);
                        LstClientes.DataTextField = "Cliente";
                        LstClientes.DataValueField = "ID_Cliente";
                        LstClientes.DataSource = objclient;
                        LstClientes.DataBind();
                        hidIdCliente.Value = "";
                        hidEsEmpleado.Value = "";
                        nomcliente.CssClass = "ocul1";
                        LstClientes.CssClass = "visi1";
                        this.txtNombre.Text = "";
                        this.txtApellidos.Text = "";
                        this.txtEmail.Text = "";
                        this.txtFecNac.Text = "";
                        this.txtTfnoCasa.Text = "";
                        this.txtMovil.Text = "";
                        

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception GetClienteActual:" + ex.Message.ToString());
            }
        }

    }
}