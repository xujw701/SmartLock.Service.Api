﻿using System;

namespace SmartELock.Core.Domain.Models.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException() { }

		public EntityNotFoundException(string message)
			: base(message)
		{
		}
	}
}