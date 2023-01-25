// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using System.Security.Claims;
  using System.Security.Principal;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Moq;

  using AspNetIdentitySample.WebApplication.Binding;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class ViewModelBinderTest
  {
#pragma warning disable CS8618
    private Mock<ModelBindingContext> _modelBindingContextMock;
    private Mock<HttpContext> _httpContextMock;
    private Mock<HttpRequest> _httpRequestMock;
    private Mock<ClaimsPrincipal> _userMock;
    private Mock<IIdentity> _identityMock;

    private ViewModelBinder _viewModelBinder;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _modelBindingContextMock = new Mock<ModelBindingContext>();
      _httpContextMock = new Mock<HttpContext>();
      _httpRequestMock = new Mock<HttpRequest>();
      _userMock = new Mock<ClaimsPrincipal>();
      _identityMock = new Mock<IIdentity>();

      _httpContextMock.SetupGet(context => context.RequestAborted)
                      .Returns(CancellationToken.None)
                      .Verifiable();

      _httpContextMock.SetupGet(context => context.Request)
                      .Returns(_httpRequestMock.Object)
                      .Verifiable();

      _httpContextMock.SetupGet(context => context.User)
                      .Returns(_userMock.Object)
                      .Verifiable();

      _userMock.SetupGet(user => user.Identity)
               .Returns(_identityMock.Object)
               .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.HttpContext)
                              .Returns(_httpContextMock.Object)
                              .Verifiable();

      _viewModelBinder = new ViewModelBinder();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Not_Fill_Out_User()
    {
      _identityMock.SetupGet(identity => identity.Name)
                   .Returns(Guid.NewGuid().ToString())
                   .Verifiable();

      _identityMock.SetupGet(identity => identity.IsAuthenticated)
                   .Returns(false)
                   .Verifiable();

      _httpRequestMock.SetupGet(request => request.HasFormContentType)
                      .Returns(false)
                      .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ModelType)
                              .Returns(typeof(TestViewModel))
                              .Verifiable();

      ModelBindingResult modelBindingResult = default;

#pragma warning disable CS0618
      _modelBindingContextMock.SetupSet(context => context.Result)
                              .Callback(result => modelBindingResult = result)
                              .Verifiable();
#pragma warning restore CS0618

      await _viewModelBinder.BindModelAsync(_modelBindingContextMock.Object);

      Assert.IsTrue(modelBindingResult.IsModelSet);

      var vm = modelBindingResult.Model as TestViewModel;

      Assert.IsNotNull(vm);
      Assert.IsFalse(vm.User.IsAuthenticated);

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Fill_Out_User()
    {
      var userEmail = Guid.NewGuid().ToString();

      _identityMock.SetupGet(identity => identity.Name)
                   .Returns(userEmail)
                   .Verifiable();

      _identityMock.SetupGet(identity => identity.IsAuthenticated)
                   .Returns(true)
                   .Verifiable();

      _httpRequestMock.SetupGet(request => request.HasFormContentType)
                      .Returns(false)
                      .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ModelType)
                              .Returns(typeof(TestViewModel))
                              .Verifiable();

      ModelBindingResult modelBindingResult = default;

#pragma warning disable CS0618
      _modelBindingContextMock.SetupSet(context => context.Result)
                              .Callback(result => modelBindingResult = result)
                              .Verifiable();
#pragma warning restore CS0618

      var userRepositoryMock = new Mock<IUserRepository>();

      userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new UserEntity())
                        .Verifiable();

      var serviceProviderMock = new Mock<IServiceProvider>();

      serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                         .Returns(userRepositoryMock.Object)
                         .Verifiable();

      _httpContextMock.SetupGet(context => context.RequestServices)
                      .Returns(serviceProviderMock.Object)
                      .Verifiable();

      await _viewModelBinder.BindModelAsync(_modelBindingContextMock.Object);

      Assert.IsTrue(modelBindingResult.IsModelSet);

      var vm = modelBindingResult.Model as TestViewModel;

      Assert.IsNotNull(vm);
      Assert.IsTrue(vm.User.IsAuthenticated);

      userRepositoryMock.Verify(repository => repository.GetUserAsync(userEmail, CancellationToken.None));
      userRepositoryMock.VerifyNoOtherCalls();

      serviceProviderMock.Verify(provider => provider.GetService(typeof(IUserRepository)));
      serviceProviderMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyGet(context => context.RequestServices);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }
  }
}
