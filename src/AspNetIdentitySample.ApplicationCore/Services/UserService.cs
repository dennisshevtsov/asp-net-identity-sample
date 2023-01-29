// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/>.</summary>
  public sealed class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.ApplicationCore.Services.UserService"/> class.</summary>
    /// <param name="userRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</param>
    /// <param name="userRoleRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/> in the database.</param>
    public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
      _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
    }

    /// <summary>Gets a collection of users.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<List<UserEntity>> GetUsersAsync(CancellationToken cancellationToken)
    {
      var userEntityCollection = await _userRepository.GetUsersAsync(cancellationToken);
      var userRoleEntityDictionary = await _userRoleRepository.GetRolesAsync(userEntityCollection, cancellationToken);

      foreach (var userEntity in userEntityCollection)
      {
        if (userRoleEntityDictionary.TryGetValue(userEntity, out var userRoleEntityCollection))
        {
          userEntity.Roles = userRoleEntityCollection;
        }
      }

      return userEntityCollection;
    }
  }
}
