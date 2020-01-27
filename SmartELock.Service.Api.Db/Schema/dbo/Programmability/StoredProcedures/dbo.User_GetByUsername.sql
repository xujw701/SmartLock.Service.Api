CREATE PROCEDURE [dbo].[User_GetByUsername]
    @username NVARCHAR(255)
AS
    SELECT * FROM [dbo].[User]
    WHERE [dbo].[User].UserName = @username

RETURN
GO

GRANT EXECUTE ON [dbo].[User_GetByUsername] TO [SmartELockServiceLoginUserRole]
GO