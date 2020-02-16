using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxListed : ISpecification<IKeyboxPropertyCreateUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;

        public KeyboxListed(IKeyboxRepository keyboxRepository)
        {
            _keyboxRepository = keyboxRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxPropertyCreateUpdateCommand command)
        {
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            return keybox.PropertyId.HasValue;
        }

        public string ErrorMessage(IKeyboxPropertyCreateUpdateCommand obj)
        {
            return "Keybox is not listed";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.KeyboxNotList;
    }
}
