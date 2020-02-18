CREATE PROCEDURE [dbo].[KeyboxHistory_Get]
    @keyboxId int,
    @propertyId int
AS
    SELECT *
	FROM [dbo].[KeyboxHistory] INNER JOIN [dbo].[User]
	ON [dbo].[KeyboxHistory].UserId = [dbo].[User].UserId
    WHERE [dbo].[KeyboxHistory].KeyboxId = @keyboxId
        AND [dbo].[KeyboxHistory].PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxHistory_Get] TO [SmartELockServiceLoginUserRole]
GO