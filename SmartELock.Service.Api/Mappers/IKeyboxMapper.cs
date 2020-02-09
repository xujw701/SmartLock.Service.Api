using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IKeyboxMapper
    {
        KeyboxCreateCommand MapToCreateCommand(KeyboxPostDto keyboxPostDto);
        PropertyCreateCommand MapToCreateCommand(PropertyPostDto propertyPostDto);
    }
}
