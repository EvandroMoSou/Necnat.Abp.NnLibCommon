using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Necnat.Abp.NnLibCommon.JsonConverters;
using System;
using Volo.Abp.Timing;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection ConfigureDateTimeOffset(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Utc;
            });

            return serviceCollection.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new ForceDateTimeOffsetConverter());
                options.JsonSerializerOptions.Converters.Add(new ForceNullableDateTimeOffsetConverter());
            });
        }
    }
}
