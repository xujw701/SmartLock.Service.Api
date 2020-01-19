using System;

namespace SmartELock.Core.Domain.Models.Exceptions
{
	public class ConflictEntityException : Exception
	{
		public ConflictEntityException() { }

		public ConflictEntityException(string message)
			: base(message)
		{
		}
	}
}
