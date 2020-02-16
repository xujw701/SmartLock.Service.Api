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

        public KeyboxAssignToCommand MapToAssignToCommand(int keyboxId, int userId)
        {
            return new KeyboxAssignToCommand
            {
                KeyboxId = keyboxId,
                TargetUserId = userId
            };
        }

        public KeyboxPropertyCreateCommand MapToKeyboxPropertyCreateCommand(int keyboxId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto)
        {
            return new KeyboxPropertyCreateCommand
            {
                KeyboxId = keyboxId,
                KeyboxName = keyboxPropertyPostPutDto.KeyboxName,
                CompanyId = keyboxPropertyPostPutDto.CompanyId,
                BranchId = keyboxPropertyPostPutDto.BranchId,
                PropertyName = keyboxPropertyPostPutDto.PropertyName,
                Address = keyboxPropertyPostPutDto.Address,
                Notes = keyboxPropertyPostPutDto.Notes,
                Price = keyboxPropertyPostPutDto.Price,
                Bedrooms = keyboxPropertyPostPutDto.Bedrooms,
                Bathrooms = keyboxPropertyPostPutDto.Bathrooms,
                FloorArea = keyboxPropertyPostPutDto.FloorArea,
                LandArea = keyboxPropertyPostPutDto.LandArea,
            };
        }

        public KeyboxPropertyUpdateCommand MapToKeyboxPropertyUpdateCommand(int keyboxId, int propertyId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto)
        {
            return new KeyboxPropertyUpdateCommand
            {
                KeyboxId = keyboxId,
                PropertyId = propertyId,
                KeyboxName = keyboxPropertyPostPutDto.KeyboxName,
                CompanyId = keyboxPropertyPostPutDto.CompanyId,
                BranchId = keyboxPropertyPostPutDto.BranchId,
                PropertyName = keyboxPropertyPostPutDto.PropertyName,
                Address = keyboxPropertyPostPutDto.Address,
                Notes = keyboxPropertyPostPutDto.Notes,
                Price = keyboxPropertyPostPutDto.Price,
                Bedrooms = keyboxPropertyPostPutDto.Bedrooms,
                Bathrooms = keyboxPropertyPostPutDto.Bathrooms,
                FloorArea = keyboxPropertyPostPutDto.FloorArea,
                LandArea = keyboxPropertyPostPutDto.LandArea,
            };
        }

        public KeyboxPropertyDeleteCommand MapToKeyboxPropertyDeleteCommand(int keyboxId, int propertyId)
        {
            return new KeyboxPropertyDeleteCommand
            {
                KeyboxId = keyboxId,
                PropertyId = propertyId
            };
        }
    }
}