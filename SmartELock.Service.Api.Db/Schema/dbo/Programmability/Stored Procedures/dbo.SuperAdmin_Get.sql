CREATE PROCEDURE [dbo].[SuperAdmin_Get]
    @superAdminId INT
AS
    SELECT * FROM [dbo].[SuperAdmin]
    WHERE [dbo].[SuperAdmin].SuperAdminId = @superAdminId

RETURN
GO

GRANT EXECUTE ON [dbo].[SuperAdmin_Get] TO [SmartELockServiceLoginUserRole]
GO