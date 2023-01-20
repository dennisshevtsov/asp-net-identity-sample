// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.ValueGeneration
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  /// <summary>Generates values for properties when an entity is added to a context.</summary>
  public sealed class DescriminatorValueGenerator : ValueGenerator
  {
    /// <summary>Gets a value indicating whether the values generated are temporary or are permanent.</summary>
    public override bool GeneratesTemporaryValues => false;

    /// <summary>Gets a value to be assigned to a property.</summary>
    /// <param name="entry">The change tracking entry of the entity for which the value is being generated.</param>
    /// <returns>The generated value.</returns>
    protected override object? NextValue(EntityEntry entry) => entry.Entity.GetType().Name;
  }
}
