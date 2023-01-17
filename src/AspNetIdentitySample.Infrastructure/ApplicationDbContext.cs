// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure
{
  using Microsoft.EntityFrameworkCore;

  using AspNetIdentitySample.Infrastructure.Configurations;

  /// <summary>Represents a session with the database and can be used to query and save instances of your entities.</summary>
  public sealed class ApplicationDbContext : DbContext
  {
    /// <summary>Configure the model that was discovered by convention from the entity types.</summary>
    /// <param name="modelBuilder">Provides a simple API surface for configuring a <see cref="Microsoft.EntityFrameworkCore.Metadata.IMutableModel" /> that defines the shape of your entities, the relationships between them, and how they map to the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
  }
}
