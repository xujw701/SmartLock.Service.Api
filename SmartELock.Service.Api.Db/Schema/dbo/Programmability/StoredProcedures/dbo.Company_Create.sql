CREATE PROCEDURE [dbo].[Company_Create]
    @companyName NVARCHAR(2048)
AS
    INSERT INTO [dbo].[Company] (CompanyName)
    VALUES (@companyName);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[Company_Create] TO [SmartELockServiceLoginUserRole]
GO