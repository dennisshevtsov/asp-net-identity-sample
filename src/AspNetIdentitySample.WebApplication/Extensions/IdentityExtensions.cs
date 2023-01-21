// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.Stores;

  /// <summary>Provides a simple API to set up ASP.NET Core Identity.</summary>
  public static class IdentityExtensions
  {
    public static IServiceCollection SetUpIdentity(this IServiceCollection services)
    {
      services.AddIdentity<UserEntity, RoleEntity>()
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>()
              .AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = "/account/signin";
        options.ReturnUrlParameter = "returnUrl";
      });

      return services;
    }
  }
}
