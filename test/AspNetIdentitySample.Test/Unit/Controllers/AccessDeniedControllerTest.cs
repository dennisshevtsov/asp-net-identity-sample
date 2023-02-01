// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetIdentitySample.WebApplication.Controllers;
  using AspNetIdentitySample.WebApplication.ViewModels;

  [TestClass]
  public sealed class AccessDeniedControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private AccessDeniedController _accessDeniedController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;
      _accessDeniedController = new AccessDeniedController();
    }

    [TestMethod]
    public void Get_Should_Return_View_Result()
    {
      var vm = new AccessDeniedViewModel();

      var actionResult = _accessDeniedController.Get(vm);

      Assert.IsNotNull(actionResult);

      var viewResult = actionResult as ViewResult;

      Assert.IsNotNull(viewResult);
      Assert.AreEqual(AccessDeniedController.ViewName, viewResult.ViewName);
    }
  }
}
