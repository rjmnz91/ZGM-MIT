using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PruebaGrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //    ((Label)e.Row.FindControl("Label1")).Attributes.Add("onclick",ClientScript.GetPostBackClientHyperlink(e.Row.FindControl("lnkSelect"), ""));
    }
}
