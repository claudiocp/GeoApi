using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace GeoApi.Models
{
    public class GeoApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GeoApiExceptionFilter> _logger;

        public GeoApiExceptionFilter(ILogger<GeoApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is GeoApiException)
            {
                _logger.LogInformation(context.Exception, "GeoApi Error: {Message}", context.Exception.Message);
                
                var result = new ObjectResult(new
                {
                    error = context.Exception.Message
                })
                {
                    StatusCode = 400 // Bad Request
                };

                context.Result = result;
                context.ExceptionHandled = true;
            }
            else
            {
                _logger.LogError(context.Exception, "Erro não tratado: {Message}", context.Exception.Message);
                
                var result = new ObjectResult(new
                {
                    error = "Ocorreu um erro no processamento da solicitação."
                })
                {
                    StatusCode = 500 // Internal Server Error
                };

                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
} 