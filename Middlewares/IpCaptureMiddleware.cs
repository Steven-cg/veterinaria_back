using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace YourNamespace.Middlewares
{
    public class IpCaptureMiddleware
    {
        private readonly RequestDelegate _next;

        public IpCaptureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            context.Items["ClientIp"] = ipAddress;

            await _next(context);
        }
    }
}
