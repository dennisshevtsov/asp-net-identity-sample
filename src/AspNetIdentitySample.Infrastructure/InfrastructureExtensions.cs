// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Options;

  using AspNetIdentitySample.ApplicationCore.Database;
  using AspNetIdentitySample.Infrastructure;
  using AspNetIdentitySample.Infrastructure.Repositories;

  /// <summary>Provides a simple API to register infrastructure services.</summary>
  public static class InfrastructureExtensions
  {
    /// <summary>Sets up services for the database.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpDatabase(
      this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<DatabaseOptions>(configuration);

      services.AddDbContext<DbContext, ApplicationDbContext>(
        (provider, builder) =>
        {
          var options = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

          if (string.IsNullOrWhiteSpace(options.AccountEndpoint))
          {
            throw new ArgumentNullException(nameof(options.AccountEndpoint));
          }

          if (string.IsNullOrWhiteSpace(options.AccountKey))
          {
            throw new ArgumentNullException(nameof(options.AccountEndpoint));
          }

          if (string.IsNullOrWhiteSpace(options.DatabaseName))
          {
            throw new ArgumentNullException(nameof(options.DatabaseName));
          }

          builder.UseCosmos(options.AccountEndpoint, options.AccountKey, options.DatabaseName);
        });

      services.AddScoped<IUserRepository, UserRepository>();

      return services;
    }
  }
}
