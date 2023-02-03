// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class ProfileController : Controller
  {
    public const string ViewName = "ProfileView";

    private readonly IUserRepository _userRepository;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.ProfileController"/> class.</summary>
    /// <param name="userRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</param>
    public ProfileController(IUserRepository userRepository)
    {
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Get(ProfileViewModel vm, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(vm.User, cancellationToken);

      vm.FromEntity(userEntity!);

      return View(ProfileController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Post(ProfileViewModel vm, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(vm.User, cancellationToken);

      vm.ToEntity(userEntity!);

      await _userRepository.UpdateUserAsync(userEntity!, cancellationToken);

      return RedirectToAction(nameof(ProfileController.Get), new { rerturnUrl = vm.ReturnUrl });
    }
  }
}
