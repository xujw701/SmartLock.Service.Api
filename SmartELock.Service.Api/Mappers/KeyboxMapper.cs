using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class KeyboxMapper : IKeyboxMapper
    {
        public KeyboxCreateCommand MapToCreateCommand(KeyboxPostDto keyboxPostDto)
        {
            return new KeyboxCreateCommand
            {
                CompanyId = keyboxPostDto.CompanyId,
                BranchId = keyboxPostDto.BranchId,
                Uuid = keyboxPostDto.Uuid,
                KeyboxName = keyboxPostDto.KeyboxName,
                BatteryLevel = keyboxPostDto.BatteryLevel,
                Pin = keyboxPostDto.Pin
            };
        }

        public PropertyCreateCommand MapToCreateCommand(PropertyPostDto propertyPostDto)
        {
            return new PropertyCreateCommand
            {
                PropertyName = propertyPostDto.PropertyName,
                Address = propertyPostDto.Address,
                Price = propertyPostDto.Price,
                Bedrooms = propertyPostDto.Bedrooms,
                Bathrooms = propertyPostDto.Bathrooms,
                FloorArea = propertyPostDto.FloorArea,
                LandArea = propertyPostDto.LandArea,
            };
        }
    }
}