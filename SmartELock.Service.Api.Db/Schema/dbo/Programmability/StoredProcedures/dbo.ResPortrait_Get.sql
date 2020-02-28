CREATE PROCEDURE [dbo].[ResPortrait_Get]
    @resPortraitId int
AS
    SELECT * FROM [dbo].[ResPortrait]
    WHERE ResPortraitId = @resPortraitId

RETURN
GO

GRANT EXECUTE ON [dbo].[ResPortrait_Get] TO [SmartELockServiceLoginUserRole]
GO