CREATE TABLE [dbo].[News] (
    [NewsId]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (80)  NOT NULL,
    [Summary]    NVARCHAR (200) NULL,
    [Link]       NVARCHAR (MAX) NULL,
    [Article]    NVARCHAR (MAX) NULL,
    [CreatedOn]  DATETIME       NOT NULL,
    [CreatedBy]  INT            NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL,
    [ModifiedBy] INT            NOT NULL,
    CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED ([NewsId] ASC)
);

