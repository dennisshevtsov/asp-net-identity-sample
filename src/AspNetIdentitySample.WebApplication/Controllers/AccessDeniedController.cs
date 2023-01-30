// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route(Routing.UserRoute)]
  public sealed class AccessDeniedController : Controller
  {
    public const string ViewName = "AccessDeniedView";

    /// <summary>Handles the GET request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.AccessDeniedEndpoint)]
    public IActionResult Get(AccessDeniedViewModel vm)
    {
      return View(AccessDeniedController.ViewName, vm);
    }
  }
}
