using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxCanList : ISpecification<IKeyboxPropertyCreateUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IPropertyRepository _propertyRepository;

        public KeyboxCanList(IKeyboxRepository keyboxRepository, IPropertyRepository propertyRepository)
        {
            _keyboxRepository = keyboxRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxPropertyCreateUpdateCommand command)
        {
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            if (keybox.PropertyId.HasValue)
            {
                var property = await _propertyRepository.GetProperty(keybox.PropertyId.Value);

                var allow = property.EndedOn != DateTime.MinValue;
            }

            return true;
        }

        public string ErrorMessage(IKeyboxPropertyCreateUpdateCommand obj)
        {
            return "Keybox cannot be listed";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.KeyboxCanList;
    }
}
