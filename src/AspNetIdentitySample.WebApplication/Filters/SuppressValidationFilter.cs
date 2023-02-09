// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Filters
{
  using Microsoft.AspNetCore.Mvc.Filters;

  /// <summary>Suppresses validation for the GET HTTP request.</summary>
  public sealed class SuppressValidationFilter : IActionFilter
  {
    /// <summary>Called before the action executes, after model binding is complete.</summary>
    /// <param name="context">The <see cref="Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext"/>.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.HttpContext.Request.Method == HttpMethods.Get)
      {
        context.ModelState.Clear();
      }
    }

    /// <summary>Called after the action executes, before the action result.</summary>
    /// <param name="context">The <see cref="Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext"/>.</param>
    public void OnActionExecuted(ActionExecutedContext context) { }
  }
}
