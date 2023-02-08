// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;
  using System.Security.Claims;

  using AutoMapper;
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class UserListController : Controller
  {
    public const string ViewName = "UserListView";

    private readonly IMapper _mapper;

    private readonly IUserService _userService;

    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.UserListController"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to convert objects.</param>
    /// <param name="userService">An object that provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</param>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public UserListController(IMapper mapper, IUserService userService, UserManager<UserEntity> userManager)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the user list action.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(UserListViewModel vm, CancellationToken cancellationToken)
    {
      var userEntityCollection = await _userService.GetUsersAsync(cancellationToken);

      vm.Users = _mapper.Map<List<UserListViewModel.UserViewModel>>(userEntityCollection);

      return View(UserListController.ViewName, vm);
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
