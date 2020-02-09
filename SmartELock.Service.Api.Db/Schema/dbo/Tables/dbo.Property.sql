CREATE TABLE [dbo].[Property]
(
	PropertyId INT NOT NULL IDENTITY(1, 1),
	CompanyId INT NOT NULL,
	BranchId INT NOT NULL,
	PropertyName NVARCHAR(2048) NOT NULL,
	Address NVARCHAR(MAX) NOT NULL,
	Notes NVARCHAR(MAX),
	Price NVARCHAR(255),
	Bedrooms FLOAT,
	Bathrooms FLOAT,
	FloorArea FLOAT,
	LandArea FLOAT,
	Latitude DECIMAL(18, 12),
	Longitude DECIMAL(18, 12),
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_Property] PRIMARY KEY ([PropertyId]),
    CONSTRAINT [FK_Property_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
	CONSTRAINT [FK_Property_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Property] TO [SmartELockServiceLoginUserRole]
GO