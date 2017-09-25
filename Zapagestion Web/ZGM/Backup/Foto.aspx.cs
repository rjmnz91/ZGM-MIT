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

public partial class Foto :  AVE.CLS.Cls_Session 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Cargamos la url de la fotografía
        FotoArticulo.ImageUrl = this.ResolveUrl(ObtenerURL());
    }


    /// <summary>
    /// Obtenemos la URL de la fotografía, a través del IdArticulo
    /// </summary>
    /// <returns> Retornamos la URL de la fotografía del producto</returns>
    private string ObtenerURL()
    {
        string rutaLocal;
        string rutaVision=String.Empty;

        DataTable dt = ((DataView)AVE_ArticuloFotoObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

        string idTemporada = dt.Rows[0]["idTemporada"].ToString();
        string idProveedor = dt.Rows[0]["idProveedor"].ToString();
        string ModeloProveedor = dt.Rows[0]["ModeloProveedor"].ToString();

        // Construimos la ruta en Local
        rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"] + "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
   
        //if (System.IO.File.Exists(Server.MapPath(rutaLocal)))
       return rutaLocal;
       // else
        //{
        //    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
        //    return rutaVision;
        //}
    }

}
