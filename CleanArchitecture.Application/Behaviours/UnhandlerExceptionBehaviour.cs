using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Behaviours
{
    public class UnhandlerExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<UnhandlerExceptionBehaviour<TRequest,TResponse>> _logger;


        public UnhandlerExceptionBehaviour(ILogger<UnhandlerExceptionBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;                
        }

        public  async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try 
            {
                return await next();
            } 
            catch (Exception ex) 
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(
                    ex, 
                    "Application Request : sucedio una excepción para el request {Name}, {@Request}", requestName, request
                    );
                throw;
            }
          

            
        }
    }
}
