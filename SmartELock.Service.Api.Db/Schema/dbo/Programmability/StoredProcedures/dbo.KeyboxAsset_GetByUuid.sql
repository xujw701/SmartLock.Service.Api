CREATE PROCEDURE [dbo].[KeyboxAsset_GetByUuid]
    @uuid NVARCHAR(1024)
AS
    SELECT * FROM [dbo].[KeyboxAsset]
    WHERE [dbo].[KeyboxAsset].Uuid = @uuid

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxAsset_GetByUuid] TO [SmartELockServiceLoginUserRole]
GO