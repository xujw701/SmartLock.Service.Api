CREATE PROCEDURE [dbo].[PropertyFeedback_Get]
    @propertyId INT
AS

    SELECT [dbo].[PropertyFeedback].PropertyFeedbackId,
           [dbo].[PropertyFeedback].PropertyId,
           [dbo].[PropertyFeedback].UserId,
           [dbo].[User].FirstName,
           [dbo].[User].LastName,
           [dbo].[PropertyFeedback].Content,
           [dbo].[PropertyFeedback].CreatedOn,
           [dbo].[PropertyFeedback].UpdatedOn
    FROM [dbo].[PropertyFeedback] INNER JOIN [dbo].[User]
    ON [dbo].[PropertyFeedback].UserId = [dbo].[User].UserId
    WHERE [dbo].[PropertyFeedback].PropertyId = @propertyId

RETURN
GO

GRANT EXECUTE ON [dbo].[PropertyFeedback_Get] TO [SmartELockServiceLoginUserRole]
GO