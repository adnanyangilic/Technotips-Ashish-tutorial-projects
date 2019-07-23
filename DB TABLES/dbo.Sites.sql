CREATE TABLE [dbo].[Sites] (
    [SitesID]    INT           IDENTITY (1, 1) NOT NULL,
    [EmployeeID] INT           NOT NULL,
    [SiteName]   VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([SitesID] ASC),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);

