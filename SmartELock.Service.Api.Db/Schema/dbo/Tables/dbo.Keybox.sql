CREATE TABLE [dbo].[Keybox]
(
	KeyboxId INT NOT NULL IDENTITY(1, 1),
	KeyboxAssetId INT NOT NULL UNIQUE,
	Uuid NVARCHAR(2048) NOT NULL,
	CompanyId INT NOT NULL,
	BranchId INT NOT NULL,
	UserId INT NOT NULL,
	PropertyId INT NOT NULL,
	KeyboxName NVARCHAR(2048) NOT NULL,
	BatteryLevel DECIMAL NOT NULL,
	Pin NVARCHAR(255) NOT NULL,
	BulletinBoard NVARCHAR(MAX),
	Latitude DECIMAL(18, 12),
	Longitude DECIMAL(18, 12),
	CreatedOn DATETIME2 NOT NULL,
	UpdatedOn DATETIME2 NOT NULL,
	CONSTRAINT [PK_Keybox] PRIMARY KEY ([KeyboxId]),
	CONSTRAINT [FK_Keybox_KeyboxAssetId] FOREIGN KEY ([KeyboxAssetId]) REFERENCES [KeyboxAsset]([KeyboxAssetId]),
	CONSTRAINT [FK_Keybox_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
	CONSTRAINT [FK_Keybox_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId]),
	CONSTRAINT [FK_Keybox_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
	CONSTRAINT [FK_Keybox_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Keybox] TO [SmartELockServiceLoginUserRole]
GO