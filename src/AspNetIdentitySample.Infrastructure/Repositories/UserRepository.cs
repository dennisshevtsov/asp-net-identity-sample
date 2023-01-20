// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Repositories
{
  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.ApplicationCore.Database;
  using AspNetIdentitySample.ApplicationCore.Entities;

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
    /// <param name="userId">An object that represents an ID of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
      => _dbContext.Set<UserEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(userId.ToString())
                   .Where(entity => entity.Id == userId)
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
  }
}
