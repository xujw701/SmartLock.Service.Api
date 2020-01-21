using System.Runtime.Serialization;

namespace SmartELock.Core.Domain.Models.Exceptions
{
    public enum ErrorCode
    {
        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError = 1,

        [EnumMember(Value = "SUPERADMIN_USERNAME_MUST_UNIQUE")]
        SuperAdminUsernameMustUnique = 2
    }
}