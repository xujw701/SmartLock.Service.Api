CREATE TABLE [dbo].[Company]
(
	CompanyId INT NOT NULL IDENTITY(1, 1),
	CompanyName NVARCHAR(2048) NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Company] TO [SmartELockServiceLoginUserRole]
GO