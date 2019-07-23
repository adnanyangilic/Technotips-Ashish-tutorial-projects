CREATE TABLE [dbo].[State] (
    [StateID]   INT            IDENTITY (1, 1) NOT NULL,
    [StateName] NVARCHAR (150) NULL,
    [CountryID] INT            NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateID] ASC),
    CONSTRAINT [FK_State_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID])
);

