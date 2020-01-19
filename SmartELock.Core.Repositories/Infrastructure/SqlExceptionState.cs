namespace SmartELock.Core.Repositories.Infrastructure
{
	internal enum SqlExceptionState
	{
		/// <summary>
		///     404. The entity not found.
		/// </summary>
		EntityNotFound = 1,

		/// <summary>
		///     400. The data validation.
		/// </summary>
		DataValidation = 2,

		/// <summary>
		///     409. The entity conflict.
		/// </summary>
		EntityConflict = 3
	}
}