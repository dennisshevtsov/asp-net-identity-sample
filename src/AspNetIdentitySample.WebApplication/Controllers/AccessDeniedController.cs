// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route("account")]
  public sealed class AccessDeniedController : Controller
  {
    public const string ViewName = "AccessDeniedView";

    /// <summary>Handles the GET request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet("access-denied")]
    public IActionResult Get()
    {
      return View(AccessDeniedController.ViewName);
    }
  }
}
