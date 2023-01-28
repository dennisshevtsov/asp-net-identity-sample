// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services
{
  using System;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  public sealed class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
      _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
    }

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
