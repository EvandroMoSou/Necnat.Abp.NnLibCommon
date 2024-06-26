using System;
using System.Reflection;

namespace Necnat.Abp.NnLibCommon.Utils
{
    public static class ReflectionUtil
    {
        public static T Clone<T>(T source)
        {
            var destination = Activator.CreateInstance<T>();
            return Clone(source, destination);
        }

        public static T Clone<T>(T source, T destination)
        {
            var fields = destination!.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    var value = field.GetValue(source);
                    field.SetValue(destination, value);
                }
                catch (FieldAccessException) { }
            }

            return destination;
        }
    }
}
