CREATE PROCEDURE [dbo].[Keybox_GetByUuid]
    @uuid NVARCHAR(1024)
AS
    SELECT * FROM [dbo].[Keybox]
    WHERE [dbo].[Keybox].Uuid = @uuid

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_GetByUuid] TO [SmartELockServiceLoginUserRole]
GO