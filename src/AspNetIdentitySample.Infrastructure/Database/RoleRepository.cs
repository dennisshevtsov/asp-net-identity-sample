// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Database
{
  using AspNetIdentitySample.ApplicationCore.Database;
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public sealed class RoleRepository : IRoleRepository
  {
    public Task<RoleEntity> GetRoleAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task CreateRoleAsync(RoleEntity roleEntity, CancellationToken cancellationToken) => throw new NotImplementedException();
  }
}
