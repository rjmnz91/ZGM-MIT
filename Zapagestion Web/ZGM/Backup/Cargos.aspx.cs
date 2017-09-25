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
using System.Data.SqlClient;
using Resources;

namespace AVE
{
    public partial class Cargos :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTiendaOrigen.Text = Contexto.IdTienda;
        }
        
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    //Aseguramos que no se arrastra de otro sitio
                    Session["idTiendaDestino"] = null;

                    SDSCrear.InsertParameters["IdCargo"].DefaultValue = this.txtNumeroCargo.Text;
                    SDSCrear.InsertParameters["IdTiendaOrigen"].DefaultValue = Contexto.IdTienda;
                    SDSCrear.InsertParameters["IdTiendaDestino"].DefaultValue = this.cmbTiendaDestino.Text;
                    SDSCrear.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                    SDSCrear.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal ;
                    SDSCrear.InsertParameters["Usuario"].DefaultValue = Contexto.IdEmpleado;
                    SDSCrear.Insert();
                    Response.Redirect(Constantes.Paginas.CargosDetalle + "?IdCargo=" + txtNumeroCargo.Text + "&IdTiendaDestino=" + this.cmbTiendaDestino.Text);
                }
            }
            catch (SqlException sqlEx)
            {
                string script;
                if (sqlEx.Number == 2627)//Excepción si el ID ya está en uso.
                {
                    script = "alert('" + Resource.IdEnUso + "');";
                }
                else //Otro tipo de excepción.
                {
                    script = "alert('" + Resource.Error + "');";
                    
                }
                ClientScript.RegisterStartupScript(typeof(string), "Error", script, true);
            }
        }

        //Generamos un número de cargo automático.
        protected void btnGenerarNumero_Click(object sender, EventArgs e)
        {
            DataView dv;
            SDSGenerar.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
            dv = (DataView)SDSGenerar.Select(new DataSourceSelectArguments());
            txtNumeroCargo.Text = dv[0].Row[0].ToString();
        }
    }
}
