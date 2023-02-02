// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.UserRoute)]
  public sealed class UserController : Controller
  {
    public const string ViewName = "UserView";

    private IUserService _userService;
    private UserManager<UserEntity> _userManager;

    public UserController(IUserService userService, UserManager<UserEntity> userManager)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.UserEndpoint, Order = 1)]
    [HttpGet(Routing.NewUserEndpoint, Order = 2)]
    public async Task<IActionResult> Get(UserViewModel vm, CancellationToken cancellationToken)
    {
      ModelState.Clear();

      var userEntity = await _userService.GetUserAsync(vm, cancellationToken);

      if (userEntity != null)
      {
        vm.FromEntity(userEntity);
      }

      return View(UserController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>AAn object that represents an asynchronous operation.</returns>
    [HttpPost(Routing.UserEndpoint, Order = 1)]
    [HttpPost(Routing.NewUserEndpoint, Order = 2)]
    public async Task<IActionResult> Post(UserViewModel vm)
    {
      if (ModelState.IsValid)
      {
        var userEntity = await _userManager.GetUserAsync(vm.ToPrincipal());

        if (userEntity != null)
        {
          vm.ToEntity(userEntity);

          await _userManager.UpdateAsync(userEntity);
        }
        else
        {
          userEntity = vm.ToEntity();

          var result = await _userManager.CreateAsync(userEntity, "test");
        }

        return RedirectToAction(nameof(UserController.Get), new { userId = userEntity.Id, returnUrl = vm.ReturnUrl });
      }

      return View(UserController.ViewName, vm);
    }
  }
}
