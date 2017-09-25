using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE
{
    public partial class ConversionMoneda :  CLS.Cls_Session 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal importe;

            if (Request.QueryString[Constantes.QueryString.ImporteConversion] != null &&
                decimal.TryParse(Request.QueryString[Constantes.QueryString.ImporteConversion].ToString(), out importe))
            {
                SDSConversion.SelectParameters[0].DefaultValue = importe.ToString();
                GridView1.DataBind();

            }
        }
       
    }
}
