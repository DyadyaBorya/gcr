CREATE TABLE [dbo].[Season] (
    [SeasonId]   INT           IDENTITY (1, 1) NOT NULL,
    [SeasonName] NVARCHAR (60) NOT NULL,
    [StartDate]  DATE          NOT NULL,
    [EndDate]    DATE          NOT NULL,
    [CreatedOn]  DATETIME      NOT NULL,
    [CreatedBy]  INT           NOT NULL,
    [ModifiedOn] DATETIME      NOT NULL,
    [ModifiedBy] INT           NOT NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED ([SeasonId] ASC)
);

