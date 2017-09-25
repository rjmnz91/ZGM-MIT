using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Configuration;
using System.Configuration; 

namespace AVE.CLS
{
    static public class AVEUtils
    {

        static string FormatearFechaParaSQL(string fecha)
        {
            return fecha;
        }

        static string FormatearFechaParaSQL(DateTime fecha)
        {
            return fecha.ToShortDateString();
        }
       /// <summary>
       /// funcion para recuperar la url de una binding
       /// </summary>
       /// <param name="ServicioWsOnline"></param>
       /// <returns></returns>
       public static string GetURLWsOnline(String ServicioWsOnline)
        {
            ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");

            string address=String.Empty;

            for (int i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name == ServicioWsOnline)
                {
                    address = clientSection.Endpoints[i].Address.ToString();
                    break; 
                }
           }

            return address;
            
        }  

    }
}