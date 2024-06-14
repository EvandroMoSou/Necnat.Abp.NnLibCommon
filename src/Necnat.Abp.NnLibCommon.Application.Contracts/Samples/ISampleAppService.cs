using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
