using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLGestionCliente9;


using System.Drawing;


namespace AVE
{
    public partial class ActivacionTarjeta9 : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        DLLGestionCliente9.Models.Cliente9 client9 = new DLLGestionCliente9.Models.Cliente9();

        
        DLLGestionCliente9.ClsCliente9 objNine = new  DLLGestionCliente9.ClsCliente9() ;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "ValidaTipoCliente();", true);
            RadioButtonlTipoCli.Attributes.Add("onclick", "ValidaTipoCliente()");
           // RadioButtonlTipoCli.Attributes.Add("onChange", "LimpiaControles()");
            
            
        }
    
        protected void btnBuscaCliente_Click(object sender, EventArgs e) {
            try
            {
                LimpiaControles();
                GetClienteActual(nomcliente.Text);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
        }
        protected void EliminaNine_Click(object sender, EventArgs e)
        {
            int result = 0;
           
           
            try
            {
                hidMsjError.Value = "";
                result = objNine.EliminaClienteNine(hidIdCliente.Value.ToString(), txtNumTarjeta.Text);
                if (result > 0)
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Tarjeta añadida al cliente');", true);
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir la tarjeta al cliente');", true);
            }
            catch (Exception ex)
            {
                log.Error("Exception btnActivarTjt:" + ex.Message);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir la tarjeta al cliente');", true);
            }

        }
        protected void Error309_Click(object sender, EventArgs e) {
            int result = 0;
            string strWs = hidMsjError.Value.ToString();
            try
            {
                result = objNine.UpdateHistoricoErr309("ALTA SOCIO",strWs, Contexto.IdTienda);
                if (result > 0)
                {
                     ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Tarjeta añadida al cliente');", true);
                     hidMsjError.Value = "";
                }
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir la tarjeta al cliente');", true);
            }
            catch (Exception ex) {
                log.Error("Exception btnActivarTjt:" + ex.Message);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir la tarjeta al cliente');", true);
            }
        
        }
        private void LimpiaControles()
        {
            this.txtNombre.Text = "";
            this.txtApellidos.Text = "";
            this.txtEmail.Text="";
            this.txtFecNac.Text = "";
            this.txtMovil.Text = "";
            this.txtTfnoCasa.Text = "";
            txtNumTarjeta.Text = "22114005";
            ValidaControles();
        }
        protected void ValidacionesContexto()
        {

             if (Contexto.IdEmpleado != null)
                  Session["IdEmpleado"] = Contexto.IdEmpleado;

             if (Contexto.IdTienda != null)
                 Session["IdTienda"] = Contexto.IdTienda;
            if(Contexto.IdTerminal!= null)
                Session["IdTerminal"] = Contexto.IdTerminal;

        }
        protected void btnActivarTjt_Click(object sender, EventArgs e) {
            int result = 0;
            int error309 = 0;
            string strWS="";
           

            if (Page.IsValid) {
                String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();
                try
                {
                    if ( hidEsEmpleado.Value =="") hidEsEmpleado.Value="0";
                    if (hidEsEmpleado.Value == "0" )
                    {
                        ValidacionesContexto();
                        if (!Comun.CheckURLWs(url, 10000))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                            return;
                        }
                        AsignaDatosNine();
                        objNine.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();



                        if (hidIdCliente.Value == "")//NuevoCliente
                        {
                            result = objNine.AniadeNuevoCliente(ref client9, Session["IdEmpleado"].ToString(), Session["IdTienda"].ToString(), Session["IdTerminal"].ToString(), ref error309, ref strWS);
                            if (result > 0) hidIdCliente.Value = client9.Id_Cliente.ToString();
                            if (error309 == 1)
                            {
                                hidMsjError.Value = strWS;
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "Numero de Tarjeta No Valido", "ConfirmarTJT('¿Desea grabar tarjeta en TPV a pesar de mensaje devuelto por C9?');", true);
                            }
                        }
                        else //Cliente existente que se pasa a cliente 9
                        {
                            result = objNine.UpdateClientetoNine(ref client9, Session["IdEmpleado"].ToString(), Session["IdTienda"].ToString(), Session["IdTerminal"].ToString(), ref error309, ref strWS);
                            if (error309 == 1)
                            {
                                hidMsjError.Value = strWS;
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "Numero de Tarjeta No Valido", "ConfirmarTJT('¿Desea grabar tarjeta en TPV a pesar de mensaje devuelto por C9?');", true);
                            }
                        }

                        if (result > 0 && result != 309)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El cliente con id: " + hidIdCliente.Value + " ha sido añadido al sistema');", true);
                            LimpiaControles();
                            ValidaControles();
                            HabilitaValidaciones();
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir el cliente, al sistema:" + strWS + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Error, El Cliente es Empleado.');", true);
                    }

                }
                catch (Exception ex) {
                    log.Error("Exception btnActivarTjt:" + ex.Message);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('Se produjo un error al añadir el cliente, al sistema:" + strWS + "');", true);
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
             //   LstClientes.Visible = false;
                nomcliente.Visible = true;
               
            }
            catch (Exception ex)
            {
                log.Error(ex);
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
        private void ValidaControles() {
            Color colorCelDes = new Color();
            colorCelDes = ColorTranslator.FromHtml("#E5DCDC");
            txtNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            this.RFVNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            this.reqNombre.Enabled = txtNombre.Text.Length > 0 ? false : true;
            txtNombre.BackColor = txtNombre.Text.Length > 0 ? colorCelDes : Color.White;
            txtApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            txtApellidos.BackColor = txtApellidos.Text.Length > 0 ? colorCelDes : Color.White;
            this.RFVApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            this.reqApellidos.Enabled = txtApellidos.Text.Length > 0 ? false : true;
            txtEmail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtEmail.BackColor = txtEmail.Text.Length > 0 ? colorCelDes : Color.White;
            this.RFVMail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            this.reqEmail.Enabled = txtEmail.Text.Length > 0 ? false : true;
            txtFecNac.Enabled = txtFecNac.Text.Length > 0 ? false : true;
            txtFecNac.BackColor = txtFecNac.Text.Length > 0 ? colorCelDes : Color.White;
            this.RFVFecha.Enabled = txtFecNac.Text.Length > 0 ? false : true;
            txtTfnoCasa.Enabled = txtTfnoCasa.Text.Length > 0 ? false : true;
            txtTfnoCasa.BackColor = txtTfnoCasa.Text.Length > 0 ? colorCelDes : Color.White;
            this.RFVTelefono.Enabled = txtTfnoCasa.Text.Length > 0 ? false : true;
            this.reqTelefono.Enabled=txtTfnoCasa.Text.Length > 0 ? false : true;
            txtMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
            txtMovil.BackColor = txtMovil.Text.Length > 0 ? colorCelDes : Color.White;
            this.RFVMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
            this.reqMovil.Enabled = txtMovil.Text.Length > 0 ? false : true;
        }
        private void AsignaDatosNine(){
            
            
            client9.Id_Cliente = hidIdCliente.Value !="" ?Convert.ToInt32(hidIdCliente.Value):0;
            client9.Nombre = txtNombre.Text;
            client9.Apellidos = txtApellidos.Text;
            client9.FechaNacimiento = txtFecNac.Text;
            client9.Email = txtEmail.Text;
            client9.Telefono = txtTfnoCasa.Text;
            client9.Movil = txtMovil.Text;
            client9.NumTarjeta = txtNumTarjeta.Text;
            client9.Aniversario = DateTime.Now.ToShortDateString();
        }
        private void GetClienteActual(string sCliente)
        {
            try
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
                        txtNumTarjeta.Text = objclient[0].NumTarjeta.ToString();
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
                        this.txtNombre.Text = "";
                        this.txtApellidos.Text = "";
                        this.txtEmail.Text = "";
                        this.txtFecNac.Text = "";
                        this.txtTfnoCasa.Text = "";
                        this.txtMovil.Text = "";
                        this.txtNumTarjeta.Text = "";

                    }
                }
            }
            catch (Exception ex) {
                log.Error("Exception GetClienteActual:" + ex.Message.ToString());
            }
        }
        
    }
}