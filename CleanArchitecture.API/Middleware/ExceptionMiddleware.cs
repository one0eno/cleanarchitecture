using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //PASAMOS AL SIGUIENTE NIVEL EN EL PIPELINE SI HAY ERROR PASA AL CACTH
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notfoundException:
                        statusCode = (int) HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                        break;

                    case  BadRequestException badRequestException:
                        statusCode =(int) HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result))
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));


                //var response = _environment.IsDevelopment()
                //    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                //    : new CodeErrorException((int)HttpStatusCode.InternalServerError);

                //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                //var json = JsonSerializer.Serialize(response, options);
                context.Response.StatusCode = statusCode;

                
                await context.Response.WriteAsync(result);
            }
        }
    }
}
