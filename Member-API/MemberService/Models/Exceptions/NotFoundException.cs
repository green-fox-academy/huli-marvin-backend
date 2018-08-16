using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MemberService.Models.Exceptions
{
    public class NotFoundException : CustomException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public NotFoundException() : base()
        {
            StatusCode = HttpStatusCode.NotFound;
            ErrorMessage = "We could not find the resource you requested. Please refer to the documentation for the list of resources.";
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            this.ErrorMessage = message;
        }
    }
}
