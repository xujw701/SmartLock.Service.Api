CREATE PROCEDURE [dbo].[Property_Update]
    @propertyId INT,
    @companyId INT,
    @branchId INT,
    @propertyName NVARCHAR(2048),
    @address NVARCHAR(MAX),
    @notes NVARCHAR(MAX),
    @price NVARCHAR(255),
    @bedrooms FLOAT,
	@bathrooms FLOAT,
	@floorArea FLOAT,
	@landArea FLOAT
AS

    UPDATE [dbo].[Property]
    SET CompanyId = @companyId,
        BranchId = @branchId,
        PropertyName = @propertyName,
        Address = @address,
        Notes = @notes,
        Price = @price,
        Bedrooms = @bedrooms,
        Bathrooms = @bathrooms,
        FloorArea = @floorArea,
        UpdatedOn = SYSUTCDATETIME()
    WHERE PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[Property_Update] TO [SmartELockServiceLoginUserRole]
GO