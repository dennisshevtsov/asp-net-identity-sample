// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using AutoMapper;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class SignUpControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private SignUpController _signUpController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _mapperMock = new Mock<IMapper>();

      _signUpController = new SignUpController(
        _mapperMock.Object, UserManagerMock.Object);
    }

    [TestMethod]
    public void Get_Should_Return_View_Result()
    {
      var vm = new SignUpAccountViewModel();

      var actionResult = _signUpController.Get(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(SignUpController.ViewName, viewResult.ViewName);
      Assert.AreEqual(vm, viewResult.Model);

      _mapperMock.VerifyNoOtherCalls();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result()
    {
      _signUpController.ControllerContext.ModelState.AddModelError("test", "test");

      var vm = new SignUpAccountViewModel();

      var actionResult = await _signUpController.Post(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(SignUpController.ViewName, viewResult.ViewName);
      Assert.AreEqual(vm, viewResult.Model);

      _mapperMock.VerifyNoOtherCalls();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Create_New_User()
    {
      var userEntity = new UserEntity();

      _mapperMock.Setup(mapper => mapper.Map<UserEntity>(It.IsAny<SignUpAccountViewModel>()))
                 .Returns(userEntity)
                 .Verifiable();

      UserManagerMock.Setup(service => service.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                     .Returns(Task.FromResult(IdentityResult.Success))
                     .Verifiable();

      var password = Guid.NewGuid().ToString();
      var vm = new SignUpAccountViewModel
      {
        Password = password,
      };

      var actionResult = await _signUpController.Post(vm);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      _mapperMock.Verify(mapper => mapper.Map<UserEntity>(vm));
      _mapperMock.VerifyNoOtherCalls();

      UserManagerMock.Verify(manager => manager.CreateAsync(userEntity, password));
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
