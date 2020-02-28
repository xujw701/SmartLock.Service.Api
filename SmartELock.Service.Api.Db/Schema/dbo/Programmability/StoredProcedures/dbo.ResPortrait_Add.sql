CREATE PROCEDURE [dbo].[ResPortrait_Add]
    @url nvarchar(1024)
AS
    INSERT INTO [dbo].[ResPortrait] ([Url])
    VALUES (@url);

    -- Select ID
    SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN
GO

GRANT EXECUTE ON [dbo].[ResPortrait_Add] TO [SmartELockServiceLoginUserRole]
GO