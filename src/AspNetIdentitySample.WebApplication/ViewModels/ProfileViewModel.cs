// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Represents the view model for the profile action.</summary>
  public sealed class ProfileViewModel : ViewModelBase
  {
    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a return URL.</summary>
    public string ReturnUrl { get; set; } = "/";

    /// <summary>Populates the view model with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    public void FromEntity(UserEntity userEntity)
    {
      Name = userEntity.Name;
      Email = userEntity.Email;
    }

    /// <summary>Updates an instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    public void ToEntity(UserEntity userEntity)
    {
      userEntity.Name = Name;
    }
  }
}
