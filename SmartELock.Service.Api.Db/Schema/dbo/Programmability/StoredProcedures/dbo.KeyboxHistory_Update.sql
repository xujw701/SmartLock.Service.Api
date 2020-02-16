CREATE PROCEDURE [dbo].[KeyboxHistory_Update]
    @keyboxHistoryId INT,
    @outOn DATETIME2
AS

    UPDATE [dbo].[KeyboxHistory]
    SET OutOn = @outOn,
        UpdatedOn = SYSUTCDATETIME()
    WHERE KeyboxHistoryId = @keyboxHistoryId

RETURN
GO

GRANT EXECUTE ON [dbo].[KeyboxHistory_Update] TO [SmartELockServiceLoginUserRole]
GO