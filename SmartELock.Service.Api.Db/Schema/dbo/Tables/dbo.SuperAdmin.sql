CREATE TABLE [dbo].[SuperAdmin]
(
	SuperAdminId INT NOT NULL IDENTITY(1, 1),
	UserName NVARCHAR(255) NOT NULL UNIQUE,
	Password NVARCHAR(255) NOT NULL,
	Token NVARCHAR(2048),
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_SuperAdmin] PRIMARY KEY ([SuperAdminId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[SuperAdmin] TO [SmartELockServiceLoginUserRole]
GO