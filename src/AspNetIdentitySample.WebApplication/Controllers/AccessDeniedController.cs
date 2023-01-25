// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using Microsoft.AspNetCore.Authorization;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route("account")]
  public sealed class AccessDeniedController : Controller
  {
    public const string ViewName = "AccessDeniedView";

    private readonly IUserRepository _userRepository;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.AccessDeniedController"/> class.</summary>
    /// <param name="userRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</param>
    public AccessDeniedController(IUserRepository userRepository)
    {
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>Handles the GET request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet("access-denied")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(User.Identity!.Name!, cancellationToken);

      var vm = new AccessDeniedViewModel();

      vm.User.Name = userEntity!.Name;
      vm.User.IsAuthenticated = true;

      return View(AccessDeniedController.ViewName, vm);
    }
  }
}
