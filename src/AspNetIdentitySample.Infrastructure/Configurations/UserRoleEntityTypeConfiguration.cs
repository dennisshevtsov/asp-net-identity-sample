// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.Infrastructure.ValueGeneration;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
  {
    private const string DescriminatorPropertyName = "_type";

    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public UserRoleEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of type <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.RoleName);
      builder.HasPartitionKey(entity => entity.UserId);

      builder.Property(typeof(string), UserRoleEntityTypeConfiguration.DescriminatorPropertyName)
             .HasValueGenerator<DescriminatorValueGenerator>();
      builder.HasDiscriminator(UserRoleEntityTypeConfiguration.DescriminatorPropertyName, typeof(string));

      builder.Property(entity => entity.RoleName).ToJsonProperty("id");
      builder.Property(entity => entity.UserId).ToJsonProperty("userId");
    }
  }
}
