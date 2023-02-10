// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Binding.Test
{
  [TestClass]
  public sealed class CheckboxValueConverterTest
  {
#pragma warning disable CS8618
    private CheckboxValueConverter _converter;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _converter = new CheckboxValueConverter();
    }

    [TestMethod]
    public void ConvertFrom_Should_Convert_True_False()
    {
      var value = _converter.ConvertFrom("true,false");

      Assert.IsNotNull(value);
      Assert.IsInstanceOfType(value, typeof(bool));
      Assert.IsTrue((bool)value);
    }

    [TestMethod]
    public void ConvertFrom_Should_Convert_True()
    {
      var value = _converter.ConvertFrom("true");

      Assert.IsNotNull(value);
      Assert.IsInstanceOfType(value, typeof(bool));
      Assert.IsTrue((bool)value);
    }

    [TestMethod]
    public void ConvertFrom_Should_Convert_False()
    {
      var value = _converter.ConvertFrom("false");

      Assert.IsNotNull(value);
      Assert.IsInstanceOfType(value, typeof(bool));
      Assert.IsFalse((bool)value);
    }
  }
}
