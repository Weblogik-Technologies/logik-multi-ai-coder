using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Logik.MultiAiCoder.Engine
{
    public static class SettingsStore
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Logik.MultiAiCoder", "settings.json");

        private static Dictionary<string, string> _store = Load();

        private static Dictionary<string, string> Load()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
            return new Dictionary<string, string>();
        }

        private static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(_store));
        }

        public static void Set(string key, string value)
        {
            _store[key] = value;
            Save();
        }

        public static string Get(string key)
        {
            return _store.TryGetValue(key, out var value) ? value : string.Empty;
        }
    }
}
