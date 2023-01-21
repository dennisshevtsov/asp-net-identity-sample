﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to set up the database.</summary>
  public static class DatabaseExtensions
  {
    /// <summary>Set up the database.</summary>
    /// <param name="app">An object that represents the web application used to configure the HTTP pipeline, and routes.</param>
    /// <returns>An object that defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
    public static WebApplication SetUpDatabase(this WebApplication app)
    {
      using (var scope = app.Services.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

        dbContext.Database.EnsureCreated();

        foreach (var userEntity in DatabaseExtensions.GetTestUsers(app.Configuration))
        {
          DatabaseExtensions.TryAddUser(userEntity, dbContext);
        }
      }

      return app;
    }

    private static IEnumerable<UserEntity> GetTestUsers(IConfiguration configuration)
    {
      yield return new UserEntity
      {
        Email = configuration.GetValue<string>("TestUserEmail"),
        Name = configuration.GetValue<string>("TestUserName"),
        PasswordHash = configuration.GetValue<string>("TestUserPasswordHash"),
      };
    }

    private static void TryAddUser(UserEntity userEntity, DbContext dbContext)
    {
      var userExists = dbContext.Set<UserEntity>().SingleOrDefault(entity => string.Equals(entity.Email, userEntity.Email, StringComparison.OrdinalIgnoreCase)) != null;

      if (!userExists)
      {
        dbContext.Add(userEntity);
        dbContext.SaveChanges();
      }
    }
  }
}