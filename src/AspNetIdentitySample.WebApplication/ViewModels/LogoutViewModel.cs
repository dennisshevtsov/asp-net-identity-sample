// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  /// <summary>Represents data to log out an account.</summary>
  public sealed class LogoutViewModel
  {
    /// <summary>Gets/sets an object that represents a URL to that the app should return after an successful login.</summary>
    [FromQuery]
    public string? ReturnUrl { get; set; }
  }
}
