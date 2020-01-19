using System.Collections.Generic;
using System.Threading.Tasks;
using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Validators;

namespace SmartELock.Core.Services.Validators
{
	public abstract class BaseCommandValidator<T> : ICommandValidator<T> where T : class, ICommand
	{
		protected abstract IList<ISpecification<T>> GetSpecifications(T command = null);

		public async Task<ValidationResult> Validate(T command)
		{
			var specs = GetSpecifications(command);
			var result = new ValidationResult();
			
			foreach (var spec in specs)
			{
				var isSatisfied = await spec.IsSatisfiedByAsync(command);
				if (!isSatisfied)
				{
					result.ErrorMessage = spec.ErrorMessage(command);
					result.ErrorCode = spec.ErrorCode;
					return result;
				}
			}

			result.IsValid = true;
			return result;
		}
	}
}
