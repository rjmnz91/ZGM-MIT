using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AVE
{
    public partial class Pedidos : CLS.Cls_Session 
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

            //Registramos en javascript el control de la caja de texto para usarlo desde otras funciones jscript;
            string script = "var txtFiltro = document.getElementById('" + txtFiltro.ClientID + "');";
            Page.ClientScript.RegisterStartupScript(typeof(string), "txtFiltro", script, true);
            
            if (!Page.IsPostBack)
            {
                //Incluimos una fila vacía al principio
                DataTable dt = ((DataView)SDSEstados.Select(new DataSourceSelectArguments())).Table;
                dt.Rows.InsertAt(dt.NewRow(), 0);
                ddlEstados.DataSource = dt;
                ddlEstados.DataBind();

              
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdPedidos.DataSourceID = SDSPedidosBuscar.ID;
            grdPedidos.DataBind();
        } 
        
        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            //Asociamos ahora para que no busque todos en la carga incial
            grdPedidos.DataSourceID = SDSPedidosAvanzado.ID;
            grdPedidos.DataBind();
        }

        protected void grdPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.PedidosDetalle + "?IdPedido=" + grdPedidos.SelectedValue);
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

        protected void grdPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Traducción de los estados al idioma. El valor que viene de BD es la clave del recurso al que corresponde
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Resource", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[11].Text = rm.GetString(((DataRowView)e.Row.DataItem).Row["EstadosPedidosResource"].ToString());
            }
        }

        protected void btnCambiarBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            pnlAvanzada.Visible = !pnlAvanzada.Visible;
            pnlNormal.Visible = !pnlNormal.Visible;
        }


    }
}
