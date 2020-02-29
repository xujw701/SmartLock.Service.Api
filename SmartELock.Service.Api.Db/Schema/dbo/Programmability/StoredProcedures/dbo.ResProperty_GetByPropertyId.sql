CREATE PROCEDURE [dbo].[ResProperty_GetByPropertyId]
    @propertyId int
AS
    SELECT * FROM [dbo].[ResProperty]
    WHERE PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[ResProperty_GetByPropertyId] TO [SmartELockServiceLoginUserRole]
GO