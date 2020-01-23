CREATE PROCEDURE [dbo].[Branch_Get]
    @branchId INT
AS
    SELECT * FROM [dbo].[Branch]
    WHERE [dbo].[Branch].BranchId = @branchId

RETURN
GO

GRANT EXECUTE ON [dbo].[Branch_Get] TO [SmartELockServiceLoginUserRole]
GO