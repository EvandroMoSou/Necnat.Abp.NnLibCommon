using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection UseAllOfType<T>(this IServiceCollection serviceCollection, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(T))));
            foreach (var typeInfo in typesFromAssemblies)
                serviceCollection.Add(new ServiceDescriptor(typeInfo, typeInfo, lifetime));

            return serviceCollection;
        }
    }
}
