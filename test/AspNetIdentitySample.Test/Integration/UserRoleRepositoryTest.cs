// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Integration
{
  using Microsoft.Extensions.DependencyInjection;

  using AspNetIdentitySample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class UserRoleRepositoryTest : IntegrationTestBase
  {
#pragma warning disable CS8618
    private IUserRoleRepository _userRoleRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userRoleRepository = Scope.ServiceProvider.GetRequiredService<IUserRoleRepository>();
    }
  }
}
