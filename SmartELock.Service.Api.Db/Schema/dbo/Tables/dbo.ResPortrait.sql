﻿CREATE TABLE [dbo].[ResPortrait]
(
	ResPortraitId INT NOT NULL IDENTITY(1, 1),
	Url NVARCHAR(2048) NOT NULL,
	CreatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	UpdatedOn DATETIME2 NOT NULL DEFAULT (sysutcdatetime()),
	CONSTRAINT [PK_ResPortrait] PRIMARY KEY ([ResPortraitId])
)
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[ResPortrait] TO [SmartELockServiceLoginUserRole]
GO