// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using Moq;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.Stores;
  
  [TestClass]
  public sealed class UserStoreTest
  {
#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;

    private UserStore _userStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _userRepositoryMock = new Mock<IUserRepository>();
      _userStore = new UserStore(_userRepositoryMock.Object);
    }
  }
}
