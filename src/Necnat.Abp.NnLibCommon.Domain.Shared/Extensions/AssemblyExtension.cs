using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class AssemblyExtension
    {
        public static async Task<List<T>?> GetManifestResourceListOfAsync<T>(this Assembly assembly, string embeddedResourcePath)
        {
            List<T>? list;

            using (Stream? stream = assembly.GetManifestResourceStream(embeddedResourcePath))
            {
                if (stream == null)
                    throw new FileNotFoundException();

                using (StreamReader sr = new StreamReader(stream))
                {
                    list = JsonSerializer.Deserialize<List<T>>(await sr.ReadToEndAsync());
                }
            }

            return list;
        }
    }
}
