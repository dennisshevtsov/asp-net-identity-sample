// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using AspNetIdentitySample.WebApplication.Binding;

  [TestClass]
  public sealed class ViewModelBinderProviderTest
  {
#pragma warning disable CS8618
    private ViewModelBinderProvider _viewModelBinderProvider;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _viewModelBinderProvider = new ViewModelBinderProvider();
    }
  }
}
