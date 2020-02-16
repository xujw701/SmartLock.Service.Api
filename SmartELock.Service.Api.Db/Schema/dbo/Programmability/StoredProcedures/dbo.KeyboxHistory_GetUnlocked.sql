CREATE PROCEDURE [dbo].[KeyboxHistory_GetUnlocked]
    @keyboxId INT,
    @userId INT,
    @tmpUserId INT,
    @propertyId INT
AS

    SELECT * FROM [dbo].[KeyboxHistory]
    WHERE [dbo].[KeyboxHistory].KeyboxId = @keyboxId
        AND [dbo].[KeyboxHistory].UserId = @userId
        AND [dbo].[KeyboxHistory].PropertyId = @propertyId
        AND [dbo].[KeyboxHistory].OutOn IS NULL
    ORDER BY  [dbo].[KeyboxHistory].InOn DESC

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxHistory_GetUnlocked] TO [SmartELockServiceLoginUserRole]
GO