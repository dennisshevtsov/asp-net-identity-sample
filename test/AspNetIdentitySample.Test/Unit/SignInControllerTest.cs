// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class SignInControllerTest : ControllerTestBase
  {
#pragma warning disable CS8618
    private SignInController _signInController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _signInController = new SignInController(SignInManagerMock.Object);
    }

    [TestMethod]
    public void Get_Should_Clear_Model_Sate()
    {
      _signInController.ControllerContext.ModelState.AddModelError("test", "test");

      _signInController.Get(new SignInAccountViewModel());

      Assert.IsTrue(_signInController.ControllerContext.ModelState.IsValid);

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Get_Should_Return_Action_Result_With_View_Name()
    {
      var actionResult = _signInController.Get(new SignInAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignInController.ViewName, ((ViewResult)actionResult).ViewName);

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result_If_Model_State_Is_Invalid()
    {
      _signInController.ControllerContext.ModelState.AddModelError("test", "test");

      var actionResult = await _signInController.Post(new SignInAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignInController.ViewName, ((ViewResult)actionResult).ViewName);

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result_If_Credentials_Are_Invalid()
    {
      SignInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                       .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed)
                       .Verifiable();

      var vm = new SignInAccountViewModel
      {
        Email = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ReturnUrl = Guid.NewGuid().ToString(),
      };

      var actionResult = await _signInController.Post(vm);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignInController.ViewName, ((ViewResult)actionResult).ViewName);

      Assert.IsFalse(_signInController.ModelState.IsValid);
      Assert.IsTrue(_signInController.ModelState.ContainsKey(nameof(SignInAccountViewModel.Email)));
      Assert.AreEqual(SignInController.InvalidCredentialsErrorMessage, _signInController.ModelState[nameof(SignInAccountViewModel.Email)]!.Errors[0].ErrorMessage);

      SignInManagerMock.Verify(manager => manager.PasswordSignInAsync(vm.Email, vm.Password, false, false));

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_Local_Redirect_Result()
    {
      SignInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                       .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success)
                       .Verifiable();

      var vm = new SignInAccountViewModel
      {
        Email = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ReturnUrl = Guid.NewGuid().ToString(),
      };

      var actionResult = await _signInController.Post(vm);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(LocalRedirectResult));
      Assert.AreEqual(vm.ReturnUrl, ((LocalRedirectResult)actionResult).Url);

      Assert.IsTrue(_signInController.ModelState.IsValid);

      SignInManagerMock.Verify(manager => manager.PasswordSignInAsync(vm.Email, vm.Password, false, false));

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }
  }
}
