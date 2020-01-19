CREATE TABLE [dbo].[ResProperty]
(
	ResPropertyId INT NOT NULL IDENTITY(1, 1),
	PropertyId INT NOT NULL,
	Url NVARCHAR(2048) NOT NULL,
	CreatedOn DATETIME2 NOT NULL,
	UpdatedOn DATETIME2 NOT NULL,
	CONSTRAINT [PK_ResProperty] PRIMARY KEY ([ResPropertyId]),
	CONSTRAINT [FK_ResProperty_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([PropertyId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[ResProperty] TO [SmartELockServiceLoginUserRole]
GO