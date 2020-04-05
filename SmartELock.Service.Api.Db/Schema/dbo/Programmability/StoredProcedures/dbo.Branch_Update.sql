CREATE PROCEDURE [dbo].[Branch_Update]
    @branchId INT,
    @branchName NVARCHAR(2048),
    @address NVARCHAR(MAX)
AS

    UPDATE [dbo].[Branch]
    SET BranchName = @branchName,
        Address = @address,
        UpdatedOn = SYSUTCDATETIME()
    WHERE BranchId = @branchId

RETURN
GO

GRANT EXECUTE ON [dbo].[Branch_Update] TO [SmartELockServiceLoginUserRole]
GO