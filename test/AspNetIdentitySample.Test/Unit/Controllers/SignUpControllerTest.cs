// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Controllers.Test
{
  [TestClass]
  public sealed class SignUpControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private SignUpController _signUpController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _signUpController = new SignUpController(UserManagerMock.Object);
    }
  }
}
