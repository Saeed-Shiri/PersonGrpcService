using Grpc.Core.Interceptors;
using Grpc.Core;

namespace PersonSrv.API.Interceptors;

public class GlobalErrorInterceptor : Interceptor
{
    private readonly ILogger<GlobalErrorInterceptor> _logger;

    public GlobalErrorInterceptor(ILogger<GlobalErrorInterceptor> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (RpcException ex)
        {
            _logger.LogWarning(ex, "gRPC error occurred: {StatusCode}, {Message}", ex.StatusCode, ex.Status.Detail);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred in gRPC call");
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."));
        }
    }
}
