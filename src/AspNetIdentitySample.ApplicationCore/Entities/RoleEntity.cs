// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Entities
{
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents details of a role.</summary>
  public sealed class RoleEntity : IUserIdentity, IRoleIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets/sets an object that represents a name of a role.</summary>
    public string? RoleName { get; set; }

    /// <summary>Gets/sets an object that represents a display name of a role.</summary>
    public string? DisplayName { get; set; }
  }
}
