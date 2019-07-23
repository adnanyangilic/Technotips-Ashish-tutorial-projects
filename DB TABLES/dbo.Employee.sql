CREATE TABLE [dbo].[Employee] (
    [EmployeeID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (255) NULL,
    [DepartmentID] INT           NOT NULL,
    [Adress]       VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID])
);

