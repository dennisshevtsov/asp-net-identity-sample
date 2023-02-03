// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.AspNetCore.Mvc.Razor;
  using Microsoft.AspNetCore.Rewrite;

  using AspNetIdentitySample.WebApplication.Binding;
  using AspNetIdentitySample.WebApplication.Defaults;

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
        options.Filters.Add(new AuthorizeFilter());
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

        options.ModelBinderProviders.Insert(0, new ViewModelBinderProvider());
      });
      services.Configure<RazorViewEngineOptions>(options =>
      {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add($"/Views/{{0}}{RazorViewEngine.ViewExtension}");
      });

      return services;
    }

    /// <summary>Sets up an authorizatio middleware.</summary>
    /// <param name="services">n object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpAuthorization(this IServiceCollection services)
    {
      services.AddAuthorization(options =>
      {
        options.AddPolicy(
          "AdminOnlyPolicy",
          builder => builder.RequireRole("admin").Build());
      });

      return services;
    }

    /// <summary>Sets up rules to rewrite URLs.</summary>
    /// <param name="app">n object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IApplicationBuilder SetUpUrlRewriter(this IApplicationBuilder app)
    {
      var options =
        new RewriteOptions().AddRedirect("(.*)/$", "$1")
                            .AddRedirect("^[/]?$", Routing.AccountRoute);

      app.UseRewriter(options);

      return app;
    }
  }
}
