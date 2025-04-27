

using FluentValidation;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonSrv.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed for request: {RequestType}", typeof(TRequest).Name);
            throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join("; ", ex.Errors.Select(e => e.ErrorMessage))));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Resource not found for request: {RequestType}", typeof(TRequest).Name);
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred for request: {RequestType}", typeof(TRequest).Name);
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."));
        }
    }
}
