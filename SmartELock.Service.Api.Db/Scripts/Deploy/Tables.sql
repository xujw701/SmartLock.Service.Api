--CREATE TABLE [dbo].[SuperAdmin]
--(
--	SuperAdminId INT NOT NULL IDENTITY(1, 1),
--	UserName NVARCHAR(255) NOT NULL UNIQUE,
--	Password NVARCHAR(255) NOT NULL,
--	Token NVARCHAR(2048),
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_SuperAdmin] PRIMARY KEY ([SuperAdminId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[SuperAdmin] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[KeyboxAsset]
--(
--	KeyboxAssetId INT NOT NULL IDENTITY(1, 1),
--	Uuid NVARCHAR(1024) NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_KeyboxAsset] PRIMARY KEY ([KeyboxAssetId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[KeyboxAsset] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[UserRole]
--(
--	UserRoleId INT NOT NULL IDENTITY(1, 1),
--	RoleName NVARCHAR(255) NOT NULL UNIQUE,
--	CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleId])
--)
--GO

--GRANT SELECT ON [dbo].[UserRole] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[ResPortrait]
--(
--	ResPortraitId INT NOT NULL IDENTITY(1, 1),
--	Url NVARCHAR(2048) NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_ResPortrait] PRIMARY KEY ([ResPortraitId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[ResPortrait] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[Company]
--(
--	CompanyId INT NOT NULL IDENTITY(1, 1),
--	CompanyName NVARCHAR(2048) NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[Company] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[Branch]
--(
--	BranchId INT NOT NULL IDENTITY(1, 1),
--	CompanyId INT NOT NULL,
--	BranchName NVARCHAR(2048) NOT NULL,
--	Address NVARCHAR(MAX),
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_Branch] PRIMARY KEY ([BranchId]),
--	CONSTRAINT [FK_Branch_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[Branch] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[User]
--(
--	UserId INT NOT NULL IDENTITY(1, 1),
--	CompanyId INT NOT NULL,
--	BranchId INT NOT NULL,
--	FirstName NVARCHAR(255) NOT NULL,
--	LastName NVARCHAR(255) NOT NULL,
--	Email NVARCHAR(1024),
--	Phone NVARCHAR(255),
--	UserName NVARCHAR(255) NOT NULL UNIQUE,
--	Password NVARCHAR(255) NOT NULL,
--	Token NVARCHAR(2048),
--	Individual BIT NOT NULL,
--	UserRoleId INT NOT NULL,
--	ResPortraitId INT,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
--	CONSTRAINT [FK_User_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
--	CONSTRAINT [FK_User_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId]),
--	CONSTRAINT [FK_User_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole]([UserRoleId]),
--	CONSTRAINT [FK_User_ResPortraitId] FOREIGN KEY ([ResPortraitId]) REFERENCES [ResPortrait]([ResPortraitId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[User] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[TmpUser]
--(
--	TmpUserId INT NOT NULL IDENTITY(1, 1),
--	UserId INT NOT NULL,
--	FirstName NVARCHAR(255) NOT NULL,
--	LastName NVARCHAR(255) NOT NULL,
--	Pin NVARCHAR(255) NOT NULL,
--	Token NVARCHAR(2048) NOT NULL,
--	ValidStartOn DATETIME2 NOT NULL,
--	ValidEndOn DATETIME2 NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_TmpUser] PRIMARY KEY ([TmpUserId]),
--	CONSTRAINT [FK_TmpUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[TmpUser] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[Property]
--(
--	PropertyId INT NOT NULL IDENTITY(1, 1),
--	CompanyId INT NOT NULL,
--	BranchId INT NOT NULL,
--	PropertyName NVARCHAR(2048) NOT NULL,
--	Address NVARCHAR(MAX) NOT NULL,
--	Notes NVARCHAR(MAX),
--	Price NVARCHAR(255),
--	Bedrooms FLOAT,
--	Bathrooms FLOAT,
--	FloorArea FLOAT,
--	LandArea FLOAT,
--	Latitude DECIMAL(18, 12),
--	Longitude DECIMAL(18, 12),
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_Property] PRIMARY KEY ([PropertyId]),
--    CONSTRAINT [FK_Property_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
--	CONSTRAINT [FK_Property_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[Property] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[ResProperty]
--(
--	ResPropertyId INT NOT NULL IDENTITY(1, 1),
--	PropertyId INT NOT NULL,
--	Url NVARCHAR(2048) NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_ResProperty] PRIMARY KEY ([ResPropertyId]),
--	CONSTRAINT [FK_ResProperty_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[ResProperty] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[Keybox]
--(
--	KeyboxId INT NOT NULL IDENTITY(1, 1),
--	CompanyId INT NOT NULL,
--	BranchId INT NOT NULL,
--	KeyboxAssetId INT NOT NULL UNIQUE,
--	Uuid NVARCHAR(1024) NOT NULL,
--	UserId INT,
--	PropertyId INT,
--	KeyboxName NVARCHAR(2048) NOT NULL,
--	BatteryLevel INT NOT NULL,
--	Pin NVARCHAR(255) NOT NULL,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_Keybox] PRIMARY KEY ([KeyboxId]),
--	CONSTRAINT [FK_Keybox_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
--	CONSTRAINT [FK_Keybox_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId]),
--	CONSTRAINT [FK_Keybox_KeyboxAssetId] FOREIGN KEY ([KeyboxAssetId]) REFERENCES [KeyboxAsset]([KeyboxAssetId]),
--	CONSTRAINT [FK_Keybox_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
--	CONSTRAINT [FK_Keybox_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[Keybox] TO [SmartELockServiceLoginUserRole]
--GO


--CREATE TABLE [dbo].[KeyboxHistory]
--(
--	KeyboxHistoryId INT NOT NULL IDENTITY(1, 1),
--	KeyboxId INT NOT NULL,
--	UserId INT NOT NULL,
--	TmpUserId INT,
--	PropertyId INT NOT NULL,
--	InOn DATETIME2 NOT NULL,
--	OutOn DATETIME2,
--	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
--	CONSTRAINT [PK_KeyboxHistory] PRIMARY KEY ([KeyboxHistoryId]),
--	CONSTRAINT [FK_KeyboxHistory_KeyboxId] FOREIGN KEY ([KeyboxId]) REFERENCES [Keybox]([KeyboxId]),
--	CONSTRAINT [FK_KeyboxHistory_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
--	CONSTRAINT [FK_KeyboxHistory_TmpUserId] FOREIGN KEY ([TmpUserId]) REFERENCES [TmpUser]([TmpUserId]),
--	CONSTRAINT [FK_KeyboxHistory_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
--)
--GO

--GRANT SELECT, INSERT, UPDATE ON [dbo].[KeyboxHistory] TO [SmartELockServiceLoginUserRole]
--GO