CREATE TABLE [dbo].[Members] (
    [MemberId]    INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (25)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [IsActive]    BIT            NOT NULL,
    [MemberSince] INT            NOT NULL,
    [Bio]         NVARCHAR (500) NULL,
    [Photo]       NVARCHAR (256) NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [CreatedBy]   INT            NOT NULL,
    [ModifiedOn]  DATE           NOT NULL,
    [ModifiedBy]  INT            NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([MemberId] ASC)
);

