// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Authorize("AdminOnlyPolicy")]
  [Route("home")]
  public sealed class HomeController : Controller
  {
    /// <summary>Handles the get todo list query request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet]
    public IActionResult Get(HomeViewModel vm, CancellationToken cancellationToken)
    {
      return View("HomeView", vm);
    }
  }
}
