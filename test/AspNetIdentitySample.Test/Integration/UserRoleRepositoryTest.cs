// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using System;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.ApplicationCore.Repositories;

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
    public async Task GetRolesAsync_Should_Return_Roles_By_User_Id()
    {
      var userId = Guid.NewGuid();
      var controlUserRoleEntityCollection = await CreateTestUserRolesAsync(userId);

      var actualUserRoleEntityCollection =
        await _userRoleRepository.GetRolesAsync(
          new UserEntity { UserId = userId }, Token);

      Assert.IsNotNull(actualUserRoleEntityCollection);

      Assert.AreEqual(controlUserRoleEntityCollection.Count, actualUserRoleEntityCollection.Count);

      Assert.AreEqual(userId, actualUserRoleEntityCollection[0].UserId);
      Assert.AreEqual(controlUserRoleEntityCollection[0].RoleName, actualUserRoleEntityCollection[0].RoleName);

      Assert.AreEqual(userId, actualUserRoleEntityCollection[1].UserId);
      Assert.AreEqual(controlUserRoleEntityCollection[1].RoleName, actualUserRoleEntityCollection[1].RoleName);
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
      var userRoleEntityCollection =  new List<UserRoleEntity>
      {
        await CreateTestUserRoleAsync(userId),
        await CreateTestUserRoleAsync(userId),
      };

      return userRoleEntityCollection.OrderBy(entity => entity.RoleName)
                                     .ToList();
    }
  }
}
