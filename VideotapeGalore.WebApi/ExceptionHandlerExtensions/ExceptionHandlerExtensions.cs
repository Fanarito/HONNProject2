using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VideotapeGalore.Services.Exceptions;

namespace VideotapeGalore.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    var exceptionType = exception.Error.GetType();

                    if (exceptionType == typeof(NotFoundException))
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;

                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new
                        {
                            errorMessage = exception.Error.Message
                        })).ConfigureAwait(false);
                });
            });
        }
    }
}