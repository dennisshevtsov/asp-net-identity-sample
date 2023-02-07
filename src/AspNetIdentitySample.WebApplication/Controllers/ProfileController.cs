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
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class ProfileController : Controller
  {
    public const string ViewName = "ProfileView";

    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Controllers.ProfileController"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to convert objects.</param>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    public ProfileController(IMapper mapper, UserManager<UserEntity> userManager)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Get(ProfileViewModel vm)
    {
      ModelState.Clear();

      var claimsPrincipal = _mapper.Map<ClaimsPrincipal>(vm.User);
      var userEntity = await _userManager.GetUserAsync(claimsPrincipal);

      _mapper.Map(userEntity, vm);

      return View(ProfileController.ViewName, vm);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="vm">An object that represents the view model for the profile action.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Routing.ProfileEndpoint)]
    public async Task<IActionResult> Post(ProfileViewModel vm)
    {
      var claimsPrincipal = _mapper.Map<ClaimsPrincipal>(vm.User);
      var userEntity = await _userManager.GetUserAsync(claimsPrincipal);

      _mapper.Map(vm, userEntity);

      await _userManager.UpdateAsync(userEntity!);

      return RedirectToAction(nameof(ProfileController.Get), new { rerturnUrl = vm.ReturnUrl });
    }
  }
}
