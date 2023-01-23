// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace AspNetIdentitySample.WebApplication.Controllers
{
  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Authorize("AdminOnlyPolicy")]
  [Route("")]
  public sealed class HomeController : Controller
  {
    /// <summary>Handles the get todo list query request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    public IActionResult Get()
    {
      return View("HomeView");
    }
  }
}
