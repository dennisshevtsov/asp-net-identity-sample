// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;
  using System.Security.Claims;

  using AutoMapper;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Authorize(Policies.AdminOnlyPolicy)]
  [Route(Routing.AccountRoute)]
  public sealed class UserController : Controller
  {
    public const string ViewName = "UserView";

    private readonly IMapper _mapper;

    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.UserController"/> class.</summary>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public UserController(IMapper mapper, UserManager<UserEntity> userManager)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.UserEndpoint)]
    public async Task<IActionResult> Get(UserViewModel vm)
    {
      var principal = _mapper.Map<ClaimsPrincipal>(vm);
      var userEntity = await _userManager.GetUserAsync(principal);

      if (userEntity != null)
      {
        _mapper.Map(userEntity, vm);
      }

      return View(UserController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>AAn object that represents an asynchronous operation.</returns>
    [HttpPost(Routing.UserEndpoint)]
    public async Task<IActionResult> Post(UserViewModel vm)
    {
      if (ModelState.IsValid)
      {
        var principal = _mapper.Map<ClaimsPrincipal>(vm);
        var userEntity = await _userManager.GetUserAsync(principal);

        if (userEntity != null)
        {
          _mapper.Map(vm, userEntity);

          await _userManager.UpdateAsync(userEntity);

          return RedirectToAction(nameof(UserController.Get), new { userId = userEntity.Id, returnUrl = vm.ReturnUrl });
        }
      }

      return View(UserController.ViewName, vm);
    }

    /// <summary>Handles the DELETE request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [HttpPost(Routing.DeleteUserEndpoint)]
    public async Task<IActionResult> Delete(DeleteAccountViewModel vm)
    {
      var principal = _mapper.Map<ClaimsPrincipal>(vm);
      var userEntity = await _userManager.GetUserAsync(principal);

      if (userEntity != null)
      {
        await _userManager.DeleteAsync(userEntity);
      }

      return RedirectToAction(nameof(UserListController.Get));
    }
  }
}
