// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("login")]
  public sealed class LoginController : Controller
  {
    [HttpGet]
    public IActionResult Get()
    {
      return View("LoginView", new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Post(LoginViewModel vm)
    {
      return RedirectToAction("LoginView");
    }
  }
}
