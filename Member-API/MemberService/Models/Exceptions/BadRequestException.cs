using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MemberService.Models.Exceptions
{
    public class BadRequestException : CustomException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public BadRequestException() : base()
        {
            StatusCode = HttpStatusCode.BadRequest;
            ErrorMessage = "Invalid syntax for this request was provided.";
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = HttpStatusCode.BadRequest;
            ErrorMessage = message;
        }
    }
}
