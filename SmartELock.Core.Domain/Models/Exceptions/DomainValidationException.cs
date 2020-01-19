using System;

namespace SmartELock.Core.Domain.Models.Exceptions
{
	public class DomainValidationException : Exception
	{
		public ErrorCode Code { get; set; }

		public DomainValidationException(Guid guid, string message) : base($"{guid} - {message}")
		{
			Code = ErrorCode.UnknownError;
		}

		public DomainValidationException(string message, ErrorCode? code = null) : base(message)
		{
			Code = code ?? ErrorCode.UnknownError;
		}
	}
}