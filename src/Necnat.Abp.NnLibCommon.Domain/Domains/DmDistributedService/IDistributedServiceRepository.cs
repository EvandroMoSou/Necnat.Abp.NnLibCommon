using System;
using Volo.Abp.Domain.Repositories;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface IDistributedServiceRepository : IRepository<DistributedService, Guid>
    {

    }
}
