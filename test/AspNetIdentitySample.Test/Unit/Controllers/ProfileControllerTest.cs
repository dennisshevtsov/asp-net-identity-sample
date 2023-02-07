// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  using System.Security.Claims;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class ProfileControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private ProfileController _profileController;
#pragma warning restore CS8618

    [TestInitialize]
    protected override void InitializeInternal()
    {
      _profileController = new ProfileController(UserManagerMock.Object);
    }

    [TestMethod]
    public async Task Get_Should_Get_Current_User()
    {
      var userEntity = new UserEntity
      {
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = Guid.NewGuid().ToString(),
      };

      UserManagerMock.Setup(repository => repository.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      var viewModel = new ProfileViewModel();

      var actionResult = await _profileController.Get(viewModel);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.IsNotNull(viewResult.Model);
      Assert.AreEqual(ProfileController.ViewName, viewResult.ViewName);

      var actualViewModel = viewResult.Model as ProfileViewModel;

      Assert.IsNotNull(actualViewModel);
      Assert.AreEqual(userEntity.FirstName, actualViewModel.FirstName);
      Assert.AreEqual(userEntity.LastName, actualViewModel.LastName);
      Assert.AreEqual(userEntity.Email, actualViewModel.Email);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Post_Should_Update_Current_User()
    {
      var userFirstName = Guid.NewGuid().ToString();
      var userLastName = Guid.NewGuid().ToString();
      var userEmail = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        FirstName = userFirstName,
        LastName = userLastName,
        Email = userEmail,
      };

      UserManagerMock.Setup(repository => repository.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                     .ReturnsAsync(userEntity)
                     .Verifiable();

      UserManagerMock.Setup(repository => repository.UpdateAsync(It.IsAny<UserEntity>()))
                     .ReturnsAsync(IdentityResult.Success)
                     .Verifiable();

      var viewModel = new ProfileViewModel
      {
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = Guid.NewGuid().ToString(),
      };

      var actionResult = await _profileController.Post(viewModel);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as RedirectToActionResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(nameof(ProfileController.Get), redirectResult.ActionName);

      Assert.AreEqual(viewModel.FirstName, userEntity.FirstName);
      Assert.AreEqual(viewModel.LastName, userEntity.LastName);
      Assert.AreEqual(userEmail, userEntity.Email);

      UserManagerMock.Verify();
      UserManagerMock.VerifyNoOtherCalls();
    }
  }
}
