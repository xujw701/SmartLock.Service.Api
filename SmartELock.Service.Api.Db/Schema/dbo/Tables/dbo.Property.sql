CREATE TABLE [dbo].[Property]
(
	PropertyId INT NOT NULL IDENTITY(1, 1),
	CompanyId INT NOT NULL,
	BranchId INT NOT NULL,
	PropertyName NVARCHAR(2048) NOT NULL,
	Address NVARCHAR(MAX) NOT NULL,
	Bedrooms DECIMAL,
	Bathrooms DECIMAL,
	FloorArea DECIMAL,
	LandArea DECIMAL,
	CreatedOn DATETIME2 NOT NULL,
	UpdatedOn DATETIME2 NOT NULL,
	CONSTRAINT [PK_Property] PRIMARY KEY ([PropertyId]),
    CONSTRAINT [FK_Property_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
	CONSTRAINT [FK_Property_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Property] TO [SmartELockServiceLoginUserRole]
GO