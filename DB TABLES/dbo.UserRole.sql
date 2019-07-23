CREATE TABLE [dbo].[UserRole] (
    [RoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

