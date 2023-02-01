// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit.Controllers
{
  using System.Threading;

  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Services;
  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Entities;

  [TestClass]
  public sealed class UserControllerTest
  {
#pragma warning disable CS8618
    private Mock<IUserService> _userServiceMock;

    private UserController _userController;
#pragma warning restore CS8618

    private CancellationToken _cancellationToken;

    [TestInitialize]
    public void Initialize()
    {
      _userServiceMock = new Mock<IUserService>();
      _userController = new UserController(_userServiceMock.Object);
      _cancellationToken = CancellationToken.None;
    }

    [TestMethod]
    public async Task Get_Should_Return_View_Result()
    {
      var userEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
      };

      _userServiceMock.Setup(service => service.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(userEntity)
                      .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Get(vm, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserController.ViewName, viewResult.ViewName);

      var model = viewResult.Model as UserViewModel;

      Assert.IsNotNull(model);
      Assert.AreEqual(userEntity.Email, model.Email);
      Assert.AreEqual(userEntity.Name, model.Name);

      _userServiceMock.Verify(service => service.GetUserAsync(vm, _cancellationToken));
      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result()
    {
      _userController.ControllerContext.ModelState.AddModelError("test", "test");

      var vm = new UserViewModel();

      var actionResult = await _userController.Post(vm, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserController.ViewName, viewResult.ViewName);
      Assert.AreEqual(vm, viewResult.Model);

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Create_New_User()
    {
      _userServiceMock.Setup(service => service.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(default(UserEntity))
                      .Verifiable();

      _userServiceMock.Setup(service => service.AddUserAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask)
                      .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Post(vm, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      _userServiceMock.Verify();
      _userServiceMock.VerifyNoOtherCalls();
    }
  }
}
