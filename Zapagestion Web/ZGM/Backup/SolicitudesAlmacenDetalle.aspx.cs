using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AVE
{
    public partial class SolicitudesAlmacenDetalle :  CLS.Cls_Session 
    {
        int IdCarro;
        protected void Page_Load(object sender, EventArgs e)
        {

            SDSSolicitud.SelectParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;

            if (!Page.IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            string Estado = "";
            //Comprobamos que tenemos el IdPedido
            if (Request.QueryString["IdPedido"] != null)
            {
                DataView dvSolicitud = (DataView)SDSSolicitud.Select(new DataSourceSelectArguments());

                //Comprobamos que hemos recuperado el Solicitud
                if (dvSolicitud.Count == 1)
                {
                    lblIdPedido.Text = dvSolicitud[0]["IdPedido"].ToString();
                    lblFechaPedido.Text = dvSolicitud[0]["FechaPedido"].ToString();
                    lblTienda.Text = dvSolicitud[0]["Tienda"].ToString();
                    lblIdProveedor.Text = dvSolicitud[0]["IdProveedor"].ToString();
                    lblProveedor.Text = dvSolicitud[0]["Proveedor"].ToString();
                    lblIdArticulo.Text = dvSolicitud[0]["IdArticulo"].ToString();
                    lblReferencia.Text = dvSolicitud[0]["Referencia"].ToString();
                    lblModelo.Text = dvSolicitud[0]["Modelo"].ToString();
                    lblDescripcion.Text = dvSolicitud[0]["Descripcion"].ToString();
                    lblColor.Text = dvSolicitud[0]["Color"].ToString();
                    lblUnidades.Text = dvSolicitud[0]["Unidades"].ToString();
                    lblTalla.Text = dvSolicitud[0]["Talla"].ToString();
                    lblVendedor.Text = dvSolicitud[0]["Vendedor"].ToString();
                    lblFechaCambio.Text = dvSolicitud[0]["FechaCambio"].ToString();

                    //Estado traducido
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
                    lblEstadoActual.Text = rm.GetString(dvSolicitud[0]["EstadoSolicitudResource"].ToString());
                    Estado = dvSolicitud[0]["IdEstado"].ToString();
                    ddlEstados.SelectedValue = Estado;
                    //ACL.07-07-2014. Si el estado es vendido, se inhabilita, para que no se pueda cambiar.
                    ddlEstados.Enabled = (Estado != "6");

                }
            }
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            String IdPedido=lblIdPedido.Text;
            String IdArticulo = lblIdArticulo.Text;
            String Talla = lblTalla.Text;
            DLLGestionVenta.ProcesarVenta v;
          
            Int64 idCarrito = 0;
            //Cambiamos el estado en la base de datos
            SDSSolicitud.Update();
            //ACL.07-07-2014.Añadimos el item al carrito.
            if (ddlEstados.SelectedItem.Text == "Vendido")
            {

                v = new DLLGestionVenta.ProcesarVenta();
                v.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                if (Session["idCarrito"] != null) { idCarrito = Int64.Parse(Session["IdCarrito"].ToString()); }

                Session["IdPedido"] = IdPedido;
                ViewState["talla"] =  lblTalla.Text;
                ViewState["IdArticulo"] = IdArticulo;
                añadirDetallealcarrito(IdPedido);
                añadirLineaCarrito();

                if (!v.ComprobarStock(Int64.Parse(IdPedido), idCarrito))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "StockSolicitud", "alert(' Se dispone a vender una Referencia sin Stock Registrado.');", true);
                    return;

                }


            }
            CargarDatos();
        }

        protected void añadirDetallealcarrito(string IdPedido)
        {
            //Si no hay carrito anterior, generamos uno nuevo
            if (Session["IdCarrito"] == null)
            {
                AnadirCarrito.InsertParameters["Usuario"].DefaultValue = Contexto.IdEmpleado;
                AnadirCarrito.InsertParameters["IdCliente"].DefaultValue = "0";
                AnadirCarrito.InsertParameters["Maquina"].DefaultValue = System.Web.HttpContext.Current.Request.UserHostAddress;
                AnadirCarrito.InsertParameters["EstadoCarrito"].DefaultValue = "0";

                AnadirCarrito.Insert();



            }
        }
        protected void AnadirCarrito_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            IdCarro = (int)((SqlParameter)(e.Command.Parameters["@IdCarrit"])).Value;
            Session["IdCarrito"] = IdCarro;



        }
        protected void añadirLineaCarrito()
        {
            if (Session["IdCarrito"] != null && ViewState["talla"] != null)
            {
                AnadirDetalleCarrito.InsertParameters["IdArticulo"].DefaultValue = ViewState["IdArticulo"].ToString();
                AnadirDetalleCarrito.InsertParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                AnadirDetalleCarrito.InsertParameters["IdPedido"].DefaultValue = Session["IdPedido"].ToString();
                AnadirDetalleCarrito.InsertParameters["Talla"].DefaultValue = ViewState["talla"].ToString();
                AnadirDetalleCarrito.InsertParameters["Cantidad"].DefaultValue = "1";
            }

            AnadirDetalleCarrito.Insert();

            var miMaster = (MasterPage)this.Master;
            miMaster.CambiarEstadoImagenCarrito(true);

            CalculoPromocion_Carrito();


        }
        private void CalculoPromocion_Carrito()
        {

            DLLGestionVenta.ProcesarVenta objVenta;
            String rMensaje = String.Empty;

            if (Session["IdCarrito"] != null)
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();

                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                rMensaje = objVenta.GetObjCarritoPromocion(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion);

                if (rMensaje.Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMensaje + "');", true);
                    return;
                }
            }
        }  

        protected void ddlEstados_DataBound(object sender, EventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (ListItem item in ddlEstados.Items)
            {
                item.Text = rm.GetString(item.Text);
            }
        }

        protected void btnFoto_Click(object sender, EventArgs e)
        {
            // Para ver la foto del producto
            Response.Redirect("~/Foto.aspx?IdArticulo=" + lblIdArticulo.Text);
        }
    }
}
