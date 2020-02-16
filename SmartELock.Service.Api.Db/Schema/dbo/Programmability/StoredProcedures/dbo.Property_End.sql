CREATE PROCEDURE [dbo].[Property_End]
    @propertyId INT
AS

    UPDATE [dbo].[Property]
    SET EndedOn = SYSUTCDATETIME(),
        UpdatedOn = SYSUTCDATETIME()
    WHERE PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[Property_End] TO [SmartELockServiceLoginUserRole]
GO