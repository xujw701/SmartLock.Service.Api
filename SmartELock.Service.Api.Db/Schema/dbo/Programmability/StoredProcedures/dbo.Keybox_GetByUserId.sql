CREATE PROCEDURE [dbo].[Keybox_GetByUserId]
    @userId int
AS
    SELECT * FROM [dbo].[Keybox]
    WHERE [dbo].[Keybox].UserId IS NOT NULL
        AND [dbo].[Keybox].UserId = @userId

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_GetByUserId] TO [SmartELockServiceLoginUserRole]
GO