CREATE PROCEDURE [dbo].[User_Update]
    @userId INT,
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

    UPDATE [dbo].[User]
    SET CompanyId = @companyId,
        BranchId = @branchId,
        FirstName = @firstName,
        LastName = @lastName,
        Email = @email,
        Phone = @phone,
        UserName = @userName,
        Password = @password,
        Individual = @individual,
        UserRoleId = @userRoleId,
        UpdatedOn = SYSUTCDATETIME()
    WHERE UserId = @userId

RETURN
GO

GRANT EXECUTE ON [dbo].[User_Update] TO [SmartELockServiceLoginUserRole]
GO