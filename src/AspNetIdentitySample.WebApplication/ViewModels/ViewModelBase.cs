// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  /// <summary>Represents a base view model.</summary>
  public abstract class ViewModelBase
  {
    /// <summary>Gets/sets an object that represents the current user.</summary>
    public UserViewModel User { get; set; } = new UserViewModel();
  }
}
