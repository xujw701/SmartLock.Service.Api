using System.Threading.Tasks;
using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Validators;

namespace SmartELock.Core.Services.Validators
{
	public interface ICommandValidator<T> where T : ICommand
	{
		Task<ValidationResult> Validate(T command);
	}
}