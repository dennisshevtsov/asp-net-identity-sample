// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public abstract class IntegrationTestBase
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IServiceScope _scope;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      _scope = new ServiceCollection().SetUpDatabase(configuration)
                                      .BuildServiceProvider()
                                      .CreateScope();

      _dbContext = _scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();

      InitializeInternal();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database.EnsureDeleted();
      _scope?.Dispose();
    }

    protected CancellationToken Token { get => _cancellationToken; }

    protected IServiceScope Scope { get => _scope; }

    protected DbContext DbContext { get => _dbContext; }

    protected virtual void InitializeInternal()
    {
    }
  }
}
