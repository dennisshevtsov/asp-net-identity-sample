// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

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

    public UserController(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
    public async Task<IActionResult> Post(UserViewModel vm, CancellationToken cancellationToken)
    {
      if (ModelState.IsValid)
      {
        var userEntity = await _userService.GetUserAsync(vm, cancellationToken);

        if (userEntity != null)
        {
          vm.ToEntity(userEntity);

          await _userService.UpdateUserAsync(userEntity, cancellationToken);
        }
        else
        {
          userEntity = new UserEntity();

          vm.ToEntity(userEntity);

          await _userService.AddUserAsync(userEntity, cancellationToken);
        }

        return RedirectToAction(nameof(UserController.Get), new { userId = userEntity.Id, returnUrl = vm.ReturnUrl });
      }

      return View(UserController.ViewName, vm);
    }
  }
}
