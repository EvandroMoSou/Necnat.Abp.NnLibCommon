using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Services
{
    public abstract class AbpDataSeederAppService : ApplicationService
    {
        protected readonly IDataSeeder _dataSeeder;

        protected string AdminEmailPropertyName { get; set; } = IdentityDataSeedContributor.AdminEmailPropertyName;
        protected string AdminEmailDefaultValue { get; set; } = IdentityDataSeedContributor.AdminEmailDefaultValue;
        protected string AdminPasswordPropertyName { get; set; } = IdentityDataSeedContributor.AdminPasswordPropertyName;
        protected string AdminPasswordDefaultValue { get; set; } = IdentityDataSeedContributor.AdminPasswordDefaultValue;

        public AbpDataSeederAppService(IDataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        public virtual async Task SeedAsync()
        {
            await _dataSeeder.SeedAsync(new DataSeedContext(null)
                .WithProperty(AdminEmailPropertyName, AdminEmailDefaultValue)
                .WithProperty(AdminPasswordPropertyName, AdminPasswordDefaultValue)
            );
        }
    }
}
