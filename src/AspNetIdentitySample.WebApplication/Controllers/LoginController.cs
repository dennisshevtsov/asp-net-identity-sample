// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  [Route("login")]
  public sealed class LoginController : Controller
  {
    [HttpGet]
    public IActionResult Get()
    {
      return View("LoginView");
    }

    [HttpPost]
    public IActionResult Post()
    {
      return RedirectToAction("LoginView");
    }
  }
}
