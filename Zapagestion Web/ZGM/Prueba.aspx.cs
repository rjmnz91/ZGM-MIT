using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLGestionVenta;  


namespace AVE
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DLLGestionVenta.ProcesarVenta v;
            int resp = 0;
            v = new ProcesarVenta();
            v.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();
            v.Terminal = "1";
            //resp=v.GenerarTraspasoAutomatico(Int64.Parse(TextBox1.Text.ToString())); 
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
             // rc4 encripta = new rc4();
             // String cadenaEncriptada=String.Empty;
             // String cadenaClaro= null;
             // String semilla="llave";
             // String xmlPos="<xmlmpos><amount>1.00</amount><reference>5122013974</reference><id_company>1015</id_company><id_branch>008</id_branch>";
             // xmlPos +="<cd_merchant>52883</cd_merchant><currency>MXN</currency><country>MEX</country><cd_user>1015P5</cd_user><password>BOT10152008</password>";
             // xmlPos +="<cd_usrtrx>USR MPOS</cd_usrtrx></xmlmpos>";
             ////encripta con rc4
             // cadenaEncriptada = encripta.StringToHexString(encripta.Salaa(xmlPos, semilla));
             // //desencripta con rc4
             // cadenaClaro = encripta.Pura(encripta.hexStringToString(cadenaEncriptada),semilla);

           
            Response.Redirect("CarritoDetalle.aspx"); 



        }
    }
}