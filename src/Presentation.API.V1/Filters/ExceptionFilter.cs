using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.V1.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var (statusCode, message) = MapExceptionToResponse(context.Exception);

        var response = new 
        {
            Message = message,
            StatusCode = (int)statusCode,
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }

    private (HttpStatusCode statusCode, string message) MapExceptionToResponse(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (HttpStatusCode.BadRequest, "Отсутствует обязательный параметр"),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            InvalidOperationException => (HttpStatusCode.Conflict, exception.Message),
            
            EntityNotFoundException => (HttpStatusCode.NotFound, exception.Message),
            DuplicateEntityException => (HttpStatusCode.Conflict, exception.Message),
            
            _ => (HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера")
        };
    }
}