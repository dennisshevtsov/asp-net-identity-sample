// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.Test.Unit.Controllers
{
    using AspNetIdentitySample.WebApplication.ViewModels;

#pragma warning disable CS0659
    public sealed class TestViewModel : ViewModelBase
    {
        public string? StringPropertyForBody { get; set; }

        public Guid? GuidPropertyForBody { get; set; }

        public string? StringPropertyForRoute { get; set; }

        public Guid? GuidPropertyForRoute { get; set; }

        public string? StringPropertyForQueryString { get; set; }

        public Guid? GuidPropertyForQueryString { get; set; }

        public override bool Equals(object? obj)
        {
            return true;
        }
    }
#pragma warning restore CS0659
}
