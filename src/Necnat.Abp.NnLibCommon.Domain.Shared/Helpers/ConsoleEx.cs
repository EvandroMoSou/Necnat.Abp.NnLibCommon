using System;
using System.Text.Json;

namespace Necnat.Abp.NnLibCommon.Helpers
{
    public static class ConsoleEx
    {
        public static void DebugVariable(object? obj)
        {
            Console.WriteLine(JsonSerializer.Serialize(obj));
        }
    }
}