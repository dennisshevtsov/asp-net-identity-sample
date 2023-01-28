// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit
{
  using System.Security.Claims;
  using System.Security.Principal;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Extensions.Primitives;
  using Moq;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.Binding;

  [TestClass]
  public sealed class ViewModelBinderTest
  {
#pragma warning disable CS8618
    private Mock<ModelBindingContext> _modelBindingContextMock;
    private Mock<ModelMetadata> _modelMetadataMock;
    private Mock<ModelMetadata> _stringPropertyForBodyMetadataMock;
    private Mock<ModelMetadata> _guidPropertyForBodyMetadataMock;
    private Mock<ModelMetadata> _stringPropertyForRouteMetadataMock;
    private Mock<ModelMetadata> _guidPropertyForRouteMetadataMock;
    private Mock<ModelMetadata> _stringPropertyForQueryStringMetadataMock;
    private Mock<ModelMetadata> _guidPropertyForQueryStringMetadataMock;

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

      _modelMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestViewModel)));

      _stringPropertyForBodyMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.StringPropertyForBody))!,
          typeof(string),
          typeof(TestViewModel)));

      _stringPropertyForBodyMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                        .Returns((object a, object? b) => ((TestViewModel)a).StringPropertyForBody = (string)b!)
                                        .Verifiable();

      _guidPropertyForBodyMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.GuidPropertyForBody))!,
          typeof(Guid),
          typeof(TestViewModel)));

      _guidPropertyForBodyMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                      .Returns((object a, object? b) => ((TestViewModel)a).GuidPropertyForBody = (Guid)b!)
                                      .Verifiable();

      _stringPropertyForRouteMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.StringPropertyForRoute))!,
          typeof(string),
          typeof(TestViewModel)));

      _stringPropertyForRouteMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                         .Returns((object a, object? b) => ((TestViewModel)a).StringPropertyForRoute = (string)b!)
                                         .Verifiable();

      _guidPropertyForRouteMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.GuidPropertyForRoute))!,
          typeof(Guid),
          typeof(TestViewModel)));

      _guidPropertyForRouteMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                      .Returns((object a, object? b) => ((TestViewModel)a).GuidPropertyForRoute = (Guid)b!)
                                      .Verifiable();

      _stringPropertyForQueryStringMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.StringPropertyForQueryString))!,
          typeof(string),
          typeof(TestViewModel)));

      _stringPropertyForQueryStringMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                         .Returns((object a, object? b) => ((TestViewModel)a).StringPropertyForQueryString = (string)b!)
                                         .Verifiable();

      _guidPropertyForQueryStringMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestViewModel).GetProperty(nameof(TestViewModel.GuidPropertyForQueryString))!,
          typeof(Guid),
          typeof(TestViewModel)));

      _guidPropertyForQueryStringMetadataMock.SetupGet(metadata => metadata.PropertySetter)
                                      .Returns((object a, object? b) => ((TestViewModel)a).GuidPropertyForQueryString = (Guid)b!)
                                      .Verifiable();

      var properties = new ModelPropertyCollection(
        new[]
        {
          _stringPropertyForBodyMetadataMock.Object,
          _guidPropertyForBodyMetadataMock.Object,
          _stringPropertyForRouteMetadataMock.Object,
          _guidPropertyForRouteMetadataMock.Object,
          _stringPropertyForQueryStringMetadataMock.Object,
          _guidPropertyForQueryStringMetadataMock.Object,
        });

      _modelMetadataMock.Setup(metadata => metadata.Properties)
                        .Returns(properties)
                        .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ModelMetadata)
                              .Returns(_modelMetadataMock.Object)
                              .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ModelType)
                              .Returns(typeof(TestViewModel))
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

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(new ActionContext
                              {
                                RouteData = new RouteData(),
                              })
                              .Verifiable();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection())
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

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
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

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(new ActionContext
                              {
                                RouteData = new RouteData(),
                              })
                              .Verifiable();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection())
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

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyGet(context => context.RequestServices);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Not_Fill_Out_Form_Values()
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

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(new ActionContext
                              {
                                RouteData = new RouteData(),
                              })
                              .Verifiable();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection())
                      .Verifiable();

#pragma warning disable CS0618
      _modelBindingContextMock.SetupSet(context => context.Result)
                              .Verifiable();
