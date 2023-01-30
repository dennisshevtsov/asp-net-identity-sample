// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.ViewModels
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;

  /// <summary>Represents the view model for the user list action.</summary>
  public sealed class UserListViewModel : ViewModelBase
  {
    /// <summary>Gets/sets an object that represents a collection of user records.</summary>
    public IList<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    /// <summary>Fills out the view model with a collection of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
    /// <param name="userEnittyCollection">An object that represents a collection of the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</param>
    public void WithUsers(List<UserEntity> userEnittyCollection)
    {
      foreach (var userEntity in userEnittyCollection)
      {
        Users.Add(new UserViewModel
        {
          UserId = userEntity.UserId,
          Name = userEntity.Name,
          Email = userEntity.Email,
        });
      }
    }

    /// <summary>Represents the view model for the user record.</summary>
    public sealed class UserViewModel : IUserIdentity
    {
      /// <summary>Gets/sets an object that represents an ID of a user.</summary>
      public Guid UserId { get; set; }

      /// <summary>Gets/sets an object that represents a name of a user.</summary>
      public string? Name { get; set; }

      /// <summary>Gets/sets an object that represents an email of a user.</summary>
      public string? Email { get; set; }

      /// <summary>Gets/sets an object that represents a string of user roles.</summary>
      public string? Roles { get; set; }
    }
  }
}
