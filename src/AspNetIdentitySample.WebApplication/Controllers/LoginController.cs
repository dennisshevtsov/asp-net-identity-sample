// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route("account")]
  public sealed class LoginController : Controller
  {
    [HttpGet("login")]
    public IActionResult Get()
    {
      return View("LoginView", new LoginViewModel());
    }

    [HttpPost("login")]
    public IActionResult Post([FromForm] LoginViewModel vm)
    {
      if (ModelState.IsValid)
      {
        RedirectToAction("Get", "Home");
      }

      return View("LoginView", vm);
    }
  }
}
