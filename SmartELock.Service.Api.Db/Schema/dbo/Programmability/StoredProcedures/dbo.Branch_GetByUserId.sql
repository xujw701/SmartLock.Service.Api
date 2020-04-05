CREATE PROCEDURE [dbo].[Branch_GetByUserId]
    @userId INT
AS
    SELECT [dbo].[Branch].[BranchId]
          ,[dbo].[Branch].[CompanyId]
          ,[dbo].[Branch].[BranchName]
          ,[dbo].[Branch].[Address]
          ,[dbo].[Branch].[CreatedOn]
          ,[dbo].[Branch].[UpdatedOn]
    FROM [dbo].[Branch] LEFT JOIN [dbo].[User]
    ON [dbo].[Branch].CompanyId = [dbo].[User].CompanyId
    WHERE [dbo].[User].UserId = @userId

RETURN
GO

GRANT EXECUTE ON [dbo].[Branch_GetByUserId] TO [SmartELockServiceLoginUserRole]
GO