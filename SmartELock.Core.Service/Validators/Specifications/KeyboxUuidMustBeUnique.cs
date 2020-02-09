using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxUuidMustBeUnique : ISpecification<IKeyboxCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;

        public KeyboxUuidMustBeUnique(IKeyboxRepository keyboxRepository)
        {
            _keyboxRepository = keyboxRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxCommand command)
        {
            var keybox = await _keyboxRepository.GetKeyboxByUuid(command.Uuid);

            var allow = keybox == null;

            return allow;
        }

        public string ErrorMessage(IKeyboxCommand obj)
        {
            return "Uuid must be unique";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
