using System.Collections.Generic;
using Newtonsoft.Json;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Default implementation storing agent ids in <see cref="SettingsStore"/>.
    /// Keys are composed using provider and context hashes.
    /// </summary>
    public class AgentSessionManager : IAgentSessionManager
    {
        private const string StoreKey = "agent_sessions";

        private Dictionary<string, string> _cache = Load();

        private static Dictionary<string, string> Load()
        {
            var json = SettingsStore.Get(StoreKey);
            return string.IsNullOrEmpty(json)
                ? new Dictionary<string, string>()
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }

        private void Save()
        {
            SettingsStore.Set(StoreKey, JsonConvert.SerializeObject(_cache));
        }

        private static string GetKey(string provider, IDictionary<string, string> context)
        {
            var ctx = string.Join("|", context);
            return $"{provider}:{ctx.GetHashCode()}";
        }

        public string GetAgentId(string provider, IDictionary<string, string> context)
        {
            var key = GetKey(provider, context);
            return _cache.TryGetValue(key, out var id) ? id : string.Empty;
        }

        public void SetAgentId(string provider, IDictionary<string, string> context, string agentId)
        {
            var key = GetKey(provider, context);
            _cache[key] = agentId;
            Save();
        }
    }
}
