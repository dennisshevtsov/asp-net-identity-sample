// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping.Test
{
  using System.Security.Claims;

  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class UserIdentityMappingTest
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
    public void Map_Should_Map_User_Identity_To_Principal()
    {
      var userId = Guid.NewGuid();
      var viewModel = new CurrentAccountViewModel
      {
        UserId = userId,
      };

      var principal = _mapper.Map<ClaimsPrincipal>(viewModel);

      Assert.IsNotNull(principal);

      var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
      
      Assert.IsNotNull(claim);
      Assert.AreEqual(userId.ToString(), claim.Value);
    }
  }
}

