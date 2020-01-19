using System.Threading.Tasks;
using SmartELock.Core.Domain.Models;

namespace SmartELock.Core.Domain.Validators
{
	public interface ICommandValidator<T> where T : ICommand
	{
		Task<ValidationResult> Validate(T command);
	}
}
