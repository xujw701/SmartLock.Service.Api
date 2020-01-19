CREATE TABLE [dbo].[UserRole]
(
	UserRoleId INT NOT NULL IDENTITY(1, 1),
	RoleName NVARCHAR(255) NOT NULL UNIQUE,
	CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleId])
)
GO

GRANT SELECT ON [dbo].[UserRole] TO [SmartELockServiceLoginUserRole]
GO