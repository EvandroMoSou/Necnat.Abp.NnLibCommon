using Dapper;
using System;
using System.Linq;
using System.Reflection;

namespace Necnat.Abp.NnLibCommon.Dapper
{
    public static class DapperUtil
    {
        public static void UseAllColumnAttributeTypeMapper(Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(IColumnAttributeTypeMapper))));
            foreach (var typeInfo in typesFromAssemblies)
                SqlMapper.SetTypeMap(typeInfo, new ColumnAttributeTypeMapper(typeInfo));
        }
    }
}
