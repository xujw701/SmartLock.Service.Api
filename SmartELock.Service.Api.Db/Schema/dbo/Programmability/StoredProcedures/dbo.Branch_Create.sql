CREATE PROCEDURE [dbo].[Branch_Create]
    @companyId INT,
    @branchName NVARCHAR(2048),
    @address NVARCHAR(MAX)
AS
    INSERT INTO [dbo].[Branch] (CompanyId, BranchName, Address)
    VALUES (@companyId, @branchName, @address);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[Branch_Create] TO [SmartELockServiceLoginUserRole]
GO