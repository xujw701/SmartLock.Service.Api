CREATE PROCEDURE [dbo].[SuperAdmin_Create]
    @username NVARCHAR(255),
    @password NVARCHAR(255)
AS
    INSERT INTO [dbo].[SuperAdmin] (UserName, Password)
    VALUES (@username, @password);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[SuperAdmin_Create] TO [SmartELockServiceLoginUserRole]
GO