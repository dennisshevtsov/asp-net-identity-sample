﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
  public sealed class SignUpController : Controller
  {
    public const string ViewName = "RegisterView";

    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.SignUpController"/> class.</summary>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public SignUpController(UserManager<UserEntity> userManager)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents data to register a new user.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.SingUpEndpoint)]
    public IActionResult Get(SignUpAccountViewModel vm)
    {
      ModelState.Clear();

      return View(SignUpController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents data to register a new user.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpPost(Routing.SingUpEndpoint)]
    public async Task<IActionResult> Post(SignUpAccountViewModel vm)
    {
      if (ModelState.IsValid)
      {
        await _userManager.CreateAsync(vm.ToEntity(), vm.Password!);

        return RedirectToAction(nameof(UserListController.Get), nameof(UserListController).Replace("Controller", ""));
      }

      return View(SignUpController.ViewName, vm);
    }
  }
}