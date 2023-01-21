// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.AspNetCore.Mvc.Razor;

  /// <summary>Provides a simple API to set up the MVC pipeline.</summary>
  public static class MvcExtensions
  {
    /// <summary>Sets up services of the MVC pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpPipeline(this IServiceCollection services)
    {
      services.AddControllersWithViews(options =>
      {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                     .Build();
        var filter = new AuthorizeFilter(policy);

        options.Filters.Add(filter);
      });
      services.Configure<RazorViewEngineOptions>(options =>
      {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add($"/Views/{{0}}{RazorViewEngine.ViewExtension}");
      });

      return services;
    }
  }
}
