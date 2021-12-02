using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DiscordBot.Common
{
    internal static class FileHelper
    {
        public static List<string> EmbeddedTextFileResourceToStringList(string file)
        {
            var rtnList = new List<string>();
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file);
            if (stream == null) throw new NullReferenceException($"Error reading {file}");
            using var sr = new StreamReader(stream);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                rtnList.Add(line);
            }

            return rtnList;
        }

        public static List<T> EmbeddedJsonResourceToList<T>(string file)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file);
            if (stream == null) throw new NullReferenceException($"Error reading {file}");
            using var sr = new StreamReader(stream);
            var json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}