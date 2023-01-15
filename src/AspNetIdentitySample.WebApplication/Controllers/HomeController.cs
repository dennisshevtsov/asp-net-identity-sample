// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  [Route("")]
  public sealed class HomeController : Controller
  {
    public IActionResult Get()
    {
      return View("HomeView");
    }
  }
}
