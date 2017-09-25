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
using AVE;

public partial class DetallesProducto :  AVE.CLS.Cls_Session 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            DataTable dtDetalles = new DataTable();

            // Obtenemos todos los detalles del producto
            if (Request.QueryString["Producto"] != null | Request.QueryString["IdArticulo"] != null)
            {
                if (Request.QueryString.Count == 1)
                {
                    AVE_ArticuloDetalleObtener.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                    dtDetalles = ((DataView)AVE_ArticuloDetalleObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];
                }
            }

            // Si se han encontrado los detalles del producto
            if (dtDetalles.Rows.Count > 0)
            {
                lblProveedor.Text = dtDetalles.Rows[0]["Proveedor"].ToString();
                lblIdArticulo.Text = Request.QueryString["IdArticulo"].ToString();
                lblReferencia.Text = dtDetalles.Rows[0]["Referencia"].ToString();
                lblModelo.Text = dtDetalles.Rows[0]["Modelo"].ToString();
                lblDescripcion.Text = dtDetalles.Rows[0]["Descripcion"].ToString();
                lblColor.Text = dtDetalles.Rows[0]["Color"].ToString();
                lblObservaciones.Text = dtDetalles.Rows[0]["Observaciones"].ToString();
            }
        }

    }

}
