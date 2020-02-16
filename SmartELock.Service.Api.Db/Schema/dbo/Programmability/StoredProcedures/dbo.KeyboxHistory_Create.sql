CREATE PROCEDURE [dbo].[KeyboxHistory_Create]
    @keyboxId INT,
    @userId INT,
    @tmpUserId INT,
    @propertyId INT,
    @inOn DATETIME2
AS

    INSERT INTO [dbo].[KeyboxHistory] (KeyboxId, UserId, TmpUserId, PropertyId, InOn)
    VALUES (@keyboxId, @userId, @tmpUserId, @propertyId, @inOn);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxHistory_Create] TO [SmartELockServiceLoginUserRole]
GO