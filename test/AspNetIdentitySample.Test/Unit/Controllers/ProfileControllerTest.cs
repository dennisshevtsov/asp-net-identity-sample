// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
            Assert.AreEqual(ProfileController.ViewName, viewResult.ViewName);

            var actualViewModel = viewResult.Model as ProfileViewModel;

            Assert.IsNotNull(actualViewModel);
            Assert.AreEqual(userEntity.Name, actualViewModel.Name);
            Assert.AreEqual(userEntity.Email, actualViewModel.Email);

            _userRepositoryMock.Verify(repository => repository.GetUserAsync(viewModel.User, _cancellationToken));
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task Post_Should_Update_Current_User()
        {
            var userName = Guid.NewGuid().ToString();
            var userEmail = Guid.NewGuid().ToString();
            var userEntity = new UserEntity
            {
                Name = userName,
                Email = userEmail,
            };

            _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(userEntity)
                               .Verifiable();

            _userRepositoryMock.Setup(repository => repository.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
                               .Returns(Task.CompletedTask)
                               .Verifiable();

            var viewModel = new ProfileViewModel
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
            };

            var actionResult = await _profileController.Post(viewModel, _cancellationToken);

            Assert.IsNotNull(actionResult);

            var redirectResult = actionResult as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(nameof(ProfileController.Get), redirectResult.ActionName);

            Assert.AreEqual(viewModel.Name, userEntity.Name);
            Assert.AreEqual(userEmail, userEntity.Email);

            _userRepositoryMock.Verify(repository => repository.GetUserAsync(viewModel.User, _cancellationToken));
            _userRepositoryMock.Verify(repository => repository.UpdateUserAsync(userEntity, _cancellationToken));
            _userRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
