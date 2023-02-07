// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping
{
  using AutoMapper;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class ProfileViewModelProfile : Profile
  {
    public ProfileViewModelProfile()
    {
      ProfileViewModelProfile.ConfigureGetMapping(this);
      ProfileViewModelProfile.ConfigureUpdateMapping(this);
    }

    private static void ConfigureGetMapping(IProfileExpression expression)
    {
      expression.CreateMap<UserEntity, ProfileViewModel>()
                .ForMember(entity => entity.PageTitle, options => options.Ignore())
                .ForMember(entity => entity.User, options => options.Ignore())
                .ForMember(entity => entity.ReturnUrl, options => options.Ignore())
                .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.Email))
                .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.FirstName))
                .ForMember(entity => entity.LastName, options => options.MapFrom(vm => vm.LastName));
    }

    private static void ConfigureUpdateMapping(IProfileExpression expression)
    {
      expression.CreateMap<ProfileViewModel, UserEntity>()
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
