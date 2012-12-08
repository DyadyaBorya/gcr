CREATE TABLE [dbo].[Team] (
    [TeamId]     INT            IDENTITY (1, 1) NOT NULL,
    [TeamName]   NVARCHAR (60)  NOT NULL,
    [Location]   NVARCHAR (100) NULL,
    [IsActive]   BIT            NOT NULL,
    [IsSelf]     BIT            NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL,
    [CreatedBy]  INT            NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL,
    [ModifiedBy] INT            NOT NULL,
    CONSTRAINT [PK_Opponent] PRIMARY KEY CLUSTERED ([TeamId] ASC)
);

