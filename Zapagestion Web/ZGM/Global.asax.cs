using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;

namespace Site
{
    public class Global : System.Web.HttpApplication
    {
        // Log
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start(object sender, EventArgs e)
        {

            // Initialize log4net.
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        
        {
            if (Request.Cookies[Constantes.CteCookie.Idioma] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Request.Cookies[Constantes.CteCookie.Idioma].Value);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Request.Cookies[Constantes.CteCookie.Idioma].Value);
            }

            Uri u = HttpContext.Current.Request.Url;
            if (u.AbsolutePath.ToLower().Contains("reserved.reportviewerwebcontrol.axd") &&
             !u.Query.ToLower().Contains("iterationid"))
                HttpContext.Current.RewritePath(u.PathAndQuery + "&IterationId=0");


        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException)
                return;

            log.Fatal(ex);
        }

        protected void Session_End(object sender, EventArgs e)
        {
        
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
      
    }
}