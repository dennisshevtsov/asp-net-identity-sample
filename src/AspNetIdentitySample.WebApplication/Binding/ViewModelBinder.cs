// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  using AspNetIdentitySample.WebApplication.ViewModels;
  using AspNetIdentitySample.ApplicationCore.Repositories;

  public sealed class ViewModelBinder : IModelBinder
  {
    /// <summary>Attempts to bind a model.</summary>
    /// <param name="bindingContext">An object that represents a context that contains operating information for model binding and validation.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var vm = (ViewModelBase)Activator.CreateInstance(bindingContext.ModelType)!;

      if (bindingContext.HttpContext.User.Identity != null &&
          bindingContext.HttpContext.User.Identity.Name != null &&
          bindingContext.HttpContext.User.Identity.IsAuthenticated)
      {
        var userRepository =
          bindingContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var userEntity =
          await userRepository.GetUserAsync(
            bindingContext.HttpContext.User.Identity.Name,
            bindingContext.HttpContext.RequestAborted);

        if (userEntity != null)
        {
          vm.User.Name = userEntity.Name;
          vm.User.IsAuthenticated = true;
        }
      }

      if (vm is SignInAccountViewModel svm)
      {
        svm.ReturnUrl = "/";
        svm.Email = "test@test.test";
        svm.Password = "test";
      }

      bindingContext.Result = ModelBindingResult.Success(vm);
    }
  }
}
