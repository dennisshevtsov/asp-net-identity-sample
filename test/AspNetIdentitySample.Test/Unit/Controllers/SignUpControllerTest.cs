// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.WebApplication.ViewModels;
  using System.Security.Claims;

  [TestClass]
  public sealed class SignUpControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private SignUpController _signUpController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _signUpController = new SignUpController(UserManagerMock.Object);
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

      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Create_New_User()
    {
      UserManagerMock.Setup(service => service.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                     .Returns(Task.FromResult(IdentityResult.Success))
                     .Verifiable();

      var vm = new SignUpAccountViewModel();

      var actionResult = await _signUpController.Post(vm);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(UserController.Get), redirectResult.ActionName);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
