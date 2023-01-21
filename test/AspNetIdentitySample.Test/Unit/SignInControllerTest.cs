// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using Moq;

  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class SignInControllerTest
  {
#pragma warning disable CS8618
    private Mock<ILogger<SignInManager<UserEntity>>> _signInManagerLoggerMock;
    private Mock<SignInManager<UserEntity>> _signInManagerMock;

    private SignInController _signInController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _signInManagerLoggerMock = new Mock<ILogger<SignInManager<UserEntity>>>();

      var userStoreMock = new Mock<IUserStore<UserEntity>>();
      var passwordHasherMock = new Mock<IPasswordHasher<UserEntity>>();
      var userValidatorMock = new Mock<IUserValidator<UserEntity>>();
      var passwordValidatorMock = new Mock<IPasswordValidator<UserEntity>>();
      var keyNormalizerMock = new Mock<ILookupNormalizer>();
      var errorsMock = new Mock<IdentityErrorDescriber>();
      var servicesMock = new Mock<IServiceProvider>();
      var contextAccessorMock = new Mock<IHttpContextAccessor>();
      var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
      var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
      var userManagerLoggerMock = new Mock<ILogger<UserManager<UserEntity>>>();
      var schemesMock = new Mock<IAuthenticationSchemeProvider>();
      var confirmationMock = new Mock<IUserConfirmation<UserEntity>>();

      var userManagerMock = new Mock<UserManager<UserEntity>>(
        userStoreMock.Object,
        optionsAccessorMock.Object,
        passwordHasherMock.Object,
        new[] { userValidatorMock.Object }.AsEnumerable(),
        new[] { passwordValidatorMock.Object }.AsEnumerable(),
        keyNormalizerMock.Object,
        errorsMock.Object,
        servicesMock.Object,
        userManagerLoggerMock.Object);

      _signInManagerMock = new Mock<SignInManager<UserEntity>>(
        userManagerMock.Object,
        contextAccessorMock.Object,
        claimsFactoryMock.Object,
        optionsAccessorMock.Object,
        _signInManagerLoggerMock.Object,
        schemesMock.Object,
        confirmationMock.Object);

      _signInController = new SignInController(_signInManagerMock.Object);
      _signInController.ControllerContext = new ControllerContext();
    }

    [TestMethod]
    public void Get_Should_Clear_Model_Sate()
    {
      _signInController.ControllerContext.ModelState.AddModelError("test", "test");

      _signInController.Get(new SignInAccountViewModel());

      Assert.IsTrue(_signInController.ControllerContext.ModelState.IsValid);

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Get_Should_Return_Action_Result_With_View_Name()
    {
      var actionResult = _signInController.Get(new SignInAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignInController.ViewName, ((ViewResult)actionResult).ViewName);

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result_If_Model_State_Is_Invalid()
    {
      _signInController.ControllerContext.ModelState.AddModelError("test", "test");

      var actionResult = await _signInController.Post(new SignInAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignInController.ViewName, ((ViewResult)actionResult).ViewName);

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_View_Result_If_Credentials_Are_Invalid()
    {
      _signInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
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

      _signInManagerMock.Verify(manager => manager.PasswordSignInAsync(vm.Email, vm.Password, false, false));

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Return_Local_Redirect_Result()
    {
      _signInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
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

      _signInManagerMock.Verify(manager => manager.PasswordSignInAsync(vm.Email, vm.Password, false, false));

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }
  }
}
