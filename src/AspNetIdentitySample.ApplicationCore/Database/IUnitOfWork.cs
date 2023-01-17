// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Database
{
  /// <summary>Provides a simple API to the database.</summary>
  public interface IUnitOfWork
  {
    /// <summary>Gets/sets an object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
    public IRoleRepository Roles { get; }

    /// <summary>Gets/sets an object that provides a simple API to a collection of <see cref="AspNetIdentitySample.ApplicationCore.Entities.UserEntity"/> in the database.</summary>
    public IUserRepository Users { get; }

    /// <summary>Commits all tracking changes to the database.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CommitAsync(CancellationToken cancellationToken);
  }
}
