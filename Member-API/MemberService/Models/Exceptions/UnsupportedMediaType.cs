using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MemberService.Models.Exceptions
{
    public class UnsupportedMediaType : CustomException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public UnsupportedMediaType() : base()
        {
            StatusCode = HttpStatusCode.UnsupportedMediaType;
            ErrorMessage = "The requested resource does not support the media type provided.";
        }

        public UnsupportedMediaType(string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = HttpStatusCode.UnsupportedMediaType;
            this.ErrorMessage = message;
        }
    }
}
