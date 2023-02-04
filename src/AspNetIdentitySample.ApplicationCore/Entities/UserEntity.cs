// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Entities
{
  /// <summary>Represents details of a user.</summary>
  public sealed class UserEntity : UserEntityBase
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a first name of a user.</summary>
    public string? FirstName { get; set; }

    /// <summary>Gets/sets an object that represents a last name of a user.</summary>
    public string? LastName { get; set; }

    /// <summary>Gets/sets an object that represents a password hash of a user.</summary>
    public string? PasswordHash { get; set; }

    /// <summary>Gets/sets an object that represents a collection of user roles.</summary>
    public List<UserRoleEntity>? Roles { get; set; }
  }
}
