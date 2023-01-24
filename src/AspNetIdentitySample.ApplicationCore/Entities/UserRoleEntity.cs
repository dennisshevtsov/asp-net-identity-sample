// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Entities
{
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents details of a user and role relation.</summary>
  public sealed class UserRoleEntity : UserEntityBase, IRoleIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a role.</summary>
    public string? RoleName { get; set; }
  }
}
