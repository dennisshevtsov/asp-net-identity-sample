// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Database
{
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public interface IRoleRepository
  {
    public Task<RoleEntity> GetRoleAsync(Guid userId, CancellationToken cancellationToken);

    public Task CreateRoleAsync(RoleEntity roleEntity, CancellationToken cancellationToken);
  }
}
