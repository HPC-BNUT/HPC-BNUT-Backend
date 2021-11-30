using System.Net;
using Framework.Exceptions;
using Infrastructure.StandardResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace HPC_Endpoints.MiddleWares
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = Log.Logger;
                    var ex = context.Features.Get<IExceptionHandlerFeature>().Error;
                    logger.Debug(ex.ToString());
                    
                    if (ex is LogicException logicEx)
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(new ApiResult(false, logicEx.ApiStatusCode, logicEx.Message).ToString());
                    }
                    else if (ex is BadRequestException badEx)
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(new ApiResult(false, badEx.ApiStatusCode, badEx.Message).ToString());
                    }
                    else if (ex is NotFoundException notEx)
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(new ApiResult(false, notEx.ApiStatusCode, notEx.Message).ToString());
                    }
                    else if (ex is AppException appEx)
                    {
                        context.Response.StatusCode = (int) appEx.HttpStatusCode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(new ApiResult(false, appEx.ApiStatusCode, appEx.Message).ToString());
                    }

                });
            });
        }
    }
}