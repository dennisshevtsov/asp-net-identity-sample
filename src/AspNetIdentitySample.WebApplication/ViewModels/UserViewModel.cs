// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents details of the authenticated user.</summary>
  public sealed class UserViewModel : IUserIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a user.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that indicates if the user is authenticated.</summary>
    public bool IsAuthenticated { get; set; }

    /// <summary>Populates the view model with the <see cref="UserEntity"/>.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    public void FromEntity(UserEntity userEntity)
    {
      UserId = userEntity.Id;
      Name = userEntity.Name;
      IsAuthenticated = true;
    }
  }
}
