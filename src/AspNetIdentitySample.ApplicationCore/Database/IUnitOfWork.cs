// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Database
{
  /// <summary>Provides a simple API to the database.</summary>
  public interface IUnitOfWork
  {
    public IUserRepository Users { get; }

    public IRoleRepository Roles { get; }

    public Task CommitAsync(CancellationToken cancellationToken);
  }
}
