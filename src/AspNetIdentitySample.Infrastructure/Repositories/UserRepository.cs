// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Repositories
{
  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public sealed class UserRepository : IUserRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.Infrastructure.Repositories.UserRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public UserRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a user by a user ID.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity?> GetUserAsync(IUserIdentity identity, CancellationToken cancellationToken)
        => _dbContext.Set<UserEntity>()
                     .AsNoTracking()
                     .WithPartitionKey(identity.UserId.ToString())
                     .Where(entity => entity.Id == identity.UserId)
                     .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Gets a user by a user email.</summary>
    /// <param name="email">An object that represents an email of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity?> GetUserAsync(string email, CancellationToken cancellationToken)
      => _dbContext.Set<UserEntity>()
                   .AsNoTracking()
                   .Where(entity => string.Equals(entity.Email, email, StringComparison.OrdinalIgnoreCase))
                   .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Updates a user.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task UpdateUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
      _dbContext.Update(userEntity);

      return _dbContext.SaveChangesAsync(cancellationToken);
    }
  }
}
