using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AVE
{
	public partial class Inventarios :  CLS.Cls_Session 
	{       
        private int? idInventario
        {
            get
            {
                if (ViewState["idInventario"] != null)
                    return ((int)ViewState["idInventario"]);
                else
                    return null;
            }
            set
            {
                ViewState["idInventario"] = value;
            }
        }

        private int? idOrdenInventario
        {
            get
            {
                if (ViewState["idOrdenInventario"] != null)
                    return ((int)ViewState["idOrdenInventario"]);
                else
                    return null;
            }
            set
            {
                ViewState["idOrdenInventario"] = value;
            }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                EvaluarInventarioExistente();
            }
		}
      
        protected void EvaluarInventarioExistente()
        {
            DataView dv;
            SDSInventario.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
            SDSInventario.SelectParameters["IdEmpleado"].DefaultValue = AVE.Contexto.IdEmpleado;
            SDSInventario.SelectParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal ;
            dv = (DataView)SDSInventario.Select(new DataSourceSelectArguments());

            if (dv.Count < 1)
            {
                //Mostramos el panel de Ordenes de Inventario (OI)
                pnlOI.Visible = true;
                pnlNuevo.Visible = false;
                pnlPendiente.Visible = false;

                SDSOrdenesInventario.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            }
            else
            {
                //Ya hay un inventario pendiente, que mostramos
                pnlNuevo.Visible = false;
                pnlOI.Visible = false;
                pnlPendiente.Visible = true;

                idInventario = (int)dv[0]["IdInventario"];
                idOrdenInventario = dv[0].Row.Field<int?>("IdOrdenInventario");
                pnlNuevo.Visible = false;
                pnlPendiente.Visible = true;
                this.lblTipo.Text = dv[0]["TipoInventario"].ToString();
                this.lblModalidad.Text = dv[0]["TipoVista"].ToString();
                this.lblEmpresa.Text = dv[0]["Empresa"].ToString();
                this.lblTienda.Text = dv[0]["Tienda"].ToString();
                this.lblEmpleado.Text = AVE.Contexto.IdEmpleado;
                this.lblTerminal.Text = Contexto.IdTerminal;
                this.lblOrdenInventario.Text = dv[0]["OrdenInventario"].ToString();
                this.lblFechaCreacion.Text = dv[0]["FechaCreacion"].ToString();
                this.lblFechaUltimaModificacion.Text = dv[0]["FechaModificacion"].ToString();
                this.lblArticulosTotales.Text = dv[0]["Cantidad"].ToString();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SDSInventario.InsertParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                SDSInventario.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                SDSInventario.InsertParameters["IdTipoVista"].DefaultValue = Configuracion.ObtenerClave(Configuracion.Clave.ModoVisualizacion).ToString();
                SDSInventario.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;
                SDSInventario.InsertParameters["IdOrdenInventario"].DefaultValue = null;
                SDSInventario.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
                SDSInventario.Insert();
                Response.Redirect(Constantes.Paginas.InventarioDetalle);
            }
        }

        protected void btnAbrir_Click(object sender, EventArgs e)
        {
            SDSInventario.Select(new DataSourceSelectArguments());
            Response.Redirect(Constantes.Paginas.InventarioDetalle);
        }
        
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SDSInventario.DeleteParameters["IdInventario"].DefaultValue = idInventario.ToString();
            SDSInventario.Delete();
            Response.Redirect(Constantes.Paginas.Inventario);
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            SDSInventario.UpdateParameters["IdInventario"].DefaultValue = idInventario.ToString();
            SDSInventario.UpdateParameters["Usuario"].DefaultValue = Contexto.Usuario;
            SDSInventario.UpdateParameters["IdOrdenInventario"].DefaultValue = idOrdenInventario.ToString();
            SDSInventario.UpdateParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            SDSInventario.UpdateParameters["ObservacionesTienda"].DefaultValue = "";
            SDSInventario.Update();
            Response.Redirect(Constantes.Paginas.Inventario);
        }

        //Cuando se va a crear un inventario nuevo se ocultan los datos de OI y el panel de inventario pendiente
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlOI.Visible = false;
            pnlPendiente.Visible = false;
            pnlNuevo.Visible = true;
        }

        protected void grdOI_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Redirigimos a la página de detalle de la OI
            Response.Redirect(Constantes.Paginas.OrdenInventarioDetalle + "?" + Constantes.QueryString.IdOrdenInventario + "=" + grdOI.SelectedValue);
        }
	}
}
