CREATE TABLE [dbo].[KeyboxAsset]
(
	KeyboxAssetId INT NOT NULL IDENTITY(1, 1),
	Uuid NVARCHAR(1024) NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_KeyboxAsset] PRIMARY KEY ([KeyboxAssetId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[KeyboxAsset] TO [SmartELockServiceLoginUserRole]
GO