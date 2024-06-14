using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class NnLibCommonApplicationTestBase<TStartupModule> : NnLibCommonTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
