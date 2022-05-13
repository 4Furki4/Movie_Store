using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MovieStore.Services;
using Newtonsoft.Json;

namespace MovieStore.MiddleWares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILoggerService _LoggerService;

        public CustomExceptionMiddleWare(RequestDelegate next, ILoggerService loggerService)
        {
            this.next = next;
            _LoggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
                var watch = Stopwatch.StartNew();
            try
            {
                var message= $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
                _LoggerService.Write(message);
                await next(context); // await Invoke.next(context) ile aynı.
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms";
                _LoggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await ExceptionHandle(context, ex, watch);
            }

        }

        private Task ExceptionHandle(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType= "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; //Herhalde HTTPStatusCode.InternalServerError ile aynı.
            string message= $"[ERROR] -    HTTP {context.Request.Method} - {context.Response.StatusCode} - Error Message : {ex.Message} responded in {watch.Elapsed.TotalMilliseconds} ms";
            _LoggerService.Write(message);
            var result = JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExeptionMiddleWareExtension
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}