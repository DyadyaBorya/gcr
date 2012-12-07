

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "DB"
//     Connection String:      "data source=QCIAPP3\SQL2012;initial catalog=WebpageTest;integrated security=True;MultipleActiveResultSets=True;App=CodeFirstGenerator"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace GCR.Model
{
    // ************************************************************************
    // Database context
    public partial class DB : DbContext
    {
        public IDbSet<UserProfile> UserProfile { get; set; } // UserProfile
        public IDbSet<WebpagesMembership> WebpagesMembership { get; set; } // webpages_Membership
        public IDbSet<WebpagesOAuthMembership> WebpagesOAuthMembership { get; set; } // webpages_OAuthMembership
        public IDbSet<WebpagesRoles> WebpagesRoles { get; set; } // webpages_Roles
        public IDbSet<WebpagesUsersInRoles> WebpagesUsersInRoles { get; set; } // webpages_UsersInRoles

        static DB()
        {
            Database.SetInitializer<DB>(null);
        }

        public DB()
            : base("Name=DB")
        {
        }

        public DB(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new WebpagesMembershipConfiguration());
            modelBuilder.Configurations.Add(new WebpagesOAuthMembershipConfiguration());
            modelBuilder.Configurations.Add(new WebpagesRolesConfiguration());
            modelBuilder.Configurations.Add(new WebpagesUsersInRolesConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

    // UserProfile
    public partial class UserProfile
    {
        public int UserId { get; set; } // UserId (Primary key)
        public string UserName { get; set; } // UserName

        // Reverse navigation
        public virtual ICollection<WebpagesUsersInRoles> WebpagesUsersInRoles { get; set; } // webpages_UsersInRoles.FK_webpages_UsersInRoles_UserProfile;

        public UserProfile()
        {
            WebpagesUsersInRoles = new List<WebpagesUsersInRoles>();
        }
    }

    // webpages_Membership
    public partial class WebpagesMembership
    {
        public int UserId { get; set; } // UserId (Primary key)
        public DateTime? CreateDate { get; set; } // CreateDate
        public string ConfirmationToken { get; set; } // ConfirmationToken
        public bool? IsConfirmed { get; set; } // IsConfirmed
        public DateTime? LastPasswordFailureDate { get; set; } // LastPasswordFailureDate
        public int PasswordFailuresSinceLastSuccess { get; set; } // PasswordFailuresSinceLastSuccess
        public string Password { get; set; } // Password
        public DateTime? PasswordChangedDate { get; set; } // PasswordChangedDate
        public string PasswordSalt { get; set; } // PasswordSalt
        public string PasswordVerificationToken { get; set; } // PasswordVerificationToken
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; } // PasswordVerificationTokenExpirationDate

        public WebpagesMembership()
        {
            IsConfirmed = false;
            PasswordFailuresSinceLastSuccess = 0;
        }
    }

    // webpages_OAuthMembership
    public partial class WebpagesOAuthMembership
    {
        public string Provider { get; set; } // Provider (Primary key)
        public string ProviderUserId { get; set; } // ProviderUserId (Primary key)
        public int UserId { get; set; } // UserId
    }

    // webpages_Roles
    public partial class WebpagesRoles
    {
        public int RoleId { get; set; } // RoleId (Primary key)
        public string RoleName { get; set; } // RoleName

        // Reverse navigation
        public virtual ICollection<WebpagesUsersInRoles> WebpagesUsersInRoles { get; set; } // webpages_UsersInRoles.FK_webpages_UsersInRoles_webpages_Roles;

        public WebpagesRoles()
        {
            WebpagesUsersInRoles = new List<WebpagesUsersInRoles>();
        }
    }

    // webpages_UsersInRoles
    public partial class WebpagesUsersInRoles
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int RoleId { get; set; } // RoleId (Primary key)

        // Foreign keys
        public virtual UserProfile UserProfile { get; set; } //  UserId - FK_webpages_UsersInRoles_UserProfile
        public virtual WebpagesRoles WebpagesRoles { get; set; } //  RoleId - FK_webpages_UsersInRoles_webpages_Roles
    }


    // ************************************************************************
    // POCO Configuration

    // UserProfile
    public partial class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            ToTable("dbo.UserProfile");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("UserName").IsOptional();
        }
    }

    // webpages_Membership
    public partial class WebpagesMembershipConfiguration : EntityTypeConfiguration<WebpagesMembership>
    {
        public WebpagesMembershipConfiguration()
        {
            ToTable("dbo.webpages_Membership");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsOptional();
            Property(x => x.ConfirmationToken).HasColumnName("ConfirmationToken").IsOptional().HasMaxLength(128);
            Property(x => x.IsConfirmed).HasColumnName("IsConfirmed").IsOptional();
            Property(x => x.LastPasswordFailureDate).HasColumnName("LastPasswordFailureDate").IsOptional();
            Property(x => x.PasswordFailuresSinceLastSuccess).HasColumnName("PasswordFailuresSinceLastSuccess").IsRequired();
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(128);
            Property(x => x.PasswordChangedDate).HasColumnName("PasswordChangedDate").IsOptional();
            Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasMaxLength(128);
            Property(x => x.PasswordVerificationToken).HasColumnName("PasswordVerificationToken").IsOptional().HasMaxLength(128);
            Property(x => x.PasswordVerificationTokenExpirationDate).HasColumnName("PasswordVerificationTokenExpirationDate").IsOptional();
        }
    }

    // webpages_OAuthMembership
    public partial class WebpagesOAuthMembershipConfiguration : EntityTypeConfiguration<WebpagesOAuthMembership>
    {
        public WebpagesOAuthMembershipConfiguration()
        {
            ToTable("dbo.webpages_OAuthMembership");
            HasKey(x => new { x.Provider, x.ProviderUserId });

            Property(x => x.Provider).HasColumnName("Provider").IsRequired().HasMaxLength(30);
            Property(x => x.ProviderUserId).HasColumnName("ProviderUserId").IsRequired().HasMaxLength(100);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
        }
    }

    // webpages_Roles
    public partial class WebpagesRolesConfiguration : EntityTypeConfiguration<WebpagesRoles>
    {
        public WebpagesRolesConfiguration()
        {
            ToTable("dbo.webpages_Roles");
            HasKey(x => x.RoleId);

            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(256);
        }
    }

    // webpages_UsersInRoles
    public partial class WebpagesUsersInRolesConfiguration : EntityTypeConfiguration<WebpagesUsersInRoles>
    {
        public WebpagesUsersInRolesConfiguration()
        {
            ToTable("dbo.webpages_UsersInRoles");
            HasKey(x => new { x.UserId, x.RoleId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Foreign keys
            HasRequired(a => a.UserProfile).WithMany(b => b.WebpagesUsersInRoles).HasForeignKey(c => c.UserId); // FK_webpages_UsersInRoles_UserProfile
            HasRequired(a => a.WebpagesRoles).WithMany(b => b.WebpagesUsersInRoles).HasForeignKey(c => c.RoleId); // FK_webpages_UsersInRoles_webpages_Roles
        }
    }

}

