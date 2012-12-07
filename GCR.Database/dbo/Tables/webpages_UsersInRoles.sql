CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_webpages_UsersInRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]),
    CONSTRAINT [FK_webpages_UsersInRoles_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])
);

