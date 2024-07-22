using AutoMapper;
using Necnat.Abp.NnLibCommon.Domains;
using Necnat.Abp.NnLibCommon.Domains.NnIdentity;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon;

public class NnLibCommonApplicationAutoMapperProfile : Profile
{
    public NnLibCommonApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<DistributedService, DistributedServiceDto>();
        CreateMap<DistributedServiceDto, DistributedService>()
            .ForMember(x => x.LastModificationTime, opt => opt.Ignore())
            .ForMember(x => x.LastModifierId, opt => opt.Ignore())
            .ForMember(x => x.CreationTime, opt => opt.Ignore())
            .ForMember(x => x.CreatorId, opt => opt.Ignore())
            .ForMember(x => x.ExtraProperties, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore());

        CreateMap<IdentityUser, NnIdentityUserDto>()
            .ForMember(x => x.DistributedAppName, opt => opt.Ignore());
        CreateMap<NnIdentityUserDto, IdentityUser>()
            .MapExtraProperties()
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.IsExternal, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.ShouldChangePasswordOnNextLogin, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.Claims, opt => opt.Ignore())
            .ForMember(dest => dest.Logins, opt => opt.Ignore())
            .ForMember(dest => dest.Tokens, opt => opt.Ignore())
            .ForMember(dest => dest.OrganizationUnits, opt => opt.Ignore());

        CreateMap<IdentityRole, NnIdentityRoleDto>()
            .ForMember(x => x.DistributedAppName, opt => opt.Ignore());
        CreateMap<NnIdentityRoleDto, IdentityRole>()
            .MapExtraProperties()
            .ForMember(dest => dest.NormalizedName, opt => opt.Ignore())
            .ForMember(dest => dest.Claims, opt => opt.Ignore())
            .ForMember(dest => dest.EntityVersion, opt => opt.Ignore());
    }
}
