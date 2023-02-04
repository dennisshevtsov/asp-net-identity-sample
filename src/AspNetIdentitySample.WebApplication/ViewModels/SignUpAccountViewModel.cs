// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  using AspNetIdentitySample.ApplicationCore.Entities;

  /// <summary>Represents data to register a new user.</summary>
  public sealed class SignUpAccountViewModel : ViewModelBase
  {
    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a fist name of a user.</summary>
    [Required]
    public string? FirstName { get; set; }

    /// <summary>Gets/sets an object that represents a last name of a user.</summary>
    [Required]
    public string? LastName { get; set; }

    /// <summary>Gets/sets an object that represents a password of a user.</summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>Gets/sets an object that represents a repeated password of a user.</summary>
    [Required]
    [Compare(nameof(SignUpAccountViewModel.Password))]
    public string? RepeatedPassowrd { get; set; }

    /// <summary>Creates an instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> class from the view model.</summary>
    /// <returns>An object that represents details of a user.</returns>
    public UserEntity ToEntity()
    {
      var userEntity = new UserEntity
      {
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
      };

      return userEntity;
    }
  }
}
