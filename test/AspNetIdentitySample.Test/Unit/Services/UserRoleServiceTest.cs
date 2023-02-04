// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services.Test
{
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.Infrastructure.Repositories;
  using System.Threading;

  [TestClass]
  public sealed class UserRoleServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRoleRepository> _userRoleRepositoryMock;

    private UserRoleService _userRoleService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userRoleRepositoryMock = new Mock<IUserRoleRepository>();

      _userRoleService = new UserRoleService(_userRoleRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetRoleNamesAsync_Should_Return_Role_Name_Collection_For_User()
    {
      var userRoleEntityCollection = new List<UserRoleEntity>
      {
        new UserRoleEntity
        {
          RoleName = Guid.NewGuid().ToString(),
        },
        new UserRoleEntity
        {
          RoleName = Guid.NewGuid().ToString(),
        },
      };

      _userRoleRepositoryMock.Setup(repository => repository.GetRolesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(userRoleEntityCollection)
                             .Verifiable();

      var userIdentity = new UserEntity();

      var userRoleCollection = await _userRoleService.GetRoleNamesAsync(userIdentity, _cancellationToken);

      Assert.IsNotNull(userRoleCollection);
      Assert.AreEqual(userRoleEntityCollection.Count, userRoleCollection.Count);

      foreach (var userRoleEntity in userRoleEntityCollection)
      {
        Assert.IsTrue(userRoleCollection.Contains(userRoleEntity.RoleName!));
      }

      _userRoleRepositoryMock.Verify(repository => repository.GetRolesAsync(userIdentity, _cancellationToken));
      _userRoleRepositoryMock.VerifyNoOtherCalls();

      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteRolesAsync_Should_Delete_Roles_For_User()
    {
      var userRoleEntityCollection = new List<UserRoleEntity>();

      _userRoleRepositoryMock.Setup(repository => repository.GetRolesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(userRoleEntityCollection)
                             .Verifiable();

      _userRoleRepositoryMock.Setup(repository => repository.DeleteRolesAsync(It.IsAny<List<UserRoleEntity>>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.CompletedTask)
                             .Verifiable();

      var identity = new UserEntity();

      await _userRoleService.DeleteRolesAsync(identity, _cancellationToken);

      _userRoleRepositoryMock.Verify(repository => repository.GetRolesAsync(identity, _cancellationToken));
      _userRoleRepositoryMock.Verify(repository => repository.DeleteRolesAsync(userRoleEntityCollection, _cancellationToken));
      _userRoleRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
