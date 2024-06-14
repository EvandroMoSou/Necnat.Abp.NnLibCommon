using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Necnat.Abp.NnLibCommon.Utils
{
    public static class JsonUtil
    {
        public static T Clone<T>(T obj)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj))!;
        }

        public static TTo CloneTo<T, TTo>(T obj)
        {
            return JsonSerializer.Deserialize<TTo>(JsonSerializer.Serialize(obj))!;
        }

        public static List<T> RemakeList<T>(List<T> recordList, List<T> remakeList)
        {
            var l = new List<T>();
            foreach (var iRemake in remakeList)
            {
                var substitute = recordList.Where(x => JsonSerializer.Serialize(x) == JsonSerializer.Serialize(iRemake)).FirstOrDefault();
                if (substitute != null)
                {
                    l.Remove(remakeList.Where(x => JsonSerializer.Serialize(x) == JsonSerializer.Serialize(iRemake)).First());
                    l.Add(substitute);
                }
                else
                    l.Add(iRemake);
            }

            return l;
        }
    }
}
