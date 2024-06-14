using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Necnat.Abp.NnLibCommon.MongoDB;

[ConnectionStringName(NnLibCommonDbProperties.ConnectionStringName)]
public interface INnLibCommonMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
