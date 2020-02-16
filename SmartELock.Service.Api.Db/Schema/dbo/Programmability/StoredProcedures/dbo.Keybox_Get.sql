CREATE PROCEDURE [dbo].[Keybox_Get]
    @keyboxId int
AS
    SELECT * FROM [dbo].[Keybox]
    WHERE [dbo].[Keybox].KeyboxId = @keyboxId

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_Get] TO [SmartELockServiceLoginUserRole]
GO