CREATE PROCEDURE [dbo].[Property_Create]
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

    INSERT INTO [dbo].[Property] (CompanyId, BranchId, PropertyName, Address, Notes, Price, Bedrooms,Bathrooms, FloorArea, LandArea)
    VALUES (@companyId, @branchId, @propertyName, @address, @notes, @price, @bedrooms, @bathrooms, @floorArea, @landArea);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[Property_Create] TO [SmartELockServiceLoginUserRole]
GO