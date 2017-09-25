using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace AVE
{
    public partial class FinalizaCompra : CLS.Cls_Session
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HyperLink lab = (HyperLink)this.Page.FindControl("lblNumArt");
                if (lab != null) lab.Visible = false;

                if (!IsPostBack)
                {
                    FinalizaVenta fVenta;

                    if (Session["FVENTA"] != null)
                    {

                        fVenta = (FinalizaVenta)Session["FVENTA"];

                        NumTicket.Text = fVenta.Ticket;
                        NomCliente.Text = fVenta.cliente;
                        Entrega.Text = fVenta.Entrega;

                        Session["FVENTA"] = null;
                        Session["objCliente"] = null;
                    }

                }
            }
            catch (Exception err)
            {
                Session["Error"] = err.Message;
                Session["lastURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect("Error.aspx");
            }
        }

        protected void cmdImprimirTicket_Click(object sender, EventArgs e)
        {

            string sTipoInforme = "Ticket";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = cn.CreateCommand())
                {

                    //cmd.CommandText = string.Format("Select top 1 isnull(ImporteEuro,0) from N_TICKETS WHERE id_ticket = {0}", NumTicket.Text);
                    //cmd.CommandType = System.Data.CommandType.Text;
                    //float Total = (float)cmd.ExecuteScalar();

                    cmd.CommandText = "AVE_PRINT_VENTA_DETALLES";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter("@SESSIONID", Session.SessionID));
                    cmd.Parameters.Add(new SqlParameter("@TDA", AVE.Contexto.IdTienda));
                    cmd.Parameters.Add(new SqlParameter("@TICK", NumTicket.Text));
                    cmd.Parameters.Add(new SqlParameter("@TICK_SIMPLIF", "0"));
                    cmd.Parameters.Add(new SqlParameter("@POS_MONEDA", "IZQ"));
                    cmd.Parameters.Add(new SqlParameter("@MONEDA", "$"));
                    cmd.Parameters.Add(new SqlParameter("@TEXTOPAGO", ""));
                    cmd.Parameters.Add(new SqlParameter("@FECHA", AVE.Contexto.FechaSesion.ToString("dd/MM/yyyy")));
                    cmd.ExecuteNonQuery();

                    // Vista del ticket.
                    cmd.CommandText = string.Format("SELECT * FROM PR_VIEW_TICKET_{0}", Session.SessionID.ToString());
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Cliente 9
                        if (reader["LineaC9"] == null || reader["LineaC9"].ToString().Length > 0)
                        {
                            sTipoInforme = "TicketC9";
                        }
                    }
                }
                cn.Close();
            }

            string szURL = string.Format("~/ImprimirTicket.aspx?Tipo={0}&idTicket={1}", sTipoInforme, NumTicket.Text);
            Response.Redirect(szURL, false);
            return;

        }
    }
}