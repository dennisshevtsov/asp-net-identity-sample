// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Options;

  using AspNetIdentitySample.Infrastructure.Configurations;

  /// <summary>Represents a session with the database and can be used to query and save instances of your entities.</summary>
  public sealed class ApplicationDbContext : DbContext
  {
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.Infrastructure.ApplicationDbContext"/> class.</summary>
    /// <param name="dbContextOptions">An object that represents the options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    /// <param name="databaseOptions">An object that represents settings of a database.</param>
    public ApplicationDbContext(
      DbContextOptions dbContextOptions,
      IOptions<DatabaseOptions> databaseOptions) : base(dbContextOptions)
    {
      _databaseOptions = databaseOptions ?? throw new ArgumentNullException(nameof(databaseOptions));
    }

    /// <summary>Configure the model that was discovered by convention from the entity types.</summary>
    /// <param name="modelBuilder">Provides a simple API surface for configuring a <see cref="Microsoft.EntityFrameworkCore.Metadata.IMutableModel" /> that defines the shape of your entities, the relationships between them, and how they map to the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(_databaseOptions.Value.UserContainerName));
    }
  }
}
