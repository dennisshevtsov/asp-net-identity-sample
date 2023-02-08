// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping.Test
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class SignUpAccountViewModelMappingTest
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
    public void Map_Should_Map_View_Model_To_Entity()
    {
      var email = Guid.NewGuid().ToString();
      var firstName = Guid.NewGuid().ToString();
      var lastName = Guid.NewGuid().ToString();

      var profileViewModel = new SignUpAccountViewModel
      {
        Email = email,
        FirstName = firstName,
        LastName = lastName,
        Password = Guid.NewGuid().ToString(),
      };

      var userEntity = _mapper.Map<UserEntity>(profileViewModel);

      Assert.IsNotNull(userEntity);

      Assert.AreEqual(default, userEntity.Id);
      Assert.AreEqual(default, userEntity.UserId);
      Assert.AreEqual(email, userEntity.Email);
      Assert.AreEqual(firstName, userEntity.FirstName);
      Assert.AreEqual(lastName, userEntity.LastName);
      Assert.AreEqual(default, userEntity.PasswordHash);
      Assert.AreEqual(default, userEntity.Roles);
    }
  }
}
