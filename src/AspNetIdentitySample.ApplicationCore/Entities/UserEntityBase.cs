// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Entities
{
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents a base of an entity for that a user is a root.</summary>
  public abstract class UserEntityBase : IUserIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid UserId { get; set; }
  }
}
