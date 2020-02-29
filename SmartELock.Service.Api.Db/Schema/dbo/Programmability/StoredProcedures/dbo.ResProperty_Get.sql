CREATE PROCEDURE [dbo].[ResProperty_Get]
    @resPropertyId int
AS
    SELECT * FROM [dbo].[ResProperty]
    WHERE ResPropertyId = @resPropertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[ResProperty_Get] TO [SmartELockServiceLoginUserRole]
GO