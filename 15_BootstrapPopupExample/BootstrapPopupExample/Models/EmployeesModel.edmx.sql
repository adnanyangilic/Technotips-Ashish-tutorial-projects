
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2017 09:20:20
-- Generated from EDMX file: \\10.31.25.5\ITFejlesztok\Examples\MVC\Tutorial\BootstrapPopupExample\BootstrapPopupExample\Models\EmployeesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EmployeesDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Employee__Depart__286302EC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK__Employee__Depart__286302EC];
GO
IF OBJECT_ID(N'[dbo].[FK__Sites__EmployeeI__36B12243]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sites] DROP CONSTRAINT [FK__Sites__EmployeeI__36B12243];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Department];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[Sites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sites];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [DepartmentID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NULL,
    [DepartmentID] int  NOT NULL,
    [Adress] varchar(255)  NULL
);
GO

-- Creating table 'Sites'
CREATE TABLE [dbo].[Sites] (
    [SitesID] int IDENTITY(1,1) NOT NULL,
    [EmployeeID] int  NOT NULL,
    [SiteName] varchar(255)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DepartmentID] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([DepartmentID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [SitesID] in table 'Sites'
ALTER TABLE [dbo].[Sites]
ADD CONSTRAINT [PK_Sites]
    PRIMARY KEY CLUSTERED ([SitesID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK__Employee__Depart__286302EC]
    FOREIGN KEY ([DepartmentID])
    REFERENCES [dbo].[Departments]
        ([DepartmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Employee__Depart__286302EC'
CREATE INDEX [IX_FK__Employee__Depart__286302EC]
ON [dbo].[Employees]
    ([DepartmentID]);
GO

-- Creating foreign key on [EmployeeID] in table 'Sites'
ALTER TABLE [dbo].[Sites]
ADD CONSTRAINT [FK__Sites__EmployeeI__36B12243]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Sites__EmployeeI__36B12243'
CREATE INDEX [IX_FK__Sites__EmployeeI__36B12243]
ON [dbo].[Sites]
    ([EmployeeID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------