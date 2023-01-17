// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Stores
{
  using System.Security.Claims;

  using Microsoft.AspNetCore.Identity;

  using AspNetIdentitySample.ApplicationCore.Entities;

  public sealed class UserStore
    : IUserStore<UserEntity>,
      IUserClaimStore<UserEntity>,
      IUserLoginStore<UserEntity>,
      IUserRoleStore<UserEntity>,
      IUserPasswordStore<UserEntity>,
      IUserSecurityStampStore<UserEntity>
  {
    #region Members of IUserStore

    /// <summary>
    /// Gets the user identifier for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose identifier should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user"/>.</returns>
    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the user name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the name for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the given <paramref name="userName" /> for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="userName">The user name to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetUserNameAsync(UserEntity user, string? userName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the normalized user name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose normalized name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the given normalized name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="normalizedName">The normalized name to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetNormalizedUserNameAsync(UserEntity user, string? normalizedName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Creates the specified <paramref name="user"/> in the user store.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the creation operation.</returns>
    public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Updates the specified <paramref name="user"/> in the user store.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the update operation.</returns>
    public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Deletes the specified <paramref name="user"/> from the user store.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the delete operation.</returns>
    public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Finds and returns a user, if any, who has the specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId"/> if it exists.
    /// </returns>
    public Task<UserEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Finds and returns a user, if any, who has the specified normalized user name.
    /// </summary>
    /// <param name="normalizedUserName">The normalized user name to search for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName"/> if it exists.
    /// </returns>
    public Task<UserEntity?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserClaimStore

    /// <summary>
    /// Gets a list of <see cref="Claim"/>s to be belonging to the specified <paramref name="user"/> as an asynchronous operation.
    /// </summary>
    /// <param name="user">The role whose claims to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <see cref="Claim"/>s.
    /// </returns>
    public Task<IList<Claim>> GetClaimsAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Add claims to a user as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user to add the claim to.</param>
    /// <param name="claims">The collection of <see cref="Claim"/>s to add.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Replaces the given <paramref name="claim"/> on the specified <paramref name="user"/> with the <paramref name="newClaim"/>
    /// </summary>
    /// <param name="user">The user to replace the claim on.</param>
    /// <param name="claim">The claim to replace.</param>
    /// <param name="newClaim">The new claim to replace the existing <paramref name="claim"/> with.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Removes the specified <paramref name="claims"/> from the given <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user to remove the specified <paramref name="claims"/> from.</param>
    /// <param name="claims">A collection of <see cref="Claim"/>s to remove.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns a list of users who contain the specified <see cref="Claim"/>.
    /// </summary>
    /// <param name="claim">The claim to look for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <typeparamref name="TUser"/> who
    /// contain the specified claim.
    /// </returns>
    public Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserLoginStore

    /// <summary>
    /// Adds an external <see cref="UserLoginInfo"/> to the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user to add the login to.</param>
    /// <param name="login">The external <see cref="UserLoginInfo"/> to add to the specified <paramref name="user"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task AddLoginAsync(UserEntity user, UserLoginInfo login, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Attempts to remove the provided login information from the specified <paramref name="user"/>.
    /// and returns a flag indicating whether the removal succeed or not.
    /// </summary>
    /// <param name="user">The user to remove the login information from.</param>
    /// <param name="loginProvider">The login provide whose information should be removed.</param>
    /// <param name="providerKey">The key given by the external login provider for the specified user.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task RemoveLoginAsync(UserEntity user, string loginProvider, string providerKey, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Retrieves the associated logins for the specified <param ref="user"/>.
    /// </summary>
    /// <param name="user">The user whose associated logins to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> for the asynchronous operation, containing a list of <see cref="UserLoginInfo"/> for the specified <paramref name="user"/>, if any.
    /// </returns>
    public Task<IList<UserLoginInfo>> GetLoginsAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Retrieves the user associated with the specified login provider and login provider key.
    /// </summary>
    /// <param name="loginProvider">The login provider who provided the <paramref name="providerKey"/>.</param>
    /// <param name="providerKey">The key provided by the <paramref name="loginProvider"/> to identify a user.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> for the asynchronous operation, containing the user, if any which matched the specified login provider and key.
    /// </returns>
    public Task<UserEntity?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserRoleStore

    /// <summary>
    /// Add the specified <paramref name="user"/> to the named role.
    /// </summary>
    /// <param name="user">The user to add to the named role.</param>
    /// <param name="roleName">The name of the role to add the user to.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task AddToRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Remove the specified <paramref name="user"/> from the named role.
    /// </summary>
    /// <param name="user">The user to remove the named role from.</param>
    /// <param name="roleName">The name of the role to remove.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task RemoveFromRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets a list of role names the specified <paramref name="user"/> belongs to.
    /// </summary>
    /// <param name="user">The user whose role names to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing a list of role names.</returns>
    public Task<IList<string>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns a flag indicating whether the specified <paramref name="user"/> is a member of the given named role.
    /// </summary>
    /// <param name="user">The user whose role membership should be checked.</param>
    /// <param name="roleName">The name of the role to be checked.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user"/> is
    /// a member of the named role.
    /// </returns>
    public Task<bool> IsInRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns a list of Users who are members of the named role.
    /// </summary>
    /// <param name="roleName">The name of the role whose membership should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing a list of users who are in the named role.
    /// </returns>
    public Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserPasswordStore

    /// <summary>
    /// Sets the password hash for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose password hash to set.</param>
    /// <param name="passwordHash">The password hash to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetPasswordHashAsync(UserEntity user, string? passwordHash, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the password hash for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose password hash to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets a flag indicating whether the specified <paramref name="user"/> has a password.
    /// </summary>
    /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user"/> has a password
    /// otherwise false.
    /// </returns>
    public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserSecurityStampStore

    /// <summary>
    /// Sets the provided security <paramref name="stamp"/> for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose security stamp should be set.</param>
    /// <param name="stamp">The security stamp to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetSecurityStampAsync(UserEntity user, string stamp, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Get the security stamp for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose security stamp should be set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetSecurityStampAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IDisposable

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose() { }

    #endregion
  }
}
