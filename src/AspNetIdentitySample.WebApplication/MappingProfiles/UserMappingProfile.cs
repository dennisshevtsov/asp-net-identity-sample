using AspNetIdentitySample.ApplicationCore.Entities;
using AspNetIdentitySample.ApplicationCore.Identities;
using AspNetIdentitySample.WebApplication.Extensions;
using AspNetIdentitySample.WebApplication.ViewModels;
using AutoMapper;
using System.Security.Claims;

namespace AspNetIdentitySample.WebApplication.MappingProfiles
{
  public sealed class UserMappingProfile : Profile
  {
    public UserMappingProfile()
    {
      CreateMap<IUserIdentity, ClaimsPrincipal>()
        .ConstructUsing(identity => identity.ToPrincipal());

      CreateMap<ProfileViewModel, UserEntity>()
        .ForMember(entity => entity.Id, options => options.Ignore())
        .ForMember(entity => entity.UserId, options => options.Ignore())
        .ForMember(entity => entity.Email, options => options.Ignore())
        .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.FirstName))
        .ForMember(entity => entity.LastName, options => options.MapFrom(vm => vm.LastName))
        .ForMember(entity => entity.PasswordHash, options => options.Ignore())
        .ForMember(entity => entity.Roles, options => options.Ignore());

      CreateMap<UserEntity, ProfileViewModel>()
        .ForMember(entity => entity.PageTitle, options => options.Ignore())
        .ForMember(entity => entity.User, options => options.Ignore())
        .ForMember(entity => entity.ReturnUrl, options => options.Ignore())
        .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.Email))
        .ForMember(entity => entity.FirstName, options => options.MapFrom(vm => vm.FirstName))
        .ForMember(entity => entity.LastName, options => options.MapFrom(vm => vm.LastName));
    }
  }
}
