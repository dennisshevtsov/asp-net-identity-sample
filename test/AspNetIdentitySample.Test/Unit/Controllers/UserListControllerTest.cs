// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.ViewModels;

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

    [TestMethod]
    public async Task Get_Should_Return_View_Result_With_View_Model()
    {
      var userId = Guid.NewGuid();
      var userEntityCollection = new List<UserEntity>
      {
        new UserEntity
        {
          Id = userId,
          UserId = userId,
          Email = Guid.NewGuid().ToString(),
          Name = Guid.NewGuid().ToString(),
        },
      };

      _userServiceMock.Setup(service => service.GetUsersAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(userEntityCollection)
                      .Verifiable();

      var vm = new UserListViewModel();

      var actionResult = await _userListController.Get(vm, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserListController.ViewName, viewResult.ViewName);

      var model = viewResult.Model as UserListViewModel;

      Assert.IsNotNull(model);
      Assert.AreEqual(userEntityCollection.Count, model.Users.Count);

      Assert.AreEqual(userEntityCollection[0].UserId, model.Users[0].UserId);
      Assert.AreEqual(userEntityCollection[0].Email, model.Users[0].Email);
      Assert.AreEqual(userEntityCollection[0].Name, model.Users[0].Name);

      _userServiceMock.Verify(service => service.GetUsersAsync(_cancellationToken));
      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Delete_Should_Delete_User()
    {
      _userServiceMock.Setup(service => service.DeleteUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask)
                      .Verifiable();

      var vm = new DeleteUserViewModel();

      var actionResult = await _userListController.Delete(vm, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserListController.Get), redirectResult.ActionName);

      _userServiceMock.Verify(service => service.DeleteUserAsync(vm, _cancellationToken));
      _userServiceMock.VerifyNoOtherCalls();
    }
  }
}
