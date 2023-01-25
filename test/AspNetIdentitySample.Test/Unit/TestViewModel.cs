
// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using AspNetIdentitySample.WebApplication.ViewModels;

#pragma warning disable CS0659
  public sealed class TestViewModel : ViewModelBase
  {
    public override bool Equals(object? obj)
    {
      return true;
    }
  }
#pragma warning restore CS0659
}
