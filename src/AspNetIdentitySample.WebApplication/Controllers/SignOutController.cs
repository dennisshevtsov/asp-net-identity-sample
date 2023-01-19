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
  public sealed class SignOutController : Controller
  {
    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.SignOutController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    public SignOutController(SignInManager<UserEntity> signInManager)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpGet("signout")]
    public IActionResult Get(SignOutAccountViewModel vm)
    {
      return View("SignOutView", vm);
    }

    [HttpPost("signout")]
    public async Task<IActionResult> Post(SignOutAccountViewModel vm)
    {
      await _signInManager.SignOutAsync();

      return LocalRedirect(vm.ReturnUrl);
    }
  }
}
