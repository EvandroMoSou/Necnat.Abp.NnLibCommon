using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class NnLibCommonDomainTestBase<TStartupModule> : NnLibCommonTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
