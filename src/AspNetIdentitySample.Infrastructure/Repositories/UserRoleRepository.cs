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
  using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<List<UserRoleEntity>> GetRolesAsync(IUserIdentity identity, CancellationToken cancellationToken)
      => _dbContext.Set<UserRoleEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(identity.UserId.ToString())
                   .OrderBy(entity => entity.RoleName)
                   .ToListAsync(cancellationToken);

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="AspNetIdentitySample.ApplicationCore.Identities.IUserIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<Dictionary<IUserIdentity, List<UserRoleEntity>>> GetRolesAsync(IEnumerable<IUserIdentity> identities, CancellationToken cancellationToken)
    {
      var userIdCollection = identities.Select(entity => entity.UserId)
                                       .ToArray();

      var userRoleEntityCollection =
        await _dbContext.Set<UserRoleEntity>()
                        .AsNoTracking()
                        .Where(entity => userIdCollection.Contains(entity.UserId))
                        .OrderBy(entity => entity.RoleName)
                        .ToListAsync(cancellationToken);

      var userRoleEntityDictionary = new Dictionary<IUserIdentity, List<UserRoleEntity>>(new UserIdentityComparer());

      foreach (var userRoleEntity in userRoleEntityCollection)
      {
        if (!userRoleEntityDictionary.TryGetValue(userRoleEntity, out var userRoleEntityCollectionForUser))
        {
          userRoleEntityCollectionForUser = new List<UserRoleEntity>();

          userRoleEntityDictionary.Add(userRoleEntity, userRoleEntityCollectionForUser);
        }

        userRoleEntityCollectionForUser.Add(userRoleEntity);
      }

      return userRoleEntityDictionary;
    }

    /// <summary>Deletes roles for a user.</summary>
    /// <param name="userRoleEntityCollection">An object that represents a collection of roles for a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task DeleteRolesAsync(
      IEnumerable<UserRoleEntity> userRoleEntityCollection,
      CancellationToken cancellationToken)
    {
      var userRoleEntityEntryCollection = new List<EntityEntry<UserRoleEntity>>();

      foreach (var userRoleEntity in userRoleEntityCollection)
      {
        userRoleEntityEntryCollection.Add(_dbContext.Remove(userRoleEntity));
      }

      await _dbContext.SaveChangesAsync(cancellationToken);

      foreach (var userRoleEntityEntry in userRoleEntityEntryCollection)
      {
        userRoleEntityEntry.State = EntityState.Detached;
      }
    }

    public sealed class UserIdentityComparer : IEqualityComparer<IUserIdentity>
    {
      public bool Equals(IUserIdentity? a, IUserIdentity? b)
      {
        if (a != null && b != null)
        {
          return a.UserId == b.UserId;
        }

        if (a == null && b == null)
        {
          return true;
        }

        return false;
      }

      public int GetHashCode(IUserIdentity identity) => identity.UserId.GetHashCode();
    }
  }
}
