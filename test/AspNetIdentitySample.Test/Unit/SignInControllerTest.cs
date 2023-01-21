// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Microsoft.AspNetCore.Identity;
  using Moq;

  using AspNetIdentitySample.WebApplication.Controllers;

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
      _signInManagerMock = new Mock<SignInManager<UserEntity>>();

      _signInController = new SignInController(_signInManagerMock.Object);
    }
  }
}
