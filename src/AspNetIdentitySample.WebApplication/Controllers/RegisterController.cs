// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route(Routing.AccountRoute)]
  public sealed class RegisterController : Controller
  {
    public const string ViewName = "RegisterView";

    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.RegisterController"/> class.</summary>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public RegisterController(UserManager<UserEntity> userManager)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents data to register a new user.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.RegisterEndpoint)]
    public IActionResult Get(RegisterUserViewModel vm)
    {
      ModelState.Clear();

      return View(RegisterController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents data to register a new user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpPost(Routing.RegisterEndpoint)]
    public async Task<IActionResult> Post(RegisterUserViewModel vm, CancellationToken cancellationToken)
    {
      if (ModelState.IsValid)
      {
        await _userManager.CreateAsync(vm.ToEntity(), vm.Password!);

        return RedirectToAction(nameof(UserListController.Get), nameof(UserListController).Replace("Controller", ""));
      }

      return View(RegisterController.ViewName, vm);
    }
  }
}
