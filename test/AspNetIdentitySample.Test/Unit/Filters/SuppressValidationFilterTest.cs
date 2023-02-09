// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Filters.Test
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.Abstractions;
  using Microsoft.AspNetCore.Mvc.Filters;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Routing;

  [TestClass]
  public sealed class SuppressValidationFilterTest
  {
#pragma warning disable CS8618
    private Mock<HttpRequest> _httpRequestMock;

    private ActionExecutingContext _actionExecutingContext;

    private SuppressValidationFilter _filter;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();

      var httpContextMock = new Mock<HttpContext>();

      httpContextMock.SetupGet(context => context.Request)
                      .Returns(_httpRequestMock.Object)
                      .Verifiable();

      var actionContext = new ActionContext(
        httpContextMock.Object,
        new RouteData(),
        new ActionDescriptor(),
        new ModelStateDictionary());

      _actionExecutingContext = new ActionExecutingContext(
        actionContext,
        new List<IFilterMetadata>(),
        new Dictionary<string, object?>(),
        new object());

      _filter = new SuppressValidationFilter();
    }

    [TestMethod]
    public void OnActionExecuting_Should_Clear_Model_State()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethods.Get)
                      .Verifiable();

      _actionExecutingContext.ModelState.AddModelError("test", "test");

      _filter.OnActionExecuting(_actionExecutingContext);

      Assert.AreEqual(0, _actionExecutingContext.ModelState.Count);
    }

    [TestMethod]
    public void OnActionExecuting_Should_Not_Clear_Model_State()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethods.Post)
                      .Verifiable();

      _actionExecutingContext.ModelState.AddModelError("test", "test");

      _filter.OnActionExecuting(_actionExecutingContext);

      Assert.AreEqual(1, _actionExecutingContext.ModelState.Count);
    }
  }
}
