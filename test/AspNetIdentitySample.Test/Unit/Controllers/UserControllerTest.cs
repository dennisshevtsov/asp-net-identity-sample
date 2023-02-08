// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using System.Security.Claims;
  
  using AutoMapper;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.WebApplication.Mapping;

  [TestClass]
  public sealed class UserControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private UserController _userController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _mapperMock = new Mock<IMapper>();

      _userController = new UserController(_mapperMock.Object, UserManagerMock.Object);
    }

    [TestMethod]
    public async Task Get_Should_Return_View_Result()
    {
      var principal = new ClaimsPrincipal();

      _mapperMock.Setup(mapper => mapper.Map<ClaimsPrincipal>(It.IsAny<IUserIdentity>()))
                 .Returns(principal)
                 .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map(It.IsAny<UserEntity>(), It.IsAny<UserViewModel>()))
                 .Returns(new UserViewModel())
                 .Verifiable();

      var userEntity = new UserEntity();

      UserManagerMock.Setup(service => service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Get(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(UserController.ViewName, viewResult.ViewName);
      Assert.AreEqual(vm, viewResult.Model);

      _mapperMock.Verify(mapper => mapper.Map<ClaimsPrincipal>(vm));
      _mapperMock.Verify(mapper => mapper.Map(userEntity, vm));
      _mapperMock.VerifyNoOtherCalls();

      UserManagerMock.Verify(manager => manager.GetUserAsync(principal));
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

      _mapperMock.VerifyNoOtherCalls();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Update_User()
    {
      var principal = new ClaimsPrincipal();

      _mapperMock.Setup(mapper => mapper.Map<ClaimsPrincipal>(It.IsAny<IUserIdentity>()))
                 .Returns(principal)
                 .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map(It.IsAny<UserViewModel>(), It.IsAny<UserEntity>()))
                 .Returns(new UserEntity())
                 .Verifiable();

      var userEntity = new UserEntity();

      UserManagerMock.Setup(service => service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      UserManagerMock.Setup(service => service.UpdateAsync(It.IsAny<UserEntity>()))
                     .Returns(Task.FromResult(IdentityResult.Success))
                     .Verifiable();

      var vm = new UserViewModel();

      var actionResult = await _userController.Post(vm);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      _mapperMock.Verify(mapper => mapper.Map<ClaimsPrincipal>(vm));
      _mapperMock.Verify(mapper => mapper.Map(vm, userEntity));

      UserManagerMock.Verify(manager => manager.GetUserAsync(principal));
      UserManagerMock.Verify(manager => manager.UpdateAsync(userEntity));
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
