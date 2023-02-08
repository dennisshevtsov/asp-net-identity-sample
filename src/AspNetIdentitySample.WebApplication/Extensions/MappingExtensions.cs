// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetIdentitySample.WebApplication.Mapping;

  /// <summary>Provides a simple API to set up the mapping.</summary>
  public static class MappingExtensions
  {
    /// <summary>Set up the database.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpMapping(this IServiceCollection services)
    {
      services.AddAutoMapper(configuration =>
      {
        configuration.AddProfile<ProfileViewModelMapping>();
        configuration.AddProfile<SignUpAccountViewModelMapping>();
        configuration.AddProfile<UserIdentityMapping>();
        configuration.AddProfile<UserListViewModelMapping>();
      });

      return services;
    }
  }
}
