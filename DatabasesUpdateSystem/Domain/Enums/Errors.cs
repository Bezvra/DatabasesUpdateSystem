using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Domain.Enums
{
    public enum Errors
    {
        [Description("USER_NOT_AUTHORIZED")]
        UserNotAuthorized = 0,
        [Description("USER_NOT_EXIST")]
        UserNotExist = 1,
        [Description("USER_NOT_REGISTER")]
        UserNotRegister = 2,
        [Description("NOT_CONFIRM")]
        NotConfirm = 3,
        [Description("USER_EXIST")]
        UserExist = 4,
        [Description("EMPTY_DATA")]
        EmptyData = 5,
        [Description("INCORRECT_EMAIL_OR_PASSWORD")]
        IncorrectEmailOrPassword = 6,
        [Description("PASSWORD_NOT_MATCH")]
        PasswordNotMatch = 7,
        [Description("DATA_NOT_FOUND")]
        DataNotFound = 8,
        [Description("INTERNAL_SERVER_ERROR")]
        InternalServerError = 9,
        [Description("ACCESS_IS_DENIED")]
        AccessIsDenied = 10,
        [Description("REDIRECT")]
        Redirect = 11,
        [Description("SERVER_IGNOR")]
        ServerIgnor = 12,
        [Description("UPLOAD_FILES_ERROR")]
        UploudFilesError = 13,
        [Description("INVALID_TOKEN")]
        InvalidToken = 14,
        [Description("USER_BLOCKED")]
        UserBlocked = 15,
        [Description("DATA_EXIST")]
        DataExist = 16,
        [Description("SOMETHING_WENT_WRONG")]
        SomethingWentWrong = 17
    }
}