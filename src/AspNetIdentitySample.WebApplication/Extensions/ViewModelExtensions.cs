// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Extensions
{
  using System.Security.Claims;

  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Provides the methods to extend the API of the view models.</summary>
  public static class ViewModelExtensions
  {
    /// <summary>Creates an instace of the <see cref="System.Security.Claims.ClaimsPrincipal"/> class.</summary>
    /// <returns>An object that contains a user identity.</returns>
    public static ClaimsPrincipal ToPrincipal(this IUserIdentity identity)
    {
      var identityClaim = new Claim(ClaimTypes.NameIdentifier, identity.UserId.ToString());
      var claims = new[] { identityClaim };
      var claimsIdentity = new ClaimsIdentity(claims);
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      return claimsPrincipal;
    }
  }
}
