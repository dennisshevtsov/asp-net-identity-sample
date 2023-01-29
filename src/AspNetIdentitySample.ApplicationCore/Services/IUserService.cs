// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services
{
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
  public interface IUserService
  {
    /// <summary>Gets a collection of users.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<List<UserEntity>> GetUsersAsync(CancellationToken cancellationToken);
  }
}
