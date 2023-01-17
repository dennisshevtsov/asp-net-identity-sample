// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Identities
{
  /// <summary>Represents conditions to query a role.</summary>
  public interface IRoleIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a role.</summary>
    public string? RoleName { get; set; }
  }
}
