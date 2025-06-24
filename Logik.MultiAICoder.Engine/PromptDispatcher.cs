using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine
{
    public class PromptDispatcher : IPromptDispatcher
    {
        private readonly Dictionary<string, IAIProvider> _clients = new();

        public PromptDispatcher()
        {
            var configs = PromptConfigurationStore.Load();
            foreach (var cfg in configs.OrderBy(c => c.Order))
            {
                RegisterClient($"{cfg.Provider}-{cfg.Model}", CreateClient(cfg));
            }
        }

        private static IAIProvider CreateClient(PromptConfiguration cfg)
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

        public void RegisterClient(string name, IAIProvider client)
        {
            _clients[name] = client;
        }

        public async Task<IList<ResultModel>> DispatchAsync(string prompt, IEnumerable<string>? providers = null)
        {
            var active = providers == null
                ? _clients
                : _clients.Where(kv => providers.Contains(kv.Key));

            var tasks = active.Select(async kv => new ResultModel
            {
                Provider = kv.Value.Provider,
                Model = kv.Value.ModelVersion,
                Content = await kv.Value.ExecuteAsync(prompt)
            });
            return (await Task.WhenAll(tasks)).ToList();
        }

        public Task UpdateContextAsync(string filePath, string content)
        {
            var tasks = _clients.Values.Select(c => c.UpdateContextAsync(filePath, content));
            return Task.WhenAll(tasks);
        }
    }
}
