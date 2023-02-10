// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding
{
  using System.ComponentModel;
  using System.Globalization;

  /// <summary>Provides a simple API to convert a form checkbox value to a boolean value.</summary>
  public sealed class CheckboxValueConverter : BooleanConverter
  {
    private const string CheckBoxTrue = "true,false";

    /// <summary>Creates a boolean value from an object.</summary>
    /// <param name="context">An object that represents a type description context.</param>
    /// <param name="culture">An object that represents a specific culture.</param>
    /// <param name="value">An object that represents a value to convert.</param>
    /// <returns>An object that represents a result value.</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
      if (value.ToString() == CheckboxValueConverter.CheckBoxTrue)
      {
        return true;
      }

      return base.ConvertFrom(context, culture, value);
    }
  }
}
