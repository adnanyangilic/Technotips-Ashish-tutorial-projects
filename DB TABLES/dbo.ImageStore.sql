CREATE TABLE [dbo].[ImageStore] (
    [ImageID]   INT             IDENTITY (1, 1) NOT NULL,
    [ImageName] NVARCHAR (1000) NULL,
    [ImageByte] IMAGE           NULL,
    [ImagePath] NVARCHAR (1000) NULL,
    CONSTRAINT [PK_ImageStore] PRIMARY KEY CLUSTERED ([ImageID] ASC)
);

