using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointRepository : IRepository<NecnatEndpoint, Guid>
    {

    }
}
