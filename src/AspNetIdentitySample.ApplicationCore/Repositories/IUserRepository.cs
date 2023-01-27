// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Repositories
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public interface IUserRepository
  {
    /// <summary>Gets a user by a user ID.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity?> GetUserAsync(IUserIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a user by a user email.</summary>
    /// <param name="email">An object that represents an email of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity?> GetUserAsync(string email, CancellationToken cancellationToken);

    /// <summary>Updates a user.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task UpdateUserAsync(UserEntity userEntity, CancellationToken cancellationToken);
  }
}
