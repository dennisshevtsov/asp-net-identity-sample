// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Represents data to register a new user.</summary>
  public sealed class RegisterUserViewModel : ViewModelBase
  {
    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    [Required]
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a password of a user.</summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>Gets/sets an object that represents a repeated password of a user.</summary>
    [Required]
    [Compare(nameof(RegisterUserViewModel.Password))]
    public string? RepeatedPassowrd { get; set; }

    /// <summary>Creates an instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> class from the view model.</summary>
    /// <returns>An object that represents details of a user.</returns>
    public UserEntity ToEntity()
    {
      var userEntity = new UserEntity
      {
        Name = Name,
        Email = Email,
      };

      return userEntity;
    }
  }
}
