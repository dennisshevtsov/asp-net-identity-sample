// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using System.Security.Claims;

  using AutoMapper;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.WebApplication.Mapping;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class ProfileControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private ProfileController _profileController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _mapperMock = new Mock<IMapper>();

      _profileController = new ProfileController(
        _mapperMock.Object, UserManagerMock.Object);
    }

    [TestMethod]
    public async Task Get_Should_Get_Current_User()
    {
      var principal = new ClaimsPrincipal();

      _mapperMock.Setup(mapper => mapper.Map<ClaimsPrincipal>(It.IsAny<IUserIdentity>()))
                 .Returns(principal)
                 .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map(It.IsAny<UserEntity>(), It.IsAny<ProfileViewModelProfile>()))
                 .Returns(new ProfileViewModelProfile())
                 .Verifiable();

      var userEntity = new UserEntity();

      UserManagerMock.Setup(repository => repository.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      var viewModel = new ProfileViewModel
      {
        User = new CurrentAccountViewModel(),
      };

      var actionResult = await _profileController.Get(viewModel);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.IsNotNull(viewResult.Model);
      Assert.AreEqual(viewModel, viewResult.Model);
      Assert.AreEqual(ProfileController.ViewName, viewResult.ViewName);

      _mapperMock.Verify(mapper => mapper.Map<ClaimsPrincipal>(viewModel.User));
      _mapperMock.Verify(mapper => mapper.Map(userEntity, viewModel));
      _mapperMock.VerifyNoOtherCalls();

      UserManagerMock.Verify(manager => manager.GetUserAsync(principal));
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Update_Current_User()
    {
      var principal = new ClaimsPrincipal();
      var userEntity = new UserEntity();

      _mapperMock.Setup(mapper => mapper.Map<ClaimsPrincipal>(It.IsAny<IUserIdentity>()))
                 .Returns(principal)
                 .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map(It.IsAny<ProfileViewModelProfile>(), It.IsAny<UserEntity>()))
                 .Returns(userEntity)
                 .Verifiable();

      UserManagerMock.Setup(repository => repository.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      UserManagerMock.Setup(repository => repository.UpdateAsync(It.IsAny<UserEntity>()))
                     .ReturnsAsync(IdentityResult.Success)
                     .Verifiable();

      var viewModel = new ProfileViewModel
      {
        User = new CurrentAccountViewModel(),
      };

      var actionResult = await _profileController.Post(viewModel);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(ProfileController.Get), redirectResult.ActionName);

      _mapperMock.Verify(mapper => mapper.Map<ClaimsPrincipal>(viewModel.User));
      _mapperMock.Verify(mapper => mapper.Map(viewModel, userEntity));
      _mapperMock.VerifyNoOtherCalls();

      UserManagerMock.Verify(manager => manager.GetUserAsync(principal));
      UserManagerMock.Verify(manager => manager.UpdateAsync(userEntity));
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
