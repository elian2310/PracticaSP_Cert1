using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services;
using Logic;
using Serilog;

namespace Practica2.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                Log.Information("Before NEXT, in Exception handler middleware.");
                await _next(httpContext);
                Log.Information("After NEXT, in Exception handler middleware.");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, httpContext);
            }
        }

        private Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
        {
            string errorMessage = "";
            int errorCode = (int)HttpStatusCode.OK;
            if (ex.InnerException is NumberServiceException)
            {
                errorMessage = "Something happens on our Backing Services (out of our system): " + ex.Message;
                errorCode = (int)HttpStatusCode.NotFound;
            }
            if (ex is InvalidProductDataException)
            {
                errorMessage = "Data Exception: " + ex.Message;
                errorCode = (int)HttpStatusCode.NotFound;
            }
            if (ex is ProductNotFoundException)
            {
                errorMessage = "Product not found: " + ex.Message;
                errorCode = (int)HttpStatusCode.NotFound;
            }
            var response = new { Message = errorMessage, ErrorCode = errorCode };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = errorCode;
            Log.Error("There was an error in the application: " + ex.Message);
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}