// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>Represents data to log in an account.</summary>
  public sealed class LoginViewModel
  {
    /// <summary>Gets/sets an object that represents an email of an account.</summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a password of an account.</summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>Gets/sets an object that indicates if a login request is persistent.</summary>
    public bool RememberMe { get; set; }
  }
}
