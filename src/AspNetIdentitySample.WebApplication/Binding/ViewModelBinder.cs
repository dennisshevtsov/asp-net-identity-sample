// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.ComponentModel;
  using System.Security.Claims;

  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.Extensions.Primitives;

  using AspNetIdentitySample.ApplicationCore.Repositories;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a simple API to create an instance of a model for an HTTP request.</summary>
  public sealed class ViewModelBinder : IModelBinder
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Binding.ViewModelBinder"/> class.</summary>
    public ViewModelBinder()
    {
      TypeDescriptor.AddAttributes(
        typeof(bool),
        new TypeConverterAttribute(typeof(CheckboxValueConverter)));
    }

    /// <summary>Attempts to bind a model.</summary>
    /// <param name="bindingContext">An object that represents a context that contains operating information for model binding and validation.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var vm = (ViewModelBase)Activator.CreateInstance(bindingContext.ModelType)!;
      var cancellationToken = bindingContext.HttpContext.RequestAborted;

      var propertySetters = ViewModelBinder.GetPropertySetters(bindingContext);

      await ViewModelBinder.FillOutFormValuesAsync(vm, propertySetters, bindingContext, cancellationToken);
      ViewModelBinder.FillOutRouteValues(vm, propertySetters, bindingContext);
      ViewModelBinder.FillOutQueryStringValues(vm, propertySetters, bindingContext);

      await ViewModelBinder.FillOutUserAsync(vm, bindingContext, cancellationToken);

      bindingContext.Result = ModelBindingResult.Success(vm);
    }

    private static void FillOutQueryStringValues(
      ViewModelBase vm,
      IDictionary<string, Action<object, object>> propertySetters,
      ModelBindingContext bindingContext)
      => ViewModelBinder.FillOutProperties(
        vm, propertySetters, bindingContext.HttpContext.Request.Query);

    private static void FillOutRouteValues(
      ViewModelBase vm,
      IDictionary<string, Action<object, object>> propertySetters,
      ModelBindingContext bindingContext)
    {
      foreach (var routeValue in bindingContext.ActionContext.RouteData.Values)
      {
        if (routeValue.Value != null &&
            propertySetters.TryGetValue(routeValue.Key, out var propertySetter))
        {
          propertySetter(vm, routeValue.Value);
        }
      }
    }

    private static async Task FillOutFormValuesAsync(
      ViewModelBase vm,
      IDictionary<string, Action<object, object>> propertySetters,
      ModelBindingContext bindingContext,
      CancellationToken cancellationToken)
    {
      if (!bindingContext.HttpContext.Request.HasFormContentType)
      {
        return;
      }

      var formCollection =
        await bindingContext.HttpContext.Request.ReadFormAsync(cancellationToken);

      ViewModelBinder.FillOutProperties(vm, propertySetters, formCollection);
    }

    private static void FillOutProperties(
      ViewModelBase vm,
      IDictionary<string, Action<object, object>> propertySetters,
      IEnumerable<KeyValuePair<string, StringValues>> values)
    {
      foreach (var value in values)
      {
        if (propertySetters.TryGetValue(value.Key, out var propertySetter))
        {
          propertySetter(vm, value.Value.ToString());
        }
      }
    }

    private static IDictionary<string, Action<object, object>> GetPropertySetters(
      ModelBindingContext bindingContext)
    {
      var properties = new Dictionary<string, Action<object, object>>(
        StringComparer.OrdinalIgnoreCase);

      foreach (var property in bindingContext.ModelMetadata.Properties)
      {
        TypeConverter? typeConverter;

        if (property != null &&
            property.PropertySetter != null &&
            property.PropertyName != null &&
            (typeConverter = TypeDescriptor.GetConverter(property.ModelType)) != null)
        {
          properties.Add(
            property.PropertyName,
            (vm, value) => property.PropertySetter(vm, typeConverter.ConvertFrom(value)));
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
        throw new Exception($"There is no user with email {userEmail}");
      }

      vm.User.FromEntity(userEntity);
    }
  }
}
