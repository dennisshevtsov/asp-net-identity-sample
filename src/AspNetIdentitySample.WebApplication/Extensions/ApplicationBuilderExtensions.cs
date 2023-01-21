// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to configure the app.</summary>
  public static class ApplicationBuilderExtensions
  {
    /// <summary>Initializes a database.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    /// <returns>An object that defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

        dbContext.Database.EnsureCreated();

        foreach (var userEntity in ApplicationBuilderExtensions.GetTestUsers())
        {
          ApplicationBuilderExtensions.TryAddUser(userEntity, dbContext);
        }
      }

      return app;
    }

    private static IEnumerable<UserEntity> GetTestUsers()
    {
      yield return new UserEntity
      {
        Email = "test@test.test",
        Name = "Test User",
        PasswordHash = "AQAAAAIAAYagAAAAEK1J2OGSiw1GPjwtTfNlKBOTGZg0ktpqEd7YkwbfMRWOw35KYVpsAQzpC2qwjtN0wg==",
      };
    }

    private static void TryAddUser(UserEntity userEntity, DbContext dbContext)
    {
      var userExists = dbContext.Set<UserEntity>().SingleOrDefaultAsync(entity => string.Equals(entity.Email, userEntity.Email, StringComparison.OrdinalIgnoreCase)) != null;

      if (!userExists)
      {
        dbContext.Add(userEntity);
        dbContext.SaveChanges();
      }
    }
  }
}
