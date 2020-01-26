CREATE PROCEDURE [dbo].[User_Create]
    @companyId INT,
    @branchId INT,
    @firstName NVARCHAR(255),
    @lastName NVARCHAR(255),
    @email NVARCHAR(1024),
    @phone NVARCHAR(255),
    @userName NVARCHAR(255),
    @password NVARCHAR(255),
    @individual BIT,
    @userRoleId INT
AS
    INSERT INTO [dbo].[User] (CompanyId, BranchId, FirstName, LastName, Email, Phone, UserName, Password, Individual, UserRoleId)
    VALUES (@companyId, @branchId, @firstName, @lastName, @email, @phone, @userName, @password, @individual, @userRoleId);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[User_Create] TO [SmartELockServiceLoginUserRole]
GO