// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using System.Threading;

  using Moq;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.Infrastructure.Repositories;

  [TestClass]
  public sealed class UserServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IUserRoleRepository> _userRoleRepositoryMock;

    private UserService _userService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userRepositoryMock = new Mock<IUserRepository>();
      _userRoleRepositoryMock = new Mock<IUserRoleRepository>();

      _userService = new UserService(
        _userRepositoryMock.Object,
        _userRoleRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetUsersAsync_Should_Return_Users_With_Roles()
    {
      var controlUserEntity = new UserEntity
      {
        UserId = Guid.NewGuid(),
      };

      var controlUserEntityCollection = new List<UserEntity>
      {
        new UserEntity(),
        controlUserEntity,
      };

      _userRepositoryMock.Setup(repository => repository.GetUsersAsync(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntityCollection)
                         .Verifiable();

      var controlUserRoleEntityCollection = new List<UserRoleEntity>
      {
        new UserRoleEntity(),
      };

      var controlUserRoleEntityDictionary = new Dictionary<IUserIdentity, List<UserRoleEntity>>(new UserRoleRepository.UserIdentityComparer())
      {
        { controlUserEntity, controlUserRoleEntityCollection },
      };

      _userRoleRepositoryMock.Setup(repository => repository.GetRolesAsync(It.IsAny<IEnumerable<IUserIdentity>>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlUserRoleEntityDictionary)
                             .Verifiable();

      var actualUserEntityCollection = await _userService.GetUsersAsync(_cancellationToken);

      Assert.IsNotNull(actualUserEntityCollection);
      Assert.AreEqual(controlUserEntityCollection, actualUserEntityCollection);
      Assert.AreEqual(controlUserEntity.Roles, controlUserRoleEntityCollection);

      _userRepositoryMock.Verify(repository => repository.GetUsersAsync(_cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userRoleRepositoryMock.Verify(repository => repository.GetRolesAsync(controlUserEntityCollection, _cancellationToken));
      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null()
    {
      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(default(UserEntity))
                         .Verifiable();

      var identity = new UserEntity();

      var actualUserEntity = await _userService.GetUserAsync(identity, _cancellationToken);

      Assert.IsNull(actualUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(identity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_With_Roles()
    {
      var controlUserEntity = new UserEntity
      {
        UserId = Guid.NewGuid(),
      };

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var controlUserRoleEntityCollection = new List<UserRoleEntity>();

      _userRoleRepositoryMock.Setup(repository => repository.GetRolesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlUserRoleEntityCollection)
                             .Verifiable();

      var identity = controlUserEntity;

      var actualUserEntity = await _userService.GetUserAsync(identity, _cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(actualUserEntity, controlUserEntity);
      Assert.AreEqual(actualUserEntity.Roles, controlUserRoleEntityCollection);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(identity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userRoleRepositoryMock.Verify(repository => repository.GetRolesAsync(identity, _cancellationToken));
      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddUserAsync_Should_Create_New_User()
    {
      _userRepositoryMock.Setup(repository => repository.AddUserAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var userEntity = new UserEntity();

      await _userService.AddUserAsync(userEntity, _cancellationToken);

      _userRepositoryMock.Verify(repository => repository.AddUserAsync(userEntity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
