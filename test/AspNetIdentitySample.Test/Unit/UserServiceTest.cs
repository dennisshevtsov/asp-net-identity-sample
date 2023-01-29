﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
    public async Task GetUsersAsync_Should_Get_Users_With_Roles()
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
  }
}
