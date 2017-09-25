using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AVE.controles
{
    public partial class UCCLiente9 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            visibilidad(false);
            if (Session["TiendaCamper"] == null)
                Session["TiendaCamper"] = Comun.checkTiendaCamper();
           
            if (Convert.ToInt32(Session["TiendaCamper"].ToString()) > 0) this.txt_id_tarjeta_c9.Enabled = false;
            else this.txt_id_tarjeta_c9.Enabled = true;
           
        }

        public void cargar2()
        {
            ws.cls_Cliente9 c9 = new ws.cls_Cliente9();
            c9.strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
            ws.cls_Cliente9.ConsultaBeneficios cb = new ws.cls_Cliente9.ConsultaBeneficios();
            cb.idTargeta = txt_id_tarjeta_c9.Text;
            cb.idTienda = AVE.Contexto.IdTienda;
            cb.idTerminal = AVE.Contexto.IdTerminal;

            String ret = c9.InvokeWS_ConsultaBeneficios(ref cb);
            if (!String.IsNullOrEmpty(ret))
            {
                ret = ret.Replace("Puntos Net", "Cliente 9");
                error.Text = ret;
                lbl_Nombre.Text = "";
                lbl_Mail.Text = "";
                lbl_Shoelover.Text = "";
                lbl_Telefono.Text = "";
                lbl_Celular.Text = "";
                lbl_Aniversario.Text = "";
                lbl_Cumpleaños.Text = "";

                lbl_Puntos.Text = "";
                lbl_ParesAcumulados.Text = "";
                lbl_PromedioPar.Text = "";
                lbl_BolsasAcumuladas.Text = "";
                lbl_PromedioBolsa.Text = "";
                visibilidad(false);
            }
            else
            {
                error.Text = "";
                lbl_Nombre.Text = cb.nombre;
                lbl_Mail.Text = cb.mail;
                lbl_Shoelover.Text = cb.strNivelActual;
                lbl_Telefono.Text = cb.telefono;
                lbl_Celular.Text = cb.celular;
                lbl_Aniversario.Text = cb.aniversario;
                lbl_Cumpleaños.Text = cb.cumpleanos;

                lbl_Puntos.Text = cb.puntos;
                lbl_ParesAcumulados.Text = cb.paresAcumulados;
                lbl_PromedioPar.Text = cb.promedioPar;
                lbl_BolsasAcumuladas.Text = cb.bolsasAcumuladas;
                lbl_PromedioBolsa.Text = cb.promedioBolsa;
                visibilidad(true);
            }
        }
        private void visibilidad(Boolean b)
        {
            tab_beneficios.Visible = b;
            d_cliente.Visible = b;
            p_lbl_cliente.Visible = b;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String url = System.Configuration.ConfigurationManager.AppSettings["URL_WS_C9"].ToString();

            if (!Comun.CheckURLWs(url, 10000))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CLIENTE 9", "alert('El Servicio de CLIENTE 9 no esta accesible.');", true);
                return;
            }
            
            cargar2();
        }
    }
}