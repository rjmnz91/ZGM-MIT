using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVE.CLS
{
    public class Cls_Session : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Url.LocalPath == "/Login.aspx" && string.IsNullOrEmpty(Request.Url.Query) ||
                Request.Url.LocalPath == "/Login.aspx" && !string.IsNullOrEmpty(Request.Url.Query) && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                return;
            
            if (Context.Session != null && Session.IsNewSession)
            {
                HttpCookie newSessionIdCookie = Request.Cookies["ASP.NET_SessionId"];
                if (newSessionIdCookie != null)
                {
                    string newSessionIdCookieValue = newSessionIdCookie.Value;
                    if (newSessionIdCookieValue != string.Empty)
                    {
                        // This means Session was timed Out and New Session was started
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                        
                        // Response.Redirect("Login.aspx");
                    }
                }
            }
        }
    }
}