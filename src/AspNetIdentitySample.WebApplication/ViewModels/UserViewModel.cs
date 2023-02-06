// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using System.ComponentModel.DataAnnotations;
  using System.Security.Claims;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents the view model of the user action.</summary>
  public sealed class UserViewModel : ViewModelBase, IUserIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets/sets an object that represents a fist name of a user.</summary>
    [Required]
    public string? FirstName { get; set; }

    /// <summary>Gets/sets an object that represents a last name of a user.</summary>
    [Required]
    public string? LastName { get; set; }

    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    public string? Email { get; set; }

    /// <summary>Fills out the view model with the data of the an instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> class.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    public void FromEntity(UserEntity userEntity)
    {
      FirstName = userEntity.FirstName;
      LastName = userEntity.LastName;
      Email = userEntity.Email;
    }

    /// <summary>Fills out the an instance of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> class with data of the view model.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    public void ToEntity(UserEntity userEntity)
    {
      userEntity.FirstName = FirstName;
      userEntity.LastName = LastName;
    }

    /// <summary>Creates an instace of the <see cref="System.Security.Claims.ClaimsPrincipal"/> class.</summary>
    /// <returns>An object that contains a user identity.</returns>
    public ClaimsPrincipal ToPrincipal()
    {
      var identityClaim = new Claim(ClaimTypes.NameIdentifier, UserId.ToString());
      var claims = new[] { identityClaim };
      var claimsIdentity = new ClaimsIdentity(claims);
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      return claimsPrincipal;
    }
  }
}
