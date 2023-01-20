// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.Infrastructure.ValueGeneration;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
  {
    private const string DescriminatorPropertyName = "type";

    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public UserEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of type <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.Id);
      builder.HasKey(entity => entity.UserId);

      builder.Property(typeof(string), UserEntityTypeConfiguration.DescriminatorPropertyName)
             .HasValueGenerator<DescriminatorValueGenerator>();
      builder.HasDiscriminator(UserEntityTypeConfiguration.DescriminatorPropertyName, typeof(string));

      builder.Property(entity => entity.Id).ToJsonProperty("id").HasValueGenerator<GuidValueGenerator>();
      builder.Property(entity => entity.UserId).ToJsonProperty("userId").HasValueGenerator<UserPartitionKeyValueGenerator>();

      builder.Property(entity => entity.Email).ToJsonProperty("email");
      builder.Property(entity => entity.Name).ToJsonProperty("name");
      builder.Property(entity => entity.PasswordHash).ToJsonProperty("passwordHash");
    }
  }
}
