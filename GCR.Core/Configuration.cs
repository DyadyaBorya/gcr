using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core
{
    public static class Configuration
    {

        public static ConnectionStringSettings DatabaseConnectionSetting
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DB"];
            }
        }
        
        /// <summary>
        /// Main connection string to the database for application.
        /// </summary>
        public static string DatabaseConnectionString
        {
            get { return DatabaseConnectionSetting.ConnectionString; }
        }
    }
}
