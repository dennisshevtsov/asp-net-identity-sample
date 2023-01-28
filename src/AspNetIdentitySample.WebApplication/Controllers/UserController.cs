// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("user")]
  public class UserController : Controller
  {
    public const string ViewName = "UserListView";

    [HttpGet]
    public IActionResult Get(UserListViewModel vm)
    {
      return View(UserController.ViewName, vm);
    }
  }
}
