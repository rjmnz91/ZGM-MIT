using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE.controles
{
  
    public partial class UCDetallesProducto : System.Web.UI.UserControl
    {
       
        public string IdArticulo
        {
            get { return AVE_ArticuloDetalleObtener.SelectParameters["idArticulo"].DefaultValue ; }
            set {  AVE_ArticuloDetalleObtener.SelectParameters["idArticulo"].DefaultValue = value; }
        }
        public string modelo
        {
            get { return lblModelo.Text; }

        }
        public void complementario(Boolean valor) {
            lblCSObservaciones.Visible = valor ;
            LtrCSObservaciones.Visible = valor ;  
        }
        public void cargar()
        {
            System.Data.DataTable dtDetalles = new System.Data.DataTable();

            // Obtenemos todos los detalles del producto
            if (!string.IsNullOrEmpty(IdArticulo))
            {

                AVE_ArticuloDetalleObtener.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                dtDetalles = ((System.Data.DataView)AVE_ArticuloDetalleObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

            }

            // Si se han encontrado los detalles del producto
            if (dtDetalles.Rows.Count > 0)
            {
                lblProveedor.Text = dtDetalles.Rows[0]["Proveedor"].ToString();
               // lblIdArticulo.Text =IdArticulo.ToString();
                lblReferencia.Text = dtDetalles.Rows[0]["Referencia"].ToString();
                lblModelo.Text = dtDetalles.Rows[0]["Modelo"].ToString();
                lblDescripcion.Text = dtDetalles.Rows[0]["Descripcion"].ToString();
                lblColor.Text = dtDetalles.Rows[0]["Color"].ToString();
                lblObservaciones.Text = dtDetalles.Rows[0]["Observaciones"].ToString();
                lblCSObservaciones.Text = dtDetalles.Rows[0]["CS_OBS"].ToString();

               
            }
        }
    }
}