// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("account")]
  public sealed class LogoutController : Controller
  {
    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.LogoutController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    public LogoutController(SignInManager<UserEntity> signInManager)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpGet("logout")]
    public IActionResult Get()
    {
      return View("LogoutView");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Post(LogoutViewModel vm)
    {
      await _signInManager.SignOutAsync();

      return LocalRedirect(vm.ReturnUrl ?? "/");
    }
  }
}
