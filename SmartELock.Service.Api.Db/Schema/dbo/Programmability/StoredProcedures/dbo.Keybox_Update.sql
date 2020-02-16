CREATE PROCEDURE [dbo].[Keybox_Update]
    @keyboxId INT,
    @companyId INT,
    @branchId INT,
    @keyboxAssetId INT,
    @uuid NVARCHAR(1024),
    @propertyId INT,
    @userId INT,
    @keyboxName NVARCHAR(2048),
    @batteryLevel INT,
    @pin NVARCHAR(255)
AS

    UPDATE [dbo].[Keybox]
    SET CompanyId = @companyId,
        BranchId = @branchId,
        KeyboxAssetId = @keyboxAssetId,
        Uuid = @uuid,
        PropertyId = @propertyId,
        UserId = @userId,
        KeyboxName = @keyboxName,
        BatteryLevel = @batteryLevel,
        Pin = @pin,
        UpdatedOn = SYSUTCDATETIME()
    WHERE KeyboxId = @keyboxId

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_Update] TO [SmartELockServiceLoginUserRole]
GO