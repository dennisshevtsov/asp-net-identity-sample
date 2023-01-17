// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Database
{
  using AspNetIdentitySample.ApplicationCore.Database;

  /// <summary>Provides a simple API to the database.</summary>
  public sealed class UnitOfWork : IUnitOfWork
  {
    public IUserRepository Users => throw new NotImplementedException();

    public IRoleRepository Roles => throw new NotImplementedException();

    public Task CommitAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
  }
}
