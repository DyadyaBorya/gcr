CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    CONSTRAINT[PK_webpages_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC),
    CONSTRAINT[UK_RoleName] UNIQUE NONCLUSTERED ([RoleName] ASC)
);

