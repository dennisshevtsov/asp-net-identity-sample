// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Database
{
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
  public interface IUserRepository
  {
    public Task<UserEntity> GetUserAsync(Guid userId, CancellationToken cancellationToken);

    public Task CreateUserAsync(UserEntity userEntity, CancellationToken cancellationToken);
  }
}
