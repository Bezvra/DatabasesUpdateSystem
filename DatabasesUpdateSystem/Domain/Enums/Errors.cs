using DatabasesUpdateSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Domain.Enums
{
    public enum Errors
    {
        //[LocalizedDescription("UserNotAuthorized", NameResourceType = typeof(ErrorsRes))]
        UserNotAuthorized = 0,
        //[LocalizedDescription("UserNotExist", NameResourceType = typeof(ErrorsRes))]
        UserNotExist = 1,
        //[LocalizedDescription("UserNotRegister", NameResourceType = typeof(ErrorsRes))]
        UserNotRegister = 2,
        //[LocalizedDescription("NotConfirm", NameResourceType = typeof(ErrorsRes))]
        NotConfirm = 3,
        //[LocalizedDescription("UserExist", NameResourceType = typeof(ErrorsRes))]
        UserExist = 4,
        //[LocalizedDescription("EmptyData", NameResourceType = typeof(ErrorsRes))]
        EmptyData = 5,
        //[LocalizedDescription("IncorrectEmailOrPassword", NameResourceType = typeof(ErrorsRes))]
        IncorrectEmailOrPassword = 6,
        //[LocalizedDescription("PasswordNotMatch", NameResourceType = typeof(ErrorsRes))]
        PasswordNotMatch = 7,
        //[LocalizedDescription("DataNotFound", NameResourceType = typeof(ErrorsRes))]
        DataNotFound = 8,
        //[LocalizedDescription("InternalServerError", NameResourceType = typeof(ErrorsRes))]
        InternalServerError = 9,
        //[LocalizedDescription("AccessIsDenied", NameResourceType = typeof(ErrorsRes))]
        AccessIsDenied = 10,
        //[LocalizedDescription("Redirect", NameResourceType = typeof(ErrorsRes))]
        Redirect = 11,
        //[LocalizedDescription("ServerIgnor", NameResourceType = typeof(ErrorsRes))]
        ServerIgnor = 12,
        //[LocalizedDescription("UploudFilesError", NameResourceType = typeof(ErrorsRes))]
        UploudFilesError = 13,
        //[LocalizedDescription("InvalidToken", NameResourceType = typeof(ErrorsRes))]
        InvalidToken = 14,
        //[LocalizedDescription("UserBlocked", NameResourceType = typeof(ErrorsRes))]
        UserBlocked = 15,
        //[LocalizedDescription("DataExist", NameResourceType = typeof(ErrorsRes))]
        DataExist = 16,
        //[LocalizedDescription("SomethingWentWrong", NameResourceType = typeof(ErrorsRes))]
        SomethingWentWrong = 17,
        //[LocalizedDescription("SortFieldDoesntExist", NameResourceType = typeof(ErrorsRes))]
        SortFieldDoesntExist = 18,
        //[LocalizedDescription("UpdateDataError", NameResourceType = typeof(ErrorsRes))]
        UpdateDataError = 19
    }
}