// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping.Test
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class UserListViewModelMappingTest
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
      var userEntityCollection = new List<UserEntity>
      {
        new UserEntity
        {
          Id = Guid.NewGuid(),
          Email = Guid.NewGuid().ToString(),
          FirstName = Guid.NewGuid().ToString(),
          LastName = Guid.NewGuid().ToString(),
          Roles = new List<UserRoleEntity>
          {
            new UserRoleEntity { RoleName= Guid.NewGuid().ToString() },
            new UserRoleEntity { RoleName= Guid.NewGuid().ToString() },
          },
        },
        new UserEntity
        {
          Id = Guid.NewGuid(),
          Email = Guid.NewGuid().ToString(),
          FirstName = Guid.NewGuid().ToString(),
          LastName = Guid.NewGuid().ToString(),
        },
      };

      var userViewModelCollection =
        _mapper.Map<List<UserListViewModel.UserViewModel>>(userEntityCollection);

      Assert.IsNotNull(userViewModelCollection);
      Assert.AreEqual(userEntityCollection.Count, userViewModelCollection.Count);
      
      Assert.AreEqual(userEntityCollection[0].Id, userViewModelCollection[0].UserId);
      Assert.AreEqual(userEntityCollection[0].Email, userViewModelCollection[0].Email);
      Assert.AreEqual(userEntityCollection[0].FirstName, userViewModelCollection[0].FirstName);
      Assert.AreEqual(userEntityCollection[0].LastName, userViewModelCollection[0].LastName);
      Assert.AreEqual($"{userEntityCollection[0].Roles![0].RoleName}, {userEntityCollection[0].Roles![1].RoleName}", userViewModelCollection[0].Roles);

      Assert.AreEqual(userEntityCollection[1].Id, userViewModelCollection[1].UserId);
      Assert.AreEqual(userEntityCollection[1].Email, userViewModelCollection[1].Email);
      Assert.AreEqual(userEntityCollection[1].FirstName, userViewModelCollection[1].FirstName);
      Assert.AreEqual(userEntityCollection[1].LastName, userViewModelCollection[1].LastName);
      Assert.AreEqual(string.Empty, userViewModelCollection[1].Roles);
    }
  }
}
