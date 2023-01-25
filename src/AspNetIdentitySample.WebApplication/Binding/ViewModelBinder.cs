// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.Security.Claims;

  using Microsoft.AspNetCore.Mvc.ModelBinding;

  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Repositories;
  using Microsoft.Extensions.Primitives;
  using System.ComponentModel;

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

      await FillOutFormAsync(vm, bindingContext, cancellationToken);
      await FillOutUserAsync(vm, bindingContext, cancellationToken);

      bindingContext.Result = ModelBindingResult.Success(vm);
    }

    private async Task FillOutFormAsync(
      ViewModelBase vm, ModelBindingContext bindingContext, CancellationToken cancellationToken)
    {
      if (!bindingContext.HttpContext.Request.HasFormContentType)
      {
        return;
      }

      var formCollection = await bindingContext.HttpContext.Request.ReadFormAsync(cancellationToken);

      foreach (var propertyMetadata in bindingContext.ModelMetadata.Properties)
      {
        StringValues propertyValue;
        TypeConverter? typeConverter;

        if (propertyMetadata != null &&
            propertyMetadata.PropertySetter != null &&
            propertyMetadata.PropertyName != null &&
            formCollection.TryGetValue(propertyMetadata.PropertyName, out propertyValue) &&
            (typeConverter = TypeDescriptor.GetConverter(propertyMetadata.ModelType)) != null)
        {
          propertyMetadata.PropertySetter(vm, typeConverter.ConvertFrom(propertyValue.ToString()));
        }
      }
    }

    private bool CheckIfRequestIsAuthenticated(ClaimsPrincipal user)
      => user.Identity == null || user.Identity.Name == null || !user.Identity.IsAuthenticated;

    private Task FillOutUserAsync(
      ViewModelBase vm, ModelBindingContext bindingContext, CancellationToken cancellationToken)
    {
      if (CheckIfRequestIsAuthenticated(bindingContext.HttpContext.User))
      {
        return Task.CompletedTask;
      }

      var userRepository =
        bindingContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

      var userEmail = bindingContext.HttpContext.User.Identity!.Name!;

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
