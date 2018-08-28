using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MemberService.Models.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public UnauthorizedException() : base()
        {
            StatusCode = HttpStatusCode.Unauthorized;
            ErrorMessage = "You are unauthorized to access the requested resource.";
        }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = HttpStatusCode.Unauthorized;
            this.ErrorMessage = message;
        }
    }
}
