// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.UserRoute)]
  public sealed class UserController : Controller
  {
    public const string ViewName = "UserView";

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.UserEndpoint)]
    [HttpGet(Routing.NewUserEndpoint)]
    public IActionResult Get(UserViewModel vm)
    {
      return View(UserController.ViewName, vm);
    }
  }
}
