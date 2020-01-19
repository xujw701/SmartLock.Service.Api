using System.Threading.Tasks;
using SmartELock.Core.Domain.Models.Exceptions;

namespace SmartELock.Core.Domain.Validators
{
	public interface ISpecification<in T>
	{
		Task<bool> IsSatisfiedByAsync(T obj);

		string ErrorMessage(T obj);

		ErrorCode ErrorCode { get; }
	}
}