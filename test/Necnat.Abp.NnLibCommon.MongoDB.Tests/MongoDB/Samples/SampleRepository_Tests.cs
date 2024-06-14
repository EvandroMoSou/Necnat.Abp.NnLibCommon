using Necnat.Abp.NnLibCommon.Samples;
using Xunit;

namespace Necnat.Abp.NnLibCommon.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<NnLibCommonMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
