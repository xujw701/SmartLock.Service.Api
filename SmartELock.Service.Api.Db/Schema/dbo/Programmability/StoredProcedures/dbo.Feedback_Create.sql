CREATE PROCEDURE [dbo].[Feedback_Create]
    @userId INT,
    @content NVARCHAR(MAX)
AS
    INSERT INTO [dbo].[Feedback] (UserId, Content)
    VALUES (@userId, @content)

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT)

RETURN
GO

GRANT EXECUTE ON [dbo].[Feedback_Create] TO [SmartELockServiceLoginUserRole]
GO