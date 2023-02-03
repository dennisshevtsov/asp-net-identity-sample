// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.Stores;
  using AspNetIdentitySample.WebApplication.Defaults;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to set up ASP.NET Core Identity.</summary>
  public static class IdentityExtensions
  {
    /// <summary>Sets up services for the Identity.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpIdentity(this IServiceCollection services)
    {
      services.AddIdentity<UserEntity, RoleEntity>(options =>
              {
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
              })
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>()
              .AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(options =>
      {
        options.AccessDeniedPath = $"/{Routing.AccountRoute}/{Routing.AccessDeniedEndpoint}";
        options.LoginPath = $"/{Routing.AccountRoute}/{Routing.SignInEndpoint}";
        options.ReturnUrlParameter = ToCamelCase(nameof(ViewModelBase.ReturnUrl));
      });

      return services;
    }

    private static string ToCamelCase(string value)
      => value.Substring(0, 1).ToLower() + value.Substring(1);
  }
}
