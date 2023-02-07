// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;
  
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.Extensions;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class ProfileController : Controller
  {
    public const string ViewName = "ProfileView";

    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.ProfileController"/> class.</summary>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public ProfileController(UserManager<UserEntity> userManager)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Get(ProfileViewModel vm)
    {
      ModelState.Clear();

      var userEntity = await _userManager.GetUserAsync(vm.User.ToPrincipal());

      vm.FromEntity(userEntity!);

      return View(ProfileController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Post(ProfileViewModel vm)
    {
      var userEntity = await _userManager.GetUserAsync(vm.User.ToPrincipal());

      vm.ToEntity(userEntity!);

      await _userManager.UpdateAsync(userEntity!);

      return RedirectToAction(nameof(ProfileController.Get), new { rerturnUrl = vm.ReturnUrl });
    }
  }
}
