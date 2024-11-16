using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
                ValidationException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
                BadRequestException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
                NotFoundException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status404NotFound),
                _ => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
            };

            var problem = new ProblemDetails { 
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            problem.Extensions.Add("traceId",httpContext.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problem.Extensions.Add("validationError", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problem,cancellationToken);

            return true;
        }
    }
}
