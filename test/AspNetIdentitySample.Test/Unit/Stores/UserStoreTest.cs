// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Stores.Test
{
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class UserStoreTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IUserRoleRepository> _userRoleRepositoryMock;

    private UserStore _userStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userRepositoryMock = new Mock<IUserRepository>();
      _userRoleRepositoryMock = new Mock<IUserRoleRepository>();

      _userStore = new UserStore(
        _userRepositoryMock.Object,
        _userRoleRepositoryMock.Object);
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

      _userRepositoryMock.VerifyNoOtherCalls();
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

      _userRepositoryMock.VerifyNoOtherCalls();
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

    [TestMethod]
    public async Task GetPasswordHashAsync_Should_Return_Password_Hash()
    {
      var controlPasswordHash = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        PasswordHash = controlPasswordHash,
      };

      var actualPasswordHash =
        await _userStore.GetPasswordHashAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(actualPasswordHash);
      Assert.AreEqual(controlPasswordHash, actualPasswordHash);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetRolesAsync_Should_Return_Role_Collection_For_User()
    {
      var controlUserRoleEntity = new UserRoleEntity
      {
        UserId = Guid.NewGuid(),
        RoleName = Guid.NewGuid().ToString(),
      };
      var controlUserRoleEntityCollection = new List<UserRoleEntity>
      {
        controlUserRoleEntity,
      };

      _userRoleRepositoryMock.Setup(repository => repository.GetRolesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlUserRoleEntityCollection)
                             .Verifiable();

      var userEntity = new UserEntity();
      var roleCollection = await _userStore.GetRolesAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(roleCollection);
      Assert.AreEqual(controlUserRoleEntityCollection.Count, roleCollection.Count);
      Assert.IsTrue(controlUserRoleEntityCollection.All(entity => roleCollection.Contains(entity.RoleName!)));

      _userRoleRepositoryMock.Verify(repository => repository.GetRolesAsync(userEntity, _cancellationToken));
      _userRoleRepositoryMock.VerifyNoOtherCalls();

      _userRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
