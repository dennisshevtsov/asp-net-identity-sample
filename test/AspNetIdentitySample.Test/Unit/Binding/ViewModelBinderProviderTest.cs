// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding.Test
{
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

  using AspNetIdentitySample.WebApplication.ViewModels.Test;

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

    [TestMethod]
    public void GetBinder_Should_Return_Null()
    {
      var modelBinderProviderContextMock = new Mock<ModelBinderProviderContext>();
      var modelMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(object)));

      modelBinderProviderContextMock.SetupGet(context => context.Metadata)
                                    .Returns(modelMetadataMock.Object)
                                    .Verifiable();

      var modelBinder = _viewModelBinderProvider.GetBinder(
        modelBinderProviderContextMock.Object);

      Assert.IsNull(modelBinder);
    }

    [TestMethod]
    public void GetBinder_Should_Return_Model_Binder()
    {
      var modelBinderProviderContextMock = new Mock<ModelBinderProviderContext>();
      var modelMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestViewModel)));

      modelBinderProviderContextMock.SetupGet(context => context.Metadata)
                                    .Returns(modelMetadataMock.Object)
                                    .Verifiable();

      var modelBinder = _viewModelBinderProvider.GetBinder(
        modelBinderProviderContextMock.Object);

      Assert.IsNotNull(modelBinder);
    }
  }
}
