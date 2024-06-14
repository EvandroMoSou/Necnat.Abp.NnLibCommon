using Dapper;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Necnat.Abp.NnLibCommon.Dapper
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper() : base(
            [
                new CustomPropertyTypeMap(
                    typeof(T),
                    (type, columnName) =>
                        type.GetProperties().FirstOrDefault(prop =>
                                prop.GetCustomAttributes(false)
                                    .OfType<ColumnAttribute>()
                                    .Any(attr => attr.Name == columnName)
                        )!
                ),
                new DefaultTypeMap(typeof(T))
            ])
        {
        }
    }

    public class ColumnAttributeTypeMapper : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper(Type type) : base(
            [
                new CustomPropertyTypeMap(
                    type,
                    (type, columnName) =>
                        type.GetProperties().FirstOrDefault(prop =>
                                prop.GetCustomAttributes(false)
                                    .OfType<ColumnAttribute>()
                                    .Any(attr => attr.Name == columnName)
                        )!
                ),
                new DefaultTypeMap(type)
            ])
        {
        }
    }
}
