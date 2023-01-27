// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  using AspNetIdentitySample.WebApplication.ViewModels;

  [Route("profile")]
  public sealed class ProfileController : Controller
  {
    public const string ViewName = "ProfileView";

    [HttpGet("{userId}")]
    public IActionResult Get(ProfileViewModel vm)
    {
      return View(ProfileController.ViewName, vm);
    }
  }
}
