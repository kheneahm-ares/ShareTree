using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            //let request go through and have global catch on all exceptions
            try
            {
                await _next (context);
            }
            catch (Exception exc)
            {
                await HandleExceptionAsync (context, exc);
            }

        }

        private async Task HandleExceptionAsync (HttpContext context, Exception exc)
        {
            Object errors = null;

            //this is basically doing if-else using the "is" statement on the exc object
            switch (exc)
            {
                case RestException re:
                    errors = re.Errors;
                    context.Response.StatusCode = (int) re.StatusCode;
                    break;
                case Exception e:
                    errors = string.IsNullOrEmpty (e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonSerializer.Serialize (new { Errors = errors });

                //return response
                await context.Response.WriteAsync (result);
            }
        }
    }
}