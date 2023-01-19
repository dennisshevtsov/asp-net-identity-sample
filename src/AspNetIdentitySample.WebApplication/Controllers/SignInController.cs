﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route("account")]
  public sealed class SignInController : Controller
  {
    private const string ViewName = "SignInView";

    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.SignInController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    public SignInController(SignInManager<UserEntity> signInManager)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpGet("signin")]
    public IActionResult Get()
    {
      return View(SignInController.ViewName, new SignInAccountViewModel());
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Post(SignInAccountViewModel vm)
    {
      if (ModelState.IsValid)
      {
        var signInResult = await _signInManager.PasswordSignInAsync(vm.Email!, vm.Password!, false, false);

        if (signInResult != null && signInResult.Succeeded)
        {
          return LocalRedirect(vm.ReturnUrl);
        }

        ModelState.Clear();
        ModelState.AddModelError(nameof(SignInAccountViewModel.Email), "The credentials are not valid.");
      }

      return View(SignInController.ViewName, vm);
    }
  }
}