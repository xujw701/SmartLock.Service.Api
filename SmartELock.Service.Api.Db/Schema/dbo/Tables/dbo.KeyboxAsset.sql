CREATE TABLE [dbo].[KeyboxAsset]
(
	KeyboxAssetId INT NOT NULL IDENTITY(1, 1),
	Uuid NVARCHAR(2048) NOT NULL,
	CreateOn DATETIME2 NOT NULL,
	UpdateOn DATETIME2 NOT NULL,
	CONSTRAINT [PK_KeyboxAsset] PRIMARY KEY ([KeyboxAssetId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[KeyboxAsset] TO [SmartELockServiceLoginUserRole]
GO