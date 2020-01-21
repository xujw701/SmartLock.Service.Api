CREATE PROCEDURE [dbo].[SuperAdmin_GetByUsername]
    @username NVARCHAR(255)
AS
    SELECT * FROM [dbo].[SuperAdmin]
    WHERE [dbo].[SuperAdmin].UserName = @username

RETURN
GO

GRANT EXECUTE ON [dbo].[SuperAdmin_GetByUsername] TO [SmartELockServiceLoginUserRole]
GO