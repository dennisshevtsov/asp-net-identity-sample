// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class SignOutControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private SignOutController _signOutController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _signOutController = new SignOutController(SignInManagerMock.Object);
      _signOutController.ControllerContext = new ControllerContext();
    }

    [TestMethod]
    public void Get_Should_Return_Action_Result_With_View_Name()
    {
      var actionResult = _signOutController.Get(new SignOutAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignOutController.ViewName, ((ViewResult)actionResult).ViewName);

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_Local_Redirect_Result()
    {
      SignInManagerMock.Setup(manager => manager.SignOutAsync())
                       .Returns(Task.CompletedTask)
                       .Verifiable();

      var vm = new SignOutAccountViewModel
      {
        ReturnUrl = Guid.NewGuid().ToString(),
      };

      var actionResult = await _signOutController.Post(vm);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(LocalRedirectResult));
      Assert.AreEqual(vm.ReturnUrl, ((LocalRedirectResult)actionResult).Url);

      SignInManagerMock.Verify(manager => manager.SignOutAsync());

      SignInManagerMock.VerifySet(manager => manager.Logger = SignInManagerLoggerMock.Object);
      SignInManagerMock.VerifyNoOtherCalls();
    }
  }
}
