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
    private Mock<SignInManager<UserEntity>> _signInManagerMock;

    private SignInController _signInController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
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
      var signInManagerLoggerMock = new Mock<ILogger<SignInManager<UserEntity>>>();
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
        signInManagerLoggerMock.Object,
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
    }
  }
}
