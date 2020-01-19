using System;

namespace SmartELock.Core.Domain.Models.Exceptions
{
	public class DataValidationException : Exception
	{
		public DataValidationException() { }

		public DataValidationException(string message)
			: base(message)
		{
		}
	}
}