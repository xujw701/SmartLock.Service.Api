using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxUuidMustBeUnique : ISpecification<IKeyboxCreateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;

        public KeyboxUuidMustBeUnique(IKeyboxRepository keyboxRepository)
        {
            _keyboxRepository = keyboxRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxCreateCommand command)
        {
            var keybox = await _keyboxRepository.GetKeyboxByUuid(command.Uuid);

            var allow = keybox == null;

            return allow;
        }

        public string ErrorMessage(IKeyboxCreateCommand obj)
        {
            return "Uuid must be unique";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
