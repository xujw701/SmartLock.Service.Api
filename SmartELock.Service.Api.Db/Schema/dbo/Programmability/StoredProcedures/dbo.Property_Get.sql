CREATE PROCEDURE [dbo].[Property_Get]
    @propertyId int
AS
    SELECT * FROM [dbo].[Property]
    WHERE [dbo].[Property].PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[Property_Get] TO [SmartELockServiceLoginUserRole]
GO