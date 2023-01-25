// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using System.Threading;

  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class SignOutControllerTest : ControllerTestBase
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;

    private SignOutController _signOutController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _cancellationToken = CancellationToken.None;

      _userRepositoryMock = new Mock<IUserRepository>();

      _signOutController = new SignOutController(
        SignInManagerMock.Object,
        _userRepositoryMock.Object);
      _signOutController.ControllerContext = new ControllerContext();
    }

    [TestMethod]
    public async Task Get_Should_Return_Action_Result_With_View_Name()
    {
      var actionResult = await _signOutController.Get(new SignOutAccountViewModel(), _cancellationToken);

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
