CREATE PROCEDURE [dbo].[ResProperty_Delete]
    @resPropertyId int
AS
	IF EXISTS (SELECT * FROM [dbo].[ResProperty] WHERE ResPropertyId = @resPropertyId)
	BEGIN
        DELETE FROM [dbo].[ResProperty]
        WHERE ResPropertyId = @resPropertyId
	END

RETURN
GO

GRANT EXECUTE ON [dbo].[ResProperty_Delete] TO [SmartELockServiceLoginUserRole]
GO