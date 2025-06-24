using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Logik.MultiAICoder.Engine
{
    public class LocalizationManager
    {
        private readonly ResourceManager _resourceManager;
        public LocalizationManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string GetString(string key, string culture)
        {
            return _resourceManager.GetString(key, new CultureInfo(culture)) ?? key;
        }
    }
}
