using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Logik.MultiAICoder.Engine
{
    public static class PromptConfigurationStore
    {
        private const string Key = "prompt_configurations";

        public static List<PromptConfiguration> Load()
        {
            var json = SettingsStore.Get(Key);
            return string.IsNullOrEmpty(json)
                ? new List<PromptConfiguration>()
                : JsonConvert.DeserializeObject<List<PromptConfiguration>>(json) ?? new List<PromptConfiguration>();
        }

        public static void Save(List<PromptConfiguration> configs)
        {
            SettingsStore.Set(Key, JsonConvert.SerializeObject(configs));
        }

        public static bool Add(PromptConfiguration config)
        {
            var configs = Load();
            if (configs.Any(c => c.Provider == config.Provider && c.Model == config.Model))
                return false;
            config.Order = configs.Count;
            configs.Add(config);
            Save(configs);
            return true;
        }

        public static void Move(int oldIndex, int newIndex)
        {
            var configs = Load();
            if (oldIndex < 0 || oldIndex >= configs.Count || newIndex < 0 || newIndex >= configs.Count)
                return;
            var item = configs[oldIndex];
            configs.RemoveAt(oldIndex);
            configs.Insert(newIndex, item);
            for (int i = 0; i < configs.Count; i++)
            {
                configs[i].Order = i;
            }
            Save(configs);
        }
    }
}
