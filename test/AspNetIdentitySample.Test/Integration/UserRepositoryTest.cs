// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class UserRepositoryTest : IntegrationTestBase
  {
#pragma warning disable CS8618
    private IUserRepository _userRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userRepository = Scope.ServiceProvider.GetRequiredService<IUserRepository>();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_By_Id()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(controlUserEntity, Token);

      Assert.IsNotNull(actualUserEntity);

      Assert.AreEqual(controlUserEntity.Email, actualUserEntity.Email);
      Assert.AreEqual(controlUserEntity.Name, actualUserEntity.Name);
      Assert.AreEqual(controlUserEntity.PasswordHash, actualUserEntity.PasswordHash);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Untracking_User_By_Id()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(controlUserEntity, Token);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(EntityState.Detached, DbContext.Entry(actualUserEntity).State);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_By_Id()
    {
      var inexistingUserId = Guid.NewGuid();
      var inexistingUserEntity = new UserEntity
      {
        Id = inexistingUserId,
        UserId = inexistingUserId,
      };

      var actualUserEntity = await _userRepository.GetUserAsync(inexistingUserEntity, Token);

      Assert.IsNull(actualUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_By_Email()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(
          controlUserEntity.Email!.ToUpper(), Token);

      Assert.IsNotNull(actualUserEntity);

      Assert.AreEqual(controlUserEntity.Email, actualUserEntity.Email);
      Assert.AreEqual(controlUserEntity.Name, actualUserEntity.Name);
      Assert.AreEqual(controlUserEntity.PasswordHash, actualUserEntity.PasswordHash);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Untracking_User_By_Email()
    {
      var controlUserEntity = await CreateTestUserAsync();

      var actualUserEntity =
        await _userRepository.GetUserAsync(
          controlUserEntity.Email!.ToUpper(), Token);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(EntityState.Detached, DbContext.Entry(actualUserEntity).State);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_By_Email()
    {
      var actualUserEntity =
        await _userRepository.GetUserAsync(Guid.NewGuid().ToString(), Token);

      Assert.IsNull(actualUserEntity);
    }

    [TestMethod]
    public async Task AddUserAsync_Should_Return_Create_User()
    {
      var creatingUserEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
      };

      await _userRepository.AddUserAsync(creatingUserEntity, Token);

      Assert.AreNotEqual(default, creatingUserEntity.Id);
      Assert.AreEqual(creatingUserEntity.Id, creatingUserEntity.UserId);
      Assert.AreEqual(EntityState.Detached, DbContext.Entry(creatingUserEntity).State);

      var createdUserEntity =
        await DbContext.Set<UserEntity>()
                       .AsNoTracking()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.Id == creatingUserEntity.Id)
                       .FirstOrDefaultAsync(Token);

      Assert.IsNotNull(createdUserEntity);
      Assert.AreEqual(creatingUserEntity.Email, createdUserEntity.Email);
      Assert.AreEqual(creatingUserEntity.Name, createdUserEntity.Name);
    }

    [TestMethod]
    public async Task UpdateUserAsync_Should_Return_Save_User()
    {
      var updatingUserEntity = await CreateTestUserAsync();

      updatingUserEntity.Email = Guid.NewGuid().ToString();
      updatingUserEntity.Name = Guid.NewGuid().ToString();

      await _userRepository.UpdateUserAsync(updatingUserEntity, Token);

      Assert.AreEqual(EntityState.Detached, DbContext.Entry(updatingUserEntity).State);

      var updatedUserEntity =
        await DbContext.Set<UserEntity>()
                       .AsNoTracking()
                       .WithPartitionKey(updatingUserEntity.UserId.ToString())
                       .Where(entity => entity.Id == updatingUserEntity.Id)
                       .FirstOrDefaultAsync(Token);

      Assert.IsNotNull(updatedUserEntity);
      Assert.AreEqual(updatingUserEntity.Email, updatedUserEntity.Email);
      Assert.AreEqual(updatingUserEntity.Name, updatedUserEntity.Name);
    }

    [TestMethod]
    public async Task DeleteUserAsync_Should_Return_Delete_User()
    {
      var deletingUserEntity = await CreateTestUserAsync();

      await _userRepository.DeleteUserAsync(deletingUserEntity, Token);

      Assert.AreEqual(EntityState.Detached, DbContext.Entry(deletingUserEntity).State);

      var deletedUserEntity =
        await DbContext.Set<UserEntity>()
                       .AsNoTracking()
                       .WithPartitionKey(deletingUserEntity.UserId.ToString())
                       .Where(entity => entity.Id == deletingUserEntity.Id)
                       .FirstOrDefaultAsync(Token);

      Assert.IsNull(deletedUserEntity);
    }

    private async Task<UserEntity> CreateTestUserAsync()
    {
      var controlUserEntity = new UserEntity
      {
        Email = $"test${Guid.NewGuid()}@example.com",
        Name = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

      var controlUserEntityEntry = DbContext.Add(controlUserEntity);

      await DbContext.SaveChangesAsync(Token);

      controlUserEntityEntry.State = EntityState.Detached;

      return controlUserEntity;
    }
  }
}
