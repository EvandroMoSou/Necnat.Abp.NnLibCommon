using Necnat.Abp.NnLibCommon.MongoDB;
using Necnat.Abp.NnLibCommon.Samples;
using Xunit;

namespace Necnat.Abp.NnLibCommon.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<NnLibCommonMongoDbTestModule>
{

}
