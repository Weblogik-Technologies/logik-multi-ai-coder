using System.Threading.Tasks;

namespace Logik.MultiAICoder.Engine.ApiClients
{
    public class ApiClient_OpenAI : IApiClient
    {
        public string Provider => "OpenAI";
        public string ModelVersion { get; }
        public string ApiKey { get; set; } = string.Empty;
        private string _contextFile = string.Empty;
        private string _contextContent = string.Empty;

        public ApiClient_OpenAI(string modelVersion)
        {
            ModelVersion = modelVersion;
        }

        public Task<bool> ValidateApiKeyAsync(string key)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(key));
        }

        public Task UpdateContextAsync(string filePath, string content)
        {
            _contextFile = filePath;
            _contextContent = content;
            return Task.CompletedTask;
        }

        public Task<string> ExecuteAsync(string prompt)
        {
            // placeholder for real OpenAI call that would use _contextContent
            return Task.FromResult($"[OpenAI:{ModelVersion}] {prompt}");
        }
    }
}
