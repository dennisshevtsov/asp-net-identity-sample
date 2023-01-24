// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  /// <summary>Represents details of the authenticated user.</summary>
  public sealed class UserViewModel
  {
    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that indicates if the user is authenticated.</summary>
    public bool IsAuthenticated { get; set; }
  }
}
