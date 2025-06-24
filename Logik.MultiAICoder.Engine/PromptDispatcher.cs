using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logik.MultiAICoder.Engine
{
    public class PromptDispatcher
    {
        private readonly Dictionary<string, IApiClient> _clients = new();

        public PromptDispatcher()
        {
            var configs = PromptConfigurationStore.Load();
            foreach (var cfg in configs.OrderBy(c => c.Order))
            {
                RegisterClient($"{cfg.Provider}-{cfg.Model}", CreateClient(cfg));
            }
        }

        private static IApiClient CreateClient(PromptConfiguration cfg)
        {
            return cfg.Provider.ToLower() switch
            {
                "openai" => new ApiClients.ApiClient_OpenAI(cfg.Model) { ApiKey = cfg.ApiKey },
                "claude" => new ApiClients.ApiClient_Claude(cfg.Model) { ApiKey = cfg.ApiKey },
                "gemini" => new ApiClients.ApiClient_Gemini(cfg.Model) { ApiKey = cfg.ApiKey },
                "azureopenai" => new ApiClients.ApiClient_AzureOpenAI(cfg.Model) { ApiKey = cfg.ApiKey },
                _ => new ApiClients.ApiClient_OpenAI(cfg.Model) { ApiKey = cfg.ApiKey }
            };
        }

        public void RegisterClient(string name, IApiClient client)
        {
            _clients[name] = client;
        }

        public async Task<Dictionary<string, string>> DispatchAsync(string prompt)
        {
            var results = new Dictionary<string, string>();
            foreach (var kv in _clients)
            {
                results[kv.Key] = await kv.Value.ExecuteAsync(prompt);
            }
            return results;
        }

        public Task UpdateContextAsync(string filePath, string content)
        {
            var tasks = _clients.Values.Select(c => c.UpdateContextAsync(filePath, content));
            return Task.WhenAll(tasks);
        }
    }
}
