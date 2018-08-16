using MemberService.Extensions;
using MemberService.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MemberService.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code = (int)HttpStatusCode.InternalServerError;
            string errorMessage = "Unexpected internal server error.";

            var myCustomException = exception as CustomException;

            if (myCustomException != null)
            {
                code = (int)Search.GetThePropertyValue(exception, "StatusCode");
                errorMessage = Search.GetThePropertyValue(exception, "ErrorMessage").ToString();
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
