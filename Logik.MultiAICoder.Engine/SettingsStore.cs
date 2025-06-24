using System.Collections.Generic;

namespace Logik.MultiAiCoder.Engine
{
    public static class SettingsStore
    {
        private static readonly Dictionary<string, string> _store = new();

        public static void Set(string key, string value)
        {
            _store[key] = value;
        }

        public static string Get(string key)
        {
            return _store.TryGetValue(key, out var value) ? value : string.Empty;
        }
    }
}
