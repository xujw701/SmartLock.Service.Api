CREATE PROCEDURE [dbo].[Company_Get]
    @companyId INT
AS
    SELECT * FROM [dbo].[Company]
    WHERE [dbo].[Company].CompanyId = @companyId

RETURN
GO

GRANT EXECUTE ON [dbo].[Company_Get] TO [SmartELockServiceLoginUserRole]
GO