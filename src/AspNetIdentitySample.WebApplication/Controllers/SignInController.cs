// Copyright (c) Dennis Shevtsov. All rights reserved.
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
    public const string ViewName = "SignInView";
    public const string InvalidCredentialsErrorMessage = "The credentials are not valid.";

    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.SignInController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    public SignInController(SignInManager<UserEntity> signInManager)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents data to sign in an account.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet("signin")]
    public IActionResult Get(SignInAccountViewModel vm)
    {
      ModelState.Clear();

      return View(SignInController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents data to sign in an account.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future. The result is an object that defines a contract that represents the result of an action method.</returns>
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
        ModelState.AddModelError(nameof(SignInAccountViewModel.Email), SignInController.InvalidCredentialsErrorMessage);
      }

      return View(SignInController.ViewName, vm);
    }
  }
}
