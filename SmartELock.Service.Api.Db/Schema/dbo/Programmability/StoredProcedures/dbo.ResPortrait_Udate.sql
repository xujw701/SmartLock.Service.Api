CREATE PROCEDURE [dbo].[ResPortrait_Update]
    @resPortraitId int,
    @url nvarchar(1024)
AS
    UPDATE [dbo].[ResPortrait]
    SET [Url] = @url, UpdatedOn = SYSUTCDATETIME()
    WHERE ResPortraitId = @resPortraitId

RETURN
GO

GRANT EXECUTE ON [dbo].[ResPortrait_Update] TO [SmartELockServiceLoginUserRole]
GO