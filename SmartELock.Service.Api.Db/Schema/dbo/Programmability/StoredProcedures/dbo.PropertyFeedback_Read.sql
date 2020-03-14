CREATE PROCEDURE [dbo].[PropertyFeedback_Read]
    @propertyFeedbackId INT
AS

    UPDATE [dbo].[PropertyFeedback]
    SET IsRead = 1,
        UpdatedOn = SYSUTCDATETIME()
    WHERE PropertyFeedbackId = @propertyFeedbackId

RETURN
GO

GRANT EXECUTE ON [dbo].[PropertyFeedback_Read] TO [SmartELockServiceLoginUserRole]
GO