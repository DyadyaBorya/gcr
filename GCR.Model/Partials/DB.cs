
namespace GCR.Model
{
    using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GCR.Core;
using GCR.Core.Entities;
    
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

        public override int SaveChanges()
        {
            CheckForAuditItems();
            return base.SaveChanges();
        }

        private void CheckForAuditItems()
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var entity in entities)
            {
                if (entity.State == System.Data.EntityState.Added)
                {
                    var auditable = entity.Entity as IAuditable;
                    if (auditable != null)
                    {
                        UpdateAuditFieldsForInsert(auditable);
                    }
                }
                else if (entity.State == System.Data.EntityState.Modified)
                {
                    var auditable = entity.Entity as IAuditable;
                    if (auditable != null)
                    {
                        UpdateAuditFieldsForUpdate(auditable);
                    }
                }

            }
        }

        private void UpdateAuditFieldsForInsert(IAuditable auditable)
        {
            auditable.CreatedBy = CurrentUser.Identity.UserId;
            auditable.ModifiedBy = CurrentUser.Identity.UserId;
            auditable.CreatedOn = DateTime.Now;
            auditable.ModifiedOn = DateTime.Now;
        }

        private void UpdateAuditFieldsForUpdate(IAuditable auditable)
        {
            auditable.ModifiedBy = CurrentUser.Identity.UserId;
            auditable.ModifiedOn = DateTime.Now;
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
