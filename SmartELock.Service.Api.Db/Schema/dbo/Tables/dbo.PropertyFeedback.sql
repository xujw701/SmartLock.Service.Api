CREATE TABLE [dbo].[PropertyFeedback]
(
	PropertyFeedbackId INT NOT NULL IDENTITY(1, 1),
	PropertyId INT NOT NULL,
	UserId INT NOT NULL,
	Content NVARCHAR(MAX) NOT NULL,
	IsRead BIT NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_PropertyFeedback] PRIMARY KEY ([PropertyFeedbackId]),
	CONSTRAINT [FK_PropertyFeedback_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId]),
	CONSTRAINT [FK_PropertyFeedback_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[PropertyFeedback] TO [SmartELockServiceLoginUserRole]
GO