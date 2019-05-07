using DatabasesUpdateSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Infrastructure
{
    public class ServerException : Exception
    {
        public ContentResult Result;
        private string errorMessage { get; set; }
        private HttpStatusCode statusCode { get; set; }

        public ServerException() : base() { }

        public ServerException(Errors messege)
        {
            switch (messege)
            {
                case Errors.UserNotAuthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotAuthorized.GetEnumDescription();
                    break;
                case Errors.UserNotExist:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotExist.GetEnumDescription();
                    break;
                case Errors.UserNotRegister:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotRegister.GetEnumDescription();
                    break;
                case Errors.NotConfirm:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.NotConfirm.GetEnumDescription();
                    break;
                case Errors.UserExist:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.UserExist.GetEnumDescription();
                    break;
                case Errors.EmptyData:
                    statusCode = HttpStatusCode.NoContent;
                    errorMessage = Errors.EmptyData.GetEnumDescription();
                    break;
                case Errors.IncorrectEmailOrPassword:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.IncorrectEmailOrPassword.GetEnumDescription();
                    break;
                case Errors.PasswordNotMatch:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.PasswordNotMatch.GetEnumDescription();
                    break;
                case Errors.DataNotFound:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = Errors.DataNotFound.GetEnumDescription();
                    break;
                case Errors.InternalServerError:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = Errors.InternalServerError.GetEnumDescription();
                    break;
                case Errors.AccessIsDenied:
                    statusCode = HttpStatusCode.Forbidden;
                    errorMessage = Errors.AccessIsDenied.GetEnumDescription();
                    break;
                case Errors.Redirect:
                    statusCode = HttpStatusCode.Redirect;
                    errorMessage = Errors.Redirect.GetEnumDescription();
                    break;
                case Errors.ServerIgnor:
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    errorMessage = Errors.ServerIgnor.GetEnumDescription();
                    break;
                case Errors.UploudFilesError:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.UploudFilesError.GetEnumDescription();
                    break;
                case Errors.InvalidToken:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.InvalidToken.GetEnumDescription();
                    break;
                case Errors.UserBlocked:
                    statusCode = HttpStatusCode.Forbidden;
                    errorMessage = Errors.UserBlocked.GetEnumDescription();
                    break;
                case Errors.DataExist:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.DataExist.GetEnumDescription();
                    break;
                default:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.SomethingWentWrong.GetEnumDescription();
                    break;
            }

            Result = new ContentResult
            {
                StatusCode = (int)statusCode,
                Content = errorMessage
            };
        }
    }
}