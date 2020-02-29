CREATE PROCEDURE [dbo].[ResProperty_Update]
    @resPropertyId int,
    @url nvarchar(1024)
AS
    UPDATE [dbo].[ResProperty]
    SET [Url] = @url, UpdatedOn = SYSUTCDATETIME()
    WHERE ResPropertyId = @resPropertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[ResProperty_Update] TO [SmartELockServiceLoginUserRole]
GO