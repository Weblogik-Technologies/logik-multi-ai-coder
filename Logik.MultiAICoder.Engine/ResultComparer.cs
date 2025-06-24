using System.Collections.Generic;
using System.Linq;

namespace Logik.MultiAiCoder.Engine
{
    public static class ResultComparer
    {
        public static string Compare(IDictionary<string, string> results)
        {
            return string.Join("\n", results.Select(kv => $"{kv.Key}: {kv.Value}"));
        }
    }
}
