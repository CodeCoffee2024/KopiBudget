using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class UserProfile : Profile
    {
        #region Public Constructors

        public UserProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserUpdateProfileDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role!.Name))
                .ReverseMap();

            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Module!.Name))
                .ReverseMap();

            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                    src.UserRoles
                        .SelectMany(ur => ur.Role!.RolePermissions)
                        .Select(rp => new PermissionDto
                        {
                            Name = rp.Permission!.Name,
                            Module = rp.Permission!.Module!.Name
                        })
                        .Distinct()
                ))
                .ReverseMap();

        }

        #endregion Public Constructors
    }
}