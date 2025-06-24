using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine.ApiClients
{
    public class ApiClient_Claude : IApiClient
    {
        public string Provider => "Claude";
        public string ModelVersion { get; }
        public string ApiKey { get; set; } = string.Empty;
        private string _contextFile = string.Empty;
        private string _contextContent = string.Empty;

        public ApiClient_Claude(string modelVersion)
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
            return Task.FromResult($"[Claude:{ModelVersion}] {prompt}");
        }
    }
}
