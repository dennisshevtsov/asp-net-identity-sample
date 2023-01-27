// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("profile")]
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
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(ProfileViewModel vm, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(vm.User, cancellationToken);

      vm.FromEntity(userEntity!);

      return View(ProfileController.ViewName, vm);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProfileViewModel vm, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(vm.User, cancellationToken);

      vm.ToEntity(userEntity!);

      await _userRepository.UpdateUserAsync(userEntity!, cancellationToken);

      return RedirectToAction(nameof(ProfileController.Get), new { rerturnUrl = vm.ReturnUrl });
    }
  }
}
