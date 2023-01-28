// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetIdentitySample.ApplicationCore.Services;

  public static class ApplicationCoreExtensions
  {
    public static IServiceCollection SetUpApplicationCore(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
