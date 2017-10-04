using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVE
{
    public class Payment
    {
        private static string auth = "";

        private static string operation = "";

        public static String getAuth()
        {
            return auth;
        }
        public static void setAuth(String data)
        {
            auth = data;
        }

        public static String getOperation()
        {
            return operation;
        }
        public static void setOperation(String data)
        {
            operation = data;
        }
    }
}