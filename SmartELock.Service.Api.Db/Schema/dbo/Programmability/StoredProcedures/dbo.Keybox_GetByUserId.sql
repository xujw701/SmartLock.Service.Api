CREATE PROCEDURE [dbo].[Keybox_GetByUserId]
    @userId int
AS
    SELECT [dbo].[Keybox].KeyboxId,
           [dbo].[Keybox].CompanyId,
           [dbo].[Keybox].BranchId,
           [dbo].[Keybox].KeyboxAssetId,
           [dbo].[Keybox].Uuid,
           [dbo].[Keybox].UserId,
           [dbo].[Keybox].PropertyId,
           [dbo].[Property].Address,
           [dbo].[Keybox].KeyboxName,
           [dbo].[Keybox].BatteryLevel,
           [dbo].[Keybox].Pin,
           [dbo].[Keybox].CreatedOn,
           [dbo].[Keybox].UpdatedOn
    FROM [dbo].[Keybox] LEFT JOIN [dbo].[Property]
    ON [dbo].[Keybox].PropertyId = [dbo].[Property].PropertyId
    WHERE [dbo].[Keybox].UserId IS NOT NULL
        AND [dbo].[Keybox].UserId = @userId

RETURN
GO

GRANT EXECUTE ON [dbo].[Keybox_GetByUserId] TO [SmartELockServiceLoginUserRole]
GO