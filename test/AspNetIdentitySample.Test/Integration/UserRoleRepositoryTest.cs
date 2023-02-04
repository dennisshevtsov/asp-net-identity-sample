// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Infrastructure.Repositories.Test
{
  using System;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.Infrastructure.Test;

  [TestClass]
  public sealed class UserRoleRepositoryTest : IntegrationTestBase
  {
#pragma warning disable CS8618
    private IUserRoleRepository _userRoleRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userRoleRepository = Scope.ServiceProvider.GetRequiredService<IUserRoleRepository>();
    }

    [TestMethod]
    public async Task GetRolesAsync_Should_Return_Roles_Collection_By_User_Id()
    {
      var controlUserId = Guid.NewGuid();
      var controlUserRoleEntityCollection = await CreateTestUserRolesAsync(controlUserId);

      var actualUserRoleEntityCollection =
        await _userRoleRepository.GetRolesAsync(
          new UserEntity { UserId = controlUserId }, Token);

      Assert.IsNotNull(actualUserRoleEntityCollection);

      Assert.AreEqual(controlUserRoleEntityCollection.Count, actualUserRoleEntityCollection.Count);

      Assert.AreEqual(controlUserId, actualUserRoleEntityCollection[0].UserId);
      Assert.AreEqual(controlUserRoleEntityCollection[0].RoleName, actualUserRoleEntityCollection[0].RoleName);

      Assert.AreEqual(controlUserId, actualUserRoleEntityCollection[1].UserId);
      Assert.AreEqual(controlUserRoleEntityCollection[1].RoleName, actualUserRoleEntityCollection[1].RoleName);
    }

    [TestMethod]
    public async Task GetRolesAsync_Should_Return_Roles_For_User()
    {
      var controlUserEntityCollection = new[]
      {
        new UserEntity { UserId = Guid.NewGuid(), },
        new UserEntity { UserId = Guid.NewGuid(), },
        new UserEntity { UserId = Guid.NewGuid(), },
      };

      var controlUserRoleEntityCollection = new List<UserRoleEntity>();

      controlUserRoleEntityCollection.AddRange(
        await CreateTestUserRolesAsync(controlUserEntityCollection[0].UserId));
      controlUserRoleEntityCollection.AddRange(
        await CreateTestUserRolesAsync(controlUserEntityCollection[1].UserId));

      controlUserRoleEntityCollection =
        controlUserRoleEntityCollection.OrderBy(entity => entity.RoleName)
                                       .ToList();

      var actualUserRoleEntityCollection =
        await _userRoleRepository.GetRolesAsync(controlUserEntityCollection, Token);

      Assert.IsNotNull(actualUserRoleEntityCollection);
      Assert.AreEqual(controlUserRoleEntityCollection.Count, actualUserRoleEntityCollection.Count);

      for (int i = 0; i < controlUserRoleEntityCollection.Count; i++)
      {
        Assert.AreEqual(controlUserRoleEntityCollection[0].RoleName, actualUserRoleEntityCollection[0].RoleName);
      }
    }

    [TestMethod]
    public async Task DeleteRolesAsync_Should_Delete_Roles()
    {
      var controlUserId = Guid.NewGuid();
      var controlUserRoleEntityCollection = await CreateTestUserRolesAsync(controlUserId);

      await _userRoleRepository.DeleteRolesAsync(controlUserRoleEntityCollection, Token);

      foreach (var controlUserRoleEntity in controlUserRoleEntityCollection)
      {
        Assert.AreEqual(EntityState.Detached, DbContext.Entry(controlUserRoleEntity).State);
      }

      var actualUserRoleEntityCollection =
        await DbContext.Set<UserRoleEntity>()
                       .AsNoTracking()
                       .WithPartitionKey(controlUserId.ToString())
                       .ToArrayAsync(Token);

      Assert.IsNotNull(actualUserRoleEntityCollection);
      Assert.AreEqual(0, actualUserRoleEntityCollection.Length);
    }

    private async Task<UserRoleEntity> CreateTestUserRoleAsync(Guid userId)
    {
      var userRoleEntity = new UserRoleEntity
      {
        UserId = userId,
        RoleName = Guid.NewGuid().ToString(),
      };

      var userRoleEntityEntry = DbContext.Add(userRoleEntity);

      await DbContext.SaveChangesAsync(Token);

      userRoleEntityEntry.State = EntityState.Detached;

      return userRoleEntity;
    }

    private async Task<List<UserRoleEntity>> CreateTestUserRolesAsync(Guid userId)
    {
      var userRoleEntityCollection = new List<UserRoleEntity>
      {
        await CreateTestUserRoleAsync(userId),
        await CreateTestUserRoleAsync(userId),
      };

      return userRoleEntityCollection.OrderBy(entity => entity.RoleName)
                                     .ToList();
    }
  }
}
