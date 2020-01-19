using SmartELock.Core.Domain.Models.Exceptions;

namespace SmartELock.Core.Domain.Validators
{
	public class ValidationResult
	{
		public ValidationResult()
		{
			IsValid = false;
		}

		public bool IsValid { get; set; }
		public string ErrorMessage { get; set; }
		public ErrorCode ErrorCode { get; set; }
	}
}