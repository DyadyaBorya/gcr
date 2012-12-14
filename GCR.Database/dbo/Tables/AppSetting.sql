CREATE TABLE [dbo].[AppSetting] (
    [AppSettingId]  INT           IDENTITY (1, 1) NOT NULL,
    [SettingName]   VARCHAR (50)  NOT NULL,
    [SettingValue]  VARCHAR (MAX) NULL,
    [ValueTypeCode] INT           NOT NULL,
    [CreatedOn]     DATETIME      NOT NULL,
    [CreatedBy]     INT           NOT NULL,
    [ModifiedOn]    DATETIME      NOT NULL,
    [ModifiedBy]    INT           NOT NULL,
    CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED ([AppSettingId] ASC)
);

