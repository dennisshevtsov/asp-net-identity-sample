// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  /// <summary>Represents a base view model.</summary>
  public abstract class ViewModelBase
  {
    /// <summary>Gets/sets an object that represents a title of page.</summary>
    public string PageTitle { get; set; } = "ASP.NET Core Identity Sample";

    /// <summary>Gets/sets an object that represents the current user.</summary>
    public CurrentAccountViewModel User { get; set; } = new CurrentAccountViewModel();

    /// <summary>Gets/sets an object that represents a URL to that the app should return after an successful login.</summary>
    public string ReturnUrl { get; set; } = "/";
  }
}
