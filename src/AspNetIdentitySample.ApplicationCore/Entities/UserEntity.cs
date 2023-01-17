// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Entities
{
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents details of a user.</summary>
  public sealed class UserEntity : IUserIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a password hash of a user.</summary>
    public string? PasswordHash { get; set; }
  }
}
