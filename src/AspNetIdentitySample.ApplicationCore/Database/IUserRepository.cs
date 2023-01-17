// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Database
{
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public interface IUserRepository
  {
    /// <summary>Gets a user by a role ID.</summary>
    /// <param name="userId">An object that represents an ID of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<UserEntity> GetUserAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>Adds a new user to the database.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task AddUserAsync(UserEntity userEntity, CancellationToken cancellationToken);
  }
}
