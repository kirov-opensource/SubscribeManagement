using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SubscribeManagement.WebAPI.Models;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;

        public ExceptionHandleMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            this.logger = loggerFactory.CreateLogger(GetType().FullName);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case UnauthorizedAccessException _:
                    await ReturnErrorMessage(context, exception, "UNAUTHORIZED_EXCEPTION", HttpStatusCode.Forbidden, exception?.Message);
                    break;
                case Exceptions.UnauthenticatedException _:
                    await ReturnErrorMessage(context, exception, "UNAUTHENTICATED_EXCEPTION", HttpStatusCode.Unauthorized, exception?.Message);
                    break;
                case Exceptions.BusinessException _:
                    await ReturnErrorMessage(context, exception, "BUSINESS_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    break;
                case ArgumentNullException _:
                case ArgumentException _:
                    await ReturnErrorMessage(context, exception, "ARGUMENT_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    break;
                default:
                    await ReturnErrorMessage(context, exception, "SYSTEM_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    break;
            }

        }

        private async Task ReturnErrorMessage(HttpContext context, Exception exception, string exceptionCode, HttpStatusCode httpStatusCode, string message)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            var text = string.Empty;
            if (exceptionCode != "UNAUTHENTICATED_EXCEPTION" && exceptionCode != "UNAUTHORIZED_EXCEPTION")
            {
                context.Response.ContentType = "application/json";
                text = JsonSerializer.Serialize(new MessageModel
                {
                    Code = exceptionCode,
                    Message = message
                });
                await context.Response.WriteAsync(text);
                return;
            }
            logger.LogError(exception, $"全局异常处理\n异常:{exceptionCode}\n请求地址：{context?.Request?.Path.Value?.ToString()}\nStatusCode：{httpStatusCode}");
            context.Response.ContentType = "application/json";
            text = JsonSerializer.Serialize(new MessageModel
            {
                Code = exceptionCode,
                Message = message
            });
            await context.Response.WriteAsync(text);
            return;
        }
    }
}