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
  public sealed class UserIdentityProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Mapping.UserIdentityProfile"/> class.</summary>
    public UserIdentityProfile()
    {
      UserIdentityProfile.Configure(this);
    }

    private static void Configure(IProfileExpression expression)
    {
      expression.CreateMap<IUserIdentity, ClaimsPrincipal>()
                .ConstructUsing(identity => identity.ToPrincipal());
    }
  }
}
