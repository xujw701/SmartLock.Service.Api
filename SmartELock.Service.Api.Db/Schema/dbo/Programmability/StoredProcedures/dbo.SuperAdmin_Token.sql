CREATE PROCEDURE [dbo].[SuperAdmin_Token]
    @superAdminId int,
    @token nvarchar(2048)
AS
    UPDATE [dbo].[SuperAdmin]
    SET Token = @token, UpdatedOn = SYSUTCDATETIME()
    WHERE SuperAdminId = @superAdminId
RETURN
GO

GRANT EXECUTE ON [dbo].[SuperAdmin_Token] TO [SmartELockServiceLoginUserRole]
GO