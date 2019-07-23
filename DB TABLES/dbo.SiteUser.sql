CREATE TABLE [dbo].[SiteUser] (
    [UserID]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50)  NULL,
    [EmailID]  NVARCHAR (50)  NULL,
    [Password] NVARCHAR (50)  NULL,
    [Address]  NVARCHAR (150) NULL,
    [RoleID]   INT            NULL,
    CONSTRAINT [PK_SiteUser] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_SiteUser_UserRole] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[UserRole] ([RoleID])
);

