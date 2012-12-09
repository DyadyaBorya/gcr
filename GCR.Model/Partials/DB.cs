
namespace GCR.Model
{
    using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
    
    internal partial class DB : DbContext
    {
        public DB(DbConnection existingConnection)
            : base(existingConnection, false)
        { 
        }
    }
}
