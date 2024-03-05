using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LawSchool.Utilities.Filters;

public class ValidationFilter : IActionFilter
{
    public ValidationFilter() { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments
            .SingleOrDefault(arg => arg.Value != null && arg.Value.ToString().Contains("Dto"))
            .Value;

        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Object sent from client is null.");
            return;
        }

        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}