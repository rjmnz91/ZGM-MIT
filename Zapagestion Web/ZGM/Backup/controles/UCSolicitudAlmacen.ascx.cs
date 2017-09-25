using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AVE.controles
{
    public partial class UCSolicitudAlmacen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(typeof(string), "", "ANTES", true);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "AUT", true);
                var miMaster = (MasterPage)((SolicitudesAlmacen)(this.Parent.Parent.Parent)).Master  ;
                //Comprobamos la visualizacion del Mensaje
                if (AVE.Configuracion.ComprobarSolicitudesPendientes())
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "SI SOL", true);
                   ((Panel)miMaster.FindControl("PanelAviso")).Visible = true;

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "NO SOL", true);
                   ((Panel)miMaster.FindControl("PanelAviso")).Visible = false;
                }
            }
            grdSolicitudes.DataSourceID = "SDSSolicitudes";
            grdSolicitudes.DataBind();
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdSolicitudes.DataSourceID = "SDSSolicitudes";
            grdSolicitudes.DataBind();
        }

        protected void grdSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.SolicitudesAlmacenDetalle + "?IdPedido=" + grdSolicitudes.SelectedValue);
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
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist;
                DataView dvEstados;

                dvEstados = new DataView();
                ddllist = (DropDownList)e.Row.FindControl("ddlEstadoSolicitud");
                dvEstados = (DataView)SDSEstados.Select(DataSourceSelectArguments.Empty);
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
            DropDownList ddllist;

            DropDownList drop = (DropDownList)sender;
            IdEstado = drop.SelectedValue;

            foreach (GridViewRow row in grdSolicitudes.Rows)
            {
                ddllist = (DropDownList)row.FindControl("ddlEstadoSolicitud");

                if (drop.ClientID == ddllist.ClientID)
                {
                    IdPedido = grdSolicitudes.DataKeys[row.RowIndex].Value.ToString();
                    SDSSolicitudes.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                    SDSSolicitudes.UpdateCommand = "AVE_PedidosCambiarEstadoSolicitud";
                    SDSSolicitudes.UpdateParameters.Add("IdPedido", IdPedido);
                    SDSSolicitudes.UpdateParameters.Add("IdEstado", IdEstado);
                    SDSSolicitudes.Update();
                    if (IdEstado == "1") { HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5); }
                    //hacemos update
                    break;
                }
            }

        }       


    }
}