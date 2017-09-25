using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class BuscarCliente :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Asociamos ahora para que no busque todos en la carga incial
            grdClientes.DataSourceID = SDSClientes.ID;
            grdClientes.DataBind();
        }

        /// <summary>
        /// Al seleccionar un cliente devolvemos a la pantalla que ha invocado a esta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cliente seleccionado
            string idCliente = grdClientes.SelectedValue.ToString();

            //Montamos la dirección de retorno, que será la de pedidos más el idcliente 
            string returnUrl = Request.QueryString["ReturnURL"];
            
            //Quitamos otros parámetros IdCliente que existan
            if (returnUrl.IndexOf("IdCliente") != -1)
            {
                int startIndex = returnUrl.IndexOf("IdCliente");         //empezamos en "IdCliente"
                int endIndex = returnUrl.IndexOf("&", startIndex);     //terminamos en el siguiente & (siguiente parámetro)
                if (endIndex == -1)                                      //Si no hay más & el fin está en el fin de la cadena
                    endIndex = returnUrl.Length;

                returnUrl = returnUrl.Remove(startIndex-1, endIndex - startIndex +1);  //Empezamos en -1 de IdCliente para incluir el &
            }

            //Incluimos el parámetro del IdCliente
            if (returnUrl.IndexOf("?") == -1)
                returnUrl += "?";
            else
                returnUrl += "&";
            returnUrl += "IdCliente=" + idCliente;

            Response.Redirect(returnUrl);
        }
    }
}
