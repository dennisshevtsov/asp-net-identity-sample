// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.ComponentModel;
  using System.Security.Claims;

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

      var properties = ViewModelBinder.GetProperties(bindingContext);

      await ViewModelBinder.FillOutFormAsync(vm, properties, bindingContext, cancellationToken);
      await ViewModelBinder.FillOutUserAsync(vm, bindingContext, cancellationToken);

      bindingContext.Result = ModelBindingResult.Success(vm);
    }

    private static async Task FillOutFormAsync(
      ViewModelBase vm,
      IDictionary<string, (Action<object, object?>, TypeConverter)> properties,
      ModelBindingContext bindingContext,
      CancellationToken cancellationToken)
    {
      if (!bindingContext.HttpContext.Request.HasFormContentType)
      {
        return;
      }

      var formCollection =
        await bindingContext.HttpContext.Request.ReadFormAsync(cancellationToken);

      foreach (var formValue in formCollection)
      {
        if (properties.TryGetValue(formValue.Key, out var property))
        {
          property.Item1(vm, property.Item2.ConvertFrom(formValue.Value.ToString()));
        }
      }
    }

    private static IDictionary<string, (Action<object, object?>, TypeConverter)> GetProperties(
      ModelBindingContext bindingContext)
    {
      var properties = new Dictionary<string, (Action<object, object?>, TypeConverter)>();

      foreach (var propertyMetadata in bindingContext.ModelMetadata.Properties)
      {
        TypeConverter? typeConverter;

        if (propertyMetadata != null &&
          propertyMetadata.PropertySetter != null &&
          propertyMetadata.PropertyName != null &&
          (typeConverter = TypeDescriptor.GetConverter(propertyMetadata.ModelType)) != null)
        {
          properties.Add(
            propertyMetadata.PropertyName,
            (propertyMetadata.PropertySetter, typeConverter));
        }
      }

      return properties;
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

      vm.User.FromEntity(userEntity);
    }
  }
}
