// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetIdentitySample.ApplicationCore.Identities;
  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class ProfileControllerTest
  {
#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;

    private ProfileController _profileController;
#pragma warning restore CS8618

    private CancellationToken _cancellationToken;

    [TestInitialize]
    public void Initialize()
    {
      _userRepositoryMock = new Mock<IUserRepository>();
      _profileController = new ProfileController(_userRepositoryMock.Object);
      _cancellationToken = CancellationToken.None;
    }

    [TestMethod]
    public async Task Get_Should_Get_Current_User()
    {
      var userEntity = new UserEntity
      {
        Name = Guid.NewGuid().ToString(),
        Email = Guid.NewGuid().ToString(),
      };

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(userEntity)
                         .Verifiable();

      var viewModel = new ProfileViewModel();

      var actionResult = await _profileController.Get(viewModel, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.IsNotNull(viewResult.Model);

      var actualViewModel = viewResult.Model as ProfileViewModel;

      Assert.IsNotNull(actualViewModel);
      Assert.AreEqual(userEntity.Name, actualViewModel.Name);
      Assert.AreEqual(userEntity.Email, actualViewModel.Email);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(viewModel.User, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
