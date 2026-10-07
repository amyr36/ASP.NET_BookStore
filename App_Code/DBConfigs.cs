using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.App_Code
{
    public class DBConfigs
    {
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
    }
}