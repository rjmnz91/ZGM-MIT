using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AVE
{
    public partial class PedidosDetalle : CLS.Cls_Session 
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Datos del pedido
                DataView dvPedido = (DataView)SDSPedido.Select(new DataSourceSelectArguments());
                if (dvPedido.Count > 0)
                {
                    lblIdPedido.Text = dvPedido[0]["IdPedido"].ToString();
                    lblDescripcion.Text = dvPedido[0]["Descripcion"].ToString();
                    lblTalla.Text = dvPedido[0]["Talla"].ToString();
                    lblUnidades.Text = dvPedido[0]["Unidades"].ToString();
                    lblPrecio.Text = string.Format("{0:0.00}", dvPedido[0]["Precio"]);
                    hddIdCliente.Value = dvPedido[0]["id_Cliente"].ToString();
                    txtCif.Text = dvPedido[0]["Cif_Cliente"].ToString();
                    txtNombreC.Text = dvPedido[0]["Nombre_Cliente"].ToString();
                    txtApellidosC.Text = dvPedido[0]["Apellidos_Cliente"].ToString();
                    txtDireccionC.Text = dvPedido[0]["Direccion_Cliente"].ToString();
                    txtCodPostalC.Text = dvPedido[0]["CodPostal_Cliente"].ToString();
                    txtPoblacionC.Text = dvPedido[0]["Poblacion_Cliente"].ToString();
                    txtProvinciaC.Text = dvPedido[0]["Provincia_Cliente"].ToString();
                    txtPaisC.Text = dvPedido[0]["Pais_Cliente"].ToString();
                    txtTelefonoC.Text = dvPedido[0]["Telefono_Cliente"].ToString();
                    txtMovilC.Text = dvPedido[0]["Movil_Cliente"].ToString();
                    txtEmailC.Text = dvPedido[0]["email_Cliente"].ToString();
                    txtObservacionesC.Text = dvPedido[0]["Observaciones_Cliente"].ToString();
                    txtNombreD.Text = dvPedido[0]["Nombre_Destinatario"].ToString();
                    txtApellidosD.Text = dvPedido[0]["Apellidos_Destinatario"].ToString();
                    txtDireccionD.Text = dvPedido[0]["Direccion_Destinatario"].ToString();
                    txtCodPostalD.Text = dvPedido[0]["CodPostal_Destinatario"].ToString();
                    txtPoblacionD.Text = dvPedido[0]["Poblacion_Destinatario"].ToString();
                    txtProvinciaD.Text = dvPedido[0]["Provincia_Destinatario"].ToString();
                    txtPaisD.Text = dvPedido[0]["Pais_Destinatario"].ToString();
                    txtTelefonoD.Text = dvPedido[0]["Telefono_Destinatario"].ToString();
                    txtMovilD.Text = dvPedido[0]["Movil_Destinatario"].ToString();
                    txtEmailD.Text = dvPedido[0]["email_Destinatario"].ToString();
                    txtObservacionesD.Text = dvPedido[0]["Observaciones_Destinatario"].ToString();

                    //Si tenemos datos de destinatario, habilitamos el panel
                    if (txtNombreD.Text != string.Empty)
                    {
                        chkDireccionEntrega.Checked = false;
                        string script = "document.getElementById('divDatosEntrega').style.display = 'block';";
                        Page.ClientScript.RegisterStartupScript(typeof(string), "HabilitardivDatosEntrega", script, true);
                    }
                }

                //Datos del cliente. Si llega por Query es que se ha seleccionado en BuscarClientes.aspx
                if (Request.QueryString["IdCliente"] != null)
                {
                    //Lo primero será actualizar el valor de id_Cliente con el que trabajar
                    hddIdCliente.Value = Request.QueryString["IdCliente"].ToString();

                    SDSClientes.SelectParameters[0].DefaultValue = hddIdCliente.Value;
                    DataView dvCliente = (DataView)SDSClientes.Select(new DataSourceSelectArguments());

                    if (dvCliente.Count > 0)
                    {
                        txtCif.Text = dvCliente[0]["Cif"].ToString();
                        txtNombreC.Text = dvCliente[0]["Nombre"].ToString();
                        txtApellidosC.Text = dvCliente[0]["Apellidos"].ToString();
                        txtDireccionC.Text = dvCliente[0]["Direccion"].ToString();
                        txtCodPostalC.Text = dvCliente[0]["CodPostal"].ToString();
                        txtPoblacionC.Text = dvCliente[0]["Poblacion"].ToString();
                        txtProvinciaC.Text = dvCliente[0]["Provincia"].ToString();
                        txtPaisC.Text = dvCliente[0]["Pais"].ToString();
                        txtEmailC.Text = dvCliente[0]["email"].ToString();
                        txtTelefonoC.Text = dvCliente[0]["Telefono"].ToString();
                        txtMovilC.Text = dvCliente[0]["Movil"].ToString();
                    }
                }

                //Solo se muestra si estamos con un cliente existente
                if (hddIdCliente.Value!= string.Empty)
                    chkActualizarCliente.Visible = true;    
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.BuscarCliente + "?ReturnURL=" + Server.UrlEncode(Request.Url.PathAndQuery));
        }

        protected void btnGuardarPedido_Click(object sender, EventArgs e)
        {
            //Gestión del cliente
            if (hddIdCliente.Value == string.Empty)
            {
                //Como no existe lo creamos
                SDSClientes.InsertParameters["id_Cliente"].DefaultValue = "";
                SDSClientes.InsertParameters["Cif"].DefaultValue = txtCif.Text;
                SDSClientes.InsertParameters["Nombre"].DefaultValue = txtNombreC.Text;
                SDSClientes.InsertParameters["Apellidos"].DefaultValue = txtApellidosC.Text;
                SDSClientes.InsertParameters["Direccion"].DefaultValue = txtDireccionC.Text;
                SDSClientes.InsertParameters["CodPostal"].DefaultValue = txtCodPostalC.Text;
                SDSClientes.InsertParameters["Poblacion"].DefaultValue = txtPoblacionC.Text;
                SDSClientes.InsertParameters["Provincia"].DefaultValue = txtProvinciaC.Text;
                SDSClientes.InsertParameters["Telefono"].DefaultValue = txtTelefonoC.Text;
                SDSClientes.InsertParameters["Movil"].DefaultValue = txtMovilC.Text;
                SDSClientes.InsertParameters["email"].DefaultValue = txtEmailC.Text;
                SDSClientes.InsertParameters["Pais"].DefaultValue = txtPaisC.Text;

                SDSClientes.Insert();
            }
            else if (chkActualizarCliente.Checked)
            {
                //Si existe y se quiere actualizar, se hace
                SDSClientes.UpdateParameters["id_Cliente"].DefaultValue = hddIdCliente.Value;
                SDSClientes.UpdateParameters["Cif"].DefaultValue = txtCif.Text;
                SDSClientes.UpdateParameters["Nombre"].DefaultValue = txtNombreC.Text;
                SDSClientes.UpdateParameters["Apellidos"].DefaultValue = txtApellidosC.Text;
                SDSClientes.UpdateParameters["Direccion"].DefaultValue = txtDireccionC.Text;
                SDSClientes.UpdateParameters["CodPostal"].DefaultValue = txtCodPostalC.Text;
                SDSClientes.UpdateParameters["Poblacion"].DefaultValue = txtPoblacionC.Text;
                SDSClientes.UpdateParameters["Provincia"].DefaultValue = txtProvinciaC.Text;
                SDSClientes.UpdateParameters["Telefono"].DefaultValue = txtTelefonoC.Text;
                SDSClientes.UpdateParameters["Movil"].DefaultValue = txtMovilC.Text;
                SDSClientes.UpdateParameters["email"].DefaultValue = txtEmailC.Text;
                SDSClientes.UpdateParameters["Pais"].DefaultValue = txtPaisC.Text;

                SDSClientes.Update();
            }

            //Guardar los datos del pedido
            SDSPedido.UpdateParameters["IdPedido"].DefaultValue = Request.QueryString["IdPedido"].ToString();
            SDSPedido.UpdateParameters["id_Cliente"].DefaultValue = hddIdCliente.Value;
            SDSPedido.UpdateParameters["Cif_Cliente"].DefaultValue = txtCif.Text;
            SDSPedido.UpdateParameters["Nombre_Cliente"].DefaultValue = txtNombreC.Text;
            SDSPedido.UpdateParameters["Apellidos_Cliente"].DefaultValue = txtApellidosC.Text;
            SDSPedido.UpdateParameters["Direccion_Cliente"].DefaultValue = txtDireccionC.Text;
            SDSPedido.UpdateParameters["CodPostal_Cliente"].DefaultValue = txtCodPostalC.Text;
            SDSPedido.UpdateParameters["Poblacion_Cliente"].DefaultValue = txtPoblacionC.Text;
            SDSPedido.UpdateParameters["Provincia_Cliente"].DefaultValue = txtProvinciaC.Text;
            SDSPedido.UpdateParameters["Pais_Cliente"].DefaultValue = txtPaisC.Text;
            SDSPedido.UpdateParameters["Telefono_Cliente"].DefaultValue = txtTelefonoC.Text;
            SDSPedido.UpdateParameters["Movil_Cliente"].DefaultValue = txtMovilC.Text;
            SDSPedido.UpdateParameters["email_Cliente"].DefaultValue = txtEmailC.Text;
            SDSPedido.UpdateParameters["Observaciones_Cliente"].DefaultValue = txtObservacionesC.Text;
            SDSPedido.UpdateParameters["Usuario"].DefaultValue = Contexto.Usuario;

            if (chkDireccionEntrega.Checked)
            {
                //Usamos los mismos valores del cliente
                SDSPedido.UpdateParameters["Nombre_Destinatario"].DefaultValue = txtNombreC.Text;
                SDSPedido.UpdateParameters["Apellidos_Destinatario"].DefaultValue = txtApellidosC.Text;
                SDSPedido.UpdateParameters["Direccion_Destinatario"].DefaultValue = txtDireccionC.Text;
                SDSPedido.UpdateParameters["CodPostal_Destinatario"].DefaultValue = txtCodPostalC.Text;
                SDSPedido.UpdateParameters["Poblacion_Destinatario"].DefaultValue = txtPoblacionC.Text;
                SDSPedido.UpdateParameters["Provincia_Destinatario"].DefaultValue = txtProvinciaC.Text;
                SDSPedido.UpdateParameters["Pais_Destinatario"].DefaultValue = txtPaisC.Text;
                SDSPedido.UpdateParameters["Telefono_Destinatario"].DefaultValue = txtTelefonoC.Text;
                SDSPedido.UpdateParameters["Movil_Destinatario"].DefaultValue = txtMovilC.Text;
                SDSPedido.UpdateParameters["email_Destinatario"].DefaultValue = txtEmailC.Text;
                SDSPedido.UpdateParameters["Observaciones_Destinatario"].DefaultValue = txtObservacionesC.Text;
            }
            else
            {
                //Usamos los campos del destinatario
                SDSPedido.UpdateParameters["Nombre_Destinatario"].DefaultValue = txtNombreD.Text;
                SDSPedido.UpdateParameters["Apellidos_Destinatario"].DefaultValue = txtApellidosD.Text;
                SDSPedido.UpdateParameters["Direccion_Destinatario"].DefaultValue = txtDireccionD.Text;
                SDSPedido.UpdateParameters["CodPostal_Destinatario"].DefaultValue = txtCodPostalD.Text;
                SDSPedido.UpdateParameters["Poblacion_Destinatario"].DefaultValue = txtPoblacionD.Text;
                SDSPedido.UpdateParameters["Provincia_Destinatario"].DefaultValue = txtProvinciaD.Text;
                SDSPedido.UpdateParameters["Pais_Destinatario"].DefaultValue = txtPaisD.Text;
                SDSPedido.UpdateParameters["Telefono_Destinatario"].DefaultValue = txtTelefonoD.Text;
                SDSPedido.UpdateParameters["Movil_Destinatario"].DefaultValue = txtMovilD.Text;
                SDSPedido.UpdateParameters["email_Destinatario"].DefaultValue = txtEmailD.Text;
                SDSPedido.UpdateParameters["Observaciones_Destinatario"].DefaultValue = txtObservacionesD.Text;
            }

            SDSPedido.Update();

            Response.Redirect(Constantes.Paginas.Pedidos);
        }

        protected void SDSClientes_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            //Capturamos el id_Cliente creado
            hddIdCliente.Value = e.Command.Parameters["@id_Cliente"].Value.ToString();
        }

        protected void btnPrecio_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.ConversionMoneda + "?" + Constantes.QueryString.ImporteConversion + "=" + lblPrecio.Text);
        }
    }
}
