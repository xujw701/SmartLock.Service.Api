CREATE TABLE [dbo].[Branch]
(
	BranchId INT NOT NULL IDENTITY(1, 1),
	CompanyId INT NOT NULL,
	BranchName NVARCHAR(2048) NOT NULL,
	Address NVARCHAR(MAX),
	CreatedOn DATETIME2,
	UpdatedOn DATETIME2,
	CONSTRAINT [PK_Branch] PRIMARY KEY ([BranchId]),
	CONSTRAINT [FK_Branch_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Branch] TO [SmartELockServiceLoginUserRole]
GO