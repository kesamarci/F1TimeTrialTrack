﻿using F1TimeTrialTrack.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F1TimeTrialTrack.Helpers
{
    public class ValidF : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                var error = new Error
                (
                    String.Join(',',
                    (context.ModelState.Values.SelectMany(t => t.Errors.Select(z => z.ErrorMessage))).ToArray())
                );
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(error);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
