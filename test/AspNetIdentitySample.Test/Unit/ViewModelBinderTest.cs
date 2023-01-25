// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetIdentitySample.WebApplication.Binding;

namespace AspNetIdentitySample.Test.Unit
{
  [TestClass]
  public sealed class ViewModelBinderTest
  {
#pragma warning disable CS8618
    private ViewModelBinder _viewModelBinder;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _viewModelBinder = new ViewModelBinder();
    }
  }
}
