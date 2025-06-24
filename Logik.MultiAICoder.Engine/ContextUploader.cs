using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Prepares project context summaries or uploads source files. Implementation is simplified.
    /// </summary>
    public class ContextUploader
    {
        public event Action<string, int>? ProgressChanged;

        public async Task UploadAsync(IEnumerable<string> files)
        {
            int total = files is ICollection<string> col ? col.Count : 0;
            int index = 0;
            foreach (var f in files)
            {
                // Simulate upload
                await Task.Delay(10);
                index++;
                ProgressChanged?.Invoke(f, total == 0 ? 0 : index * 100 / total);
            }
        }
    }
}
