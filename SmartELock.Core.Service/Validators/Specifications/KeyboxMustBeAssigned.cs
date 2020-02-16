using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxMustBeAssigned : ISpecification<IKeyboxAssignToCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;

        public KeyboxMustBeAssigned(IKeyboxRepository keyboxRepository)
        {
            _keyboxRepository = keyboxRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxAssignToCommand command)
        {
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            var allow = keybox.UserId.HasValue;

            return allow;
        }

        public string ErrorMessage(IKeyboxAssignToCommand obj)
        {
            return "Uuid must be assigned";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.KeyboxMustBeAssigned;
    }
}
