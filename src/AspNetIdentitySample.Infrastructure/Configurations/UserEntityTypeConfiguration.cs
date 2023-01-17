// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
  {
    /// <summary>Configures the entity of type <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
    }
  }
}
