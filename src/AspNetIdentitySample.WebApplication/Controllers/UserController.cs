// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route("user")]
  public class UserController : Controller
  {
    public const string ViewName = "UserListView";

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public async Task<IActionResult> Get(UserListViewModel vm, CancellationToken cancellationToken)
    {
      var userEntityCollection = await _userService.GetUsersAsync(cancellationToken);

      vm.WithUsers(userEntityCollection);

      return View(UserController.ViewName, vm);
    }
  }
}
