// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping
{
  using System.Security.Claims;

  using AutoMapper;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.WebApplication.Extensions;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class UserIdentityMapping : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Mapping.UserIdentityMapping"/> class.</summary>
    public UserIdentityMapping()
    {
      UserIdentityMapping.ConfigureMapping(this);
    }

    private static void ConfigureMapping(IProfileExpression expression)
    {
      expression.CreateMap<IUserIdentity, ClaimsPrincipal>()
                .ConstructUsing(identity => identity.ToPrincipal());
    }
  }
}
