// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class RegisterController : Controller
  {
    public const string ViewName = "RegisterView";

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents data to register a new user.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.RegisterEndpoint)]
    public IActionResult Get(RegisterUserViewModel vm)
    {
      ModelState.Clear();

      return View(RegisterController.ViewName, vm);
    }
  }
}
