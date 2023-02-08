// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Mapping
{
  using AutoMapper;

  using AspNetIdentitySample.ApplicationCore.Entities;
  using AspNetIdentitySample.WebApplication.ViewModels;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class UserListViewModelMapping : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetIdentitySample.WebApplication.Mapping.UserListViewModelMapping"/> class.</summary>
    public UserListViewModelMapping()
    {
      UserListViewModelMapping.ConfigureGetMapping(this);
    }

    private static void ConfigureGetMapping(IProfileExpression expression)
    {
      expression.CreateMap<UserEntity, UserListViewModel.UserViewModel>()
                .ForMember(vm => vm.UserId, options => options.MapFrom(entity => entity.Id))
                .ForMember(vm => vm.Email, options => options.MapFrom(entity => entity.Email))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(entity => entity.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(entity => entity.LastName))
                .AfterMap(UserListViewModelMapping.ConstructRolesString);
    }

    private static void ConstructRolesString(
      UserEntity userEntity, UserListViewModel.UserViewModel viewModel)
    {
      if (userEntity.Roles == null)
      {
        viewModel.Roles = string.Empty;
      }
      else
      {
        viewModel.Roles = string.Join(", ", userEntity.Roles.Select(entity => entity.RoleName!));
      }
    }
  }
}
