CREATE PROCEDURE [dbo].[KeyboxExtra_GetByUserId]
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
			[dbo].[Keybox].UpdatedOn,
			[dbo].[KeyboxHistory].KeyboxHistoryId,
			[dbo].[KeyboxHistory].UserId AS 'AcessUserId',
			[dbo].[KeyboxHistory].InOn
	FROM ([dbo].[Keybox] INNER JOIN [dbo].[KeyboxHistory]
		  ON [dbo].[Keybox].KeyboxId = [dbo].[KeyboxHistory].KeyboxId) LEFT JOIN [dbo].[Property]
		  ON [dbo].[Keybox].PropertyId = [dbo].[Property].PropertyId
	WHERE [dbo].[KeyboxHistory].UserId = @userId
		AND [dbo].[KeyboxHistory].KeyboxHistoryId IN (SELECT MAX([dbo].[KeyboxHistory].KeyboxHistoryId)
														FROM [dbo].[KeyboxHistory]
														WHERE [UserId] = @userId
														GROUP BY [dbo].[KeyboxHistory].KeyboxId)
	ORDER BY [dbo].[KeyboxHistory].KeyboxHistoryId DESC

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxExtra_GetByUserId] TO [SmartELockServiceLoginUserRole]
GO