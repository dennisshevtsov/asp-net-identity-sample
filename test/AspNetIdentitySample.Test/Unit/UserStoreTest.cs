﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Moq;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.Stores;
  
  [TestClass]
  public sealed class UserStoreTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;

    private UserStore _userStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;
      _userRepositoryMock = new Mock<IUserRepository>();
      _userStore = new UserStore(_userRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetUserIdAsync_Should_Return_Id()
    {
      var controlUserId = Guid.NewGuid();
      var userEntity = new UserEntity
      {
        Id = controlUserId,
      };

      var actualUserIdString =
        await _userStore.GetUserIdAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(actualUserIdString);

      Guid actualUserId;
      Assert.IsTrue(Guid.TryParse(actualUserIdString, out actualUserId));

      Assert.AreEqual(controlUserId, actualUserId);
    }
  }
}