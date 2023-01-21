// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using AspNetIdentitySample.ApplicationCore.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class DbContextTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      var scope = new ServiceCollection().SetUpDatabase(configuration)
                                         .BuildServiceProvider()
                                         .CreateScope();

      _disposable = scope;
      _dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

      _dbContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Populate_Id_And_UserId_For_User()
    {
      var controlUserEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

      var controlUserEntityEntry = _dbContext.Add(controlUserEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      controlUserEntityEntry.State = EntityState.Detached;

      Assert.IsTrue(controlUserEntity.Id != default);
      Assert.IsTrue(controlUserEntity.Id == controlUserEntity.UserId);

      var actualUserEntity =
        await _dbContext.Set<UserEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(controlUserEntity.UserId.ToString())
                        .Where(entity => entity.Id == controlUserEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(controlUserEntity.UserId, actualUserEntity.UserId);
      Assert.AreEqual(controlUserEntity.Email, actualUserEntity.Email);
      Assert.AreEqual(controlUserEntity.Name, actualUserEntity.Name);
      Assert.AreEqual(controlUserEntity.PasswordHash, actualUserEntity.PasswordHash);
    }
  }
}
