using DatabasesUpdateSystem.Domain.Enums;
using DatabasesUpdateSystem.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var cryptoEx = exception is ServerException ? (ServerException)exception : new ServerException(Errors.InternalServerError);
            context.Response.StatusCode = (int)cryptoEx.Result.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = cryptoEx.Result.Content }));
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var cryptoEx = ex as ServerException;
                if (cryptoEx == null)
                {
                    var message = ex.Message;
                    var stackTrace = ex.StackTrace;
                    var method = context.Request.Method;
                    var url = context.Request.Path;
                    var log = $"{method} | {url} | {message}" +
                              $"\r\n{stackTrace}";
                    logger.LogError(default(EventId), ex, log);
                }

                await HandleExceptionAsync(context, ex);
            }
        }
    }
}