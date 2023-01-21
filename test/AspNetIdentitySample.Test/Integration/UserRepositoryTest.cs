// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.ApplicationCore.Entities;

  [TestClass]
  public sealed class UserRepositoryTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;

    private IUserRepository _userRepository;
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
      _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

      _dbContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(
          controlUserEntity.Email!.ToUpper(), _cancellationToken);

      Assert.IsNotNull(actualUserEntity);

      Assert.AreEqual(controlUserEntity.Email, actualUserEntity.Email);
      Assert.AreEqual(controlUserEntity.Name, actualUserEntity.Name);
      Assert.AreEqual(controlUserEntity.PasswordHash, actualUserEntity.PasswordHash);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Untracking_User()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(
          controlUserEntity.Email!.ToUpper(), _cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(EntityState.Detached, _dbContext.Entry(actualUserEntity).State);
    }

    private async Task<UserEntity> CreateTestUserAsync()
    {
      var controlUserEntity = new UserEntity
      {
        Email = $"test${Guid.NewGuid()}@example.com",
        Name = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

      var controlUserEntityEntry = _dbContext.Add(controlUserEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      controlUserEntityEntry.State = EntityState.Detached;

      return controlUserEntity;
    }
  }
}
