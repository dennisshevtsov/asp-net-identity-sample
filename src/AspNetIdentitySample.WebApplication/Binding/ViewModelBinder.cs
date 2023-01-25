// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.Security.Claims;

  using Microsoft.AspNetCore.Mvc.ModelBinding;

  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to create an instance of a model for an HTTP request.</summary>
  public sealed class ViewModelBinder : IModelBinder
  {
    /// <summary>Attempts to bind a model.</summary>
    /// <param name="bindingContext">An object that represents a context that contains operating information for model binding and validation.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var vm = (ViewModelBase)Activator.CreateInstance(bindingContext.ModelType)!;
      var cancellationToken = bindingContext.HttpContext.RequestAborted;

      await FillOutFormAsync(vm, bindingContext.HttpContext, cancellationToken);
      await FillOutUserAsync(vm, bindingContext.HttpContext, cancellationToken);

      bindingContext.Result = ModelBindingResult.Success(vm);
    }

    private Task FillOutFormAsync(
      ViewModelBase vm, HttpContext httpContext, CancellationToken cancellationToken)
    {
      if (vm is SignInAccountViewModel svm)
      {
        svm.ReturnUrl = "/";
        svm.Email = "test@test.test";
        svm.Password = "test";
      }

      //if (httpContext.Request.HasFormContentType)
      //{
      //  var form = await httpContext.Request.ReadFormAsync();
      //}

      return Task.CompletedTask;
    }

    private bool CheckIfRequestIsAuthenticated(ClaimsPrincipal user)
      => user.Identity == null || user.Identity.Name == null || !user.Identity.IsAuthenticated;

    private Task FillOutUserAsync(
      ViewModelBase vm, HttpContext httpContext, CancellationToken cancellationToken)
    {
      if (CheckIfRequestIsAuthenticated(httpContext.User))
      {
        return Task.CompletedTask;
      }

      var userRepository =
        httpContext.RequestServices.GetRequiredService<IUserRepository>();

      var userEmail = httpContext.User.Identity!.Name!;

      return FillOutUserAsync(vm, userEmail, userRepository, cancellationToken);
    }

    private async Task FillOutUserAsync(
      ViewModelBase vm, string userEmail, IUserRepository userRepository, CancellationToken cancellationToken)
    {
      var userEntity = await userRepository.GetUserAsync(userEmail, cancellationToken);

      if (userEntity == null)
      {
        throw new Exception($"There is no user with email ${userEmail}");
      }

      vm.User.Name = userEntity.Name;
      vm.User.IsAuthenticated = true;
    }
  }
}
