using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Resources;
using System.Data;

namespace AVE
{
    public partial class Solicitud :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Comprobamos que vienen todos los parámetros
                if (Request["IdArticulo"] == null ||
                        Request["Talla"] == null ||
                        Request["IdTienda"] == null ||
                        Request["Tienda"] == null ||
                        Request["Stock"] == null)
                {

                    string script = "alert('" + Resource.SolicitudFaltanParametros + "');" +
                                    "history.back();";
                    ClientScript.RegisterStartupScript(typeof(string), "Error", script, true);
                }
                else
                {
                    SDSDetalleArticulo.SelectParameters["IdArticulo"].DefaultValue = Request["IdArticulo"].ToString();
                    SDSDetalleArticulo.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                    DataView dv = (DataView)SDSDetalleArticulo.Select(new DataSourceSelectArguments());

                    if (dv.Count > 0)
                    {
                        lblTienda.Text = Server.UrlDecode(Request["Tienda"].ToString());
                        //lblProveedor.Text  = dv[0]["Proveedor"].ToString();
                        LblMarca.Text = dv[0]["Proveedor"].ToString(); //marca NineWest es el proveedor
                        //lblIdArticulo.Text = Request.QueryString["IdArticulo"].ToString();
                        lblReferencia.Text = dv[0]["Referencia"].ToString();
                        lblModelo.Text = dv[0]["Modelo"].ToString();
                        //lblDescripcion.Text = dv[0]["Descripcion"].ToString();
                        lblColor.Text = dv[0]["Color"].ToString();
                        lblTalla.Text = Request.QueryString["Talla"].ToString();
                        txtUnidades.Text = ConfigurationManager.AppSettings["Pedidos.UnidadesDefault"];
                        lblPrecio.Text = string.Format("{0:0.00}", dv[0]["Precio"]);
                        LblStock.Text=(Request["Stock"]==null ? "0":Request["Stock"].ToString());

                        //Foco a Unidades. Como en BB no funciona txtUnidades.Focus() insertaremos un script para hacerlo
                        txtUnidades.Focus();

                    }
                }
                //Registramos en javascript el control de la caja de texto para usarlo desde otras funciones jscript, como la del foco en onload
                string script1 = "var txtUnidades = document.getElementById('" + txtUnidades.ClientID + "');";
                script1 += "var txtStock = document.getElementById('" + LblStock.ClientID + "');";
                Page.ClientScript.RegisterStartupScript(typeof(string), "txtUnidades", script1, true);
               
            }
        }



        protected void btnPedir_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SDSPedido.InsertParameters["IdArticulo"].DefaultValue = Request["IdArticulo"].ToString();
                SDSPedido.InsertParameters["Talla"].DefaultValue = Request["Talla"].ToString();
                SDSPedido.InsertParameters["Unidades"].DefaultValue = this.txtUnidades.Text;
                SDSPedido.InsertParameters["Precio"].DefaultValue = lblPrecio.Text; 
                SDSPedido.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
                SDSPedido.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                SDSPedido.InsertParameters["IdTienda"].DefaultValue = Request["IdTienda"].ToString();
                SDSPedido.InsertParameters["Stock"].DefaultValue = Request["Stock"].ToString();

                string script;
                if (SDSPedido.Insert() > 0)
                {
                       script = "alert('" + (Request["IdTienda"] == Contexto.IdTienda ? Resource.SolicitudPedidoRegistrado : Resource.CargoSolicitudRegistrado ) + " ' + idPedido);" +
                             "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.Inicio) + "';";
                    
                    HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5); 
                }
                else

                    script = "alert('" + Resource.ErrorPeticion + "');";

                ClientScript.RegisterStartupScript(typeof(string), "", script, true);
            }
        }

        /// <summary>
        /// Para recoger el id insertado hay que acudir a este evento 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SDSPedido_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            int id = (int)((SqlParameter)(e.Command.Parameters["@IdPedido"])).Value;

            //Registramos el idPedido para usarlo desde el alert
            ClientScript.RegisterStartupScript(typeof(string), "id", "var idPedido = " + id + ";", true);
        }

        protected void btnPrecio_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.ConversionMoneda + "?" + Constantes.QueryString.ImporteConversion + "=" + lblPrecio.Text);
        }
    }
}
