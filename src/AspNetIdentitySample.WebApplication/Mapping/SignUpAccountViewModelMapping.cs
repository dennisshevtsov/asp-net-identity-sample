// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping
{
  using AutoMapper;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class SignUpAccountViewModelMapping : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Mapping.SignUpAccountViewModelMapping"/> class.</summary>
    public SignUpAccountViewModelMapping()
    {
      SignUpAccountViewModelMapping.ConfigureMapping(this);
    }

    private static void ConfigureMapping(IProfileExpression expression)
    {
      expression.CreateMap<SignUpAccountViewModel, UserEntity>()
                .ForMember(entity => entity.Id, options => options.Ignore())
                .ForMember(entity => entity.UserId, options => options.Ignore())
                .ForMember(entity => entity.Email, options => options.MapFrom(vm => vm.Email))
                .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.FirstName))
                .ForMember(entity => entity.LastName, options => options.MapFrom(vm => vm.LastName))
                .ForMember(entity => entity.PasswordHash, options => options.Ignore())
                .ForMember(entity => entity.Roles, options => options.Ignore());
    }
  }
}
