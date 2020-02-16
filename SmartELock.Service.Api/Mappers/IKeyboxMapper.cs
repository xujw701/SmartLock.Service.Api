using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IKeyboxMapper
    {
        KeyboxCreateCommand MapToCreateCommand(KeyboxPostDto keyboxPostDto);
        KeyboxAssignToCommand MapToAssignToCommand(int keyboxId, int userId);
        KeyboxPropertyCreateCommand MapToKeyboxPropertyCreateCommand(int keyboxId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto);
        KeyboxPropertyUpdateCommand MapToKeyboxPropertyUpdateCommand(int keyboxId, int propertyId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto);
        KeyboxPropertyDeleteCommand MapToKeyboxPropertyDeleteCommand(int keyboxId, int propertyId);
    }
}
