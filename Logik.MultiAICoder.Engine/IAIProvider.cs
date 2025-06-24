using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Represents a generic AI provider capable of receiving a prompt and returning a response.
    /// </summary>
    public interface IAIProvider
    {
        string Provider { get; }
        string ModelVersion { get; }
        string ApiKey { get; set; }
        Task<bool> ValidateApiKeyAsync(string key);
        Task UpdateContextAsync(string filePath, string content);
        Task<string> ExecuteAsync(string prompt);
    }
}
