
namespace GCR.Model
{
    using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GCR.Core;
    
    internal partial class DB : DbContext
    {
        public DB()
            : base(DB.EntityFrameworkConnectionString)
        { 
            OnConstructorCalled();
        }
        
        public DB(DbConnection existingConnection)
            : base(existingConnection, false)
        { 
            OnConstructorCalled();
        }

        /// <summary>
        /// Main entity framework style connection string for the application.
        /// </summary>
        protected static string EntityFrameworkConnectionString
        {
            get
            {
                var cs = GCR.Core.Configuration.DatabaseConnectionSetting;
                if (cs != null)
                {
                    return new System.Data.EntityClient.EntityConnectionStringBuilder
                    {
                        Metadata = "res://*/DB.csdl|res://*/DB.ssdl|res://*/DB.msl",
                        Provider = cs.ProviderName,
                        ProviderConnectionString = cs.ConnectionString
                    }.ConnectionString;
                }
                return null;
            }
        }
    }
}
