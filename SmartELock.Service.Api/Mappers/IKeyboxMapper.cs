using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IKeyboxMapper
    {
        KeyboxCreateCommand MapToCreateCommand(KeyboxPostDto keyboxPostDto);
        KeyboxCommand MapToGetCommand(int keyboxId, string uuid);
        KeyboxAssignToCommand MapToAssignToCommand(int keyboxId, int userId);
        KeyboxPropertyCreateCommand MapToKeyboxPropertyCreateCommand(int keyboxId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto);
        KeyboxPropertyUpdateCommand MapToKeyboxPropertyUpdateCommand(int keyboxId, int propertyId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto);
        KeyboxPropertyDeleteCommand MapToKeyboxPropertyDeleteCommand(int keyboxId, int propertyId);
        KeyboxPropertyGetCommand MapToGetCommand(int keyboxId, int propertyId);
        KeyboxHistoryCommand MapToKeyboxHistoryCommand(int keyboxId, KeyboxHistoryPostDto keyboxHistoryPostDto);
    }
}
