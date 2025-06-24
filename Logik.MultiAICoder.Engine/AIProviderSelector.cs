using System.Collections.Generic;
using System.Linq;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Allows enabling or disabling specific providers at runtime.
    /// </summary>
    public class AIProviderSelector
    {
        private readonly HashSet<string> _enabled = new();

        public AIProviderSelector(IEnumerable<string> enabled)
        {
            foreach (var e in enabled)
                _enabled.Add(e);
        }

        public bool IsEnabled(string provider)
            => _enabled.Contains(provider);

        public void Enable(string provider) => _enabled.Add(provider);

        public void Disable(string provider) => _enabled.Remove(provider);

        public IEnumerable<string> EnabledProviders() => _enabled.ToList();
    }
}
