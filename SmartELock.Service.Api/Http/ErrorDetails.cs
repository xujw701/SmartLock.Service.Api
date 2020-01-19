using System;
using SmartELock.Core.Domain.Models.Exceptions;

namespace SmartELock.Service.Api.Http
{
	public class ErrorDetails
	{
		public string ErrorMessage { get; set; }
		public ErrorCode? ErrorCode { get; set; }
		public string ExceptionType { get; set; }
		public string StackTrace { get; set; }

		public ErrorDetails(Exception exception, ErrorCode? errorCode, string errorMessage)
		{
			ErrorMessage = errorMessage;
			ErrorCode = errorCode;
			ExceptionType = exception.GetType().ToString();
			StackTrace = exception.StackTrace;
		}
	}
}