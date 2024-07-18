using AutoMapper;
using Necnat.Abp.NnLibCommon.Domains;

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
    }
}
