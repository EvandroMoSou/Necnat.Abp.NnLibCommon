﻿using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter? writer = null;
            try
            {
                var contentsToWriteToFile = JsonSerializer.Serialize(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader? reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                var json = JsonSerializer.Deserialize<T>(fileContents);
                return json == null ? new T() : json;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
