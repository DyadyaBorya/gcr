CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    CONSTRAINT [PK_webpages_OAuthMembership] PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC)
);

