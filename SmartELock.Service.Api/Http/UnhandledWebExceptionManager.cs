using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace SmartELock.Service.Api.Http
{
	public static class UnhandledWebExceptionManager
	{
		/// <summary>
		/// The exception handlers dictionary
		/// </summary>
		private static readonly IDictionary<Type, IWebExceptionHandler> Handlers = new Dictionary<Type, IWebExceptionHandler>();

		/// <summary>
		/// Adds an exception handler to the dictionary.
		/// </summary>
		/// <typeparam name="TException">The type of the exception.</typeparam>
		/// <param name="exceptionHandler">The exception handler.</param>
		public static void AddHandler<TException>(WebExceptionHandler<TException> exceptionHandler)
			where TException : Exception
		{
			if (exceptionHandler == null)
			{
				throw new ArgumentException(nameof(exceptionHandler));
			}

			var exceptionType = typeof(TException);

			if (!Handlers.ContainsKey(exceptionType))
			{
				Handlers.Add(exceptionType, exceptionHandler);
			}
			else
			{
				throw new InvalidOperationException(string.Format("The exception handler of type [{0}] cannot be added twice", exceptionType));
			}
		}

		/// <summary>
		/// Removes all custom error handlers. Used for unit tests.
		/// </summary>
		public static void RemoveAllHandlers()
		{
			Handlers.Clear();
		}

		/// <summary>
		/// Handles an unhandled exception by checking against all registered exception handlers.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception instance.</param>
		/// <returns>true if the exception was handled; otherwise false</returns>
		public static bool HandleException(HttpActionExecutedContext actionExecutedContext, Exception exception)
		{
			foreach (var webExceptionHandler in Handlers)
			{
				if (exception.GetType() == webExceptionHandler.Key)
				{
					webExceptionHandler.Value.HandleException(actionExecutedContext, exception);
					return true;
				}
			}

			return false;
		}
	}
}