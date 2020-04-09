using System;
using System.Net;

namespace Application.Exceptions
{
    public class RestException : Exception
    {
        public RestException (HttpStatusCode statusCode, object errors = null)
        {
            this.StatusCode = statusCode;
            this.Errors = errors;

        }
        public HttpStatusCode StatusCode { get; set; }
        public object Errors { get; set; }

    }
}