CREATE PROCEDURE [dbo].[Company_GetByCompanyName]
    @companyName NVARCHAR(2048)
AS
    SELECT * FROM [dbo].[Company]
    WHERE [dbo].[Company].CompanyName = @companyName

RETURN
GO

GRANT EXECUTE ON [dbo].[Company_GetByCompanyName] TO [SmartELockServiceLoginUserRole]
GO