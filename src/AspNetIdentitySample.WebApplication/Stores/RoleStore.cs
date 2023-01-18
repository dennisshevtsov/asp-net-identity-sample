using AspNetIdentitySample.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentitySample.WebApplication.Stores
{
  public sealed class RoleStore : IRoleStore<RoleEntity>
  {
    /// <summary>
    /// Creates a new role in a store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role to create in the store.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
    public Task<IdentityResult> CreateAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Updates a role in a store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role to update in the store.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
    public Task<IdentityResult> UpdateAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Deletes a role from the store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role to delete from the store.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
    public Task<IdentityResult> DeleteAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the ID for a role from the store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role whose ID should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that contains the ID of the role.</returns>
    public Task<string> GetRoleIdAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the name of a role from the store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role whose name should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
    public Task<string?> GetRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the name of a role in the store as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role whose name should be set.</param>
    /// <param name="roleName">The name of the role.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetRoleNameAsync(RoleEntity role, string? roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Get a role's normalized name as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role whose normalized name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
    public Task<string?> GetNormalizedRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Set a role's normalized name as an asynchronous operation.
    /// </summary>
    /// <param name="role">The role whose normalized name should be set.</param>
    /// <param name="normalizedName">The normalized name to set</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetNormalizedRoleNameAsync(RoleEntity role, string? normalizedName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Finds the role who has the specified ID as an asynchronous operation.
    /// </summary>
    /// <param name="roleId">The role ID to look for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
    public Task<RoleEntity?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Finds the role who has the specified normalized name as an asynchronous operation.
    /// </summary>
    /// <param name="normalizedRoleName">The normalized role name to look for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
    public Task<RoleEntity?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose() { }
  }
}
