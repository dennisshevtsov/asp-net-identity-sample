// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.ApplicationCore.Services.Test
{
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;

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
    public async Task GetRolesAsync_Should_Return_Roles()
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

      var userRoleCollection = await _userRoleService.GetRolesAsync(userIdentity, _cancellationToken);

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
  }
}
