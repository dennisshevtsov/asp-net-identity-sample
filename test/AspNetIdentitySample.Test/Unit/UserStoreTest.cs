// Copyright (c) Dennis Shevtsov. All rights reserved.
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

    [TestMethod]
    public async Task GetUserNameAsync_Should_Return_Email()
    {
      var controlEmail = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        Email = controlEmail,
      };

      var actualEmail =
        await _userStore.GetUserNameAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(actualEmail);
      Assert.AreEqual(controlEmail, actualEmail);
    }

    [TestMethod]
    public async Task FindByNameAsync_Should_Get_User_By_Email()
    {
      var controlUserEntity = new UserEntity();

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var username = Guid.NewGuid().ToString();

      var actualUserEntity =
        await _userStore.FindByNameAsync(username, _cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(controlUserEntity, actualUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(username, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
