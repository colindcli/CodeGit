using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

/// <summary>
/// ActionFilter
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomActionAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        //设置了context.Result值会停止往下执行
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //设置了context.Result值会停止往下执行
    }
}