using DatabasesUpdateSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Infrastructure
{
    public class MyException : Exception
    {
        public ContentResult Result;
        private string errorMessage { get; set; }
        private HttpStatusCode statusCode { get; set; }

        public MyException() : base() { }

        public MyException(Errors messege)
        {
            switch (messege)
            {
                case Errors.UserNotAuthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotAuthorized.GetLocalizedDescription();
                    break;
                case Errors.UserNotExist:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotExist.GetLocalizedDescription();
                    break;
                case Errors.UserNotRegister:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.UserNotRegister.GetLocalizedDescription();
                    break;
                case Errors.NotConfirm:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.NotConfirm.GetLocalizedDescription();
                    break;
                case Errors.UserExist:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.UserExist.GetLocalizedDescription();
                    break;
                case Errors.EmptyData:
                    statusCode = HttpStatusCode.NoContent;
                    errorMessage = Errors.EmptyData.GetLocalizedDescription();
                    break;
                case Errors.IncorrectEmailOrPassword:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.IncorrectEmailOrPassword.GetLocalizedDescription();
                    break;
                case Errors.PasswordNotMatch:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.PasswordNotMatch.GetLocalizedDescription();
                    break;
                case Errors.DataNotFound:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = Errors.DataNotFound.GetLocalizedDescription();
                    break;
                case Errors.InternalServerError:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = Errors.InternalServerError.GetLocalizedDescription();
                    break;
                case Errors.AccessIsDenied:
                    statusCode = HttpStatusCode.Forbidden;
                    errorMessage = Errors.AccessIsDenied.GetLocalizedDescription();
                    break;
                case Errors.Redirect:
                    statusCode = HttpStatusCode.Redirect;
                    errorMessage = Errors.Redirect.GetLocalizedDescription();
                    break;
                case Errors.ServerIgnor:
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    errorMessage = Errors.ServerIgnor.GetLocalizedDescription();
                    break;
                case Errors.UploudFilesError:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.UploudFilesError.GetLocalizedDescription();
                    break;
                case Errors.InvalidToken:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = Errors.InvalidToken.GetLocalizedDescription();
                    break;
                case Errors.UserBlocked:
                    statusCode = HttpStatusCode.Forbidden;
                    errorMessage = Errors.UserBlocked.GetLocalizedDescription();
                    break;
                case Errors.DataExist:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.DataExist.GetLocalizedDescription();
                    break;
                case Errors.SortFieldDoesntExist:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = Errors.SortFieldDoesntExist.GetLocalizedDescription();
                    break;
                case Errors.UpdateDataError:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.UpdateDataError.GetLocalizedDescription();
                    break;
                default:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = Errors.SomethingWentWrong.GetLocalizedDescription();
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