#pragma warning restore CS0618

      await _viewModelBinder.BindModelAsync(_modelBindingContextMock.Object);

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Fill_Out_Form_Values()
    {
      _identityMock.SetupGet(identity => identity.Name)
                   .Returns(Guid.NewGuid().ToString())
                   .Verifiable();

      _identityMock.SetupGet(identity => identity.IsAuthenticated)
                   .Returns(false)
                   .Verifiable();

      _httpRequestMock.SetupGet(request => request.HasFormContentType)
                      .Returns(true)
                      .Verifiable();

      var stringPropertyForBody = Guid.NewGuid().ToString();
      var guidPropertyForBody = Guid.NewGuid();

      _httpRequestMock.Setup(request => request.ReadFormAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new FormCollection(new Dictionary<string, StringValues>
                      {
                        { nameof(TestViewModel.StringPropertyForBody), stringPropertyForBody },
                        { nameof(TestViewModel.GuidPropertyForBody), guidPropertyForBody.ToString() },
                      }))
                      .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(new ActionContext
                              {
                                RouteData = new RouteData(),
                              })
                              .Verifiable();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection())
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
      Assert.AreEqual(stringPropertyForBody, vm.StringPropertyForBody);
      Assert.AreEqual(guidPropertyForBody, vm.GuidPropertyForBody);

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.Verify(request => request.ReadFormAsync(CancellationToken.None));
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Fill_Out_Route_Values()
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

      var stringPropertyForRoute = Guid.NewGuid().ToString();
      var guidPropertyForRoute = Guid.NewGuid();

      var actionContext = new ActionContext
      {
        RouteData = new RouteData
        {
          Values = {
            { nameof(TestViewModel.StringPropertyForRoute), stringPropertyForRoute },
            { nameof(TestViewModel.GuidPropertyForRoute), guidPropertyForRoute.ToString() },
          },
        },
      };

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(actionContext)
                              .Verifiable();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection())
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
      Assert.AreEqual(stringPropertyForRoute, vm.StringPropertyForRoute);
      Assert.AreEqual(guidPropertyForRoute, vm.GuidPropertyForRoute);

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Fill_Out_Query_String_Values()
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

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(new ActionContext
                              {
                                RouteData = new RouteData(),
                              })
                              .Verifiable();

      var stringPropertyForQueryString = Guid.NewGuid().ToString();
      var guidPropertyForQueryString = Guid.NewGuid();

      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(new QueryCollection(new Dictionary<string, StringValues>
                      {
                        { nameof(TestViewModel.StringPropertyForQueryString), stringPropertyForQueryString },
                        { nameof(TestViewModel.GuidPropertyForQueryString), guidPropertyForQueryString.ToString() },
                      }))
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
      Assert.AreEqual(stringPropertyForQueryString, vm.StringPropertyForQueryString);
      Assert.AreEqual(guidPropertyForQueryString, vm.GuidPropertyForQueryString);

      _modelMetadataMock.Verify(metadata => metadata.Properties);
      _modelMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForBodyMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForBodyMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForRouteMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForRouteMetadataMock.VerifyNoOtherCalls();

      _stringPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _stringPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _guidPropertyForQueryStringMetadataMock.Verify(metadata => metadata.PropertySetter);
      _guidPropertyForQueryStringMetadataMock.VerifyNoOtherCalls();

      _identityMock.Verify(identity => identity.Name);
      _identityMock.Verify(identity => identity.IsAuthenticated);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyNoOtherCalls();

      _httpRequestMock.VerifyGet(request => request.Query);
      _httpRequestMock.VerifyGet(request => request.HasFormContentType);
      _httpRequestMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyGet(context => context.Request);
      _httpContextMock.VerifyGet(context => context.RequestAborted);
      _httpContextMock.VerifyNoOtherCalls();

      _modelBindingContextMock.VerifyGet(context => context.ActionContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelMetadata);
      _modelBindingContextMock.VerifyGet(context => context.HttpContext);
      _modelBindingContextMock.VerifyGet(context => context.ModelType);
      _modelBindingContextMock.VerifySet(context => context.Result = ModelBindingResult.Success(new TestViewModel()));
      _modelBindingContextMock.VerifyNoOtherCalls();
    }
  }
}
