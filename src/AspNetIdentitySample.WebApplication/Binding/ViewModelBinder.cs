// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  public sealed class ViewModelBinder : IModelBinder
  {
    /// <summary>Attempts to bind a model.</summary>
    /// <param name="bindingContext">An object that represents a context that contains operating information for model binding and validation.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      return Task.CompletedTask;
    }
  }
}
