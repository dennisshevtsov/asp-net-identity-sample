// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("account")]
  public sealed class SignOutController : Controller
  {
    public const string ViewName = "SignOutView";

    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IUserRepository _userRepository;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.SignOutController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    /// <param name="userRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</param>
    public SignOutController(SignInManager<UserEntity> signInManager, IUserRepository userRepository)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents data to sign out an account.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet("signout")]
    public async Task<IActionResult> Get(SignOutAccountViewModel vm, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(User.Identity!.Name!, cancellationToken);

      vm.User.Name = userEntity!.Name;
      vm.User.IsAuthenticated = true;

      return View(SignOutController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents data to sign out an account.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future. The result is an object that defines a contract that represents the result of an action method.</returns>
    [HttpPost("signout")]
    public async Task<IActionResult> Post(SignOutAccountViewModel vm)
    {
      await _signInManager.SignOutAsync();

      return LocalRedirect(vm.ReturnUrl);
    }
  }
}
