﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using TempArAn.Domain.Exceptions.ApplicationExceptions;

namespace TempArAn.Domain.Exceptions.ExceptionHandlerMiddleware
{
    public class ApplicationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApplicationExceptionHandlerMiddleware> _logger;

        public ApplicationExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ApplicationExceptionHandlerMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccessDeniedException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.Forbidden);
            }
            catch (NotFoundException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.BadRequest);
            }
            catch (WrongSourceDetailsException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await HandleExceptionAsInternalServerErrorAsync(context, e);
            }
        }

        private Task HandleExceptionAsInternalServerErrorAsync<TException>(HttpContext context, TException e)
            where TException : Exception
        {
            context.Response.ContentType = "text";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errorDetailsSeparator = new string('-', 50);
            var fullErrorMessage = $"Internal server error\n{errorDetailsSeparator}\nMessage: {e.Message}\n{errorDetailsSeparator}\nStack trace: {e.StackTrace}\n{errorDetailsSeparator}";
            _logger.LogCritical(fullErrorMessage);
            return context.Response.WriteAsync(fullErrorMessage, Encoding.UTF8);
        }

        private static Task HandleExceptionAsync<TException>(HttpContext context, TException e, HttpStatusCode statusCode)
            where TException : Exception
        {
            context.Response.ContentType = "text";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(e.Message, Encoding.UTF8);
        }
    }
}
