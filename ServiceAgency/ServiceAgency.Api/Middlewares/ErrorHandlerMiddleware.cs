using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceAgency.Domain.Exceptions;
using ServiceAgency.Infrastructure.Repo;

namespace ServiceAgency.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ErrorResponse()
                {
                    Errors = new()
                };

                switch (error)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        if (e.Message != null)
                            responseModel.Errors.Add(e.Message);
                        break;
                    case ConcurrencyException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        if (e.Message != null)
                            responseModel.Errors.Add(e.Message);
                        break;
                    case DateTimeParseException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        if (e.Message != null)
                            responseModel.Errors.Add(e.Message);
                        break;
                    case PrivateNumberException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        if (e.Message != null)
                            responseModel.Errors.Add(e.Message);
                        break;
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors.Add(e.ValidationResult.ErrorMessage);
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                logger.LogError(error.Message);
                await response.WriteAsync(JsonSerializer.Serialize(responseModel));
            }

        }

        internal class ErrorResponse
        {
            public List<string> Errors { get; set; }
        }
    }
}