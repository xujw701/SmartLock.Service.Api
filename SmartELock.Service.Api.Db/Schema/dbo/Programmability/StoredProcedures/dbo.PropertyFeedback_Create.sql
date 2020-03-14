CREATE PROCEDURE [dbo].[PropertyFeedback_Create]
    @propertyId INT,
    @userId INT,
    @content NVARCHAR(MAX)
AS
    INSERT INTO [dbo].[PropertyFeedback] (PropertyId, UserId, Content, IsRead)
    VALUES (@propertyId, @userId, @content, 0)

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT)

RETURN
GO

GRANT EXECUTE ON [dbo].[PropertyFeedback_Create] TO [SmartELockServiceLoginUserRole]
GO