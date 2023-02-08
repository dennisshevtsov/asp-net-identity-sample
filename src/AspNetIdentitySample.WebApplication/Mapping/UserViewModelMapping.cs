// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping
{
  using AutoMapper;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class UserViewModelMapping : Profile
  {
    public UserViewModelMapping()
    {
      UserViewModelMapping.ConfigureGetMapping(this);
      UserViewModelMapping.ConfigureUpdateMapping(this);
    }

    private static void ConfigureGetMapping(IProfileExpression expression)
    {
      expression.CreateMap<UserEntity, UserViewModel>()
                .ForMember(vm => vm.PageTitle, options => options.Ignore())
                .ForMember(vm => vm.User, options => options.Ignore())
                .ForMember(vm => vm.ReturnUrl, options => options.Ignore())
                .ForMember(vm => vm.Email, options => options.MapFrom(entity => entity.Email))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(entity => entity.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(entity => entity.LastName));
    }

    private static void ConfigureUpdateMapping(IProfileExpression expression)
    {
      expression.CreateMap<UserViewModel, UserEntity>()
                .ForMember(entity => entity.Id, options => options.Ignore())
                .ForMember(entity => entity.UserId, options => options.Ignore())
                .ForMember(entity => entity.Email, options => options.Ignore())
                .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.FirstName))
                .ForMember(entity => entity.LastName, options => options.MapFrom(vm => vm.LastName))
                .ForMember(entity => entity.PasswordHash, options => options.Ignore())
                .ForMember(entity => entity.Roles, options => options.Ignore());
    }
  }
}
