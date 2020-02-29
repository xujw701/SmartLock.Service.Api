CREATE PROCEDURE [dbo].[ResProperty_Add]
    @propertyId int,
    @url nvarchar(1024)
AS
    INSERT INTO [dbo].[ResProperty] (PropertyId, [Url])
    VALUES (@propertyId, @url);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[ResProperty_Add] TO [SmartELockServiceLoginUserRole]
GO