// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers
{
  [Route("profile")]
  public sealed class ProfileController : Controller
  {
    public const string ViewName = "ProfileView";

    public IActionResult Get()
    {
      return View(ProfileController.ViewName);
    }
  }
}
