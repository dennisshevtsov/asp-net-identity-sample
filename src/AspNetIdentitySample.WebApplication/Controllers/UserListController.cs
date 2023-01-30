// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.UserRoute)]
  public sealed class UserListController : Controller
  {
    public const string ViewName = "UserListView";

    private readonly IUserService _userService;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.UserListController"/> class.</summary>
    /// <param name="userService">An object that provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</param>
    public UserListController(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the user list action.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(UserListViewModel vm, CancellationToken cancellationToken)
    {
      var userEntityCollection = await _userService.GetUsersAsync(cancellationToken);

      vm.WithUsers(userEntityCollection);

      return View(UserListController.ViewName, vm);
    }
  }
}
