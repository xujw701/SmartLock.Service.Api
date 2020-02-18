CREATE TABLE [dbo].[Feedback]
(
	FeedbackId INT NOT NULL IDENTITY(1, 1),
	UserId INT NOT NULL,
	Content NVARCHAR(MAX) NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_Feedback] PRIMARY KEY ([FeedbackId]),
	CONSTRAINT [FK_Feedback_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[Feedback] TO [SmartELockServiceLoginUserRole]
GO