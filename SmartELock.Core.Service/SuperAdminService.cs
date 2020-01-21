using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Domain.Service;
using SmartELock.Core.Services.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        private readonly SuperAdminCreateValidator _superAdminCreateValidator;

        public SuperAdminService(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;

            _superAdminCreateValidator = new SuperAdminCreateValidator(_superAdminRepository);
        }

        public async Task<int> CreateSuperAdmin(SuperAdminCreateCommand command)
        {
            var validationResult = await _superAdminCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var superAdmin = SuperAdmin.CreateFrom(command);

            return await _superAdminRepository.CreateSuperAdmin(superAdmin);
        }

        public async Task<SuperAdmin> Login(SuperAdminLoginCommand command)
        {
            var superAdminId = await Auth(command);

            if (superAdminId > 0)
            {
                var success = await IssueToken(superAdminId);

                if (!success) return null;

                var superAdmin = await _superAdminRepository.GetSuperAdmin(superAdminId);

                return superAdmin;
            }

            return null;
        }

        public async Task<bool> CheckToken(int superAdminId, string token)
        {
            var superAdmin = await _superAdminRepository.GetSuperAdmin(superAdminId);

            if (superAdmin == null || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(superAdmin.Token)) return false;

            return true;
        }

        private async Task<int> Auth(SuperAdminLoginCommand command)
        {
            var superAdmin = await _superAdminRepository.GetSuperAdmin(command.Username);

            if (superAdmin == null || string.IsNullOrEmpty(command.Password) || string.IsNullOrEmpty(command.Password)) return 0;

            if (command.Password.Equals(superAdmin.Password))
            {
                return superAdmin.SuperAdminId;
            }

            return 0;
        }

        private async Task<bool> IssueToken(int superAdminId)
        {
            // Generate a token based on Now
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            // Get the timestamp of a token
            //var data = Convert.FromBase64String(token);
            //var when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            return await _superAdminRepository.UpdateToken(superAdminId, token);
        }
    }
}
