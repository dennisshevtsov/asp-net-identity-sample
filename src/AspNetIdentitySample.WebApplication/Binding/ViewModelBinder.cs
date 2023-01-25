// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.ComponentModel;
  using System.Security.Claims;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.ViewModels;

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

      await ViewModelBinder.FillOutFormAsync(vm, bindingContext, cancellationToken);
      await ViewModelBinder.FillOutUserAsync(vm, bindingContext, cancellationToken);

      bindingContext.Result = ModelBindingResult.Success(vm);
    }

    private static async Task FillOutFormAsync(
      ViewModelBase vm, ModelBindingContext bindingContext, CancellationToken cancellationToken)
    {
      if (!bindingContext.HttpContext.Request.HasFormContentType)
      {
        return;
      }

      var formCollection =
        await bindingContext.HttpContext.Request.ReadFormAsync(cancellationToken);

      foreach (var propertyMetadata in bindingContext.ModelMetadata.Properties)
      {
        ViewModelBinder.FillOutFormProperty(vm, propertyMetadata, formCollection);
      }
    }

    private static void FillOutFormProperty(
      ViewModelBase vm, ModelMetadata propertyMetadata, IFormCollection formCollection)
    {
      TypeConverter? typeConverter;

      if (propertyMetadata != null &&
          propertyMetadata.PropertySetter != null &&
          propertyMetadata.PropertyName != null &&
          formCollection.TryGetValue(propertyMetadata.PropertyName, out var propertyValue) &&
          (typeConverter = TypeDescriptor.GetConverter(propertyMetadata.ModelType)) != null)
      {
        propertyMetadata.PropertySetter(
          vm, typeConverter.ConvertFrom(propertyValue.ToString()));
      }
    }

    private static bool CheckIfRequestIsAuthenticated(ClaimsPrincipal user)
      => user.Identity == null || user.Identity.Name == null || !user.Identity.IsAuthenticated;

    private static Task FillOutUserAsync(
      ViewModelBase vm, ModelBindingContext bindingContext, CancellationToken cancellationToken)
    {
      if (ViewModelBinder.CheckIfRequestIsAuthenticated(bindingContext.HttpContext.User))
      {
        return Task.CompletedTask;
      }

      var userRepository =
        bindingContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

      var userEmail = bindingContext.HttpContext.User.Identity!.Name!;

      return ViewModelBinder.FillOutUserAsync(vm, userEmail, userRepository, cancellationToken);
    }

    private static async Task FillOutUserAsync(
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
