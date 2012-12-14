CREATE TABLE [dbo].[HomePagePhoto] (
    [HomePagePhotoId] INT           IDENTITY (1, 1) NOT NULL,
    [PhotoPath]       VARCHAR (256) NOT NULL,
    [DisplayOrder]    INT           NOT NULL,
    [CreatedOn]       DATETIME      NOT NULL,
    [CreatedBy]       INT           NOT NULL,
    [ModifiedOn]      DATETIME      NOT NULL,
    [ModifiedBy]      INT           NOT NULL,
    CONSTRAINT [PK_HomePagePhoto] PRIMARY KEY CLUSTERED ([HomePagePhotoId] ASC)
);

