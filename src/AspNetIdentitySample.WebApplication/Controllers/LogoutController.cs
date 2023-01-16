﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("logout")]
  public sealed class LogoutController : Controller
  {
    [HttpGet]
    public IActionResult Get()
    {
      return View("LogoutView");
    }

    [HttpPost]
    public IActionResult Post()
    {
      return RedirectToAction("LogoutView");
    }
  }
}