CREATE PROCEDURE [dbo].[User_Token]
    @userId int,
    @token nvarchar(2048)
AS
    UPDATE [dbo].[User]
    SET Token = @token, UpdatedOn = SYSUTCDATETIME()
    WHERE UserId = @userId
RETURN
GO

GRANT EXECUTE ON [dbo].[User_Token] TO [SmartELockServiceLoginUserRole]
GO