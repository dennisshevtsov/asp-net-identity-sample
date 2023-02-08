// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping.Test
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class ProfileViewModelMappingTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;

    private IMapper _mapper;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var scope = new ServiceCollection().SetUpMapping()
                                         .BuildServiceProvider()
                                         .CreateAsyncScope();

      _disposable = scope;

      _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _disposable?.Dispose();
    }

    [TestMethod]
    public void Map_Should_Map_Entity_View_Model()
    {
      var pageTitle = Guid.NewGuid().ToString();
      var returnUrl = Guid.NewGuid().ToString();
      var currentUserViewModel = new CurrentAccountViewModel();

      var profileViewModel = new ProfileViewModel
      {
        PageTitle = pageTitle,
        ReturnUrl = returnUrl,
        User = currentUserViewModel,
      };

      var email = Guid.NewGuid().ToString();
      var firstName = Guid.NewGuid().ToString();
      var lastName = Guid.NewGuid().ToString();

      var userEntity = new UserEntity
      {
        Email = email,
        FirstName = firstName,
        LastName = lastName,
      };

      _mapper.Map(userEntity, profileViewModel);

      Assert.AreEqual(pageTitle, profileViewModel.PageTitle);
      Assert.AreEqual(returnUrl, profileViewModel.ReturnUrl);
      Assert.AreEqual(currentUserViewModel, profileViewModel.User);

      Assert.AreEqual(email, profileViewModel.Email);
      Assert.AreEqual(firstName, profileViewModel.FirstName);
      Assert.AreEqual(lastName, profileViewModel.LastName);
    }

    [TestMethod]
    public void Map_Should_Map_View_Model_To_Entity()
    {
      var firstName = Guid.NewGuid().ToString();
      var lastName = Guid.NewGuid().ToString();

      var profileViewModel = new ProfileViewModel
      {
        FirstName = firstName,
        LastName = lastName,
      };

      var userId = Guid.NewGuid();
      var email = Guid.NewGuid().ToString();
      var roles = new List<UserRoleEntity>();

      var userEntity = new UserEntity
      {
        Id = userId,
        UserId = userId,
        Email = email,
        Roles = roles,
      };

      _mapper.Map(profileViewModel, userEntity);

      Assert.AreEqual(userId, userEntity.Id);
      Assert.AreEqual(userId, userEntity.UserId);
      Assert.AreEqual(email, userEntity.Email);
      Assert.AreEqual(roles, userEntity.Roles);

      Assert.AreEqual(firstName, userEntity.FirstName);
      Assert.AreEqual(lastName, userEntity.LastName);
    }
  }
}
