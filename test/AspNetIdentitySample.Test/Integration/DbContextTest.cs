// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class DbContextTest : IntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Populate_Id_And_UserId_For_User()
    {
      var controlUserEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

      var controlUserEntityEntry = DbContext.Add(controlUserEntity);

      await DbContext.SaveChangesAsync(Token);

      controlUserEntityEntry.State = EntityState.Detached;

      Assert.IsTrue(controlUserEntity.Id != default);
      Assert.IsTrue(controlUserEntity.Id == controlUserEntity.UserId);

      var actualUserEntity =
        await DbContext.Set<UserEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(controlUserEntity.UserId.ToString())
                        .Where(entity => entity.Id == controlUserEntity.Id)
                        .FirstOrDefaultAsync(Token);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(controlUserEntity.UserId, actualUserEntity.UserId);
      Assert.AreEqual(controlUserEntity.Email, actualUserEntity.Email);
      Assert.AreEqual(controlUserEntity.Name, actualUserEntity.Name);
      Assert.AreEqual(controlUserEntity.PasswordHash, actualUserEntity.PasswordHash);
    }
  }
}
