// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit.Controllers
{
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.Controllers;

  [TestClass]
  public sealed class UserListControllerTest
  {
#pragma warning disable CS8618
    private Mock<IUserService> _userServiceMock;

    private UserListController _userListController;
#pragma warning restore CS8618

    private CancellationToken _cancellationToken;

    [TestInitialize]
    public void Initialize()
    {
      _userServiceMock = new Mock<IUserService>();
      _userListController = new UserListController(_userServiceMock.Object);
      _cancellationToken = CancellationToken.None;
    }
  }
}
