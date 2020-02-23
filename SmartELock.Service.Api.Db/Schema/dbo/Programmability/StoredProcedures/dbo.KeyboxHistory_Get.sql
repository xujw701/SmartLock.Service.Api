CREATE PROCEDURE [dbo].[KeyboxHistory_Get]
    @keyboxId int,
    @propertyId int
AS
    SELECT [dbo].[KeyboxHistory].KeyboxHistoryId,
           [dbo].[KeyboxHistory].KeyboxId,
           [dbo].[KeyboxHistory].UserId,
           [dbo].[KeyboxHistory].TmpUserId,
           [dbo].[KeyboxHistory].PropertyId,
           [dbo].[User].FirstName,
           [dbo].[User].LastName,
           [dbo].[User].ResPortraitId,
           [dbo].[KeyboxHistory].InOn,
           [dbo].[KeyboxHistory].OutOn,
           [dbo].[KeyboxHistory].CreatedOn,
           [dbo].[KeyboxHistory].UpdatedOn
	FROM [dbo].[KeyboxHistory] INNER JOIN [dbo].[User]
	ON [dbo].[KeyboxHistory].UserId = [dbo].[User].UserId
    WHERE [dbo].[KeyboxHistory].KeyboxId = @keyboxId
        AND [dbo].[KeyboxHistory].PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxHistory_Get] TO [SmartELockServiceLoginUserRole]
GO