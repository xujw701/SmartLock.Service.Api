CREATE TABLE [dbo].[Keybox]
(
	KeyboxId INT NOT NULL IDENTITY(1, 1),
	CompanyId INT NOT NULL,
	BranchId INT NOT NULL,
	KeyboxAssetId INT NOT NULL UNIQUE,
	Uuid NVARCHAR(1024) NOT NULL,
	UserId INT,
	PropertyId INT,
	KeyboxName NVARCHAR(2048) NOT NULL,
	BatteryLevel INT NOT NULL,
	Pin NVARCHAR(255) NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_Keybox] PRIMARY KEY ([KeyboxId]),
	CONSTRAINT [FK_Keybox_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
	CONSTRAINT [FK_Keybox_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId]),
	CONSTRAINT [FK_Keybox_KeyboxAssetId] FOREIGN KEY ([KeyboxAssetId]) REFERENCES [KeyboxAsset]([KeyboxAssetId]),
	CONSTRAINT [FK_Keybox_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
	CONSTRAINT [FK_Keybox_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Keybox] TO [SmartELockServiceLoginUserRole]
GO