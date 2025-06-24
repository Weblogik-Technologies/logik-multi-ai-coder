using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Dispatches a prompt to one or more AI providers in parallel.
    /// </summary>
    public interface IPromptDispatcher
    {
        Task<IList<ResultModel>> DispatchAsync(string prompt, IEnumerable<string>? providers = null);
        Task UpdateContextAsync(string filePath, string content);
    }
}
