CREATE PROCEDURE [dbo].[Keybox_Create]
    @companyId INT,
    @branchId INT,
    @keyboxAssetId INT,
    @uuid NVARCHAR(1024),
    @userId INT,
    @keyboxName NVARCHAR(2048),
    @batteryLevel INT,
    @pin NVARCHAR(255)
AS

    INSERT INTO [dbo].[Keybox] (CompanyId, BranchId, KeyboxAssetId, Uuid, UserId, KeyboxName, BatteryLevel, Pin)
    VALUES (@companyId, @branchId, @keyboxAssetId, @uuid, @userId, @keyboxName, @batteryLevel, @pin);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_Create] TO [SmartELockServiceLoginUserRole]
GO