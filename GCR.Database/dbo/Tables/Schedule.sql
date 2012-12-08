CREATE TABLE [dbo].[Schedule] (
    [ScheduleId] INT      IDENTITY (1, 1) NOT NULL,
    [SeasonId]   INT      NULL,
    [TeamId]     INT      NULL,
    [Date]       DATETIME NULL,
    [AtHome]     BIT      NOT NULL,
    [CreatedOn]  DATETIME NOT NULL,
    [CreatedBy]  INT      NOT NULL,
    [ModifiedOn] DATETIME NOT NULL,
    [Modifiedby] INT      NOT NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    CONSTRAINT [FK_Schedule_Season] FOREIGN KEY ([SeasonId]) REFERENCES [dbo].[Season] ([SeasonId]),
    CONSTRAINT [FK_Schedule_Team] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([TeamId])
);

