// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Repositories
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public sealed class RoleRepository : IRoleRepository
  {
    /// <summary>Gets a role by a role ID.</summary>
    /// <param name="roleName">An object that represents a name of a role.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<RoleEntity> GetRoleAsync(string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <summary>Adds a new role to the database.</summary>
    /// <param name="roleEntity">An object that represents details of a role.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task AddRoleAsync(RoleEntity roleEntity, CancellationToken cancellationToken) => throw new NotImplementedException();
  }
}
