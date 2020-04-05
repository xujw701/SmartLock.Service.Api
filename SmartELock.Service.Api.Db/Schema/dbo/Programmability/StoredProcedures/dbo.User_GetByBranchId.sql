CREATE PROCEDURE [dbo].[User_GetByBranchId]
    @branchId INT
AS
    SELECT * FROM [dbo].[User]
    WHERE [dbo].[User].BranchId = @branchId

RETURN
GO

GRANT EXECUTE ON [dbo].[User_GetByBranchId] TO [SmartELockServiceLoginUserRole]
GO