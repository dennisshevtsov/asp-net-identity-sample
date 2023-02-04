// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute queries and commands with the <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/>.</summary>
  public sealed class UserRoleService : IUserRoleService
  {
    private readonly IUserRoleRepository _userRoleRepository;

    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.ApplicationCore.Services.UserRoleService"/> class.</summary>
    /// <param name="userRoleRepository">An object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserRoleEntity"/> in the database.</param>
    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
      _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
    }

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<List<UserRoleEntity>> GetRolesAsync(IUserIdentity identity, CancellationToken cancellationToken)
      => _userRoleRepository.GetRolesAsync(identity, cancellationToken);

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identities">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<Dictionary<IUserIdentity, List<UserRoleEntity>>> GetRolesAsync(IEnumerable<IUserIdentity> identities, CancellationToken cancellationToken)
    {
      var userRoleEntityCollection = await _userRoleRepository.GetRolesAsync(identities, cancellationToken);
      var userRoleEntityDictionary = new Dictionary<IUserIdentity, List<UserRoleEntity>>(new UserIdentityComparer());

      foreach(var identity in identities)
      {
        userRoleEntityDictionary.TryAdd(identity, new List<UserRoleEntity>());
      }

      foreach (var userRoleEntity in userRoleEntityCollection)
      {
        userRoleEntityDictionary[userRoleEntity].Add(userRoleEntity);
      }

      return userRoleEntityDictionary;
    }

    /// <summary>Gets a collection of user and role relations.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<IList<string>> GetRoleNamesAsync(IUserIdentity identity, CancellationToken cancellationToken)
    {
      var userRoleEntityCollection =
        await _userRoleRepository.GetRolesAsync(identity, cancellationToken);

      return userRoleEntityCollection.Select(userRoleEntity => userRoleEntity.RoleName!)
                                     .ToList();
    }

    /// <summary>Deletes roles for a user.</summary>
    /// <param name="identity">An object that represents conditions to query a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task DeleteRolesAsync(IUserIdentity identity, CancellationToken cancellationToken)
    {
      var userRoleEntityCollection =
        await _userRoleRepository.GetRolesAsync(identity, cancellationToken);

      await _userRoleRepository.DeleteRolesAsync(userRoleEntityCollection, cancellationToken);
    }

    public sealed class UserIdentityComparer : IEqualityComparer<IUserIdentity>
    {
      public bool Equals(IUserIdentity? a, IUserIdentity? b)
      {
        if (a != null && b != null)
        {
          return a.UserId == b.UserId;
        }

        if (a == null && b == null)
        {
          return true;
        }

        return false;
      }

      public int GetHashCode(IUserIdentity identity) => identity.UserId.GetHashCode();
    }
  }
}
