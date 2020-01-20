CREATE TABLE [dbo].[User]
(
	UserId INT NOT NULL IDENTITY(1, 1),
	CompanyId INT NOT NULL,
	BranchId INT NOT NULL,
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255) NOT NULL,
	Email NVARCHAR(1024),
	Phone NVARCHAR(255),
	UserName NVARCHAR(255) NOT NULL UNIQUE,
	Password NVARCHAR(255) NOT NULL,
	Token NVARCHAR(2048),
	Individual BIT NOT NULL,
	UserRoleId INT NOT NULL,
	ResPortraitId INT NOT NULL,
	CreatedOn DATETIME2 NOT NULL,
	UpdatedOn DATETIME2 NOT NULL,
	CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
	CONSTRAINT [FK_User_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
	CONSTRAINT [FK_User_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([BranchId]),
	CONSTRAINT [FK_User_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole]([UserRoleId]),
	CONSTRAINT [FK_User_ResPortraitId] FOREIGN KEY ([ResPortraitId]) REFERENCES [ResPortrait]([ResPortraitId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[User] TO [SmartELockServiceLoginUserRole]
GO