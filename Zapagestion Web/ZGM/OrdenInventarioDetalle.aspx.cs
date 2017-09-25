using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class OrdenInventarioDetalle :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((QueryStringParameter)sdsOI.SelectParameters["IdOrdenInventario"]).QueryStringField = Constantes.QueryString.IdOrdenInventario;
            sdsOI.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            dvOI.DataBind();

            //Si la OI está en fechas y no está ya realizada, habilitamos el botón "Realizar"
            DateTime fInicio = (DateTime)((DataRowView)(dvOI.DataItem)).Row["FechaInventarioIni"];
            DateTime fFin = (DateTime)((DataRowView)(dvOI.DataItem)).Row["FechaInventarioFin"];
            bool realizada = (bool)((DataRowView)(dvOI.DataItem)).Row["Realizada"];
            if (!realizada && DateTime.Today >= fInicio && DateTime.Today <= fFin)
                btnRealizar.Enabled = true;
            else
                btnRealizar.Enabled = false;

        }

        protected void btnRealizar_Click(object sender, EventArgs e)
        {
            //Creamos un nuevo inventario pendiente
            sdsInventario.InsertParameters["IdTipoInventario"].DefaultValue = ((DataRowView)(dvOI.DataItem)).Row["IdTipoInventario"].ToString();
            sdsInventario.InsertParameters["IdTipoVista"].DefaultValue = ((DataRowView)(dvOI.DataItem)).Row["IdTipoVista"].ToString();
            sdsInventario.InsertParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            sdsInventario.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
            sdsInventario.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;
            sdsInventario.InsertParameters["IdOrdenInventario"].DefaultValue = Request.QueryString[Constantes.QueryString.IdOrdenInventario].ToString();
            sdsInventario.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
            sdsInventario.Insert();
            Response.Redirect(Constantes.Paginas.InventarioDetalle);
        }
       
    }
}
