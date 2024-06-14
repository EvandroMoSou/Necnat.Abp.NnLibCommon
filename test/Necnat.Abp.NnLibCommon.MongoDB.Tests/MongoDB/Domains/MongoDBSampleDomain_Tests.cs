using Necnat.Abp.NnLibCommon.Samples;
using Xunit;

namespace Necnat.Abp.NnLibCommon.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<NnLibCommonMongoDbTestModule>
{

}
