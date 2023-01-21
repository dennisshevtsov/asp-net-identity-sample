﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
  public sealed class SignOutControllerTest
  {
#pragma warning disable CS8618
    private Mock<ILogger<SignInManager<UserEntity>>> _signInManagerLoggerMock;
    private Mock<SignInManager<UserEntity>> _signInManagerMock;

    private SignOutController _signOutController;
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

      _signOutController = new SignOutController(_signInManagerMock.Object);
      _signOutController.ControllerContext = new ControllerContext();
    }

    [TestMethod]
    public void Get_Should_Return_Action_Result_With_View_Name()
    {
      var actionResult = _signOutController.Get(new SignOutAccountViewModel());

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
      Assert.AreEqual(SignOutController.ViewName, ((ViewResult)actionResult).ViewName);

      _signInManagerMock.VerifySet(manager => manager.Logger = _signInManagerLoggerMock.Object);
      _signInManagerMock.VerifyNoOtherCalls();
    }
  }
}
