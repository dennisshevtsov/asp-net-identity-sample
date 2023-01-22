// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  [AllowAnonymous]
  [Route("account")]
  public sealed class AccessDeniedController : Controller
  {
    public const string ViewName = "AccessDeniedView";

    [HttpGet("access-denied")]
    public IActionResult Get()
    {
      return View(AccessDeniedController.ViewName);
    }
  }
}
