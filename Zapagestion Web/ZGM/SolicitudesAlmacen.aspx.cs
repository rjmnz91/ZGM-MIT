using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using DLLGestionVenta;
using System.Web.Services;
using System.Text;
using DLLGestionVenta.CapaDatos;
using DLLGestionVenta.Models;
namespace AVE
{
    public partial class SolicitudesAlmacen : CLS.Cls_Session
    {
        int IdCarro;
        List<String> lstSolicitudes;
        DLLGestionVenta.ProcesarVenta v;

        // MJM 28/03/2014 INICIO
        // http://www.codeproject.com/Tips/616164/jQuery-Active-Tab-After-Page-Postback-jQuery
        private void setTabs()
        {
            String hiddenFieldValue = hidSelectedTab.Value;
            StringBuilder js = new StringBuilder();
            js.Append("<script type='text/javascript'>");
            js.Append("var previouslySelectedTab = ");
            js.Append(hiddenFieldValue);
            js.Append(";</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "acttab", js.ToString());
        }
        // MJM 28/03/2014 FIN

        private string UserACPiaguiAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserACPiagui"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["UserACPiagui"].ToString();
                else
                    return "ERROR";
            }
        }

        private string PassACPiaguiAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["PassACPiagui"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["PassACPiagui"].ToString();
                else
                    return "ERROR";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String[] Cadena;
            int resp = 0;

            this.setTabs();

            SDSSolicitudes.SelectParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var miMaster = (MasterPage)this.Master;
                //Comprobamos la visualizacion del Mensaje
                if (AVE.Configuracion.ComprobarSolicitudesPendientes())
                {

                    ((Panel)miMaster.FindControl("PanelAviso")).Visible = true;

                }
                else
                {
                    ((Panel)miMaster.FindControl("PanelAviso")).Visible = false;
                }

            }
            else
            {

                lstSolicitudes = new List<string>();

                if (Session["SOL_TIENDA"] != null)
                {
                    Cadena = ((String)Session["SOL_TIENDA"].ToString()).Split('|');

                    for (int i = 0; i < Cadena.Length; i++)
                    {

                        lstSolicitudes.Add(Cadena[i]);

                    }

                }

                if (Session["IdCarrito"] != null)
                {

                    v = new ProcesarVenta();
                    v.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                    v.Terminal = Contexto.IdTerminal;
                    resp = v.GetPedidoCarrito(Int64.Parse(Session["IdCarrito"].ToString()), ref lstSolicitudes);
                    var miMaster = (MasterPage)this.Master;
                    miMaster.CambiarEstadoImagenCarrito(true);
                    int ArtiCarrito = ValidaArticulosCarrito(Session["IdCarrito"].ToString());
                    miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));

                }
                else {
                    var miMaster = (MasterPage)this.Master;
                    miMaster.MuestraArticulosCarrito("0");
                }

            }



        }
        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdSolicitudes.DataSourceID = "SDSSolicitudes";
            grdSolicitudes.DataBind();
        }

        protected void grdSolicitudesOtras_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.SolicitudesAlmacenDetalle + "?IdPedido=" + grdSolicitudesOtras.SelectedValue + "&Tienda=" + grdSolicitudesOtras.DataKeys[grdSolicitudesOtras.SelectedRow.RowIndex].Values["IdTienda"].ToString());

        }

        protected void grdSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.SolicitudesAlmacenDetalle + "?IdPedido=" + grdSolicitudes.SelectedValue + "&Tienda=" + Contexto.IdTienda);
        }

        protected void ddlEstados_DataBound(object sender, EventArgs e)
        {
            //Traducción de los estados al idioma. El valor que viene de BD es la clave del recurso al que corresponde
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (ListItem item in ddlEstados.Items)
            {
                item.Text = rm.GetString(item.Text);
            }
        }

        protected void grdSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Traducción de los estados al idioma. El valor que viene de BD es la clave del recurso al que corresponde

            string idPedido;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist;
                DataView dvEstados;

                dvEstados = new DataView();
                dvEstados = (DataView)SDSEstadosAlmacen.Select(DataSourceSelectArguments.Empty);
                ddllist = (DropDownList)e.Row.FindControl("ddlEstadoSolicitud");
                ddllist.DataSource = dvEstados;
                ddllist.DataTextField = "Resource";
                ddllist.DataValueField = "IdEstado";
                ddllist.DataBind();

                foreach (ListItem item in ddllist.Items)
                {
                    item.Text = rm.GetString(item.Text);
                }

                // 22/03/2014 INICIO
                int idEstado = (int)DataBinder.Eval(e.Row.DataItem, "IdEstado");
                ddllist.SelectedValue = ((System.Data.DataRowView)(e.Row.DataItem)).Row["IdEstado"].ToString();
                // Deshabilitamos cuando está en estado vendido.
                //ddllist.Enabled  = (idEstado != 6);

                // 22/03/2014 FIN

                idPedido = DataBinder.Eval(e.Row.DataItem, "IdPedido").ToString();
                if (lstSolicitudes.Contains(idPedido))
                {
                    e.Row.BackColor = System.Drawing.Color.Azure;
                }

            }


        }

        protected void grdSolicitudesOtras_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string idPedido;
            //Traducción de los estados al idioma. El valor que viene de BD es la clave del recurso al que corresponde
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist;
                DataView dvEstados;

                dvEstados = new DataView();
                ddllist = (DropDownList)e.Row.FindControl("ddlEstadoSolicitud");
                dvEstados = (DataView)SDSEstadosOtras.Select(DataSourceSelectArguments.Empty);
                ddllist.DataSource = dvEstados;
                ddllist.DataTextField = "Resource";
                ddllist.DataValueField = "IdEstado";
                ddllist.DataBind();

                foreach (ListItem item in ddllist.Items)
                {
                    item.Text = rm.GetString(item.Text);
                }

                ddllist.SelectedValue = ((System.Data.DataRowView)(e.Row.DataItem)).Row["IdEstado"].ToString();
                //e.Row.Cells[11].Text = rm.GetString(((DataRowView)e.Row.DataItem).Row["EstadoSolicitudResource"].ToString());

                dvEstados = new DataView();
                ddllist = (DropDownList)e.Row.FindControl("ddlIdTienda");
                SDSTiendasProducto.SelectParameters["IdArticulo"].DefaultValue = ((System.Data.DataRowView)(e.Row.DataItem)).Row["IdArticulo"].ToString();
                dvEstados = (DataView)SDSTiendasProducto.Select(DataSourceSelectArguments.Empty);

                DataTable uniqueTable = dvEstados.ToTable(true, "IdTienda");
                dvEstados = new DataView(uniqueTable);
                ddllist.DataSource = dvEstados;
                ddllist.DataTextField = "IdTienda";
                ddllist.DataValueField = "IdTienda";
                ddllist.DataBind();

                foreach (ListItem item in ddllist.Items)
                {
                    item.Text = rm.GetString(item.Text);
                }

                ddllist.SelectedValue = ((System.Data.DataRowView)(e.Row.DataItem)).Row["IdTienda"].ToString();

                idPedido = DataBinder.Eval(e.Row.DataItem, "IdPedido").ToString();

                if (lstSolicitudes.Contains(idPedido))
                {
                    e.Row.BackColor = System.Drawing.Color.Azure;
                }


            }


        }

        protected void fecha_TextChanged(object sender, EventArgs e)
        {

            grdSolicitudes.DataSourceID = "SqlDataSolicitudesPorDia";
            grdSolicitudes.DataBind();


        }

        protected void ddlEstadoSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            String IdEstado;
            String IdPedido;
            String IdArticulo;
            String Talla;
            DropDownList ddllist;
            Int64 idCarrito = 0;

            DropDownList drop = (DropDownList)sender;
            IdEstado = drop.SelectedValue;

            foreach (GridViewRow row in grdSolicitudes.Rows)
            {
                ddllist = (DropDownList)row.FindControl("ddlEstadoSolicitud");

                if (drop.ClientID == ddllist.ClientID)
                {
                    IdPedido = grdSolicitudes.DataKeys[row.RowIndex].Values["IdPedido"].ToString();
                    IdArticulo = grdSolicitudes.DataKeys[row.RowIndex].Values["IdArticulo"].ToString();
                    Talla = grdSolicitudes.DataKeys[row.RowIndex].Values["Talla"].ToString();
                    SDSSolicitudes.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                    SDSSolicitudes.UpdateCommand = "AVE_PedidosCambiarEstadoSolicitud";
                    SDSSolicitudes.UpdateParameters.Add("IdPedido", IdPedido);
                    SDSSolicitudes.UpdateParameters.Add("IdEstado", IdEstado);
                    SDSSolicitudes.Update();
                    if (IdEstado == "1") { HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5); }
                    //hacemos update
                    if (((System.Web.UI.WebControls.ListControl)(drop)).SelectedItem.Text == "Vendido")
                    {

                        v = new DLLGestionVenta.ProcesarVenta();
                        v.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                        if (Session["idCarrito"] != null) { idCarrito = Int64.Parse(Session["IdCarrito"].ToString()); }

                        Session["IdPedido"] = IdPedido;
                        ViewState["talla"] = Talla;
                        ViewState["IdArticulo"] = IdArticulo;
                        añadiralcarrito(IdPedido);
                        añadirLineaCarrito();

                        if (!v.ComprobarStock(Int64.Parse(IdPedido), idCarrito))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "StockSolicitud", "alert(' Se dispone a vender una Referencia sin Stock Registrado.');", true);
                            return;

                        }


                    }
                    break;
                }
            }

        }

        protected void ddlEstadoSolicitudotros_SelectedIndexChanged(object sender, EventArgs e)
        {
            String IdEstado;
            String IdPedido;
            String IdArticulo;
            String Talla;
            DropDownList ddllist;
            DropDownList ddlTienda;
            DLLGestionVenta.ProcesarVenta v;
            int resp = 0;

            DropDownList drop = (DropDownList)sender;
            IdEstado = drop.SelectedValue;
            string tienda;

            AlmacenPiaguiWS almacenPiagui = new AlmacenPiaguiWS();
            string codigoAlmacenCentral = almacenPiagui.ObtenerAlmacen(UserACPiaguiAppSettings, PassACPiaguiAppSettings);
            string almacen = "T-" + codigoAlmacenCentral;

            foreach (GridViewRow row in grdSolicitudesOtras.Rows)
            {
                ddllist = (DropDownList)row.FindControl("ddlEstadoSolicitud");
                ddlTienda = (DropDownList)row.FindControl("ddlIdTienda");

                if (drop.ClientID == ddllist.ClientID)
                {
                    tienda = ddlTienda.SelectedValue.ToString();

                    if (((System.Web.UI.WebControls.ListControl)(drop)).SelectedItem.Text == "Confirmado" && tienda != almacen)
                    {
                        //vamos a controlar la ws del online para no generar dato erroneo
                        String Url = AVE.CLS.AVEUtils.GetURLWsOnline("SModdoOnlineSoap");
                        //codigo comentado sobre comprobar una url
                        if (!Comun.CheckURLWs(Url, 10000))
                        {
                            grdSolicitudesOtras.DataBind();
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "SOLIURL", "alert('No esta accesible la Ws para tramitar esta operación. Intentelo mas tarde.');", true);
                            return;

                        }

                    }


                    IdPedido = grdSolicitudesOtras.DataKeys[row.RowIndex].Values["IdPedido"].ToString();
                    IdArticulo = grdSolicitudesOtras.DataKeys[row.RowIndex].Values["IdArticulo"].ToString();
                    Talla = grdSolicitudesOtras.DataKeys[row.RowIndex].Values["Talla"].ToString();
                    SDSSolicitudes.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                    SDSSolicitudes.UpdateCommand = "AVE_PedidosCambiarEstadoSolicitud";
                    SDSSolicitudes.UpdateParameters.Add("IdPedido", IdPedido);
                    SDSSolicitudes.UpdateParameters.Add("IdEstado", IdEstado);
                    SDSSolicitudes.Update();
                    if (IdEstado == "1") { HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5); }
                    //hacemos update
                    if (((System.Web.UI.WebControls.ListControl)(drop)).SelectedItem.Text == "Confirmado" && tienda != almacen)
                    {

                        v = new ProcesarVenta();
                        v.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
                        v.Terminal = Contexto.IdTerminal;
                        resp = v.GenerarTraspasoAutomatico(Int64.Parse(IdPedido));
                    }
                    else if (((System.Web.UI.WebControls.ListControl)(drop)).SelectedItem.Text == "Confirmado" && tienda == almacen)
                    {
                        string referencia = almacenPiagui.GetReferencia(IdArticulo);
                        string[] resultado = new string[4];
                                                
                        resultado = almacenPiagui.CrearPedido(UserACPiaguiAppSettings, PassACPiaguiAppSettings, "1", almacen, almacen, Contexto.IdTienda, IdArticulo, Talla, 1, Convert.ToInt32(Contexto.IdEmpleado));

                        if (resultado[0] == "OK")
                        {
                            //Devolvemos ok con el Nº de pedido que nos genera el WS
                            string script = string.Empty;
                            ClientScriptManager cs = Page.ClientScript;
                            script = "Pedido Nº " + resultado[2] + " realizado correctamente!";
                            cs.RegisterStartupScript(this.GetType(), "correcto", "alert('" + script + "');", true);
                        }

                        if (resultado[0] == "ERROR")
                        {
                            //Devolvemos Error
                            string script = string.Empty;
                            ClientScriptManager cs = Page.ClientScript;
                            script = "Ha ocurrido un error al realizar el pedido a Almacen Central. ";
                            script += resultado[1];
                            cs.RegisterStartupScript(this.GetType(), "error", "alert('" + script + "');", true);
                        }
                    }
                    break;
                }
            }

        }

        protected void añadiralcarrito(string IdPedido)
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
        protected int ValidaArticulosCarrito(string idCarrito)
        {
            int numArticulos = 0;
            DLLGestionVenta.ProcesarVenta objCarrito;
            try
            {
                objCarrito = new DLLGestionVenta.ProcesarVenta();
                objCarrito.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objCarrito.GetArticulosCarrito(idCarrito);
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }
            return numArticulos;
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
            int ArtiCarrito = ValidaArticulosCarrito(Session["IdCarrito"].ToString());

            
            miMaster.MuestraArticulosCarrito(Convert.ToString(ArtiCarrito));

            CalculoPromocion_Carrito();


        }

        protected void ddlIdTiendaSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {


            String IdTienda;
            String IdPedido;
            DropDownList ddllist;

            DropDownList drop = (DropDownList)sender;
            IdTienda = drop.SelectedValue;

            foreach (GridViewRow row in grdSolicitudesOtras.Rows)
            {
                ddllist = (DropDownList)row.FindControl("ddlIdTienda");

                if (drop.ClientID == ddllist.ClientID)
                {
                    IdPedido = grdSolicitudesOtras.DataKeys[row.RowIndex].Value.ToString();

                    SDSPedidosCambiarTienda.InsertParameters["IdPedido"].DefaultValue = IdPedido;
                    SDSPedidosCambiarTienda.InsertParameters["idTienda"].DefaultValue = IdTienda;
                    SDSPedidosCambiarTienda.Insert();

                    break;
                }
            }
            grdSolicitudes.DataSourceID = "SDSSolicitudes";
            grdSolicitudes.DataBind();
            grdSolicitudesOtras.DataSourceID = "SDSSolicitudesOtras";
            grdSolicitudesOtras.DataBind();
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

    }


}
