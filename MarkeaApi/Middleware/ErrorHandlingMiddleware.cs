using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Ocurrió un error interno inesperado.";

        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound; // 404
                message = notFoundException.Message;
                break;
            case ConflictException conflictException:
                statusCode = HttpStatusCode.Conflict; // 409
                message = conflictException.Message;
                break;
            case SqlException sqlException when sqlException.Number == 50000:
                statusCode = HttpStatusCode.BadRequest; // 400
                message = sqlException.Message;
                break;
                // Aquí puedes añadir más tipos de excepciones
        }

        var result = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }
}