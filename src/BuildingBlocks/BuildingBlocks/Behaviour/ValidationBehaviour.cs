using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviour
{
    public class ValidationBehaviour<TReq, TRes>(IEnumerable<IValidator<TReq>> validators) : IPipelineBehavior<TReq, TRes>
        where TReq : ICommand<TRes>
    {
        public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TReq>(request);

            var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failure = validationResult.Where(X=>X.Errors.Any()).SelectMany(X=>X.Errors).ToList();

            if (failure.Any())
                throw new ValidationException(failure);

            return await next();
        }
    }
}
