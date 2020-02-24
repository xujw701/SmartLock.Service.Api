CREATE PROCEDURE [dbo].[Keybox_GetHistoryByUserId]
    @userId int
AS
    SELECT *
    FROM [dbo].[Keybox]
    WHERE [dbo].[Keybox].KeyboxId IN 
		(SELECT DISTINCT [KeyboxId]
		 FROM [dbo].[KeyboxHistory]
		 WHERE [UserId] = @userId)
RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_GetHistoryByUserId] TO [SmartELockServiceLoginUserRole]
GO