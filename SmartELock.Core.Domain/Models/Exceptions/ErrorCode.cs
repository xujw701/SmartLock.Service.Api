using System.Runtime.Serialization;

namespace SmartELock.Core.Domain.Models.Exceptions
{
    public enum ErrorCode
    {
        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError = 1,

        [EnumMember(Value = "FIELD_MUST_BE_UNIQUE")]
        FieldMustUnique = 2,

        [EnumMember(Value = "MUST_HAS_PERMISSION")]
        MustHasPermission = 3,

        [EnumMember(Value = "FIELD_MUST_EXIST")]
        FieldMustExist = 4,

        [EnumMember(Value = "KEYBOX_MUST_BE_ASSIGNED")]
        KeyboxMustBeAssigned = 5,

        [EnumMember(Value = "KEYBOX_CAN_LIST")]
        KeyboxCanList = 6,

        [EnumMember(Value = "KEYBOX_NOT_LIST")]
        KeyboxNotList = 7
    }
}