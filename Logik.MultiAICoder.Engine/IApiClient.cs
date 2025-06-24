using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine
{
    public interface IApiClient
    {
        string Provider { get; }
        string ModelVersion { get; }
        string ApiKey { get; set; }
        Task<bool> ValidateApiKeyAsync(string key);
        Task UpdateContextAsync(string filePath, string content);
        Task<string> ExecuteAsync(string prompt);
    }
}
