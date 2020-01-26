CREATE PROCEDURE [dbo].[User_Get]
    @userId INT
AS
    SELECT * FROM [dbo].[User]
    WHERE [dbo].[User].UserId = @userId

RETURN
GO

GRANT EXECUTE ON [dbo].[User_Get] TO [SmartELockServiceLoginUserRole]
GO