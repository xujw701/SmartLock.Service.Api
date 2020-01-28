CREATE PROCEDURE [dbo].[KeyboxAsset_Create]
    @uuid NVARCHAR(1024)
AS
    INSERT INTO [dbo].[KeyboxAsset] (Uuid)
    VALUES (@uuid);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxAsset_Create] TO [SmartELockServiceLoginUserRole]
GO