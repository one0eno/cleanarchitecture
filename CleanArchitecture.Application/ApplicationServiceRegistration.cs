using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CleanArchitecture.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {

            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            //busca todas las clases del proyecto application que referencian addValidation y paquetes de fluent validation para injectarlas
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandlerExceptionBehaviour<,>));
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                
            return service;
        }
    }
}
