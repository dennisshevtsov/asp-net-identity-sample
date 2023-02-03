﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using System.Security.Claims;
  using Microsoft.AspNetCore.Identity;

  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class UserControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private UserController _userController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userController = new UserController(UserManagerMock.Object);
    }

    [TestMethod]
    public async Task Get_Should_Return_View_Result()
    {
      var userEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
      };

      UserManagerMock.Setup(service => service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Get(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserController.ViewName, viewResult.ViewName);

      var model = viewResult.Model as UserViewModel;

      Assert.IsNotNull(model);
      Assert.AreEqual(userEntity.Email, model.Email);
      Assert.AreEqual(userEntity.Name, model.Name);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result()
    {
      _userController.ControllerContext.ModelState.AddModelError("test", "test");

      var vm = new UserViewModel();

      var actionResult = await _userController.Post(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserController.ViewName, viewResult.ViewName);
      Assert.AreEqual(vm, viewResult.Model);

      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Create_New_User()
    {
      UserManagerMock.Setup(service => service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(default(UserEntity))
                     .Verifiable();

      UserManagerMock.Setup(service => service.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                     .Returns(Task.FromResult(IdentityResult.Success))
                     .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Post(vm);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Update_User()
    {
      var userEntity = new UserEntity();

      UserManagerMock.Setup(service => service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      UserManagerMock.Setup(service => service.UpdateAsync(It.IsAny<UserEntity>()))
                     .Returns(Task.FromResult(IdentityResult.Success))
                     .Verifiable();

      var email = Guid.NewGuid().ToString();
      var name = Guid.NewGuid().ToString();

      var vm = new UserViewModel
      {
        Email = email,
        Name = name,
      };

      var actionResult = await _userController.Post(vm);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      Assert.AreEqual(email, userEntity.Email);
      Assert.AreEqual(name, userEntity.Name);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
