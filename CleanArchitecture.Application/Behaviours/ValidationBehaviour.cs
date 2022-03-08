using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }
        //Captura el elemento request que llegua desde el cliente y realiza validaciones
        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validator.Any())
            { 
                var context = new ValidationContext<TRequest>(request);

                //EVALUACION DE CADA VALIDACION QUE HAY EN LA APLICACION POR EJEMPLO
                //CreateStreamerCommandValidator
                //Busca todas las validaciones y las ejecuta, pero lo hace en el pipeline no al final
                var validationsResult = await Task.WhenAll(_validator.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationsResult.SelectMany(v => v.Errors).Where(x => x != null).ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }

            }

            return await next();
        }
    }
}
