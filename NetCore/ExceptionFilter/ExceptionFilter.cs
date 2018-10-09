using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

/// <summary>
/// 全局异常处理
/// </summary>
public static class ExceptionFilter
{
    public static void UseExceptionFilter(this IApplicationBuilder app) => app.UseExceptionHandler(builder => builder.Run(async context => await RequestDelegate(context)));

    private static Task RequestDelegate(HttpContext context)
    {
        return Task.Run(() =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var ex = feature?.Error;
            if (ex != null)
            {
                LogHelper.Fatal(ex.Message, ex);
            }
        });
    }
}