// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetIdentitySample.ApplicationCore.Services;

  /// <summary>Provides a simple API to register the application core services.</summary>
  public static class ApplicationCoreExtensions
  {
    /// <summary>Sets up services for the application core.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpApplicationCore(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
