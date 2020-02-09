﻿using System.Runtime.Serialization;

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
        FieldMustExist = 4
    }
}