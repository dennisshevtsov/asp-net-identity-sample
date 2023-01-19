// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  using Microsoft.AspNetCore.Mvc;

  /// <summary>Represents data to log in an account.</summary>
  public sealed class LoginAccountViewModel : AccountViewModelBase
  {
    /// <summary>Gets/sets an object that represents an email of an account.</summary>
    [Required]
    [FromForm]
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a password of an account.</summary>
    [Required]
    [FromForm]
    public string? Password { get; set; }

    /// <summary>Gets/sets an object that indicates if a login request is persistent.</summary>
    [FromForm]
    public bool RememberMe { get; set; }
  }
}
