CREATE TABLE [dbo].[UserSetting] (
    [UserSettingId] INT           IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [SettingName]   VARCHAR (50)  NOT NULL,
    [SettingValue]  VARCHAR (MAX) NULL,
    [ValueTypeCode] INT           NOT NULL,
    [CreatedOn]     DATETIME      NOT NULL,
    [CreatedBy]     INT           NOT NULL,
    [ModifiedOn]    DATETIME      NOT NULL,
    [ModifiedBy]    INT           NOT NULL,
    CONSTRAINT [PK_UserSetting] PRIMARY KEY CLUSTERED ([UserSettingId] ASC),
    CONSTRAINT [FK_UserSetting_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])
);

