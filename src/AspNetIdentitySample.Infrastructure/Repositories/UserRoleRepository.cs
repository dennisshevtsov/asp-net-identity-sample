// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/> in the database.</summary>
  public sealed class UserRoleRepository : IUserRoleRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.Infrastructure.Repositories.UserRoleRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public UserRoleRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="userIdentity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<List<UserRoleEntity>> GetRolesAsync(IUserIdentity userIdentity, CancellationToken cancellationToken)
      => _dbContext.Set<UserRoleEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(userIdentity.UserId.ToString())
                   .ToListAsync(cancellationToken);
  }
}
