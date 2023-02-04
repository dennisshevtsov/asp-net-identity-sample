// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/>.</summary>
  public interface IUserRoleService
  {
    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<List<UserRoleEntity>> GetRolesAsync(IUserIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identities">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<Dictionary<IUserIdentity, List<UserRoleEntity>>> GetRolesAsync(IEnumerable<IUserIdentity> identities, CancellationToken cancellationToken);

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<IList<string>> GetRoleNamesAsync(IUserIdentity identity, CancellationToken cancellationToken);

    /// <summary>Deletes roles for a user.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task DeleteRolesAsync(IUserIdentity identity, CancellationToken cancellationToken);
  }
}
