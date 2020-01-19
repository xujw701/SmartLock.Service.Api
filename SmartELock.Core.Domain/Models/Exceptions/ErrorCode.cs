using System.Runtime.Serialization;

namespace SmartELock.Core.Domain.Models.Exceptions
{
    public enum ErrorCode
    {
        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError = 1,

        [EnumMember(Value = "DUPLICATE_COMPANY_NAME")]
        DuplicateCompanyName = 2,

        [EnumMember(Value = "DUPLICATE_TRAVELLER_EMAIL")]
        DuplicateTravellerEmail = 3,

        [EnumMember(Value = "DUPLICATE_TRAVELLER_MOBILE_NUMBER")]
        DuplicateTravellerMobileNumber = 4,

        [EnumMember(Value = "DUPLICATE_USER_EMAIL")]
        DuplicateUserEmail = 5
    }
}