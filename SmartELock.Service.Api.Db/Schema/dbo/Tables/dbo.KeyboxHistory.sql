CREATE TABLE [dbo].[KeyboxHistory]
(
	KeyboxHistoryId INT NOT NULL IDENTITY(1, 1),
	KeyboxId INT NOT NULL,
	UserId INT NOT NULL,
	TmpUserId INT,
	PropertyId INT NOT NULL,
	InOn DATETIME2 NOT NULL,
	OutOn DATETIME2,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_KeyboxHistory] PRIMARY KEY ([KeyboxHistoryId]),
	CONSTRAINT [FK_KeyboxHistory_KeyboxId] FOREIGN KEY ([KeyboxId]) REFERENCES [Keybox]([KeyboxId]),
    CONSTRAINT [FK_KeyboxHistory_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
	CONSTRAINT [FK_KeyboxHistory_TmpUserId] FOREIGN KEY ([TmpUserId]) REFERENCES [TmpUser]([TmpUserId]),
	CONSTRAINT [FK_KeyboxHistory_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[KeyboxHistory] TO [SmartELockServiceLoginUserRole]
GO