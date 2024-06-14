using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Necnat.Abp.NnLibCommon.MongoDB;

public static class NnLibCommonMongoDbContextExtensions
{
    public static void ConfigureNnLibCommon(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
